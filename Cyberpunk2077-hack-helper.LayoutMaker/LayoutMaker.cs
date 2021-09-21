using Cyberpunk2077_hack_helper.Grabbing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace Cyberpunk2077_hack_helper.LayoutMaker
{
	public partial class LayoutMaker : Form
	{
		private Layout _currentLayout = new Layout(new LayoutTable(), new LayoutTable());

		public LayoutMaker()
		{
			InitializeComponent();

			propertyGrid.SelectedObject = _currentLayout;
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult result = openLayoutDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				_currentLayout = ReadLayoutFromFile(openLayoutDialog.FileName);
				propertyGrid.SelectedObject = _currentLayout;
			}
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult result = saveLayoutDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				WriteLayoutToFile(_currentLayout, openLayoutDialog.FileName);
			}
		}

		private void pictureBox_Paint(object sender, PaintEventArgs e)
		{
			SolidBrush redBrush = new SolidBrush(Color.Red);
			Pen pen = new Pen(redBrush, 1);
			for (int y = 0; y < 8; ++y)
				for (int x = 0; x < 8; ++x)
				{
					e.Graphics.DrawRectangle(pen, new Rectangle(
						_currentLayout.Matrix.Position.X + x * _currentLayout.Matrix.PaddingX,
						_currentLayout.Matrix.Position.Y + y * _currentLayout.Matrix.PaddingY,
						_currentLayout.Matrix.PaddingX, _currentLayout.Matrix.PaddingY));
				}

			for (int y = 0; y < 8; ++y)
				for (int x = 0; x < 8; ++x)
				{
					e.Graphics.DrawRectangle(pen, new Rectangle(
						_currentLayout.Sequences.Position.X + x * _currentLayout.Sequences.PaddingX,
						_currentLayout.Sequences.Position.Y + y * _currentLayout.Sequences.PaddingY,
						_currentLayout.Sequences.PaddingX, _currentLayout.Sequences.PaddingY));
				}
		}

		private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			pictureBox.Update();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private static Layout ReadLayoutFromFile(string fileName)
		{
			string contents = File.ReadAllText(fileName);
			return JsonConvert.DeserializeObject<Layout>(contents);
		}

		private static void WriteLayoutToFile(Layout layout, string fileName)
		{
			string contents = JsonConvert.SerializeObject(layout);
			File.WriteAllText(fileName, contents);
		}
	}
}
