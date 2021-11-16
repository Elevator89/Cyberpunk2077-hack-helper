using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Cyberpunk2077_hack_helper.LayoutMarker.Views
{
	public class PointView : FrameworkElement
	{
		public static readonly DependencyProperty TablePositionProperty = DependencyProperty.Register(
			"TablePosition",
			typeof(System.Drawing.Point),
			typeof(PointView),
			new FrameworkPropertyMetadata(
				new System.Drawing.Point(0, 0),
				FrameworkPropertyMetadataOptions.AffectsMeasure |
				FrameworkPropertyMetadataOptions.AffectsRender,
				new PropertyChangedCallback(OnTablePositionChanged)));

		public static readonly DependencyProperty TableCellSizeProperty = DependencyProperty.Register(
			"TableCellSize",
			typeof(System.Drawing.Size),
			typeof(PointView),
			new FrameworkPropertyMetadata(
				new System.Drawing.Size(0, 0),
				FrameworkPropertyMetadataOptions.AffectsMeasure |
				FrameworkPropertyMetadataOptions.AffectsRender,
				new PropertyChangedCallback(OnTableCellSizeChanged)));

		public static readonly DependencyProperty TableCellCountProperty = DependencyProperty.Register(
			"TableCellCount",
			typeof(System.Drawing.Size),
			typeof(PointView),
			new FrameworkPropertyMetadata(
				new System.Drawing.Size(0, 0),
				FrameworkPropertyMetadataOptions.AffectsMeasure |
				FrameworkPropertyMetadataOptions.AffectsRender,
				new PropertyChangedCallback(OnTableCellCountChanged)));

		public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(
			"Position",
			typeof(System.Drawing.Point),
			typeof(PointView),
			new FrameworkPropertyMetadata(
				new System.Drawing.Point(0, 0),
				FrameworkPropertyMetadataOptions.AffectsMeasure |
				FrameworkPropertyMetadataOptions.AffectsRender,
				new PropertyChangedCallback(OnPositionChanged))
			{
				BindsTwoWayByDefault = true
			});

		public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(
			"Brush",
			typeof(Brush),
			typeof(PointView),
			new FrameworkPropertyMetadata(
				Brushes.White,
				FrameworkPropertyMetadataOptions.AffectsRender,
				new PropertyChangedCallback(OnBrushChanged)));

		private const double PointSize = 2f;
		private const double PointHalfSize = 0.5 * PointSize;
		private const double Thickness = 1.0;

		private System.Drawing.Point _tablePosition;
		private System.Drawing.Size _tableCellSize;
		private System.Drawing.Size _tableCellCount;
		private System.Drawing.Point _pointPosition;

		private Brush _brush;
		private Pen _pen;

		private readonly DrawingVisual _visual;
		private readonly VisualCollection _visuals;

		private Drag<int> _drag = null;

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

		public System.Drawing.Point Position
		{
			get { return (System.Drawing.Point)GetValue(PositionProperty); }
			set { SetValue(PositionProperty, value); }
		}

		public Brush Brush
		{
			get { return (Brush)GetValue(BrushProperty); }
			set { SetValue(BrushProperty, value); }
		}

		protected override int VisualChildrenCount => _visuals.Count;

		public PointView()
		{
			_tablePosition = new System.Drawing.Point(0, 0);
			_tableCellSize = new System.Drawing.Size(0, 0);
			_tableCellCount = new System.Drawing.Size(0, 0);
			_pointPosition = new System.Drawing.Point(0, 0);
			_pen = new Pen(Brushes.Red, Thickness);

			_visual = new DrawingVisual();
			_visuals = new VisualCollection(this) { _visual };

			MouseLeftButtonDown += HandleMouseLeftButtonDown;
			MouseMove += HandleMouseMove;
			MouseLeftButtonUp += HandleMouseLeftButtonUp;
		}

		private void HandleMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Point mousePos = e.GetPosition((UIElement)sender);
			_drag = new Drag<int>(0, mousePos, Position);
			CaptureMouse();
		}

		private void HandleMouseMove(object sender, MouseEventArgs e)
		{
			if (!IsMouseCaptured || _drag == null)
				return;

			Point mousePos = e.GetPosition((UIElement)sender);
			_drag.Update(mousePos);
			Position = _drag.TargetEnd;
		}

		private void HandleMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (!IsMouseCaptured || _drag == null)
				return;

			Point mousePos = e.GetPosition((UIElement)sender);
			_drag.Update(mousePos);
			Position = _drag.TargetEnd;

			_drag = null;
			ReleaseMouseCapture();
		}

		// Provide a required override for the GetVisualChild method.
		protected override Visual GetVisualChild(int index)
		{
			if (index < 0 || index >= _visuals.Count)
				throw new ArgumentOutOfRangeException();

			return _visuals[index];
		}

		private static void OnTablePositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PointView thisObj = (PointView)d;
			thisObj._tablePosition = (System.Drawing.Point)e.NewValue;
			thisObj.RefreshDrawing();
		}

		private static void OnTableCellSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PointView thisObj = (PointView)d;
			thisObj._tableCellSize = (System.Drawing.Size)e.NewValue;
			thisObj.RefreshDrawing();
		}

		private static void OnTableCellCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PointView thisObj = (PointView)d;
			thisObj._tableCellCount = (System.Drawing.Size)e.NewValue;
			thisObj.RefreshDrawing();
		}

		private static void OnPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PointView thisObj = (PointView)d;
			thisObj._pointPosition = (System.Drawing.Point)e.NewValue;
			thisObj.RefreshDrawing();
		}

		private static void OnBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PointView thisObj = (PointView)d;
			Brush brush = (Brush)e.NewValue;
			thisObj._pen.Brush = brush;

			Brush transparentBrush = brush.Clone();
			transparentBrush.Opacity = 0.1;
			thisObj._brush = transparentBrush;
			thisObj.RefreshDrawing();
		}

		private void RefreshDrawing()
		{
			using (DrawingContext drawingContext = _visual.RenderOpen())
			{
				Vector pointV = new Vector(_pointPosition.X, _pointPosition.Y);

				for (int row = 0; row < _tableCellCount.Height; ++row)
				{
					for (int col = 0; col < _tableCellCount.Width; ++col)
					{
						Point cellPos = new Point(
							_tablePosition.X + col * _tableCellSize.Width,
							_tablePosition.Y + row * _tableCellSize.Height);

						Point cellPointPos = cellPos + pointV;

						Rect rect = new Rect(cellPointPos.X - PointHalfSize, cellPointPos.Y - PointHalfSize, PointSize, PointSize);
						drawingContext.DrawRectangle(_brush, _pen, rect);
					}
				}
				drawingContext.Close();
			}
		}
	}
}
