
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

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

using System.Windows.Forms;

using Microsoft.Win32;

using Tilde.Framework.Controller;
using Tilde.Framework.View;
using Tilde.Framework.Controls;

namespace Tilde.TildeApp
{
	[ToolWindowAttribute]
	public class OutputPanel : ToolWindow
	{
		public delegate void ClearLogDelegate(string log);
		public delegate void AddMessageDelegate(string log, string message);
	
		private ClearLogDelegate mClearLogDelegate;
		private AddMessageDelegate mAddMessageDelegate;

		public AddMessageDelegate InvokeAddMessage
		{
			get { return mAddMessageDelegate; }
		}

		public ClearLogDelegate InvokeClearLog
		{
			get { return mClearLogDelegate; }
		}

		private System.Windows.Forms.ComboBox mCombobox;
		private System.Windows.Forms.Panel mLogPanel;
		private System.Windows.Forms.RichTextBox mTemplateLog;
		private System.ComponentModel.IContainer components = null;

		public OutputPanel(IManager manager)
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

			mAddMessageDelegate = new AddMessageDelegate(AddMessage);
			mClearLogDelegate = new ClearLogDelegate(ClearLog);

			mTemplateLog.Parent.Controls.Remove(mTemplateLog);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mCombobox = new System.Windows.Forms.ComboBox();
			this.mLogPanel = new System.Windows.Forms.Panel();
			this.mTemplateLog = new System.Windows.Forms.RichTextBox();
			this.mLogPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mCombobox
			// 
			this.mCombobox.Dock = System.Windows.Forms.DockStyle.Top;
			this.mCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mCombobox.Name = "mCombobox";
			this.mCombobox.Size = new System.Drawing.Size(760, 21);
			this.mCombobox.TabIndex = 2;
			this.mCombobox.SelectedIndexChanged += new System.EventHandler(this.mCombobox_SelectedIndexChanged);
			// 
			// mLogPanel
			// 
			this.mLogPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.mTemplateLog});
			this.mLogPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mLogPanel.Location = new System.Drawing.Point(0, 21);
			this.mLogPanel.Name = "mLogPanel";
			this.mLogPanel.Size = new System.Drawing.Size(760, 201);
			this.mLogPanel.TabIndex = 3;
			// 
			// mTemplateLog
			// 
			this.mTemplateLog.Dock = System.Windows.Forms.DockStyle.Top;
			this.mTemplateLog.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.mTemplateLog.Name = "mTemplateLog";
			this.mTemplateLog.ReadOnly = true;
			this.mTemplateLog.Size = new System.Drawing.Size(760, 222);
			this.mTemplateLog.TabIndex = 2;
			this.mTemplateLog.Text = "";
			this.mTemplateLog.WordWrap = false;
			// 
			// OutputPanel
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(760, 222);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.mLogPanel,
																		  this.mCombobox});
			this.HideOnClose = true;
			this.Name = "OutputPanel";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockBottom;
			this.TabText = "Output";
			this.mLogPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public void AddMessage(string log, string message)
		{
			RichTextBox box = FindLogWindow(log);
            HandleRef hnd = new HandleRef(box, box.Handle);

			Win32.SCROLLINFO scrollinfo = new Win32.SCROLLINFO(Win32.SIF_RANGE | Win32.SIF_PAGE | Win32.SIF_POS);

			if(box.Visible)
                Win32.SendMessage(hnd, Win32.WM_SETREDRAW, Win32.FALSE, 0); 

			// Remember the selection
			int selstart = box.SelectionStart;
			int sellength = box.SelectionLength;

			// Remember where we were before adding the text
			int oldmaxpos, oldpos;
            Win32.GetScrollInfo(hnd, Win32.SB_VERT, ref scrollinfo);
			oldmaxpos = scrollinfo.nMax;
			oldpos = scrollinfo.nPos; 

			// Append the text
			box.AppendText(message);

			// Find out where we are now
			int minpos, maxpos, pagesize;
            Win32.GetScrollInfo(hnd, Win32.SB_VERT, ref scrollinfo);
			minpos = scrollinfo.nMin;
			maxpos = scrollinfo.nMax;
			pagesize = (int) scrollinfo.nPage;

			// Fix the selection (this breaks the scroll position!)
			if(selstart > 0)
				box.SelectionStart = selstart;
			if(sellength > 0)
				box.SelectionLength = sellength;
			
			// Fix the scroll position
			if(oldpos >= oldmaxpos - pagesize * 2)        // Allow two extra lines leeway
				// Only scroll to bottom if we WERE at the max (before text was added)
				//Win32.SendMessage(box.Handle, Win32.EM_SETSCROLLPOS, 0, new Win32.POINT(0, maxpos - pagesize - 1));
                Win32.RichTextBox_SetScrollPos(hnd, 0, maxpos - pagesize - 1);
			else
				// Return to same place we were in when text was added
				//Win32.SendMessage(box.Handle, Win32.EM_SETSCROLLPOS, 0, new Win32.POINT(0, oldpos));
                Win32.RichTextBox_SetScrollPos(hnd, 0, oldpos);

			if(box.Visible)
			{
                Win32.SendMessage(hnd, Win32.WM_SETREDRAW, Win32.TRUE, 0);
				box.Invalidate();
			} 		
		}

		public void ClearLog(string log)
		{
			RichTextBox box = FindLogWindow(log);

			box.Clear();
		}

		public void ShowLog(string log)
		{
			FindLogWindow(log);

			foreach(Control ctrl in mLogPanel.Controls)
			{
				if(ctrl.Name == log)
					ctrl.Show();
				else
					ctrl.Hide();
			}

			// Bring this window to the front
			//Show();
		}

		private RichTextBox FindLogWindow(string type)
		{
			foreach(RichTextBox ctrl in mLogPanel.Controls)
			{
				if(ctrl.Name == type)
					return ctrl;
			}

			RichTextBox box = new RichTextBox();
			box.Name = type;
			box.Font = mTemplateLog.Font;
			box.ReadOnly = true;
			box.WordWrap = false;
			box.Dock = DockStyle.Fill;
			box.LinkClicked += new LinkClickedEventHandler(box_LinkClicked);
			mLogPanel.Controls.Add(box);

            HandleRef hnd = new HandleRef(box, box.Handle);
			Win32.SendMessage(hnd, Win32.EM_SETEVENTMASK, 0, 0); 

			mCombobox.Items.Add(type);
			mCombobox.SelectedItem = type;

			return box;
		}

		void box_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.LinkText);
		}

		private void mCombobox_SelectedIndexChanged(object sender, System.EventArgs e)
		{	
			ShowLog(mCombobox.SelectedItem.ToString());
		}
	}
}

