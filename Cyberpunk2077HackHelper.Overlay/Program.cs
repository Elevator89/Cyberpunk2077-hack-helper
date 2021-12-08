using Cyberpunk2077HackHelper.Common;
using Cyberpunk2077HackHelper.Grabbing;
using Cyberpunk2077HackHelper.Solving;
using LowLevelInput.Hooks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Cyberpunk2077HackHelper.Overlay
{
	class Program
	{
		private const string SymbolMapsPath = "Data/SymbolMaps/";
		private const string LayoutsPath = "Data/Layouts/";
		private const string ScreenshotsPath = "Data/Screenshots/";

		private static InputManager _inputManager;
		private static Overlay _overlay;

		private static List<Layout> _layouts = new List<Layout>();

		private static Grabber _grabber;
		private static Solver _solver = new Solver();

		static void Main(string[] args)
		{
			GameOverlay.TimerService.EnableHighPrecisionTimers();

			string matrixSymbolMapsContents = File.ReadAllText(Path.Combine(SymbolMapsPath, "matrixSymbolMaps.json"));
			string sequenceSymbolMapsContents = File.ReadAllText(Path.Combine(SymbolMapsPath, "sequenceSymbolMaps.json"));

			List<SymbolMap> matrixSymbolMaps = JsonConvert.DeserializeObject<List<SymbolMap>>(matrixSymbolMapsContents);
			List<SymbolMap> sequenceSymbolMaps = JsonConvert.DeserializeObject<List<SymbolMap>>(sequenceSymbolMapsContents);

			_grabber = new Grabber(matrixSymbolMaps, sequenceSymbolMaps);

			foreach (string layoutFileName in Directory.GetFiles(LayoutsPath, "Matrix*.json"))
			{
				string layoutContents = File.ReadAllText(layoutFileName);
				_layouts.Add(JsonConvert.DeserializeObject<Layout>(layoutContents));
			}
			_layouts.Sort((a, b) => b.Matrix.CellCount.Width - a.Matrix.CellCount.Width);

			try
			{
				_inputManager = new InputManager();
				_inputManager.OnKeyboardEvent += InputManager_OnKeyboardEvent;
				_inputManager.Initialize();

				_overlay = new Overlay();

				_overlay.Run();
			}
			finally
			{
				_overlay.Dispose();
				_inputManager.Dispose();
			}
		}

		private static void InputManager_OnKeyboardEvent(VirtualKeyCode key, KeyState state)
		{
			if (state == KeyState.Up)
			{
				switch (key)
				{
					case VirtualKeyCode.F6:
						if (_overlay.IsActive)
							_overlay.Hide();
						else
						{
							TryGrabAndSolve(out Layout grabbedLayout, out Problem problem, out IReadOnlyList<Point> solution);
							_overlay.Show(grabbedLayout, problem, solution);
						}
						break;
					case VirtualKeyCode.F7:
						using (Bitmap screenshot = MakeScreenshot())
							SaveScreenshot(screenshot);
						break;
					case VirtualKeyCode.F8:
						_overlay.Dispose();
						break;
				}
			}
		}

		private static bool TryGrabAndSolve(out Layout grabbedLayout, out Problem problem, out IReadOnlyList<Point> solution)
		{
			using (Bitmap screenshot = MakeScreenshot())
			{
				if (TryGrabProblem(screenshot, out grabbedLayout, out problem))
				{
					solution = _solver.Solve(problem).FirstOrDefault();
					return solution != null;
				}
				else
				{
					solution = null;
					return false;
				}
			}
		}

		private static bool TryGrabProblem(Bitmap screenshot, out Layout grabbedLayout, out Problem problem)
		{
			foreach (Layout layout in _layouts)
			{
				if (_grabber.TryGrab(screenshot, layout, out problem))
				{
					grabbedLayout = layout;
					return true;
				}
			}
			grabbedLayout = null;
			problem = null;
			return false;
		}

		private static Bitmap MakeScreenshot()
		{
			Size size = _overlay.Size;
			Bitmap bmpScreenCapture = new Bitmap(size.Width, size.Height);

			using (Graphics g = Graphics.FromImage(bmpScreenCapture))
			{
				g.CopyFromScreen(0, 0, 0, 0, bmpScreenCapture.Size, CopyPixelOperation.SourceCopy);
			}
			return bmpScreenCapture;
		}

		private static void SaveScreenshot(Bitmap screenshot)
		{
			screenshot.Save(Path.Combine(ScreenshotsPath, $"Screen_{DateTimeToFileNameString(DateTime.Now)}.png"), System.Drawing.Imaging.ImageFormat.Png);
		}

		private static string DateTimeToFileNameString(DateTime dateTime)
		{
			return $"{dateTime.Year}{dateTime.Month}{dateTime.Day}_{dateTime.Hour}{dateTime.Minute}{dateTime.Second}";
		}
	}
}
