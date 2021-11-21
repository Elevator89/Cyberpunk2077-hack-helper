using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.IO;
using Cyberpunk2077HackHelper.Common;

namespace Cyberpunk2077HackHelper.Grabbing.Test
{
	[TestClass]
	public class GrabbingTests
	{
		[TestMethod]
		public void Grabs7x7()
		{
			Symbol[,] expectedMatrix = new Symbol[,]
			{
				{ Symbol._FF, Symbol._FF, Symbol._55, Symbol._1C, Symbol._55, Symbol._BD, Symbol._FF },
				{ Symbol._FF, Symbol._1C, Symbol._FF, Symbol._1C, Symbol._55, Symbol._1C, Symbol._E9 },
				{ Symbol._E9, Symbol._E9, Symbol._55, Symbol._1C, Symbol._7A, Symbol._7A, Symbol._E9 },
				{ Symbol._1C, Symbol._FF, Symbol._55, Symbol._7A, Symbol._55, Symbol._55, Symbol._1C },
				{ Symbol._BD, Symbol._55, Symbol._7A, Symbol._1C, Symbol._55, Symbol._55, Symbol._BD },
				{ Symbol._7A, Symbol._55, Symbol._1C, Symbol._55, Symbol._55, Symbol._1C, Symbol._55 },
				{ Symbol._FF, Symbol._1C, Symbol._55, Symbol._55, Symbol._E9, Symbol._7A, Symbol._1C },
			};

			Symbol[][] expectedSequences = new Symbol[][]
			{
				new Symbol[] { Symbol._7A, Symbol._55, Symbol._7A },
				new Symbol[] { Symbol._1C, Symbol._1C, Symbol._7A },
				new Symbol[] { Symbol._BD, Symbol._55, Symbol._1C },
			};

			Problem expectedProblem = new Problem(expectedMatrix, expectedSequences, -1);
			Problem actualProblem = LoadProblem("Matrix7x7.png", "Matrix7x7.json");

			CompareProblems(actualProblem, expectedProblem);
		}

		[TestMethod]
		public void Grabs6x6_1()
		{
			Symbol[,] expectedMatrix = new Symbol[,]
			{
				{ Symbol._1C, Symbol._E9, Symbol._1C, Symbol._55, Symbol._55, Symbol._1C },
				{ Symbol._E9, Symbol._55, Symbol._1C, Symbol._55, Symbol._1C, Symbol._55 },
				{ Symbol._7A, Symbol._1C, Symbol._1C, Symbol._1C, Symbol._BD, Symbol._7A },
				{ Symbol._BD, Symbol._7A, Symbol._55, Symbol._55, Symbol._7A, Symbol._1C },
				{ Symbol._7A, Symbol._1C, Symbol._E9, Symbol._E9, Symbol._55, Symbol._7A },
				{ Symbol._1C, Symbol._7A, Symbol._7A, Symbol._7A, Symbol._1C, Symbol._55 },
			};

			Symbol[][] expectedSequences = new Symbol[][]
			{
				new Symbol[] { Symbol._55, Symbol._1C, Symbol._7A },
			};

			Problem expectedProblem = new Problem(expectedMatrix, expectedSequences, -1);
			Problem actualProblem = LoadProblem("Matrix6x6_1.png", "Matrix6x6.json");

			CompareProblems(actualProblem, expectedProblem);
		}

		[TestMethod]
		public void Grabs6x6_2()
		{
			Symbol[,] expectedMatrix = new Symbol[,]
			{
				{ Symbol._1C, Symbol._BD, Symbol._1C, Symbol._1C, Symbol._E9, Symbol._BD },
				{ Symbol._55, Symbol._1C, Symbol._1C, Symbol._E9, Symbol._55, Symbol._BD },
				{ Symbol._55, Symbol._55, Symbol._55, Symbol._7A, Symbol._7A, Symbol._1C },
				{ Symbol._55, Symbol._7A, Symbol._E9, Symbol._E9, Symbol._1C, Symbol._1C },
				{ Symbol._E9, Symbol._E9, Symbol._7A, Symbol._55, Symbol._7A, Symbol._55 },
				{ Symbol._55, Symbol._E9, Symbol._1C, Symbol._7A, Symbol._7A, Symbol._E9 },
			};

			Symbol[][] expectedSequences = new Symbol[][]
			{
				new Symbol[] { Symbol._7A, Symbol._E9, Symbol._E9, Symbol._1C },
			};

			Problem expectedProblem = new Problem(expectedMatrix, expectedSequences, -1);
			Problem actualProblem = LoadProblem("Matrix6x6_2.png", "Matrix6x6.json");

			CompareProblems(actualProblem, expectedProblem);
		}

		private Problem LoadProblem(string screenshotFileName, string layoutFileName)
		{
			Bitmap bitmap = new Bitmap(screenshotFileName);
			string layoutContents = File.ReadAllText(layoutFileName);

			Layout layout = JsonConvert.DeserializeObject<Layout>(layoutContents);
			Grabber grabber = new Grabber();
			return grabber.Grab(bitmap, layout);
		}

		private void CompareProblems(Problem actual, Problem expected)
		{
			Assert.AreEqual(actual.Matrix.GetLength(0), expected.Matrix.GetLength(0));
			Assert.AreEqual(actual.Matrix.GetLength(1), expected.Matrix.GetLength(1));
			Assert.AreEqual(actual.DaemonSequences.Count, expected.DaemonSequences.Count);

			for (int row = 0; row < actual.Matrix.GetLength(0); ++row)
				for (int col = 0; col < actual.Matrix.GetLength(1); ++col)
				{
					Assert.AreEqual(expected.Matrix[row, col], actual.Matrix[row, col], $"Matrix row={row}, col={col}");
				}

			for (int row = 0; row < actual.DaemonSequences.Count; ++row)
			{
				Assert.AreEqual(actual.DaemonSequences[row].Count, expected.DaemonSequences[row].Count);
				for (int col = 0; col < actual.DaemonSequences[row].Count; ++col)
				{
					Assert.AreEqual(expected.DaemonSequences[row][col], actual.DaemonSequences[row][col], $"Sequences row={row}, col={col}");
				}
			}
		}
	}
}
