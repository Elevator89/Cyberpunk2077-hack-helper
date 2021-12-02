using LowLevelInput.Converters;
using LowLevelInput.Hooks;
using System;
using System.Threading.Tasks;

namespace Cyberpunk2077HackHelper.Overlay
{
	class Program
	{
		private static OverlayApplication _overlayApp;

		static void Main(string[] args)
		{
			GameOverlay.TimerService.EnableHighPrecisionTimers();

			InputManager inputManager = new InputManager();
			inputManager.OnKeyboardEvent += InputManager_OnKeyboardEvent;
			inputManager.Initialize();

			Console.WriteLine("Waiting for Decimal key to exit!");

			// This method will block the current thread until the up arrow key changes it's state to Down
			// There is no performance penalty (spinning loop waiting for this)
			inputManager.WaitForEvent(VirtualKeyCode.Decimal, KeyState.Down);

			inputManager.Dispose();
		}

		private static void InputManager_OnKeyboardEvent(VirtualKeyCode key, KeyState state)
		{
			if (key == VirtualKeyCode.Add && state == KeyState.Up)
			{
				if (_overlayApp == null)
				{
					_overlayApp = new OverlayApplication();
					_overlayApp.Run();
				}
			}
			else if (key == VirtualKeyCode.Subtract && state == KeyState.Up)
			{
				if (_overlayApp != null)
				{
					_overlayApp.Dispose();
					_overlayApp = null;
				}
			}
		}
	}
}
