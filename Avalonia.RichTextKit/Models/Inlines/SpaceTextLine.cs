using Avalonia.Media;

namespace Avalonia.RichTextKit.Models.Inlines;

public class SpaceTextLine : DomInline
{
    public override double Width { get; }
    public override double Height { get; }

    public SpaceTextLine(DomDocument document)
    {
        var style = document.GetStyle(0);
        var formattedText = new FormattedText(" ", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, Typeface.Default, 12, Brush.Parse(style.ForegroundColor));
        Width = formattedText.Width;
        Height = formattedText.Height;
    }
}