using System;
using System.Collections.Generic;
using System.Text;
using Scintilla.Enums;

namespace Scintilla.Configuration
{
    public interface ILexerConfigCollection
    {
        ILexerConfig this[int lexerId] { get; }

        ILexerConfig this[Lexer lexer] { get; }

        ILexerConfig this[string lexerName] { get; }
    }
}
