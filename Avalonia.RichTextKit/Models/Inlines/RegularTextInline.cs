using Avalonia.Media;

namespace Avalonia.RichTextKit.Models.Inlines;

internal class RegularTextInline : DomInline
{
	private readonly DomDocument _document;
	private readonly string _text;
	private FormattedText? _formattedText;
	
	public override double Width => _formattedText!.Width;
	public override double Height => _formattedText!.Height;

	public FormattedText FormattedText => _formattedText!;

	public RegularTextInline(DomDocument document, string text)
	{
		_document = document;
		_text = text;
		this.Invalidate();
	}

	public sealed override void Invalidate()
	{		
		var style = _document.GetStyle(0);
		_formattedText = new FormattedText(_text, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, Typeface.Default, 12, Brush.Parse(style.ForegroundColor));
	}
}