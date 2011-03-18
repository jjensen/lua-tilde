using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Scintilla.Configuration.SciTE
{
    /// <summary>
    /// 
    /// </summary>
    public enum SciTEPropertyType
    {
        /// <summary>
        /// 
        /// </summary>
        Property = 0,
        /// <summary>
        /// 
        /// </summary>
        Comment,
        /// <summary>
        /// 
        /// </summary>
        Import,
        /// <summary>
        /// 
        /// </summary>
        If,
        /// <summary>
        /// 
        /// </summary>
        IgnoredLine,
        /// <summary>
        /// 
        /// </summary>
        EmptyLine
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="file"></param>
    /// <param name="propertyType"></param>
    /// <param name="key"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    public delegate bool SciTEPropertyDelegate(FileInfo file, SciTEPropertyType propertyType, Queue<string> keyQueue, string key, string val);

    /// <summary>
    /// A class that reads in ths properties from the 
    /// Justin Greenwood - justin.greenwood@gmail.com
    /// </summary>
    public static class SciTEPropertiesReader
    {
        private static SciTEProperties s_props = null;
        private static bool s_supressImports = true;

        private enum ReadMode
        {
            Key = 0,
            Value,
            Import,
            If,
            FlushWhiteSpace
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propsFileInfo"></param>
        /// <param name="props"></param>
        public static void Read(FileInfo propsFileInfo, SciTEProperties props)
        {
            Read(propsFileInfo, props, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propsFileInfo"></param>
        /// <param name="props"></param>
        /// <param name="supressImports"></param>
        public static void Read(FileInfo propsFileInfo, SciTEProperties props, bool supressImports)
        {
            s_props = props;
            s_supressImports = supressImports;

            Read(propsFileInfo, PropertyRead);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propsFileInfo"></param>
        /// <param name="propertyRead"></param>
        public static void Read(FileInfo propsFileInfo, SciTEPropertyDelegate propertyRead)
        {
            StreamReader reader = new StreamReader(propsFileInfo.OpenRead());

            char c = ' ', prev = '\\';
            int lastStart = 0, ignoreCount = 0;
            bool ignoreProperties = false;
            string key = null, var = null;
            StringBuilder currentVar = new StringBuilder();
            StringBuilder currentToken = new StringBuilder();

            Queue<string> queue = new Queue<string>();
            StringBuilder currentTokenPiece = new StringBuilder();

            ReadMode mode = ReadMode.Key;
            ReadMode nextModeAfterSpaces = ReadMode.Key;

            string line = reader.ReadLine();
            while (line != null)
            {
                int start = 0;
                bool skipLine = false;

                while ((start < line.Length) && char.IsWhiteSpace(line[start])) ++start;

                if (start >= line.Length)
                {
                    propertyRead(propsFileInfo, SciTEPropertyType.EmptyLine, queue, string.Empty, string.Empty);
                }
                else if (line[start] == '#')
                {
                    propertyRead(propsFileInfo, SciTEPropertyType.Comment, queue, "#", line);
                }
                else
                {
                    if (ignoreProperties)
                    {
                        if ((ignoreCount == 0) || (start == lastStart))
                        {
                            ignoreCount++;
                            lastStart = start;
                            skipLine = true;
                        }
                        else
                        {
                            ignoreCount = 0;
                            ignoreProperties = false;
                        }
                    }

                    if (skipLine)
                    {
                        propertyRead(propsFileInfo, SciTEPropertyType.EmptyLine, queue, string.Empty, string.Empty);
                    }
                    else
                    {
                        for (int i = start; i < line.Length; i++)
                        {
                            c = line[i];

                            if (mode == ReadMode.Key)
                            {
                                if (c == '=')
                                {
                                    if (currentTokenPiece.Length > 0)
                                    {
                                        queue.Enqueue(currentTokenPiece.ToString());
                                    }

                                    currentTokenPiece.Remove(0, currentTokenPiece.Length);

                                    key = currentToken.ToString();
                                    currentToken.Remove(0, currentToken.Length);

                                    mode = ReadMode.Value;
                                    continue;
                                }
                                else if (char.IsWhiteSpace(c))
                                {
                                    key = currentToken.ToString();
                                    currentToken.Remove(0, currentToken.Length);
                                    currentTokenPiece.Remove(0, currentTokenPiece.Length);

                                    if (key == "if")
                                    {
                                        nextModeAfterSpaces = ReadMode.If;
                                    }
                                    else if (key == "import")
                                    {
                                        nextModeAfterSpaces = ReadMode.Import;
                                    }
                                    else
                                    {
                                        break;
                                    }

                                    mode = ReadMode.FlushWhiteSpace;
                                    continue;
                                }
                                else if (c == '.')
                                {
                                    currentToken.Append(c);

                                    queue.Enqueue(currentTokenPiece.ToString());
                                    currentTokenPiece.Remove(0, currentTokenPiece.Length);
                                }
                                else
                                {
                                    currentTokenPiece.Append(c);
                                    currentToken.Append(c);
                                }
                            }
                            else if (mode == ReadMode.FlushWhiteSpace)
                            {
                                if (!char.IsWhiteSpace(c))
                                {
                                    currentToken.Append(c);
                                    mode = nextModeAfterSpaces;
                                }
                            }
                            else if (mode == ReadMode.Import)
                            {
                                currentToken.Append(c);
                            }
                            else if (mode == ReadMode.If)
                            {
                                currentToken.Append(c);
                            }
                            else if (mode == ReadMode.Value)
                            {
                                currentToken.Append(c);
                            }
                            prev = c;
                        }

                        if (prev != '\\')
                        {
                            var = currentToken.ToString();
                            if (mode == ReadMode.If)
                            {
                                ignoreProperties = propertyRead(propsFileInfo, SciTEPropertyType.If, queue, key, var);
                            }
                            else if (mode == ReadMode.Import)
                            {
                                // Open another file inline with this one.
                                if (propertyRead(propsFileInfo, SciTEPropertyType.Import, queue, key, var))
                                {
                                    FileInfo fileToImport = new FileInfo(string.Format(@"{0}\{1}.properties", propsFileInfo.Directory.FullName, var));
                                    if (fileToImport.Exists)
                                    {
                                        Read(fileToImport, propertyRead);
                                    }
                                }
                            }
                            else if (mode == ReadMode.Value)
                            {
                                propertyRead(propsFileInfo, SciTEPropertyType.Property, queue, key, var);
                            }
                            currentToken.Remove(0, currentToken.Length);
                            queue.Clear();
                            key = null;
                            mode = ReadMode.Key;
                        }
                        else
                        {
                            currentToken.Remove(currentToken.Length - 1, 1);
                        }
                    }
                }
                line = reader.ReadLine();
            }
            reader.Close();

            if (key != null)
            {
                var = currentToken.ToString();
                propertyRead(propsFileInfo, SciTEPropertyType.Property, queue, key, var);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="propertyType"></param>
        /// <param name="keyQueue"></param>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool PropertyRead(FileInfo file, SciTEPropertyType propertyType, Queue<string> keyQueue, string key, string var)
        {
            bool success = false;
            string filePatternPrefix = "file.patterns.";
            string languageNameListPrefix = "language.names";
            string lang, extList;

            if (s_props != null)
            {
                switch (propertyType)
                {
                    case SciTEPropertyType.Property:
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
                    case SciTEPropertyType.If:
                        if (s_props.ContainsKey(var))
                        {
                            success = !Convert.ToBoolean(s_props[var]);
                        }
                        break;
                    case SciTEPropertyType.Import:
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
    }
}
