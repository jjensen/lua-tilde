using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Scintilla.Enums;
using WinForms = System.Windows.Forms;

namespace Scintilla.Configuration
{
    public class MenuItemConfig : IMenuItemConfig
    {
        private string text;
        private string val;
        private WinForms.Keys shortcutKeys;

        public MenuItemConfig() { }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public string Value
        {
            get { return val; }
            set { val = value; }
        }

        public WinForms.Keys ShortcutKeys
        {
            get { return shortcutKeys; }
            set { shortcutKeys = value; }
        }
    }
}
