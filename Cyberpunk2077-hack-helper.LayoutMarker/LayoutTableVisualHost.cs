using Cyberpunk2077_hack_helper.Grabbing;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class LayoutTableVisualHost : FrameworkElement
	{
		private readonly LayoutTableViewModel _layoutTableVm;
		private readonly Brush _brush;

		private readonly DrawingVisual _drawingVisual;

		public LayoutTableVisualHost(LayoutTableViewModel layoutTableVm, Brush brush)
		{
			_layoutTableVm = layoutTableVm;
			_brush = brush;

			_drawingVisual = CreateDrawingVisualLayout(_layoutTableVm.Model, _brush);

			layoutTableVm.PropertyChanged += (object sender, PropertyChangedEventArgs e) => DrawVisualLayout(_drawingVisual, layoutTableVm.Model, Brushes.Red);
		}

		private DrawingVisual CreateDrawingVisualLayout(LayoutTable layoutTable, Brush brush)
		{
			DrawingVisual drawingVisual = new DrawingVisual();
			DrawVisualLayout(drawingVisual, layoutTable, brush);
			return drawingVisual;
		}

		// Create a DrawingVisual that contains a rectangle.
		private void DrawVisualLayout(DrawingVisual drawingVisual, LayoutTable layoutTable, Brush brush)
		{
			// Retrieve the DrawingContext in order to create new drawing content.
			using (DrawingContext drawingContext = drawingVisual.RenderOpen())
			{
				Pen pen = new Pen(brush, 1.0);

				for (int row = 0; row <= layoutTable.CellCount.Height; ++row)
				{
					drawingContext.DrawLine(
						pen,
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
						pen,
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
		protected override int VisualChildrenCount => 1;

		// Provide a required override for the GetVisualChild method.
		protected override Visual GetVisualChild(int index)
		{
			if (index < 0 || index >= 1)
			{
				throw new ArgumentOutOfRangeException();
			}

			return _drawingVisual;
		}
	}
}
