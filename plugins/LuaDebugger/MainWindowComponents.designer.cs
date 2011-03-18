
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
	partial class MainWindowComponents
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindowComponents));
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.reconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.buildAndRestartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cancelBuildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stepIntoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stepOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.breakToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.targetStateLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.btnConnect = new System.Windows.Forms.ToolStripButton();
			this.btnDisconnect = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.btnRun = new System.Windows.Forms.ToolStripButton();
			this.btnBreak = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.btnStartProfile = new System.Windows.Forms.ToolStripButton();
			this.btnStopProfile = new System.Windows.Forms.ToolStripButton();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.notifyContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.notifyContextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(342, 24);
			this.menuStrip.TabIndex = 2;
			this.menuStrip.Text = "menuStrip1";
			// 
			// debugToolStripMenuItem
			// 
			this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem,
            this.reconnectToolStripMenuItem,
            this.toolStripSeparator2,
            this.buildToolStripMenuItem,
            this.buildAndRestartToolStripMenuItem,
            this.cancelBuildToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripSeparator1,
            this.runToolStripMenuItem,
            this.stepToolStripMenuItem,
            this.stepIntoToolStripMenuItem,
            this.stepOutToolStripMenuItem,
            this.breakToolStripMenuItem});
			this.debugToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.debugToolStripMenuItem.MergeIndex = 4;
			this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
			this.debugToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
			this.debugToolStripMenuItem.Text = "Debug";
			// 
			// connectToolStripMenuItem
			// 
			this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
			this.connectToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.connectToolStripMenuItem.Text = "Connect...";
			this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
			// 
			// disconnectToolStripMenuItem
			// 
			this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
			this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.disconnectToolStripMenuItem.Text = "Disconnect";
			this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
			// 
			// reconnectToolStripMenuItem
			// 
			this.reconnectToolStripMenuItem.Name = "reconnectToolStripMenuItem";
			this.reconnectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
			this.reconnectToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.reconnectToolStripMenuItem.Text = "Reconnect";
			this.reconnectToolStripMenuItem.Click += new System.EventHandler(this.reconnectToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(197, 6);
			// 
			// buildToolStripMenuItem
			// 
			this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
			this.buildToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
			this.buildToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.buildToolStripMenuItem.Text = "Build";
			this.buildToolStripMenuItem.Click += new System.EventHandler(this.buildToolStripMenuItem_Click);
			// 
			// buildAndRestartToolStripMenuItem
			// 
			this.buildAndRestartToolStripMenuItem.Name = "buildAndRestartToolStripMenuItem";
			this.buildAndRestartToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F7)));
			this.buildAndRestartToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.buildAndRestartToolStripMenuItem.Text = "Build and Restart";
			this.buildAndRestartToolStripMenuItem.Click += new System.EventHandler(this.buildAndRestartToolStripMenuItem_Click);
			// 
			// cancelBuildToolStripMenuItem
			// 
			this.cancelBuildToolStripMenuItem.Name = "cancelBuildToolStripMenuItem";
			this.cancelBuildToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Break";
			this.cancelBuildToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Pause)));
			this.cancelBuildToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.cancelBuildToolStripMenuItem.Text = "Cancel Build";
			this.cancelBuildToolStripMenuItem.Click += new System.EventHandler(this.cancelBuildToolStripMenuItem_Click);
			// 
			// stopToolStripMenuItem
			// 
			this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
			this.stopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5)));
			this.stopToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.stopToolStripMenuItem.Text = "Stop";
			this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(197, 6);
			// 
			// runToolStripMenuItem
			// 
			this.runToolStripMenuItem.Name = "runToolStripMenuItem";
			this.runToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.runToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.runToolStripMenuItem.Text = "Run";
			this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
			// 
			// stepToolStripMenuItem
			// 
			this.stepToolStripMenuItem.Name = "stepToolStripMenuItem";
			this.stepToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
			this.stepToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.stepToolStripMenuItem.Text = "Step";
			this.stepToolStripMenuItem.Click += new System.EventHandler(this.stepToolStripMenuItem_Click);
			// 
			// stepIntoToolStripMenuItem
			// 
			this.stepIntoToolStripMenuItem.Name = "stepIntoToolStripMenuItem";
			this.stepIntoToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
			this.stepIntoToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.stepIntoToolStripMenuItem.Text = "Step Into";
			this.stepIntoToolStripMenuItem.Click += new System.EventHandler(this.stepIntoToolStripMenuItem_Click);
			// 
			// stepOutToolStripMenuItem
			// 
			this.stepOutToolStripMenuItem.Name = "stepOutToolStripMenuItem";
			this.stepOutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F11)));
			this.stepOutToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.stepOutToolStripMenuItem.Text = "Step Out";
			this.stepOutToolStripMenuItem.Click += new System.EventHandler(this.stepOutToolStripMenuItem_Click);
			// 
			// breakToolStripMenuItem
			// 
			this.breakToolStripMenuItem.Name = "breakToolStripMenuItem";
			this.breakToolStripMenuItem.ShortcutKeyDisplayString = "Alt+Break";
			this.breakToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Pause)));
			this.breakToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.breakToolStripMenuItem.Text = "Break";
			this.breakToolStripMenuItem.Click += new System.EventHandler(this.breakToolStripMenuItem_Click);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.targetStateLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 226);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(342, 22);
			this.statusStrip.TabIndex = 3;
			this.statusStrip.Text = "statusStrip";
			// 
			// targetStateLabel
			// 
			this.targetStateLabel.Name = "targetStateLabel";
			this.targetStateLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConnect,
            this.btnDisconnect,
            this.toolStripSeparator3,
            this.btnRun,
            this.btnBreak,
            this.toolStripSeparator4,
            this.btnStartProfile,
            this.btnStopProfile});
			this.toolStrip.Location = new System.Drawing.Point(0, 24);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(342, 25);
			this.toolStrip.TabIndex = 4;
			this.toolStrip.Text = "toolStrip1";
			// 
			// btnConnect
			// 
			this.btnConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnConnect.Image = global::Tilde.LuaDebugger.Properties.Resources.Connect;
			this.btnConnect.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(23, 22);
			this.btnConnect.Text = "Connect";
			this.btnConnect.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
			// 
			// btnDisconnect
			// 
			this.btnDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnDisconnect.Image = global::Tilde.LuaDebugger.Properties.Resources.Disconnect;
			this.btnDisconnect.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.btnDisconnect.Name = "btnDisconnect";
			this.btnDisconnect.Size = new System.Drawing.Size(23, 22);
			this.btnDisconnect.Text = "Disconnect";
			this.btnDisconnect.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// btnRun
			// 
			this.btnRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnRun.Image = global::Tilde.LuaDebugger.Properties.Resources.Run;
			this.btnRun.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.btnRun.Name = "btnRun";
			this.btnRun.Size = new System.Drawing.Size(23, 22);
			this.btnRun.Text = "Run";
			this.btnRun.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
			// 
			// btnBreak
			// 
			this.btnBreak.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnBreak.Image = ((System.Drawing.Image)(resources.GetObject("btnBreak.Image")));
			this.btnBreak.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.btnBreak.Name = "btnBreak";
			this.btnBreak.Size = new System.Drawing.Size(23, 22);
			this.btnBreak.Text = "Break";
			this.btnBreak.Click += new System.EventHandler(this.breakToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// btnStartProfile
			// 
			this.btnStartProfile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnStartProfile.Image = global::Tilde.LuaDebugger.Properties.Resources.StartProfile;
			this.btnStartProfile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnStartProfile.Name = "btnStartProfile";
			this.btnStartProfile.Size = new System.Drawing.Size(68, 22);
			this.btnStartProfile.Text = "Start Profile";
			this.btnStartProfile.Click += new System.EventHandler(this.btnStartProfile_Click);
			// 
			// btnStopProfile
			// 
			this.btnStopProfile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnStopProfile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnStopProfile.Name = "btnStopProfile";
			this.btnStopProfile.Size = new System.Drawing.Size(66, 22);
			this.btnStopProfile.Text = "Stop Profile";
			this.btnStopProfile.Click += new System.EventHandler(this.btnStopProfile_Click);
			// 
			// notifyIcon
			// 
			this.notifyIcon.Text = "notifyIcon1";
			this.notifyIcon.Visible = true;
			this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.notifyIcon_BalloonTipClicked);
			this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
			// 
			// notifyContextMenuStrip
			// 
			this.notifyContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
			this.notifyContextMenuStrip.Name = "notifyContextMenuStrip";
			this.notifyContextMenuStrip.Size = new System.Drawing.Size(170, 92);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(169, 22);
			this.toolStripMenuItem1.Text = "Connect...";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(169, 22);
			this.toolStripMenuItem2.Text = "Disconnect";
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
			this.toolStripMenuItem3.Size = new System.Drawing.Size(169, 22);
			this.toolStripMenuItem3.Text = "Reconnect";
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Pause)));
			this.toolStripMenuItem4.Size = new System.Drawing.Size(169, 22);
			this.toolStripMenuItem4.Text = "Break";
			// 
			// MainWindowComponents
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.menuStrip);
			this.Name = "MainWindowComponents";
			this.Size = new System.Drawing.Size(342, 248);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.notifyContextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stepToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stepIntoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem breakToolStripMenuItem;
		public System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stepOutToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		public System.Windows.Forms.StatusStrip statusStrip;
		public System.Windows.Forms.ToolStripStatusLabel targetStateLabel;
		private System.Windows.Forms.ToolStripMenuItem reconnectToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton btnStartProfile;
		private System.Windows.Forms.ToolStripButton btnStopProfile;
		private System.Windows.Forms.ToolStripButton btnConnect;
		private System.Windows.Forms.ToolStripButton btnDisconnect;
		public System.Windows.Forms.ToolStrip toolStrip;
		internal System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip notifyContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem buildAndRestartToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton btnRun;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton btnBreak;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem cancelBuildToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem;
	}
}
