
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

using Tilde.Framework.Controller;
using Tilde.Framework.View;
using Tilde.Framework.Controls;

namespace Tilde.LuaDebugger
{
	public partial class VariablesWindow : ToolWindow
	{
		public class NodeComparer : IComparer<TreeTableNode>
		{
			DebugManager mDebugger;

			public NodeComparer(DebugManager debugger)
			{
				mDebugger = debugger;

				// The higher the order value, the higher the type appears in the output
				m_typeOrder = new SortedList<LuaValueType, int>();
				m_typeOrder.Add(LuaValueType.TILDE_METATABLE, 4);
				m_typeOrder.Add(LuaValueType.TILDE_ENVIRONMENT, 3);
				m_typeOrder.Add(LuaValueType.TILDE_UPVALUES, 2);
				m_typeOrder.Add(LuaValueType.NUMBER, 1);
			}

			public int Compare(TreeTableNode x, TreeTableNode y)
			{
				VariableDetails varx = (VariableDetails)x.Tag;
				VariableDetails vary = (VariableDetails)y.Tag;

				int orderx, ordery;
				m_typeOrder.TryGetValue(varx.Key.Type, out orderx);
				m_typeOrder.TryGetValue(vary.Key.Type, out ordery);

				if (varx.Key.Type == LuaValueType.NUMBER && vary.Key.Type == LuaValueType.NUMBER)
					return Math.Sign(varx.Key.AsNumber() - vary.Key.AsNumber());
				else if (orderx == ordery)
				{
					string keyx = mDebugger.GetValueString(varx.Key);
					string keyy = mDebugger.GetValueString(vary.Key);
					return String.Compare(keyx, keyy);
				}
				else
					return ordery - orderx;
			}

			private SortedList<LuaValueType, int> m_typeOrder;
		}

		protected DebugManager mDebugger;
		protected ListViewItem mContextMenuItem = null;
		private NodeComparer mNodeComparer;

		bool mShowFunctions = true;
		bool mShowMetadata = true;
		protected Font mBoldFont;
		protected Font mItalicFont;
		protected bool mUpdateInProgress = false;

		public VariablesWindow()
		{
			InitializeComponent();
		}

		public VariablesWindow(IManager manager)
		{
			InitializeComponent();

			mDebugger = ((LuaPlugin)manager.GetPlugin(typeof(LuaPlugin))).Debugger;
			mNodeComparer = new NodeComparer(mDebugger);

			mBoldFont = new Font(variablesListView.Font, FontStyle.Bold);
			mItalicFont = new Font(variablesListView.Font, FontStyle.Italic);
		}

		protected bool ShowFunctions
		{
			get { return mShowFunctions; }
			set
			{
				if(value != mShowFunctions)
				{
					mShowFunctions = value;
					variablesListView.BeginUpdate();
					UpdateFilterRecursive(variablesListView.Root);
					variablesListView.EndUpdate();
				}
			}
		}

		protected bool ShowMetadata
		{
			get { return mShowMetadata; }
			set
			{
				if (value != mShowMetadata)
				{
					mShowMetadata = value;
					variablesListView.BeginUpdate();
					UpdateFilterRecursive(variablesListView.Root);
					variablesListView.EndUpdate();
				}
			}
		}

		private bool IsVariableVisible(VariableDetails details)
		{
			bool isMetadata = details.Key.Type == LuaValueType.TILDE_ENVIRONMENT || details.Key.Type == LuaValueType.TILDE_METATABLE || details.Key.Type == LuaValueType.TILDE_UPVALUES;
			bool isFunction = details.Value.Type == LuaValueType.FUNCTION;

			if (details.Path.Length == 0)
				return true;
			else if ((isMetadata && !mShowMetadata) || (isFunction && !mShowFunctions))
				return false;
			else
				return true;
		}

		private void UpdateFilterRecursive(TreeTableNode root)
		{
			foreach (TreeTableNode child in root.Items)
			{
				VariableDetails details = (VariableDetails)child.Tag;

				child.Visible = IsVariableVisible(details);
				child.Expandable = details.HasEntries || (details.HasMetadata && ShowMetadata);

				UpdateFilterRecursive(child);
			}
		}

		protected void Clear()
		{
			variablesListView.BeginUpdate();
			variablesListView.Root.Items.Clear();
			variablesListView.EndUpdate();
		}

		protected TreeTableNode FindClosestAncestor(VariableDetails var)
		{
			// Check if its in there exactly
			string searchkey = var.ToString();
			if (variablesListView.ContainsKey(searchkey))
				return variablesListView[searchkey];

			// 3>2:STRING.123323253:NUMBER.5:USERDATA

			// Find its closest ancestor
			int separator;
			while ( (separator = searchkey.LastIndexOf('.')) >= 0)
			{
				searchkey = searchkey.Substring(0, separator);
				if (variablesListView.ContainsKey(searchkey))
				{
					return variablesListView[searchkey];
				}
			}
			return variablesListView.Root;
		}

