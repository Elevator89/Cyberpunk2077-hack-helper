using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.IO;
using Cyberpunk2077HackHelper.Common;
using System.Collections.Generic;

namespace Cyberpunk2077HackHelper.Grabbing.Test
{
	[TestClass]
	public class GrabbingTests
	{
		private Grabber _grabber;

		[TestInitialize]
		public void Init()
		{
			string layoutContents = File.ReadAllText("TestData/matrixSymbolMaps.json");
			string sequenceContents = File.ReadAllText("TestData/sequenceSymbolMaps.json");

			List<SymbolMap> matrixSymbolMaps = JsonConvert.DeserializeObject<List<SymbolMap>>(layoutContents);
			List<SymbolMap> sequenceSymbolMaps = JsonConvert.DeserializeObject<List<SymbolMap>>(sequenceContents);
			_grabber = new Grabber(matrixSymbolMaps, sequenceSymbolMaps);
		}

		[DataTestMethod]
		[DataRow(61, "TestData/Matrix6_1.png", "TestData/Matrix6.json")]
		[DataRow(62, "TestData/Matrix6_2.png", "TestData/Matrix6.json")]
		[DataRow(74, "TestData/Matrix7_4.png", "TestData/Matrix7.json")]
		public void Grabs(int caseId, string screenshotFileName, string layoutFileName)
		{
			int[,] expectedMatrix = GetExpectedMatrix(caseId);
			int[][] expectedSequences = GetExpectedSequences(caseId);

			Grabs(expectedMatrix, expectedSequences, screenshotFileName, layoutFileName);
		}

		private void Grabs(int[,] expectedMatrix, int[][] expectedSequences, string screenshotFileName, string layoutFileName)
		{
			Problem expectedProblem = Problem.FromHex(expectedMatrix, expectedSequences, -1);
			Problem actualProblem = LoadProblem(screenshotFileName, layoutFileName);

			CompareProblems(actualProblem, expectedProblem);
		}

		private Problem LoadProblem(string screenshotFileName, string layoutFileName)
		{
			Bitmap bitmap = new Bitmap(screenshotFileName);
			string layoutContents = File.ReadAllText(layoutFileName);

			Layout layout = JsonConvert.DeserializeObject<Layout>(layoutContents);
			return _grabber.Grab(bitmap, layout);
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

		private int[,] GetExpectedMatrix(int caseId)
		{
			switch (caseId)
			{
				case 61:
					return new int[,]
					{
						{ 0x1C, 0xE9, 0x1C, 0x55, 0x55, 0x1C },
						{ 0xE9, 0x55, 0x1C, 0x55, 0x1C, 0x55 },
						{ 0x7A, 0x1C, 0x1C, 0x1C, 0xBD, 0x7A },
						{ 0xBD, 0x7A, 0x55, 0x55, 0x7A, 0x1C },
						{ 0x7A, 0x1C, 0xE9, 0xE9, 0x55, 0x7A },
						{ 0x1C, 0x7A, 0x7A, 0x7A, 0x1C, 0x55 },
					};

				case 62:
					return new int[,]
					{
						{ 0x1C, 0xBD, 0x1C, 0x1C, 0xE9, 0xBD },
						{ 0x55, 0x1C, 0x1C, 0xE9, 0x55, 0xBD },
						{ 0x55, 0x55, 0x55, 0x7A, 0x7A, 0x1C },
						{ 0x55, 0x7A, 0xE9, 0xE9, 0x1C, 0x1C },
						{ 0xE9, 0xE9, 0x7A, 0x55, 0x7A, 0x55 },
						{ 0x55, 0xE9, 0x1C, 0x7A, 0x7A, 0xE9 },
					};

				case 74:
					return new int[,]
					{
						{ 0xFF, 0xFF, 0x55, 0x1C, 0x55, 0xBD, 0xFF },
						{ 0xFF, 0x1C, 0xFF, 0x1C, 0x55, 0x1C, 0xE9 },
						{ 0xE9, 0xE9, 0x55, 0x1C, 0x7A, 0x7A, 0xE9 },
						{ 0x1C, 0xFF, 0x55, 0x7A, 0x55, 0x55, 0x1C },
						{ 0xBD, 0x55, 0x7A, 0x1C, 0x55, 0x55, 0xBD },
						{ 0x7A, 0x55, 0x1C, 0x55, 0x55, 0x1C, 0x55 },
						{ 0xFF, 0x1C, 0x55, 0x55, 0xE9, 0x7A, 0x1C },
					};
				default:
					return null;
			}
		}

		private int[][] GetExpectedSequences(int caseId)
		{
			switch (caseId)
			{
				case 61:
					return new int[][]
					{
						new int[] { 0x55, 0x1C, 0x7A },
					};

				case 62:
					return new int[][]
					{
						new int[] { 0x7A, 0xE9, 0xE9, 0x1C },
					};

				case 74:
					return new int[][]
					{
						new int[] { 0x7A, 0x55, 0x7A },
						new int[] { 0x1C, 0x1C, 0x7A },
						new int[] { 0xBD, 0x55, 0x1C },
					};
				default:
					return null;
			}
		}
	}
}
