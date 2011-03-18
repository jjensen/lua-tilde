
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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Tilde.CorePlugins.CanvasControl
{
	public partial class Canvas : UserControl
	{
		private LayerCollection mLayers;

		public Canvas()
		{
			InitializeComponent();

			mLayers = new LayerCollection(this);

			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);
		}

		public LayerCollection Layers
		{
			get { return mLayers; }
		}

		protected override void OnPaint(PaintEventArgs eventArgs)
		{
			foreach(Layer layer in mLayers)
			{
				if(layer.Visible)
				{
					foreach(CanvasItem item in layer.Items)
					{
						item.Paint(eventArgs.Graphics);
					}
				}
			}

			base.OnPaint(eventArgs);
		}

		protected override void OnPaintBackground(PaintEventArgs eventArgs)
		{
			eventArgs.Graphics.Clear(this.BackColor);

			base.OnPaintBackground(eventArgs);
		}
	}
}
