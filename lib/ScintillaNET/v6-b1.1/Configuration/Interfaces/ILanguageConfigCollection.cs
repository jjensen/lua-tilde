using System;
using System.Collections.Generic;
using System.Text;

namespace Scintilla.Configuration
{
    public interface ILanguageConfigCollection
    {
        ILanguageConfig this[string name] { get; }
    }
}
