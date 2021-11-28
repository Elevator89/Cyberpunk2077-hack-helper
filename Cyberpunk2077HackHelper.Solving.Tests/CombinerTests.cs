using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cyberpunk2077HackHelper.Solving.Tests
{
	[TestClass]
	public class CombinerTests
	{
		private Combiner<int> _combiner;

		[TestInitialize]
		public void Init()
		{
			_combiner = new Combiner<int>(EqualityComparer<int>.Default);
		}

		[DataTestMethod]
		[DataRow(new[] { 1, 2, 3, 4 }, new[] { 1, 2, 3, 4 }, 0, 0)]
		[DataRow(new[] { 1, 1, 2, 3, 4 }, new[] { 1, 2 }, 0, 1)]
		[DataRow(new[] { 1, 1, 2, 1, 2 }, new[] { 1, 2 }, 2, 3)]
		[DataRow(new[] { 1, 2, 3, 4 }, new[] { 1, 2, 5, 6 }, 0, 4)]
		[DataRow(new[] { 1, 2, 3, 4 }, new[] { 3, 4, 5, 6 }, 0, 2)]
		public void FindsSubsequence(int[] seqA, int[] seqB, int startFrom, int expectedIntersectionStart)
		{
			int actualIntersectionStart = _combiner.GetIntersectionStart(seqA, seqB, startFrom);
			Assert.AreEqual(expectedIntersectionStart, actualIntersectionStart);
		}

		[DataTestMethod]
		[DataRow(new[] { 1, 2, 3, 4 }, new[] { 1, 2, 3, 4 }, 0, new[] { 1, 2, 3, 4 })]
		[DataRow(new[] { 1, 1, 2, 3, 4 }, new[] { 1, 2 }, 0, new[] { 1, 1, 2, 3, 4 })]
		[DataRow(new[] { 1, 1, 2, 1, 2 }, new[] { 1, 2 }, 2, new[] { 1, 1, 2, 1, 2 })]
		[DataRow(new[] { 1, 2, 3, 4 }, new[] { 1, 2, 5, 6 }, 0, new[] { 1, 2, 3, 4, 1, 2, 5, 6 })]
		[DataRow(new[] { 1, 2, 3, 4 }, new[] { 3, 4, 5, 6 }, 0, new[] { 1, 2, 3, 4, 5, 6 })]
		[DataRow(new[] { 1, 2 }, new[] { 1, 2, 3, 4 }, 0, new[] { 1, 2, 3, 4 })]
		[DataRow(new[] { 0, 1, 1, 2, 1, 2, 1, 3 }, new[] { 1, 2 }, 0, new[] { 0, 1, 1, 2, 1, 2, 1, 3 })]
		[DataRow(new[] { 0, 1, 1, 2, 1, 2, 1, 3 }, new[] { 1, 2 }, 3, new[] { 0, 1, 1, 2, 1, 2, 1, 3 })]
		[DataRow(new[] { 0, 1, 1, 2, 1, 2, 1, 3 }, new[] { 1, 2 }, 5, new[] { 0, 1, 1, 2, 1, 2, 1, 3, 1, 2 })]
		public void GetsCombination(int[] seqA, int[] seqB, int startFrom, int[] expSeq)
		{
			int[] actualSeq = _combiner.GetCombination(seqA, seqB, startFrom, out int intersectionStart).ToArray();
			CollectionAssert.AreEqual(expSeq, actualSeq);
		}

		[DataTestMethod]
		[DataRow(0)]
		[DataRow(1)]
		[DataRow(2)]
		public void GetPossibleCombinations(int caseId)
		{
			(IReadOnlyList<int> seqA, IReadOnlyList<int> seqB, int maxCombinationLength, int wildValue, int wildMaxCount, IReadOnlyList<int>[] expectedCombinations) = GetSequencesAndCombinations(caseId);

			IReadOnlyList<int>[] combinations = _combiner.GetPossibleCombinations(seqA, seqB, maxCombinationLength, wildValue, wildMaxCount).ToArray();

			Assert.AreEqual(expectedCombinations.Length, combinations.Length);
			for (int i = 0; i < combinations.Length; ++i)
				CollectionAssert.AreEqual(expectedCombinations[i].ToArray(), combinations[i].ToArray());
		}

		[DataTestMethod]
		[DataRow(0)]
		[DataRow(1)]
		[DataRow(2)]
		public void GetAllPossibleCombinations(int caseId)
		{
			(IReadOnlyList<int>[] sequences, int maxCombinationLength, int wildValue, int wildMaxCount, IReadOnlyList<int>[] expectedCombinations) = GetAllSequencesAndCombinations(caseId);

			IReadOnlyList<int>[] combinations = _combiner.GetPossibleCombinations(sequences, maxCombinationLength, wildValue, wildMaxCount).ToArray();

			Assert.AreEqual(expectedCombinations.Length, combinations.Length);
			for (int i = 0; i < combinations.Length; ++i)
				CollectionAssert.AreEqual(expectedCombinations[i].ToArray(), combinations[i].ToArray());
		}

		private (IReadOnlyList<int> seqA, IReadOnlyList<int> seqB, int maxCombinationLength, int wildValue, int wildMaxCount, IReadOnlyList<int>[] expectedCombinations) GetSequencesAndCombinations(int caseId)
		{
			switch (caseId)
			{
				case 0:
					return (
						new[] { 1, 2 },
						new[] { 1, 2 },
						100, -1, 0,
						new[] {
							new[] { 1, 2 },
							new[] { 1, 2, 1, 2 }
						});
				case 1:
					return (
						new[] { 1, 2 },
						new[] { 1, 2 },
						100, -1, 1,
						new[] {
							new[] { 1, 2 },
							new[] { 1, 2, 1, 2 },
							new[] { 1, 2, -1, 1, 2 },
						});
				case 2:
					return (
						new[] { 1, 2 },
						new[] { 1, 2 },
						100, -1, 2,
						new[] {
							new[] { 1, 2 },
							new[] { 1, 2, 1, 2 },
							new[] { 1, 2, -1, 1, 2 },
							new[] { 1, 2, -1, -1, 1, 2 },
						});
				default:
					throw new NotImplementedException();
			}
		}

		private (IReadOnlyList<int>[] sequences, int maxCombinationLength, int wildValue, int wildMaxCount, IReadOnlyList<int>[] expectedCombinations) GetAllSequencesAndCombinations(int caseId)
		{
			switch (caseId)
			{
				case 0:
					return (
						new[] {
							new[] { 1, 2 },
							new[] { 1, 2 },
							new[] { 1, 2 },
						},
						100, -1, 0,
						new[] {
							new[] { 1, 2 },
							new[] { 1, 2, 1, 2 },
							new[] { 1, 2, 1, 2, 1, 2 },
						});
				case 1:
					return (
						new[] {
							new[] { 1, 2 },
							new[] { 3, 4 },
							new[] { 5, 6 },
						},
						100, -1, 1,
						new[] {
							new[] { 1, 2, 3, 4, 5, 6 },
							new[] { 1, 2, 3, 4, -1, 5, 6 },
							new[] { 1, 2, -1, 3, 4, 5, 6 },
							new[] { 1, 2, -1, 3, 4, -1, 5, 6 },
							new[] { -1, 1, 2, 3, 4, 5, 6 },
							new[] { -1, 1, 2, 3, 4, -1, 5, 6 },
							new[] { -1, 1, 2, -1, 3, 4, 5, 6 },
							new[] { -1, 1, 2, -1, 3, 4, -1, 5, 6 },
						});
				case 2:
					return (
						new[] {
							new[] { 1, 2 },
							new[] { 3, 4 },
							new[] { 5, 6 },
						},
						7, -1, 1,
						new[] {
							new[] { 1, 2, 3, 4, 5, 6 },
							new[] { 1, 2, 3, 4, -1, 5, 6 },
							new[] { 1, 2, -1, 3, 4, 5, 6 },
							new[] { -1, 1, 2, 3, 4, 5, 6 },
						});
				default:
					throw new NotImplementedException();
			}
		}
	}
}
