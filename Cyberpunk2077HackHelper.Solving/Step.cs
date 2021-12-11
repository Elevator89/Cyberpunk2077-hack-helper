using System.Drawing;

namespace Cyberpunk2077HackHelper.Solving
{
	public class Step
	{
		public readonly Point Cell;
		public readonly int Index;
		public readonly Step PrevStep;

		public Step(Point point, int index, Step prevPoint)
		{
			Cell = point;
			Index = index;
			PrevStep = prevPoint;
		}
	}
}
