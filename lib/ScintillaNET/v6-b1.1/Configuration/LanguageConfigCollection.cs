using System;
using System.Collections.Generic;
using System.Text;
using Scintilla.Enums;

namespace Scintilla.Configuration
{
    public class LanguageConfigCollection : ILanguageConfigCollection
    {
        private IScintillaConfigProvider provider;
        private ILexerConfigCollection lexers;
        private IScintillaConfig parent;
        private SortedDictionary<string, LanguageConfig> Languages = new SortedDictionary<string,LanguageConfig>();

        public LanguageConfigCollection(IScintillaConfig parent, IScintillaConfigProvider provider, ILexerConfigCollection lexers)
        {
            if (provider == null)
                throw new Exception("IScintillaConfigProvider must be provided in the LanguageConfigCollection constructor!");
            if (lexers == null)
                throw new Exception("The LexerConfigCollection must be provided in the LanguageConfigCollection constructor!");
            if (parent == null)
                throw new Exception("The ScintillaConfig must be provided in the LanguageConfigCollection constructor!");

            this.parent = parent;
            this.provider = provider;
            this.lexers = lexers;
        }

        public ILanguageConfig this[string name]
        {
            get
            {
                LanguageConfig config = null;
                if (!Languages.ContainsKey(name))
                {
                    config = new LanguageConfig(name);
                    if (provider.PopulateLanguageConfig(config, lexers))
                        Languages[name] = config;
                    else
                        config = null;
                }
                else
                {
                    config = Languages[name];
                }
                return config;
            }
        }
    }
}
