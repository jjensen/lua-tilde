using System;
using System.Runtime;
using System.Xml.Serialization;

namespace Scintilla.Legacy.Configuration
{
    [SerializableAttribute()]
    public class UseStyle : StyleClass
    {
        [XmlAttributeAttribute("class")]
        public string cls;

        public override void init(ConfigurationUtility utility, ConfigFile theParent)
        {
            base.init(utility, theParent);
            if (cls != null && cls.Length > 0)
            {
                inheritstyle = cls;
            }
        }
    }
}
