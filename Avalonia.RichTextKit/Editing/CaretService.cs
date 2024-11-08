using Avalonia.Media;
using Avalonia.Threading;

namespace Avalonia.RichTextKit.Editing;

public class CaretService(DomDocument doc)
{
	private readonly DomDocument _doc = doc;
	internal CaretView Caret { get; } = new();

	public void Location(Point point)
	{
		if (point.Y < 0)
		{
			
		}

		if (point.Y > doc.Height)
		{
			
		}		
		
	}
	
	

	#region caret view

	internal class CaretView : Visual
	{
		private static readonly StyledProperty<bool> IsBlinkProperty;
		private static readonly StyledProperty<Point?> CoordProperty;
		private static readonly StyledProperty<double> HeightProperty;
		private static readonly StyledProperty<bool> IsShowProperty;
		private readonly DispatcherTimer _timer;

		internal Point? Coord
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

		internal bool IsShow
		{
			get => GetValue(IsShowProperty);
			set => SetValue(IsShowProperty, value);
		}


		static CaretView()
		{
			IsBlinkProperty = AvaloniaProperty.Register<CaretView, bool>(nameof(IsBlink));
			IsShowProperty = AvaloniaProperty.Register<CaretView, bool>(nameof(IsBlink));
			CoordProperty = AvaloniaProperty.Register<CaretView, Point?>(nameof(Coord), new Point(10, 10));
			HeightProperty = AvaloniaProperty.Register<CaretView, double>(nameof(Coord), 32);
		}

		internal CaretView()
		{
			AffectsRender<CaretView>(IsBlinkProperty);
			AffectsRender<CaretView>(CoordProperty);
			AffectsRender<CaretView>(HeightProperty);

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

		private  void Refresh()
		{
			this.InvalidateVisual();
		}

		private  void Show()
		{
			IsShow = true;
			IsBlink = true;
			_timer.Start();
		}

		private void Hide()
		{
			_timer.Stop();
			IsShow = false;
			IsBlink = false;
		}

		public override void Render(DrawingContext dc)
		{
			if (IsShow && IsBlink && Coord.HasValue)
			{
				dc.DrawLine(new Pen(Brushes.DarkViolet), Coord.Value,
					new Point(Coord.Value.X + 1, Coord.Value.Y + Height));
			}
		}
	}

	#endregion
}