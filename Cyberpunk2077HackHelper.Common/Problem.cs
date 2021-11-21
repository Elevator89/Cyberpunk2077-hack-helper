using System.Collections.Generic;
using System.Linq;

namespace Cyberpunk2077HackHelper.Common
{
	public class Problem
	{
		public readonly Symbol[,] Matrix;
		public readonly IReadOnlyList<IReadOnlyList<Symbol>> DaemonSequences;
		public readonly int BufferLength;

		public Problem(Symbol[,] matrix, IReadOnlyList<IReadOnlyList<Symbol>> daemonSequences, int bufferLength)
		{
			Matrix = matrix;
			DaemonSequences = daemonSequences;
			BufferLength = bufferLength;
		}

		public static Problem FromHex(int[,] matrixHex, IReadOnlyList<IReadOnlyList<int>> daemonSequencesHex, int bufferLength)
		{
			Symbol[,] matrix = new Symbol[matrixHex.GetLength(0), matrixHex.GetLength(1)];
			for (int row = 0; row < matrixHex.GetLength(0); ++row)
				for (int col = 0; col < matrixHex.GetLength(1); ++col)
				{
					matrix[row, col] = (Symbol)matrixHex[row, col];
				}

			List<IReadOnlyList<Symbol>> daemonSequences = new List<IReadOnlyList<Symbol>>(daemonSequencesHex.Count);
			foreach (IReadOnlyList<int> sequenceHex in daemonSequencesHex)
			{
				daemonSequences.Add(sequenceHex.Select(n => (Symbol)n).ToArray());
			}

			return new Problem(matrix, daemonSequences, bufferLength);
		}
	}
}
