using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using Cyberpunk2077_hack_helper.LayoutMarker.ViewModels;
using System.Windows.Input;

namespace Cyberpunk2077_hack_helper.LayoutMarker.Views
{
	public class SymbolMapView : FrameworkElement
	{
		public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(
			"Position",
			typeof(System.Drawing.Point),
			typeof(SymbolMapView),
			new FrameworkPropertyMetadata(
				new System.Drawing.Point(0, 0),
				FrameworkPropertyMetadataOptions.AffectsMeasure |
				FrameworkPropertyMetadataOptions.AffectsRender,
				new PropertyChangedCallback(OnPositionChanged)));

		public static readonly DependencyProperty CellSizeProperty = DependencyProperty.Register(
			"CellSize",
			typeof(System.Drawing.Size),
			typeof(SymbolMapView),
			new FrameworkPropertyMetadata(
				new System.Drawing.Size(0, 0),
				FrameworkPropertyMetadataOptions.AffectsMeasure |
				FrameworkPropertyMetadataOptions.AffectsRender,
				new PropertyChangedCallback(OnCellSizeChanged)));

		public static readonly DependencyProperty CellCountProperty = DependencyProperty.Register(
			"CellCount",
			typeof(System.Drawing.Size),
			typeof(SymbolMapView),
			new FrameworkPropertyMetadata(
				new System.Drawing.Size(0, 0),
				FrameworkPropertyMetadataOptions.AffectsMeasure |
				FrameworkPropertyMetadataOptions.AffectsRender,
				new PropertyChangedCallback(OnCellCountChanged)));

		public static readonly DependencyProperty PointsProperty = DependencyProperty.Register(
			"Points",
			typeof(ObservableCollection<PointViewModel>),
			typeof(SymbolMapView),
			new FrameworkPropertyMetadata(
				null,
				FrameworkPropertyMetadataOptions.AffectsMeasure |
				FrameworkPropertyMetadataOptions.AffectsRender,
				new PropertyChangedCallback(OnPointsChanged)));

		public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(
			"Brush",
			typeof(Brush),
			typeof(SymbolMapView),
			new FrameworkPropertyMetadata(
				Brushes.White,
				FrameworkPropertyMetadataOptions.AffectsRender,
				new PropertyChangedCallback(OnBrushChanged)));

		private System.Drawing.Point _position;
		private System.Drawing.Size _cellSize;
		private System.Drawing.Size _cellCount;

		private INotifyCollectionChanged _pointsNotifyCollectionChangedInternal;

		private Brush _brush;

		// Create a collection of child visual objects.
		private readonly VisualCollection _visuals;

		private Drag<int> _drag = null;

		public System.Drawing.Point Position
		{
			get { return (System.Drawing.Point)GetValue(PositionProperty); }
			set { SetValue(PositionProperty, value); }
		}

		public System.Drawing.Size CellSize
		{
			get { return (System.Drawing.Size)GetValue(CellSizeProperty); }
			set { SetValue(CellSizeProperty, value); }
		}

		public System.Drawing.Size CellCount
		{
			get { return (System.Drawing.Size)GetValue(CellCountProperty); }
			set { SetValue(CellCountProperty, value); }
		}

		public IEnumerable<PointViewModel> Points
		{
			get { return (IEnumerable<PointViewModel>)GetValue(PointsProperty); }
			set { SetValue(PointsProperty, value); }
		}

		public Brush Brush
		{
			get { return (Brush)GetValue(BrushProperty); }
			set { SetValue(BrushProperty, value); }
		}

		protected override int VisualChildrenCount => _visuals.Count;

		public SymbolMapView()
		{
			_brush = Brushes.Red;
			_position = new System.Drawing.Point(0, 0);
			_cellSize = new System.Drawing.Size(0, 0);
			_cellCount = new System.Drawing.Size(0, 0);

			_visuals = new VisualCollection(this);

			MouseLeftButtonDown += HandleMouseLeftButtonDown;
			MouseMove += HandleMouseMove;
			MouseLeftButtonUp += HandleMouseLeftButtonUp;
		}

		private void HandleMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Point mousePos = e.GetPosition((UIElement)Parent);

			// Initiate the hit test by setting up a hit test result callback method.
			VisualTreeHelper.HitTest(this, null, hitTestResult => ProcessMouseBuddonDownHitTestResult(mousePos, hitTestResult), new PointHitTestParameters(mousePos));
		}

