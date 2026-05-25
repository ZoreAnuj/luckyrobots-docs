using System.Text;
using System.Text.RegularExpressions;

namespace LuckyEngine.ApiDocGen;

/// <summary>Turns the scanned <see cref="DocType"/> set into a Markdown tree under the output dir.</summary>
public sealed class MarkdownWriter
{
    private readonly Config _cfg;
    private readonly string _outDir;
    private readonly Dictionary<string, DocType> _byFull = new(StringComparer.Ordinal);
    private readonly Dictionary<string, List<DocType>> _bySimple = new(StringComparer.Ordinal);

    private static readonly Regex CrefToken =
        new($"{XmlDocParser.Sep}{XmlDocParser.CrefTag}{XmlDocParser.Sep}([^{XmlDocParser.Sep}]*){XmlDocParser.Sep}([^{XmlDocParser.Sep}]*){XmlDocParser.Sep}");

    public MarkdownWriter(Config cfg, string outDir)
    {
        _cfg = cfg;
        _outDir = outDir;
    }

    public int Write(IReadOnlyList<DocType> types)
    {
        Organize(types);

        if (Directory.Exists(_outDir)) Directory.Delete(_outDir, recursive: true);
        Directory.CreateDirectory(_outDir);

        var categories = types.Select(t => t.Category).Distinct().OrderBy(c => c.Order).ToList();

        foreach (var cat in categories)
        {
            var catTypes = types.Where(t => t.Category == cat)
                                .OrderBy(t => t.DisplayName, StringComparer.OrdinalIgnoreCase).ToList();
            var catDir = Path.Combine(_outDir, cat.Slug);
            Directory.CreateDirectory(catDir);

            WriteCategoryPages(cat, catTypes, catDir);
            foreach (var t in catTypes)
                File.WriteAllText(Path.Combine(_outDir, t.OutputRelPath), RenderType(t));
        }

        WriteApiIndex(categories, types);
        return types.Count;
    }

    // ---- organization & indexes --------------------------------------------------

    private void Organize(IReadOnlyList<DocType> types)
    {
        foreach (var t in types)
        {
            _byFull[t.FullName] = t;
            (_bySimple.TryGetValue(t.Name, out var list) ? list : _bySimple[t.Name] = new()).Add(t);
        }

        var usedSlugs = new Dictionary<string, HashSet<string>>(); // category slug -> used file slugs
        foreach (var t in types)
        {
            t.Category = _cfg.ResolveCategory(t);
            var baseSlug = Slugify(t.DisplayName.Replace('.', '-'));
            var used = usedSlugs.TryGetValue(t.Category.Slug, out var s) ? s : usedSlugs[t.Category.Slug] = new();
            var slug = baseSlug;
            for (int i = 2; used.Contains(slug); i++) slug = $"{baseSlug}-{i}";
            used.Add(slug);
            t.Slug = slug;
            t.OutputRelPath = $"{t.Category.Slug}/{slug}.md";

            AssignMemberAnchors(t);
        }
    }

    private static void AssignMemberAnchors(DocType t)
    {
        var used = new HashSet<string>();
        foreach (var m in t.Members)
        {
            var baseAnchor = "m-" + Slugify(m.Name);
            var anchor = baseAnchor;
            for (int i = 2; used.Contains(anchor); i++) anchor = $"{baseAnchor}-{i}";
            used.Add(anchor);
            m.Anchor = anchor;
        }
    }

    private void WriteApiIndex(List<CategoryRule> categories, IReadOnlyList<DocType> types)
    {
        var sb = new StringBuilder();
        sb.AppendLine("# API Reference").AppendLine();
        sb.AppendLine($"Auto-generated reference for the **{_cfg.ProjectName}** C# scripting API ")
          .AppendLine("(namespace `" + _cfg.RootNamespace + "`). Browse by area:").AppendLine();

        foreach (var cat in categories)
        {
            int count = types.Count(t => t.Category == cat);
            sb.AppendLine($"## [{cat.Title}]({cat.Slug}/index.md)").AppendLine();
            if (!string.IsNullOrWhiteSpace(cat.Summary)) sb.AppendLine(cat.Summary).AppendLine();
            sb.AppendLine($"*{count} type{(count == 1 ? "" : "s")}*").AppendLine();
        }

        File.WriteAllText(Path.Combine(_outDir, "index.md"), sb.ToString());

        // Top-level nav for the API tree (awesome-pages plugin).
        var pages = new StringBuilder();
        pages.AppendLine("title: API Reference");
        pages.AppendLine("nav:");
        pages.AppendLine("  - index.md");
        foreach (var cat in categories) pages.AppendLine($"  - {cat.Slug}");
        File.WriteAllText(Path.Combine(_outDir, ".pages"), pages.ToString());
    }

