using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Cyberpunk2077HackHelper.Common;

namespace Cyberpunk2077HackHelper.Solving
{

	public class Solver
	{
		public Point[] Solve(Problem problem)
		{
			throw new NotImplementedException();
		}

		private static List<Symbol[]> GetPossibleSequenceCombinations(IReadOnlyList<IReadOnlyList<Symbol>> sequences)
		{
			int[] sequenceIndices = Enumerable.Range(0, sequences.Count).ToArray();
			bool orderIsValid = true;
			do
			{
				IReadOnlyList<Symbol>[] orderedSequences = sequenceIndices.Select(i => sequences[i]).ToArray();

				IReadOnlyList<Symbol> possibleSequence = orderedSequences[0];

				for (int seqIndex = 1; seqIndex < orderedSequences.Length; ++seqIndex)
				{
					IEnumerable<IReadOnlyList<Symbol>> possibleCombinations = Combiner.GetPossibleCombinations(possibleSequence, orderedSequences[seqIndex], (a, b) => a == b, Symbol.Unknown, 0);
				}

				orderIsValid = PermutationNarayana.NextPermutation(sequenceIndices, (a, b) => a < b);
			} while (orderIsValid);

			throw new NotImplementedException();
		}

	}
}
