using Avalonia.Input;

namespace Avalonia.RichTextKit.Editing;

[TemplatePart(Name = ContentControlTemplateName, IsRequired = true, Type = typeof(ContentControl))]
public class TextArea : TemplatedControl
{
	private const string ContentControlTemplateName = "PART_ContentControl";
	private ContentControl? contentControl;
	private readonly TextView _textView;
	private readonly DomDocument _document;
	private readonly Caret _caret;

	static TextArea()
	{
		FocusableProperty.OverrideDefaultValue<TextArea>(true);
	}
	
	public TextArea()
	{
		_caret = new Caret();
		_textView = new TextView(_caret);
		_document = new DomDocument();
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
		_caret.Show();
	}

	protected override void OnLostFocus(RoutedEventArgs e)
	{
		base.OnLostFocus(e);
		_caret.Hide();
	}

	protected override void OnLoaded(RoutedEventArgs e)
	{
		var json = File.ReadAllText("./Assets/doc.json");
		var doc = new DomDocument();
		doc.LoadFromJson(json);
		_textView.DomDocument = doc;
	}
}