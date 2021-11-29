using System.Collections.Generic;
using System.Drawing;
using Cyberpunk2077HackHelper.Common;
using Cyberpunk2077HackHelper.Solving.Wave;

namespace Cyberpunk2077HackHelper.Solving
{
	public readonly struct MatrixSequencePoint
	{
		public readonly Point Point;
		public readonly int Index;

		public MatrixSequencePoint(Point point, int index) : this()
		{
			Point = point;
			Index = index;
		}
	}

	public class MatrixCellEnumerator : IWaveItemsEnumerator<MatrixSequencePoint>
	{
		private static readonly MatrixSequencePoint InitialPoint = new MatrixSequencePoint(new Point(-1, 0), -1); // GetOtherCellsInLine will return the whole first row for this

		private readonly Symbol[,] _matrix;
		private readonly IReadOnlyList<Symbol> _combination;

		public MatrixCellEnumerator(Symbol[,] matrix, IReadOnlyList<Symbol> combination)
		{
			_matrix = matrix;
			_combination = combination;
		}

		public IEnumerable<MatrixSequencePoint> GetInitialItems()
		{
			return GetNextItems(InitialPoint);
		}

		public IEnumerable<MatrixSequencePoint> GetNextItems(MatrixSequencePoint currrentPoint)
		{
			int nextSymbolIndex = currrentPoint.Index + 1;
			if (nextSymbolIndex >= _combination.Count)
				yield break;

			MatrixLineDirection nextItemLineDirection = GetSymbolDirection(nextSymbolIndex);
			Symbol nextCombinationSymbol = _combination[nextSymbolIndex];

			foreach (Point possibleNextPoint in _matrix.GetOtherCellsInLine(nextItemLineDirection, currrentPoint.Point))
			{
				if (nextCombinationSymbol == Symbol.Unknown || _matrix.Get(possibleNextPoint) == nextCombinationSymbol)
					yield return new MatrixSequencePoint(possibleNextPoint, nextSymbolIndex);
			}
		}

		private static MatrixLineDirection GetSymbolDirection(int symbolIndex)
		{
			// 0-th symbol should be found in row, 1-th - in column, etc.
			return symbolIndex % 2 == 0 ? MatrixLineDirection.Row : MatrixLineDirection.Column;
		}
	}
}
