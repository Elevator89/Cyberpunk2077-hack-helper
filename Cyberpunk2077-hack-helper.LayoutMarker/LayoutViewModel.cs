using Cyberpunk2077_hack_helper.LayoutMarker.Tools;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public delegate void PointEditEventHandler(PointViewModel point);
	public delegate void LayoutTablePositionEditEventHandler(LayoutTableViewModel layoutTableViewModel);
	public delegate void LayoutTableCellSizeEditEventHandler(LayoutTableViewModel layoutTableViewModel);

	public class LayoutViewModel : INotifyPropertyChanged
	{
		private LayoutTableViewModel _matrixViewModel;
		private LayoutTableViewModel _sequencesViewModel;

		private int _selectedTableIndex = 1;

		public event PointEditEventHandler PointEdit;
		public event LayoutTablePositionEditEventHandler LayoutTablePositionEdit;
		public event LayoutTableCellSizeEditEventHandler LayoutTableCellSizeEdit;
		public event PropertyChangedEventHandler PropertyChanged;

		private readonly IToolManager _toolManager;

		public LayoutTableViewModel Matrix
		{
			get { return _matrixViewModel; }
		}

		public LayoutTableViewModel Sequences
		{
			get { return _sequencesViewModel; }
		}

		public int SelectedTableIndex
		{
			get { return _selectedTableIndex; }
			set
			{
				if (_selectedTableIndex == value)
					return;
				_selectedTableIndex = value;
				OnPropertyChanged();
			}
		}

		public LayoutViewModel(IToolManager toolManager)
		{
			_toolManager = toolManager;
			_matrixViewModel = new LayoutTableViewModel(this, _toolManager);
			_sequencesViewModel = new LayoutTableViewModel(this, _toolManager);
		}

		public void NotifyPointEdit(PointViewModel point)
		{
			PointEdit?.Invoke(point);
		}

		public void NotifyLayoutTablePositionEdit(LayoutTableViewModel layoutTableViewModel)
		{
			LayoutTablePositionEdit?.Invoke(layoutTableViewModel);
		}

		public void NotifyLayoutTableCellSizeEdit(LayoutTableViewModel layoutTableViewModel)
		{
			LayoutTableCellSizeEdit?.Invoke(layoutTableViewModel);
		}

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
