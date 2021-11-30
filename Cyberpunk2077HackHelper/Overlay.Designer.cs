﻿
using System.Drawing;
using System.Windows.Forms;

namespace Cyberpunk2077HackHelper
{
	partial class Overlay
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
			this.SuspendLayout();
			// 
			// Overlay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(1920, 1080);
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Overlay";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Game Overlay";
			this.TopMost = true;
			this.TransparencyKey = System.Drawing.Color.Black;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Overlay_FormClosed);
			this.Load += new System.EventHandler(this.Overlay_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Overlay_Paint);
			this.ResumeLayout(false);

		}

		#endregion
	}
}