		// If a child visual object is hit, toggle its opacity to visually indicate a hit.
		private HitTestResultBehavior ProcessMouseBuddonDownHitTestResult(Point mousePos, HitTestResult result)
		{
			if (result.VisualHit is PointVisual pointVisual)
			{
				_drag = new Drag<int>(_visuals.IndexOf(pointVisual), mousePos, Position);
				CaptureMouse();
				return HitTestResultBehavior.Stop;
			}
			return HitTestResultBehavior.Continue;
		}

		private void HandleMouseMove(object sender, MouseEventArgs e)
		{
			if (!IsMouseCaptured || _drag == null)
				return;

			Point mousePos = e.GetPosition((UIElement)Parent);
			_drag.Update(mousePos);

			// Call point move command
		}

		private void HandleMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (!IsMouseCaptured || _drag == null)
				return;

			Point mousePos = e.GetPosition((UIElement)Parent);
			_drag.Update(mousePos);

			// Call point move command

			_drag = null;
			ReleaseMouseCapture();
		}

		private void SetPointsInternal(IEnumerable<PointViewModel> newPoints)
		{
			if (_pointsNotifyCollectionChangedInternal != null)
			{
				_pointsNotifyCollectionChangedInternal.CollectionChanged -= HandlePointsCollectionChanged;
				_pointsNotifyCollectionChangedInternal = null;
			}

			_visuals.Clear();

			if (newPoints != null)
			{
				_pointsNotifyCollectionChangedInternal = newPoints as INotifyCollectionChanged;

				foreach (PointViewModel pointVm in newPoints)
				{
					PointVisual pointVisual = new PointVisual(_position, _cellSize, _cellCount, pointVm.Point, _brush);
					_visuals.Add(pointVisual);
				}

				if (_pointsNotifyCollectionChangedInternal != null)
					_pointsNotifyCollectionChangedInternal.CollectionChanged += HandlePointsCollectionChanged;
			}
		}

		private void HandlePointsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					for (int i = 0; i < e.NewItems.Count; ++i)
					{
						PointViewModel pointVm = (PointViewModel)e.NewItems[i];
						PointVisual pointVisual = new PointVisual(_position, _cellSize, _cellCount, pointVm.Point, _brush);
						_visuals.Insert(e.NewStartingIndex + i, pointVisual);
					}
					break;
				case NotifyCollectionChangedAction.Remove:
					_visuals.RemoveRange(e.OldStartingIndex, e.OldItems.Count);
					break;
				case NotifyCollectionChangedAction.Replace:
					_visuals.RemoveRange(e.OldStartingIndex, e.OldItems.Count);
					for (int i = 0; i < e.NewItems.Count; ++i)
					{
						PointViewModel pointVm = (PointViewModel)e.NewItems[i];
						PointVisual pointVisual = new PointVisual(_position, _cellSize, _cellCount, pointVm.Point, _brush);
						_visuals.Insert(e.NewStartingIndex + i, pointVisual);
					}
					break;
				case NotifyCollectionChangedAction.Reset:
					_visuals.Clear();
					break;
			}
		}

		// Provide a required override for the GetVisualChild method.
		protected override Visual GetVisualChild(int index)
		{
			if (index < 0 || index >= _visuals.Count)
			{
				throw new ArgumentOutOfRangeException();
			}

			return _visuals[index];
		}

		private static void OnPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SymbolMapView thisObj = (SymbolMapView)d;
			System.Drawing.Point position = (System.Drawing.Point)e.NewValue;

			thisObj._position = position;
			foreach (PointVisual pointVisual in thisObj._visuals)
				pointVisual.TablePosition = position;
		}

		private static void OnCellSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SymbolMapView symbolMapView = (SymbolMapView)d;
			System.Drawing.Size cellSize = (System.Drawing.Size)e.NewValue;

			symbolMapView._cellSize = cellSize;
			foreach (PointVisual pointVisual in symbolMapView._visuals)
				pointVisual.TableCellSize = cellSize;
		}

		private static void OnCellCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SymbolMapView symbolMapView = (SymbolMapView)d;
			System.Drawing.Size cellCount = (System.Drawing.Size)e.NewValue;

			symbolMapView._cellCount = cellCount;
			foreach (PointVisual pointVisual in symbolMapView._visuals)
				pointVisual.TableCellCount = cellCount;
		}

		private static void OnPointsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SymbolMapView symbolMapView = (SymbolMapView)d;

			IEnumerable<PointViewModel> points = (IEnumerable<PointViewModel>)e.NewValue;

			symbolMapView.SetPointsInternal(points);
		}

		private static void OnBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SymbolMapView symbolMapView = (SymbolMapView)d;
			Brush brush = (Brush)e.NewValue;

			symbolMapView._brush = brush;
			foreach (PointVisual pointVisual in symbolMapView._visuals)
				pointVisual.Brush = brush;
		}
	}
}
