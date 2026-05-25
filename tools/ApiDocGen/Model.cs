namespace LuckyEngine.ApiDocGen;

public enum TypeKind { Class, Struct, Interface, Enum, Record, Delegate }

public enum MemberKind { Constructor, Field, Property, Indexer, Method, Operator, Event, EnumMember }

/// <summary>A documented public/protected type extracted from source.</summary>
public sealed class DocType
{
    public required string Name { get; init; }          // "Vector3" (no generic params)
    public required string DisplayName { get; init; }   // "Vector3" or "AnimationSequence<T>"
    public required string FullName { get; init; }      // "Hazel.Vector3" (+ "<T>" stripped)
    public required string Namespace { get; init; }
    public required TypeKind Kind { get; init; }

    public string Signature { get; set; } = "";         // full declaration: "public struct Vector3 : IEquatable<Vector3>"
    public string? BaseType { get; set; }               // simple base type name, if any
    public List<string> Interfaces { get; } = new();
    public bool IsComponent { get; set; }
    public bool IsStatic { get; set; }
    public bool IsAbstract { get; set; }
    public bool IsSealed { get; set; }

    public bool IsObsolete { get; set; }
    public string? ObsoleteMessage { get; set; }

    public XmlDoc Doc { get; set; } = XmlDoc.Empty;
    public List<DocMember> Members { get; } = new();

    public string TopFolder { get; set; } = "";         // first folder under the source root, e.g. "Scene"
    public string SourceFile { get; set; } = "";        // relative source path, for "view source" / debugging

    // Assigned during organization:
    public CategoryRule Category { get; set; } = null!;
    public string Slug { get; set; } = "";              // file slug, e.g. "vector3"
    public string OutputRelPath { get; set; } = "";     // "scene/entity.md" (relative to outputDir)
}

/// <summary>A documented member of a type.</summary>
public sealed class DocMember
{
    public required MemberKind Kind { get; init; }
    public required string Name { get; init; }          // "IsKeyPressed"
    public required string Signature { get; init; }     // "public static bool IsKeyPressed(KeyCode keycode)"
    public string Anchor { get; set; } = "";            // explicit heading id, e.g. "m-iskeypressed"
    public string Heading { get; set; } = "";           // display heading, e.g. "IsKeyPressed(KeyCode)"

    public bool IsStatic { get; set; }
    public bool IsObsolete { get; set; }
    public string? ObsoleteMessage { get; set; }
    public string? ConstValue { get; set; }             // for consts / enum members

    public XmlDoc Doc { get; set; } = XmlDoc.Empty;

    // Stable ordering within a kind group:
    public int SortGroup { get; set; }
}

/// <summary>Parsed XML doc comment, with inline tags already rendered to Markdown.</summary>
public sealed class XmlDoc
{
    public string? Summary { get; set; }
    public string? Remarks { get; set; }
    public string? Returns { get; set; }
    public string? Value { get; set; }
    public List<(string Name, string Text)> Params { get; } = new();
    public List<(string Name, string Text)> TypeParams { get; } = new();
    public List<(string Cref, string Text)> Exceptions { get; } = new();
    public List<string> Examples { get; } = new();
    /// <summary>Raw cref of an &lt;inheritdoc cref="..."/&gt;, if present and resolvable.</summary>
    public string? InheritFrom { get; set; }
    public bool HasInheritDoc { get; set; }

    public static readonly XmlDoc Empty = new();

    public bool IsEmpty =>
        string.IsNullOrWhiteSpace(Summary) && string.IsNullOrWhiteSpace(Remarks) &&
        string.IsNullOrWhiteSpace(Returns) && string.IsNullOrWhiteSpace(Value) &&
        Params.Count == 0 && TypeParams.Count == 0 && Exceptions.Count == 0 && Examples.Count == 0;

    /// <summary>First sentence of the summary, for index tables.</summary>
    public string FirstSentence()
    {
        if (string.IsNullOrWhiteSpace(Summary)) return "";
        var s = Summary.Replace("\n", " ").Trim();
        // Strip code fences / markdown that would break a table cell.
        s = s.Replace("|", "\\|");
        int dot = s.IndexOf(". ", StringComparison.Ordinal);
        if (dot > 0) return s[..(dot + 1)].Trim();
        return s.Length > 160 ? s[..160].TrimEnd() + "…" : s;
    }
}
