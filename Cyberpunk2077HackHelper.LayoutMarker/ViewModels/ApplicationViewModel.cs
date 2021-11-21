using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Cyberpunk2077HackHelper.Common;
using Cyberpunk2077HackHelper.Grabbing;

namespace Cyberpunk2077HackHelper.LayoutMarker.ViewModels
{
	public class ApplicationViewModel
	{
		private const string DefaultMatrixSymbolMapsFileName = "matrixSymbolMaps.json";
		private const string DefaultSequenceSymbolMapsFileName = "sequenceSymbolMaps.json";

		private readonly IDialogService _dialogService;
		private readonly IFileService _fileService;

		public LayoutViewModel Layout { get; }
		public SymbolMapsViewModel MatrixSymbolMaps { get; }
		public SymbolMapsViewModel SequenceSymbolMaps { get; }

		public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
		{
			_dialogService = dialogService;
			_fileService = fileService;

			Layout = new LayoutViewModel();
			MatrixSymbolMaps = new SymbolMapsViewModel();
			SequenceSymbolMaps = new SymbolMapsViewModel();

			New();
			LoadSymbolMaps(MatrixSymbolMaps, DefaultMatrixSymbolMapsFileName);
			LoadSymbolMaps(SequenceSymbolMaps, DefaultSequenceSymbolMapsFileName);
		}

		public void New()
		{
			FillViewModelFromLayout(CreateDefaultLayout(), Layout);
		}

		public void LoadLayout()
		{
			try
			{
				if (_dialogService.OpenFileDialog())
				{
					Layout layout = _fileService.LoadLayout(_dialogService.FilePath);
					FillViewModelFromLayout(layout, Layout);
				}
			}
			catch (Exception ex)
			{
				_dialogService.ShowMessage(ex.Message);
			}
		}

		public void SaveLayout()
		{
			try
			{
				if (_dialogService.SaveFileDialog())
				{
					Layout layout = GetLayoutFromViewModel(Layout);
					_fileService.SaveLayout(_dialogService.FilePath, layout);
				}
			}
			catch (Exception ex)
			{
				_dialogService.ShowMessage(ex.Message);
			}
		}

		public void LoadMatrixSymbolMaps()
		{
			LoadSymbolMaps(MatrixSymbolMaps);
		}

		public void LoadSequenceSymbolMaps()
		{
			LoadSymbolMaps(SequenceSymbolMaps);
		}

		public void SaveMatrixSymbolMaps()
		{
			SaveSymbolMaps(MatrixSymbolMaps);
		}

		public void SaveSequenceSymbolMaps()
		{
			SaveSymbolMaps(SequenceSymbolMaps);
		}

		private void SaveSymbolMaps(SymbolMapsViewModel symbolMapsViewModel)
		{
			if (_dialogService.SaveFileDialog())
				SaveSymbolMaps(symbolMapsViewModel, _dialogService.FilePath);
		}

		private void LoadSymbolMaps(SymbolMapsViewModel symbolMapsViewModel)
		{
			if (_dialogService.OpenFileDialog())
				LoadSymbolMaps(symbolMapsViewModel, _dialogService.FilePath);
		}

		private void SaveSymbolMaps(SymbolMapsViewModel symbolMapsViewModel, string fileName)
		{
			try
			{
				List<SymbolMap> symbolMaps = GetSymbolMapsFromViewModel(symbolMapsViewModel);
				_fileService.SaveSymbolMaps(fileName, symbolMaps);
			}
			catch (Exception ex)
			{
				_dialogService.ShowMessage(ex.Message);
			}
		}

		private void LoadSymbolMaps(SymbolMapsViewModel symbolMapsViewModel, string fileName)
		{
			try
			{
				List<SymbolMap> symbolMaps = _fileService.LoadSymbolMaps(fileName);
				FillViewModelFromSymbolMaps(symbolMaps, symbolMapsViewModel);
			}
			catch (Exception ex)
			{
				_dialogService.ShowMessage(ex.Message);
			}
		}

		private static List<SymbolMap> GetSymbolMapsFromViewModel(SymbolMapsViewModel symbolMapsViewModel)
		{
			return symbolMapsViewModel.SymbolMaps.Select(GetSymbolMapFromViewModel).ToList();
		}

		private static Layout GetLayoutFromViewModel(LayoutViewModel layoutViewModel)
		{
			return new Layout(GetLayoutTableFromViewModel(layoutViewModel.Matrix), GetLayoutTableFromViewModel(layoutViewModel.Sequences));
		}

		private static LayoutTable GetLayoutTableFromViewModel(LayoutTableViewModel layoutTableViewModel)
		{
			return new LayoutTable()
			{
				Position = layoutTableViewModel.Position,
				CellSize = layoutTableViewModel.CellSize,
				CellCount = layoutTableViewModel.CellCount,
			};
		}

		private static SymbolMap GetSymbolMapFromViewModel(SymbolMapViewModel symbolMapViewModel)
		{
			return new SymbolMap(symbolMapViewModel.Symbol, symbolMapViewModel.Points.Select(pointVm => pointVm.Point).ToList());
		}

		private static void FillViewModelFromSymbolMaps(IReadOnlyList<SymbolMap> symbolMaps, SymbolMapsViewModel symbolMapsViewModel)
		{
			symbolMapsViewModel.SelectedSymbolMapIndex = -1;

			symbolMapsViewModel.SymbolMaps.Clear();
			foreach (SymbolMap symbolMap in symbolMaps)
			{
				symbolMapsViewModel.SymbolMaps.Add(CreateSymbolMapViewModel(symbolMap));
			}
		}

		private static void FillViewModelFromLayout(Layout layout, LayoutViewModel layoutViewModel)
		{
			FillLayoutTableViewModel(layout.Matrix, layoutViewModel.Matrix);
			FillLayoutTableViewModel(layout.Sequences, layoutViewModel.Sequences);
			layoutViewModel.SelectedTableIndex = -1;
		}

		private static void FillLayoutTableViewModel(LayoutTable layoutTable, LayoutTableViewModel layoutTableViewModel)
		{
			layoutTableViewModel.Position = layoutTable.Position;
			layoutTableViewModel.CellSize = layoutTable.CellSize;
			layoutTableViewModel.CellCount = layoutTable.CellCount;
		}

		private static SymbolMapViewModel CreateSymbolMapViewModel(SymbolMap symbolMap)
		{
			SymbolMapViewModel symbolMapViewModel = new SymbolMapViewModel { Symbol = symbolMap.Symbol };

			foreach (Point point in symbolMap.Points)
				symbolMapViewModel.Points.Add(new PointViewModel(point));

			return symbolMapViewModel;
		}

		private static Layout CreateDefaultLayout()
		{
			return new Layout(
				matrix: new LayoutTable()
				{
					Position = new Point(250, 362),
					CellSize = new Size(64, 64),
					CellCount = new Size(7, 7),
				},
				sequences: new LayoutTable()
				{
					Position = new Point(892, 346),
					CellSize = new Size(42, 68),
					CellCount = new Size(6, 6),
				});
		}
	}
}
