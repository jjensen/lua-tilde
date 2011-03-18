
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
using System.Reflection;

namespace Tilde.Framework.Controller
{
	public delegate void OptionsChangedDelegate(IOptions sender, string option);

	public class IOptions
	{
		public event OptionsChangedDelegate OptionsChanged;

		public IOptions()
		{

		}

		public void Load(OptionsManager options)
		{
			options.Load(this);
		}

		public void Store(OptionsManager options)
		{
			options.Store(this);
		}

		protected void OnOptionsChanged(string option)
		{
			if (OptionsChanged != null)
				OptionsChanged(this, option);
		}
	}


	public class OptionAttribute : Attribute
	{
		private OptionLocation mLocation = OptionLocation.Registry;
		private string mPath = "";
		private object mDefaultValue = "";

		public OptionAttribute()
		{
		}

		public OptionAttribute(OptionLocation location, string path, string defaultValue)
		{
			mLocation = location;
			mPath = path;
			mDefaultValue = defaultValue;
		}

		public OptionLocation Location
		{
			get { return mLocation; }
			set { mLocation = value; }
		}

		public string Path
		{
			get { return mPath; }
			set { mPath = value; }
		}

		public object DefaultValue
		{
			get { return mDefaultValue; }
			set { mDefaultValue = value; }
		}

		public static OptionAttribute ForType(Type type)
		{
			object[] instances = type.GetCustomAttributes(typeof(OptionAttribute), false);
			if (instances.Length >= 1)
				return (OptionAttribute)instances[0];
			else
				return null;
		}
	}

	public class OptionsCollectionAttribute : Attribute
	{
		public OptionsCollectionAttribute()
		{

		}

		public OptionsCollectionAttribute(string path, Type editor)
		{
			mPath = path;
			mEditor = editor;
		}

		public string Path
		{
			get { return mPath; }
			set { mPath = value; }
		}

		public Type Editor
		{
			get { return mEditor; }
			set { mEditor = value; }
		}

		public static OptionsCollectionAttribute ForType(Type type)
		{
			object[] instances = type.GetCustomAttributes(typeof(OptionsCollectionAttribute), false);
			if (instances.Length >= 1)
				return (OptionsCollectionAttribute)instances[0];
			else
				return null;
		}

		private string mPath;
		private Type mEditor;
	}
}
