using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cyberpunk2077_hack_helper
{
	public partial class Overlay : Form
	{
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
	}
}
