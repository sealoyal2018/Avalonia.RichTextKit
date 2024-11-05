using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.RichTextKit.Editing;

namespace Avalonia.RichTextKit;

[TemplatePart(Name= ContentControlTemplateName, IsRequired = true, Type = typeof(ContentControl))]
public class RichTextBox : TemplatedControl
{
    private const string ContentControlTemplateName = "PART_ContentControl";
    private ContentControl? contentControl;
    private readonly TextArea _textArea = new();

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        contentControl = e.NameScope.Find<ContentControl>(ContentControlTemplateName);
        if ( contentControl is null )
        {
            throw new NullReferenceException($"{ContentControlTemplateName} is null");
        }
        
        contentControl.Content = _textArea;
    }
}