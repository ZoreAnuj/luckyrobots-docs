using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LuckyEngine.ApiDocGen;

/// <summary>Walks the engine's .cs source, extracting the public/protected API as <see cref="DocType"/>s.</summary>
public sealed class SourceScanner
{
    private readonly Config _cfg;
    private readonly List<Regex> _excludeFileRegexes;
    private readonly List<Regex> _excludeTypeRegexes;
    private readonly Dictionary<string, DocType> _byFullName = new(StringComparer.Ordinal);

    public SourceScanner(Config cfg)
    {
        _cfg = cfg;
        _excludeFileRegexes = cfg.ExcludeFileGlobs.Select(GlobToRegex).ToList();
        _excludeTypeRegexes = cfg.ExcludeTypeNamePatterns.Select(GlobToRegex).ToList();
    }

    public IReadOnlyList<DocType> Scan(string sourceRoot)
    {
        var parseOptions = new CSharpParseOptions(
            languageVersion: LanguageVersion.Latest,
            documentationMode: DocumentationMode.Parse,
            preprocessorSymbols: _cfg.PreprocessorSymbols);

        foreach (var file in Directory.EnumerateFiles(sourceRoot, "*.cs", SearchOption.AllDirectories))
        {
            var normalized = file.Replace('\\', '/');
            if (_excludeFileRegexes.Any(r => r.IsMatch(normalized)))
                continue;

            string text;
            try { text = File.ReadAllText(file); }
            catch { continue; }

            var tree = CSharpSyntaxTree.ParseText(text, parseOptions, path: file);
            var root = tree.GetCompilationUnitRoot();
            var rel = Path.GetRelativePath(sourceRoot, file).Replace('\\', '/');
            var topFolder = TopFolderOf(rel);

            foreach (var member in DescendantTypeMembers(root))
                TryAddType(member, rel, topFolder);
        }

        return _byFullName.Values.ToList();
    }

    // Enumerate every type-ish declaration (namespace-level and nested).
    private static IEnumerable<MemberDeclarationSyntax> DescendantTypeMembers(SyntaxNode root)
    {
        foreach (var node in root.DescendantNodes())
        {
            if (node is BaseTypeDeclarationSyntax or DelegateDeclarationSyntax)
                yield return (MemberDeclarationSyntax)node;
        }
    }

    private void TryAddType(MemberDeclarationSyntax decl, string relFile, string topFolder)
    {
        var ns = NamespaceOf(decl);
        if (!IsIncludedNamespace(ns)) return;

        var name = NameOf(decl);
        if (name is null) return;
        if (_excludeTypeRegexes.Any(r => r.IsMatch(name))) return;

        bool topLevel = decl.Parent is BaseNamespaceDeclarationSyntax or CompilationUnitSyntax;
        var modifiers = ModifiersOf(decl);
        if (!IsAccessible(modifiers, isInterfaceMember: false, topLevel: topLevel, isType: true))
            return;

        var (kind, keyword) = KindOf(decl);
        var (displayName, typeParams) = NameAndTypeParams(decl, name);
        var qualifiedName = QualifiedNestedName(decl, displayName);
        var fullName = ns + "." + Regex.Replace(qualifiedName, "<.*?>", ""); // strip generics for the key

        if (_byFullName.TryGetValue(fullName, out var existing))
        {
            // partial type across files — merge members into the first occurrence.
            AddMembers(existing, decl);
            return;
        }

        var doc = XmlDocParser.Parse(decl);
        var (obsolete, obsoleteMsg) = ObsoleteOf(decl);

        var type = new DocType
        {
            Name = name,
            DisplayName = qualifiedName,
            FullName = fullName,
            Namespace = ns,
            Kind = kind,
            IsStatic = modifiers.Any(m => m.IsKind(SyntaxKind.StaticKeyword)),
            IsAbstract = modifiers.Any(m => m.IsKind(SyntaxKind.AbstractKeyword)),
            IsSealed = modifiers.Any(m => m.IsKind(SyntaxKind.SealedKeyword)),
            IsObsolete = obsolete,
            ObsoleteMessage = obsoleteMsg,
            Doc = doc,
            TopFolder = topFolder,
            SourceFile = relFile,
        };

        FillBaseAndInterfaces(type, decl);
        type.Signature = TypeSignature(modifiers, keyword, displayName, decl);
        AddMembers(type, decl);

        _byFullName[fullName] = type;
    }

