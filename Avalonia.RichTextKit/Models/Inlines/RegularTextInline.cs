using Avalonia.Media;
using static System.Net.Mime.MediaTypeNames;

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
		_formattedText = CreateFormattedText(_document, _text);
    }
}