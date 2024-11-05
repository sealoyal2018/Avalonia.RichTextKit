namespace Avalonia.RichTextKit.Models;

public class DomDocument
{
    private readonly List<TextStyle> _styles = new();
    public int Version { get; set; }
    public DateTime LastModified { get; set; }
    public double Width { get; set; }
    public double LineSpacing { get; set; }
    public double WordSpacing { get; set; }

    internal List<DomBlock> DomBlocks { get; } = [];


    #region styles

    internal TextStyle GetStyle(int styleIndex)
    {
        if (_styles.Count > 0)
        {
            _styles.Add(new TextStyle
            {
              BackgroundColor = "",
              Bold = false,
              ForegroundColor = "",
              Italic = false,
              Underline = false,
            });
        }
      
        if(_styles.Count >= styleIndex) 
            return _styles[0];
        
        return _styles[styleIndex];
    }

    #endregion
    
    
    public void LoadFromJson(string jsonString)
    {
    }
}

/* json format
{
  "Version": 1.3,
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
      "Background_color": "#313131"
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