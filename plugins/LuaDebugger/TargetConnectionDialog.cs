
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

namespace Tilde.LuaDebugger
{
	public partial class TargetConnectionDialog : Form
	{
		DebugManager mDebugger;
		HostInfo mSelection;

		public TargetConnectionDialog(DebugManager debugger)
		{
			mDebugger = debugger;
			mSelection = null;

			InitializeComponent();
		}

		public HostInfo Selection
		{
			get { return mSelection; }
		}

		private void RefreshConnections()
		{
			treeViewTargets.Nodes.Clear();
			foreach (ITransport transport in mDebugger.Transports)
			{
				TransportClassAttribute transportAttr = TransportClassAttribute.ForType(transport.GetType());
				TreeNode transportNode = new TreeNode(transportAttr.Name);
				transportNode.Tag = transport;
				foreach(HostInfo hostInfo in transport.EnumerateDevices())
				{
					TreeNode hostNode = new TreeNode(hostInfo.Name);
					hostNode.Tag = hostInfo;
					transportNode.Nodes.Add(hostNode);
				}
				transportNode.ExpandAll();
				treeViewTargets.Nodes.Add(transportNode);
			}
			mSelection = null;
			buttonConnect.Enabled = false;
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			RefreshConnections();
		}

		private void TargetConnectionDialog_Load(object sender, EventArgs e)
		{
			RefreshConnections();
		}

		private void treeViewTargets_DoubleClick(object sender, EventArgs e)
		{
			if (treeViewTargets.SelectedNode != null && treeViewTargets.SelectedNode.Tag != null && treeViewTargets.SelectedNode.Tag is HostInfo)
			{
				DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void treeViewTargets_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (treeViewTargets.SelectedNode != null && treeViewTargets.SelectedNode.Tag != null && treeViewTargets.SelectedNode.Tag is HostInfo)
			{
				mSelection = (HostInfo)treeViewTargets.SelectedNode.Tag;
			}
			else
			{
				mSelection = null;
			}
			buttonConnect.Enabled = mSelection != null;
		}
	}
}