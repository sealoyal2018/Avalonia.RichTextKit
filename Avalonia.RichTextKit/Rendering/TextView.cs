using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.RichTextKit.Models;

namespace Avalonia.RichTextKit.Rendering;

public class TextView: Control
{
    private readonly List<VisualTextBlock> textBlocks = [];

    private static readonly StyledProperty<DomDocument> DomcumentProperty;
    public DomDocument Domcument
    {
        get => GetValue(DomcumentProperty);
        set => SetValue(DomcumentProperty, value);
    }

    static TextView()
    {
        DomcumentProperty = AvaloniaProperty.Register<TextView, DomDocument>(nameof(DomDocument), new DomDocument());
        AffectsMeasure<TextView>(DomcumentProperty);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        return base.MeasureOverride(availableSize);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        return base.ArrangeOverride(finalSize);
    }


    public override void Render(DrawingContext context)
    {
        context.DrawEllipse(Brushes.Peru, null, new Point(50, 50), 30, 50);
    }
}