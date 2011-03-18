
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

using Tilde.Framework.Controller;
using System.ComponentModel;
using System.Drawing;

namespace Tilde.CorePlugins.TextEditor
{
	[OptionsCollection(Path = "Text Editor", Editor = typeof(OptionsGridPanel))]
	public class TextOptions : IOptions
	{
		[Category("Whitespace")]
		[Description("Specifies if whitespace is visible in text editors.")]
		[Option(Path = "TextEditor/VisibleWhitespace", DefaultValue = Scintilla.Enums.WhiteSpace.Invisible)]
		[DisplayName("Visible whitespace")]
		public Scintilla.Enums.WhiteSpace Whitespace
		{
			get { return m_whitespace; }
			set { if (m_whitespace != value) { m_whitespace = value; OnOptionsChanged("Whitespace"); } }
		}
		private Scintilla.Enums.WhiteSpace m_whitespace;

		[Category("Whitespace")]
		[Description("Sets the foreground colour of whitespace markers.")]
		[Option(Path = "TextEditor/WhitespaceForeground", DefaultValue = "LightBlue")]
		[DisplayName("Whitespace foreground colour")]
		public Color WhitespaceForeground
		{
			get { return m_whitespaceForegroundColour; }
			set { if (m_whitespaceForegroundColour != value) { m_whitespaceForegroundColour = value; OnOptionsChanged("WhitespaceForeground"); } }
		}
		private Color m_whitespaceForegroundColour;

		[Category("Whitespace")]
		[Description("Sets the background colour of whitespace markers.")]
		[Option(Path = "TextEditor/WhitespaceBackground", DefaultValue = "Transparent")]
		[DisplayName("Whitespace background colour")]
		public Color WhitespaceBackground
		{
			get { return m_whitespaceBackgroundColour; }
			set { if (m_whitespaceBackgroundColour != value) { m_whitespaceBackgroundColour = value; OnOptionsChanged("WhitespaceBackground"); } }
		}
		private Color m_whitespaceBackgroundColour;

		[Category("Indentation guides")]
		[Description("Displays dotted vertical lines within indentation white space every indent size columns.")]
		[Option(Path = "TextEditor/IndentationGuides", DefaultValue = false)]
		[DisplayName("Indentation guides")]
		public bool IndentationGuides
		{
			get { return m_indentationGuides; }
			set { if (m_indentationGuides != value) { m_indentationGuides = value; OnOptionsChanged("IndentationGuides"); } }
		}
		private bool m_indentationGuides;

		[Category("Indentation guides")]
		[Description("Highlights the indentation guide associated with a brace when that brace is highlighted.")]
		[Option(Path = "TextEditor/IndentationGuideHighlight", DefaultValue = false)]
		[DisplayName("Indentation guide highlight")]
		public bool IndentationGuideHighlight
		{
			get { return m_indentationGuideHighlight; }
			set { if (m_indentationGuideHighlight != value) { m_indentationGuideHighlight = value; OnOptionsChanged("IndentationGuideHighlight"); } }
		}
		private bool m_indentationGuideHighlight;

		[Category("Line edges")]
		[Description("Marks long lines by drawing a vertical line or colouring the background of characters that exceed the set length.")]
		[Option(Path = "TextEditor/LineEdgeMode", DefaultValue = EdgeMode.None)]
		[DisplayName("Line edge mode")]
		public EdgeMode LineEdgeMode
		{
			get { return m_lineEdgeMode; }
			set { if (m_lineEdgeMode != value) { m_lineEdgeMode = value; OnOptionsChanged("LineEdgeMode"); } }
		}
		private EdgeMode m_lineEdgeMode;

		[Category("Line edges")]
		[Description("Sets the column number of the line edge marker.")]
		[Option(Path = "TextEditor/LineEdgeColumn", DefaultValue = 132)]
		[DisplayName("Line edge column")]
		public int LineEdgeColumn
		{
			get { return m_lineEdgeColumn; }
			set { if (m_lineEdgeColumn != value) { m_lineEdgeColumn = value; OnOptionsChanged("LineEdgeColumn"); } }
		}
		private int m_lineEdgeColumn;

		[Category("Line edges")]
		[Description("Sets the colour of the line edge marker.")]
		[Option(Path = "TextEditor/LineEdgeColour", DefaultValue = "Black")]
		[DisplayName("Line edge colour")]
		public Color LineEdgeColor
		{
			get { return m_lineEdgeColor; }
			set { if (m_lineEdgeColor != value) { m_lineEdgeColor = value; OnOptionsChanged("LineEdgeColor"); } }
		}
		private Color m_lineEdgeColor;

		public TextOptions()
		{
		}
	}

}
