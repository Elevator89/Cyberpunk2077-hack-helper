using System;
using System.Collections.Generic;
using System.Linq;

namespace Cyberpunk2077HackHelper.Solving
{
	public delegate bool Equals<T>(T a, T b);

	public class Combiner<T>
	{
		private class ListComparer : IEqualityComparer<IReadOnlyList<T>>
		{
			private IEqualityComparer<T> _comparer;

			public ListComparer(IEqualityComparer<T> comparer)
			{
				_comparer = comparer;
			}

			public bool Equals(IReadOnlyList<T> x, IReadOnlyList<T> y)
			{
				if (x.Count != y.Count)
					return false;

				for (int i = 0; i < x.Count; ++i)
				{
					if (!_comparer.Equals(x[i], y[i]))
						return false;
				}
				return true;
			}

			public int GetHashCode(IReadOnlyList<T> obj)
			{
				int hash = 0;
				for (int i = 0; i < obj.Count; ++i)
				{
					hash = ShiftAndWrap(hash, 2) ^ _comparer.GetHashCode(obj[i]);
				}
				return hash;
			}

			private static int ShiftAndWrap(int value, int positions)
			{
				positions = positions & 0x1F;

				// Save the existing bit pattern, but interpret it as an unsigned integer.
				uint number = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
				// Preserve the bits to be discarded.
				uint wrapped = number >> (32 - positions);
				// Shift and wrap the discarded bits.
				return BitConverter.ToInt32(BitConverter.GetBytes((number << positions) | wrapped), 0);
			}
		}

		private readonly IEqualityComparer<T> _comparer;
		private readonly ListComparer _listComparer;

		public Combiner(IEqualityComparer<T> comparer)
		{
			_comparer = comparer;
			_listComparer = new ListComparer(_comparer);
		}

		public IEnumerable<IReadOnlyList<T>> GetPossibleCombinations(IReadOnlyList<IReadOnlyList<T>> sequences, T wildValue, int wildMaxCount)
		{
			foreach (IReadOnlyList<T> fullCombination in GetPossibleCombinationsRecursively(sequences[0], sequences, 1, wildValue, wildMaxCount).Distinct(_listComparer))
			{
				for (int wildCount = 0; wildCount < wildMaxCount; ++wildCount)
					yield return Enumerable.Repeat(wildValue, wildCount).Concat(fullCombination).ToArray();
			}
		}

		private IEnumerable<IReadOnlyList<T>> GetPossibleCombinationsRecursively(IReadOnlyList<T> currentCombination, IReadOnlyList<IReadOnlyList<T>> sequences, int startFrom, T wildValue, int wildMaxCount)
		{
			if (startFrom == sequences.Count)
				yield return currentCombination;
			else
				foreach (IReadOnlyList<T> newCombination in GetPossibleCombinations(currentCombination, sequences[startFrom], wildValue, wildMaxCount).Distinct(_listComparer))
				{
					foreach (IReadOnlyList<T> fullCombination in GetPossibleCombinationsRecursively(newCombination, sequences, startFrom + 1, wildValue, wildMaxCount).Distinct(_listComparer))
						yield return fullCombination;
				}
		}

		public IEnumerable<IReadOnlyList<T>> GetPossibleCombinations(IReadOnlyList<T> seqA, IReadOnlyList<T> seqB, T wildValue, int wildMaxCount)
		{
			int intersectionStart = 0;
			do
			{
				yield return GetCombination(seqA, seqB, intersectionStart, out intersectionStart);
				intersectionStart++;
			}
			while (intersectionStart <= seqA.Count);

			for (int wildCount = 1; wildCount < wildMaxCount; ++wildCount)
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
