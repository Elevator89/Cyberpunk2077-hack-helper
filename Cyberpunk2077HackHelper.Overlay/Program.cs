using Cyberpunk2077HackHelper.Grabbing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Cyberpunk2077HackHelper.Overlay
{
	class Program
	{
		static void Main(string[] args)
		{
			GameOverlay.TimerService.EnableHighPrecisionTimers();

			string matrixSymbolMapsContents = File.ReadAllText("SymbolMaps/matrixSymbolMaps.json");
			string sequenceSymbolMapsContents = File.ReadAllText("SymbolMaps/sequenceSymbolMaps.json");

			List<SymbolMap> matrixSymbolMaps = JsonConvert.DeserializeObject<List<SymbolMap>>(matrixSymbolMapsContents);
			List<SymbolMap> sequenceSymbolMaps = JsonConvert.DeserializeObject<List<SymbolMap>>(sequenceSymbolMapsContents);

			List<Layout> layouts = new List<Layout>();
			foreach (string layoutFileName in Directory.GetFiles("Layouts/", "Matrix*.json"))
			{
				string layoutContents = File.ReadAllText(layoutFileName);
				layouts.Add(JsonConvert.DeserializeObject<Layout>(layoutContents));
			}

			using (OverlayApplication overlayApp = new OverlayApplication())
			{
				overlayApp.Run();
			}
		}
	}
}
