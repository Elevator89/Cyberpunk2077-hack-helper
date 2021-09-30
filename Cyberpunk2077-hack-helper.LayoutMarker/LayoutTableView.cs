using System;
using System.Windows;
using System.Windows.Media;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class LayoutTableView : FrameworkElement
	{
		public static readonly DependencyProperty PositionProperty;
		public static readonly DependencyProperty CellSizeProperty;
		public static readonly DependencyProperty CellCountProperty;
		public static readonly DependencyProperty BrushProperty;

		static LayoutTableView()
		{
			PositionProperty = DependencyProperty.Register(
						"Position",
						typeof(System.Drawing.Point),
						typeof(LayoutTableView),
						new FrameworkPropertyMetadata(
							new System.Drawing.Point(0, 0),
							FrameworkPropertyMetadataOptions.AffectsMeasure |
							FrameworkPropertyMetadataOptions.AffectsRender,
							new PropertyChangedCallback(OnPositionChanged)));

			CellSizeProperty = DependencyProperty.Register(
						"CellSize",
						typeof(System.Drawing.Size),
						typeof(LayoutTableView),
						new FrameworkPropertyMetadata(
							new System.Drawing.Size(0, 0),
							FrameworkPropertyMetadataOptions.AffectsMeasure |
							FrameworkPropertyMetadataOptions.AffectsRender,
							new PropertyChangedCallback(OnCellSizeChanged)));

			CellCountProperty = DependencyProperty.Register(
						"CellCount",
						typeof(System.Drawing.Size),
						typeof(LayoutTableView),
						new FrameworkPropertyMetadata(
							new System.Drawing.Size(0, 0),
							FrameworkPropertyMetadataOptions.AffectsMeasure |
							FrameworkPropertyMetadataOptions.AffectsRender,
							new PropertyChangedCallback(OnCellCountChanged)));

			BrushProperty = DependencyProperty.Register(
						"Brush",
						typeof(Brush),
						typeof(LayoutTableView),
						new FrameworkPropertyMetadata(
							Brushes.White,
							FrameworkPropertyMetadataOptions.AffectsRender,
							new PropertyChangedCallback(OnBrushChanged)));
		}

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

		public LayoutTableView()
		{
			_brush = Brushes.Red;
			_pen = new Pen(_brush, 1.0);

			_visuals = new VisualCollection(this)
			{
				new DrawingVisual()
			};
		}

		protected override int VisualChildrenCount => _visuals.Count;

		private void RedrawSelf()
		{
			RedrawGrid((DrawingVisual)GetVisualChild(0), _pen, _position, _cellSize, _cellCount);
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
			thisObj.RedrawSelf();
		}

		private static void OnCellSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LayoutTableView thisObj = (LayoutTableView)d;
			System.Drawing.Size cellSize = (System.Drawing.Size)e.NewValue;

			thisObj._cellSize = cellSize;
			thisObj.RedrawSelf();
		}

		private static void OnCellCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LayoutTableView thisObj = (LayoutTableView)d;
			System.Drawing.Size cellCount = (System.Drawing.Size)e.NewValue;

			thisObj._cellCount = cellCount;
			thisObj.RedrawSelf();
		}

		private static void OnBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LayoutTableView thisObj = (LayoutTableView)d;
			Brush brush = (Brush)e.NewValue;

			thisObj._brush = brush;
			thisObj._pen = new Pen(brush, 1.0);
			thisObj.RedrawSelf();
		}

		private static void RedrawGrid(DrawingVisual drawingVisual, Pen pen, System.Drawing.Point position, System.Drawing.Size cellSize, System.Drawing.Size cellCount)
		{
			// Retrieve the DrawingContext in order to create new drawing content.
			using (DrawingContext drawingContext = drawingVisual.RenderOpen())
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

				// Persist the drawing content.
				drawingContext.Close();
			}
		}
	}
}
