
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
	public partial class SourceControlWindow : Form
	{
		private IManager mManager;
		private IVersionController mVCS;

		struct VCSInfo
		{
			Type mType;

			public VCSInfo(Type type)
			{
				mType = type;
			}

			public Type Type
			{
				get { return mType; }
			}

			public override string ToString()
			{
				if (mType == null)
					return "None";
				else
					return ((DescriptionAttribute)mType.GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description;
			}
		}

		public SourceControlWindow(IManager manager)
		{
			InitializeComponent();

			mManager = manager;
			if(mManager.Project.VCS != null)
				mVCS = (IVersionController) mManager.Project.VCS.Clone();
			propertyGrid.SelectedObject = mVCS;

			comboBoxVCSType.Items.Add(new VCSInfo(null));
			if (mVCS == null)
				comboBoxVCSType.SelectedIndex = 0;
			foreach (Type type in mManager.GetPluginImplementations(typeof(IVersionController)))
			{
				VCSInfo info = new VCSInfo(type);
				comboBoxVCSType.Items.Add(info);
				if (mVCS != null && mVCS.GetType() == type)
					comboBoxVCSType.SelectedItem = info;
			}

			UpdateControls();
		}

		private void UpdateControls()
		{
			VCSInfo info = (VCSInfo)comboBoxVCSType.SelectedItem;
			if (info.Type == null)
				mVCS = null;
			else
				mVCS = (IVersionController)Activator.CreateInstance(info.Type, new object[] { });
			propertyGrid.SelectedObject = mVCS;

			if (mVCS == null)
			{
				labelVCSInfo.Text = "";
				labelVCSInfo.Visible = false;
				propertyGrid.Visible = false;
			}
			else
			{
				labelVCSInfo.Text = mVCS.ConfigurationMessage;
				labelVCSInfo.Visible = true;
				propertyGrid.Visible = true;
			}
		}

		private void comboBoxVCSType_SelectionChangeCommitted(object sender, EventArgs e)
		{
			UpdateControls();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			mManager.Project.VCS = mVCS;
		}
	}
}