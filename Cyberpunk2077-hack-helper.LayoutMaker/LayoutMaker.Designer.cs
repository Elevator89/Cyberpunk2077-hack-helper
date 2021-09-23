
namespace Cyberpunk2077_hack_helper.LayoutMaker
{
	partial class LayoutMaker
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayoutMaker));
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.mainMenu = new System.Windows.Forms.MenuStrip();
			this.layoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openLayoutDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveLayoutDialog = new System.Windows.Forms.SaveFileDialog();
			this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.MatixPosXUpDown = new System.Windows.Forms.NumericUpDown();
			this.layoutBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.pointControl1 = new Cyberpunk2077_hack_helper.LayoutMaker.PointControl();
			this.layoutTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.mainMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MatixPosXUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutTableBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox
			// 
			this.pictureBox.BackColor = System.Drawing.Color.White;
			this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(1919, 1079);
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
			// 
			// mainMenu
			// 
			this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layoutToolStripMenuItem,
            this.imageToolStripMenuItem});
			this.mainMenu.Location = new System.Drawing.Point(0, 0);
			this.mainMenu.Name = "mainMenu";
			this.mainMenu.Size = new System.Drawing.Size(800, 24);
			this.mainMenu.TabIndex = 3;
			this.mainMenu.Text = "menuStrip";
			// 
			// layoutToolStripMenuItem
			// 
			this.layoutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.layoutToolStripMenuItem.Name = "layoutToolStripMenuItem";
			this.layoutToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
			this.layoutToolStripMenuItem.Text = "Layout";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.newToolStripMenuItem.Text = "New";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(100, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// imageToolStripMenuItem
			// 
			this.imageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadImageToolStripMenuItem});
			this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
			this.imageToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
			this.imageToolStripMenuItem.Text = "Image";
			// 
			// loadImageToolStripMenuItem
			// 
			this.loadImageToolStripMenuItem.Name = "loadImageToolStripMenuItem";
			this.loadImageToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.loadImageToolStripMenuItem.Text = "Load image";
			// 
			// openLayoutDialog
			// 
			this.openLayoutDialog.FileName = "openFileDialog1";
			// 
			// openImageDialog
			// 
			this.openImageDialog.FileName = "openFileDialog1";
			// 
			// statusStrip
			// 
			this.statusStrip.Location = new System.Drawing.Point(0, 428);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(800, 22);
			this.statusStrip.TabIndex = 4;
			this.statusStrip.Text = "statusStrip";
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 24);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.AutoScroll = true;
			this.splitContainer.Panel1.Controls.Add(this.pictureBox);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.pointControl1);
			this.splitContainer.Panel2.Controls.Add(this.MatixPosXUpDown);
			this.splitContainer.Size = new System.Drawing.Size(800, 404);
			this.splitContainer.SplitterDistance = 566;
			this.splitContainer.TabIndex = 6;
			// 
			// MatixPosXUpDown
			// 
			this.MatixPosXUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.layoutTableBindingSource, "TestValue", true));
			this.MatixPosXUpDown.Location = new System.Drawing.Point(38, 20);
			this.MatixPosXUpDown.Maximum = new decimal(new int[] {
            1920,
            0,
            0,
            0});
			this.MatixPosXUpDown.Name = "MatixPosXUpDown";
			this.MatixPosXUpDown.Size = new System.Drawing.Size(120, 20);
			this.MatixPosXUpDown.TabIndex = 0;
			// 
			// layoutBindingSource
			// 
			this.layoutBindingSource.DataSource = typeof(Cyberpunk2077_hack_helper.Grabbing.Layout);
			// 
			// pointControl1
			// 
			this.pointControl1.DataBindings.Add(new System.Windows.Forms.Binding("Point", this.layoutTableBindingSource, "Position", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.pointControl1.Location = new System.Drawing.Point(16, 65);
			this.pointControl1.MaximumSize = new System.Drawing.Size(0, 20);
			this.pointControl1.MinimumSize = new System.Drawing.Size(154, 20);
			this.pointControl1.Name = "pointControl1";
			this.pointControl1.Point = new System.Drawing.Point(0, 0);
			this.pointControl1.Size = new System.Drawing.Size(154, 20);
			this.pointControl1.TabIndex = 1;
			// 
			// layoutTableBindingSource
			// 
			this.layoutTableBindingSource.DataSource = typeof(Cyberpunk2077_hack_helper.Grabbing.LayoutTable);
			// 
			// LayoutMaker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.mainMenu);
			this.MainMenuStrip = this.mainMenu;
			this.Name = "LayoutMaker";
			this.Text = "LayoutMaker";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.MatixPosXUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutTableBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem layoutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadImageToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openLayoutDialog;
		private System.Windows.Forms.SaveFileDialog saveLayoutDialog;
		private System.Windows.Forms.OpenFileDialog openImageDialog;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.NumericUpDown MatixPosXUpDown;
		private System.Windows.Forms.BindingSource layoutBindingSource;
		private PointControl pointControl1;
		private System.Windows.Forms.BindingSource layoutTableBindingSource;
	}
}

