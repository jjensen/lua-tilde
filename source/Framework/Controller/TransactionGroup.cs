
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

namespace Tilde.Framework.Controller
{
	public class TransactionGroup : ITransaction
	{
		public TransactionGroup(string label)
		{
			mLabel = label;
			mOperations = new List<ITransaction>();
		}

		public void Add(ITransaction operation)
		{
			mOperations.Add(operation);
		}

		public override void Commit()
		{
			for (int index = 0; index < mOperations.Count; index++)
				mOperations[index].Commit();
		}

		public override void Rollback()
		{
			for (int index = mOperations.Count - 1; index >= 0; index--)
				mOperations[index].Rollback();
		}

		public override string Label
		{
			get { return mLabel; }
		}

		private string mLabel;
		private List<ITransaction> mOperations;

	}
}
