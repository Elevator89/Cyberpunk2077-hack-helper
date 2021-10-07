using Cyberpunk2077_hack_helper.Grabbing;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class LayoutTableViewModel : INotifyPropertyChanged
	{
		private LayoutTable _model;

		private RelayCommand _addSymbolMapCommand;
		private RelayCommand _removeSymbolMapCommand;

		private RelayCommand _debugCommand;

		private int _selectedSymbolMapIndex;

		public LayoutTable Model
		{
			get { return _model; }
			set
			{
				_model = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(Position));
				OnPropertyChanged(nameof(CellSize));

				SymbolMaps.CollectionChanged -= HandleCollectionChanged;
				SymbolMaps.Clear();
				foreach (SymbolMap symbolMap in _model.SymbolMaps)
					SymbolMaps.Add(new SymbolMapViewModel(symbolMap));
				SymbolMaps.CollectionChanged += HandleCollectionChanged;
			}
		}

		public Point Position
		{
			get { return _model.Position; }
			set
			{
				if (_model.Position == value)
					return;

				_model.Position = value;
				OnPropertyChanged();
			}
		}

		public Size CellSize
		{
			get { return _model.CellSize; }
			set
			{
				if (_model.CellSize == value)
					return;

				_model.CellSize = value;
				OnPropertyChanged();
			}
		}

		public Size CellCount
		{
			get { return _model.CellCount; }
			set
			{
				if (_model.CellCount == value)
					return;

				_model.CellCount = value;
				OnPropertyChanged();
			}
		}

		public int SelectedSymbolMapIndex
		{
			get { return _selectedSymbolMapIndex; }
			set
			{
				_selectedSymbolMapIndex = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(SelectedSymbolMapPoints));
			}
		}

		public TrulyObservableCollection<PointViewModel> SelectedSymbolMapPoints
		{
			get
			{
				if (SelectedSymbolMapIndex < 0 || SelectedSymbolMapIndex >= Model.SymbolMaps.Count)
					return null;

				return SymbolMaps[SelectedSymbolMapIndex].Points;
			}
		}

		public RelayCommand AddSymbolMapCommand
		{
			get
			{
				return _addSymbolMapCommand ??
				  (_addSymbolMapCommand = new RelayCommand(obj =>
				  {
					  SymbolMap newMap = new SymbolMap(Common.Symbol._1C, new List<Point>());
					  SymbolMapViewModel newMapVm = new SymbolMapViewModel(newMap);
					  SymbolMaps.Add(newMapVm);
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

		public RelayCommand DebugCommand
		{
			get
			{
				return _debugCommand ??
				  (_debugCommand = new RelayCommand(obj =>
				  {
					  SymbolMapViewModel selectedSymbolMap = SymbolMaps[SelectedSymbolMapIndex];
					  if (selectedSymbolMap.SelectedPointIndex >= 0)
						  selectedSymbolMap.Points[selectedSymbolMap.SelectedPointIndex].X++;
				  }));
			}
		}

		public TrulyObservableCollection<SymbolMapViewModel> SymbolMaps { get; }

		public LayoutTableViewModel(LayoutTable layoutTable)
		{
			_model = layoutTable;
			SymbolMaps = new TrulyObservableCollection<SymbolMapViewModel>(_model.SymbolMaps.Select(m => new SymbolMapViewModel(m)));
			SymbolMaps.CollectionChanged += HandleCollectionChanged;
		}

		private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					_model.SymbolMaps.InsertRange(e.NewStartingIndex, e.NewItems.Cast<SymbolMapViewModel>().Select(vm => vm.Model));
					return;
				case NotifyCollectionChangedAction.Remove:
					_model.SymbolMaps.RemoveRange(e.OldStartingIndex, e.OldItems.Count);
					return;
				case NotifyCollectionChangedAction.Replace:
					_model.SymbolMaps.RemoveRange(e.OldStartingIndex, e.OldItems.Count);
					_model.SymbolMaps.InsertRange(e.NewStartingIndex, e.NewItems.Cast<SymbolMapViewModel>().Select(vm => vm.Model));
					return;
				case NotifyCollectionChangedAction.Reset:
					_model.SymbolMaps.Clear();
					return;
			}
		}

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
