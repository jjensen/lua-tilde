using System;
using System.Collections.Generic;
using System.Text;

namespace Scintilla.Configuration
{
    public class LanguageConfig : ILanguageConfig
    {
        private SortedDictionary<int, string> keywordLists;
        private SortedDictionary<int, ILexerStyle> styles;
        private ILexerConfig lexer;
        private string name;
        private string wordCharacters;
        private string whitespaceCharacters;
        private string preprocessorSymbol;
        private string preprocessorStart;
        private string preprocessorMiddle;
        private string preprocessorEnd;
        private string filePattern;
        private string fileFilter;
        private string extension;
        private string extensionList;

        public LanguageConfig(string name)
        {
            this.name = name;
        }

        public ILexerConfig Lexer
        {
            get 
            {
                UpdateStylesFromLexer(false);
                return lexer; 
            }
            set 
            { 
                lexer = value;
                UpdateStylesFromLexer(true);
            }
        }

        public SortedDictionary<int, string> KeywordLists
        {
            get
            {
                if (keywordLists == null)
                {
                    keywordLists = new SortedDictionary<int, string>();
                }
                return keywordLists;
            }
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

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string WordCharacters
        {
            get { return wordCharacters; }
            set { wordCharacters = value; }
        }

        public string WhitespaceCharacters
        {
            get { return whitespaceCharacters; }
            set { whitespaceCharacters = value; }
        }

        public string PreprocessorSymbol
        {
            get { return preprocessorSymbol; }
            set { preprocessorSymbol = value; }
        }

        public string PreprocessorStart
        {
            get { return preprocessorStart; }
            set { preprocessorStart = value; }
        }

        public string PreprocessorMiddle
        {
            get { return preprocessorMiddle; }
            set { preprocessorMiddle = value; }
        }

        public string PreprocessorEnd
        {
            get { return preprocessorEnd; }
            set { preprocessorEnd = value; }
        }

        public string FilePattern
        {
            get { return filePattern; }
            set { filePattern = value; }
        }

        public string FileFilter
        {
            get { return fileFilter; }
            set { fileFilter = value; }
        }

        public string Extension
        {
            get { return extension; }
            set { extension = value; }
        }

        public string ExtensionList
        {
            get { return extensionList; }
            set { extensionList = value; }
        }

        private void UpdateStylesFromLexer(bool settingProperty)
        {
            if (lexer != null)
            {
                if ((!settingProperty && (this.Styles.Count == 0)) ||
                    (settingProperty && (lexer.Styles.Count > 0)))
                {
                    foreach (int key in lexer.Styles.Keys)
                    {
                        if (!this.Styles.ContainsKey(key))
                        {
                            this.Styles[key] = lexer.Styles[key];
                        }
                    }
                }
            }
        }


        /*private string tabSize;
        private string indentSize;
        private string useTabs;
        private string statementIndent;
        private string indentMaintain;
        private string statementEnd;
        private string statementLookback;
        private string blockStart;
        private string blockEnd;
        private string openPath;

        preprocessor.symbol.filepattern
        preprocessor.start.filepattern
        preprocessor.middle.filepattern
        preprocessor.end.filepattern 
        keywords.filepattern
        keywords2.filepattern
        keywords3.filepattern
        keywords4.filepattern
        keywords5.filepattern
        keywords6.filepattern
        keywords7.filepattern
        keywords8.filepattern
        keywords9.filepattern
        word.characters.filepattern 
        whitespace.characters.filepattern 
        tab.size.filepattern
        indent.size.filepattern
        use.tabs.filepattern
        statement.indent.filepattern
        indent.maintain.filepattern 
        statement.end.filepattern
        statement.lookback.filepattern
        block.start.filepattern
        block.end.filepattern 
        extension.filepattern 
        openpath.filepattern*/
    }
}
