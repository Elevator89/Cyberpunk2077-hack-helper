namespace Cyberpunk2077HackHelper.Solving
{
	public static class PermutationPascal
	{
		public static bool NextPer(int[] m, int n)
		{
			int numbesp, indmax = 0;

			// Зададим начальные границы рассматриваемой перестановки
			int i1 = 0;
			int i2 = n - 1;

			// Зададим максимальный элемент в рассматриваемой перестановке
			int max = n - 1;
			while (i1 < i2)
			{
				// Подсчитаем число беспорядков для чисел, меньших max
				label1:
				numbesp = 0;
				for (int i = i1; i < i2 - 2; ++i)
				{
					if (m[i] < max)
					{
						for (int j = i + 1; j < i2 - 1; ++j)
							if (m[j] < m[i])
								numbesp++;
					}
				}

				// Найдем индекс indmax числа max в перестановке m }
				for (int i = i1; i < i2 - 1; ++i)
				{
					if (m[i] == max)
					{
						indmax = i;
						goto label2;
					}
				}

				label2:
				if (numbesp % 2 == 1)
				{

					if (indmax != i2)
					{

						m[indmax] = m[indmax + 1];
						m[indmax + 1] = max;
						return true;
					};
					// Переходим к  перестановке без max
					max--;
					i2--;
					goto label1;
				}

				if (indmax != i1)
				{
					m[indmax] = m[indmax - 1];
					m[indmax - 1] = max;
					return true;
				}
				// Переходим к перестановке без max
				max--;
				i1++;
			}
			return false;
		}
	}
}
