﻿using Cyberpunk2077HackHelper.Common;
using Cyberpunk2077HackHelper.Grabbing;
using Cyberpunk2077HackHelper.LayoutMarker.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Cyberpunk2077HackHelper.LayoutMarker.ViewModels
{
	public class ApplicationViewModel
	{
		private readonly IDialogService _dialogService;
		private readonly IFileService _fileService;
		private readonly IToolManager _toolManager;

		private readonly LayoutViewModel _layoutViewModel;

		public LayoutViewModel Layout
		{
			get { return _layoutViewModel; }
		}

		public ApplicationViewModel(IDialogService dialogService, IFileService fileService, IToolManager toolManager)
		{
			_dialogService = dialogService;
			_fileService = fileService;
			_toolManager = toolManager;

			_layoutViewModel = new LayoutViewModel(_toolManager);
			New();
		}

		public void New()
		{
			FillViewModelFromLayout(_toolManager, CreateDefaultLayout(), _layoutViewModel);
		}

		public void Open()
		{
			try
			{
				if (_dialogService.OpenFileDialog() == true)
				{
					Layout layout = _fileService.Open(_dialogService.FilePath);
					FillViewModelFromLayout(_toolManager, layout, _layoutViewModel);
				}
			}
			catch (Exception ex)
			{
				_dialogService.ShowMessage(ex.Message);
			}
		}

		public void Save()
		{
			try
			{
				if (_dialogService.SaveFileDialog() == true)
				{
					Layout layout = GetLayoutFromViewModel(_layoutViewModel);
					_fileService.Save(_dialogService.FilePath, layout);
				}
			}
			catch (Exception ex)
			{
				_dialogService.ShowMessage(ex.Message);
			}
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
				SymbolMaps = layoutTableViewModel.SymbolMaps.Select(GetSymbolMapFromViewModel).ToList()
			};
		}

		private static SymbolMap GetSymbolMapFromViewModel(SymbolMapViewModel symbolMapViewModel)
		{
			return new SymbolMap(symbolMapViewModel.Symbol, symbolMapViewModel.Points.Select(pointVm => pointVm.Point).ToList());
		}

		private static void FillViewModelFromLayout(IToolManager toolManager, Layout layout, LayoutViewModel layoutViewModel)
		{
			FillLayoutTableViewModel(toolManager, layout.Matrix, layoutViewModel.Matrix);
			FillLayoutTableViewModel(toolManager, layout.Sequences, layoutViewModel.Sequences);
			layoutViewModel.SelectedTableIndex = -1;
		}

		private static void FillLayoutTableViewModel(IToolManager toolManager, LayoutTable layoutTable, LayoutTableViewModel layoutTableViewModel)
		{
			layoutTableViewModel.Position = layoutTable.Position;
			layoutTableViewModel.CellSize = layoutTable.CellSize;
			layoutTableViewModel.CellCount = layoutTable.CellCount;
			layoutTableViewModel.SelectedSymbolMapIndex = -1;

			layoutTableViewModel.SymbolMaps.Clear();
			foreach (SymbolMap symbolMap in layoutTable.SymbolMaps)
			{
				layoutTableViewModel.SymbolMaps.Add(CreateSymbolMapViewModel(layoutTableViewModel, toolManager, symbolMap));
			}
		}

		private static SymbolMapViewModel CreateSymbolMapViewModel(LayoutTableViewModel layoutTableViewModel, IToolManager toolManager, SymbolMap symbolMap)
		{
			SymbolMapViewModel symbolMapViewModel = new SymbolMapViewModel(layoutTableViewModel, toolManager);
			symbolMapViewModel.Symbol = symbolMap.Symbol;

			foreach (Point point in symbolMap.Points)
				symbolMapViewModel.Points.Add(new PointViewModel(layoutTableViewModel, toolManager, point));

			return symbolMapViewModel;
		}

		private static Layout CreateDefaultLayout()
		{
			return new Layout(
				matrix: new LayoutTable()
				{
					Position = new Point(100, 100),
					CellSize = new Size(10, 10),
					CellCount = new Size(6, 6),
					SymbolMaps = new List<SymbolMap>() {
						new SymbolMap(Symbol._1C, new List<Point>(){
							new Point(1,1),
							new Point(2,2),
							new Point(3,3),
						}),
						new SymbolMap(Symbol._55, new List<Point>(){
							new Point(1,5),
							new Point(2,5),
							new Point(3,5),
						})
					}
				},
				sequences: new LayoutTable()
				{
					Position = new Point(400, 200),
					CellSize = new Size(20, 20),
					CellCount = new Size(8, 2),
					SymbolMaps = new List<SymbolMap>() {
						new SymbolMap(Symbol._7A, new List<Point>(){
							new Point(7,1),
							new Point(7,2),
							new Point(7,3),
						}),
						new SymbolMap(Symbol._BD, new List<Point>(){
							new Point(8,5),
							new Point(8,5),
							new Point(8,5),
						})
					}
				});
		}
	}
}