
/****************************************************************************

Tilde

Copyright (c) 2008 Tantalus Media Pty

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

****************************************************************************/

namespace Tilde.LuaDebugger
{
	partial class ThreadsWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThreadsWindow));
			this.threadListView = new Tilde.Framework.Controls.DBListView();
			this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderThreadID = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderLocation = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderState = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderPeakTime = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderAverageTime = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderStackSize = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderStackUsed = new System.Windows.Forms.ColumnHeader();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.buttonRefresh = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// threadListView
			// 
			this.threadListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderThreadID,
            this.columnHeaderLocation,
            this.columnHeaderState,
            this.columnHeaderPeakTime,
            this.columnHeaderAverageTime,
            this.columnHeaderStackSize,
            this.columnHeaderStackUsed});
			this.threadListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.threadListView.FullRowSelect = true;
			this.threadListView.Location = new System.Drawing.Point(0, 25);
			this.threadListView.Name = "threadListView";
			this.threadListView.Size = new System.Drawing.Size(658, 270);
			this.threadListView.SmallImageList = this.imageList;
			this.threadListView.StateImageList = this.imageList;
			this.threadListView.TabIndex = 0;
			this.threadListView.UseCompatibleStateImageBehavior = false;
			this.threadListView.View = System.Windows.Forms.View.Details;
			this.threadListView.ItemActivate += new System.EventHandler(this.threadListView_ItemActivate);
			this.threadListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.threadListView_ColumnClick);
			// 
			// columnHeaderName
			// 
			this.columnHeaderName.Text = "Name";
			this.columnHeaderName.Width = 196;
			// 
			// columnHeaderThreadID
			// 
			this.columnHeaderThreadID.Text = "Thread ID";
			this.columnHeaderThreadID.Width = 68;
			// 
			// columnHeaderLocation
			// 
			this.columnHeaderLocation.Text = "Location";
			this.columnHeaderLocation.Width = 170;
			// 
			// columnHeaderState
			// 
			this.columnHeaderState.Text = "State";
			// 
			// columnHeaderPeakTime
			// 
			this.columnHeaderPeakTime.Text = "Peak (us)";
			this.columnHeaderPeakTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// columnHeaderAverageTime
			// 
			this.columnHeaderAverageTime.Text = "Average (us)";
			this.columnHeaderAverageTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderAverageTime.Width = 80;
			// 
			// columnHeaderStackSize
			// 
			this.columnHeaderStackSize.Text = "Stack Size";
			this.columnHeaderStackSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderStackSize.Width = 64;
			// 
			// columnHeaderStackUsed
			// 
			this.columnHeaderStackUsed.Text = "Stack Used";
			this.columnHeaderStackUsed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderStackUsed.Width = 70;
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Fuchsia;
			this.imageList.Images.SetKeyName(0, "CurrentFrame.bmp");
			this.imageList.Images.SetKeyName(1, "BreakedThread.bmp");
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonRefresh});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(658, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// buttonRefresh
			// 
			this.buttonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.buttonRefresh.Image = global::Tilde.LuaDebugger.Properties.Resources.BrwRefresh;
			this.buttonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new System.Drawing.Size(23, 22);
			this.buttonRefresh.Text = "Refresh";
			this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
			// 
			// ThreadsWindow
			// 
			this.ClientSize = new System.Drawing.Size(658, 295);
			this.Controls.Add(this.threadListView);
			this.Controls.Add(this.toolStrip1);
			this.HideOnClose = true;
			this.Name = "ThreadsWindow";
			this.TabText = "Threads";
			this.Text = "Threads";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ColumnHeader columnHeaderThreadID;
		private System.Windows.Forms.ColumnHeader columnHeaderState;
		private System.Windows.Forms.ColumnHeader columnHeaderPeakTime;
		private System.Windows.Forms.ColumnHeader columnHeaderAverageTime;
		private System.Windows.Forms.ColumnHeader columnHeaderLocation;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ColumnHeader columnHeaderStackSize;
		private System.Windows.Forms.ColumnHeader columnHeaderStackUsed;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
        private Tilde.Framework.Controls.DBListView threadListView;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton buttonRefresh;
	}
}
