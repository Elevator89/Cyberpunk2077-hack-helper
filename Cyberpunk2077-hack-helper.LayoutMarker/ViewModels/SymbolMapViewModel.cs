using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;
using Cyberpunk2077_hack_helper.Common;
using Cyberpunk2077_hack_helper.LayoutMarker.Tools;
using System.Collections.ObjectModel;

namespace Cyberpunk2077_hack_helper.LayoutMarker.ViewModels
{
	public class SymbolMapViewModel : INotifyPropertyChanged
	{
		private readonly LayoutTableViewModel _layoutTableViewModel;
		private readonly IToolManager _toolManager;

		private RelayCommand _addPointCommand;
		private RelayCommand _removePointCommand;

		private Symbol _symbol;

		private int _selectedPointIndex = -1;

		public event PropertyChangedEventHandler PropertyChanged;

		public Symbol Symbol
		{
			get { return _symbol; }
			set
			{
				if (_symbol == value)
					return;

				_symbol = value;
				OnPropertyChanged();
			}
		}

		public int SelectedPointIndex
		{
			get { return _selectedPointIndex; }
			set
			{
				if (_selectedPointIndex == value)
					return;

				_selectedPointIndex = value;
				OnPropertyChanged();
			}
		}

		public RelayCommand AddPointCommand
		{
			get
			{
				return _addPointCommand ??
				  (_addPointCommand = new RelayCommand(obj =>
				  {
					  Points.Add(new PointViewModel(_layoutTableViewModel, _toolManager, new Point(0, 0)));
				  }));
			}
		}

		public RelayCommand RemovePointCommand
		{
			get
			{
				return _removePointCommand ??
				  (_removePointCommand = new RelayCommand(obj =>
				  {
					  if (SelectedPointIndex < 0)
						  return;

					  Points.RemoveAt(SelectedPointIndex);
				  }));
			}
		}

		public ObservableCollection<PointViewModel> Points { get; }

		public SymbolMapViewModel(LayoutTableViewModel layoutTableViewModel, IToolManager toolManager)
		{
			_layoutTableViewModel = layoutTableViewModel;
			_toolManager = toolManager;
			_symbol = Symbol._1C;
			Points = new ObservableCollection<PointViewModel>();
		}

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
