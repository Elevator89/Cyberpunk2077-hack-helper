using Cyberpunk2077HackHelper.Grabbing;
using LowLevelInput.Hooks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Cyberpunk2077HackHelper.Overlay
{
	class Program
	{
		private static InputManager _inputManager;
		private static OverlayApplication _overlayApp;

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

			try
			{
				_inputManager = new InputManager();
				_inputManager.OnKeyboardEvent += InputManager_OnKeyboardEvent;
				_inputManager.Initialize();

				_overlayApp = new OverlayApplication();

				_overlayApp.Run();
			}
			finally
			{
				_overlayApp.Dispose();
				_inputManager.Dispose();
			}
		}

		private static void InputManager_OnKeyboardEvent(VirtualKeyCode key, KeyState state)
		{
			if (state == KeyState.Up)
			{
				switch (key)
				{
					case VirtualKeyCode.Add:
						_overlayApp.Show();
						break;
					case VirtualKeyCode.Subtract:
						_overlayApp.Hide();
						break;
					case VirtualKeyCode.Decimal:
						_overlayApp.Dispose();
						break;
				}
			}
		}
	}
}
