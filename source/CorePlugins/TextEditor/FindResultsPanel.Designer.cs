
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

namespace Tilde.CorePlugins
{
	partial class FindResultsPanel
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
			this.outputBrowser = new Tilde.Framework.Controls.MyWebBrowser();
			this.SuspendLayout();
			// 
			// outputBrowser
			// 
			this.outputBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.outputBrowser.IsWebBrowserContextMenuEnabled = false;
			this.outputBrowser.Location = new System.Drawing.Point(0, 0);
			this.outputBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.outputBrowser.Name = "outputBrowser";
			this.outputBrowser.Size = new System.Drawing.Size(292, 273);
			this.outputBrowser.TabIndex = 0;
			this.outputBrowser.Url = new System.Uri("", System.UriKind.Relative);
			this.outputBrowser.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.outputBrowser_PreviewKeyDown);
			// 
			// FindResultsPanel
			// 
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.outputBrowser);
			this.Name = "FindResultsPanel";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
			this.TabText = "Find Results";
			this.Text = "Find Results";
			this.Load += new System.EventHandler(this.FindResultsPanel_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.WebBrowser outputBrowser;

	}
}
