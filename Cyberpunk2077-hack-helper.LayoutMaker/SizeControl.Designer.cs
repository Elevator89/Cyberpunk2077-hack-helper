
namespace Cyberpunk2077_hack_helper.LayoutMaker
{
	partial class SizeControl
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
			this.widthLabel = new System.Windows.Forms.Label();
			this.heightLabel = new System.Windows.Forms.Label();
			this.heightUpDown = new System.Windows.Forms.NumericUpDown();
			this.widthUpDown = new System.Windows.Forms.NumericUpDown();
			this.sizeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sizeBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// widthLabel
			// 
			this.widthLabel.AutoSize = true;
			this.widthLabel.Location = new System.Drawing.Point(3, 2);
			this.widthLabel.Name = "widthLabel";
			this.widthLabel.Size = new System.Drawing.Size(18, 13);
			this.widthLabel.TabIndex = 16;
			this.widthLabel.Text = "W";
			// 
			// heightLabel
			// 
			this.heightLabel.AutoSize = true;
			this.heightLabel.Location = new System.Drawing.Point(80, 2);
			this.heightLabel.Name = "heightLabel";
			this.heightLabel.Size = new System.Drawing.Size(15, 13);
			this.heightLabel.TabIndex = 15;
			this.heightLabel.Text = "H";
			// 
			// heightUpDown
			// 
			this.heightUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.sizeBindingSource, "Height", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0", "N0"));
			this.heightUpDown.Location = new System.Drawing.Point(100, 0);
			this.heightUpDown.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
			this.heightUpDown.Name = "heightUpDown";
			this.heightUpDown.Size = new System.Drawing.Size(50, 20);
			this.heightUpDown.TabIndex = 14;
			// 
			// widthUpDown
			// 
			this.widthUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.sizeBindingSource, "Width", true));
			this.widthUpDown.Location = new System.Drawing.Point(24, 0);
			this.widthUpDown.Maximum = new decimal(new int[] {
            1920,
            0,
            0,
            0});
			this.widthUpDown.Name = "widthUpDown";
			this.widthUpDown.Size = new System.Drawing.Size(50, 20);
			this.widthUpDown.TabIndex = 13;
			// 
			// sizeBindingSource
			// 
			this.sizeBindingSource.DataSource = typeof(System.Drawing.Size);
			// 
			// Size
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.widthLabel);
			this.Controls.Add(this.heightLabel);
			this.Controls.Add(this.heightUpDown);
			this.Controls.Add(this.widthUpDown);
			this.MaximumSize = new System.Drawing.Size(0, 20);
			this.MinimumSize = new System.Drawing.Size(154, 20);
			this.Name = "Size";
			this.Size = new System.Drawing.Size(154, 20);
			((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sizeBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label widthLabel;
		private System.Windows.Forms.Label heightLabel;
		private System.Windows.Forms.NumericUpDown heightUpDown;
		private System.Windows.Forms.NumericUpDown widthUpDown;
		private System.Windows.Forms.BindingSource sizeBindingSource;
	}
}
