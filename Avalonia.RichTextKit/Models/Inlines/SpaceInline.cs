using Avalonia.Media;

namespace Avalonia.RichTextKit.Models.Inlines;

public class SpaceInline : DomInline
{
    private readonly DomDocument document;
    private FormattedText _formattedText;

    public override double Width => _formattedText.Width;
    public override double Height => _formattedText.Height;

    public SpaceInline(DomDocument document)
    {
        this.document = document;
        _formattedText = CreateFormattedText(document, " ");
    }

    public override void Invalidate()
    {
        _formattedText = CreateFormattedText(document, " ");
    }
}