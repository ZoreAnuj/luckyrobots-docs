using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LuckyEngine.ApiDocGen;

/// <summary>
/// Parses a C# XML documentation comment into structured Markdown. Inline cross-references
/// (&lt;see cref="..."/&gt;) are emitted as <c>CREFcreflabel</c> tokens;
/// MarkdownWriter resolves them to relative links once it knows the page being written.
/// </summary>
public static class XmlDocParser
{
    public const char Sep = '';
    public const string CrefTag = "CREF";

    public static XmlDoc Parse(SyntaxNode node)
    {
        var raw = ExtractRawDocXml(node);
        return string.IsNullOrWhiteSpace(raw) ? XmlDoc.Empty : ParseXml(raw!);
    }

    private static string? ExtractRawDocXml(SyntaxNode node)
    {
        foreach (var trivia in node.GetLeadingTrivia())
        {
            if (trivia.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) ||
                trivia.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia))
            {
                if (trivia.GetStructure() is DocumentationCommentTriviaSyntax doc)
                    return StripExterior(doc.ToFullString());
            }
        }
        return null;
    }

    /// <summary>Remove the leading <c>///</c> or <c>/** * */</c> markers, preserving inner indentation.</summary>
    private static string StripExterior(string text)
    {
        var sb = new StringBuilder();
        foreach (var rawLine in text.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n'))
        {
            var line = rawLine;
            var trimmed = line.TrimStart();
            int leading = line.Length - trimmed.Length;

            if (trimmed.StartsWith("///")) line = trimmed[3..];
            else if (trimmed.StartsWith("/**")) line = trimmed[3..];
            else if (trimmed.StartsWith("*/")) { continue; }
            else if (trimmed.StartsWith("*")) line = trimmed[1..];
            else line = rawLine; // keep raw (preserves indentation inside <code>)

            if (line.StartsWith(" ")) line = line[1..]; // one conventional space after the marker
            sb.Append(line).Append('\n');
            _ = leading;
        }
        return sb.ToString();
    }

    private static XmlDoc ParseXml(string inner)
    {
        XElement root;
        try
        {
            root = XElement.Parse("<doc>" + inner + "</doc>", LoadOptions.PreserveWhitespace);
        }
        catch
        {
            // Malformed XML (stray '<', unescaped '&', etc.) — degrade gracefully to plain text.
            var stripped = Regex.Replace(inner, "<[^>]+>", " ");
            stripped = WebUtilityDecode(stripped);
            return new XmlDoc { Summary = NormalizeText(stripped) };
        }

        var doc = new XmlDoc();
        foreach (var el in root.Elements())
        {
            switch (el.Name.LocalName.ToLowerInvariant())
            {
                case "summary": doc.Summary = RenderBlock(el); break;
                case "remarks": doc.Remarks = RenderBlock(el); break;
                case "returns": doc.Returns = RenderBlock(el); break;
                case "value": doc.Value = RenderBlock(el); break;
                case "param":
                    doc.Params.Add((el.Attribute("name")?.Value ?? "", RenderBlock(el))); break;
                case "typeparam":
                    doc.TypeParams.Add((el.Attribute("name")?.Value ?? "", RenderBlock(el))); break;
                case "exception":
                    doc.Exceptions.Add((StripCrefPrefix(el.Attribute("cref")?.Value ?? ""), RenderBlock(el))); break;
                case "example":
                    doc.Examples.Add(RenderBlock(el)); break;
                case "inheritdoc":
                    doc.HasInheritDoc = true;
                    doc.InheritFrom = el.Attribute("cref")?.Value is { } c ? StripCrefPrefix(c) : null;
                    break;
            }
        }
        return doc;
    }

    // ---- inline / block rendering ------------------------------------------------

    private static string RenderBlock(XElement el)
    {
        var sb = new StringBuilder();
        RenderChildren(el, sb);
        // Collapse 3+ newlines, trim.
        var s = Regex.Replace(sb.ToString(), "\n{3,}", "\n\n").Trim();
        return s;
    }

    private static void RenderChildren(XElement el, StringBuilder sb)
    {
        foreach (var node in el.Nodes())
            RenderNode(node, sb);
    }

    private static void RenderNode(XNode node, StringBuilder sb)
    {
        switch (node)
        {
            case XText t:
                sb.Append(EscapeText(NormalizeText(t.Value)));
                break;
            case XElement e:
                RenderElement(e, sb);
                break;
        }
    }

    private static void RenderElement(XElement e, StringBuilder sb)
    {
        switch (e.Name.LocalName.ToLowerInvariant())
        {
            case "see":
            case "seealso":
            {
                var cref = e.Attribute("cref")?.Value;
                var langword = e.Attribute("langword")?.Value;
                var href = e.Attribute("href")?.Value;
                var label = e.Value?.Trim() ?? "";
                if (!string.IsNullOrEmpty(cref))
                    sb.Append(Sep).Append(CrefTag).Append(Sep).Append(StripCrefPrefix(cref)).Append(Sep).Append(label).Append(Sep);
                else if (!string.IsNullOrEmpty(langword))
                    sb.Append('`').Append(langword).Append('`');
                else if (!string.IsNullOrEmpty(href))
                    sb.Append('[').Append(string.IsNullOrEmpty(label) ? href : label).Append("](").Append(href).Append(')');
                break;
            }
            case "paramref":
            case "typeparamref":
                sb.Append('`').Append(e.Attribute("name")?.Value ?? "").Append('`');
                break;
            case "c":
                sb.Append('`').Append(NormalizeText(e.Value).Trim()).Append('`');
                break;
            case "code":
            {
                var code = Dedent(e.Value);
                if (code.Contains('\n'))
                {
                    var lang = e.Attribute("language")?.Value ?? "csharp";
                    sb.Append("\n\n```").Append(lang).Append('\n').Append(code).Append("\n```\n\n");
                }
                else
                {
                    sb.Append('`').Append(code.Trim()).Append('`'); // single-line <code> reads better inline
                }
                break;
            }
            case "para":
                sb.Append("\n\n");
                RenderChildren(e, sb);
                sb.Append("\n\n");
                break;
            case "br":
                sb.Append("  \n");
                break;
            case "b":
            case "strong":
                sb.Append("**"); RenderChildren(e, sb); sb.Append("**");
                break;
            case "i":
            case "em":
                sb.Append('*'); RenderChildren(e, sb); sb.Append('*');
                break;
            case "list":
            {
                sb.Append('\n');
                bool numbered = (e.Attribute("type")?.Value ?? "") == "number";
                int i = 1;
                foreach (var item in e.Elements())
                {
                    if (!item.Name.LocalName.Equals("item", StringComparison.OrdinalIgnoreCase) &&
                        !item.Name.LocalName.Equals("listheader", StringComparison.OrdinalIgnoreCase))
                        continue;
                    var term = item.Element("term");
                    var desc = item.Element("description");
                    var bullet = numbered ? $"{i++}. " : "- ";
                    sb.Append('\n').Append(bullet);
                    if (term is not null) { sb.Append("**"); RenderChildren(term, sb); sb.Append("** — "); }
                    if (desc is not null) RenderChildren(desc, sb);
                    else if (term is null) RenderChildren(item, sb);
                }
                sb.Append('\n');
                break;
            }
            default:
                // Unknown wrapper element — render its children.
                RenderChildren(e, sb);
                break;
        }
    }

    // ---- helpers -----------------------------------------------------------------

    private static string NormalizeText(string s) => Regex.Replace(s, @"\s+", " ");

    private static string EscapeText(string s) =>
        s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");

    private static string Dedent(string s)
    {
        var lines = s.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n').ToList();
        while (lines.Count > 0 && lines[0].Trim().Length == 0) lines.RemoveAt(0);
        while (lines.Count > 0 && lines[^1].Trim().Length == 0) lines.RemoveAt(lines.Count - 1);
        if (lines.Count == 0) return "";
        int min = lines.Where(l => l.Trim().Length > 0)
                       .Select(l => l.Length - l.TrimStart().Length)
                       .DefaultIfEmpty(0).Min();
        return string.Join("\n", lines.Select(l => l.Length >= min ? l[min..] : l));
    }

    private static string StripCrefPrefix(string cref) =>
        cref.Length > 2 && cref[1] == ':' ? cref[2..] : cref;

    private static string WebUtilityDecode(string s) =>
        s.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&").Replace("&quot;", "\"");
}
