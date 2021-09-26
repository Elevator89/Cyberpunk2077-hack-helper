﻿using System.Collections.Generic;
using System.Drawing;

namespace Cyberpunk2077_hack_helper.Grabbing
{
	public class LayoutTable
	{
		public Point Position { get; set; }
		public Size CellSize { get; set; }
		public Size CellCount { get; set; }
		public List<SymbolMap> SymbolMaps { get; set; }
	}
}
