namespace Avalonia.RichTextKit.Streams;

public interface IDocumentFormatter
{
	DocumentFormatter Formatter { get; }

	/// <summary>
	/// 加载Document
	/// </summary>
	/// <param name="document"></param>
	/// <param name="body"></param>
	void Load(DomDocument document, string body);

	/// <summary>
	/// 输出字符串
	/// </summary>
	/// <param name="document"></param>
	/// <returns></returns>
	string Save(DomDocument document);
}

public enum DocumentFormatter
{
	Json, // default.
	Text,
}