    private void FillBaseAndInterfaces(DocType type, MemberDeclarationSyntax decl)
    {
        if (decl is not TypeDeclarationSyntax tds || tds.BaseList is null) return;
        bool first = true;
        foreach (var b in tds.BaseList.Types)
        {
            var bn = b.Type.ToString();
            // Heuristic: for class/record the first entry is the base class (unless it's an interface).
            if (first && (type.Kind is TypeKind.Class or TypeKind.Record) && !bn.StartsWith("I", StringComparison.Ordinal))
            {
                type.BaseType = bn;
                if (bn == "Component" || bn.EndsWith(".Component", StringComparison.Ordinal))
                    type.IsComponent = true;
            }
            else
            {
                type.Interfaces.Add(bn);
            }
            first = false;
        }
        // A class whose declared base is literally "Component".
        if (type.BaseType is "Component") type.IsComponent = true;
    }

    private void AddMembers(DocType type, MemberDeclarationSyntax decl)
    {
        if (decl is EnumDeclarationSyntax en)
        {
            foreach (var m in en.Members)
            {
                var d = XmlDocParser.Parse(m);
                var (ob, obm) = ObsoleteOf(m);
                var value = m.EqualsValue is not null ? m.EqualsValue.Value.ToString() : null;
                type.Members.Add(new DocMember
                {
                    Kind = MemberKind.EnumMember,
                    Name = m.Identifier.Text,
                    Signature = m.Identifier.Text + (value is not null ? " = " + value : ""),
                    Doc = d, ConstValue = value, IsObsolete = ob, ObsoleteMessage = obm,
                    SortGroup = 0,
                });
            }
            return;
        }

        if (decl is not TypeDeclarationSyntax tds) return;

        foreach (var m in tds.Members)
        {
            bool isInterface = type.Kind == TypeKind.Interface;
            var mods = ModifiersOf(m);
            // Enum members handled above; nested types handled by the outer descendant walk.
            if (m is BaseTypeDeclarationSyntax or DelegateDeclarationSyntax) continue;

            if (!IsAccessible(mods, isInterface, topLevel: false, isType: false)) continue;

            var built = BuildMember(type, m, mods);
            if (built is not null) type.Members.AddRange(built);
        }
    }

    private IEnumerable<DocMember>? BuildMember(DocType type, MemberDeclarationSyntax m, SyntaxTokenList mods)
    {
        var doc = XmlDocParser.Parse(m);
        var (obsolete, obsoleteMsg) = ObsoleteOf(m);
        bool isStatic = mods.Any(x => x.IsKind(SyntaxKind.StaticKeyword));
        string modText = CleanModifiers(mods);

        DocMember Make(MemberKind kind, string name, string sig, string heading, string? constValue = null) => new()
        {
            Kind = kind, Name = name, Signature = sig.Trim(), Heading = heading,
            Doc = doc, IsStatic = isStatic, IsObsolete = obsolete, ObsoleteMessage = obsoleteMsg,
            ConstValue = constValue, SortGroup = (int)kind,
        };

        switch (m)
        {
            case ConstructorDeclarationSyntax c:
                return One(Make(MemberKind.Constructor, type.Name,
                    $"{modText} {type.Name}({Params(c.ParameterList)})",
                    $"{type.Name}({ParamTypes(c.ParameterList)})"));

            case MethodDeclarationSyntax me:
            {
                var tp = me.TypeParameterList?.ToString() ?? "";
                var cons = me.ConstraintClauses.Count > 0 ? " " + string.Join(" ", me.ConstraintClauses) : "";
                return One(Make(MemberKind.Method, me.Identifier.Text,
                    $"{modText} {me.ReturnType} {me.Identifier.Text}{tp}({Params(me.ParameterList)}){cons}",
                    $"{me.Identifier.Text}{tp}({ParamTypes(me.ParameterList)})"));
            }

            case OperatorDeclarationSyntax op:
                return One(Make(MemberKind.Operator, "operator " + op.OperatorToken.Text,
                    $"{modText} {op.ReturnType} operator {op.OperatorToken.Text}({Params(op.ParameterList)})",
                    $"operator {op.OperatorToken.Text}({ParamTypes(op.ParameterList)})"));

            case ConversionOperatorDeclarationSyntax cv:
                return One(Make(MemberKind.Operator, $"{cv.ImplicitOrExplicitKeyword.Text} operator {cv.Type}",
                    $"{modText} {cv.ImplicitOrExplicitKeyword.Text} operator {cv.Type}({Params(cv.ParameterList)})",
                    $"{cv.ImplicitOrExplicitKeyword.Text} operator {cv.Type}"));

            case PropertyDeclarationSyntax p:
                return One(Make(MemberKind.Property, p.Identifier.Text,
                    $"{modText} {p.Type} {p.Identifier.Text} {PropertyAccessors(p)}".TrimEnd(),
                    p.Identifier.Text));

            case IndexerDeclarationSyntax ix:
                return One(Make(MemberKind.Indexer, "this[]",
                    $"{modText} {ix.Type} this[{Params(ix.ParameterList)}] {PropertyAccessors(ix)}".TrimEnd(),
                    $"this[{ParamTypes(ix.ParameterList)}]"));

            case EventDeclarationSyntax ev:
                return One(Make(MemberKind.Event, ev.Identifier.Text,
                    $"{modText} event {ev.Type} {ev.Identifier.Text}", ev.Identifier.Text));

            case EventFieldDeclarationSyntax evf:
                return evf.Declaration.Variables.Select(v => Make(MemberKind.Event, v.Identifier.Text,
                    $"{modText} event {evf.Declaration.Type} {v.Identifier.Text}", v.Identifier.Text));

            case FieldDeclarationSyntax f:
            {
                bool isConst = mods.Any(x => x.IsKind(SyntaxKind.ConstKeyword));
                return f.Declaration.Variables.Select(v =>
                {
                    string init = (isConst && v.Initializer is not null) ? " = " + v.Initializer.Value : "";
                    return Make(MemberKind.Field, v.Identifier.Text,
                        $"{modText} {f.Declaration.Type} {v.Identifier.Text}{init}", v.Identifier.Text,
                        constValue: isConst ? v.Initializer?.Value.ToString() : null);
                });
            }

            default:
                return null;
        }
    }

