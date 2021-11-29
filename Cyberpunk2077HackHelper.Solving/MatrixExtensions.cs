using System.Collections.Generic;
using System.Drawing;
using Cyberpunk2077HackHelper.Common;

namespace Cyberpunk2077HackHelper.Solving
{
	public static class MatrixExtensions
	{
		public static Symbol Get(this Symbol[,] matrix, Point point)
		{
			return matrix[point.Y, point.X];
		}

		public static void Set(this Symbol[,] matrix, Point point, Symbol symbol)
		{
			matrix[point.Y, point.X] = symbol;
		}

		public static int GetRowCount(this Symbol[,] matrix)
		{
			return matrix.GetLength(0);
		}

		public static int GetColCount(this Symbol[,] matrix)
		{
			return matrix.GetLength(1);
		}

		public static IEnumerable<Point> GetOtherCellsInLine(this Symbol[,] matrix, MatrixLineDirection lineDirection, Point point)
		{
			switch (lineDirection)
			{
				case MatrixLineDirection.Column:
					for (int row = 0; row < matrix.GetRowCount(); ++row)
						if (row != point.Y)
							yield return new Point(point.X, row);
					yield break;

				case MatrixLineDirection.Row:
					for (int col = 0; col < matrix.GetColCount(); ++col)
						if (col != point.X)
							yield return new Point(col, point.Y);
					yield break;
			}
		}
	}
}
