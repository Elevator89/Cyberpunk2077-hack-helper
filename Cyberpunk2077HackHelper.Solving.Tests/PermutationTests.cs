using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cyberpunk2077HackHelper.Solving.Tests
{

	[TestClass]
	public class PermutationTests
	{
		[TestMethod]
		public void Narayana()
		{
			int[] array = new[] { 1, 2, 3, 4 };

			PermutationNarayana.NextPermutation(array, (a, b) => a < b);
			CollectionAssert.AreEqual(new int[] { 1, 2, 4, 3 }, array);

			PermutationNarayana.NextPermutation(array, (a, b) => a < b);
			CollectionAssert.AreEqual(new int[] { 1, 3, 2, 4 }, array);

			PermutationNarayana.NextPermutation(array, (a, b) => a < b);
			CollectionAssert.AreEqual(new int[] { 1, 3, 4, 2 }, array);

			PermutationNarayana.NextPermutation(array, (a, b) => a < b);
			CollectionAssert.AreEqual(new int[] { 1, 4, 2, 3 }, array);

			PermutationNarayana.NextPermutation(array, (a, b) => a < b);
			CollectionAssert.AreEqual(new int[] { 1, 4, 3, 2 }, array);

			PermutationNarayana.NextPermutation(array, (a, b) => a < b);
			CollectionAssert.AreEqual(new int[] { 2, 1, 3, 4 }, array);
		}
	}
}
