using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Scintilla.Configuration
{
    public interface IScintillaConfig
    {
        ILanguageConfigCollection Languages { get; }

        List<string> LanguageNames { get; }

        ILexerConfigCollection Lexers { get; }

        List<IMenuItemConfig> LanguageMenuItems { get; }

        SortedDictionary<int, ILexerStyle> Styles { get; }

        SortedDictionary<string, string> Properties { get; }
        
        int? CodePage { get; set; }

        int? TabSize { get; set; }

        int? IndentSize { get; set; }

		bool? UseTabs { get; set; }

        bool? Fold { get; set; }

        bool? FoldCompact { get; set; }

        int? FoldFlags { get; set; }

        bool? FoldComment { get; set; }

        bool? FoldPreprocessor { get; set; }

        bool? FoldSymbols { get; set; }

        bool? FoldOnOpen { get; set; }

        bool? FoldHTML { get; set; }

        bool? FoldHTMLPreprocessor { get; set; }

        int? FoldMarginWidth { get; set; }

        Color FoldMarginColor { get; set; }

        Color FoldMarginHighlightColor { get; set; }

        int? SelectionAlpha { get; set; }

        Color SelectionBackColor { get; set; }

        string DefaultFileExtention { get; set; }

        string FileOpenFilter { get; set; }

    }
}
