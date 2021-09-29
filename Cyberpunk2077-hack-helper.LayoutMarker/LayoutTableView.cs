using Cyberpunk2077_hack_helper.Grabbing;
using System;
using System.Windows;
using System.Windows.Media;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class LayoutTableView : FrameworkElement
	{
		public static readonly DependencyProperty LayoutTableProperty;
		public static readonly DependencyProperty BrushProperty;

		private Brush _brush;
		private Pen _pen;

		// Create a collection of child visual objects.
		private readonly VisualCollection _visuals;

		static LayoutTableView()
		{
			LayoutTableProperty = DependencyProperty.Register(
						"LayoutTable",
						typeof(LayoutTableViewModel),
						typeof(LayoutTableView),
						new FrameworkPropertyMetadata(
							null,
							FrameworkPropertyMetadataOptions.AffectsMeasure |
							FrameworkPropertyMetadataOptions.AffectsRender,
							new PropertyChangedCallback(OnLayoutTableChanged)));

			BrushProperty = DependencyProperty.Register(
						"Brush",
						typeof(Brush),
						typeof(LayoutTableView),
						new FrameworkPropertyMetadata(
							Brushes.White,
							FrameworkPropertyMetadataOptions.AffectsRender,
							new PropertyChangedCallback(OnBrushChanged)));
		}

		public LayoutTableViewModel LayoutTable
		{
			get { return (LayoutTableViewModel)GetValue(LayoutTableProperty); }
			set { SetValue(LayoutTableProperty, value); }
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

		// Create a DrawingVisual that contains a rectangle.
		private void DrawVisualLayout(DrawingVisual drawingVisual, LayoutTable layoutTable)
		{
			// Retrieve the DrawingContext in order to create new drawing content.
			using (DrawingContext drawingContext = drawingVisual.RenderOpen())
			{
				for (int row = 0; row <= layoutTable.CellCount.Height; ++row)
				{
					drawingContext.DrawLine(
						_pen,
						new Point(
							layoutTable.Position.X,
							layoutTable.Position.Y + row * layoutTable.CellSize.Height),
						new Point(
							layoutTable.Position.X + layoutTable.CellCount.Width * layoutTable.CellSize.Width,
							layoutTable.Position.Y + row * layoutTable.CellSize.Height));
				}

				for (int col = 0; col <= layoutTable.CellCount.Width; ++col)
				{
					drawingContext.DrawLine(
						_pen,
						new Point(
							layoutTable.Position.X + col * layoutTable.CellSize.Width,
							layoutTable.Position.Y),
						new Point(
							layoutTable.Position.X + col * layoutTable.CellSize.Width,
							layoutTable.Position.Y + layoutTable.CellCount.Height * layoutTable.CellSize.Height));
				}

				// Persist the drawing content.
				drawingContext.Close();
			}
		}

		// Provide a required override for the VisualChildrenCount property.
		protected override int VisualChildrenCount => _visuals.Count;

		// Provide a required override for the GetVisualChild method.
		protected override Visual GetVisualChild(int index)
		{
			if (index < 0 || index >= _visuals.Count)
			{
				throw new ArgumentOutOfRangeException();
			}

			return _visuals[index];
		}

		private static void OnLayoutTableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LayoutTableView thisObj = (LayoutTableView)d;
			LayoutTableViewModel value = (LayoutTableViewModel)e.NewValue;

			thisObj.DrawVisualLayout((DrawingVisual)thisObj.GetVisualChild(0), value.Model);
		}

		private static void OnBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LayoutTableView thisObj = (LayoutTableView)d;
			Brush brush = (Brush)e.NewValue;

			thisObj._brush = brush;
			thisObj._pen = new Pen(brush, 1.0);
		}
	}
}
