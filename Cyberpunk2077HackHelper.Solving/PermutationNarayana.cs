namespace Cyberpunk2077HackHelper.Solving
{
	public static class PermutationNarayana
	{
		/// <summary>
		/// Возвращает true, если value_0 меньше value_1, иначе — false
		/// </summary>
		public delegate bool Less<T>(T value_0, T value_1);

		/// <summary>
		/// Поиск очередной перестановки
		/// </summary>
		public static bool NextPermutation<T>(T[] sequence, Less<T> less)
		{
			// Этап № 1
			int i = sequence.Length;
			do
			{
				if (i < 2)
					return false; // Перебор закончен
				--i;
			} while (!less(sequence[i - 1], sequence[i]));

			// Этап № 2
			int j = sequence.Length;
			while (i < j && !less(sequence[i - 1], sequence[--j]))
				;
			SwapItems(sequence, i - 1, j);

			// Этап № 3
			j = sequence.Length;
			while (i < --j)
				SwapItems(sequence, i++, j);

			return true;
		}

		private static int GetIndexOfFirstDisorder<T>(T[] sequence, Less<T> less)
		{
			for (int i = sequence.Length - 2; i > 0; --i)
			{
				if (less(sequence[i - 1], sequence[i]))
					return i;
			}
			return 0;
		}

		/// <summary>
		/// Обмен значениями двух элементов последовательности
		/// </summary>
		private static void SwapItems<T>(T[] sequence, int index0, int index1)
		{
			T item = sequence[index0];
			sequence[index0] = sequence[index1];
			sequence[index1] = item;
		}
	}
}
