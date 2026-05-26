using System.Text.Json;
using System.Text.Json.Serialization;

namespace LuckyEngine.ApiDocGen;

/// <summary>Generator configuration, loaded from apidocgen.config.json (+ optional local override).</summary>
public sealed class Config
{
    public string ProjectName { get; set; } = "Lucky Engine";
    public string RootNamespace { get; set; } = "Hazel";

    public List<string> SourceRootCandidates { get; set; } = new();
    public string OutputDir { get; set; } = "docs/scripting/api";

    public bool IncludeProtected { get; set; } = true;
    public List<string> PreprocessorSymbols { get; set; } = new();

    public List<string> ExcludeNamespaces { get; set; } = new();
    public List<string> ExcludeFileGlobs { get; set; } = new();
    public List<string> ExcludeTypeNamePatterns { get; set; } = new();

    public List<CategoryRule> Categories { get; set; } = new();

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public static Config Load(string path)
    {
        var json = File.ReadAllText(path);
        var cfg = JsonSerializer.Deserialize<Config>(json, JsonOpts)
                  ?? throw new InvalidOperationException($"Could not parse config: {path}");

        // Merge a machine-local override if present (gitignored), so contributors can point
        // at their own engine checkout without touching the committed config.
        var localPath = Path.Combine(Path.GetDirectoryName(Path.GetFullPath(path))!, "apidocgen.local.json");
        if (File.Exists(localPath))
        {
            var local = JsonSerializer.Deserialize<Config>(File.ReadAllText(localPath), JsonOpts);
            if (local is not null)
            {
                if (local.SourceRootCandidates.Count > 0)
                    cfg.SourceRootCandidates.InsertRange(0, local.SourceRootCandidates);
                if (!string.IsNullOrWhiteSpace(local.OutputDir) && local.OutputDir != "docs/scripting/api")
                    cfg.OutputDir = local.OutputDir;
            }
        }
        return cfg;
    }

    /// <summary>Pick the category for a type: a "components" rule wins for Component-derived
    /// types; otherwise match on the type's top-level source folder; otherwise fall back.</summary>
    public CategoryRule ResolveCategory(DocType type)
    {
        if (type.IsComponent)
        {
            var comp = Categories.FirstOrDefault(c => c.Components);
            if (comp is not null) return comp;
        }
        var byFolder = Categories.FirstOrDefault(c => c.Folders.Contains(type.TopFolder, StringComparer.OrdinalIgnoreCase));
        if (byFolder is not null) return byFolder;

        return Fallback;
    }

    private CategoryRule? _fallback;
    public CategoryRule Fallback => _fallback ??= new CategoryRule
    {
        Slug = "other", Title = "Other", Order = 9999,
        Summary = "Additional public types."
    };
}

public sealed class CategoryRule
{
    public string Slug { get; set; } = "";
    public string Title { get; set; } = "";
    public int Order { get; set; }
    public List<string> Folders { get; set; } = new();
    /// <summary>If true, all Component-derived types are routed to this category.</summary>
    public bool Components { get; set; }
    public string? Summary { get; set; }
}
