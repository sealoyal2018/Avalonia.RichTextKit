﻿namespace Avalonia.RichTextKit.Models.Inlines;

internal class LineBreakInline : DomInline
{
    public override double Width => 0;
    public override double Height => 1;
}