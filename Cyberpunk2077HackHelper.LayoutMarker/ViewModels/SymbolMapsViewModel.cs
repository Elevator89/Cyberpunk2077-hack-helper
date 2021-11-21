using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace Cyberpunk2077HackHelper.LayoutMarker.ViewModels
{
	public class SymbolMapsViewModel : INotifyPropertyChanged
	{
		private RelayCommand _addSymbolMapCommand;
		private RelayCommand _removeSymbolMapCommand;

		private int _selectedSymbolMapIndex;

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<SymbolMapViewModel> SymbolMaps { get; }

		public int SelectedSymbolMapIndex
		{
			get { return _selectedSymbolMapIndex; }
			set
			{
				_selectedSymbolMapIndex = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(SelectedSymbolMap));
				OnPropertyChanged(nameof(SelectedSymbolMapPoints));
			}
		}

		public SymbolMapViewModel SelectedSymbolMap
		{
			get
			{
				if (SelectedSymbolMapIndex < 0 || SelectedSymbolMapIndex >= SymbolMaps.Count)
					return null;

				return SymbolMaps[SelectedSymbolMapIndex];
			}
		}

		public ObservableCollection<PointViewModel> SelectedSymbolMapPoints
		{
			get { return SelectedSymbolMap?.Points; }
		}

		public RelayCommand AddSymbolMapCommand
		{
			get
			{
				return _addSymbolMapCommand ??
				  (_addSymbolMapCommand = new RelayCommand(obj =>
				  {
					  SymbolMaps.Add(new SymbolMapViewModel());
				  }));
			}
		}

		public RelayCommand RemoveSymbolMapCommand
		{
			get
			{
				return _removeSymbolMapCommand ??
				  (_removeSymbolMapCommand = new RelayCommand(obj =>
				  {
					  SymbolMaps.RemoveAt(SelectedSymbolMapIndex);
				  }));
			}
		}

		public SymbolMapsViewModel()
		{
			SymbolMaps = new ObservableCollection<SymbolMapViewModel>();
			_selectedSymbolMapIndex = -1;
		}

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
