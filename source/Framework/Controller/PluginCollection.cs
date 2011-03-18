
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

using Tilde.Framework.Controller;
using Tilde.Framework.Model;

namespace Tilde.Framework.Controller
{
	public class PluginDetails
	{
		private string m_name;
		private string m_path;
		private Assembly m_assembly;
		private IPlugin m_plugin;

		public PluginDetails(string name, string path)
		{
			m_name = name;
			m_path = path;
		}

		public string Name
		{
			get { return m_name; }
		}

		public string Path
		{
			get { return m_path; }
		}

		public Assembly Assembly
		{
			get { return m_assembly; }
			set { m_assembly = value; }
		}

		public IPlugin Plugin
		{
			get { return m_plugin; }
			set { m_plugin = value; }
		}

		public List<Type> GetImplementations(Type interfaceType)
		{
			List<Type> result = new List<Type>();
			if (m_assembly != null)
			{
				foreach (Type type in m_assembly.GetTypes())
				{
					if (interfaceType.IsAssignableFrom(type) && type != interfaceType && !type.IsAbstract)
					{
						result.Add(type);
					}
				}
			}
			return result;
		}

	}

	public class PluginCollection : ListCollectionBase<PluginDetails>
	{
		public PluginCollection()
		{

		}

		protected override void OnRemove(PluginDetails item)
		{
		}

		protected override void OnAdd(PluginDetails item)
		{
		}
	}
}
