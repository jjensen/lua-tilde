
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
using Tilde.Framework.View;
using Tilde.Framework.Controller;
using Tilde.CorePlugins.TextEditor;

namespace Tilde.CorePlugins
{
	[ToolWindowAttribute]
	public partial class FindResultsPanel : Tilde.Framework.View.ToolWindow
	{
		IManager m_manager;
		int m_resultCount;
		HtmlElement m_hilightedElement;

		[System.Runtime.InteropServices.ComVisibleAttribute(true)]
		public class ScriptHelper
		{
			FindResultsPanel m_owner;
			public ScriptHelper(FindResultsPanel owner)
			{
				m_owner = owner;
			}
		}

		public FindResultsPanel(IManager manager)
		{
			InitializeComponent();

			m_manager = manager;
			m_resultCount = 0;

			m_manager.FindInFilesStarted += new FindInFilesStartedEventHandler(Manager_FindInFilesStarted);
			m_manager.FindInFilesResult += new FindInFilesResultEventHandler(Manager_FindInFilesResult);
			m_manager.FindInFilesStopped += new FindInFilesStoppedEventHandler(Manager_FindInFilesStopped);
			m_manager.GoToNextLocation += new GoToNextLocationEventHandler(Manager_GoToNextLocation);
			m_manager.GoToPreviousLocation += new GoToPreviousLocationEventHandler(Manager_GoToPreviousLocation);

			// The Navigate() and Write() are needed to get around a bug in .NET 2.0 apparently (!) 
			// http://geekswithblogs.net/paulwhitblog/archive/2005/12/12/62961.aspx
			outputBrowser.Navigate("about:blank");
			outputBrowser.Document.Write("");
			outputBrowser.DocumentText = Tilde.CorePlugins.Properties.Resources.FindResultsTemplate;
			outputBrowser.Document.Write("");
			outputBrowser.ObjectForScripting = new ScriptHelper(this);
		}


		private void FindResultsPanel_Load(object sender, EventArgs e)
		{
		}

		public void GotoLine(string file, int line, int startChar, int endChar)
		{
			DocumentView view = m_manager.ShowDocument(file);
			if (view != null && view is TextView)
			{
				((TextView)view).SelectText(line, startChar, endChar);
			}
		}

