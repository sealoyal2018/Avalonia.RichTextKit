namespace Avalonia.RichTextKit.Streams;

public class TextDocumentFormatter : IDocumentFormatter
{
	public DocumentFormatter Formatter => DocumentFormatter.Text;
	
	public void Load(DomDocument document, string body)
	{
		throw new NotImplementedException();
	}

	public string Save(DomDocument document)
	{
		throw new NotImplementedException();
	}
}