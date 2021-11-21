using Cyberpunk2077HackHelper.LayoutMarker.ViewModels;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Cyberpunk2077HackHelper.LayoutMarker
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly IDialogService _dialogService;
		private readonly IFileService _fileService;
		private readonly ApplicationViewModel _applicationViewModel;

		private readonly SolidColorBrush _probeBrush = new SolidColorBrush();

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
			_applicationViewModel.LoadLayout();
		}

		private void SaveCmdExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			_applicationViewModel.SaveLayout();
		}

		private void MartixSymbolMapsLoadMenuItem_Click(object target, RoutedEventArgs e)
		{
			_applicationViewModel.LoadMatrixSymbolMaps();
		}

		private void MartixSymbolMapsSaveMenuItem_Click(object sender, RoutedEventArgs e)
		{
			_applicationViewModel.SaveMatrixSymbolMaps();
		}

		private void SequenceSymbolMapsLoadMenuItem_Click(object target, RoutedEventArgs e)
		{
			_applicationViewModel.LoadSequenceSymbolMaps();
		}

		private void SequenceSymbolMapsSaveMenuItem_Click(object sender, RoutedEventArgs e)
		{
			_applicationViewModel.SaveSequenceSymbolMaps();
		}

		private void CloseCmdExecuted(object target, ExecutedRoutedEventArgs e)
		{
			Close();
		}

		private void ImageLoadMenuItem_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (_dialogService.OpenFileDialog() == true)
				{
					Screenshot.Source = new BitmapImage(new Uri(_dialogService.FilePath));
				}
			}
			catch (Exception ex)
			{
				_dialogService.ShowMessage(ex.Message);
			}
		}

		private void Canvas_MouseUp(object sender, MouseButtonEventArgs e) { }
		private void Canvas_MouseDown(object sender, MouseButtonEventArgs e) { }
		private void Canvas_MouseEnter(object sender, MouseEventArgs e) { }
		private void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			Point mousePos = e.GetPosition(Screenshot);
			ImageSource imageSource = Screenshot.Source;
			BitmapSource bitmap = (BitmapSource)imageSource;

			Color color = GetPixel(bitmap, mousePos);
			_probeBrush.Color = color;
		}
		private void Canvas_MouseLeave(object sender, MouseEventArgs e) { }
		private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e) { }

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

	}
}
