using LuckyEngine.ApiDocGen;

// ---- parse args --------------------------------------------------------------
var cliSources = new List<string>();
string? configPath = null;
string? outputOverride = null;
string? rootOverride = null;

for (int i = 0; i < args.Length; i++)
{
    switch (args[i])
    {
        case "--source" or "-s" when i + 1 < args.Length: cliSources.Add(args[++i]); break;
        case "--config" or "-c" when i + 1 < args.Length: configPath = args[++i]; break;
        case "--output" or "-o" when i + 1 < args.Length: outputOverride = args[++i]; break;
        case "--root" or "-r" when i + 1 < args.Length: rootOverride = args[++i]; break;
        case "--help" or "-h":
            PrintHelp();
            return 0;
        default:
            Console.Error.WriteLine($"Unknown argument: {args[i]}");
            PrintHelp();
            return 2;
    }
}

// ---- locate config -----------------------------------------------------------
configPath ??= FindConfig();
if (configPath is null || !File.Exists(configPath))
{
    Console.Error.WriteLine("Could not find apidocgen.config.json (pass --config <path>).");
    return 2;
}
var cfg = Config.Load(configPath);

// ---- resolve bases & source --------------------------------------------------
string primaryBase = rootOverride is not null ? Path.GetFullPath(rootOverride) : Directory.GetCurrentDirectory();
string repoRootGuess = GuessRepoRoot(AppContext.BaseDirectory) ?? primaryBase;

var sourceRoots = ResolveSources(cfg, cliSources, primaryBase, repoRootGuess);
if (sourceRoots.Count == 0)
{
    Console.Error.WriteLine("Could not find the engine ScriptCore source. Tried:");
    foreach (var c in cliSources.Concat(EnumEnv()).Concat(cfg.SourceRootCandidates))
        Console.Error.WriteLine("  - " + c);
    Console.Error.WriteLine("Pass --source <path-to-Hazel-ScriptCore/Source>, set LUCKYENGINE_SCRIPTCORE, " +
                            "or add a tools/ApiDocGen/apidocgen.local.json with sourceRootCandidates.");
    return 1;
}

string outDir = Path.GetFullPath(Path.Combine(primaryBase, outputOverride ?? cfg.OutputDir));

// ---- scan & write ------------------------------------------------------------
Console.WriteLine($"apidocgen: {cfg.ProjectName} C# -> Markdown");
foreach (var s in sourceRoots) Console.WriteLine($"  source : {s}");
Console.WriteLine($"  output : {outDir}");

var scanner = new SourceScanner(cfg);
IReadOnlyList<DocType> types = Array.Empty<DocType>();
foreach (var root in sourceRoots) types = scanner.Scan(root);

if (types.Count == 0)
{
    Console.Error.WriteLine("No public types matched the include/exclude rules — nothing written.");
    return 1;
}

foreach (var t in types) t.Category = cfg.ResolveCategory(t);
var writer = new MarkdownWriter(cfg, outDir);
int written = writer.Write(types);

// ---- summary -----------------------------------------------------------------
Console.WriteLine($"\nWrote {written} type pages:");
foreach (var grp in types.GroupBy(t => t.Category).OrderBy(g => g.Key.Order))
    Console.WriteLine($"  {grp.Key.Title,-28} {grp.Count(),4}");

int documented = types.Count(t => !t.Doc.IsEmpty);
Console.WriteLine($"\nTypes with a doc comment: {documented}/{types.Count} " +
                  $"({100.0 * documented / types.Count:0.#}%). Members without docs still list signatures.");
return 0;

// ---- helpers -----------------------------------------------------------------
static void PrintHelp()
{
    Console.WriteLine("""
        apidocgen — generate Markdown API reference from LuckyEngine C# source.

        Usage:
          apidocgen [--source <dir>]... [--config <file>] [--output <dir>] [--root <dir>]

          --source, -s   Path to Hazel-ScriptCore/Source (repeatable). Overrides config/env.
          --config, -c   Path to apidocgen.config.json (default: next to the binary, then CWD).
          --output, -o   Output dir for generated Markdown (default: config.outputDir, relative to --root/CWD).
          --root,   -r   Base dir that relative config paths resolve against (default: current directory).

        Source resolution order: --source > $LUCKYENGINE_SCRIPTCORE > config.sourceRootCandidates.
        """);
}

static IEnumerable<string> EnumEnv()
{
    var e = Environment.GetEnvironmentVariable("LUCKYENGINE_SCRIPTCORE");
    if (!string.IsNullOrWhiteSpace(e)) yield return e + "  (from $LUCKYENGINE_SCRIPTCORE)";
}

static string? FindConfig()
{
    var beside = Path.Combine(AppContext.BaseDirectory, "apidocgen.config.json");
    if (File.Exists(beside)) return beside;
    var cwd = Path.Combine(Directory.GetCurrentDirectory(), "apidocgen.config.json");
    if (File.Exists(cwd)) return cwd;
    // tools/ApiDocGen/apidocgen.config.json relative to a guessed repo root
    var guess = GuessRepoRoot(AppContext.BaseDirectory);
    if (guess is not null)
    {
        var p = Path.Combine(guess, "tools", "ApiDocGen", "apidocgen.config.json");
        if (File.Exists(p)) return p;
    }
    return null;
}

static string? GuessRepoRoot(string start)
{
    var dir = new DirectoryInfo(start);
    for (int i = 0; i < 8 && dir is not null; i++, dir = dir.Parent)
    {
        if (File.Exists(Path.Combine(dir.FullName, "requirements.txt")) ||
            File.Exists(Path.Combine(dir.FullName, "mkdocs.yml")))
            return dir.FullName;
    }
    return null;
}

static List<string> ResolveSources(Config cfg, List<string> cliSources, string primaryBase, string repoRootGuess)
{
    var bases = new[] { primaryBase, repoRootGuess };
    var resolved = new List<string>();

    void TryAdd(string candidate)
    {
        if (Path.IsPathRooted(candidate))
        {
            var full = Path.GetFullPath(candidate);
            if (Directory.Exists(full) && !resolved.Contains(full)) resolved.Add(full);
            return;
        }
        foreach (var b in bases)
        {
            var full = Path.GetFullPath(Path.Combine(b, candidate));
            if (Directory.Exists(full) && !resolved.Contains(full)) { resolved.Add(full); return; }
        }
    }

    if (cliSources.Count > 0)
    {
        foreach (var s in cliSources) TryAdd(s);
        return resolved;
    }

    var env = Environment.GetEnvironmentVariable("LUCKYENGINE_SCRIPTCORE");
    if (!string.IsNullOrWhiteSpace(env)) { TryAdd(env); if (resolved.Count > 0) return resolved; }

    foreach (var c in cfg.SourceRootCandidates) { TryAdd(c); if (resolved.Count > 0) break; }
    return resolved;
}
