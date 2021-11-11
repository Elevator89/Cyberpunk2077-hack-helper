using System.Windows;
using System.Windows.Media;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class PointVisual : DrawingVisual
	{
		private const double PointSize = 2f;
		private const double PointHalfSize = 0.5 * PointSize;
		private const double Thickness = 1.0;

		private System.Drawing.Point _tablePosition;
		private System.Drawing.Size _tableCellSize;
		private System.Drawing.Size _tableCellCount;
		private System.Drawing.Point _pointPosition;

		private Brush _brush;
		private Pen _pen;

		public System.Drawing.Point TablePosition
		{
			get { return _tablePosition; }
			set
			{
				if (_tablePosition == value)
					return;

				_tablePosition = value;
				RefreshDrawing();
			}
		}

		public System.Drawing.Size TableCellSize
		{
			get { return _tableCellSize; }
			set
			{
				if (_tableCellSize == value)
					return;

				_tableCellSize = value;
				RefreshDrawing();
			}
		}

		public System.Drawing.Size TableCellCount
		{
			get { return _tableCellCount; }
			set
			{
				if (_tableCellCount == value)
					return;

				_tableCellCount = value;
				RefreshDrawing();
			}
		}

		public System.Drawing.Point PointPosition
		{
			get { return _pointPosition; }
			set
			{
				if (_pointPosition == value)
					return;

				_pointPosition = value;
				RefreshDrawing();
			}
		}

		public Brush Brush
		{
			get { return _brush; }
			set
			{
				_brush = value;
				_pen = new Pen(_brush, Thickness);
				RefreshDrawing();
			}
		}

		public PointVisual(
			System.Drawing.Point tablePosition,
			System.Drawing.Size tableCellSize,
			System.Drawing.Size tableCellCount,
			System.Drawing.Point pointPosition,
			Brush brush)
		{
			_tablePosition = tablePosition;
			_tableCellSize = tableCellSize;
			_tableCellCount = tableCellCount;
			_pointPosition = pointPosition;
			_brush = brush;
			_pen = new Pen(_brush, Thickness);
			RefreshDrawing();
		}

		private void RefreshDrawing()
		{
			using (DrawingContext drawingContext = RenderOpen())
			{
				Vector pointV = new Vector(PointPosition.X, PointPosition.Y);

				for (int row = 0; row < TableCellCount.Height; ++row)
				{
					for (int col = 0; col < TableCellCount.Width; ++col)
					{
						Point cellPos = new Point(
							TablePosition.X + col * TableCellSize.Width,
							TablePosition.Y + row * TableCellSize.Height);

						Point cellPointPos = cellPos + pointV;

						Rect rect = new Rect(cellPointPos.X - PointHalfSize, cellPointPos.Y - PointHalfSize, PointSize, PointSize);
						drawingContext.DrawRectangle(null, _pen, rect);
					}
				}
				drawingContext.Close();
			}
		}
	}
}
