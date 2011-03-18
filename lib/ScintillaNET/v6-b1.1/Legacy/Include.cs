using System;
using System.Runtime;
using System.Xml.Serialization;

namespace Scintilla.Legacy.Configuration
{
    [SerializableAttribute()]
    public class include : ConfigItem
    {
        [XmlAttributeAttribute()]
        public string file;
    }
}
