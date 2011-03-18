using System;
using System.Collections.Generic;
using System.Text;
using Scintilla.Enums;

namespace Scintilla.Configuration
{
    public interface ILanguageConfig
    {
        SortedDictionary<int, string> KeywordLists { get; }

        SortedDictionary<int, ILexerStyle> Styles { get; }
        
        ILexerConfig Lexer { get; set; }

        string Name { get; set; }

        string WordCharacters { get; set; }

        string WhitespaceCharacters { get; set; }

        string PreprocessorSymbol { get; set; }

        string PreprocessorStart { get; set; }

        string PreprocessorMiddle { get; set; }

        string PreprocessorEnd { get; set; }

        string FilePattern { get; set; }

        string FileFilter { get; set; }

        string Extension { get; set; }

        string ExtensionList { get; set; }
    }
}
