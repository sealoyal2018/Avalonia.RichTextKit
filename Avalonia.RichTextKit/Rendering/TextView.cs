using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.RichTextKit.Models;

namespace Avalonia.RichTextKit.Rendering;

public class TextView(Caret caret) : Control
{
    private readonly List<VisualTextBlock> textBlocks = [];

    private static readonly StyledProperty<DomDocument> DomDocumentProperty;
    public DomDocument DomDocument
    {
        get => GetValue(DomDocumentProperty);
        set => SetValue(DomDocumentProperty, value);
    }

    static TextView()
    {
        DomDocumentProperty = AvaloniaProperty.Register<TextView, DomDocument>(nameof(DomDocument), new DomDocument());
        AffectsMeasure<TextView>(DomDocumentProperty);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        this.VisualChildren.Clear();
        var height = 0d;
        foreach (var block in this.DomDocument.Blocks)
        {
            if (height > availableSize.Height)
                break;

            var newBlock = new VisualTextBlock(block);
            textBlocks.Add(newBlock);
            newBlock.RenderTransform = new TranslateTransform(0, height);
            this.VisualChildren.Add(newBlock);
            height += block.Height;
        }
        VisualChildren.Add(caret);

        return new Size(this.DomDocument.Width, height);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        //var height = 0d;
        //foreach(var block in textBlocks)
        //{
        //    block.RenderTransform = new TranslateTransform(0, height);
        //    //block.RenderTransformOrigin = new RelativePoint(new Point(0, height), RelativeUnit.Relative);
        //    height += block.DomBlock.Height;
        //}

        return base.ArrangeOverride(finalSize);
    }

    public override void Render(DrawingContext context)
    {
        context.FillRectangle(Brush.Parse("#F1F1F1"), this.Bounds);
    }
}