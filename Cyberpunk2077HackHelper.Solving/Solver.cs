using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Cyberpunk2077HackHelper.Common;
using Cyberpunk2077HackHelper.Solving.Wave;

namespace Cyberpunk2077HackHelper.Solving
{
	public class Solver
	{
		private readonly Combiner<Symbol> _combiner = new Combiner<Symbol>(EqualityComparer<Symbol>.Default);

		public IEnumerable<IReadOnlyList<Point>> Solve(Problem problem)
		{
			foreach (IReadOnlyList<Symbol> possibleCombination in GetPossibleSequenceCombinations(problem.DaemonSequences, problem.BufferLength))
			{
				MatrixCellEnumerator matrixCellEnumerator = new MatrixCellEnumerator(problem.Matrix, possibleCombination);
				MatrixCellProcessor matrixCellProcessor = new MatrixCellProcessor();
				Wave<MatrixSequencePoint, MatrixSequencePoint> wave = new Wave<MatrixSequencePoint, MatrixSequencePoint>(matrixCellEnumerator, matrixCellProcessor, new MatrixSequencePointComparer());

				MatrixSequencePoint[] result = wave.Run().ToArray();
				foreach (MatrixSequencePoint reusltPoint in result)
				{
					if (reusltPoint.Index == possibleCombination.Count - 1)
						yield return UnwindMatrixSequencePoint(reusltPoint);
				}
			}
		}

		private static IReadOnlyList<Point> UnwindMatrixSequencePoint(MatrixSequencePoint matrixSequencePoint)
		{
			int length = matrixSequencePoint.Index + 1;
			Point[] result = new Point[length];

			for (int i = length - 1; i >= 0; --i)
			{
				result[i] = matrixSequencePoint.Point;
				matrixSequencePoint = matrixSequencePoint.PrevPoint;
			}

			return result;
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

	}
}
