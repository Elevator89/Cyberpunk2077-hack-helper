using Cyberpunk2077_hack_helper.LayoutMarker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	/// <summary>
	/// Interaction logic for LayoutTableControl.xaml
	/// </summary>
	public partial class LayoutTableControl : UserControl
	{
		public delegate void PointChangedEventHandler(object sender, PointChangedEventArgs e);
		public delegate void SizeChangedEventHandler(object sender, SizeChangedEventArgs e);

		public class PointChangedEventArgs : RoutedEventArgs
		{
			private System.Drawing.Point _point;

			public System.Drawing.Point Point { get { return _point; } }

			public PointChangedEventArgs(RoutedEvent id, System.Drawing.Point point)
			{
				_point = point;
				RoutedEvent = id;
			}
		}

		public class SizeChangedEventArgs : RoutedEventArgs
		{
			private System.Drawing.Size _size;

			public System.Drawing.Size Size { get { return _size; } }

			public SizeChangedEventArgs(RoutedEvent id, System.Drawing.Size size)
			{
				_size = size;
				RoutedEvent = id;
			}
		}

		public static readonly DependencyProperty TablePositionProperty =
			DependencyProperty.Register(
				nameof(TablePosition),
				typeof(System.Drawing.Point),
				typeof(LayoutTableControl),
				new FrameworkPropertyMetadata(
					new System.Drawing.Point(0, 0),
					new PropertyChangedCallback(OnTablePositionChanged))
				{
					BindsTwoWayByDefault = true
				});

		public static readonly DependencyProperty TableCellSizeProperty =
			DependencyProperty.Register(
				nameof(TableCellSize),
				typeof(System.Drawing.Size),
				typeof(LayoutTableControl),
				new FrameworkPropertyMetadata(
					new System.Drawing.Size(0, 0),
					new PropertyChangedCallback(OnTableCellSizeChanged))
				{
					BindsTwoWayByDefault = true
				});

		public static readonly DependencyProperty TableCellCountProperty =
			DependencyProperty.Register(
				nameof(TableCellCount),
				typeof(System.Drawing.Size),
				typeof(LayoutTableControl),
				new FrameworkPropertyMetadata(
					new System.Drawing.Size(0, 0),
					new PropertyChangedCallback(OnTableCellCountChanged))
				{
					BindsTwoWayByDefault = true
				});

		public static readonly RoutedEvent TablePositionChangedEvent =
			EventManager.RegisterRoutedEvent(
				nameof(TablePositionChanged),
				RoutingStrategy.Direct,
				typeof(PointChangedEventHandler),
				typeof(LayoutTableControl));

		public static readonly RoutedEvent TableCellSizeChangedEvent =
			EventManager.RegisterRoutedEvent(
				nameof(TableCellSizeChanged),
				RoutingStrategy.Direct,
				typeof(SizeChangedEventHandler),
				typeof(LayoutTableControl));

		public static readonly RoutedEvent TableCellCountChangedEvent =
			EventManager.RegisterRoutedEvent(
				nameof(TableCellCountChanged),
				RoutingStrategy.Direct,
				typeof(SizeChangedEventHandler),
				typeof(LayoutTableControl));

		public System.Drawing.Point TablePosition
		{
			get { return (System.Drawing.Point)GetValue(TablePositionProperty); }
			set { SetValue(TablePositionProperty, value); }
		}

		public System.Drawing.Size TableCellSize
		{
			get { return (System.Drawing.Size)GetValue(TableCellSizeProperty); }
			set { SetValue(TableCellSizeProperty, value); }
		}

		public System.Drawing.Size TableCellCount
		{
			get { return (System.Drawing.Size)GetValue(TableCellCountProperty); }
			set { SetValue(TableCellCountProperty, value); }
		}

		public event PointChangedEventHandler TablePositionChanged
		{
			add { AddHandler(TablePositionChangedEvent, value); }
			remove { RemoveHandler(TablePositionChangedEvent, value); }
		}

		public event SizeChangedEventHandler TableCellSizeChanged
		{
			add { AddHandler(TableCellSizeChangedEvent, value); }
			remove { RemoveHandler(TableCellSizeChangedEvent, value); }
		}

		public event SizeChangedEventHandler TableCellCountChanged
		{
			add { AddHandler(TableCellCountChangedEvent, value); }
			remove { RemoveHandler(TableCellCountChangedEvent, value); }
		}

		public LayoutTableControl()
		{
			InitializeComponent();
		}

		private static void OnTablePositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LayoutTableControl thisObj = (LayoutTableControl)d;
			System.Drawing.Point newPosition = (System.Drawing.Point)e.NewValue;

			thisObj.RaiseEvent(new PointChangedEventArgs(TablePositionChangedEvent, newPosition));
		}

		private static void OnTableCellSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LayoutTableControl thisObj = (LayoutTableControl)d;
			System.Drawing.Size newCellSize = (System.Drawing.Size)e.NewValue;

			thisObj.RaiseEvent(new SizeChangedEventArgs(TableCellSizeChangedEvent, newCellSize));
		}

		private static void OnTableCellCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LayoutTableControl thisObj = (LayoutTableControl)d;
			System.Drawing.Size newCellCount = (System.Drawing.Size)e.NewValue;

			thisObj.RaiseEvent(new SizeChangedEventArgs(TableCellCountChangedEvent, newCellCount));
		}
	}
}
