using Cyberpunk2077_hack_helper.Common;
using Cyberpunk2077_hack_helper.Grabbing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

			_model = new Layout(
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

			_layoutViewModel = new LayoutViewModel(_model);
		}

		//// команда сохранения файла
		//private RelayCommand saveCommand;
		//public RelayCommand SaveCommand
		//{
		//	get
		//	{
		//		return saveCommand ??
		//		  (saveCommand = new RelayCommand(obj =>
		//		  {
		//			  try
		//			  {
		//				  if (dialogService.SaveFileDialog() == true)
		//				  {
		//					  fileService.Save(dialogService.FilePath, Phones.ToList());
		//					  dialogService.ShowMessage("Файл сохранен");
		//				  }
		//			  }
		//			  catch (Exception ex)
		//			  {
		//				  dialogService.ShowMessage(ex.Message);
		//			  }
		//		  }));
		//	}
		//}

		//// команда открытия файла
		//private RelayCommand openCommand;
		//public RelayCommand OpenCommand
		//{
		//	get
		//	{
		//		return openCommand ??
		//		  (openCommand = new RelayCommand(obj =>
		//		  {
		//			  try
		//			  {
		//				  if (dialogService.OpenFileDialog() == true)
		//				  {
		//					  var phones = fileService.Open(dialogService.FilePath);
		//					  Phones.Clear();
		//					  foreach (var p in phones)
		//						  Phones.Add(p);
		//					  dialogService.ShowMessage("Файл открыт");
		//				  }
		//			  }
		//			  catch (Exception ex)
		//			  {
		//				  dialogService.ShowMessage(ex.Message);
		//			  }
		//		  }));
		//	}
		//}

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
