using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.RichTextKit.Models;
using Avalonia.RichTextKit.Rendering;

namespace Avalonia.RichTextKit.Editing;

[TemplatePart(Name= ContentControlTemplateName, IsRequired = true, Type = typeof(ContentControl))]
public class TextArea : TemplatedControl
{
    private const string ContentControlTemplateName = "PART_ContentControl";
    private ContentControl? contentControl;
    private readonly TextView _textView = new();
    private readonly DomDocument document = new();

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        contentControl = e.NameScope.Find<ContentControl>(ContentControlTemplateName);
        if ( contentControl is null )
        {
            throw new NullReferenceException($"{ContentControlTemplateName} is null");
        }
        
        contentControl.Content = _textView;
    }
}