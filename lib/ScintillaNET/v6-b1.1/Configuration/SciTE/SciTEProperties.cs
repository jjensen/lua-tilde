using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Scintilla;
using Scintilla.Enums;
using Scintilla.Configuration;

namespace Scintilla.Configuration.SciTE
{
    /// <summary>
    /// A class that encapsulates the properties
    /// Justin Greenwood - justin.greenwood@gmail.com
    /// </summary>
    public class SciTEProperties : IScintillaConfigProvider
    {
        private static Regex regExFindVars = new Regex(@"[\$][\(](?<varname>.*?)[\)]", RegexOptions.Compiled);
        private static Regex regExKeyValPairs = new Regex(@"[\s]*(?:(?<key>[^,]*?)[\s]*:[\s]*(?<value>[^,^:]*[,]*?)|(?<single>[^,^:]*[,]*?))[\s]*", RegexOptions.Compiled);

        private Dictionary<string, string> properties = new Dictionary<string, string>();
        private Dictionary<string, string> extentionLanguages = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
		private Dictionary<string, string> languageExtentions = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        private List<string> languageNames = new List<string>();
        
        /// <summary>
        /// 
        /// </summary>
        public SciTEProperties()
        {
            // these variables are used in the properties files from Scite for the two different versions.
            properties["PLAT_WIN"] = Boolean.TrueString;
            properties["PLAT_GTK"] = Boolean.FalseString;
        }

