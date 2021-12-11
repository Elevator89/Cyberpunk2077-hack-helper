﻿using Cyberpunk2077HackHelper.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Cyberpunk2077HackHelper.Solving.Tests
{
	[TestClass]
	public class SolverTests
	{
		private Walker _solver;
		private Combiner<Symbol> _combiner;

		[TestInitialize]
		public void Init()
		{
			_solver = new Walker();
			_combiner = new Combiner<Symbol>(EqualityComparer<Symbol>.Default);
		}

		[DataTestMethod]
		[DataRow(61)]
		[DataRow(62)]
		[DataRow(63)]
		[DataRow(71)]
		[DataRow(72)]
		[DataRow(73)]
		[DataRow(74)]
		public void Solves(int caseId)
		{
			Problem problem = GetProblem(caseId);
			IEnumerable<IReadOnlyList<Symbol>> combinations = _combiner.GetUnorderedSequenceCombinations(problem.DaemonSequences, 8, Symbol.Unknown, 1);
			foreach (IReadOnlyList<Symbol> combination in combinations.OrderBy(c => c.Count))
			{
				IReadOnlyList<System.Drawing.Point>[] solutions = _solver.Walk(problem.Matrix, combination).ToArray();
			}
		}

		private Problem GetProblem(int caseId)
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
					7);

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
					7);

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
					7);

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
					7);

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
					7);

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
					7);

				default:
					return null;
			}
		}

	}
}
