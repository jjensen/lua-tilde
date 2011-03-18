
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

using Tilde.Framework.Controller;
using Tilde.Framework.Model;
using Tilde.Framework;

namespace Tilde.TildeApp
{
	public partial class PluginsWindow : Form
	{
		private MainWindow mMainWindow;

		public PluginsWindow(MainWindow mainWindow)
		{
			InitializeComponent();

			mMainWindow = mainWindow;
		}

		private void PluginsWindow_Load(object sender, EventArgs e)
		{
			listViewPlugins.Items.Clear();
			foreach(PluginDetails details in mMainWindow.Manager.Plugins)
			{
				IPlugin plugin = details.Plugin;
				ListViewItem item = new ListViewItem(details.Name);
				item.Checked = plugin != null;
				if (plugin != null)
					item.SubItems.Add(details.Assembly.GetName().Version.ToString());
				else
					item.SubItems.Add("");
				item.Tag = details;
				listViewPlugins.Items.Add(item);
			}
		}

		private void listViewPlugins_SelectedIndexChanged(object sender, EventArgs e)
		{
			richTextBoxDetails.Clear();

			if (listViewPlugins.SelectedItems.Count == 1)
			{
				PluginDetails details = (PluginDetails)listViewPlugins.SelectedItems[0].Tag;
				IPlugin plugin = details.Plugin;
				if (plugin != null)
				{
					richTextBoxDetails.AppendText(details.Assembly.GetName().Name + "\n");
					richTextBoxDetails.AppendText("Documents:\n");
					foreach (Type docType in details.GetImplementations(typeof(Document)))
					{
						DocumentClassAttribute attr = DocumentClassAttribute.ForType(docType);
						richTextBoxDetails.AppendText("\t" + attr.Name + " (" + attr.ViewType.ToString() + ")\n");
						richTextBoxDetails.AppendText("\tFile Extensions:\n");
						foreach (string ext in attr.FileExtensions)
						{
							richTextBoxDetails.AppendText("\t\t" + ext + "\n");
						}
					}
				}
			}
		}

		private void PluginsWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.Hide();
			e.Cancel = true;
		}

		private void listViewPlugins_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			PluginDetails details = (PluginDetails)e.Item.Tag;
			mMainWindow.Manager.OptionsManager.RegistryDatabase.SetBooleanOption("Application/Plugins/" + details.Name, e.Item.Checked);
		}
	}
}