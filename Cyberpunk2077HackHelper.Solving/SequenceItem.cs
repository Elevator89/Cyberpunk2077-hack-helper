using System.Drawing;

namespace Cyberpunk2077HackHelper.Solving
{
	public class SequenceItem
	{
		public readonly Point Cell;
		public readonly int Index;
		public readonly SequenceItem PrevPoint;

		public SequenceItem(Point point, int index, SequenceItem prevPoint)
		{
			Cell = point;
			Index = index;
			PrevPoint = prevPoint;
		}
	}
}
