using System;
using System.Runtime;
using System.Xml.Serialization;

namespace Scintilla.Legacy.Configuration
{
    [SerializableAttribute()]
    public class Language : ConfigItem
    {
        [XmlAttributeAttribute()]
        public string name;

        public Lexer lexer;

        [XmlElementAttribute(ElementName="file-extensions")]
        public string fileextensions;

        [XmlArrayAttribute("use-keywords")]
        [XmlArrayItemAttribute("keyword")]
        public UseKeyword[] usekeywords;

        [XmlArrayAttribute("use-styles")]
        [XmlArrayItemAttribute("style")]
        public UseStyle[] usestyles;

        public override void init(ConfigurationUtility utility, ConfigFile theParent)
        {
            base.init(utility, theParent);
            if (usekeywords == null)
            {
                usekeywords = new UseKeyword[0];
            }
            if (usestyles == null)
            {
                usestyles = new UseStyle[0];
            }
            for (int j = 0; j < usekeywords.Length; j++)
            {
                usekeywords[j].init(utility, _parent);
            }
            for (int i = 0; i < usestyles.Length; i++)
            {
                usestyles[i].init(utility, _parent);
            }
            if (lexer != null)
            {
                lexer.init(utility, _parent);
            }
        }
    }
}
