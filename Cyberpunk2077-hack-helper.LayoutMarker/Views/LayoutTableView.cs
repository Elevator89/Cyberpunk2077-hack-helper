using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Cyberpunk2077_hack_helper.LayoutMarker.Views
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

		private Drag<int> _drag = null;

		private readonly VisualCollection _visuals;

		private readonly DrawingVisual _grid;
		private readonly DrawingVisual _positioner;
		private readonly DrawingVisual _sizer;

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

			_grid = new DrawingVisual();
			_positioner = new DrawingVisual();
			_sizer = new DrawingVisual();

			_visuals = new VisualCollection(this) { _grid, _positioner, _sizer };

			MouseLeftButtonDown += LayoutTableView_MouseLeftButtonDown;
			MouseMove += LayoutTableView_MouseMove;
			MouseLeftButtonUp += LayoutTableView_MouseLeftButtonUp;
		}

		protected override int VisualChildrenCount => 3;

		private void LayoutTableView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Point mousePos = e.GetPosition((UIElement)Parent);

			// Initiate the hit test by setting up a hit test result callback method.
			VisualTreeHelper.HitTest(this, null, hitTestResult => HandleMouseDown(mousePos, hitTestResult), new PointHitTestParameters(mousePos));
		}

		private HitTestResultBehavior HandleMouseDown(Point mousePos, HitTestResult result)
		{
			if (result.VisualHit is DrawingVisual drawingVisual)
			{
				if (ReferenceEquals(drawingVisual, _positioner))
				{
					_drag = new Drag<int>(1, mousePos, Position);
					CaptureMouse();
					return HitTestResultBehavior.Stop;
				}

				if (ReferenceEquals(drawingVisual, _sizer))
				{
					_drag = new Drag<int>(2, mousePos, Util.ToPoint(Util.Multiply(CellCount, CellSize)));
					CaptureMouse();
					return HitTestResultBehavior.Stop;
				}
			}
			return HitTestResultBehavior.Continue;
		}

		private void LayoutTableView_MouseMove(object sender, MouseEventArgs e)
		{
			if (!IsMouseCaptured || _drag == null)
				return;

			Point mousePos = e.GetPosition((UIElement)Parent);
			_drag.Update(mousePos);

			switch (_drag.TargetId)
			{
				case 1:
					Position = _drag.TargetEnd;
					break;
				case 2:
					CellSize = Util.Divide(Util.ToSize(_drag.TargetEnd), CellCount);
					break;
			}
		}

		private void LayoutTableView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (!IsMouseCaptured || _drag == null)
				return;

			Point mousePos = e.GetPosition((UIElement)Parent);
			_drag.Update(mousePos);

			switch (_drag.TargetId)
			{
				case 1:
					Position = _drag.TargetEnd;
					break;
				case 2:
					CellSize = Util.Divide(Util.ToSize(_drag.TargetEnd), CellCount);
					break;
			}

			_drag = null;
			ReleaseMouseCapture();
		}

		private void RedrawGrid()
		{
			using (DrawingContext drawingContext = _grid.RenderOpen())
			{
				for (int row = 0; row <= _cellCount.Height; ++row)
				{
					drawingContext.DrawLine(
						_pen,
						new Point(
							_position.X,
							_position.Y + row * _cellSize.Height),
						new Point(
							_position.X + _cellCount.Width * _cellSize.Width,
							_position.Y + row * _cellSize.Height));
				}

				for (int col = 0; col <= _cellCount.Width; ++col)
				{
					drawingContext.DrawLine(
						_pen,
						new Point(
							_position.X + col * _cellSize.Width,
							_position.Y),
						new Point(
							_position.X + col * _cellSize.Width,
							_position.Y + _cellCount.Height * _cellSize.Height));
				}
				drawingContext.Close();
			}
		}

		private void RedrawPositioner()
		{
			using (DrawingContext drawingContext = _positioner.RenderOpen())
			{
				drawingContext.DrawRectangle(_pen.Brush, _pen, new Rect(_position.X - PositionerHalfSize, _position.Y - PositionerHalfSize, PositionerSize, PositionerSize));
				drawingContext.Close();
			}
		}

		private void RedrawSizer()
		{
			using (DrawingContext drawingContext = _sizer.RenderOpen())
			{
				drawingContext.DrawRectangle(_pen.Brush, _pen, new Rect(_position.X + _cellCount.Width * _cellSize.Width, _position.Y + _cellCount.Height * _cellSize.Height, SizerSize, SizerSize));
				drawingContext.Close();
			}
		}

		// Provide a required override for the GetVisualChild method.
		protected override Visual GetVisualChild(int index)
		{
			if (index < 0 || index >= _visuals.Count)
				throw new ArgumentOutOfRangeException();

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
	}
}
