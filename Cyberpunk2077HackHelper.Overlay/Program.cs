using LowLevelInput.Converters;
using LowLevelInput.Hooks;
using System;
using System.Threading.Tasks;

namespace Cyberpunk2077HackHelper.Overlay
{
	class Program
	{
		static void Main(string[] args)
		{
			GameOverlay.TimerService.EnableHighPrecisionTimers();

			//var inputManager = new InputManager();
			//inputManager.Initialize();
			//inputManager.RegisterEvent(VirtualKeyCode.K, InputManager_KeyStateChanged);

			LowLevelKeyboardHook keyboardHook = new LowLevelKeyboardHook();
			keyboardHook.OnKeyboardEvent += KeyboardHook_OnKeyboardEvent;
			keyboardHook.InstallHook();

			using (OverlayApplication overlayApp = new OverlayApplication())
			{
				overlayApp.Run();
			}

			keyboardHook.Dispose();
			//inputManager.Dispose();
		}

		private static void InputManager_OnKeyboardEvent(VirtualKeyCode key, KeyState state)
		{
			Console.WriteLine("InputManager_OnKeyboardEvent: " + KeyCodeConverter.ToString(key) + " - " + KeyStateConverter.ToString(state));
		}

		private static void KeyboardHook_OnKeyboardEvent(VirtualKeyCode key, KeyState state)
		{
			Console.WriteLine("KeyboardHook_OnKeyboardEvent: " + KeyCodeConverter.ToString(key) + " - " + KeyStateConverter.ToString(state));
		}

		private static void InputManager_KeyStateChanged(VirtualKeyCode key, KeyState state)
		{
			// you may use the same callback for every key or define a new one for each
			Console.WriteLine("The key state of " + KeyCodeConverter.ToString(key) + " changed to " + KeyStateConverter.ToString(state));
		}
	}
}
