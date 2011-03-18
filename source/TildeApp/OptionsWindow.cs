
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
using System.Reflection;

using Tilde.Framework.Controller;

namespace Tilde.TildeApp
{
	public partial class OptionsWindow : Form
	{
		private MainWindow mMainWindow;
		private OptionsPanel mCurrentPanel;
		private List<OptionsPanel> mOptionsPanels;

		public OptionsWindow(MainWindow mainWindow)
		{
			InitializeComponent();

			mMainWindow = mainWindow;
			mCurrentPanel = null;
			mOptionsPanels = new List<OptionsPanel>();

			foreach(IOptions options in mMainWindow.Manager.OptionsManager.Options)
			{
				OptionsCollectionAttribute attr = OptionsCollectionAttribute.ForType(options.GetType());
				OptionsPanel panel = Activator.CreateInstance(attr.Editor, new object[] { mMainWindow.Manager, options }) as OptionsPanel;

				AddPanel(panel, attr.Path);
			}
		}

		private void AddPanel(OptionsPanel panel, string panelPath)
		{
			mOptionsPanels.Add(panel);

			string[] fullpath = panelPath.Split(new char[] { '/' });
			string [] path = new string[fullpath.Length - 1];
			Array.Copy(fullpath, path, path.Length);
			string name = fullpath[fullpath.Length - 1];

			TreeNode node = new TreeNode(name);
			node.Name = panelPath;
			node.Tag = panel;

			TreeNode root = null;
			foreach(string folder in path)
			{
				if(root == null)
				{
					if(pagesTree.Nodes.ContainsKey(folder))
						root = pagesTree.Nodes[folder];
					else
						root = pagesTree.Nodes.Add(folder, folder);
				}
				else
				{
					if(root.Nodes.ContainsKey(folder))
						root = root.Nodes[folder];
					else
						root = root.Nodes.Add(folder, folder);
				}
			}

			if (root == null)
			{
				pagesTree.Nodes.Add(node);
			}
			else
			{
				root.Nodes.Add(node);
			}
		}

		private void ShowPanel(OptionsPanel panel)
		{
			if(mCurrentPanel != null)
				optionsPanel.Controls.Remove(mCurrentPanel);

			mCurrentPanel = panel;

			if(mCurrentPanel != null)
			{
				optionsPanel.Controls.Add(mCurrentPanel);
				mCurrentPanel.Dock = DockStyle.Fill;
				mCurrentPanel.Show();
			}

			optionsPanel.PerformLayout();
		}

		private void pagesTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode node = e.Node;
			while (node != null && !(node.Tag is OptionsPanel))
				node = node.FirstNode;

			if (node != null && node.Tag is OptionsPanel)
				ShowPanel((OptionsPanel) node.Tag);
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			foreach (OptionsPanel panel in mOptionsPanels)
				panel.CancelOptions();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			foreach (OptionsPanel panel in mOptionsPanels)
			{
				if (!panel.ValidateOptions())
					return;
			}
			foreach (OptionsPanel panel in mOptionsPanels)
			{
				panel.AcceptOptions();
			}
			this.DialogResult = DialogResult.OK;
		}

		private void OptionsWindow_Shown(object sender, EventArgs e)
		{
			foreach (OptionsPanel panel in mOptionsPanels)
				panel.OpenOptions();

			pagesTree.ExpandAll();
//			pagesTree.SelectedNode = pagesTree.Nodes[0];
		}
	}
}