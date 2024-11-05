using Avalonia.Media;

namespace Avalonia.RichTextKit.Models;

public abstract class DomInline
{
    public TextStyle Style { get; } = new();
    
    public abstract double Width { get; }
    
    public abstract double Height { get; }
    
    public int StyleIndex { get; set; }

    public virtual void Invalidate(){ }

}

public readonly struct TextStyle
{
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public bool Underline { get; init; }
    public string ForegroundColor { get; init; }
    public string BackgroundColor { get; init; }
}

