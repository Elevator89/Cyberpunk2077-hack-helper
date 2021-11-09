using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class SymbolMapView : FrameworkElement
	{
		public static readonly DependencyProperty PositionProperty;
		public static readonly DependencyProperty CellSizeProperty;
		public static readonly DependencyProperty CellCountProperty;
		public static readonly DependencyProperty PointsProperty;
		public static readonly DependencyProperty BrushProperty;

		static SymbolMapView()
		{
			PositionProperty = DependencyProperty.Register(
						"Position",
						typeof(System.Drawing.Point),
						typeof(SymbolMapView),
						new FrameworkPropertyMetadata(
							new System.Drawing.Point(0, 0),
							FrameworkPropertyMetadataOptions.AffectsMeasure |
							FrameworkPropertyMetadataOptions.AffectsRender,
							new PropertyChangedCallback(OnPositionChanged)));

			CellSizeProperty = DependencyProperty.Register(
						"CellSize",
						typeof(System.Drawing.Size),
						typeof(SymbolMapView),
						new FrameworkPropertyMetadata(
							new System.Drawing.Size(0, 0),
							FrameworkPropertyMetadataOptions.AffectsMeasure |
							FrameworkPropertyMetadataOptions.AffectsRender,
							new PropertyChangedCallback(OnCellSizeChanged)));

			CellCountProperty = DependencyProperty.Register(
						"CellCount",
						typeof(System.Drawing.Size),
						typeof(SymbolMapView),
						new FrameworkPropertyMetadata(
							new System.Drawing.Size(0, 0),
							FrameworkPropertyMetadataOptions.AffectsMeasure |
							FrameworkPropertyMetadataOptions.AffectsRender,
							new PropertyChangedCallback(OnCellCountChanged)));

			PointsProperty = DependencyProperty.Register(
						"Points",
						typeof(ObservableCollection<PointViewModel>),
						typeof(SymbolMapView),
						new FrameworkPropertyMetadata(
							null,
							FrameworkPropertyMetadataOptions.AffectsMeasure |
							FrameworkPropertyMetadataOptions.AffectsRender,
							new PropertyChangedCallback(OnPointsChanged)));

			BrushProperty = DependencyProperty.Register(
						"Brush",
						typeof(Brush),
						typeof(SymbolMapView),
						new FrameworkPropertyMetadata(
							Brushes.White,
							FrameworkPropertyMetadataOptions.AffectsRender,
							new PropertyChangedCallback(OnBrushChanged)));
		}

		private const double PointSize = 2f;
		private const double PointHalfSize = 0.5 * PointSize;

		private System.Drawing.Point _position;
		private System.Drawing.Size _cellSize;
		private System.Drawing.Size _cellCount;

		private readonly List<System.Drawing.Point> _pointsInternal;
		private INotifyCollectionChanged _pointsNotifyCollectionChangedInternal;

		private Brush _brush;
		private Pen _pen;

		// Create a collection of child visual objects.
		private readonly VisualCollection _visuals;

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
			_pen = new Pen(_brush, 1.0);
			_position = new System.Drawing.Point(0, 0);
			_cellSize = new System.Drawing.Size(0, 0);
			_cellCount = new System.Drawing.Size(0, 0);
			_pointsInternal = new List<System.Drawing.Point>();

			_visuals = new VisualCollection(this);
		}

		private void RedrawPoints()
		{
			for (int pointIndex = 0; pointIndex < _pointsInternal.Count; ++pointIndex)
				RedrawPoint(pointIndex);
		}

		private void RedrawPoint(int pointIndex)
		{
			DrawingVisual positionerVisual = (DrawingVisual)_visuals[pointIndex];
			using (DrawingContext drawingContext = positionerVisual.RenderOpen())
			{
				System.Drawing.Point point = _pointsInternal[pointIndex];
				Vector pointV = new Vector(point.X, point.Y);

				for (int row = 0; row < _cellCount.Height; ++row)
				{
					for (int col = 0; col < _cellCount.Width; ++col)
					{
						Point cellPos = new Point(
							_position.X + col * _cellSize.Width,
							_position.Y + row * _cellSize.Height);

						Point cellPointPos = cellPos + pointV;

						Rect rect = new Rect(cellPointPos.X - PointHalfSize, cellPointPos.Y - PointHalfSize, PointSize, PointSize);
						drawingContext.DrawRectangle(null, _pen, rect);
					}
				}
				drawingContext.Close();
			}
		}

		private void SetPointsInternal(IEnumerable<PointViewModel> newPoints)
		{
			if (_pointsNotifyCollectionChangedInternal != null)
			{
				_pointsNotifyCollectionChangedInternal.CollectionChanged -= HandlePointsCollectionChanged;
				_pointsNotifyCollectionChangedInternal = null;
			}

			_pointsInternal.Clear();
			_visuals.Clear();

			if (newPoints != null)
			{
				_pointsInternal.AddRange(newPoints.Select(pvm => pvm.Point));
				_pointsNotifyCollectionChangedInternal = newPoints as INotifyCollectionChanged;

				foreach (System.Drawing.Point point in _pointsInternal)
					_visuals.Add(new DrawingVisual());

				if (_pointsNotifyCollectionChangedInternal != null)
					_pointsNotifyCollectionChangedInternal.CollectionChanged += HandlePointsCollectionChanged;
			}

			RedrawPoints();
		}

		private void HandlePointsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					_pointsInternal.InsertRange(e.NewStartingIndex, e.NewItems.Cast<PointViewModel>().Select(vm => vm.Point));
					for (int i = 0; i < e.NewItems.Count; ++i)
						_visuals.Insert(e.NewStartingIndex + i, new DrawingVisual());
					break;
				case NotifyCollectionChangedAction.Remove:
					_pointsInternal.RemoveRange(e.OldStartingIndex, e.OldItems.Count);
					_visuals.RemoveRange(e.OldStartingIndex, e.OldItems.Count);
					break;
				case NotifyCollectionChangedAction.Replace:
					_pointsInternal.RemoveRange(e.OldStartingIndex, e.OldItems.Count);
					_visuals.RemoveRange(e.OldStartingIndex, e.OldItems.Count);
					_pointsInternal.InsertRange(e.NewStartingIndex, e.NewItems.Cast<PointViewModel>().Select(vm => vm.Point));
					for (int i = 0; i < e.NewItems.Count; ++i)
						_visuals.Insert(e.NewStartingIndex + i, new DrawingVisual());
					break;
				case NotifyCollectionChangedAction.Reset:
					_pointsInternal.Clear();
					_visuals.Clear();
					break;
			}
			RedrawPoints();
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
			thisObj.RedrawPoints();
		}

		private static void OnCellSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SymbolMapView thisObj = (SymbolMapView)d;
			System.Drawing.Size cellSize = (System.Drawing.Size)e.NewValue;

			thisObj._cellSize = cellSize;
			thisObj.RedrawPoints();
		}

		private static void OnCellCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SymbolMapView thisObj = (SymbolMapView)d;
			System.Drawing.Size cellCount = (System.Drawing.Size)e.NewValue;

			thisObj._cellCount = cellCount;
			thisObj.RedrawPoints();
		}

		private static void OnPointsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SymbolMapView symbolMapView = (SymbolMapView)d;

			IEnumerable<PointViewModel> points = (IEnumerable<PointViewModel>)e.NewValue;

			symbolMapView.SetPointsInternal(points);
		}

		private static void OnBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SymbolMapView thisObj = (SymbolMapView)d;
			Brush brush = (Brush)e.NewValue;

			thisObj._brush = brush;
			thisObj._pen = new Pen(brush, 1.0);
			thisObj.RedrawPoints();
		}
	}
}
