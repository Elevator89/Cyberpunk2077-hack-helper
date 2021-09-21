namespace Cyberpunk2077_hack_helper.Common
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
