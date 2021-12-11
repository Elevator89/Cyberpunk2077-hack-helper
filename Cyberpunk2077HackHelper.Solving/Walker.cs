using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Cyberpunk2077HackHelper.Common;

namespace Cyberpunk2077HackHelper.Solving
{
	public class Walker
	{
		public IEnumerable<IEnumerable<Point>> Walk(Symbol[,] matrix, IReadOnlyList<Symbol> combination)
		{
			List<Step> foundPath = new List<Step>(20); // The actual size is usually less than 20

			Queue<Step> queue = new Queue<Step>();

			foreach (Step initialStep in GetInitialSteps())
				queue.Enqueue(initialStep);

			while (queue.Count > 0)
			{
				Step currentStep = queue.Dequeue();

				foundPath.Add(currentStep);

				foreach (Step nextStep in GetNextItems())
					queue.Enqueue(nextStep);

				IEnumerable<Step> GetNextItems()
				{
					int nextStepIndex = currentStep.Index + 1;
					if (nextStepIndex >= combination.Count)
						yield break;

					MatrixLineDirection nextStepLineDirection = GetSymbolDirection(nextStepIndex);
					Symbol nextCombinationSymbol = combination[nextStepIndex];

					foreach (Point nextCell in matrix.GetOtherCellsInLine(nextStepLineDirection, currentStep.Cell).Where(nextCell => !currentStep.ContainsCell(nextCell)))
					{
						if (nextCombinationSymbol == Symbol.Unknown || matrix.Get(nextCell) == nextCombinationSymbol)
							yield return new Step(nextCell, nextStepIndex, currentStep);
					}
				}
			}

			foreach (Step step in foundPath)
			{
				if (step.Index == combination.Count - 1)
					yield return step.Unwind();
			}

			IEnumerable<Step> GetInitialSteps()
			{
				MatrixLineDirection initialStepLineDirection = GetSymbolDirection(0);
				Symbol initialCombinationSymbol = combination[0];

				foreach (Point initialCell in matrix.GetOtherCellsInLine(initialStepLineDirection, new Point(-1, 0)))
				{
					if (initialCombinationSymbol == Symbol.Unknown || matrix.Get(initialCell) == initialCombinationSymbol)
						yield return new Step(initialCell, 0, null);
				}
			}
		}

		private static MatrixLineDirection GetSymbolDirection(int symbolIndex)
		{
			// 0-th symbol should be found in row, 1-th - in column, etc.
			return symbolIndex % 2 == 0 ? MatrixLineDirection.Row : MatrixLineDirection.Column;
		}
	}
}
