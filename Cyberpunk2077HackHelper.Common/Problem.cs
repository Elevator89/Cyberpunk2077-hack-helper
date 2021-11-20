namespace Cyberpunk2077HackHelper.Common
{
	public class Problem
	{
		public readonly Symbol[,] Matrix;
		public readonly Symbol[][] DaemonSequences;
		public readonly int BufferLength;

		public Problem(Symbol[,] matrix, Symbol[][] daemonSequences, int bufferLength)
		{
			Matrix = matrix;
			DaemonSequences = daemonSequences;
			BufferLength = bufferLength;
		}
	}
}
