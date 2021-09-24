using Cyberpunk2077_hack_helper.Common;
using System.Collections.Generic;
using System.Drawing;

namespace Cyberpunk2077_hack_helper.Grabbing
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
