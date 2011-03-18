
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
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Tilde.CorePlugins.Wizard
{
	public class WizardTabControl : System.Windows.Forms.TabControl
	{
		private bool m_HideTabs = false;

		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		public bool HideTabs
		{
			get { return m_HideTabs; }
			set
			{
				if (m_HideTabs == value) return;
				m_HideTabs = value;
				if (value == true) this.Multiline = true;
				this.UpdateStyles();
			}
		}

		[RefreshProperties(RefreshProperties.All)]
		public new bool Multiline
		{
			get
			{
				if (this.HideTabs) return true;
				return base.Multiline;
			}
			set
			{
				if (this.HideTabs)
					base.Multiline = true;
				else
					base.Multiline = value;
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if ((keyData & (Keys.Tab | Keys.Control)) == (Keys.Tab | Keys.Control))
				return true;

			return base.ProcessCmdKey(ref msg, keyData);
		}

		public override System.Drawing.Rectangle DisplayRectangle
		{
			get
			{
				if (this.HideTabs)
					return new Rectangle(0, 0, Width, Height);
				else
				{
					int tabStripHeight, itemHeight;

					if (this.Alignment <= TabAlignment.Bottom)
						itemHeight = this.ItemSize.Height;
					else
						itemHeight = this.ItemSize.Width;

					if (this.Appearance == TabAppearance.Normal)
						tabStripHeight = 5 + (itemHeight * this.RowCount);
					else
						tabStripHeight = (3 + itemHeight) * this.RowCount;

					switch (this.Alignment)
					{
						case TabAlignment.Bottom:
							return new Rectangle(4, 4, Width - 8, Height - tabStripHeight - 4);
						case TabAlignment.Left:
							return new Rectangle(tabStripHeight, 4, Width - tabStripHeight - 4, Height - 8);
						case TabAlignment.Right:
							return new Rectangle(4, 4, Width - tabStripHeight - 4, Height - 8);
						default:
							return new Rectangle(4, tabStripHeight, Width - 8, Height - tabStripHeight - 4);
					}

				}

			}

		}

	}
}
