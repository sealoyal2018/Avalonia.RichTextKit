
using Avalonia.Media;
using Avalonia.RichTextKit.Models.Inlines;

namespace Avalonia.RichTextKit.Rendering;

public class VisualTextBlock(DomBlock domBlock): Visual
{
	public DomBlock DomBlock => domBlock;

    public override void Render(DrawingContext dc)
	{
		var x = 0d;
		var y = 0d;
		foreach (var line in domBlock.Lines)
		{
			foreach (var inline in line)
			{
				if (inline is RegularTextInline regularTextInline)
				{
					var style = domBlock.Document.GetStyle(inline.StyleIndex);

					dc.FillRectangle(Brush.Parse(style.BackgroundColor), new Rect(x, y, inline.Width * domBlock.Document.WordSpacing, line.LineHeight));
                    dc.DrawText(regularTextInline.FormattedText,new Point(x, y));
					x += inline.Width * domBlock.Document.WordSpacing;
                }
			}

			y += line.LineHeight;
            x = 0;
        }
	}
}