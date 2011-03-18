
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
using System.Text;
using System.Windows.Forms;

using Tilde.Framework.Controller;
using Tilde.Framework.Controls;
using System.Xml;
using System.IO;

namespace Tilde.Framework.View
{
	public partial class ToolWindow : WeifenLuo.WinFormsUI.Docking.DockContent
	{
		public ToolWindow()
		{
			InitializeComponent();
		}

		protected override string GetPersistString()
		{
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.OmitXmlDeclaration = true;

			StringBuilder builder = new StringBuilder();
			XmlWriter writer = XmlWriter.Create(builder, settings);

			List<Control> listViews = new List<Control>();
			FindControlsByType(this, typeof(ListView), listViews);
			foreach(ListView listView in listViews)
			{
				SerialseListView(writer, listView);
			}

			writer.Close();

			return builder.Length == 0 ? null : builder.ToString();
		}

		public virtual bool ConfigureFromPersistString(string persistString)
		{
			if (persistString == "")
				return true;

			StringReader stream = new StringReader(persistString);
			XmlReader reader = XmlReader.Create(stream);

			while (reader.Read() && reader.IsStartElement("ListView"))
			{
				DeserialiseListView(reader);
			}

			reader.Close();

			return true;
		}

		private void SerialseListView(XmlWriter writer, ListView listView)
		{
			writer.WriteStartElement("ListView");
			writer.WriteAttributeString("name", listView.Name);
			foreach (ColumnHeader column in listView.Columns)
			{
				writer.WriteStartElement("Column");
				writer.WriteAttributeString("Index", column.Index.ToString());
				writer.WriteAttributeString("DisplayIndex", column.DisplayIndex.ToString());
				writer.WriteAttributeString("Width", column.Width.ToString());
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		private void DeserialiseListView(XmlReader reader)
		{
			string name = reader.GetAttribute("name");
			Control [] controls = this.Controls.Find(name, true);
			if (controls.Length != 1)
			{
				reader.Skip();
				return;
			}

			ListView listView = controls[0] as ListView;

			while (reader.Read() && reader.IsStartElement("Column"))
			{
				try
				{
					int index = Int32.Parse(reader.GetAttribute("Index"));
					int displayIndex = Int32.Parse(reader.GetAttribute("DisplayIndex"));
					int width = Int32.Parse(reader.GetAttribute("Width"));

					listView.Columns[index].DisplayIndex = displayIndex;
					listView.Columns[index].Width = width;
				}
				catch(Exception)
				{

				}
			}
		}

		private void FindControlsByType(Control root, Type type, List<Control> result)
		{
			foreach(Control control in root.Controls)
			{
				if(type.IsAssignableFrom(control.GetType()))
				{
					result.Add(control);
				}
				FindControlsByType(control, type, result);
			}
		}


	}
}

