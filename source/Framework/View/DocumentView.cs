
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
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

using Tilde.Framework.Controller;
using Tilde.Framework.Model;

namespace Tilde.Framework.View
{
	public delegate void DocumentViewClosedEventHandler(DocumentView sender);

	public partial class DocumentView : WeifenLuo.WinFormsUI.Docking.DockContent
	{
		public DocumentView()
		{
			InitializeComponent();
		}

		public DocumentView(IManager manager, Document doc)
		{
			mManager = manager;
			mDocument = doc;

			InitializeComponent();

			this.TabText = String.Format("{0}{1}", Path.GetFileName(Document.FileName), Document.Modified ? " *" : "");
			this.Text = Path.GetFileName(doc.FileName);
			this.ToolTipText = doc.FileName;
			doc.PropertyChange += new PropertyChangeEventHandler(Document_PropertyChange);
			doc.Closed += new DocumentClosedEventHandler(Document_Closed);
		}

		public IManager Manager
		{
			get { return mManager; }
		}

		public Document Document
		{
			get { return mDocument; }
		}

		protected override string GetPersistString()
		{
			return Document.FileName;
		}

		void Document_PropertyChange(object sender, PropertyChangeEventArgs args)
		{
			if (args.Property == "FileName" || args.Property == "Modified")
			{
				TabText = String.Format("{0}{1}", Path.GetFileName(Document.FileName), Document.Modified ? " *" : "");
				ToolTipText = Document.FileName;
			}
		}

		void Document_Closed(Document sender)
		{
			Close();
		}

		private IManager mManager;
		private Document mDocument;


		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mDocument.SaveDocument();
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void tabMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			saveToolStripMenuItem.Enabled = !mDocument.ReadOnly;
		}

		private void openContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process process = new Process();
			process.StartInfo.FileName = Path.GetDirectoryName(mDocument.FileName);
			process.StartInfo.Verb = "Open";
			process.Start();
		}

		private void copyFullPathToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(mDocument.FileName);
		}

		private void DocumentView_Activated(object sender, EventArgs e)
		{
		}

		private void DocumentView_Enter(object sender, EventArgs e)
		{
            FocusReceived();
		}
        protected virtual void FocusReceived()
        {
            mManager.SelectedObject = null;
        }

	}
}

