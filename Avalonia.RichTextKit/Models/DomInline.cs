using Avalonia.Media;
using Avalonia.Styling;

namespace Avalonia.RichTextKit.Models;

public abstract class DomInline
{
    public abstract double Width { get; }
    
    public abstract double Height { get; }
    
    public int StyleIndex { get; private set; }

    internal virtual void SetStyleIndex(int index)
    {
        StyleIndex = index;
        Invalidate();
    }

    public virtual void Invalidate(){ }

    protected virtual FormattedText CreateFormattedText(DomDocument domDocument, string text)
    {
        var style = domDocument.GetStyle(StyleIndex);
        var formattedText = new FormattedText(text, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, Typeface.Default, style.FontSize, Brush.Parse(style.ForegroundColor));
        if (style.Italic)
            formattedText.SetFontStyle(FontStyle.Italic);

        if (style.Bold)
            formattedText.SetFontWeight(FontWeight.Bold);

        if (style.Underline)
            formattedText.SetTextDecorations(TextDecorations.Underline);
        return formattedText;
    }
}

public readonly struct TextStyle
{
    public double FontSize { get; init; }
    public bool Bold { get; init; } 
    public bool Italic { get; init; }
    public bool Underline { get; init; }
    public string ForegroundColor { get; init; }
    public string BackgroundColor { get; init; }
}

