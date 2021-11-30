using Cyberpunk2077HackHelper.Grabbing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cyberpunk2077HackHelper
{
	public partial class Overlay : Form
	{
		private readonly Hotkey _hk = new Hotkey() { Control = true, KeyCode = Keys.D1 };

		private List<SymbolMap> _matrixSymbolMaps;
		private List<SymbolMap> _sequenceSymbolMaps;
		private readonly List<Layout> _layouts = new List<Layout>();

		public Overlay()
		{
			InitializeComponent();
		}

		private void Overlay_Paint(object sender, PaintEventArgs e)
		{
			// Create point for upper-left corner of image.
			PointF ulCorner = new PointF(100.0F, 100.0F);
			PointF ul2Corner = new PointF(200.0F, 200.0F);

			SolidBrush myBrush = new SolidBrush(Color.Red);
			Pen pen = new Pen(myBrush, 3);
			e.Graphics.DrawEllipse(pen, new Rectangle(0, 0, 200, 300));
			e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, Width, Height));
		}


		private void MakeScreenshot()
		{
			using (Bitmap bmpScreenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
			using (Graphics g = Graphics.FromImage(bmpScreenCapture))
			{
				g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
								 Screen.PrimaryScreen.Bounds.Y,
								 0, 0,
								 bmpScreenCapture.Size,
								 CopyPixelOperation.SourceCopy);
			}


		}

		private void Overlay_Load(object sender, EventArgs e)
		{
			string matrixSymbolMapsContents = File.ReadAllText("Layouts/matrixSymbolMaps.json");
			string sequenceSymbolMapsContents = File.ReadAllText("Layouts/sequenceSymbolMaps.json");

			_matrixSymbolMaps = JsonConvert.DeserializeObject<List<SymbolMap>>(matrixSymbolMapsContents);
			_sequenceSymbolMaps = JsonConvert.DeserializeObject<List<SymbolMap>>(sequenceSymbolMapsContents);

			_hk.Pressed += ProcessHotKeyPressed;

			if (_hk.GetCanRegister(this))
				_hk.Register(this.Handle);
			else
				Console.WriteLine("Whoops, looks like attempts to register will fail or throw an exception, show an error / visual user feedback");
		}

		private void Overlay_FormClosed(object sender, FormClosedEventArgs e)
		{
			_hk.Pressed -= ProcessHotKeyPressed;

			if (_hk.Registered)
				_hk.Unregister();
		}

		private void ProcessHotKeyPressed(object sender, HandledEventArgs e)
		{
			this.WindowState = FormWindowState.Maximized;
			this.TopLevel = true;
			Console.WriteLine("Windows + 1 pressed!");
		}
	}
}
