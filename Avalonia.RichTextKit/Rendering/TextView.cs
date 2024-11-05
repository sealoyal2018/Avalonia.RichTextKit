using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.RichTextKit.Rendering;

public class TextView : Control
{
    public override void Render(DrawingContext context)
    {
        context.DrawEllipse(Brushes.Peru, null, new Point(50, 50), 30, 50);
    }
}