    private static IEnumerable<DocMember> One(DocMember m) => new[] { m };

    // ---- signatures & names ------------------------------------------------------

    private string TypeSignature(SyntaxTokenList mods, string keyword, string displayName, MemberDeclarationSyntax decl)
    {
        var sb = new StringBuilder();
        var mod = CleanModifiers(mods);
        if (mod.Length > 0) sb.Append(mod).Append(' ');
        sb.Append(keyword).Append(' ').Append(displayName);

        if (decl is TypeDeclarationSyntax tds && tds.BaseList is { } bl && bl.Types.Count > 0)
            sb.Append(" : ").Append(string.Join(", ", bl.Types.Select(t => t.Type.ToString())));
        if (decl is DelegateDeclarationSyntax del)
        {
            // Re-render the delegate as a full signature.
            sb.Clear();
            if (mod.Length > 0) sb.Append(mod).Append(' ');
            var tp = del.TypeParameterList?.ToString() ?? "";
            sb.Append("delegate ").Append(del.ReturnType).Append(' ').Append(del.Identifier.Text)
              .Append(tp).Append('(').Append(Params(del.ParameterList)).Append(')');
        }
        return sb.ToString();
    }

    private static string Params(BaseParameterListSyntax? list) =>
        list is null ? "" : string.Join(", ", list.Parameters.Select(p => CleanParam(p)));

    private static string CleanParam(ParameterSyntax p)
    {
        // Drop parameter attributes; keep modifiers (ref/out/in/params), type, name, default.
        var mods = string.Join(" ", p.Modifiers.Select(t => t.Text));
        var s = (mods.Length > 0 ? mods + " " : "") + p.Type + " " + p.Identifier.Text;
        if (p.Default is not null) s += " " + p.Default; // "= value"
        return s.Trim();
    }

    private static string ParamTypes(BaseParameterListSyntax? list) =>
        list is null ? "" : string.Join(", ", list.Parameters.Select(p => p.Type?.ToString() ?? ""));

    private static string PropertyAccessors(BasePropertyDeclarationSyntax p)
    {
        if (p is PropertyDeclarationSyntax { ExpressionBody: not null }) return "{ get; }";
        if (p.AccessorList is null) return "";
        var parts = new List<string>();
        foreach (var a in p.AccessorList.Accessors)
        {
            // Hide accessors that aren't part of the public/protected surface.
            if (a.Modifiers.Any(t => t.IsKind(SyntaxKind.PrivateKeyword) || t.IsKind(SyntaxKind.InternalKeyword)))
                continue;
            var mod = string.Join(" ", a.Modifiers.Select(t => t.Text));
            parts.Add((mod.Length > 0 ? mod + " " : "") + a.Keyword.Text + ";");
        }
        return parts.Count == 0 ? "{ get; }" : "{ " + string.Join(" ", parts) + " }";
    }

    private static (string display, string typeParams) NameAndTypeParams(MemberDeclarationSyntax decl, string name)
    {
        string tp = decl switch
        {
            TypeDeclarationSyntax t => t.TypeParameterList?.ToString() ?? "",
            DelegateDeclarationSyntax d => d.TypeParameterList?.ToString() ?? "",
            _ => "",
        };
        return (name + tp, tp);
    }

    private static string QualifiedNestedName(MemberDeclarationSyntax decl, string displayName)
    {
        var prefix = new List<string>();
        for (var p = decl.Parent; p is not null; p = p.Parent)
        {
            if (p is TypeDeclarationSyntax t) prefix.Insert(0, t.Identifier.Text);
        }
        return prefix.Count == 0 ? displayName : string.Join(".", prefix) + "." + displayName;
    }

