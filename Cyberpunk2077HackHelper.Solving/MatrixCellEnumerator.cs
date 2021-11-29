using System.Collections.Generic;
using System.Drawing;
using Cyberpunk2077HackHelper.Common;
using Cyberpunk2077HackHelper.Solving.Wave;

namespace Cyberpunk2077HackHelper.Solving
{
	public class MatrixCellEnumerator : IWaveItemsEnumerator<MatrixSequencePoint>
	{
		private readonly Symbol[,] _matrix;
		private readonly IReadOnlyList<Symbol> _combination;

		public MatrixCellEnumerator(Symbol[,] matrix, IReadOnlyList<Symbol> combination)
		{
			_matrix = matrix;
			_combination = combination;
		}

		public IEnumerable<MatrixSequencePoint> GetInitialItems()
		{
			int nextSymbolIndex = 0;

			MatrixLineDirection nextItemLineDirection = GetSymbolDirection(nextSymbolIndex);
			Symbol nextCombinationSymbol = _combination[nextSymbolIndex];

			foreach (Point firstPoint in _matrix.GetOtherCellsInLine(nextItemLineDirection, new Point(-1, 0)))
			{
				if (nextCombinationSymbol == Symbol.Unknown || _matrix.Get(firstPoint) == nextCombinationSymbol)
					yield return new MatrixSequencePoint(firstPoint, nextSymbolIndex, null);
			}
		}

		public IEnumerable<MatrixSequencePoint> GetNextItems(MatrixSequencePoint currrentPoint)
		{
			int nextSymbolIndex = currrentPoint.Index + 1;
			if (nextSymbolIndex >= _combination.Count)
				yield break;

			MatrixLineDirection nextItemLineDirection = GetSymbolDirection(nextSymbolIndex);
			Symbol nextCombinationSymbol = _combination[nextSymbolIndex];

			foreach (Point nextPoint in _matrix.GetOtherCellsInLine(nextItemLineDirection, currrentPoint.Point))
			{
				if (nextCombinationSymbol == Symbol.Unknown || _matrix.Get(nextPoint) == nextCombinationSymbol)
					yield return new MatrixSequencePoint(nextPoint, nextSymbolIndex, currrentPoint);
			}
		}

		private static MatrixLineDirection GetSymbolDirection(int symbolIndex)
		{
			// 0-th symbol should be found in row, 1-th - in column, etc.
			return symbolIndex % 2 == 0 ? MatrixLineDirection.Row : MatrixLineDirection.Column;
		}
	}
}
