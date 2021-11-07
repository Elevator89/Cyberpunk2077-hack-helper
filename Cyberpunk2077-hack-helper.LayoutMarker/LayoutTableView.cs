using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class LayoutTableView : FrameworkElement
	{
		public static readonly DependencyProperty PositionProperty =
			DependencyProperty.Register(
				"Position",
				typeof(System.Drawing.Point),
				typeof(LayoutTableView),
				new FrameworkPropertyMetadata(
					new System.Drawing.Point(0, 0),
					FrameworkPropertyMetadataOptions.AffectsMeasure |
					FrameworkPropertyMetadataOptions.AffectsRender,
					new PropertyChangedCallback(OnPositionChanged))
				{
					BindsTwoWayByDefault = true
				});

		public static readonly DependencyProperty CellSizeProperty =
			DependencyProperty.Register(
				"CellSize",
				typeof(System.Drawing.Size),
				typeof(LayoutTableView),
				new FrameworkPropertyMetadata(
					new System.Drawing.Size(0, 0),
					FrameworkPropertyMetadataOptions.AffectsMeasure |
					FrameworkPropertyMetadataOptions.AffectsRender,
					new PropertyChangedCallback(OnCellSizeChanged))
				{
					BindsTwoWayByDefault = true
				});

		public static readonly DependencyProperty CellCountProperty =
			DependencyProperty.Register(
				"CellCount",
				typeof(System.Drawing.Size),
				typeof(LayoutTableView),
				new FrameworkPropertyMetadata(
					new System.Drawing.Size(0, 0),
					FrameworkPropertyMetadataOptions.AffectsMeasure |
					FrameworkPropertyMetadataOptions.AffectsRender,
					new PropertyChangedCallback(OnCellCountChanged)));

		public static readonly DependencyProperty BrushProperty =
			DependencyProperty.Register(
				"Brush",
				typeof(Brush),
				typeof(LayoutTableView),
				new FrameworkPropertyMetadata(
					Brushes.White,
					FrameworkPropertyMetadataOptions.AffectsRender,
					new PropertyChangedCallback(OnBrushChanged)));

		private const double PositionerSize = 10.0;
		private const double PositionerHalfSize = 0.5 * PositionerSize;
		private const double SizerSize = 10.0;

		private System.Drawing.Point _position;
		private System.Drawing.Size _cellSize;
		private System.Drawing.Size _cellCount;

		private Brush _brush;
		private Pen _pen;

		private Drag _positionerDrag = null;
		private Drag _sizerDrag = null;

		// Create a collection of child visual objects.
		private readonly VisualCollection _visuals;

		public System.Drawing.Point Position
		{
			get { return (System.Drawing.Point)GetValue(PositionProperty); }
			set { SetValue(PositionProperty, value); }
		}

		public System.Drawing.Size CellSize
		{
			get { return (System.Drawing.Size)GetValue(CellSizeProperty); }
			set { SetValue(CellSizeProperty, value); }
		}

		public System.Drawing.Size CellCount
		{
			get { return (System.Drawing.Size)GetValue(CellCountProperty); }
			set { SetValue(CellCountProperty, value); }
		}

		public Brush Brush
		{
			get { return (Brush)GetValue(BrushProperty); }
			set { SetValue(BrushProperty, value); }
		}

		public LayoutTableView()
		{
			_brush = Brushes.Red;
			_pen = new Pen(_brush, 1.0);
			_position = new System.Drawing.Point(0, 0);
			_cellSize = new System.Drawing.Size(0, 0);
			_cellCount = new System.Drawing.Size(0, 0);

			_visuals = new VisualCollection(this)
			{
				new DrawingVisual(),
				new DrawingVisual(),
				new DrawingVisual(),
			};

			MouseLeftButtonDown += LayoutTableView_MouseLeftButtonDown;
			MouseMove += LayoutTableView_MouseMove;
			MouseLeftButtonUp += LayoutTableView_MouseLeftButtonUp;
		}

		protected override int VisualChildrenCount => _visuals.Count;

		// Capture the mouse event and hit test the coordinate point value against
		// the child visual objects.
		private void LayoutTableView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Point mousePos = e.GetPosition((UIElement)Parent);

			// Initiate the hit test by setting up a hit test result callback method.
			VisualTreeHelper.HitTest(this, null, hitTestResult => HandleMouseDown(mousePos, hitTestResult), new PointHitTestParameters(mousePos));
		}

		private void LayoutTableView_MouseMove(object sender, MouseEventArgs e)
		{
			Point mousePos = e.GetPosition((UIElement)Parent);

			if (_positionerDrag != null)
			{
				_positionerDrag.Update(mousePos);
				Position = _positionerDrag.TargetEnd;
			}
			else if (_sizerDrag != null)
			{
				_sizerDrag.Update(mousePos);
				CellSize = Util.Divide(Util.ToSize(_sizerDrag.TargetEnd), CellCount);
			}
		}

		private void LayoutTableView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			Point mousePos = e.GetPosition((UIElement)Parent);

			if (_positionerDrag != null)
			{
				_positionerDrag.Update(mousePos);
				Position = _positionerDrag.TargetEnd;
				_positionerDrag = null;
			}
			else if (_sizerDrag != null)
			{
				_sizerDrag.Update(mousePos);
				CellSize = Util.Divide(Util.ToSize(_sizerDrag.TargetEnd), CellCount);
				_sizerDrag = null;
			}
		}

		// If a child visual object is hit, toggle its opacity to visually indicate a hit.
		public HitTestResultBehavior HandleMouseDown(Point mousePos, HitTestResult result)
		{
			if (result.VisualHit is DrawingVisual drawingVisual)
			{
				if (ReferenceEquals(drawingVisual, _visuals[1]))
				{
					_positionerDrag = new Drag(mousePos, Position);
					return HitTestResultBehavior.Stop;
				}

				if (ReferenceEquals(drawingVisual, _visuals[2]))
				{
					_sizerDrag = new Drag(mousePos, Util.ToPoint(Util.Multiply(CellCount, CellSize)));
					return HitTestResultBehavior.Stop;
				}
			}
			return HitTestResultBehavior.Continue;
		}

		private HitTestFilterBehavior HandleMouseDown(Point mousePos, DependencyObject potentialHitTestTarget)
		{
			if (potentialHitTestTarget.GetType() == typeof(LayoutTableView))
				return HitTestFilterBehavior.Continue;

			if (potentialHitTestTarget is DrawingVisual drawingVisual)
			{
				if (ReferenceEquals(drawingVisual, _visuals[1]))
				{
					_positionerDrag = new Drag(mousePos, Position);
					return HitTestFilterBehavior.Stop;
				}

				if (ReferenceEquals(drawingVisual, _visuals[2]))
				{
					_sizerDrag = new Drag(mousePos, Util.ToPoint(Util.Multiply(CellCount, CellSize)));
					return HitTestFilterBehavior.Stop;
				}
			}
			return HitTestFilterBehavior.ContinueSkipSelfAndChildren;
		}

		private void RedrawGrid()
		{
			DrawingVisual gridVisual = (DrawingVisual)_visuals[0];
			using (DrawingContext drawingContext = gridVisual.RenderOpen())
			{
				DrawGrid(drawingContext, _pen, _position, _cellSize, _cellCount);
				drawingContext.Close();
			}
		}

		private void RedrawPositioner()
		{
			DrawingVisual positionerVisual = (DrawingVisual)_visuals[1];
			using (DrawingContext drawingContext = positionerVisual.RenderOpen())
			{
				drawingContext.DrawRectangle(_pen.Brush, _pen, new Rect(_position.X - PositionerHalfSize, _position.Y - PositionerHalfSize, PositionerSize, PositionerSize));
				drawingContext.Close();
			}
		}

		private void RedrawSizer()
		{
			DrawingVisual sizerVisual = (DrawingVisual)_visuals[2];
			using (DrawingContext drawingContext = sizerVisual.RenderOpen())
			{
				drawingContext.DrawRectangle(_pen.Brush, _pen, new Rect(_position.X + _cellCount.Width * _cellSize.Width, _position.Y + _cellCount.Height * _cellSize.Height, SizerSize, SizerSize));
				drawingContext.Close();
			}
		}

		// Provide a required override for the GetVisualChild method.
		protected override Visual GetVisualChild(int index)
		{
			if (index < 0 || index >= _visuals.Count)
			{
				throw new ArgumentOutOfRangeException();
			}

			return _visuals[index];
		}

		private static void OnPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LayoutTableView thisObj = (LayoutTableView)d;
			System.Drawing.Point position = (System.Drawing.Point)e.NewValue;

			thisObj._position = position;
			thisObj.RedrawGrid();
			thisObj.RedrawPositioner();
			thisObj.RedrawSizer();
		}

		private static void OnCellSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LayoutTableView thisObj = (LayoutTableView)d;
			System.Drawing.Size cellSize = (System.Drawing.Size)e.NewValue;

			thisObj._cellSize = cellSize;
			thisObj.RedrawGrid();
			thisObj.RedrawSizer();
		}

		private static void OnCellCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LayoutTableView thisObj = (LayoutTableView)d;
			System.Drawing.Size cellCount = (System.Drawing.Size)e.NewValue;

			thisObj._cellCount = cellCount;
			thisObj.RedrawGrid();
			thisObj.RedrawSizer();
		}

		private static void OnBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LayoutTableView thisObj = (LayoutTableView)d;
			Brush brush = (Brush)e.NewValue;

			thisObj._brush = brush;
			thisObj._pen = new Pen(brush, 1.0);
			thisObj.RedrawGrid();
			thisObj.RedrawPositioner();
			thisObj.RedrawSizer();
		}

		private static void DrawGrid(DrawingContext drawingContext, Pen pen, System.Drawing.Point position, System.Drawing.Size cellSize, System.Drawing.Size cellCount)
		{
			for (int row = 0; row <= cellCount.Height; ++row)
			{
				drawingContext.DrawLine(
					pen,
					new Point(
						position.X,
						position.Y + row * cellSize.Height),
					new Point(
						position.X + cellCount.Width * cellSize.Width,
						position.Y + row * cellSize.Height));
			}

			for (int col = 0; col <= cellCount.Width; ++col)
			{
				drawingContext.DrawLine(
					pen,
					new Point(
						position.X + col * cellSize.Width,
						position.Y),
					new Point(
						position.X + col * cellSize.Width,
						position.Y + cellCount.Height * cellSize.Height));
			}
		}
	}
}
