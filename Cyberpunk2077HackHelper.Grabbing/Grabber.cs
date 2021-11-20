using System;
using System.Collections.Generic;
using System.Drawing;
using Cyberpunk2077HackHelper.Common;

namespace Cyberpunk2077HackHelper.Grabbing
{
	public class Grabber
	{
		public Problem Grab(Bitmap bitmap, Layout layout)
		{
			return new Problem(GrabMatrix(bitmap, layout.Matrix), GrabSequences(bitmap, layout.Sequences), -1);
		}

		private IReadOnlyList<IReadOnlyList<Symbol>> GrabSequences(Bitmap bitmap, LayoutTable sequencesTable)
		{
			List<IReadOnlyList<Symbol>> result = new List<IReadOnlyList<Symbol>>();

			for (int row = 0; row < sequencesTable.CellCount.Height; ++row)
			{
				int baseY = sequencesTable.Position.Y + row * sequencesTable.CellSize.Height;
				int baseX = sequencesTable.Position.X;

				Symbol symbol0;
				if (!TryGrabSymbol(bitmap, new Point(baseX, baseY), sequencesTable.SymbolMaps, out symbol0))
					break;

				List<Symbol> rowSymbols = new List<Symbol>() { symbol0 };
				for (int col = 1; col < sequencesTable.CellCount.Width; ++col)
				{
					baseX += sequencesTable.CellSize.Width;

					Symbol symbol;
					if (TryGrabSymbol(bitmap, new Point(baseX, baseY), sequencesTable.SymbolMaps, out symbol))
						rowSymbols.Add(symbol);
					else
						break;
				}
				result.Add(rowSymbols);
			}
			return result;
		}

		private Symbol[,] GrabMatrix(Bitmap bitmap, LayoutTable matrixTable)
		{
			Symbol[,] result = new Symbol[matrixTable.CellCount.Width, matrixTable.CellCount.Height];

			for (int row = 0; row < matrixTable.CellCount.Height; ++row)
				for (int col = 0; col < matrixTable.CellCount.Width; ++col)
				{
					Point basePoint = new Point(
						matrixTable.Position.X + col * matrixTable.CellSize.Width,
						matrixTable.Position.Y + row * matrixTable.CellSize.Height);

					Symbol symbol;
					if (TryGrabSymbol(bitmap, basePoint, matrixTable.SymbolMaps, out symbol))
						result[row, col] = symbol;
					else
						throw new Exception($"Unable to parse symbol of row={row}, col={col}, basePoint={basePoint}");
				}
			return result;
		}

		private bool TryGrabSymbol(Bitmap bitmap, Point basePoint, List<SymbolMap> symbolMaps, out Symbol symbol)
		{
			foreach (SymbolMap symbolMap in symbolMaps)
			{
				if (CorrespondsSymbol(bitmap, basePoint, symbolMap))
				{
					symbol = symbolMap.Symbol;
					return true;
				}
			}
			symbol = default(Symbol);
			return false;
		}

		private bool CorrespondsSymbol(Bitmap bitmap, Point basePoint, SymbolMap symbolMap)
		{
			const float MinBrightness = 0.3f;

			foreach (Point point in symbolMap.Points)
			{
				Color color = bitmap.GetPixel(basePoint.X + point.X, basePoint.Y + point.Y);
				if (color.GetBrightness() < MinBrightness)
					return false;
			}
			return true;
		}
	}
}
