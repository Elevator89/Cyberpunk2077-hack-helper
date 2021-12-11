using System.Collections.Generic;
using System.Linq;

namespace Cyberpunk2077HackHelper.Solving
{
	public delegate bool Equals<T>(T a, T b);

	public class Combiner<T>
	{
		private readonly IEqualityComparer<T> _comparer;
		private readonly ListComparer<T> _listComparer;

		public Combiner(IEqualityComparer<T> comparer)
		{
			_comparer = comparer;
			_listComparer = new ListComparer<T>(_comparer);
		}

		public IEnumerable<IReadOnlyList<T>> GetUnorderedSequenceCombinations(IReadOnlyList<IReadOnlyList<T>> sequences, int maxCombinationLength, T wildValue, int wildMaxCount)
		{
			int[] sequenceIndices = Enumerable.Range(0, sequences.Count).ToArray();
			bool orderIsValid = true;
			do
			{
				IReadOnlyList<T>[] orderedSequences = sequenceIndices.Select(i => sequences[i]).ToArray();
				foreach (IReadOnlyList<T> possibleCombination in GetOrderedSequenceCombinations(orderedSequences, maxCombinationLength, wildValue, wildMaxCount))
					yield return possibleCombination;

				orderIsValid = PermutationNarayana.NextPermutation(sequenceIndices, (a, b) => a < b);

			} while (orderIsValid);
		}

		public IEnumerable<IReadOnlyList<T>> GetOrderedSequenceCombinations(IReadOnlyList<IReadOnlyList<T>> sequences, int maxCombinationLength, T wildValue, int wildMaxCount)
		{
			for (int wildCount = 0; wildCount <= wildMaxCount && wildCount + sequences[0].Count <= maxCombinationLength; ++wildCount)
			{
				IReadOnlyList<T> initialSequence = Enumerable.Repeat(wildValue, wildCount).Concat(sequences[0]).ToArray();
				foreach (IReadOnlyList<T> fullCombination in GetCombinationsRecursively(initialSequence, sequences, 1, maxCombinationLength, wildValue, wildMaxCount).Distinct(_listComparer))
					yield return fullCombination;
			}
		}

		private IEnumerable<IReadOnlyList<T>> GetCombinationsRecursively(IReadOnlyList<T> currentCombination, IReadOnlyList<IReadOnlyList<T>> sequences, int startFrom, int maxCombinationLength, T wildValue, int wildMaxCount)
		{
			if (startFrom == sequences.Count)
				yield return currentCombination;
			else
				foreach (IReadOnlyList<T> newCombination in GetCombinations(currentCombination, sequences[startFrom], maxCombinationLength, wildValue, wildMaxCount).Distinct(_listComparer))
				{
					foreach (IReadOnlyList<T> fullCombination in GetCombinationsRecursively(newCombination, sequences, startFrom + 1, maxCombinationLength, wildValue, wildMaxCount).Distinct(_listComparer))
						yield return fullCombination;
				}
		}

		public IEnumerable<IReadOnlyList<T>> GetCombinations(IReadOnlyList<T> seqA, IReadOnlyList<T> seqB, int maxCombinationLength, T wildValue, int wildMaxCount)
		{
			if (seqA.Count > maxCombinationLength || seqB.Count > maxCombinationLength)
				yield break;

			int intersectionStart = 0;
			do
			{
				IReadOnlyList<T> combination = GetCombination(seqA, seqB, intersectionStart, out intersectionStart);
				if (combination.Count <= maxCombinationLength)
				{
					yield return combination;
					intersectionStart++;
				}
				else
				{
					yield break;
				}
			}
			while (intersectionStart <= seqA.Count);

			for (int wildCount = 1; wildCount <= wildMaxCount && seqA.Count + wildCount + seqB.Count <= maxCombinationLength; ++wildCount)
				yield return seqA.Concat(Enumerable.Repeat(wildValue, wildCount)).Concat(seqB).ToArray();
		}

		public IReadOnlyList<T> GetCombination(IReadOnlyList<T> seqA, IReadOnlyList<T> seqB, int startFrom, out int intersectionStart)
		{
			intersectionStart = GetIntersectionStart(seqA, seqB, startFrom);
			if (seqA.Count - intersectionStart < seqB.Count)
				return seqA.Take(intersectionStart).Concat(seqB).ToArray();
			return seqA;
		}

		public int GetIntersectionStart(IReadOnlyList<T> seqA, IReadOnlyList<T> seqB, int startFrom)
		{
			int indA = startFrom;
			int indB = 0;
			int intersectionStart = startFrom;

			while (indA < seqA.Count && indB < seqB.Count)
			{
				if (_comparer.Equals(seqA[indA], seqB[indB]))
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
