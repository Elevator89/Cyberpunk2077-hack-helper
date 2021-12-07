using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Cyberpunk2077HackHelper.Common;

namespace Cyberpunk2077HackHelper.Solving
{
	public class Solver
	{
		private readonly Combiner<Symbol> _combiner = new Combiner<Symbol>(EqualityComparer<Symbol>.Default);

		public IEnumerable<IReadOnlyList<Point>> Solve(Problem problem)
		{
			foreach (IReadOnlyList<Symbol> possibleCombination in GetPossibleSequenceCombinations(problem.DaemonSequences, problem.BufferLength).OrderBy(c => c.Count))
			{
				List<SequenceItem> foundSequence = new List<SequenceItem>(20); // The actual size is usually less than 20

				HashSet<Point> visitedCells = new HashSet<Point>();
				Queue<SequenceItem> queue = new Queue<SequenceItem>();

				foreach (SequenceItem initialSequenceItem in GetInitialItems())
				{
					visitedCells.Add(initialSequenceItem.Cell);
					queue.Enqueue(initialSequenceItem);
				}

				while (queue.Count > 0)
				{
					SequenceItem currentSequenceItem = queue.Dequeue();

					foundSequence.Add(currentSequenceItem);

					foreach (SequenceItem nextSequenceItem in GetNextItems())
					{
						visitedCells.Add(nextSequenceItem.Cell);
						queue.Enqueue(nextSequenceItem);
					}

					IEnumerable<SequenceItem> GetNextItems()
					{
						int nextSequenceItemIndex = currentSequenceItem.Index + 1;
						if (nextSequenceItemIndex >= possibleCombination.Count)
							yield break;

						MatrixLineDirection nextSequenceItemLineDirection = GetSymbolDirection(nextSequenceItemIndex);
						Symbol nextCombinationSymbol = possibleCombination[nextSequenceItemIndex];

						foreach (Point nextCell in problem.Matrix.GetOtherCellsInLine(nextSequenceItemLineDirection, currentSequenceItem.Cell).Where(nextCell => !visitedCells.Contains(nextCell)))
						{
							if (nextCombinationSymbol == Symbol.Unknown || problem.Matrix.Get(nextCell) == nextCombinationSymbol)
								yield return new SequenceItem(nextCell, nextSequenceItemIndex, currentSequenceItem);
						}
					}
				}

				foreach (SequenceItem sequenceItem in foundSequence)
				{
					if (sequenceItem.Index == possibleCombination.Count - 1)
						yield return UnwindMatrixSequencePoint(sequenceItem);
				}

				IEnumerable<SequenceItem> GetInitialItems()
				{
					MatrixLineDirection initialSequenceItemLineDirection = GetSymbolDirection(0);
					Symbol initialCombinationSymbol = possibleCombination[0];

					foreach (Point initialCell in problem.Matrix.GetOtherCellsInLine(initialSequenceItemLineDirection, new Point(-1, 0)))
					{
						if (initialCombinationSymbol == Symbol.Unknown || problem.Matrix.Get(initialCell) == initialCombinationSymbol)
							yield return new SequenceItem(initialCell, 0, null);
					}
				}
			}
		}

		private IEnumerable<IReadOnlyList<Symbol>> GetPossibleSequenceCombinations(IReadOnlyList<IReadOnlyList<Symbol>> sequences, int maxCombinationLength)
		{
			int[] sequenceIndices = Enumerable.Range(0, sequences.Count).ToArray();
			bool orderIsValid = true;
			do
			{
				IReadOnlyList<Symbol>[] orderedSequences = sequenceIndices.Select(i => sequences[i]).ToArray();
				foreach (IReadOnlyList<Symbol> possibleCombination in _combiner.GetPossibleCombinations(orderedSequences, maxCombinationLength, Symbol.Unknown, 1))
					yield return possibleCombination;

				orderIsValid = PermutationNarayana.NextPermutation(sequenceIndices, (a, b) => a < b);

			} while (orderIsValid);
		}


		private static MatrixLineDirection GetSymbolDirection(int symbolIndex)
		{
			// 0-th symbol should be found in row, 1-th - in column, etc.
			return symbolIndex % 2 == 0 ? MatrixLineDirection.Row : MatrixLineDirection.Column;
		}

		private static IReadOnlyList<Point> UnwindMatrixSequencePoint(SequenceItem matrixSequencePoint)
		{
			int length = matrixSequencePoint.Index + 1;
			Point[] result = new Point[length];

			for (int i = length - 1; i >= 0; --i)
			{
				result[i] = matrixSequencePoint.Cell;
				matrixSequencePoint = matrixSequencePoint.PrevPoint;
			}

			return result;
		}
	}
}
