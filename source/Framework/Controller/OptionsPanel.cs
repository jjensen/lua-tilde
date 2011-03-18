
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

namespace Tilde.Framework.Controller
{
	public partial class OptionsPanel : UserControl
	{
		protected IManager mManager;

		protected IOptions mCurrentOptions;
		protected IOptions mTempOptions;


		public OptionsPanel()
			: this(null, null)
		{
		}

		public OptionsPanel(IManager manager, IOptions options)
		{
			InitializeComponent();

			mManager = manager;

			mCurrentOptions = options;
			mTempOptions = options == null ? null : (IOptions)options.GetType().GetConstructor(new Type[] { }).Invoke(new object[] { });
		}

		public IManager Manager
		{
			get { return mManager; }
		}

		public virtual void OpenOptions()
		{
			Manager.OptionsManager.Copy(mCurrentOptions, mTempOptions, false);
		}

		public virtual void CancelOptions()
		{

		}

		public virtual bool ValidateOptions()
		{
			return Manager.OptionsManager.Validate(mTempOptions, mCurrentOptions);
		}

		public virtual bool AcceptOptions()
		{
			return Manager.OptionsManager.Copy(mTempOptions, mCurrentOptions, true);
//			Manager.OptionsManager.Store(mCurrentOptions);
		}
	}
}