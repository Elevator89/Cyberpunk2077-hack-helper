
namespace Cyberpunk2077_hack_helper.LayoutMaker
{
	partial class PointControl
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
			this.components = new System.ComponentModel.Container();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pointYnumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.pointBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.pointXNumericUpDown = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.pointYnumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pointBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pointXNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 2);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(14, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "X";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(80, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(14, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Y";
			// 
			// pointYnumericUpDown
			// 
			this.pointYnumericUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.pointBindingSource, "Y", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, "0", "N0"));
			this.pointYnumericUpDown.Location = new System.Drawing.Point(100, 0);
			this.pointYnumericUpDown.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
			this.pointYnumericUpDown.MinimumSize = new System.Drawing.Size(50, 0);
			this.pointYnumericUpDown.Name = "pointYnumericUpDown";
			this.pointYnumericUpDown.Size = new System.Drawing.Size(50, 20);
			this.pointYnumericUpDown.TabIndex = 10;
			// 
			// pointBindingSource
			// 
			this.pointBindingSource.DataSource = typeof(System.Drawing.Point);
			// 
			// pointXNumericUpDown
			// 
			this.pointXNumericUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.pointBindingSource, "X", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, "0", "N0"));
			this.pointXNumericUpDown.Location = new System.Drawing.Point(24, 0);
			this.pointXNumericUpDown.Maximum = new decimal(new int[] {
            1920,
            0,
            0,
            0});
			this.pointXNumericUpDown.Name = "pointXNumericUpDown";
			this.pointXNumericUpDown.Size = new System.Drawing.Size(50, 20);
			this.pointXNumericUpDown.TabIndex = 9;
			// 
			// PointControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pointYnumericUpDown);
			this.Controls.Add(this.pointXNumericUpDown);
			this.MaximumSize = new System.Drawing.Size(0, 20);
			this.MinimumSize = new System.Drawing.Size(154, 20);
			this.Name = "PointControl";
			this.Size = new System.Drawing.Size(154, 20);
			((System.ComponentModel.ISupportInitialize)(this.pointYnumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pointBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pointXNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown pointYnumericUpDown;
		private System.Windows.Forms.NumericUpDown pointXNumericUpDown;
		public System.Windows.Forms.BindingSource pointBindingSource;
	}
}
