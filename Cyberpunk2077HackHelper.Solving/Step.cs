using System.Collections.Generic;
using System.Linq;
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

		public IEnumerable<Point> GetPoints()
		{
			return GetPoints(this);
		}

		public IEnumerable<Point> Unwind()
		{
			return Unwind(this);
		}

		public bool ContainsCell(Point cell)
		{
			return ContainsCell(this, cell);
		}

		public static IEnumerable<Point> Unwind(Step step)
		{
			return GetPoints(step).Reverse();
		}

		public bool ContainsCell(Step step, Point cell)
		{
			foreach (var stepCell in GetPoints(step))
				if (stepCell == cell)
					return true;

			return false;
		}

		public static IEnumerable<Point> GetPoints(Step step)
		{
			while (step != null)
			{
				yield return step.Cell;
				step = step.PrevStep;
			}
		}
	}
}
