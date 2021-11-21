using Cyberpunk2077HackHelper.Grabbing;
using System.Collections.Generic;

namespace Cyberpunk2077HackHelper.LayoutMarker
{
	public interface IFileService
	{
		Layout LoadLayout(string filename);
		void SaveLayout(string filename, Layout layout);

		List<SymbolMap> LoadSymbolMaps(string filename);
		void SaveSymbolMaps(string filename, List<SymbolMap> symbolMaps);
	}
}
