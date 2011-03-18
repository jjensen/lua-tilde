
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

namespace Tilde.CorePlugins.Wizard
{
	public partial class WizardForm : Form
	{
		TabPage m_previousPage;
		TabPage m_nextPage;
		bool m_canComplete = false;
		bool m_canCancel = true;

		public WizardForm()
		{
			InitializeComponent();
		}

		protected TabPage PreviousPage
		{
			get { return m_previousPage; }
			set { m_previousPage = value; UpdateStatus(); }
		}

		protected TabPage NextPage
		{
			get { return m_nextPage; }
			set { m_nextPage = value; UpdateStatus(); }
		}

		protected bool CanComplete
		{
			get { return m_canComplete; }
			set { m_canComplete = value; UpdateStatus(); }
		}

		protected bool CanCancel
		{
			get { return m_canCancel; }
			set { m_canCancel = value; UpdateStatus(); }
		}

		protected virtual bool OnPageChanging(TabPage currPage, TabPage nextPage)
		{
			return true;
		}

		protected virtual void OnPageChanged(TabPage currPage)
		{

		}

		protected virtual bool OnCancel()
		{
			return true;
		}

		private void WizardForm_Load(object sender, EventArgs e)
		{
			m_previousPage = null;
			m_nextPage = null;
			tabControlPages.SelectedIndex = 0;
			OnPageChanging(null, tabControlPages.SelectedTab);
			OnPageChanged(tabControlPages.SelectedTab);
			UpdateStatus();
		}

		private void tabControlPages_SelectedIndexChanged(object sender, EventArgs e)
		{
			OnPageChanged(tabControlPages.SelectedTab);
			UpdateStatus();
		}

		private void UpdateStatus()
		{
			buttonPrevious.Enabled = m_previousPage != null;
			buttonNext.Enabled = m_nextPage != null;
			buttonFinish.Enabled = m_canComplete;
			buttonCancel.Enabled = m_canCancel;

			labelHeading.Text = tabControlPages.SelectedTab == null ? "" : tabControlPages.SelectedTab.Text;
		}

		private void buttonPrevious_Click(object sender, EventArgs e)
		{
			if (m_previousPage != null)
			{
				if(OnPageChanging(tabControlPages.SelectedTab, m_previousPage))
					tabControlPages.SelectedTab = m_previousPage;
			}
		}

		private void buttonNext_Click(object sender, EventArgs e)
		{
			if (m_nextPage != null)
			{
				if(OnPageChanging(tabControlPages.SelectedTab, m_nextPage))
					tabControlPages.SelectedTab = m_nextPage;
			}
		}

		private void buttonFinish_Click(object sender, EventArgs e)
		{

		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			if (OnCancel())
				this.Close();
		}
	}
}