using Avalonia.Media;
using Avalonia.Threading;

namespace Avalonia.RichTextKit.Rendering;

public class Caret : Visual
{
    private static readonly StyledProperty<bool> IsBlinkProperty;
    private static readonly StyledProperty<Point?> CoordProperty;
    private static readonly StyledProperty<double> HeightProperty;
    private static readonly StyledProperty<bool> IsShowProperty;
    private readonly DispatcherTimer _timer;

    public Point? Coord
    {
        get => GetValue(CoordProperty);
        set => SetValue(CoordProperty, value);
    }

    private bool IsBlink
    {
        get => GetValue(IsBlinkProperty);
        set => SetValue(IsBlinkProperty, value);
    }

    public double Height
    {
        get => GetValue(HeightProperty);
        set => SetValue(HeightProperty, value);
    }

    public bool IsShow
    {
        get => GetValue(IsShowProperty);
        set => SetValue(IsShowProperty, value);
    }
    

    static Caret()
    {
        IsBlinkProperty = AvaloniaProperty.Register<Caret, bool>(nameof(IsBlink));
        IsShowProperty = AvaloniaProperty.Register<Caret, bool>(nameof(IsBlink));
        CoordProperty = AvaloniaProperty.Register<Caret, Point?>(nameof(Coord), new Point(10,10));
        HeightProperty = AvaloniaProperty.Register<Caret, double>(nameof(Coord), 32);
    }
    
    public Caret()
    {
        AffectsRender<Caret>(IsBlinkProperty);
        AffectsRender<Caret>(CoordProperty);
        AffectsRender<Caret>(HeightProperty);
        
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(500)
        };
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        _timer.Tick -= TimerOnTick;
        _timer.Tick += TimerOnTick;
        _timer.Start();
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        _timer.Stop();
        _timer.Tick -= TimerOnTick;
        base.OnDetachedFromVisualTree(e);
    }

    private void TimerOnTick(object? sender, EventArgs e)
    {
        this.IsBlink = !this.IsBlink;
    }

    public void Refresh()
    {
        this.InvalidateVisual();
    }

    public void Show()
    {
        IsShow = true;
        IsBlink = true;
        _timer.Start();
    }

    public void Hide()
    {
        _timer.Stop();
        IsShow = false;
        IsBlink = false;
    }
    
    public override void Render(DrawingContext dc)
    {
        if (IsShow && IsBlink && Coord.HasValue)
        {
            dc.DrawLine(new Pen(Brushes.DarkViolet), Coord.Value, new Point(Coord.Value.X + 1,Coord.Value.Y + Height));
        }
    }
}