using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Scintilla.Enums;

namespace Scintilla.Configuration
{
    public class ScintillaConfig : IScintillaConfig
    {
        private IScintillaConfigProvider provider;
        private ILexerConfigCollection lexers;
        private ILanguageConfigCollection languages;
        private SortedDictionary<string, string> properties;
        private SortedDictionary<int, ILexerStyle> styles;
        private List<string> languageNames;
        private List<IMenuItemConfig> languageMenuItems;
        private Color selectionBackColor = Color.Empty;
        private Color foldMarginColor = Color.Empty;
        private Color foldMarginHighlightColor = Color.Empty;
        private int? codePage;
        private bool? fold;
        private bool? foldCompact;
        private bool? foldComment;
        private bool? foldPreprocessor;
        private bool? foldSymbols;
        private bool? foldOnOpen;
        private bool? foldHTML;
        private bool? foldHTMLPreprocessor;
        private int? foldFlags;
        private int? foldMarginWidth;
        private int? selectionAlpha;
        private int? tabSize;
        private int? indentSize;
		private bool? useTabs;
		private string defaultFileExtention;
        private string fileOpenFilter;


		public ScintillaConfig(IScintillaConfigProvider provider, string filepattern) 
        {
            if (provider == null)
                throw new Exception("IScintillaConfigProvider must be provided to the ScintillaConfig constructor!");

            this.provider = provider;
			provider.PopulateScintillaConfig(this, filepattern);
        }

        public SortedDictionary<string, string> Properties
        {
            get
            {
                if (properties == null)
                {
                    properties = new SortedDictionary<string, string>();
                } 
                return properties;
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

        public ILanguageConfigCollection Languages
        {
            get
            {
                if (languages == null)
                {
                    languages = new LanguageConfigCollection(this, provider, Lexers);
                }
                return languages;
            }
        }

        public List<string> LanguageNames
        {
            get
            {
                if (languageNames == null)
                {
                    languageNames = new List<string>();
                }
                return languageNames;
            }
        }

        public ILexerConfigCollection Lexers
        {
            get 
            {
                if (lexers == null)
                {
                    lexers = new LexerConfigCollection(this, provider);
                }
                return lexers; 
            }
        }

        public int? CodePage
        {
            get { return codePage; }
            set { codePage = value; }
        }

        public int? TabSize
        {
            get { return tabSize; }
            set { tabSize = value; }
        }

        public int? IndentSize
        {
            get { return indentSize; }
            set { indentSize = value; }
        }

        public bool? UseTabs
        {
            get { return useTabs; }
            set { useTabs = value; }
        }

        //TODO: move folding into a subclass gouping if enabled?
        public bool? Fold
        {
            get { return fold; }
            set { fold = value; }
        }

        public bool? FoldCompact
        {
            get { return foldCompact; }
            set { foldCompact = value; }
        }

        public int? FoldFlags
        {
            get { return foldFlags; }
            set { foldFlags = value; }
        }

        public bool? FoldComment
        {
            get { return foldComment; }
            set { foldComment = value; }
        }

        public bool? FoldPreprocessor
        {
            get { return foldPreprocessor; }
            set { foldPreprocessor = value; }
        }

        public bool? FoldSymbols
        {
            get { return foldSymbols; }
            set { foldSymbols = value; }
        }

        public bool? FoldOnOpen
        {
            get { return foldOnOpen; }
            set { foldOnOpen = value; }
        }

        public bool? FoldHTML
        {
            get { return foldHTML; }
            set { foldHTML = value; }
        }

        public bool? FoldHTMLPreprocessor
        {
            get { return foldHTMLPreprocessor; }
            set { foldHTMLPreprocessor = value; }
        }

        public int? FoldMarginWidth
        {
            get { return foldMarginWidth; }
            set { foldMarginWidth = value; }
        }

        public Color FoldMarginColor
        {
            get { return foldMarginColor; }
            set { foldMarginColor = value; }
        }

        public Color FoldMarginHighlightColor
        {
            get { return foldMarginHighlightColor; }
            set { foldMarginHighlightColor = value; }
        }

        //TODO: move selection into a subclass gouping if enabled?
        public int? SelectionAlpha
        {
            get { return selectionAlpha; }
            set { selectionAlpha = value; }
        }

        public Color SelectionBackColor
        {
            get { return selectionBackColor; }
            set { selectionBackColor = value; }
        }

        //SciDE Specific Properties, should be moved to a different config file?
        public string DefaultFileExtention
        {
            get { return defaultFileExtention; }
            set { defaultFileExtention = value; }
        }

        public string FileOpenFilter
        {
            get { return fileOpenFilter; }
            set { fileOpenFilter = value; }
        }

        public List<IMenuItemConfig> LanguageMenuItems
        {
            get
            {
                if (languageMenuItems == null)
                {
                    languageMenuItems = new List<IMenuItemConfig>();
                }
                return languageMenuItems;
            }
        }
    }
}
