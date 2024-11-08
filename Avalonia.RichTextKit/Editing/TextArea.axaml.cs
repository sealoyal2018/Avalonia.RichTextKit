using Avalonia.Input;
using Avalonia.RichTextKit.Streams;

namespace Avalonia.RichTextKit.Editing;

[TemplatePart(Name = ContentControlTemplateName, IsRequired = true, Type = typeof(ContentControl))]
public class TextArea : TemplatedControl
{
	private const string ContentControlTemplateName = "PART_ContentControl";
	private ContentControl? contentControl;
	private readonly TextView _textView;
	private readonly DomDocument _document;
	private readonly CaretService _caretService;

	static TextArea()
	{
		FocusableProperty.OverrideDefaultValue<TextArea>(true);
	}
	
	public TextArea()
	{
		_document = new DomDocument();
		_caretService = new(_document);
		_textView = new TextView(_document, _caretService.Caret);
	}


	protected override void OnPointerReleased(PointerReleasedEventArgs e)
	{
		base.OnPointerReleased(e);
		var currentPoint = e.GetCurrentPoint(this);
		var pos = currentPoint.Position;
	}


	protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
	{
		base.OnApplyTemplate(e);
		contentControl = e.NameScope.Find<ContentControl>(ContentControlTemplateName);
		if (contentControl is null)
		{
			throw new NullReferenceException($"{ContentControlTemplateName} is null");
		}

		contentControl.Content = _textView;
	}
	
	protected override void OnGotFocus(GotFocusEventArgs e)
	{
		base.OnGotFocus(e);
	}

	protected override void OnLostFocus(RoutedEventArgs e)
	{
		base.OnLostFocus(e);
	}

	protected override void OnLoaded(RoutedEventArgs e)
	{
		var json = File.ReadAllText("./Assets/doc.json");
		var jsonFormatter = new JsonDocumentFormatter();
		jsonFormatter.Load(_document, json);
		_document.RaiseDocumentChanged();
	}
}