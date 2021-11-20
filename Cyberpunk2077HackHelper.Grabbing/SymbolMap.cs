using Cyberpunk2077HackHelper.Common;
using System.Collections.Generic;
using System.Drawing;

namespace Cyberpunk2077HackHelper.Grabbing
{
	public class SymbolMap
	{
		public Symbol Symbol { get; set; }
		public List<Point> Points { get; set; }

		public SymbolMap(Symbol symbol, List<Point> points)
		{
			Symbol = symbol;
			Points = points;
		}
	}
}
