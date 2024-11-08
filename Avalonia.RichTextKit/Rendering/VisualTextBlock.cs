
using Avalonia.Media;
using Avalonia.RichTextKit.Models.Inlines;

namespace Avalonia.RichTextKit.Rendering;

public class VisualTextBlock(DomBlock domBlock): Visual
{
    public override void Render(DrawingContext dc)
	{
		var x = 0d;
		var y = 10d;
		foreach (var line in domBlock.Lines)
		{
			// dc.DrawLine(new Pen(Brushes.DarkBlue), new Point(0, y), new Point( line.LineWidth, y));
			foreach (var inline in line)
			{
				if (inline is RegularTextInline regularTextInline)
				{
					var style = domBlock.Document.GetStyle(inline.StyleIndex);
					dc.FillRectangle(Brush.Parse(style.BackgroundColor), new Rect(x, y, domBlock.Document.Width, line.LineHeight));
                    dc.DrawText(regularTextInline.FormattedText, new Point(x, y + line.Baseline - inline.Height));
					x += inline.Width * domBlock.Document.WordSpacing;
                }
			}

			// dc.DrawLine(new Pen(Brushes.DarkGreen), new Point(0, y + line.Baseline), new Point( line.LineWidth, y + line.Baseline));
			// dc.DrawLine(new Pen(Brushes.DarkOrchid), new Point(0, y + line.LineHeight), new Point( line.LineWidth, y + line.LineHeight));
			y += line.LineHeight;
            x = 0;
        }
	}
}