using System.Collections.Generic;

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
	}
}
