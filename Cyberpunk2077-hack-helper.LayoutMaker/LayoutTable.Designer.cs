
namespace Cyberpunk2077_hack_helper.LayoutMaker
{
	partial class LayoutTable
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.positionLabel = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.point1 = new Cyberpunk2077_hack_helper.LayoutMaker.PointControl();
			this.size1 = new Cyberpunk2077_hack_helper.LayoutMaker.SizeControl();
			this.SuspendLayout();
			// 
			// positionLabel
			// 
			this.positionLabel.AutoSize = true;
			this.positionLabel.Location = new System.Drawing.Point(8, 6);
			this.positionLabel.Name = "positionLabel";
			this.positionLabel.Size = new System.Drawing.Size(44, 13);
			this.positionLabel.TabIndex = 1;
			this.positionLabel.Text = "Position";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 32);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(45, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Cell size";
			// 
			// point1
			// 
			this.point1.Location = new System.Drawing.Point(58, 3);
			this.point1.MaximumSize = new System.Drawing.Size(0, 20);
			this.point1.MinimumSize = new System.Drawing.Size(154, 20);
			this.point1.Name = "point1";
			this.point1.Size = new System.Drawing.Size(154, 20);
			this.point1.TabIndex = 6;
			// 
			// size1
			// 
			this.size1.Location = new System.Drawing.Point(58, 29);
			this.size1.MaximumSize = new System.Drawing.Size(0, 20);
			this.size1.MinimumSize = new System.Drawing.Size(154, 20);
			this.size1.Name = "size1";
			this.size1.Size = new System.Drawing.Size(154, 20);
			this.size1.TabIndex = 7;
			// 
			// LayoutTable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.size1);
			this.Controls.Add(this.point1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.positionLabel);
			this.MaximumSize = new System.Drawing.Size(0, 52);
			this.MinimumSize = new System.Drawing.Size(212, 52);
			this.Name = "LayoutTable";
			this.Size = new System.Drawing.Size(212, 52);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label positionLabel;
		private System.Windows.Forms.Label label3;
		private PointControl point1;
		private SizeControl size1;
	}
}