		protected void UpdateVariables(VariableDetails[] vars)
		{
			mUpdateInProgress = true;
			variablesListView.BeginUpdate();
			BeginVariableUpdate();

			foreach (VariableDetails varInfo in vars)
			{
				UpdateVariable(varInfo);
			}

			mUpdateInProgress = false ;
			variablesListView.EndUpdate();
			variablesListView.Refresh();
		}

		protected void BeginVariableUpdate()
		{
			variablesListView.Root.ForEach(
				delegate(TreeTableNode node)
				{
					if (node.ForeColor == Color.Red)
						node.ForeColor = variablesListView.ForeColor;
				}
			);
		}

		protected void UpdateVariable(VariableDetails varInfo)
		{
			if (variablesListView.ContainsKey(varInfo.ToString()))
			{
				TreeTableNode node = variablesListView[varInfo.ToString()];
				node.Tag = varInfo;

				if (!varInfo.Valid)
				{
					node.Items.Clear();

					// Only remove the variable if it's not a root watch expression
					if (varInfo.WatchID > 0 && varInfo.Path.Length == 0)
					{
						node.ForeColor = SystemColors.InactiveCaptionText;
						node.SubItems[1].Text = QuoteValueString(varInfo.Value);
						node.SubItems[2].Text = varInfo.Value.Type.ToString();
					}
					else
						node.Parent.Items.Remove(node);
				}
				else
				{
					node.Expandable = varInfo.HasEntries || (varInfo.HasMetadata && ShowMetadata);
					node.Expanded = varInfo.Expanded;

					string value = QuoteValueString(varInfo.Value);

					if (!varInfo.Expanded)
						node.Items.Clear();

					if (varInfo.Value.Type == LuaValueType.NONE)
					{
						node.Items.Clear();

						node.ForeColor = SystemColors.InactiveCaptionText;
					}
					else if(node.SubItems[1].Text != value || node.SubItems[2].Text != varInfo.Value.Type.ToString())
					{
						node.ForeColor = Color.Red;
						node.SubItems[1].Text = value;
						node.SubItems[2].Text = varInfo.Value.Type.ToString();
					}
					else
					{
						node.ForeColor = variablesListView.ForeColor;
					}
				}
				node.Visible = IsVariableVisible(varInfo);
			}
			else if(varInfo.Valid && (varInfo.WatchID == 0 || mDebugger.FindWatch(varInfo.WatchID) != null))
			{
				TreeTableNode parent = FindClosestAncestor(varInfo);
				TreeTableNode node = AddVariable(varInfo);
				parent.Items.Insert(node, mNodeComparer);
				node.Visible = IsVariableVisible(varInfo);
			}
		}

		private string QuoteValueString(LuaValue value)
		{
			string result = mDebugger.GetValueString(value);
			if (value.Type == LuaValueType.STRING)
			{
				result = "\"" + result + "\"";
			}
			return result;
		}

		protected TreeTableNode AddVariable(VariableDetails varInfo)
		{
			string value = "";
			string type = "";
			if (varInfo.Value != null)
			{
				value = QuoteValueString(varInfo.Value);
				type = varInfo.Value.Type.ToString();
			}

			TreeTableNode node = new TreeTableNode(mDebugger.GetValueString(varInfo.Key));
			node.Key = varInfo.ToString();
			node.Tag = varInfo;
			node.SubItems.Add(value);
			node.SubItems.Add(type);

			if (varInfo.VariableClass == VariableClass.Upvalue)
				node.Font = mBoldFont;

			if (varInfo.Key != null)
			{
				if (varInfo.Key.Type == LuaValueType.TILDE_ENVIRONMENT || varInfo.Key.Type == LuaValueType.TILDE_METATABLE || varInfo.Key.Type == LuaValueType.TILDE_UPVALUES)
					node.Font = mItalicFont;
			}

			if (varInfo.Value != null)
			{
				node.Expandable = varInfo.HasEntries || (varInfo.HasMetadata && ShowMetadata);
				node.Expanded = varInfo.Expanded;
			}

			return node;
		}

		protected void baseContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			Point hitPos = variablesListView.PointToClient(Control.MousePosition);
			ListViewHitTestInfo info = variablesListView.HitTest(hitPos.X, hitPos.Y);
			mContextMenuItem = info.Item;

			/*
			if (info.Item != null)
			{
				ListItemTag watchVar = (ListItemTag)info.Item.Tag;
				VariableDetails varInfo = watchVar.mDetails;

				goToSource.Enabled = varInfo.Value.Type == LuaValueType.FUNCTION;
			}
			else
			{
				modifyValue.Enabled = false;
				goToSource.Enabled = false;
			}
			*/
		}

		private void goToSource_Click(object sender, EventArgs e)
		{

		}
	}
}

