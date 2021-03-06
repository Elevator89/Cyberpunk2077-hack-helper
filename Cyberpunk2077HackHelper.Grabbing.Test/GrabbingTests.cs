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
			string layoutContents = File.ReadAllText("Data/SymbolMaps/matrixSymbolMaps.json");
			string sequenceContents = File.ReadAllText("Data/SymbolMaps/sequenceSymbolMaps.json");

			List<SymbolMap> matrixSymbolMaps = JsonConvert.DeserializeObject<List<SymbolMap>>(layoutContents);
			List<SymbolMap> sequenceSymbolMaps = JsonConvert.DeserializeObject<List<SymbolMap>>(sequenceContents);
			_grabber = new Grabber(matrixSymbolMaps, sequenceSymbolMaps);
		}

		[DataTestMethod]
		[DataRow(61, "Data/Screenshots/Matrix6_1.png", "Data/Layouts/Matrix6.json")]
		[DataRow(62, "Data/Screenshots/Matrix6_2.png", "Data/Layouts/Matrix6.json")]
		[DataRow(71, "Data/Screenshots/Matrix7_1.png", "Data/Layouts/Matrix7.json")]
		[DataRow(72, "Data/Screenshots/Matrix7_2.png", "Data/Layouts/Matrix7.json")]
		[DataRow(73, "Data/Screenshots/Matrix7_3.png", "Data/Layouts/Matrix7.json")]
		[DataRow(74, "Data/Screenshots/Matrix7_4.png", "Data/Layouts/Matrix7.json")]
		public void Grabs(int caseId, string screenshotFileName, string layoutFileName)
		{
			Grabs(screenshotFileName, layoutFileName, GetExpectedProblem(caseId));
		}

		private void Grabs(string screenshotFileName, string layoutFileName, Problem expectedProblem)
		{
			Problem actualProblem = GrabProblem(screenshotFileName, layoutFileName);
			CompareProblems(actualProblem, expectedProblem);
		}

		private Problem GrabProblem(string screenshotFileName, string layoutFileName)
		{
			Bitmap bitmap = new Bitmap(screenshotFileName);
			string layoutContents = File.ReadAllText(layoutFileName);

			Layout layout = JsonConvert.DeserializeObject<Layout>(layoutContents);
			return _grabber.Grab(bitmap, layout);
		}

		private void CompareProblems(Problem actual, Problem expected)
		{
			Assert.AreEqual(expected.Matrix.GetLength(0), actual.Matrix.GetLength(0));
			Assert.AreEqual(expected.Matrix.GetLength(1), actual.Matrix.GetLength(1));
			Assert.AreEqual(expected.DaemonSequences.Count, actual.DaemonSequences.Count);

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

		private Problem GetExpectedProblem(int caseId)
		{
			switch (caseId)
			{
				case 61:
					return Problem.FromHex(new int[,]
					{
						{ 0x1C, 0xE9, 0x1C, 0x55, 0x55, 0x1C },
						{ 0xE9, 0x55, 0x1C, 0x55, 0x1C, 0x55 },
						{ 0x7A, 0x1C, 0x1C, 0x1C, 0xBD, 0x7A },
						{ 0xBD, 0x7A, 0x55, 0x55, 0x7A, 0x1C },
						{ 0x7A, 0x1C, 0xE9, 0xE9, 0x55, 0x7A },
						{ 0x1C, 0x7A, 0x7A, 0x7A, 0x1C, 0x55 },
					},
					new int[][]
					{
						new int[] { 0x55, 0x1C, 0x7A },
					},
					-1);

				case 62:
					return Problem.FromHex(new int[,]
					{
						{ 0x1C, 0xBD, 0x1C, 0x1C, 0xE9, 0xBD },
						{ 0x55, 0x1C, 0x1C, 0xE9, 0x55, 0xBD },
						{ 0x55, 0x55, 0x55, 0x7A, 0x7A, 0x1C },
						{ 0x55, 0x7A, 0xE9, 0xE9, 0x1C, 0x1C },
						{ 0xE9, 0xE9, 0x7A, 0x55, 0x7A, 0x55 },
						{ 0x55, 0xE9, 0x1C, 0x7A, 0x7A, 0xE9 },
					},
					new int[][]
					{
						new int[] { 0x7A, 0xE9, 0xE9, 0x1C },
					},
					-1);

				case 71:
					return Problem.FromHex(new int[,]
					{
						{ 0x1C, 0x7A, 0xE9, 0x55, 0x55, 0xE9, 0x7A },
						{ 0x1C, 0x1C, 0xE9, 0x7A, 0xFF, 0x7A, 0x7A },
						{ 0xBD, 0x1C, 0xBD, 0x1C, 0x55, 0x7A, 0x7A },
						{ 0x1C, 0x55, 0x55, 0xFF, 0xE9, 0x55, 0x7A },
						{ 0x55, 0x55, 0x55, 0xE9, 0x55, 0x7A, 0x7A },
						{ 0xBD, 0x1C, 0x55, 0x1C, 0xBD, 0xFF, 0x1C },
						{ 0x7A, 0x7A, 0xE9, 0x55, 0xBD, 0x7A, 0x55 },
					},
					new int[][]
					{
						new int[] { 0x55, 0xBD },
						new int[] { 0xBD, 0x55, 0xE9, 0x7A },
						new int[] { 0x7A, 0x7A, 0x7A, 0x55 },
					},
					-1);

				case 72:
					return Problem.FromHex(new int[,]
					{
						{ 0xBD, 0x7A, 0xFF, 0xBD, 0x55, 0x1C, 0x1C },
						{ 0x55, 0xFF, 0xFF, 0x55, 0x55, 0x7A, 0x7A },
						{ 0xE9, 0xBD, 0xFF, 0x55, 0xE9, 0x1C, 0x55 },
						{ 0x7A, 0x1C, 0x55, 0xBD, 0x7A, 0xBD, 0xFF },
						{ 0xE9, 0xE9, 0x1C, 0xBD, 0x7A, 0xE9, 0xFF },
						{ 0x7A, 0x1C, 0x7A, 0xFF, 0xBD, 0xE9, 0xFF },
						{ 0x1C, 0xE9, 0xE9, 0x55, 0x1C, 0x7A, 0x55 },
					},
					new int[][]
					{
						new int[] { 0xE9, 0x1C, 0xE9 },
						new int[] { 0xE9, 0xE9, 0xFF },
						new int[] { 0xFF, 0x55, 0xFF },
					},
					-1);

				case 73:
					return Problem.FromHex(new int[,]
					{
						{ 0x1C, 0x55, 0xE9, 0x1C, 0xFF, 0xFF, 0x7A },
						{ 0x7A, 0x7A, 0x55, 0x7A, 0x55, 0x55, 0xBD },
						{ 0xBD, 0xE9, 0x1C, 0x55, 0xE9, 0x55, 0x1C },
						{ 0x55, 0xFF, 0x55, 0x7A, 0xBD, 0x7A, 0x7A },
						{ 0x7A, 0xBD, 0x1C, 0xBD, 0x1C, 0xBD, 0x7A },
						{ 0x55, 0x1C, 0x1C, 0x7A, 0x55, 0x7A, 0xFF },
						{ 0x55, 0xBD, 0x7A, 0x7A, 0x7A, 0xBD, 0xFF },
					},
					new int[][]
					{
						new int[] { 0x1C, 0x7A, 0x7A },
						new int[] { 0xE9, 0x7A, 0xFF, 0x1C },
						new int[] { 0x1C, 0x1C, 0xBD, 0x1C },
					},
					-1);

				case 74:
					return Problem.FromHex(new int[,]
					{
						{ 0xFF, 0xFF, 0x55, 0x1C, 0x55, 0xBD, 0xFF },
						{ 0xFF, 0x1C, 0xFF, 0x1C, 0x55, 0x1C, 0xE9 },
						{ 0xE9, 0xE9, 0x55, 0x1C, 0x7A, 0x7A, 0xE9 },
						{ 0x1C, 0xFF, 0x55, 0x7A, 0x55, 0x55, 0x1C },
						{ 0xBD, 0x55, 0x7A, 0x1C, 0x55, 0x55, 0xBD },
						{ 0x7A, 0x55, 0x1C, 0x55, 0x55, 0x1C, 0x55 },
						{ 0xFF, 0x1C, 0x55, 0x55, 0xE9, 0x7A, 0x1C },
					},
					new int[][]
					{
						new int[] { 0x7A, 0x55, 0x7A },
						new int[] { 0x1C, 0x1C, 0x7A },
						new int[] { 0xBD, 0x55, 0x1C },
					},
					-1);

				default:
					return null;
			}
		}
	}
}
