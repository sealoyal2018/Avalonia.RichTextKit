
using Avalonia.Media;
using Avalonia.RichTextKit.Models;
using Avalonia.RichTextKit.Models.Inlines;

namespace Avalonia.RichTextKit.Rendering;

public class VisualTextBlock(DomBlock domBlock): Visual
{
	public override void Render(DrawingContext context)
	{
		var x = 0d;
		var y = 0d;
		foreach (var line in domBlock.Lines)
		{
			foreach (var inline in line)
			{
				if (inline is RegularTextInline regularTextInline)
				{
					context.DrawText(regularTextInline.FormattedText,new Point(x, y));
					x += inline.Width * domBlock.Document.WordSpacing;
				}
			}

			y += line.LineHeight;
		}
	}
}