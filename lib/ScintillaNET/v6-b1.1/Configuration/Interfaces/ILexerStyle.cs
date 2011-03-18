using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Scintilla.Enums;

namespace Scintilla.Configuration
{
    public interface ILexerStyle
    {
        int StyleIndex { get; }

        String StyleDescription { get; set; }

        String FontName { get; set; }

        int? FontSize { get; set; }

        Color ForeColor { get; set; }

        Color BackColor { get; set; }

        bool? Italics { get; set; }

        bool? Bold { get; set; }

        bool? Underline { get; set; }

        bool? EOLFilled { get; set; }

        CaseVisible CaseVisibility { get; set; }
    }
}
