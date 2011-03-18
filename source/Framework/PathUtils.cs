
/****************************************************************************

Tilde

Copyright (c) 2008 Tantalus Media Pty

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tilde.Framework
{
	public class PathUtils
	{

		/// <summary>
		/// Removes any "." and ".." entries in the path.
		/// </summary>
		/// <param name="input"></param>
		/// <returns>The input filename with no parent or current directory references.</returns>
		public static string MakeCanonicalFileName(string input)
		{
			string drive = input.StartsWith(@"\\") ? @"\\" :System.IO.Path.GetPathRoot(input);
			string dirstring = drive == null ? input : input.Remove(0, drive.Length);
			string[] dirs = dirstring.Split(new char[] { System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar });
			List<string> dirList = new List<string>(dirs);

			for (int index = 0; index < dirList.Count; )
			{
				if (dirList[index] == "." || dirList[index] == "")
				{
					dirList.RemoveAt(index);
				}

				else if (dirList[index] == ".." && index > 0)
				{
					dirList.RemoveAt(index);
					dirList.RemoveAt(index - 1);
					--index;
				}

				else
				{
					++index;
				}
			}

			return drive + String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), dirList.ToArray());
		}

		/// <summary>
		/// Makes the filename relative to the baseName, removes any "." or ".." paths and replaces backslashes with forward slashes.
		/// The result might still be an absolute path if the docName is on a different drive to baseName.
		/// </summary>
		/// <param name="docName"></param>
		/// <param name="baseName"></param>
		/// <returns></returns>
		public static string NormaliseFileName(string docName, string baseName)
		{
			string absName;

			if (Path.IsPathRooted(docName))
				absName = docName;
			else if (baseName != null)
				absName = Path.Combine(baseName, docName);
			else
				absName = Path.GetFullPath(docName);

			string canonicalName = PathUtils.MakeCanonicalFileName(absName);

			string relName;
			if (baseName != null)
				relName = MakeRelativePath(baseName, canonicalName);
			else
				relName = MakeRelativePath(Environment.CurrentDirectory, canonicalName);

			string normName = relName.Replace('\\', '/');
			return normName;
		}

		public static string MakeRelativePath(string from, string to)
		{
			string fromdrive = Path.GetPathRoot(from);
			string[] fromdirs = from.Remove(0, fromdrive.Length).Split(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });

			string todrive = Path.GetPathRoot(to);
			string[] todirs = to.Remove(0, todrive.Length).Split(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });

            // Are they even on the same drive?
            if (String.Compare(fromdrive, todrive, true) != 0)
                return to; 

			StringBuilder result = new StringBuilder();

			for (int index = 0; index < Math.Min(fromdirs.Length, todirs.Length); ++index)
			{
				if (String.Compare(fromdirs[index], todirs[index], true) != 0)
				{
					// The two paths differ somewhere along the way from the start.
					// Go up from the base directory to the point of difference.
					for (int parent = 0; parent < fromdirs.Length - index; ++parent)
						result.Append("../");

					// Go down to the target file
					result.Append(String.Join("/", todirs, index, todirs.Length - index));
					return result.ToString();
				}
			}

			// The two paths are identical, so the file is inside the base directory
			return String.Join("/", todirs, fromdirs.Length, todirs.Length - fromdirs.Length);
		}

		public static bool IsValidFileName(string input)
		{
			string path = "";
			string file = input;

			int fileNamePos = input.LastIndexOfAny(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
			if(fileNamePos >= 0)
			{
				path = input.Substring(0, fileNamePos - 1);
				file = input.Substring(fileNamePos + 1);
			}

			return path.IndexOfAny(Path.GetInvalidPathChars()) < 0 && file.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;
		}

		public static int Compare(string lhs, string rhs)
		{
			lhs = MakeCanonicalFileName(lhs);
			rhs = MakeCanonicalFileName(rhs);
			return String.Compare(lhs, rhs, true);
		}
	}
}
