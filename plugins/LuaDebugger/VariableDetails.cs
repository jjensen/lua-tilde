
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

namespace Tilde.LuaDebugger
{
	public class VariableDetails
	{
		public VariableDetails(int watchid, LuaValue[] path, LuaValue key, LuaValue value, bool expanded, bool hasEntries, bool hasMetadata, bool valid, VariableClass varclass)
		{
			mWatchID = watchid;
			mPath = path;
			mKey = key;
			mValue = value;
			mExpanded = expanded;
			mHasEntries = hasEntries;
			mHasMetadata = hasMetadata;
			mValid = valid;
			mClass = varclass;
		}

		public int WatchID
		{
			get { return mWatchID; }
		}

		public LuaValue[] Path
		{
			get { return mPath; }
		}

		public LuaValue Key
		{
			get { return mKey; }
		}

		public LuaValue Value
		{
			get { return mValue; }
		}

		public bool Expanded
		{
			get { return mExpanded; }
		}

		public bool HasEntries
		{
			get { return mHasEntries; }
		}

		public bool HasMetadata
		{
			get { return mHasMetadata; }
		}

		public bool Valid
		{
			get { return mValid; }
		}

		public VariableClass VariableClass
		{
			get { return mClass; }
		}

		public LuaValue [] MakePath()
		{
			LuaValue[] result = new LuaValue[mPath.Length + 1];
			Array.Copy(mPath, result, mPath.Length);
			result[mPath.Length] = mKey;
			return result;
		}

		public override string ToString()
		{
			if(mWatchID == 0)
			{
				if (mPath.Length > 0)
					return String.Join(".", Array.ConvertAll<LuaValue, string>(mPath, delegate(LuaValue value) { return value.ToString(); })) + "." + mKey.ToString();
				else
					return mKey.ToString();
			}
			else
			{
				// Replace the first element in the path with the watchID
				if (mPath.Length > 1)
					return "[" + mWatchID.ToString() + "]." + String.Join(".", Array.ConvertAll<LuaValue, string>(mPath, delegate(LuaValue value) { return value.ToString(); }), 1, mPath.Length - 1) + "." + mKey.ToString();
				else if (mPath.Length == 1)
					return "[" + mWatchID.ToString() + "]." + mKey.ToString();
				else
					return "[" + mWatchID.ToString() + "]";
			}
		}

		private int mWatchID;
		private LuaValue [] mPath;
		private LuaValue mKey;
		private LuaValue mValue;
		private bool mExpanded;
		private bool mHasEntries;
		private bool mHasMetadata;
		private bool mValid;
		private VariableClass mClass;
	}

}
