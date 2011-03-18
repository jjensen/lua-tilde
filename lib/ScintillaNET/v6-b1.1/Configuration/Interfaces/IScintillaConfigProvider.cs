using System;
using System.Collections.Generic;
using System.Text;

namespace Scintilla.Configuration
{
    public interface IScintillaConfigProvider
    {
		bool PopulateScintillaConfig(IScintillaConfig config, string filepattern);
        bool PopulateLexerConfig(ILexerConfig config);
        bool PopulateLanguageConfig(ILanguageConfig config, ILexerConfigCollection lexers);
    }
}