    private void WriteCategoryPages(CategoryRule cat, List<DocType> catTypes, string catDir)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"# {cat.Title}").AppendLine();
        if (!string.IsNullOrWhiteSpace(cat.Summary)) sb.AppendLine(cat.Summary).AppendLine();

        sb.AppendLine("| Type | Kind | Summary |");
        sb.AppendLine("|------|------|---------|");
        foreach (var t in catTypes)
        {
            var first = FirstSentence(Render(t.Doc.Summary, t));
            sb.AppendLine($"| [{t.DisplayName}]({t.Slug}.md) | `{KindWord(t)}` | {first} |");
        }
        File.WriteAllText(Path.Combine(catDir, "index.md"), sb.ToString());

        var pages = new StringBuilder();
        pages.AppendLine($"title: {cat.Title}");
        pages.AppendLine("nav:");
        pages.AppendLine("  - index.md");
        pages.AppendLine("  - ...");
        File.WriteAllText(Path.Combine(catDir, ".pages"), pages.ToString());
    }

    // ---- per-type page -----------------------------------------------------------

    private string RenderType(DocType t)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"# {t.DisplayName}").AppendLine();

        // Subtitle / badges line.
        var badges = new List<string> { "`" + KindWord(t) + "`" };
        badges.Add("namespace `" + t.Namespace + "`");
        if (t.IsComponent) badges.Add("**Component**");
        sb.AppendLine(string.Join(" · ", badges)).AppendLine();

        if (t.IsObsolete)
            sb.AppendLine($"!!! warning \"Obsolete\"").AppendLine($"    {t.ObsoleteMessage ?? "This type is obsolete."}").AppendLine();

        if (!string.IsNullOrWhiteSpace(t.Doc.Summary))
            sb.AppendLine(Render(t.Doc.Summary, t)).AppendLine();

        // Inheritance line.
        var inh = new List<string>();
        if (t.BaseType is not null) inh.Add("Inherits " + TypeRefInline(t.BaseType, t));
        if (t.Interfaces.Count > 0)
            inh.Add("Implements " + string.Join(", ", t.Interfaces.Select(i => TypeRefInline(i, t))));
        if (inh.Count > 0) sb.AppendLine(string.Join(" · ", inh)).AppendLine();

        if (!string.IsNullOrWhiteSpace(t.Doc.Remarks))
            sb.AppendLine(Render(t.Doc.Remarks, t)).AppendLine();

        sb.AppendLine("```csharp").AppendLine(t.Signature).AppendLine("```").AppendLine();

        if (t.Kind == TypeKind.Enum)
        {
            RenderEnumValues(sb, t);
        }
        else
        {
            RenderMemberGroup(sb, t, MemberKind.Constructor, "Constructors");
            RenderFieldTable(sb, t);
            RenderMemberGroup(sb, t, MemberKind.Property, "Properties");
            RenderMemberGroup(sb, t, MemberKind.Indexer, "Indexers");
            RenderMemberGroup(sb, t, MemberKind.Method, "Methods");
            RenderMemberGroup(sb, t, MemberKind.Operator, "Operators");
            RenderMemberGroup(sb, t, MemberKind.Event, "Events");
        }

        if (t.Doc.Examples.Count > 0)
        {
            sb.AppendLine("## Examples").AppendLine();
            foreach (var ex in t.Doc.Examples) sb.AppendLine(Render(ex, t)).AppendLine();
        }

        sb.AppendLine().AppendLine("---").AppendLine($"<small>Source: `{t.SourceFile}`</small>");
        return sb.ToString();
    }

    private void RenderEnumValues(StringBuilder sb, DocType t)
    {
        sb.AppendLine("## Values").AppendLine();
        sb.AppendLine("| Name | Value | Description |");
        sb.AppendLine("|------|-------|-------------|");
        foreach (var m in t.Members) // preserve declaration order for enums
        {
            var desc = FirstSentence(Render(m.Doc.Summary, t));
            // Anchor span (md_in_html) so cross-references to a specific enum value resolve.
            sb.AppendLine($"| <span id=\"{m.Anchor}\">`{m.Name}`</span> | `{m.ConstValue ?? ""}` | {desc} |");
        }
        sb.AppendLine();
    }

    private void RenderFieldTable(StringBuilder sb, DocType t)
    {
        var fields = t.Members.Where(m => m.Kind == MemberKind.Field)
                              .OrderBy(m => m.Name, StringComparer.OrdinalIgnoreCase).ToList();
        if (fields.Count == 0) return;
        sb.AppendLine("## Fields").AppendLine();
        sb.AppendLine("| Field | Description |");
        sb.AppendLine("|-------|-------------|");
        foreach (var m in fields)
        {
            var desc = FirstSentence(Render(m.Doc.Summary, t));
            if (m.IsObsolete) desc = "**(obsolete)** " + desc;
            sb.AppendLine($"| <code>{HtmlInline(m.Signature)}</code> | {desc} |");
        }
        sb.AppendLine();
    }

    private void RenderMemberGroup(StringBuilder sb, DocType t, MemberKind kind, string title)
    {
        var members = t.Members.Where(m => m.Kind == kind)
                               .OrderBy(m => m.Name, StringComparer.OrdinalIgnoreCase)
                               .ThenBy(m => m.Signature, StringComparer.Ordinal).ToList();
        if (members.Count == 0) return;

        sb.AppendLine($"## {title}").AppendLine();
        foreach (var m in members)
        {
            var flags = new List<string>();
            if (m.IsStatic) flags.Add("`static`");
            if (m.IsObsolete) flags.Add("`obsolete`");
            var flagStr = flags.Count > 0 ? " " + string.Join(" ", flags) : "";

            sb.AppendLine($"### {m.Heading} {{#{m.Anchor}}}").AppendLine();
            if (flagStr.Length > 0) sb.AppendLine(flagStr.Trim()).AppendLine();
            if (m.IsObsolete && m.ObsoleteMessage is not null)
                sb.AppendLine($"!!! warning \"Obsolete\"").AppendLine($"    {m.ObsoleteMessage}").AppendLine();

            sb.AppendLine("```csharp").AppendLine(m.Signature).AppendLine("```").AppendLine();

            if (!string.IsNullOrWhiteSpace(m.Doc.Summary))
                sb.AppendLine(Render(m.Doc.Summary, t)).AppendLine();

            if (m.Doc.TypeParams.Count > 0)
            {
                sb.AppendLine("**Type parameters**").AppendLine();
                sb.AppendLine("| Name | Description |").AppendLine("|------|-------------|");
                foreach (var (n, d) in m.Doc.TypeParams)
                    sb.AppendLine($"| `{n}` | {InlineCell(Render(d, t))} |");
                sb.AppendLine();
            }

            if (m.Doc.Params.Count > 0)
            {
                sb.AppendLine("**Parameters**").AppendLine();
                sb.AppendLine("| Name | Description |").AppendLine("|------|-------------|");
                foreach (var (n, d) in m.Doc.Params)
                    sb.AppendLine($"| `{n}` | {InlineCell(Render(d, t))} |");
                sb.AppendLine();
            }

            if (!string.IsNullOrWhiteSpace(m.Doc.Returns))
                sb.AppendLine("**Returns** — " + Render(m.Doc.Returns, t).Replace("\n", " ").Trim()).AppendLine();

            if (m.Doc.Exceptions.Count > 0)
            {
                sb.AppendLine("**Exceptions**").AppendLine();
                foreach (var (cref, d) in m.Doc.Exceptions)
                    sb.AppendLine($"- {TypeRefInline(cref, t)} — {InlineCell(Render(d, t))}");
                sb.AppendLine();
            }

            if (!string.IsNullOrWhiteSpace(m.Doc.Remarks))
                sb.AppendLine(Render(m.Doc.Remarks, t)).AppendLine();

            foreach (var ex in m.Doc.Examples)
                sb.AppendLine(Render(ex, t)).AppendLine();
        }
    }

    // ---- cref resolution & rendering ---------------------------------------------

    /// <summary>Resolve cref tokens in a parsed doc string into relative Markdown links from <paramref name="page"/>.</summary>
    private string Render(string? text, DocType page)
    {
        if (string.IsNullOrEmpty(text)) return "";
        return CrefToken.Replace(text, m => ResolveCref(m.Groups[1].Value, m.Groups[2].Value, page));
    }

    private string ResolveCref(string cref, string label, DocType page)
    {
        cref = cref.Trim();
        string display = cref.Replace('{', '<').Replace('}', '>');

        string namePart = cref;
        int paren = namePart.IndexOf('(');
        if (paren >= 0) namePart = namePart[..paren];
        namePart = namePart.Trim();

        string lookup = Regex.Replace(namePart, @"[<{].*$", "");
        lookup = lookup.Split('`')[0];

        var type = ResolveType(lookup);
        string? anchor = null;
        string text;

        if (type is not null)
        {
            text = string.IsNullOrEmpty(label) ? LastSeg(display) : label;
        }
        else
        {
            int dot = lookup.LastIndexOf('.');
            if (dot > 0)
            {
                var typeName = lookup[..dot];
                var memberName = lookup[(dot + 1)..];
                type = ResolveType(typeName);
                if (type is not null)
                {
                    var mem = type.Members.FirstOrDefault(x => x.Name == memberName || x.Name == "operator " + memberName);
                    anchor = mem?.Anchor;
                    text = string.IsNullOrEmpty(label) ? $"{LastSeg(typeName)}.{memberName}" : label;
                }
                else text = string.IsNullOrEmpty(label) ? display : label;
            }
            else text = string.IsNullOrEmpty(label) ? display : label;
        }

        if (type is not null && type != page)
        {
            var rel = RelLink(page.OutputRelPath, type.OutputRelPath);
            if (anchor is not null) rel += "#" + anchor;
            return $"[`{text}`]({rel})";
        }
        if (type == page && anchor is not null)
            return $"[`{text}`](#{anchor})";
        return $"`{text}`";
    }

    /// <summary>Render a (possibly external) type name as an inline link if it's in our doc set.</summary>
    private string TypeRefInline(string typeName, DocType page)
    {
        string lookup = Regex.Replace(typeName, @"[<{].*$", "").Split('`')[0].Trim();
        var t = ResolveType(lookup);
        var display = typeName.Replace('{', '<').Replace('}', '>');
        if (t is not null && t != page)
            return $"[`{display}`]({RelLink(page.OutputRelPath, t.OutputRelPath)})";
        return $"`{display}`";
    }

    private DocType? ResolveType(string name)
    {
        if (_byFull.TryGetValue(name, out var t)) return t;
        if (_bySimple.TryGetValue(name, out var list)) return list[0];
        int dot = name.LastIndexOf('.');
        if (dot > 0 && _bySimple.TryGetValue(name[(dot + 1)..], out var l2)) return l2[0];
        return null;
    }

    // ---- small helpers -----------------------------------------------------------

    private static string KindWord(DocType t) => t.Kind switch
    {
        TypeKind.Struct => "struct",
        TypeKind.Interface => "interface",
        TypeKind.Enum => "enum",
        TypeKind.Record => "record",
        TypeKind.Delegate => "delegate",
        _ => t.IsStatic ? "static class" : t.IsAbstract ? "abstract class" : "class",
    };

    private static string RelLink(string fromRel, string toRel)
    {
        var fromDir = Path.GetDirectoryName(fromRel);
        var rel = Path.GetRelativePath(string.IsNullOrEmpty(fromDir) ? "." : fromDir, toRel);
        return rel.Replace('\\', '/');
    }

    private static string LastSeg(string s)
    {
        int i = s.LastIndexOf('.');
        return i >= 0 ? s[(i + 1)..] : s;
    }

    private static string Slugify(string s)
    {
        s = s.ToLowerInvariant();
        s = Regex.Replace(s, "[^a-z0-9]+", "-").Trim('-');
        return s.Length == 0 ? "x" : s;
    }

    /// <summary>Collapse a rendered block to a single safe table-cell line.</summary>
    private static string InlineCell(string s) =>
        Regex.Replace(s, @"\s+", " ").Replace("|", "\\|").Trim();

    private static string HtmlInline(string s) =>
        s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("|", "\\|");

    private static string FirstSentence(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return "";
        s = Regex.Replace(s, @"\s+", " ").Replace("|", "\\|").Trim();
        int dot = s.IndexOf(". ", StringComparison.Ordinal);
        if (dot > 0) return s[..(dot + 1)].Trim();
        return s.Length > 160 ? s[..160].TrimEnd() + "…" : s;
    }
}
