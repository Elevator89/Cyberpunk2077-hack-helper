using System.Drawing;
using Cyberpunk2077HackHelper.Solving.Wave;

namespace Cyberpunk2077HackHelper.Solving
{
	public class MatrixCellProcessor : IWaveItemProcessor<MatrixSequencePoint, MatrixSequencePoint>
	{
		public bool TryProcessItem(MatrixSequencePoint point, out MatrixSequencePoint result)
		{
			result = point;
			return true;
		}
	}
}
