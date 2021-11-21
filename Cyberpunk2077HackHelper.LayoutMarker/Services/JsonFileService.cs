using Cyberpunk2077HackHelper.Grabbing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Cyberpunk2077HackHelper.LayoutMarker
{
	public class JsonFileService : IFileService
	{
		public Layout LoadLayout(string filename)
		{
			string contents = File.ReadAllText(filename);
			return JsonConvert.DeserializeObject<Layout>(contents);
		}

		public List<SymbolMap> LoadSymbolMaps(string filename)
		{
			string contents = File.ReadAllText(filename);
			return JsonConvert.DeserializeObject<List<SymbolMap>>(contents);
		}

		public void SaveLayout(string filename, Layout layout)
		{
			string contents = JsonConvert.SerializeObject(layout, Formatting.Indented);
			File.WriteAllText(filename, contents);
		}

		public void SaveSymbolMaps(string filename, List<SymbolMap> symbolMaps)
		{
			string contents = JsonConvert.SerializeObject(symbolMaps, Formatting.Indented);
			File.WriteAllText(filename, contents);
		}
	}
}
