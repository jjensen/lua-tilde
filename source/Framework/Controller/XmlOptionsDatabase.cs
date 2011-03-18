
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
using System.Xml;

namespace Tilde.Framework.Controller
{
	public delegate bool XmlOptionsDatabaseCanModifyHandler(XmlOptionsDatabase sender, string path);
	public delegate void XmlOptionsDatabaseModifiedHandler(XmlOptionsDatabase sender, string path);

	public class XmlOptionsDatabase : IOptionsDatabase
	{
		XmlElement mDatabaseRoot;

		public XmlOptionsDatabase(XmlElement root)
		{
			mDatabaseRoot = root;
		}

		public event XmlOptionsDatabaseCanModifyHandler CanModify;
		public event XmlOptionsDatabaseModifiedHandler Modified;

		public bool SetBooleanOption(string path, bool value)
		{
			return SetBooleanOption(path, value, false);
		}

		public bool SetBooleanOption(string path, bool value, bool validateOnly)
		{
			XmlAttribute opt = GetOption(path, true);
			if(opt.Value != value.ToString())
			{
				if (!OnCanModify(path))
					return false;

				if (!validateOnly)
				{
					opt.Value = value.ToString();
					OnModified(path);
				}
			}
			return true;
		}

		public bool SetIntegerOption(string path, int value)
		{
			return SetIntegerOption(path, value, false);
		}

		public bool SetIntegerOption(string path, int value, bool validateOnly)
		{
			XmlAttribute opt = GetOption(path, true);
			if (opt.Value != value.ToString())
			{
				if (!OnCanModify(path))
					return false;

				if (!validateOnly)
				{
					opt.Value = value.ToString();
					OnModified(path);
				}
			}
			return true;
		}

		public bool SetStringOption(string path, string value)
		{
			return SetStringOption(path, value, false);
		}

		public bool SetStringOption(string path, string value, bool validateOnly)
		{
			XmlAttribute opt = GetOption(path, true);
			if (opt.Value != value)
			{
				if (!OnCanModify(path))
					return false;

				if (!validateOnly)
				{
					opt.Value = value;
					OnModified(path);
				}
			}
			return true;
		}

		public bool SetStringArrayOption(string path, string [] value)
		{
			return SetStringArrayOption(path, value, false);
		}

		public bool SetStringArrayOption(string path, string [] value, bool validateOnly)
		{
			XmlElement opt = GetArrayOption(path, true);
			string [] currValue = GetArray(opt);

			if (!CompareArrays(currValue, value))
			{
				if (!OnCanModify(path))
					return false;

				if (!validateOnly)
				{
					SetArray(opt, value);
					OnModified(path);
				}
			}
			return true;
		}

		public bool GetBooleanOption(string path, bool defaultval)
		{
			XmlAttribute opt = GetOption(path, false);
			if (opt == null)
				return defaultval;
			else
				return Boolean.Parse(opt.Value);
		}

		public int GetIntegerOption(string path, int defaultval)
		{
			XmlAttribute opt = GetOption(path, false);
			if (opt == null)
				return defaultval;
			else
				return Int32.Parse(opt.Value);
		}

		public string GetStringOption(string path, string defaultval)
		{
			XmlAttribute opt = GetOption(path, false);
			if (opt == null)
				return defaultval;
			else
				return opt.Value;
		}

		public string [] GetStringArrayOption(string path, string [] defaultval)
		{
			XmlElement opt = GetArrayOption(path, false);
			if (opt == null)
				return defaultval;
			else
				return GetArray(opt);
		}

		private XmlAttribute GetOption(string path, bool create)
		{
			string xmlname = Uri.EscapeUriString(path);

			XmlElement element = (XmlElement)mDatabaseRoot.SelectSingleNode("Option[@name='" + xmlname + "']");
			if(element == null)
			{
				if(!create)
					return null;

				XmlElement newElement = mDatabaseRoot.OwnerDocument.CreateElement("Option");
				XmlAttribute newAttr = mDatabaseRoot.OwnerDocument.CreateAttribute("value");
				newElement.SetAttribute("name", xmlname);
				newElement.SetAttributeNode(newAttr);
				mDatabaseRoot.AppendChild(newElement);
				return newAttr;
			}
			return element.GetAttributeNode("value");
		}

		private XmlElement GetArrayOption(string path, bool create)
		{
			string xmlname = Uri.EscapeUriString(path);

			XmlElement element = (XmlElement)mDatabaseRoot.SelectSingleNode("Option[@name='" + xmlname + "']");
			if (element == null)
			{
				if (!create)
					return null;

				XmlElement newElement = mDatabaseRoot.OwnerDocument.CreateElement("Option");
				newElement.SetAttribute("name", xmlname);
				mDatabaseRoot.AppendChild(newElement);
				return newElement;
			}
			return element;
		}

		private string[] GetArray(XmlElement opt)
		{
			XmlNodeList items = opt.SelectNodes("Value");
			string[] values = new string[items.Count];
			for (int index = 0; index < items.Count; ++index)
			{
				values[index] = items[index].InnerText;
			}
			return values;
		}

		private void SetArray(XmlElement opt, string[] values)
		{
			opt.InnerXml = "";
			foreach (string value in values)
			{
				XmlElement item = opt.OwnerDocument.CreateElement("Value");
				item.InnerText = value;
				opt.AppendChild(item);
			}
		}

		private bool CompareArrays(string[] lhs, string[] rhs)
		{
			if (lhs == null || rhs == null || lhs.Length != rhs.Length)
				return false;
			else
			{
				for (int index = 0; index < lhs.Length; ++index)
				{
					if (lhs[index] != rhs[index])
						return false;
				}
			}
			return true;
		}

		private void OnModified(string path)
		{
			if (Modified != null)
				Modified(this, path);
		}

		private bool OnCanModify(string path)
		{
			if (CanModify != null)
				return CanModify(this, path);
			else
				return true;
		}
	}
}
