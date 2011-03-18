using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Scintilla.Enums;

namespace Scintilla.Configuration
{
    public class LexerConfig : ILexerConfig
    {
        private SortedDictionary<int, ILexerStyle> styles;
        private Lexer lexerType;
        private string lexerName;

        public LexerConfig(Lexer lexer)
        {
            this.lexerType = lexer;
            lexerName = GetLexerName(lexerType);
        }

        public LexerConfig(int lexer)
        {
            this.lexerType = (Lexer)lexer;
            lexerName = GetLexerName(lexerType);
        }

        public Lexer Type
        {
            get { return lexerType; }
        }

        public int LexerID
        {
            get { return (int)lexerType; }
        }

        public string LexerName
        {
            get { return lexerName; }
        }

        public SortedDictionary<int, ILexerStyle> Styles
        {
            get
            {
                if (styles == null)
                {
                    styles = new SortedDictionary<int, ILexerStyle>();
                }
                return styles;
            }
        }

        #region Lexer Name to Enum Translation
        public static string GetLexerName(Lexer lexer)
        {
            string lexerName = string.Empty;
            switch (lexer)
            {
                case Lexer.Html:
                    lexerName = "hypertext";
                    break;
                case Lexer.Properties:
                    lexerName = "props";
                    break;
                default:
                    lexerName = lexer.ToString().ToLower();
                    break;
            }
            return lexerName;
        }

        public static Lexer GetLexerFromName(string lexerName)
        {
            Lexer lexer = Lexer.Null;
            switch (lexerName)
            {
                case "hypertext":
                    lexer = Lexer.Html;
                    break;
                case "props":
                    lexer = Lexer.Properties;
                    break;
                default:
                    lexer = (Lexer)Enum.Parse(typeof(Lexer), lexerName, true);
                    break;
            }
            return lexer;
        }
        #endregion
    }
}
