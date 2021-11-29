using System.Drawing;

namespace Cyberpunk2077HackHelper.Solving
{
	public class MatrixSequencePoint
	{
		public readonly Point Point;
		public readonly int Index;
		public readonly MatrixSequencePoint PrevPoint;

		public MatrixSequencePoint(Point point, int index, MatrixSequencePoint prevPoint)
		{
			Point = point;
			Index = index;
			PrevPoint = prevPoint;
		}
	}
}
