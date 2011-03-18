
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
	public class CommandLineArguments
	{
		public List<KeyValuePair<string, string>> m_arguments;

		public CommandLineArguments(string [] args)
		{
			m_arguments = new List<KeyValuePair<string, string>>();

			foreach(string arg in args)
			{
				if(arg.StartsWith("--"))
				{
					int assignmentIndex = arg.IndexOf('=');
					if(assignmentIndex > 0)
					{
						string key = arg.Substring(0, assignmentIndex - 1);
						string value = arg.Substring(assignmentIndex + 1);
						m_arguments.Add(new KeyValuePair<string, string>(key, value));
					}
					else
					{
						m_arguments.Add(new KeyValuePair<string, string>(arg, null));
					}
				}
			}
		}

		public bool HasParameter(string name)
		{
			foreach(KeyValuePair<string, string> pair in m_arguments)
			{
				if (name == pair.Key)
					return true;
			}
			return false;
		}

		public string GetValue(string name)
		{
			foreach (KeyValuePair<string, string> pair in m_arguments)
			{
				if (name == pair.Key)
					return pair.Value;
			}
			return null;
		}

		public string [] GetValues(string name)
		{
			List<string> result = new List<string>();
			foreach (KeyValuePair<string, string> pair in m_arguments)
			{
				if (name == pair.Key)
					result.Add(pair.Value);
			}
			return result.ToArray();
		}

	}
}
