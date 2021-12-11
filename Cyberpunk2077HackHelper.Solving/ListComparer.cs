using System;
using System.Collections.Generic;

namespace Cyberpunk2077HackHelper.Solving
{

	public class ListComparer<T> : IEqualityComparer<IReadOnlyList<T>>
	{
		private readonly IEqualityComparer<T> _comparer;

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
}
