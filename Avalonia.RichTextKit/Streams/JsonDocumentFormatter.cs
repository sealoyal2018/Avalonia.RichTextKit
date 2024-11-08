using System.Text.Json;
using System.Text.Json.Nodes;
using Avalonia.RichTextKit.Models.Inlines;

namespace Avalonia.RichTextKit.Streams;

/// <summary>
/// json 格式输出
/// </summary>
public class JsonDocumentFormatter : IDocumentFormatter
{
	public DocumentFormatter Formatter => DocumentFormatter.Json;
	public void Load(DomDocument document, string body)
	{
        var jsonNode = JsonObject.Parse(body);
        if (jsonNode is JsonNode node)
        {
            document.Version = jsonNode[nameof(Version)]?.ToString();
            document.Width = 800;
            if (jsonNode["Style"] is JsonArray styleArray)
            {
                foreach (var style in styleArray)
                {
                    var textStyle = style.Deserialize<TextStyle>();
                    document.AddStyle(textStyle);
                }
            }

            if (jsonNode["Content"] is JsonArray contentArray)
            {
                foreach (var item in contentArray)
                {
                    if (item is null)
                        continue;
                    var newBlock = new DomBlock(document);
                    if (string.Compare(item["Type"]?.AsValue()?.ToString(), "block", true) == 0)
                    {
                        if (item["Children"] is JsonArray children)
                        {
                            foreach (var child in children)
                            {
                                if (child is null) continue;
                                var s = child["Type"]?.AsValue()?.ToJsonString();
                                if (string.Compare(child["Type"]?.ToString(), "regular", true) == 0)
                                {
                                    var styleIndex = 0;
                                    int.TryParse(child["StyleIndex"]?.ToString(), out styleIndex);
                                    if (child["Text"]?.ToString() is string str)
                                    {
                                        foreach (var v in str.ToCharArray())
                                        {
                                            var newInline = new RegularTextInline(document, $"{v}");
                                            newInline.SetStyleIndex(styleIndex);
                                            newBlock.InsertRange(newBlock.Inlines.Count, [newInline]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    document.InsertBlock(document.Blocks.Count, newBlock);
                }

            }
        }
	}

	public string Save(DomDocument document)
	{
        var styleJson = string.Join(",", document.Styles.Select(style => $@"
            {{
                ""Bold"": {style.Bold},
                ""Italic"": {style.Italic},
                ""Underline"": {style.Underline},
                ""ForegroundColor"": {style.ForegroundColor},
                ""BackgroundColor"": {style.BackgroundColor},
            }}
        "));

        var json = $@"
            {{
                ""Version"": ""{document.Version}"",
                ""LatestTime"": ""{document.LastModified}"",
                ""Width"": ""{document.Width}"",
                ""LineSpacing"": ""{document.LineSpacing}"",
                ""WordSpacing"": ""{document.WordSpacing}"",
                ""Style"": [{styleJson}],
            }}
        ";
        return json;
    }
}