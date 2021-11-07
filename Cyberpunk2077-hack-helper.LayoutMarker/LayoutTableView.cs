using System;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	[ContentProperty("MainContent")]
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
					new PropertyChangedCallback(OnPositionChanged)));

		public static readonly DependencyProperty CellSizeProperty =
			DependencyProperty.Register(
				"CellSize",
				typeof(System.Drawing.Size),
				typeof(LayoutTableView),
				new FrameworkPropertyMetadata(
					new System.Drawing.Size(0, 0),
					FrameworkPropertyMetadataOptions.AffectsMeasure |
					FrameworkPropertyMetadataOptions.AffectsRender,
					new PropertyChangedCallback(OnCellSizeChanged)));

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

		public static readonly DependencyProperty MainContentProperty =
			DependencyProperty.Register(
				"MainContent",
				typeof(object),
				typeof(LayoutTableView),
				null);

		private const double PositionerSize = 5.0;
		private const double PositionerHalfSize = 0.5 * PositionerSize;
		private const double SizerSize = 5.0;

		private System.Drawing.Point _position;
		private System.Drawing.Size _cellSize;
		private System.Drawing.Size _cellCount;

		private Brush _brush;
		private Pen _pen;

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

		public object MainContent
		{
			get { return GetValue(MainContentProperty); }
			set { SetValue(MainContentProperty, value); }
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
		}

		protected override int VisualChildrenCount => _visuals.Count;

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
