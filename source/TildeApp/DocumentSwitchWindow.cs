
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tilde.Framework.View;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using Tilde.Framework.Controls;

namespace Tilde.TildeApp
{
	public partial class DocumentSwitchWindow : Form
	{
		MainWindow mMainWindow;
		DockContent mSelectedContent;

		public struct ButtonSettings
		{
			public Brush mLabelBrush;
			public StringFormat mLabelFormat;
		}

		public class FileButton : Button
		{
			ButtonSettings mButtonSettings;

			public FileButton(ButtonSettings settings)
			{
				mButtonSettings = settings;
				this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
			}

			protected override void OnPaintBackground(PaintEventArgs pevent)
			{
//				base.OnPaintBackground(pevent);
			}

			protected override void OnPaint(PaintEventArgs pevent)
			{
				RectangleF rect = new RectangleF(this.DisplayRectangle.Left + this.Padding.Left, this.DisplayRectangle.Top + this.Padding.Top, this.DisplayRectangle.Width - this.Padding.Horizontal - 1, this.DisplayRectangle.Height - this.Padding.Vertical - 1);
				if (this.Focused)
				{
					pevent.Graphics.Clear(Color.LightSteelBlue);
					pevent.Graphics.DrawRectangle(SystemPens.WindowFrame, Rectangle.Round(rect));
				}
				else
				{
					pevent.Graphics.Clear(SystemColors.Control);
				}
				pevent.Graphics.DrawString(this.Text, this.Font, mButtonSettings.mLabelBrush, rect, mButtonSettings.mLabelFormat);
			}
		}

		ButtonSettings mButtonSettings;

		public DocumentSwitchWindow(MainWindow window)
		{
			InitializeComponent();

			mMainWindow = window;
			mSelectedContent = null;

			//SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

			mButtonSettings = new ButtonSettings();
			mButtonSettings.mLabelBrush = new SolidBrush(Button.DefaultForeColor);

			mButtonSettings.mLabelFormat = new StringFormat(StringFormatFlags.NoWrap);
			mButtonSettings.mLabelFormat.Trimming = StringTrimming.EllipsisCharacter;
			mButtonSettings.mLabelFormat.Alignment = StringAlignment.Near;
			mButtonSettings.mLabelFormat.LineAlignment = StringAlignment.Center;
		}

		public DockContent SelectedContent
		{
			get { return mSelectedContent; }
		}

		private void DocumentSwitchWindow_KeyDown(object sender, KeyEventArgs e)
		{
			System.Diagnostics.Debug.Print("KeyCode=[" + e.KeyData.ToString() + "] KeyData=[" + e.KeyData.ToString() + "] KeyCode=[" + e.KeyCode.ToString() + "]");
			if (e.KeyCode == Keys.Tab)
			{
				/*
				Control next = this.GetNextControl(Control.FromHandle(Win32.GetFocus()), !e.Shift);

				if (next == null && panelActiveFiles.Controls.Count > 0)
					next = panelActiveFiles.Controls[e.Shift ? panelActiveFiles.Controls.Count - 1 : 0];

				if(next != null)
					next.Select();
				*/

				Control focusControl;
				if (this.ActiveControl.Parent == panelActiveFiles)
					focusControl = panelActiveFiles;
				else if (this.ActiveControl.Parent == panelActiveToolWindows)
					focusControl = panelActiveToolWindows;
				else
					focusControl = this;

				focusControl.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
				e.Handled = true;
			}
		}

		private void DocumentSwitchWindow_KeyUp(object sender, KeyEventArgs e)
		{
			if (!e.Control)
			{
				Control focused = Control.FromHandle(Win32.GetFocus());
				this.DialogResult = DialogResult.OK;
				if (focused is Button)
				{
					DockContent content = (focused as Button).Tag as DockContent;
					ActivateDockContent(content);
				}
				e.Handled = true;
			}
		}

		public void UpdateWindows()
		{
			int docIndex = 0;
			int toolIndex = 0;
			List<Button> fileButtons = new List<Button>();
			List<Button> toolButtons = new List<Button>();
			foreach (IDockContent icontent in mMainWindow.DockContentHistory)
			{
				DockContent content = icontent as DockContent;
				if (content != null)
				{
					Button button = new FileButton(mButtonSettings);
					button.Tag = content;
					button.Text = content.TabText;
					button.TextAlign = ContentAlignment.MiddleLeft;
					button.FlatStyle = FlatStyle.Flat;
					button.AutoEllipsis = true;
					button.Margin = new Padding(1);
					button.Width = 160;
					button.Height -= 6;
					button.Enter += new EventHandler(FileButton_Enter);
					button.Leave += new EventHandler(FileButton_Leave);
					button.MouseClick += new MouseEventHandler(FileButton_MouseClick);

					if (content.DockState == DockState.Document)
					{
						button.TabIndex = docIndex++;
						fileButtons.Add(button);
					}
					else
					{
						button.TabIndex = toolIndex++;
						toolButtons.Add(button);
					}
				}
			}

			this.SuspendLayout();

			panelActiveFiles.SuspendLayout();
			panelActiveFiles.Controls.Clear();
			panelActiveFiles.Controls.AddRange(fileButtons.ToArray());
			panelActiveFiles.ResumeLayout();

			panelActiveToolWindows.SuspendLayout();
			panelActiveToolWindows.Controls.Clear();
			panelActiveToolWindows.Controls.AddRange(toolButtons.ToArray());
			panelActiveToolWindows.ResumeLayout();

			this.ResumeLayout();

			if (panelActiveFiles.Controls.Count > 1)
				panelActiveFiles.Controls[1].Select();
			else if (panelActiveFiles.Controls.Count > 0)
				panelActiveFiles.Controls[0].Select();
		}

		void FileButton_Enter(object sender, EventArgs e)
		{
			DockContent content = (sender as Button).Tag as DockContent;
			DocumentView view = (sender as Button).Tag as DocumentView;
			labelDocumentName.Text = view != null ? Path.GetFileName(view.Document.FileName) : content.TabText;
			labelDocumentPath.Text = view != null ? view.Document.FileName : "";
			labelDocumentType.Text = view != null ? view.GetType().ToString() : "";
		}

		void FileButton_Leave(object sender, EventArgs e)
		{
			labelDocumentName.Text = "";
			labelDocumentPath.Text = "";
			labelDocumentType.Text = "";
		}

		void FileButton_MouseClick(object sender, MouseEventArgs e)
		{
			DockContent content = (sender as Button).Tag as DockContent;
			ActivateDockContent(content);
		}

		void ActivateDockContent(DockContent content)
		{
			mSelectedContent = content;
			this.DialogResult = DialogResult.OK;
		}
	}
}