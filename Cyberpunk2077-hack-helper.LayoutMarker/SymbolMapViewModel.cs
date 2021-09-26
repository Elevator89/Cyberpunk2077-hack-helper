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
					Points.Add(point);
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
					  Point point = new Point(0, 0);
					  Points.Add(point);
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
					  Points.RemoveAt(SelectedPointIndex);
				  }));
			}
		}

		public ObservableCollection<Point> Points { get; }

		public SymbolMapViewModel(SymbolMap model)
		{
			_model = model;
			Points = new ObservableCollection<Point>(_model.Points);
			Points.CollectionChanged += HandleCollectionChanged;
		}

		private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					_model.Points.InsertRange(e.NewStartingIndex, e.NewItems.Cast<Point>());
					return;
				case NotifyCollectionChangedAction.Remove:
					_model.Points.RemoveRange(e.OldStartingIndex, e.OldItems.Count);
					return;
				case NotifyCollectionChangedAction.Replace:
					_model.Points.RemoveRange(e.OldStartingIndex, e.OldItems.Count);
					_model.Points.InsertRange(e.NewStartingIndex, e.NewItems.Cast<Point>());
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