        public void Load(FileInfo globalConfigFile)
        {
            s_props = this;
            PropertiesReader.Read(globalConfigFile, PropertyRead);
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string>.KeyCollection Keys
        {
            get { return properties.Keys; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {
                return properties[key];
            }
            set
            {
                if (key.IndexOf("$(") == -1)
                    properties[key] = value;
                else
                    properties[Evaluate(key)] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ext"></param>
        /// <param name="f"></param>
        public void AddFileExtentionMapping(string extensionList, string languageName) 
        {
			if (!languageNames.Contains(languageName))
				languageNames.Add(languageName);

            languageExtentions[languageName] = extensionList;

            string[] exts = extensionList.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string ext in exts)
            {
                string trimExt = ext.TrimStart('*', '.');
                if (!extentionLanguages.ContainsKey(trimExt))
                {
                    extentionLanguages[trimExt] = languageName;
                }
            }
        }

        public void AddLanguageNames(params string[] names) 
        {
            this.languageNames.AddRange(names);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public string GetLanguageFromExtension(string ext)
        {
            string lang = null;
            if (extentionLanguages.ContainsKey(ext))
            {
                lang = extentionLanguages[ext];
            }
            return lang;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public string GetExtensionListFromLanguage(string language)
        {
            string extList = null;
            if (languageExtentions.ContainsKey(language))
            {
                extList = languageExtentions[language];
            }
            return extList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return this.properties.ContainsKey(key);
        }

        public string GetByKey(string key)
        {
            return this[key];
        }

        #region Populate ScintillaConfig data structure
        /// <summary>
        /// Apply the lexer style according to the scite config files
        /// </summary>
        /// <param name="config"></param>
        public bool PopulateScintillaConfig(IScintillaConfig config, string filepattern)
        {
            string key, val, lang;
            int? valInt;
            bool? valBool;

            // Menu Items in the format: (menuString|fileExtension|key|)*
            key = "menu.language";
            if (properties.ContainsKey(key))
            {
                val = this.Evaluate(properties[key]);
                string[] menuItems = val.Split('|');
                for (int i = 2; i < menuItems.Length; i += 3)
                {
                    lang = this.GetLanguageFromExtension(menuItems[i - 1]);

                    MenuItemConfig menuItem = new MenuItemConfig();
                    menuItem.Text = menuItems[i - 2];
                    menuItem.Value = (lang == null) ? menuItems[i - 1] : lang;
                    menuItem.ShortcutKeys = GetKeys(menuItems[i]);

                    config.LanguageMenuItems.Add(menuItem);
                }
            }

            valInt = GetInt("code.page");
            if (valInt.HasValue) config.CodePage = valInt.Value;

            valInt = GetInt("selection.alpha");
            if (valInt.HasValue) config.SelectionAlpha = valInt;

            config.SelectionBackColor = GetColor("selection.back", config.SelectionBackColor);

            config.FileOpenFilter = GetString("open.filter", config.FileOpenFilter);

            config.DefaultFileExtention = GetString("default.file.ext", config.DefaultFileExtention);

			valInt = GetInt("tab.size", filepattern);
			if(!valInt.HasValue)
			{
				valInt = GetInt("tabsize");
				if (valInt.HasValue) config.TabSize = valInt;
			}

            valInt = GetInt("indent.size", filepattern);
            if (valInt.HasValue) config.IndentSize = valInt;

			valBool = GetBool("use.tabs", filepattern);
			if (valBool.HasValue) config.UseTabs = valBool;

            valBool = GetBool("fold");
            if (valBool.HasValue) config.Fold = valBool.Value;

            valBool = GetBool("fold.compact");
            if (valBool.HasValue) config.FoldCompact = valBool.Value;

            valBool = GetBool("fold.symbols");
            if (valBool.HasValue) config.FoldSymbols = valBool.Value;

            valBool = GetBool("fold.comment");
            if (valBool.HasValue) config.FoldComment = valBool.Value;

            valBool = GetBool("fold.on.open");
            if (valBool.HasValue) config.FoldOnOpen = valBool.Value;

            valBool = GetBool("fold.preprocessor");
            if (valBool.HasValue) config.FoldPreprocessor = valBool.Value;

            valBool = GetBool("fold.html");
            if (valBool.HasValue) config.FoldHTML = valBool.Value;

            valBool = GetBool("fold.html.preprocessor");
            if (valBool.HasValue) config.FoldHTMLPreprocessor = valBool.Value;

            valInt = GetInt("fold.flags");
            if (valInt.HasValue) config.FoldFlags = valInt;

            valInt = GetInt("fold.margin.width");
            if (valInt.HasValue) config.FoldMarginWidth = valInt;

            config.FoldMarginColor = GetColor("fold.margin.colour", config.FoldMarginColor);

            config.FoldMarginHighlightColor = GetColor("fold.margin.highlight.colour", config.FoldMarginHighlightColor);

            //tabsize=8
            //indent.size=8
            //default.file.ext=.cxx
            //open.filter
            //code.page
            //selection.alpha=30
            //selection.back=#000000
            //fold=1
            //fold.compact=1
            //fold.flags=16
            //fold.symbols=1
            //#fold.on.open=1
            //fold.comment=1
            //fold.preprocessor=1
            // there are a ton of generic scintilla properties - we will eventually implement them here
            //#fold.margin.width=16
            //#fold.margin.colour=#FF0000
            //#fold.margin.highlight.colour=#0000FF

            //fold.quotes.python
            //fold.perl.package
            //fold.perl.pod
            return true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public bool PopulateLexerConfig(ILexerConfig config)
        {
            /*--------------------------------------- styles ---------------------------------------
            The lexers determine a style number for each lexical type, such as keyword, comment or number. These settings 
            determine the visual style to be used for each style number of each lexer.
            The value of each setting is a set of ',' separated fields, some of which have a subvalue after a ':'. The fields 
            are font, size, fore, back, italics, notitalics, bold, notbold, eolfilled, noteolfilled, underlined, 
            notunderlined, and case. The font field has a subvalue which is the name of the font, the fore and back 
            have colour subvalues, the size field has a numeric size subvalue, the case field has a subvalue of 'm', 
            'u', or 'l' for mixed, upper or lower case, and the bold, italics and eolfilled fields have no subvalue. 
            The value "fore:#FF0000,font:Courier,size:14" represents 14 point, red Courier text.
            A global style can be set up using style.*.stylenumber. Any style options set in the global style will be 
            inherited by each lexer style unless overridden.
            ----------------------------------------------------------------------------------------*/
            string key, s, lexer = config.LexerName.ToLower();
            ILexerStyle style;
            bool dataIsFound = false;
            Dictionary<string, string> dict;

            for (int i = 0; i < 128; i++)
            {
                if (config.Styles.ContainsKey(i)) 
                    style = config.Styles[i];
                else 
                    style = new LexerStyle(i);

                dataIsFound = true;
                foreach (string lang in new string[] { "*", lexer })
                {
                    key = string.Format("style.{0}.{1}", lang, i);
                    if (properties.ContainsKey(key))
                    {
                        dataIsFound = true;

                        s = this.Evaluate(properties[key]);
                        dict = PropertiesReader.GetKeyValuePairs(s);
                        foreach (string styleKey in dict.Keys)
                        {
                            s = dict[styleKey];
                            switch (styleKey)
                            {
                                case "font":
                                    style.FontName = s;
                                    break;
                                case "size":
                                    style.FontSize = Convert.ToInt32(s);
                                    break;
                                case "fore":
                                    style.ForeColor = ColorTranslator.FromHtml(s);
                                    break;
                                case "back":
                                    style.BackColor = ColorTranslator.FromHtml(s);
                                    break;
                                case "italics":
                                    style.Italics = true;
                                    break;
                                case "notitalics":
                                    style.Italics = false;
                                    break;
                                case "bold":
                                    style.Bold = true;
                                    break;
                                case "notbold":
                                    style.Bold = false;
                                    break;
                                case "eolfilled":
                                    style.EOLFilled = true;
                                    break;
                                case "noteolfilled":
                                    style.EOLFilled = false;
                                    break;
                                case "underlined":
                                    style.Underline = true;
                                    break;
                                case "notunderlined":
                                    style.Underline = false;
                                    break;
                                case "case":
                                    style.CaseVisibility = ((s == "m") ? CaseVisible.Mixed : ((s == "u") ? CaseVisible.Upper : CaseVisible.Lower));
                                    break;
                            }
                        }
                    }
                }

                if (dataIsFound) config.Styles[i] = style;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public bool PopulateLanguageConfig(ILanguageConfig config, ILexerConfigCollection lexers)
        {
            bool success = true;
            string key, s, 
                language = config.Name.ToLower(),
                extList = GetExtensionListFromLanguage(language);

            if (extList == null)
            {
                extList = "*." + language;
                languageExtentions[language] = extList;
            }

            config.ExtensionList = extList;

            key = string.Format("lexer.{0}", extList);
            if (properties.ContainsKey(key))
            {
                config.Lexer = lexers[this.Evaluate(properties[key])];
            }

            if (config.Lexer == null) success = false;

            /*--------------------------------------- keywords ---------------------------------------
            Most of the lexers differentiate between names and keywords and use the keywords variables to do so. 
            To avoid repeating the keyword list for each file extension, where several file extensions are 
            used for one language, a keywordclass variable is defined in the distributed properties file 
            although this is just a convention. Some lexers define a second set of keywords which will be 
            displayed in a different style to the first set of keywords. This is used in the HTML lexer 
            to display JavaScript keywords in a different style to HTML tags and attributes.
            ----------------------------------------------------------------------------------------*/
            for (int i = 0; i <= 8; i++)
            {
                s = (i == 0) ? string.Empty : i.ToString();
                key = string.Format("keywords{0}.{1}", s, extList);
                s = GetString(key);
                if (s != null) config.KeywordLists[i] = s;
            }

            /*--------------------------------------- word characters ---------------------------------------
            Defines which characters can be parts of words. The default value here is all the alphabetic and  
            numeric characters and the underscore which is a reasonable value for languages such as C++. 
            ----------------------------------------------------------------------------------------*/
            key = string.Format("word.characters.{0}", extList);
            config.WordCharacters = GetString(key);

            /*--------------------------------------- whitespace characters ---------------------------------------
            Defines which characters are considered whitespace. The default value is that initially set up by Scintilla, 
            which is space and all chars less than 0x20. Setting this property allows you to force Scintilla to consider other 
            characters as whitespace (e.g. punctuation) during such activities as cursor navigation (ctrl+left/right).  
            ----------------------------------------------------------------------------------------*/
            key = string.Format("whitespace.characters.{0}", extList);
            config.WhitespaceCharacters = GetString(key);

            return success;
        }
        #endregion

        #region Helper Methods for pulling data out of the scite config files
        private System.Windows.Forms.Keys GetKeys(string data)
        {
            System.Windows.Forms.Keys shortcutKeys = System.Windows.Forms.Keys.None;
            if (!string.IsNullOrEmpty(data)) 
            {
                string[] keys = data.Split(new char[] {'+'}, StringSplitOptions.RemoveEmptyEntries);
                foreach (string key in keys)
                {
                    shortcutKeys = shortcutKeys | (System.Windows.Forms.Keys)Enum.Parse(
                        typeof(System.Windows.Forms.Keys), 
                        (key.Equals("Ctrl",StringComparison.CurrentCultureIgnoreCase) ? "Control" : key),
                        true);
                }
            }
            return shortcutKeys;
        }

        private string GetString(string key)
        {
            return GetString(key, null);
        }

        private string GetString(string key, string nullValue)
        {
            string val = nullValue;
            if (properties.ContainsKey(key))
            {
                val = this.Evaluate(properties[key]);
            }
            return val;
        }

        private Color GetColor(string key, Color colorIfNull)
        {
            Color val = colorIfNull;
            string sval = GetString(key);
            if (sval != null) val = ColorTranslator.FromHtml(sval);
            return val;
        }

        private int? GetInt(string key, string filepattern)
		{
			int? result = filepattern == null ? null : GetInt(key + "." + filepattern);
			if (!result.HasValue)
				result = GetInt(key);
			return result;
		}

        private int? GetInt(string key)
        {
            int? val = null;
            string sval = GetString(key);
            if (sval != null) val = Convert.ToInt32(sval);
            return val;
        }

		private bool? GetBool(string key, string filepattern)
		{
			bool? result = filepattern == null ? null : GetBool(key + "." + filepattern);
			if (!result.HasValue)
				result = GetBool(key);
			return result;
		}

        private bool? GetBool(string key)
        {
            bool? val = null;
            string sval = GetString(key);
            if (sval != null)
            {
                sval = sval.ToLower();
                switch (sval)
                {
                    case "1":
                    case "y":
                    case "t":
                    case "yes":
                    case "true":
                        val = true;
                        break;
                    default:
                        val = false;
                        break;

                }
            }
            return val;
        }
        #endregion

        #region Evaluate String with embedded variables
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Evaluate(string str)
        {
            return PropertiesReader.Evaluate(str, this.GetByKey, this.ContainsKey);
        }
        #endregion

        #region Special Debug Methods for testing (will delete later)
#if DEBUG
        /// <summary>
        /// This is a method I am using to test the properties file parser
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string val;

            foreach (string key in this.properties.Keys)
            {
                val = properties[key];
                sb.Append(key).Append(" = ").AppendLine(val);
                sb.Append('\t').Append(Evaluate(key)).Append(" = ").AppendLine(Evaluate(val));
            }
            return sb.ToString();
        }
#endif
#endregion

        #region Static Property Reader Methods
        protected static SciTEProperties s_props = null;
        protected static bool s_supressImports = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="propertyType"></param>
        /// <param name="keyQueue"></param>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected static bool PropertyRead(FileInfo file, PropertyType propertyType, Queue<string> keyQueue, string key, string var)
        {
            bool success = false;
            string filePatternPrefix = "file.patterns.";
            string languageNameListPrefix = "language.names";
            string lang, extList;

            if (s_props != null)
            {
                switch (propertyType)
                {
                    case PropertyType.Property:
                        success = true;
                        s_props[key] = var;
                        if (key.StartsWith(languageNameListPrefix))
                        {
                            extList = s_props.Evaluate(var);
                            s_props.AddLanguageNames(var.Split(' '));
                        }
                        else if (key.StartsWith(filePatternPrefix))
                        {
                            lang = key.Substring(filePatternPrefix.Length);
                            if (lang.LastIndexOf('.') == -1)
                            {
                                extList = s_props.Evaluate(var);
                                s_props.AddFileExtentionMapping(extList, lang);
                            }
                        }
                        break;
                    case PropertyType.If:
                        if (s_props.ContainsKey(var))
                        {
                            success = !Convert.ToBoolean(s_props[var]);
                        }
                        break;
                    case PropertyType.Import:
                        if (!s_supressImports)
                        {
                            FileInfo fileToImport = new FileInfo(string.Format(@"{0}\{1}.properties", file.Directory.FullName, var));
                            success = fileToImport.Exists;
                        }
                        break;
                }
            }

            return success;
        }
        #endregion
    }
}
