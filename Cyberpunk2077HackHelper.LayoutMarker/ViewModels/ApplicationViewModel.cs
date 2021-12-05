using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Cyberpunk2077HackHelper.Grabbing;

namespace Cyberpunk2077HackHelper.LayoutMarker.ViewModels
{
	public class ApplicationViewModel
	{
		private const string DefaultLayoutPath = "Data/Layouts/Matrix7.json";
		private const string DefaultMatrixSymbolMapsPath = "Data/SymbolMaps/matrixSymbolMaps.json";
		private const string DefaultSequenceSymbolMapsPath = "Data/SymbolMaps/sequenceSymbolMaps.json";

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
			LoadSymbolMaps(MatrixSymbolMaps, DefaultMatrixSymbolMapsPath);
			LoadSymbolMaps(SequenceSymbolMaps, DefaultSequenceSymbolMapsPath);
		}

		public void New()
		{
			LoadLayout(Layout, DefaultLayoutPath);
		}

		public void LoadLayout()
		{
			if (_dialogService.OpenFileDialog())
				LoadLayout(Layout, _dialogService.FilePath);
		}

		public void SaveLayout()
		{
			if (_dialogService.SaveFileDialog())
				SaveLayout(Layout, _dialogService.FilePath);
		}

		public void LoadMatrixSymbolMaps()
		{
			if (_dialogService.OpenFileDialog())
				LoadSymbolMaps(MatrixSymbolMaps, _dialogService.FilePath);
		}

		public void LoadSequenceSymbolMaps()
		{
			if (_dialogService.OpenFileDialog())
				LoadSymbolMaps(SequenceSymbolMaps, _dialogService.FilePath);
		}

		public void SaveMatrixSymbolMaps()
		{
			if (_dialogService.SaveFileDialog())
				SaveSymbolMaps(MatrixSymbolMaps, _dialogService.FilePath);
		}

		public void SaveSequenceSymbolMaps()
		{
			if (_dialogService.SaveFileDialog())
				SaveSymbolMaps(SequenceSymbolMaps, _dialogService.FilePath);
		}

		private void SaveLayout(LayoutViewModel layoutViewModel, string fileName)
		{
			try
			{
				Layout layout = GetLayoutFromViewModel(layoutViewModel);
				_fileService.SaveLayout(fileName, layout);
			}
			catch (Exception ex)
			{
				_dialogService.ShowMessage(ex.Message);
			}
		}

		private void LoadLayout(LayoutViewModel layoutViewModel, string fileName)
		{
			try
			{
				Layout layout = _fileService.LoadLayout(fileName);
				FillViewModelFromLayout(layout, layoutViewModel);
			}
			catch (Exception ex)
			{
				_dialogService.ShowMessage(ex.Message);
			}
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
	}
}
