using Cyberpunk2077_hack_helper.Grabbing;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class LayoutTableViewModel : INotifyPropertyChanged
	{
		private LayoutTable _model;

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

		public ObservableCollection<SymbolMapViewModel> SymbolMaps { get; }

		public LayoutTableViewModel(LayoutTable layoutTable)
		{
			_model = layoutTable;
			SymbolMaps = new ObservableCollection<SymbolMapViewModel>(_model.SymbolMaps.Select(m => new SymbolMapViewModel(m)));
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
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
