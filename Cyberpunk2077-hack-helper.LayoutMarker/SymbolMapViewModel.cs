using Cyberpunk2077_hack_helper.Grabbing;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Collections.ObjectModel;
using Cyberpunk2077_hack_helper.Common;
using System.Collections.Specialized;
using System.Linq;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class SymbolMapViewModel : INotifyPropertyChanged
	{
		private SymbolMap _model;

		private RelayCommand _addPointCommand;
		private RelayCommand _removePointCommand;

		private int _selectedPointIndex;

		public SymbolMap Model
		{
			get { return _model; }
			set
			{
				_model = value;

				OnPropertyChanged(nameof(Symbol));

				Points.CollectionChanged -= HandleCollectionChanged;
				Points.Clear();
				foreach (Point point in _model.Points)
					Points.Add(new PointViewModel(point));

				Points.CollectionChanged += HandleCollectionChanged;
			}
		}

		public Symbol Symbol
		{
			get { return _model.Symbol; }
			set
			{
				if (_model.Symbol == value)
					return;

				_model.Symbol = value;
				OnPropertyChanged();
			}
		}

		public int SelectedPointIndex
		{
			get { return _selectedPointIndex; }
			set { _selectedPointIndex = value; }
		}

		public RelayCommand AddPointCommand
		{
			get
			{
				return _addPointCommand ??
				  (_addPointCommand = new RelayCommand(obj =>
				  {
					  Points.Add(new PointViewModel(new Point(0, 0)));
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

		public TrulyObservableCollection<PointViewModel> Points { get; }

		public SymbolMapViewModel(SymbolMap model)
		{
			_model = model;

			Points = new TrulyObservableCollection<PointViewModel>(_model.Points.Select(p => new PointViewModel(p)));
			Points.CollectionChanged += HandleCollectionChanged;
		}

		private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					_model.Points.InsertRange(e.NewStartingIndex, e.NewItems.Cast<PointViewModel>().Select(pvm => pvm.Point));
					return;
				case NotifyCollectionChangedAction.Remove:
					_model.Points.RemoveRange(e.OldStartingIndex, e.OldItems.Count);
					return;
				case NotifyCollectionChangedAction.Replace:
					_model.Points.RemoveRange(e.OldStartingIndex, e.OldItems.Count);
					_model.Points.InsertRange(e.NewStartingIndex, e.NewItems.Cast<PointViewModel>().Select(pvm => pvm.Point));
					return;
				case NotifyCollectionChangedAction.Reset:
					_model.Points.Clear();
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
