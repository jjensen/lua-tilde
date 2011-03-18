using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Scintilla.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public enum PropertyType
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
    public delegate bool PropertyReadDelegate(FileInfo file, PropertyType propertyType, Queue<string> keyQueue, string key, string val);
    public delegate string PropertyGetValueDelegate(string key);
    public delegate bool PropertyContainsKeyDelegate(string key);

    /// <summary>
    /// A class that reads in ths properties from the 
    /// Justin Greenwood - justin.greenwood@gmail.com
    /// </summary>
    public static class PropertiesReader
    {
        private static Regex regExFindVars = new Regex(@"[\$][\(](?<varname>.*?)[\)]", RegexOptions.Compiled);
        private static Regex regExKeyValPairs = new Regex(@"[\s]*(?:(?<key>[^,]*?)[\s]*:[\s]*(?<value>[^,^:]*[,]*?)|(?<single>[^,^:]*[,]*?))[\s]*", RegexOptions.Compiled);
        private static PropertyGetValueDelegate getValue;
        private static PropertyContainsKeyDelegate containsKey;
        private static int replaceCount = 0;

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
        /// <param name="propertyRead"></param>
        public static void Read(FileInfo propsFileInfo, PropertyReadDelegate propertyRead)
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
                    propertyRead(propsFileInfo, PropertyType.EmptyLine, queue, string.Empty, string.Empty);
                }
                else if (line[start] == '#')
                {
                    propertyRead(propsFileInfo, PropertyType.Comment, queue, "#", line);
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
                        propertyRead(propsFileInfo, PropertyType.EmptyLine, queue, string.Empty, string.Empty);
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
                                ignoreProperties = propertyRead(propsFileInfo, PropertyType.If, queue, key, var);
                            }
                            else if (mode == ReadMode.Import)
                            {
                                // Open another file inline with this one.
                                if (propertyRead(propsFileInfo, PropertyType.Import, queue, key, var))
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
                                propertyRead(propsFileInfo, PropertyType.Property, queue, key, var);
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
                propertyRead(propsFileInfo, PropertyType.Property, queue, key, var);
            }
        }

        #region Evaluate String with embedded variables
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>

        public static string Evaluate(string str, PropertyGetValueDelegate getValueDel, PropertyContainsKeyDelegate containsKeyDel)
        {
            string tmp = str;

            if ((getValueDel != null) && (containsKeyDel != null))
            {
                getValue = getValueDel;
                containsKey = containsKeyDel;

                do
                {
                    replaceCount = 0;
                    tmp = regExFindVars.Replace(tmp, new MatchEvaluator(ReplaceVariableMatch));
                } while (replaceCount > 0);
            }
            return tmp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private static string ReplaceVariableMatch(Match m)
        {
            string output = m.Value;
            if ((getValue != null) && (containsKey != null))
            {
                if ((m.Groups["varname"].Length > 0) && (m.Groups["varname"].Captures.Count > 0))
                {
                    Capture capture = m.Groups["varname"].Captures[0];
                    if (containsKey(capture.Value))
                    {
                        output = getValue(capture.Value);
                    }
                    else
                    {
                        output = string.Empty;
                    }
                    replaceCount++;
                }
            }
            return output;
        }
        #endregion

        #region Parse out "key:value," pairs for those properties that use them.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetKeyValuePairs(string str)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            MatchCollection matches = regExKeyValPairs.Matches(str);
            foreach (Match m in matches)
            {
                if (((m.Groups["key"].Length > 0) && (m.Groups["key"].Captures.Count > 0)) &&
                    ((m.Groups["value"].Length > 0) && (m.Groups["value"].Captures.Count > 0)))
                {
                    string captureKey = m.Groups["key"].Captures[0].Value;
                    string captureValue = m.Groups["value"].Captures[0].Value;

                    if (!string.IsNullOrEmpty(captureKey))
                    {
                        dictionary[captureKey] = (captureValue != null) ? captureValue : Boolean.TrueString;
                    }
                }
                else if ((m.Groups["single"].Length > 0) && (m.Groups["single"].Captures.Count > 0))
                {
                    string captureKey = m.Groups["single"].Captures[0].Value;
                    if (!string.IsNullOrEmpty(captureKey))
                    {
                        dictionary[captureKey] = Boolean.TrueString;
                    }
                }
            }
            return dictionary;
        }
        #endregion
    }
}
