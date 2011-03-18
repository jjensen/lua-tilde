
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
using Microsoft.Win32;

namespace Tilde.Framework.Controller
{
	public class RegistryOptionsDatabase : IOptionsDatabase
	{
		private RegistryKey mRegistryRoot;

		public RegistryOptionsDatabase()
		{
			mRegistryRoot = Registry.CurrentUser.CreateSubKey("Software\\Tantalus\\Tilde");
		}

		public bool SetBooleanOption(string path, bool value, bool validateOnly)
		{
			if(validateOnly)
				return true;
			else
				return SetOption(path, value.ToString(), RegistryValueKind.String);
		}

		public bool SetBooleanOption(string path, bool value)
		{
			return SetOption(path, value.ToString(), RegistryValueKind.String);
		}

		public bool SetIntegerOption(string path, int value, bool validateOnly)
		{
			if (validateOnly)
				return true;
			else
				return SetOption(path, value, RegistryValueKind.DWord);
		}

		public bool SetIntegerOption(string path, int value)
		{
			return SetOption(path, value, RegistryValueKind.DWord);
		}

		public bool SetStringOption(string path, string value, bool validateOnly)
		{
			if (validateOnly)
				return true;
			else
				return SetOption(path, value, RegistryValueKind.String);
		}

		public bool SetStringOption(string path, string value)
		{
			return SetOption(path, value, RegistryValueKind.String);
		}

		public bool SetStringArrayOption(string path, string[] value, bool validateOnly)
		{
			if (validateOnly)
				return true;
			else
				return SetOption(path, value, RegistryValueKind.MultiString);
		}

		public bool SetStringArrayOption(string path, string[] value)
		{
			return SetOption(path, value, RegistryValueKind.MultiString);
		}


		public bool GetBooleanOption(string path, bool defaultval)
		{
			object result = GetOption(path);
			if (result != null)
				return Boolean.Parse(result.ToString());
			else
				return defaultval;
		}

		public int GetIntegerOption(string path, int defaultval)
		{
			object result = GetOption(path);
			if (result == null)
				return defaultval;
			else if (result.GetType() == typeof(string))
				return Int32.Parse(result.ToString());
			else
				return (int)result;
		}

		public string GetStringOption(string path, string defaultval)
		{
			object result = GetOption(path);
			return result == null ? defaultval : result.ToString();
		}

		public string [] GetStringArrayOption(string path, string [] defaultval)
		{
			object result = GetOption(path);
			return result == null || result.GetType() != typeof(string []) ? defaultval : (string []) result;
		}


		private bool SetOption(string path, object value, RegistryValueKind kind)
		{
			string[] pathsplit = path.Split(new char[] { '/' });
			string[] keys = new string[pathsplit.Length - 1];
			Array.Copy(pathsplit, keys, keys.Length);
			string name = pathsplit[pathsplit.Length - 1];

			RegistryKey node = mRegistryRoot;
			foreach (string key in keys)
			{
				node = node.CreateSubKey(key);
			}
			node.SetValue(name, value, kind);
			return true;
		}

		private object GetOption(string path)
		{
			string[] pathsplit = path.Split(new char[] { '/' });
			string[] keys = new string[pathsplit.Length - 1];
			Array.Copy(pathsplit, keys, keys.Length);
			string name = pathsplit[pathsplit.Length - 1];

			RegistryKey node = mRegistryRoot;
			foreach (string key in keys)
			{
				node = node.OpenSubKey(key);
				if (node == null)
					return null;
			}
			return node.GetValue(name);
		}


	}
}
