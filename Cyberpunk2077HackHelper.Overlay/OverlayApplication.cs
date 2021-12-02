using GameOverlay.Drawing;
using GameOverlay.Windows;
using LowLevelInput.Hooks;
using System;
using System.Text;

namespace Cyberpunk2077HackHelper.Overlay
{
	public class OverlayApplication : IDisposable
	{
		readonly InputManager _inputManager = new InputManager();

		private readonly StickyWindow _window;
		private bool _disposedValue;

		private SolidBrush _clearBrush;
		private SolidBrush _backgroundBrush;
		private SolidBrush _foregroundBrush;
		private Font _font;

		public OverlayApplication()
		{
			_inputManager.OnKeyboardEvent += InputManager_OnKeyboardEvent;
			_inputManager.Initialize();

			Graphics gfx = new Graphics()
			{
				MeasureFPS = true,
				PerPrimitiveAntiAliasing = true,
				TextAntiAliasing = true
			};

			//IntPtr windowHandle = Utils.WinGetHandle("Noita");
			IntPtr windowHandle = Utils.WinGetHandle("Cyberpunk 2077 (C) 2020");

			_window = new StickyWindow(windowHandle)
			{
				FPS = 30,
				IsTopmost = true,
				IsVisible = true
			};

			_window.DestroyGraphics += WindowDestroyGraphics;
			_window.DrawGraphics += WindowDrawGraphics;
			_window.SetupGraphics += WindowSetupGraphics;
		}

		public void Run()
		{
			_window.Create();
			_window.Hide();
			_window.Join();
		}

		private void InputManager_OnKeyboardEvent(VirtualKeyCode key, KeyState state)
		{
			if (state == KeyState.Up)
			{
				switch (key)
				{
					case VirtualKeyCode.Add:
						_window.Show();
						break;
					case VirtualKeyCode.Subtract:
						_window.Hide();
						break;
					case VirtualKeyCode.Decimal:
						Dispose();
						break;
				}
			}
		}

		private void WindowSetupGraphics(object sender, SetupGraphicsEventArgs e)
		{
			Graphics gfx = e.Graphics;

			if (e.RecreateResources)
			{
				_clearBrush.Dispose();
				_backgroundBrush.Dispose();
				_foregroundBrush.Dispose();
			}

			_clearBrush = gfx.CreateSolidBrush(0x33, 0x36, 0x3F, 0);
			_backgroundBrush = gfx.CreateSolidBrush(0x30, 0x30, 0x30);
			_foregroundBrush = gfx.CreateSolidBrush(0xFF, 0xFF, 0x00);
			_font = gfx.CreateFont("Arial", 28);

			if (e.RecreateResources)
				return;
		}

		private void WindowDrawGraphics(object sender, DrawGraphicsEventArgs e)
		{
			Graphics gfx = e.Graphics;

			int padding = 16;
			string infoText = new StringBuilder()
				.Append("FPS: ").Append(gfx.FPS.ToString().PadRight(padding))
				.Append("FrameTime: ").Append(e.FrameTime.ToString().PadRight(padding))
				.Append("FrameCount: ").Append(e.FrameCount.ToString().PadRight(padding))
				.Append("DeltaTime: ").Append(e.DeltaTime.ToString().PadRight(padding))
				.ToString();

			gfx.ClearScene(_clearBrush);
			gfx.DrawTextWithBackground(_font, _foregroundBrush, _backgroundBrush, 58, 30, infoText);
		}

		private void WindowDestroyGraphics(object sender, DestroyGraphicsEventArgs e)
		{
			_font.Dispose();
			_clearBrush.Dispose();
			_backgroundBrush.Dispose();
			_foregroundBrush.Dispose();
		}

		~OverlayApplication()
		{
			Dispose(false);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				_window.Dispose();
				_inputManager.Dispose();
				_disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
