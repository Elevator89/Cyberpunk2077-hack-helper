using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public sealed class TrulyObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
	{
		public TrulyObservableCollection()
		{
			CollectionChanged += FullObservableCollectionCollectionChanged;
		}

		public TrulyObservableCollection(IEnumerable<T> pItems) : this()
		{
			foreach (var item in pItems)
			{
				this.Add(item);
			}
		}

		private void FullObservableCollectionCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems != null)
			{
				foreach (object item in e.NewItems)
				{
					((INotifyPropertyChanged)item).PropertyChanged += ItemPropertyChanged;
				}
			}
			if (e.OldItems != null)
			{
				foreach (object item in e.OldItems)
				{
					((INotifyPropertyChanged)item).PropertyChanged -= ItemPropertyChanged;
				}
			}
		}

		private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			int index = IndexOf((T)sender);
			NotifyCollectionChangedEventArgs removedArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, sender, index);
			NotifyCollectionChangedEventArgs addedArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, sender, index);
			OnCollectionChanged(removedArgs);
			OnCollectionChanged(addedArgs);

			// The code below doesn't work for ListView
			// NotifyCollectionChangedEventArgs replacedArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, sender, sender, IndexOf((T)sender));
			// OnCollectionChanged(replacedArgs);
		}
	}
}
