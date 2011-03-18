using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Scintilla.Enums;

namespace Scintilla.Configuration
{
    public class LexerStyle : ILexerStyle
    {
        private int styleIndex;
        private String styleDescription;
        private String fontName;
        private int? fontSize;
        private Color foreColor;
        private Color backColor;
        private bool? italics;
        private bool? bold;
        private bool? underline;
        private bool? eolFilled;
        private CaseVisible caseVisibility = CaseVisible.Mixed;

        public LexerStyle(int styleIndex)
        {
            this.styleIndex = styleIndex;
        }

        public int StyleIndex
        {
            get { return styleIndex; }
        }

        public String StyleDescription
        {
            get { return styleDescription; }
            set { styleDescription = value; }
        }

        public String FontName
        {
            get { return fontName; }
            set { fontName = value; }
        }

        public int? FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
        }

        public Color ForeColor
        {
            get { return foreColor; }
            set { foreColor = value; }
        }

        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        public bool? Italics
        {
            get { return italics; }
            set { italics = value; }
        }

        public bool? Bold
        {
            get { return bold; }
            set { bold = value; }
        }

        public bool? Underline
        {
            get { return underline; }
            set { underline = value; }
        }

        public bool? EOLFilled
        {
            get { return eolFilled; }
            set { eolFilled = value; }
        }

        public CaseVisible CaseVisibility
        {
            get { return caseVisibility; }
            set { caseVisibility = value; }
        }
    }
}
