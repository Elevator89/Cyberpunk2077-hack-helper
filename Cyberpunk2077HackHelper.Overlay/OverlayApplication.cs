using Cyberpunk2077HackHelper.Common;
using Cyberpunk2077HackHelper.Grabbing;
using GameOverlay.Drawing;
using GameOverlay.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cyberpunk2077HackHelper.Overlay
{
	public class OverlayApplication : IDisposable
	{
		private readonly GraphicsWindow _window;
		private bool _disposedValue;

		private SolidBrush _clearBrush;
		private SolidBrush _backgroundBrush;
		private SolidBrush _foregroundBrush;
		private Font _font;

		private Geometry _matrixTableGeometry;
		private Geometry _sequencesTableGeometry;

		private Layout _layout = null;
		private Problem _problem = null;
		private IReadOnlyList<System.Drawing.Point> _solution = null;

		public System.Drawing.Size Size
		{
			get { return new System.Drawing.Size(_window.Width, _window.Height); }
		}

		public OverlayApplication()
		{
			Graphics gfx = new Graphics()
			{
				MeasureFPS = true,
				PerPrimitiveAntiAliasing = true,
				TextAntiAliasing = true,
			};

			IntPtr windowHandle = Utils.WinGetHandle("Noita");
			//IntPtr windowHandle = Utils.WinGetHandle("Cyberpunk 2077 (C) 2020");

			//_window = new StickyWindow(windowHandle, gfx)
			_window = new GraphicsWindow(gfx)
			{
				FPS = 30,
				IsTopmost = true,
				IsVisible = true,
				Width = 1920,
				Height = 1080,
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

		public void Show()
		{
			_window.Show();
		}

		public void Show(Layout layout, Problem problem, IReadOnlyList<System.Drawing.Point> solution)
		{
			_layout = layout;
			_problem = problem;
			_solution = solution;

			if (_matrixTableGeometry != null)
			{
				_matrixTableGeometry.Dispose();
				_matrixTableGeometry = null;
			}

			if (_sequencesTableGeometry != null)
			{
				_sequencesTableGeometry.Dispose();
				_sequencesTableGeometry = null;
			}

			if (_layout != null)
			{
				Graphics gfx = _window.Graphics;
				_matrixTableGeometry = CreateLayoutTableGeometry(gfx, _layout.Matrix);
				_sequencesTableGeometry = CreateLayoutTableGeometry(gfx, _layout.Sequences);
			}

			_window.Show();
		}

		private Geometry CreateLayoutTableGeometry(Graphics gfx, LayoutTable layoutTable)
		{
			Geometry geometry = gfx.CreateGeometry();

			for (int row = 0; row <= layoutTable.CellCount.Height; ++row)
			{
				Line line = new Line(
					layoutTable.Position.X,
					layoutTable.Position.Y + row * layoutTable.CellSize.Height,
					layoutTable.Position.X + layoutTable.CellCount.Width * layoutTable.CellSize.Width,
					layoutTable.Position.Y + row * layoutTable.CellSize.Height);

				geometry.BeginFigure(line);
				geometry.EndFigure(false);
			}

			for (int col = 0; col <= layoutTable.CellCount.Width; ++col)
			{
				Line line = new Line(
					layoutTable.Position.X + col * layoutTable.CellSize.Width,
					layoutTable.Position.Y,
					layoutTable.Position.X + col * layoutTable.CellSize.Width,
					layoutTable.Position.Y + layoutTable.CellCount.Height * layoutTable.CellSize.Height);

				geometry.BeginFigure(line);
				geometry.EndFigure(false);
			}
			geometry.Close();
			return geometry;
		}

		public void Hide()
		{
			_window.Hide();
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

			if (_matrixTableGeometry != null)
				gfx.DrawGeometry(_matrixTableGeometry, _foregroundBrush, 1.0f);

			if (_sequencesTableGeometry != null)
				gfx.DrawGeometry(_sequencesTableGeometry, _foregroundBrush, 1.0f);

			if (_problem != null)
			{
				string matrixText = GetMatrixText(_problem.Matrix);
				gfx.DrawTextWithBackground(_font, _foregroundBrush, _backgroundBrush, 58, 60, matrixText);

				string sequencesText = GetSequencesText(_problem.DaemonSequences);
				gfx.DrawTextWithBackground(_font, _foregroundBrush, _backgroundBrush, 658, 60, sequencesText);
			}
		}

		private string GetMatrixText(Symbol[,] matrix)
		{
			StringBuilder matrixSb = new StringBuilder();
			for (int row = 0; row < matrix.GetLength(0); ++row)
			{
				for (int col = 0; col < matrix.GetLength(1); ++col)
					matrixSb.Append(matrix[row, col] + "  ");

				matrixSb.AppendLine();
			}

			return matrixSb.ToString();
		}

		private string GetSequencesText(IReadOnlyList<IReadOnlyList<Symbol>> sequences)
		{
			StringBuilder sequencesSb = new StringBuilder();

			foreach (IReadOnlyList<Symbol> sequence in sequences)
			{
				foreach (Symbol symbol in sequence)
					sequencesSb.Append(symbol + "  ");

				sequencesSb.AppendLine();
			}

			return sequencesSb.ToString();
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