    private static (TypeKind, string) KindOf(MemberDeclarationSyntax d) => d switch
    {
        InterfaceDeclarationSyntax => (TypeKind.Interface, "interface"),
        EnumDeclarationSyntax => (TypeKind.Enum, "enum"),
        StructDeclarationSyntax => (TypeKind.Struct, "struct"),
        RecordDeclarationSyntax r => (TypeKind.Record,
            r.ClassOrStructKeyword.IsKind(SyntaxKind.StructKeyword) ? "record struct" : "record"),
        DelegateDeclarationSyntax => (TypeKind.Delegate, "delegate"),
        _ => (TypeKind.Class, "class"),
    };

    private static string? NameOf(MemberDeclarationSyntax d) => d switch
    {
        BaseTypeDeclarationSyntax b => b.Identifier.Text,
        DelegateDeclarationSyntax del => del.Identifier.Text,
        _ => null,
    };

    private static SyntaxTokenList ModifiersOf(MemberDeclarationSyntax d) => d switch
    {
        BaseTypeDeclarationSyntax b => b.Modifiers,
        DelegateDeclarationSyntax del => del.Modifiers,
        _ => d.Modifiers,
    };

    // ---- accessibility, namespaces, attributes -----------------------------------

    private bool IsAccessible(SyntaxTokenList mods, bool isInterfaceMember, bool topLevel, bool isType)
    {
        bool pub = mods.Any(m => m.IsKind(SyntaxKind.PublicKeyword));
        bool prot = mods.Any(m => m.IsKind(SyntaxKind.ProtectedKeyword));
        bool priv = mods.Any(m => m.IsKind(SyntaxKind.PrivateKeyword));
        bool intern = mods.Any(m => m.IsKind(SyntaxKind.InternalKeyword));

        if (priv) return false;
        if (pub) return true;
        if (prot) return _cfg.IncludeProtected;          // protected / protected internal
        if (intern) return false;                         // internal-only
        // No access modifier:
        if (isInterfaceMember) return true;               // interface members are public
        return false;                                     // top-level types & class/struct members default to internal/private
    }

    private bool IsIncludedNamespace(string ns)
    {
        if (ns != _cfg.RootNamespace && !ns.StartsWith(_cfg.RootNamespace + ".", StringComparison.Ordinal))
            return false;
        foreach (var ex in _cfg.ExcludeNamespaces)
            if (ns == ex || ns.StartsWith(ex + ".", StringComparison.Ordinal)) return false;
        return true;
    }

    private static string NamespaceOf(SyntaxNode node)
    {
        var parts = new List<string>();
        for (var p = node.Parent; p is not null; p = p.Parent)
        {
            if (p is BaseNamespaceDeclarationSyntax nsd)
                parts.Insert(0, nsd.Name.ToString());
        }
        return string.Join(".", parts);
    }

    private static (bool, string?) ObsoleteOf(MemberDeclarationSyntax d)
    {
        foreach (var list in d.AttributeLists)
            foreach (var attr in list.Attributes)
            {
                var n = attr.Name.ToString();
                if (n is "Obsolete" or "System.Obsolete" or "ObsoleteAttribute")
                {
                    var msg = attr.ArgumentList?.Arguments.FirstOrDefault()?.Expression as LiteralExpressionSyntax;
                    return (true, msg?.Token.ValueText);
                }
            }
        return (false, null);
    }

    private static string CleanModifiers(SyntaxTokenList mods)
    {
        var skip = new HashSet<SyntaxKind> { SyntaxKind.UnsafeKeyword, SyntaxKind.ExternKeyword, SyntaxKind.AsyncKeyword };
        return string.Join(" ", mods.Where(m => !skip.Contains(m.Kind())).Select(m => m.Text));
    }

    private static string TopFolderOf(string relPath)
    {
        var segs = relPath.Split('/');
        // Source layout is "<RootNamespace>/<Folder>/.../File.cs"; the folder after the root ns is the category hint.
        if (segs.Length >= 3) return segs[1];
        return ""; // file sits directly under the root namespace folder
    }

    private static Regex GlobToRegex(string glob)
    {
        var sb = new StringBuilder("^");
        for (int i = 0; i < glob.Length; i++)
        {
            char c = glob[i];
            if (c == '*')
            {
                if (i + 1 < glob.Length && glob[i + 1] == '*') { sb.Append(".*"); i++; }
                else sb.Append("[^/]*");
            }
            else if (c == '?') sb.Append('.');
            else sb.Append(Regex.Escape(c.ToString()));
        }
        sb.Append('$');
        return new Regex(sb.ToString(), RegexOptions.IgnoreCase);
    }
}
