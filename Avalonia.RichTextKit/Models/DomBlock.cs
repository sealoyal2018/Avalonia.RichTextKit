using System.Collections;
using Avalonia.Media.TextFormatting;
using Avalonia.RichTextKit.Models.Inlines;

namespace Avalonia.RichTextKit.Models;

public class DomBlock : IEnumerable<DomInline>
{
    private readonly List<DomLine> _lines = new();
    private readonly List<DomInline> _inlines = new();
    
    internal DomDocument Document { get; init; }
    internal IReadOnlyList<DomLine> Lines => _lines;
    
    public double Left { get; set; }
    public double Top { get; set; }

    public double Height => _lines.Aggregate(0d,(i, v) => i + v.LineHeight);

    public DomBlock(DomDocument document)
    {
        this.Document = document;
        _inlines = [new BlockEndInline()]; // default inline while block end inline.
    }

    private void Invalidate()
    {
        _lines.Clear();
        DomLine? newLine = null;
        foreach (var inline in _inlines)
        {
            inline.Invalidate();
            newLine ??= new DomLine(this);
            if (newLine.LineWidth + (inline.Width* this.Document.WordSpacing) > Document.Width)
            {
                _lines.Add(newLine);
                newLine = new DomLine(this);
                newLine.AddInline(inline);
            }
        }
        if(newLine is not null)
            _lines.Add(newLine);
    }

    #region IEnumerator
    
    public IEnumerator<DomInline> GetEnumerator()
    {
        foreach (var inline in _inlines)
        {
            yield return inline;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    #endregion
    
    internal record DomLine(DomBlock block) : IEnumerable<DomInline>
    {
        private readonly List<DomInline> _inlines = new();
    
        public double LineHeight { get; private set; }
        
        public double LineWidth { get; private set; }
        
        public void AddInline(DomInline inline)
        {
            if (inline.Width + LineWidth > block.Document.Width)
                throw new OutOfMemoryException();
            _inlines.Add(inline);
            LineWidth += (inline.Width * block.Document.WordSpacing);
            LineHeight = Math.Max(LineHeight, inline.Height * block.Document.LineSpacing);
        }

        public IEnumerator<DomInline> GetEnumerator()
        {
            foreach (var inline in _inlines)
                yield return inline;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

