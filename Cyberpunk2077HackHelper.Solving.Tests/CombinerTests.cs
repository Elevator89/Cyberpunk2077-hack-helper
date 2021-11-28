using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Cyberpunk2077HackHelper.Solving.Tests
{
	[TestClass]
	public class CombinerTests
	{
		[DataTestMethod]
		[DataRow(new[] { 1, 2, 3, 4 }, new[] { 1, 2, 3, 4 }, 0, 0)]
		[DataRow(new[] { 1, 1, 2, 3, 4 }, new[] { 1, 2 }, 0, 1)]
		[DataRow(new[] { 1, 1, 2, 1, 2 }, new[] { 1, 2 }, 2, 3)]
		[DataRow(new[] { 1, 2, 3, 4 }, new[] { 1, 2, 5, 6 }, 0, 4)]
		[DataRow(new[] { 1, 2, 3, 4 }, new[] { 3, 4, 5, 6 }, 0, 2)]
		public void FindsSubsequence(int[] seqA, int[] seqB, int startFrom, int expectedIntersectionStart)
		{
			int actualIntersectionStart = Combiner.GetIntersectionStart(seqA, seqB, startFrom, (a, b) => a == b);
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
			int[] actualSeq = Combiner.GetCombination(seqA, seqB, startFrom, (a, b) => a == b, out int intersectionStart).ToArray();
			CollectionAssert.AreEqual(expSeq, actualSeq);
		}
	}
}
