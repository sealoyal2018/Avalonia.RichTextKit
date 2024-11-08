using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.RichTextKit.Models;

namespace Avalonia.RichTextKit.Rendering;

internal class TextView(DomDocument document, CaretService.CaretView caret) : Control
{
    private readonly List<VisualTextBlock> textBlocks = [];

    protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        base.OnAttachedToLogicalTree(e);
        if(document is not null)
        {
            document.DocumentChanged -= DocumentOnDocumentChanged;
            document.DocumentChanged += DocumentOnDocumentChanged;
        }
    }

    protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        if(document is not null)
        {
            document.DocumentChanged -= DocumentOnDocumentChanged;
        }
        base.OnDetachedFromLogicalTree(e);
    }

    private void DocumentOnDocumentChanged(DomDocument domDocument)
    {
        this.InvalidateMeasure();
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        this.VisualChildren.Clear();
        var height = 0d;
        if (document is null)
        {
            return base.MeasureOverride(availableSize);
        }
        foreach (var block in document.Blocks)
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

        return new Size(document.Width, height);
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