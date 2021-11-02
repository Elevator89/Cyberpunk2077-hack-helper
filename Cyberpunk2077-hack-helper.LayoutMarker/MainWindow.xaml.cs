using Cyberpunk2077_hack_helper.Grabbing;
using Cyberpunk2077_hack_helper.LayoutMarker.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private IDialogService _dialogService;
		private IFileService _fileService;
		private ApplicationViewModel _applicationViewModel;

		private SolidColorBrush _probeBrush = new SolidColorBrush();

		private SizeTool _sizeTool = new SizeTool();
		private PointTool _pointTool = new PointTool();

		private ITool _activeTool = null;

		public MainWindow()
		{
			_dialogService = new DefaultDialogService();
			_fileService = new JsonFileService();
			_applicationViewModel = new ApplicationViewModel(_dialogService, _fileService);

			InitializeComponent();
			DataContext = _applicationViewModel;
			Cursor.Fill = _probeBrush;
		}

		private void WindowLoaded(object sender, EventArgs e)
		{
		}

		private void OpenCmdExecuted(object target, ExecutedRoutedEventArgs e)
		{
			_applicationViewModel.Open();
		}

		private void SaveCmdExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			_applicationViewModel.Save();
		}

		private void CloseCmdExecuted(object target, ExecutedRoutedEventArgs e)
		{
			Close();
		}


		private Color GetPixel(BitmapSource source, Point position)
		{
			if (source.Format != PixelFormats.Bgra32)
				source = new FormatConvertedBitmap(source, PixelFormats.Bgra32, null, 0);

			// Stride = (width) x (bytes per pixel)
			int bytesPerPixel = source.Format.BitsPerPixel / 8;
			int stride = source.PixelWidth * bytesPerPixel; // bytes in one line
			byte[] pixel = new byte[bytesPerPixel];

			int x = (int)position.X;
			int y = (int)position.Y;

			Int32Rect rect = new Int32Rect(x, y, 1, 1);
			source.CopyPixels(rect, pixel, stride, 0);

			return Color.FromRgb(pixel[2], pixel[1], pixel[0]);
		}

		private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
		{
			_activeTool?.MouseUp(Util.ToDrawingPoint(e.GetPosition(Image)), e.ChangedButton);
		}

		private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
		{
			_activeTool?.MouseDown(Util.ToDrawingPoint(e.GetPosition(Image)), e.ChangedButton);
		}

		private void Canvas_MouseEnter(object sender, MouseEventArgs e)
		{
			_activeTool?.MouseEnter(Util.ToDrawingPoint(e.GetPosition(Image)));
		}

		private void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			Point mousePos = e.GetPosition(Image);
			System.Drawing.Point drawingPoint = Util.ToDrawingPoint(mousePos);

			_activeTool?.MouseMove(drawingPoint);

			//_applicationViewModel.Layout.Sequences.SelectedSymbolMap.Points[_applicationViewModel.Layout.Sequences.SelectedSymbolMap.SelectedPointIndex].Point = drawingPoint;

			ImageSource imageSource = Image.Source;
			BitmapSource bitmap = (BitmapSource)imageSource;

			Color color = GetPixel(bitmap, mousePos);
			_probeBrush.Color = color;
		}

		private void Canvas_MouseLeave(object sender, MouseEventArgs e)
		{
			_activeTool?.MouseLeave(Util.ToDrawingPoint(e.GetPosition(Image)));
		}

		private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
		{
			_activeTool?.MouseWheel(Util.ToDrawingPoint(e.GetPosition(Image)), e.Delta);
		}
	}
}
