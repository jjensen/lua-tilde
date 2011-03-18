
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
using System.ComponentModel;

namespace Tilde.Framework.Controller
{
	[OptionsCollection(Path = "Application", Editor = typeof(OptionsGridPanel))]
	public class ApplicationOptions : IOptions
	{
		[Category("Misc")]
		[Description("Is the most recently opened project automatically opened upon starting Tilde?")]
		[Option(Location = OptionLocation.Registry, Path = "Application/LoadProjectOnStartup", DefaultValue = true)]
		[DisplayName("Load last project on startup")]
		public bool LoadProjectOnStartup
		{
			get { return m_loadProjectOnStartup; }
			set { if (m_loadProjectOnStartup != value) { m_loadProjectOnStartup = value; OnOptionsChanged("LoadProjectOnStartup"); } }
		}
		private bool m_loadProjectOnStartup;

		[Category("Misc")]
		[Description("When a file is detected as being modified outside of Tilde, will it be automatically reloaded if not modified inside Tilde?")]
		[Option(Location = OptionLocation.Registry, Path = "Application/AutomaticallyReloadModifiedFiles", DefaultValue = true)]
		[DisplayName("Automatically reload modified files")]
		public bool AutomaticallyReloadModifiedFiles
		{
			get { return m_automaticallyReloadModifiedFiles; }
			set { if (m_automaticallyReloadModifiedFiles != value) { m_automaticallyReloadModifiedFiles = value; OnOptionsChanged("AutomaticallyReloadModifiedFiles"); } }
		}
		private bool m_automaticallyReloadModifiedFiles;

		public ApplicationOptions()
		{
		}
	}
}
