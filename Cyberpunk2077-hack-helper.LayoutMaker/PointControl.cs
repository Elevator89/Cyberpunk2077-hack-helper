using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cyberpunk2077_hack_helper.LayoutMaker
{
	public partial class PointControl : UserControl
	{
		private Point _point;

		public Point Point
		{
			get { return _point; }
			set
			{
				if (_point == value)
					return;

				_point = value;
				pointBindingSource.DataSource = _point;
			}
		}

		public PointControl()
		{
			InitializeComponent();
		}

		private void size1_Load(object sender, EventArgs e)
		{

		}
	}
}
