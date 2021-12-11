using Cyberpunk2077HackHelper.Common;
using Cyberpunk2077HackHelper.Grabbing;
using GameOverlay.Drawing;
using GameOverlay.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyberpunk2077HackHelper.Overlay
{
	public class Overlay : IDisposable
	{
		private static readonly Point _precisionDisplacement = new Point(0.5f, 0.5f);

		private readonly GraphicsWindow _window;
		private bool _disposedValue;

		private SolidBrush _clearBrush;
		private SolidBrush _darkBrush;
		private SolidBrush _backgroundBrush;
		private SolidBrush _brightBrush;
		private Font _fontSmall;
		private Font _fontBig;

		private Geometry _gridGeometry;
		private Geometry _solutionGeometry;

		private Layout _layout = null;
		private Problem _problem = null;
		private IReadOnlyList<Symbol>[] _combinations = null;
		private System.Drawing.Point[] _solution = null;

		public System.Drawing.Size Size
		{
			get { return new System.Drawing.Size(_window.Width, _window.Height); }
		}

		public bool IsActive { get { return _window.IsVisible; } }

		public Overlay()
		{
			Graphics gfx = new Graphics()
			{
				MeasureFPS = true,
				PerPrimitiveAntiAliasing = true,
				TextAntiAliasing = true,
			};

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

		public void Show(Layout layout, Problem problem, IReadOnlyList<IReadOnlyList<Symbol>> combinations, IEnumerable<System.Drawing.Point> solution)
		{
			_layout = layout;
			_problem = problem;
			_combinations = combinations.OrderBy(c => c.Count).ToArray();
			_solution = solution?.ToArray();

			if (_gridGeometry != null)
			{
				_gridGeometry.Dispose();
				_gridGeometry = null;
			}

			if (_solutionGeometry != null)
			{
				_solutionGeometry.Dispose();
				_solutionGeometry = null;
			}

			Graphics gfx = _window.Graphics;

			if (_layout != null)
			{
				_gridGeometry = gfx.CreateGeometry();
				AddLayoutTableGeometry(_gridGeometry, _layout.Matrix);
				AddLayoutTableGeometry(_gridGeometry, _layout.Sequences);
				_gridGeometry.Close();
			}

			if (_solution != null)
			{
				_solutionGeometry = gfx.CreateGeometry();
				Point cellMiddle = new Point(0.75f * _layout.Matrix.CellSize.Width, 0.25f * _layout.Matrix.CellSize.Height);
				AddSolutionGeometry(_solutionGeometry, _layout.Matrix, _solution, cellMiddle, new Point(40, 40));
				_solutionGeometry.Close();
			}

			_window.Show();
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
				_darkBrush.Dispose();
				_brightBrush.Dispose();
				_backgroundBrush.Dispose();
			}

			_clearBrush = gfx.CreateSolidBrush(0x00, 0x00, 0x00, 0);
			_darkBrush = gfx.CreateSolidBrush(0x00, 0xFF, 0xFF, 0x40);
			_brightBrush = gfx.CreateSolidBrush(0xFF, 0xFF, 0xFF);
			_backgroundBrush = gfx.CreateSolidBrush(0x00, 0x00, 0x00, 0x40);
			_fontSmall = gfx.CreateFont("Arial", 12);
			_fontBig = gfx.CreateFont("Arial", 24);

			if (e.RecreateResources)
				return;
		}

		private void WindowDrawGraphics(object sender, DrawGraphicsEventArgs e)
		{
			Graphics gfx = e.Graphics;

			gfx.ClearScene(_clearBrush);

			StringBuilder sb = new StringBuilder();

			if (_layout != null && _problem != null)
			{
				gfx.DrawGeometry(_gridGeometry, _darkBrush, 1.0f);

				DrawMatrixTable(_layout.Matrix, _problem.Matrix, gfx, _fontSmall, _darkBrush);
				DrawSequencesTable(_layout.Sequences, _problem.DaemonSequences, gfx, _fontSmall, _darkBrush);
				DrawCombinations(_layout.Sequences, _combinations, gfx, _fontSmall, _brightBrush, _backgroundBrush);
			}
			else
			{
				sb.AppendLine("Unable to grab a problem");
			}

			if (_solution != null)
			{
				gfx.DrawGeometry(_solutionGeometry, _brightBrush, 1.0f);
			}
			else
			{
				sb.AppendLine("Unable to find a solution");
			}

			gfx.DrawTextWithBackground(_fontBig, _brightBrush, _backgroundBrush, 250, 880, sb.ToString());
		}


		private void WindowDestroyGraphics(object sender, DestroyGraphicsEventArgs e)
		{
			_fontSmall.Dispose();
			_fontBig.Dispose();
			_clearBrush.Dispose();
			_darkBrush.Dispose();
			_brightBrush.Dispose();
		}

		~Overlay()
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

		private static void AddLayoutTableGeometry(Geometry geometry, LayoutTable layoutTable)
		{
			float startX = layoutTable.Position.X;
			float startY = layoutTable.Position.Y;

			for (int row = 0; row <= layoutTable.CellCount.Height; ++row)
			{
				float lineY = startY + row * layoutTable.CellSize.Height;

				Line line = new Line(
					Precise(new Point(startX, lineY)),
					Precise(new Point(startX + layoutTable.CellCount.Width * layoutTable.CellSize.Width, lineY)));

				geometry.BeginFigure(line);
				geometry.EndFigure(false);
			}

			for (int col = 0; col <= layoutTable.CellCount.Width; ++col)
			{
				float lineX = startX + col * layoutTable.CellSize.Width;

				Line line = new Line(
					Precise(new Point(lineX, startY)),
					Precise(new Point(lineX, startY + layoutTable.CellCount.Height * layoutTable.CellSize.Height)));

				geometry.BeginFigure(line);
				geometry.EndFigure(false);
			}
		}

		private static void AddSolutionGeometry(Geometry geometry, LayoutTable layoutTable, IReadOnlyList<System.Drawing.Point> solution, Point cellMiddle, Point cellSize)
		{
			int solutionHalfLength = solution.Count / 2;
			Point startPoint = PointOps.Add(TransformPoint(solution[0]), cellMiddle);
			AddRect(geometry, Precise(startPoint), cellSize.X, cellSize.Y);

			for (int pointIndex = 1; pointIndex < solution.Count; ++pointIndex)
			{
				int coordDisplacement = pointIndex - solutionHalfLength;
				Point displacement = new Point(pointIndex % 2 * coordDisplacement, (pointIndex + 1) % 2 * coordDisplacement);

				Point endPoint = PointOps.Add(TransformPoint(solution[pointIndex]), cellMiddle);
				AddRect(geometry, Precise(endPoint), cellSize.X, cellSize.Y);
				AddArrow(geometry, Precise(PointOps.Add(startPoint, displacement)), Precise(PointOps.Add(endPoint, displacement)), 0.5f * cellSize.X);
				startPoint = endPoint;
			}

			Point TransformPoint(System.Drawing.Point point)
			{
				return new Point(
					layoutTable.Position.X + point.X * layoutTable.CellSize.Width,
					layoutTable.Position.Y + point.Y * layoutTable.CellSize.Height);
			}
		}

		private static void AddRect(Geometry geometry, Point center, float width, float height)
		{
			geometry.AddRectangle(new Rectangle(center.X - 0.5f * width, center.Y - 0.5f * height, center.X + 0.5f * width, center.Y + 0.5f * height));
		}

		private static void AddArrow(Geometry geometry, Point start, Point end, float cut)
		{
			const float ArrowLength = 10.0f;
			const float ArrowWidth = 5.0f;

			Point vector = PointOps.Subtract(end, start);
			Point normalized = vector.Normalized();
			Point lineStart = PointOps.Add(start, normalized.Multiply(cut));
			Point lineEnd = PointOps.Subtract(end, normalized.Multiply(cut));
			Point arrowStart = PointOps.Subtract(lineEnd, normalized.Multiply(ArrowLength));
			Point arrowLeftSide = normalized.RotatedLeft().Multiply(ArrowWidth);

			geometry.BeginFigure(new Line(lineStart, lineEnd));
			geometry.EndFigure(false);
			geometry.BeginFigure(new Line(PointOps.Add(arrowStart, arrowLeftSide), lineEnd));
			geometry.EndFigure(false);
			geometry.BeginFigure(new Line(PointOps.Subtract(arrowStart, arrowLeftSide), lineEnd));
			geometry.EndFigure(false);
		}

		private static void DrawMatrixTable(LayoutTable table, Symbol[,] matrix, Graphics gfx, Font font, SolidBrush brush)
		{
			float startX = table.Position.X;
			float startY = table.Position.Y;

			for (int row = 0; row < table.CellCount.Height; ++row)
			{
				for (int col = 0; col < table.CellCount.Width; ++col)
				{
					Point point = new Point(
						startX + col * table.CellSize.Width,
						startY + row * table.CellSize.Height);

					gfx.DrawText(font, brush, Precise(point), SymbolToString(matrix[row, col]));
				}
			}
		}

		private static void DrawSequencesTable(LayoutTable table, IReadOnlyList<IReadOnlyList<Symbol>> sequences, Graphics gfx, Font font, SolidBrush brush)
		{
			float startX = table.Position.X;
			float startY = table.Position.Y;

			for (int sequenceIndex = 0; sequenceIndex < sequences.Count; ++sequenceIndex)
			{
				IReadOnlyList<Symbol> sequence = sequences[sequenceIndex];
				for (int symbolIndex = 0; symbolIndex < sequence.Count; ++symbolIndex)
				{
					Point point = new Point(
						startX + symbolIndex * table.CellSize.Width,
						startY + sequenceIndex * table.CellSize.Height);

					gfx.DrawText(font, brush, Precise(point), SymbolToString(sequence[symbolIndex]));
				}
			}
		}

		private static void DrawCombinations(LayoutTable table, IReadOnlyList<IReadOnlyList<Symbol>> combinations, Graphics gfx, Font font, SolidBrush brush, SolidBrush backgroundBrush)
		{
			// Below the table
			float startX = table.Position.X;
			float startY = table.Position.Y + table.CellCount.Height * table.CellSize.Height;

			float stepX = 0.5f * table.CellSize.Width;
			float stepY = 0.3f * table.CellSize.Height;

			for (int combinationIndex = 0; combinationIndex < combinations.Count; ++combinationIndex)
			{
				IReadOnlyList<Symbol> combination = combinations[combinationIndex];
				for (int symbolIndex = 0; symbolIndex < combination.Count; ++symbolIndex)
				{
					Point point = new Point(
						startX + symbolIndex * stepX,
						startY + combinationIndex * stepY);

					gfx.DrawTextWithBackground(font, brush, backgroundBrush, Precise(point), SymbolToString(combination[symbolIndex]));
				}
			}
		}

		private static string SymbolToString(Symbol symbol)
		{
			switch (symbol)
			{
				case Symbol._1C:
					return "1C";
				case Symbol._55:
					return "55";
				case Symbol._7A:
					return "7A";
				case Symbol._BD:
					return "BD";
				case Symbol._E9:
					return "E9";
				case Symbol._FF:
					return "FF";
				case Symbol.Unknown:
				default:
					return "**";
			}
		}

		private static Point Precise(Point p)
		{
			return PointOps.Add(p.Round(), _precisionDisplacement);
		}
	}
}