		void Manager_FindInFilesStarted(object sender, string message)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(delegate() { Manager_FindInFilesStarted(sender, message); }));
			}
			else
			{
				this.Show();
				outputBrowser.Document.Body.InnerHtml = "";
				m_resultCount = 0;
				m_hilightedElement = null;

				HtmlElement element = outputBrowser.Document.CreateElement("p");
				element.SetAttribute("className", "status");
				element.InnerText = message;
				outputBrowser.Document.Body.AppendChild(element);
			}
		}

		void Manager_FindInFilesStopped(object sender, string message)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(delegate() { Manager_FindInFilesStopped(sender, message); }));
			}
			else
			{
				HtmlElement element = outputBrowser.Document.CreateElement("p");
				element.SetAttribute("className", "status");
				element.InnerText = message;
				outputBrowser.Document.Body.AppendChild(element);
				//element.ScrollIntoView(false);
			}
		}

		void Manager_FindInFilesResult(object sender, FindInFilesResultEventArgs args)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(delegate() { Manager_FindInFilesResult(sender, args); }));
			}
			else
			{
				HtmlElement element = outputBrowser.Document.CreateElement("p");
				int id = m_resultCount++;
				element.Id = id.ToString();
				element.SetAttribute("className", "result");
				element.SetAttribute("myFile", args.File);
				element.SetAttribute("myLine", args.Line.ToString());
				element.SetAttribute("myStartChar", args.StartChar.ToString());
				element.SetAttribute("myEndChar", args.EndChar.ToString());
				
				element.DoubleClick += new HtmlElementEventHandler(Result_DoubleClick);
				element.Click += new HtmlElementEventHandler(Result_Click);
				element.KeyDown += new HtmlElementEventHandler(
					delegate(object s, HtmlElementEventArgs e)
					{
						if (e.KeyPressedCode == (int) Keys.Up && id > 1)
						{
							SelectResult(id - 1);
							e.ReturnValue = true;
						}
						else if (e.KeyPressedCode == (int) Keys.Down && id < m_resultCount)
						{
							SelectResult(id + 1);
							e.ReturnValue = true;
						}
					}
				);
				if (args.StartChar >= 0 && args.EndChar >= 0)
				{
					element.InnerHtml =
						  System.Web.HttpUtility.HtmlEncode(String.Format("{0}({1}): {2}", args.File, args.Line, args.Message.Substring(0, args.StartChar)))
						+ "<span class='match'>"
						+ System.Web.HttpUtility.HtmlEncode(args.Message.Substring(args.StartChar, args.EndChar - args.StartChar))
						+ "</span>"
						+ System.Web.HttpUtility.HtmlEncode(args.Message.Substring(args.EndChar));
				}
				else
				{
					element.InnerText = String.Format("{0}({1}): {2}", args.File, args.Line, args.Message);
				}
				outputBrowser.Document.Body.AppendChild(element);
//				element.ScrollIntoView(false);
			}
		}

		void Manager_GoToNextLocation(IManager sender, ref bool consumed)
		{
			if (m_resultCount == 0)
				return;

			int nextId;
			if(m_hilightedElement != null)
			{
				int id = Int32.Parse(m_hilightedElement.Id);
				if (id < m_resultCount - 1)
					nextId = id + 1;
				else
					nextId = 0;
			}
			else if(m_resultCount > 0)
				nextId = 0;
			else
				return;

			if (outputBrowser.Focused)
				SelectResult(nextId);
			else
				ShowResult(nextId);

			consumed = true;
		}

		void Manager_GoToPreviousLocation(IManager sender, ref bool consumed)
		{
			if (m_resultCount == 0)
				return;

			int nextId;
			if (m_hilightedElement != null)
			{
				int id = Int32.Parse(m_hilightedElement.Id);
				if (id > 0)
					nextId = id - 1;
				else
					nextId = m_resultCount - 1;
			}
			else if (m_resultCount > 0)
				nextId = m_resultCount - 1;
			else
				return;

			if (outputBrowser.Focused)
				SelectResult(nextId);
			else
				ShowResult(nextId);

			consumed = true;
		}

		private void ShowResult(int id)
		{
			SelectResult(id);

			if (m_hilightedElement != null)
			{
				string file;
				int line, startChar, endChar;
				if (GetInfoForResult(m_hilightedElement, out file, out line, out startChar, out endChar))
					this.BeginInvoke(new MethodInvoker(delegate() { GotoLine(file, line, startChar, endChar); }));
			}
		}

		private void SelectResult(int id)
		{
			if(m_hilightedElement != null)
				m_hilightedElement.SetAttribute("className", "result");

			m_hilightedElement = outputBrowser.Document.GetElementById(id.ToString());

			if (m_hilightedElement != null)
			{
				m_hilightedElement.SetAttribute("className", "result hilight");
				int bufferHeight = m_hilightedElement.OffsetRectangle.Height * 3;
				Rectangle viewRect = new Rectangle(
					outputBrowser.Document.Body.ScrollLeft, 
					outputBrowser.Document.Body.ScrollTop + bufferHeight, 
					outputBrowser.Document.Body.ClientRectangle.Width,
					outputBrowser.Document.Body.ClientRectangle.Height - bufferHeight * 2);
				if (!viewRect.IntersectsWith(m_hilightedElement.OffsetRectangle))
				{
					if (m_hilightedElement.OffsetRectangle.Top < viewRect.Top)
						outputBrowser.Document.Body.ScrollTop = Math.Max(0, m_hilightedElement.OffsetRectangle.Top - bufferHeight);
					else
						outputBrowser.Document.Body.ScrollTop = Math.Min(outputBrowser.Document.Body.ScrollRectangle.Height, m_hilightedElement.OffsetRectangle.Bottom - outputBrowser.Document.Body.ClientRectangle.Height + bufferHeight);
				}
			}
		}

		void Result_DoubleClick(object sender, HtmlElementEventArgs e)
		{
			string file;
			int line, startChar, endChar;
			if(GetInfoForResult((HtmlElement) sender, out file, out line, out startChar, out endChar))
				this.BeginInvoke(new MethodInvoker(delegate() { GotoLine(file, line, startChar, endChar); }));
		}

		void Result_Click(object sender, HtmlElementEventArgs e)
		{
			SelectResult(Int32.Parse(((HtmlElement) sender).Id));
		}


		bool GetInfoForResult(HtmlElement element, out string file, out int line, out int startChar, out int endChar)
		{
			file = element.GetAttribute("myFile");
			line = Int32.Parse(element.GetAttribute("myLine"));
			startChar = Int32.Parse(element.GetAttribute("myStartChar"));
			endChar = Int32.Parse(element.GetAttribute("myEndChar"));

			return file != "";
		}

		private void outputBrowser_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			/*
			if(e.KeyCode == Keys.Up && m_hilightedElement != null)
			{
				int id = Int32.Parse(m_hilightedElement.Id);
				if (id > 1)
					SelectLine(id - 1);
			}
			else if (e.KeyCode == Keys.Down && m_hilightedElement != null)
			{
				int id = Int32.Parse(m_hilightedElement.Id);
				if (id < m_resultCount)
					SelectLine(id + 1);
			}
			*/
		}

	}
}

