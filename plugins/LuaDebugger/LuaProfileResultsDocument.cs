
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
using System.IO;
using System.Xml;

using Tilde.Framework.Model;
using System.Text.RegularExpressions;

namespace Tilde.LuaDebugger
{
	public class LuaProfileEntry
	{
		public LuaProfileEntry(string name, string function, string language, string file, int line, int count, double time, double timeChildren)
		{
			m_name = name;
			m_function = function;
			m_language = language;
			m_file = file;
			m_line = line;
			m_count = count;
			m_timeSelf = time;
			m_timeChildren = timeChildren;
		}

		public string m_name;
		public string m_function;
		public string m_language;
		public string m_file;
		public int m_line;
		public int m_count;
		public double m_timeSelf;
		public double m_timeChildren;
	}

	[DocumentClassAttribute("Lua Profile Results",
		ViewType = typeof(LuaProfileResultsView),
		FileExtensions = new string[] { ".luaprof.xml" })
	]

	public class LuaProfileResultsDocument : Document
	{
		private List<LuaProfileEntry> m_functions;

		public LuaProfileResultsDocument(Tilde.Framework.Controller.IManager manager, string fileName)
			: base(manager, fileName)
		{
			m_functions = new List<LuaProfileEntry>();
		}

		public List<LuaProfileEntry> Functions
		{
			get { return m_functions; }
		}

		protected override bool New(Stream stream)
		{
			return Load(stream);
		}

		protected override bool Load()
		{
			StreamReader reader = new StreamReader(FileName, Encoding.UTF8);
			bool result = Load(reader.BaseStream);
			reader.Close();

			return result;;
		}

		protected override bool Save()
		{
			XmlDocument document = new XmlDocument();
			XmlElement profileNode = document.CreateElement("luaprofile");
			document.AppendChild(profileNode);

			foreach(LuaProfileEntry entry in m_functions)
			{
				XmlElement functionNode = document.CreateElement("function");
				profileNode.AppendChild(functionNode);

				functionNode.SetAttribute("name", entry.m_name);
				functionNode.SetAttribute("call_count", entry.m_count.ToString());
				functionNode.SetAttribute("self", entry.m_timeSelf.ToString());
				functionNode.SetAttribute("children", entry.m_timeChildren.ToString());
			}

			XmlTextWriter writer = new XmlTextWriter(FileName, Encoding.UTF8);
			writer.Formatting = Formatting.Indented;
			document.WriteContentTo(writer);
			writer.Close();
			
			return true;
		}

		private bool Load(Stream stream)
		{
			if (stream == null)
				return true;

			XmlDocument document = new XmlDocument();
			document.Load(stream);

			// name (class::method) [C]
			// name (C function 0x00000000) [C]
			// name (file:line) [Lua]
			string languageExpr = @"\[(?<language>\w*)\]";
			string sourceLineExpr = @"(?<source>[^:)]*):(?<line>\d+)";
			string detailsExpr = @"(?<details>[^)]*)";
			string nameExpr = @"(?<name>.*?)?";
			Regex functionRegex = new Regex(@"^" + nameExpr + @"\s*\((?:" + sourceLineExpr + "|" + detailsExpr + @")\)\s*" + languageExpr + "$");

			XmlNode profileNode = document.SelectSingleNode("luaprofile");
			foreach (XmlElement functionNode in profileNode.SelectNodes("function"))
			{
				string name = functionNode.GetAttribute("name");

				string function = name;
				string file = "";
				int line = -1;
				int count = 0;
				double time = 0;
				double timeChildren = 0;
				string language = "?";

				Match match = functionRegex.Match(name);
				if (match.Success)
				{
					string details = match.Groups["details"].Value;
					function = match.Groups["name"].Value;
					language = match.Groups["language"].Value;
					file = match.Groups["source"].Value;
					Int32.TryParse(match.Groups["line"].Value, out line);
					if (file == "")
						file = details;
				}

				Int32.TryParse(functionNode.GetAttribute("call_count"), out count);
				Double.TryParse(functionNode.GetAttribute("self"), out time);
				Double.TryParse(functionNode.GetAttribute("children"), out timeChildren);

				LuaProfileEntry entry = new LuaProfileEntry(name, function, language, file, line, count, time, timeChildren);
				m_functions.Add(entry);
			}

			return true;
		}
	}
}
