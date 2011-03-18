using System;
using System.Collections.Generic;
using System.Text;
using Scintilla.Enums;

namespace Scintilla.Configuration
{
    public class LexerConfigCollection : ILexerConfigCollection
    {
        private IScintillaConfigProvider provider;
        private IScintillaConfig parent;
        private SortedDictionary<int, LexerConfig> lexers = new SortedDictionary<int, LexerConfig>();

        public LexerConfigCollection(IScintillaConfig parent, IScintillaConfigProvider provider)
        {
            if (provider == null)
                throw new Exception("IScintillaConfigProvider must be provided to the LexerConfigCollection constructor!");
            if (parent == null)
                throw new Exception("The ScintillaConfig must be provided in the LexerConfigCollection constructor!");

            this.parent = parent;
            this.provider = provider;
        }

        public ILexerConfig this[int lexerId]
        {
            get
            {
                LexerConfig config = null;
                if (!lexers.ContainsKey(lexerId))
                {
                    config = new LexerConfig(lexerId);
                    AddDefaultStylesToLexer(config);

                    if (provider.PopulateLexerConfig(config))
                        lexers[lexerId] = config;
                    else
                        config = null;
                }
                else
                {
                    config = lexers[lexerId];
                }
                return config;
            }
        }

        public ILexerConfig this[Lexer lexer]
        {
            get
            {
                return this[(int)lexer];
            }
        }

        public ILexerConfig this[string lexerName]
        {
            get
            {
                return this[(int)LexerConfig.GetLexerFromName(lexerName)];
            }
        }

        private void AddDefaultStylesToLexer(ILexerConfig lexer)
        {
            if (parent.Styles.Count > 0)
            {
                foreach (int key in parent.Styles.Keys)
                {
                    lexer.Styles[key] = parent.Styles[key];
                }
            }
        }
    }
}
