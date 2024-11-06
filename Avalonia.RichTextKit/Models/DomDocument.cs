using Avalonia.RichTextKit.Models.Inlines;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Avalonia.RichTextKit.Models;

public class DomDocument
{
    private readonly List<TextStyle> _styles = [];
    private readonly List<DomBlock> _blocks = [];

    public string Version { get; set; }
    public DateTime LastModified { get; set; }
    public double Width { get; set; }
    public double LineSpacing { get; set; } = 1.5;
    public double WordSpacing { get; set; } = 1.3;
    public double FontSize { get; set; } = 16;

    internal IReadOnlyList<DomBlock> Blocks => _blocks;


    #region styles

    internal TextStyle GetStyle(int styleIndex)
    {
        if (_styles.Count <= 0)
        {
            _styles.Add(new TextStyle
            {
                BackgroundColor = "#F1F1F1",
                Bold = false,
                ForegroundColor = "#313131",
                Italic = false,
                Underline = false,
            });
        }

        if (_styles.Count < styleIndex)
            return _styles[0];

        return _styles[styleIndex];
    }

    internal int AddStyle(TextStyle style)
    {
        var index = _styles.IndexOf(style);
        if (index != -1)
        {
            return index;
        }
        _styles.Add(style);
        return _styles.Count - 1;
    }

    #endregion

    private void Reset()
    {
        _styles.Clear();
        _styles.Add(new TextStyle
        {
            BackgroundColor = "#F1F1F1",
            Bold = false,
            ForegroundColor = "#313131",
            Italic = false,
            FontSize = 16,
            Underline = false,
        });
        _blocks.Clear();
        _blocks.Add(new DomBlock(this));
    }

    public void LoadFromJson(string jsonString)
    {
        _styles.Clear();
        _blocks.Clear();
        var jsonNode = JsonObject.Parse(jsonString);
        if (jsonNode is JsonNode node)
        {
            Version = jsonNode[nameof(Version)]?.ToString();
            this.Width = 800;
            if (jsonNode["Style"] is JsonArray styleArray)
            {
                foreach (var style in styleArray)
                {
                    var textStyle = style.Deserialize<TextStyle>();
                    this.AddStyle(textStyle);
                }
            }

            if (jsonNode["Content"] is JsonArray contentArray)
            {
                foreach (var item in contentArray)
                {
                    if (item is null)
                        continue;
                    var newBlock = new DomBlock(this);
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
                                            var newInline = new RegularTextInline(this, $"{v}");
                                            newInline.SetStyleIndex(styleIndex);
                                            newBlock.InsertRange(newBlock.Inlines.Count, [newInline]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    this._blocks.Insert(this._blocks.Count, newBlock);
                }

            }
        }
    }


    public string ToJson()
    {

        var styleJson = string.Join(",", _styles.Select(style => $@"
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
                ""Version"": ""{Version}"",
                ""LatestTime"": ""{LastModified}"",
                ""Width"": ""{Width}"",
                ""LineSpacing"": ""{LineSpacing}"",
                ""WordSpacing"": ""{WordSpacing}"",
                ""Style"": [{styleJson}],
            }}
        ";

        return json;
    }

}

/* json format
{
  "Version": "1.3",
  "LatestTime": "2024/11/05 15:48:38",
  "Width": 800,
  "LineSpacing": 1.5,
  "WordSpacing": 1,
  "Style": [
    {
      "Bold": false,
      "Underline": false,
      "Italic": false,
      "ForegroundColor": "#F1F1F1",
      "BackgroundColor": "#313131"
    },
    {
      "Bold": true,
      "Underline": false,
      "Italic": true,
      "ForegroundColor": "#F1F1F1",
      "BackgroundColor": "#313131"
    },
    {
      "Bold": false,
      "Underline": false,
      "Italic": true,
      "ForegroundColor": "#F1F1F1",
      "BackgroundColor": "#313131"
    }
  ],
  "Content": [
    {
      "Type": "block",
      "Children": [
        {
          "Type": "regular",
          "Text": "Hello, World",
          "StyleIndex": 0
        },
        {
          "Type": "regular",
          "Text": "Hello, World",
          "StyleIndex": 1
        }
      ]
    },
    {
      "Type": "block",
      "Children": [
        {
          "Type": "regular",
          "Text": "Hello, World",
          "StyleIndex": 0
        },
        {
          "Type": "regular",
          "Text": "Hello, World",
          "StyleIndex": 2
        }
      ]
    }
  ]
}
*/