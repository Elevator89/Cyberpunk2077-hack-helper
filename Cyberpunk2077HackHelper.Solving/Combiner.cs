using System.Collections.Generic;
using System.Linq;

namespace Cyberpunk2077HackHelper.Solving
{
	public delegate bool Equals<T>(T a, T b);

	public class Combiner
	{
		public static IEnumerable<IReadOnlyList<T>> GetPossibleCombinations<T>(IReadOnlyList<IReadOnlyList<T>> sequences, Equals<T> equals, T wildValue, int wildMaxCount)
		{
			foreach (IReadOnlyList<T> fullCombination in GetPossibleCombinationsRecursively(sequences[0], sequences, 1, equals, wildValue, wildMaxCount))
			{
				yield return fullCombination;
			}
		}

		private static IEnumerable<IReadOnlyList<T>> GetPossibleCombinationsRecursively<T>(IReadOnlyList<T> currentCombination, IReadOnlyList<IReadOnlyList<T>> sequences, int startFrom, Equals<T> equals, T wildValue, int wildMaxCount)
		{
			if (startFrom == sequences.Count)
				yield return currentCombination;
			else
				foreach (IReadOnlyList<T> newCombination in GetPossibleCombinations(currentCombination, sequences[startFrom], equals, wildValue, wildMaxCount))
				{
					foreach (IReadOnlyList<T> fullCombination in GetPossibleCombinationsRecursively(newCombination, sequences, startFrom + 1, equals, wildValue, wildMaxCount))
						yield return fullCombination;
				}
		}

		public static IEnumerable<IReadOnlyList<T>> GetPossibleCombinations<T>(IReadOnlyList<T> seqA, IReadOnlyList<T> seqB, Equals<T> equals, T wildValue, int wildMaxCount)
		{
			int intersectionStart = 0;
			do
			{
				yield return GetCombination(seqA, seqB, intersectionStart, equals, out intersectionStart);
				intersectionStart++;
			}
			while (intersectionStart <= seqA.Count);

			for (int wildCount = 0; wildCount < wildMaxCount; ++wildCount)
				yield return seqA.Concat(Enumerable.Repeat(wildValue, wildCount)).Concat(seqB).ToArray();
		}

		public static IReadOnlyList<T> GetCombination<T>(IReadOnlyList<T> seqA, IReadOnlyList<T> seqB, int startFrom, Equals<T> equals, out int intersectionStart)
		{
			intersectionStart = GetIntersectionStart(seqA, seqB, startFrom, equals);
			if (seqA.Count - intersectionStart < seqB.Count)
				return seqA.Take(intersectionStart).Concat(seqB).ToArray();
			return seqA;
		}

		public static int GetIntersectionStart<T>(IReadOnlyList<T> seqA, IReadOnlyList<T> seqB, int startFrom, Equals<T> equals)
		{
			// A: 1 1 1 2 3 4
			// B: 1 1 2 3  

			int indA = startFrom;
			int indB = 0;
			int intersectionStart = startFrom;

			while (indA < seqA.Count && indB < seqB.Count)
			{
				if (equals(seqA[indA], seqB[indB]))
				{
					indA++;
					indB++;
				}
				else
				{
					indA = ++intersectionStart;
					indB = 0;
				}
			}
			return intersectionStart;
		}
	}
}
