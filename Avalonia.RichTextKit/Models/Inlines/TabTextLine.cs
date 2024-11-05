using Avalonia.Media;

namespace Avalonia.RichTextKit.Models.Inlines;

public class TabTextLine: DomInline
{
	public override double Width { get; }
	public override double Height { get; }

	public TabTextLine(DomDocument document)
	{
		var style = document.GetStyle(0);
		var formattedText = new FormattedText("\t", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, Typeface.Default, 12, Brush.Parse(style.ForegroundColor));
		Width = formattedText.Width;
		Height = formattedText.Height;
	}
}