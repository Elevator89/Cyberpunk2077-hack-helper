using Cyberpunk2077_hack_helper.Common;
using Cyberpunk2077_hack_helper.Grabbing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class ApplicationViewModel : INotifyPropertyChanged
	{
		private IDialogService _dialogService;
		private IFileService _fileService;

		private Layout _model;

		private LayoutViewModel _layoutViewModel;

		public LayoutViewModel Layout
		{
			get { return _layoutViewModel; }
		}

		public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
		{
			_dialogService = dialogService;
			_fileService = fileService;

			_model = CreateDefaultLayout();
			_layoutViewModel = new LayoutViewModel(_model);
		}

		public void New()
		{
			_model = CreateDefaultLayout();
			_layoutViewModel.Model = _model;
		}

		public void Open()
		{
			try
			{
				if (_dialogService.OpenFileDialog() == true)
				{
					_model = _fileService.Open(_dialogService.FilePath);
					Layout.Model = _model;
					_dialogService.ShowMessage("Файл открыт");
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
					_fileService.Save(_dialogService.FilePath, _model);
					_dialogService.ShowMessage("Файл сохранён");
				}
			}
			catch (Exception ex)
			{
				_dialogService.ShowMessage(ex.Message);
			}
		}

		private Layout CreateDefaultLayout()
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

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
