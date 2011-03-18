using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Permissions;

using Scintilla.Enums;
using Scintilla.Configuration;
using System.Resources;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Scintilla
{
    internal enum VOID
    {
        NULL
    }

    public partial class ScintillaControl : Control
    {
        static ScintillaControl()
        {
            // setup Enum-based-indexers
            Collection<IndicatorStyle>.Setup(2080, 2081);
        }

        public const string DefaultDllName = "SciLexer.dll";
        private static readonly object _nativeEventKey = new object();
        private const int WM_KEYDOWN = 0x0100;
		private const int WM_SYSKEYDOWN = 0x0104;

        private Encoding _encoding;
        private string _sciLexerDllName = null;
        private Scintilla.Legacy.Configuration.Scintilla _legacyConfiguration;
        private EventHandler<CharAddedEventArgs> _smartIndenting = null;
        private IScintillaConfig _configuration;
        private string _configurationLanguage = null;
        private Dictionary<int, int> _ignoredKeys = new Dictionary<int, int>();
        
        public ScintillaControl()
            : this(DefaultDllName)
        {
        }

        public ScintillaControl(string sciLexerDllName)
        {
            _sciLexerDllName = sciLexerDllName;

            // Instantiate the indexers for this instance
            IndicatorStyle = new Collection<IndicatorStyle>(this);
            IndicatorForegroundColor = new IntCollection(this);
            MarkerForegroundColor = new CachingIntCollection(this);
            MarkerBackgroundColor = new CachingIntCollection(this);
            Line = new ReadOnlyStringCollection(this);

            // setup instance-based-indexers
            IndicatorForegroundColor.Setup(2082, 2083);
            MarkerForegroundColor.Setup(2041);
            MarkerBackgroundColor.Setup(2042);
            Line.Setup(2153,2350);
            
            // Set up default encoding
            _encoding = Encoding.GetEncoding(this.CodePage);
            
            InitializeComponent();
        }

        public override string Text
        {
            get
            {
                return this.GetText();
            }
            set
            {
                this.SetText(value);
            }
        }

		public void UseMonospaceFont()
		{
			UseMonospaceFont("font:Courier New,size:10");
		}

		public void UseMonospaceFont(string fontinfo)
		{
			this.StyleResetDefault();
			Regex regex = new Regex("font:(.*),size:(.*)");
			Match match = regex.Match(fontinfo);
			if (match.Success)
			{
				string fontname = match.Groups[1].Value;
				int fontsize = Int32.Parse(match.Groups[2].Value);

				for (int style = 0; style <= (int)Scintilla.Enums.StylesCommon.Max; ++style)
				{
					if (style != (int)Scintilla.Enums.StylesCommon.LineNumber)
					{
						this.StyleSetFont(style, fontname);
						this.StyleSetSize(style, fontsize);
					}
				}
			}
		}


        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                //	Otherwise Scintilla won't paint. When UserPaint is set to
                //	true the base Class (Control) eats the WM_PAINT message.
                //	Of course when this set to false we can't use the Paint
                //	events. This is why I'm relying on the Paint notification
                //	sent from scintilla to paint the Marker Arrows.
                SetStyle(ControlStyles.UserPaint, false);

                //	Registers the Scintilla Window Class
                //	I'm relying on the fact that a version specific renamed
                //	SciLexer exists either in the Current Dir or a global path
                //	(See LoadLibrary Windows API Search Rules)

				IntPtr handle = NativeMethods.LoadLibrary(_sciLexerDllName);
				if (handle == IntPtr.Zero)
				{
					throw new Exception(
							"Could not load Scintilla Lexer DLL '" + _sciLexerDllName
						+	"'; current working directory is:\r\n" + System.IO.Directory.GetCurrentDirectory());
				}

                //	Tell Windows Forms to create a Scintilla
                //	derived Window Class for this control
                CreateParams cp = base.CreateParams;
                cp.ClassName = "Scintilla";
                return cp;
            }
		}
		
        #region Event Dispatch Mechanism
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            //	Uh-oh. Code based on undocumented unsupported .NET behavior coming up!
            //	Windows Forms Sends Notify messages back to the originating
            //	control ORed with 0x2000. This is way cool becuase we can listen for
            //	WM_NOTIFY messages originating form our own hWnd (from Scintilla)
            if ((m.Msg ^ 0x2000) != NativeMethods.WM_NOTIFY)
            {
                base.WndProc(ref m);
                return;
            }

            SCNotification scnotification = (SCNotification)Marshal.PtrToStructure(m.LParam, typeof(SCNotification));
            // dispatch to listeners of the native event first
            // this allows listeners to get the raw event if they really wish
            // but ideally, they'd just use the .NET event 
            if (Events[_nativeEventKey] != null)
                ((EventHandler<NativeScintillaEventArgs>)Events[_nativeEventKey])(this, new NativeScintillaEventArgs(m, scnotification));

            DispatchScintillaEvent(scnotification);
            base.WndProc(ref m);

        }
        
        protected event EventHandler<NativeScintillaEventArgs> NativeScintillaEvent
        {
            add { Events.AddHandler(_nativeEventKey, value); }
            remove { Events.RemoveHandler(_nativeEventKey, value); }
        }
        #endregion

        #region SendMessageDirect 

        /// <summary>
        /// This is the primary Native communication method with Scintilla
        /// used by this control. All the other overloads call into this one.
        /// </summary>
        internal IntPtr SendMessageDirect(uint msg, IntPtr wParam, IntPtr lParam)
        {
            Message m = new Message();
            m.Msg = (int)msg;
            m.WParam = wParam;
            m.LParam = lParam;
            m.HWnd = Handle;

            //  DefWndProc is the Window Proc associated with the window
            //  class for this control created by Windows Forms. It will
            //  in turn call Scintilla's DefWndProc Directly. This has 
            //  the same net effect as using Scintilla's DirectFunction
            //  in that SendMessage isn't used to get the message to 
            //  Scintilla but requires 1 less PInvoke and I don't have
            //  to maintain the FunctionPointer and "this" reference
            DefWndProc(ref m);
            return m.Result;
        }

        //  Various overloads provided for syntactical convinience.
        //  note that the return value is int (32 bit signed Integer). 
        //  If you are invoking a message that returns a pointer or
        //  handle like SCI_GETDIRECTFUNCTION or SCI_GETDOCPOINTER
        //  you MUST use the IntPtr overload to ensure 64bit compatibility

        /// <summary>
        /// Handles Scintilla Call Style:
        ///    (,)
        /// </summary>
        /// <param name="msg">Scintilla Message Number</param>
        /// <returns></returns>
        internal int SendMessageDirect(uint msg)
        {
            return (int)SendMessageDirect(msg, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        /// Handles Scintilla Call Style:
        ///    (int,int)    
        /// </summary>
        /// <param name="msg">Scintilla Message Number</param>
        /// <param name="wParam">wParam</param>
        /// <param name="lParam">lParam</param>
        /// <returns></returns>
        internal int SendMessageDirect(uint msg, int wParam, int lParam)
        {
            return (int)SendMessageDirect(msg, (IntPtr)wParam, (IntPtr)lParam);
        }

        /// <summary>
        /// Handles Scintilla Call Style:
        ///    (int,)    
        /// </summary>
        /// <param name="msg">Scintilla Message Number</param>
        /// <param name="wParam">wParam</param>
        /// <returns></returns>
        internal int SendMessageDirect(uint msg, int wParam)
        {
            return (int)SendMessageDirect(msg, (IntPtr)wParam, IntPtr.Zero);
        }

        /// <summary>
        /// Handles Scintilla Call Style:
        ///    (,int)    
        /// </summary>
        /// <param name="msg">Scintilla Message Number</param>
        /// <param name="NULL">always pass null--Unused parameter</param>
        /// <param name="lParam">lParam</param>
        /// <returns></returns>
        internal int SendMessageDirect(uint msg, VOID NULL, int lParam)
        {
            return (int)SendMessageDirect(msg, IntPtr.Zero, (IntPtr)lParam);
        }

        
        /// <summary>
        /// Handles Scintilla Call Style:
        ///    (bool,int)    
        /// </summary>
        /// <param name="msg">Scintilla Message Number</param>
        /// <param name="wParam">boolean wParam</param>
        /// <param name="lParam">int lParam</param>
        /// <returns></returns>
        internal int SendMessageDirect(uint msg, bool wParam, int lParam)
        {
            return (int)SendMessageDirect(msg, (IntPtr)(wParam ? 1 : 0), (IntPtr)lParam);
        }

        /// <summary>
        /// Handles Scintilla Call Style:
        ///    (bool,)    
        /// </summary>
        /// <param name="msg">Scintilla Message Number</param>
        /// <param name="wParam">boolean wParam</param>
        /// <returns></returns>
        internal int SendMessageDirect(uint msg, bool wParam)
        {
            return (int)SendMessageDirect(msg, (IntPtr)(wParam ? 1 : 0), IntPtr.Zero);
        }

        /// <summary>
        /// Handles Scintilla Call Style:
        ///    (int,bool)    
        /// </summary>
        /// <param name="msg">Scintilla Message Number</param>
        /// <param name="wParam">int wParam</param>
        /// <param name="lParam">boolean lParam</param>
        /// <returns></returns>
        internal int SendMessageDirect(uint msg, int wParam, bool lParam)
        {
            return (int)SendMessageDirect(msg, (IntPtr)wParam, (IntPtr)(lParam ? 1 : 0));
        }

        /// <summary>
        /// Handles Scintilla Call Style:
        ///    (,stringresult)    
        /// Notes:
        ///  Helper method to wrap all calls to messages that take a char*
        ///  in the lParam and returns a regular .NET String. This overload
        ///  assumes there will be no wParam and obtains the string length
        ///  by calling the message with a 0 lParam. 
        /// </summary>
        /// <param name="msg">Scintilla Message Number</param>
        /// <param name="text">String output</param>
        /// <returns></returns>
        internal int SendMessageDirect(uint msg, out string text)
        {
            int length = SendMessageDirect(msg, 0, 0);
            return SendMessageDirect(msg, IntPtr.Zero, out text, length);
        }

        /// <summary>
        /// Handles Scintilla Call Style:
        ///    (int,stringresult)    
        /// Notes:
        ///  Helper method to wrap all calls to messages that take a char*
        ///  in the lParam and returns a regular .NET String. This overload
        ///  assumes there will be no wParam and obtains the string length
        ///  by calling the message with a 0 lParam. 
        /// </summary>
        /// <param name="msg">Scintilla Message Number</param>
        /// <param name="text">String output</param>
        /// <returns></returns>
        internal int SendMessageDirect(uint msg, int wParam, out string text)
        {
            int length = SendMessageDirect(msg, 0, 0);
            return SendMessageDirect(msg, (IntPtr)wParam, out text, length);
        }

        /// <summary>
        /// Handles Scintilla Call Style:
        ///    (?)    
        /// Notes:
        ///  Helper method to wrap all calls to messages that take a char*
        ///  in the wParam and set a regular .NET String in the lParam. 
        ///  Both the length of the string and an additional wParam are used 
        ///  so that various string Message styles can be acommodated.
        /// </summary>
        /// <param name="msg">Scintilla Message Number</param>
        /// <param name="wParam">int wParam</param>
        /// <param name="text">String output</param>
        /// <param name="length">length of the input buffer</param>
        /// <returns></returns>
        internal unsafe int SendMessageDirect(uint msg, IntPtr wParam, out string text, int length)
        {
            IntPtr ret;

            //  Allocate a buffer the size of the string + 1 for 
            //  the NULL terminator. Scintilla always sets this
            //  regardless of the encoding
            byte[] buffer = new byte[length + 1];

            //  Get a direct pointer to the the head of the buffer
            //  to pass to the message along with the wParam. 
            //  Scintilla will fill the buffer with string data.
            fixed (byte* bp = buffer)
            {
                ret = SendMessageDirect(msg, wParam, (IntPtr)bp);

                //	If this string is NULL terminated we want to trim the
                //	NULL before converting it to a .NET String
                if (bp[length - 1] == 0)
                    length--;
            }


            //  We always assume UTF8 encoding to ensure maximum
            //  compatibility. Manually changing the encoding to 
            //  something else will cuase 2 Byte characters to
            //  be interpreted as junk.
            text = _encoding.GetString(buffer, 0, length);

            return (int)ret;
        }


        
        /// <summary>
        /// Handles Scintilla Call Style:
        ///    (int,string)    
        /// Notes:
        ///  This helper method handles all messages that take
        ///  const char* as an input string in the lParam. In
        ///  some messages Scintilla expects a NULL terminated string
        ///  and in others it depends on the string length passed in
        ///  as wParam. This method handles both situations and will
        ///  NULL terminate the string either way. 
        /// 
        /// </summary>
        /// <param name="msg">Scintilla Message Number</param>
        /// <param name="wParam">int wParam</param>
        /// <param name="lParam">string lParam</param>
        /// <returns></returns>
        internal unsafe int SendMessageDirect(uint msg, int wParam, string lParam)
        {
            //  Just as when retrieving we make to convert .NET's
            //  UTF-16 strings into a UTF-8 encoded byte array.
            fixed (byte* bp = _encoding.GetBytes(ZeroTerminated(lParam)))
                return (int)SendMessageDirect(msg, (IntPtr)wParam, (IntPtr)bp);
        }

        /// <summary>
        /// Handles Scintilla Call Style:
        ///    (,string)    
        /// 
        /// Notes:
        ///  This helper method handles all messages that take
        ///  const char* as an input string in the lParam. In
        ///  some messages Scintilla expects a NULL terminated string
        ///  and in others it depends on the string length passed in
        ///  as wParam. This method handles both situations and will
        ///  NULL terminate the string either way. 
        /// 
        /// </summary>
        /// <param name="msg">Scintilla Message Number</param>
        /// <param name="NULL">always pass null--Unused parameter</param>
        /// <param name="lParam">string lParam</param>
        /// <returns></returns>
        internal unsafe int SendMessageDirect(uint msg, VOID NULL, string lParam)
        {
            //  Just as when retrieving we make to convert .NET's
            //  UTF-16 strings into a UTF-8 encoded byte array.
            fixed (byte* bp = _encoding.GetBytes(ZeroTerminated(lParam)))
                return (int)SendMessageDirect(msg, IntPtr.Zero, (IntPtr)bp);
        }


     

        
        /// <summary>
        /// Handles Scintilla Call Style:
        ///    (string,string)    
        /// 
        /// Notes:
        ///    Used by SCI_SETPROPERTY
        /// </summary>
        /// <param name="msg">Scintilla Message Number</param>
        /// <param name="wParam">string wParam</param>
        /// <param name="lParam">string lParam</param>
        /// <returns></returns>
        internal unsafe int SendMessageDirect(uint msg, string wParam, string lParam)
        {
            fixed (byte* bpw = _encoding.GetBytes(ZeroTerminated(wParam)))
            fixed (byte* bpl = _encoding.GetBytes(ZeroTerminated(lParam)))
                return (int)SendMessageDirect(msg, (IntPtr)bpw, (IntPtr)bpl);
        }

        /// <summary>
        /// Handles Scintilla Call Style:
        ///    (string,stringresult)    
        /// 
        /// Notes:
        ///  This one is used specifically by SCI_GETPROPERTY and SCI_GETPROPERTYEXPANDED
        ///  so it assumes it's usage
        /// 
        /// </summary>
        /// <param name="msg">Scintilla Message Number</param>
        /// <param name="wParam">string wParam</param>
        /// <param name="stringResult">Stringresult output</param>
        /// <returns></returns>
        internal unsafe int SendMessageDirect(uint msg, string wParam, out string stringResult)
        {
            IntPtr ret;

            fixed (byte* bpw = _encoding.GetBytes(ZeroTerminated(wParam)))
            {
                int length = (int)SendMessageDirect(msg, (IntPtr)bpw, IntPtr.Zero);


                byte[] buffer = new byte[length + 1];

                fixed (byte* bpl = buffer)
                    ret = SendMessageDirect(msg, (IntPtr)bpw, (IntPtr)bpl);

                stringResult = _encoding.GetString(buffer, 0, length);
            }

            return (int)ret;
        }
   
        /// <summary>
        /// Handles Scintilla Call Style:
        ///    (string,int)    
        /// </summary>
        /// <param name="msg">Scintilla Message Number</param>
        /// <param name="wParam">string wParam</param>
        /// <param name="lParam">int lParam</param>
        /// <returns></returns>
        internal unsafe int SendMessageDirect(uint msg, string wParam, int lParam)
        {
            fixed (byte* bp = _encoding.GetBytes(ZeroTerminated(wParam)))
                return (int)SendMessageDirect(msg, (IntPtr)bp, (IntPtr)lParam);
        }

        /// <summary>
        /// Handles Scintilla Call Style:
        ///    (string,)    
        /// </summary>
        /// <param name="msg">Scintilla Message Number</param>
        /// <param name="wParam">string wParam</param>
        /// <returns></returns>
        internal unsafe int SendMessageDirect(uint msg, string wParam)
        {
            fixed (byte* bp = _encoding.GetBytes(ZeroTerminated(wParam)))
                return (int)SendMessageDirect(msg, (IntPtr)bp, IntPtr.Zero);
        }

        private static String ZeroTerminated(string param)
        {
            if (string.IsNullOrEmpty(param))
                return "\0";
            else if (!param.EndsWith("\0"))
                return param + "\0";
            return param;
        }
        #endregion

        #region Hand crafted members
		// Function void AddStyledText(int,cells) skipped.
		unsafe public void AddStyledText(int length, byte[] s)
		{
			fixed(byte* bp = s)
				SendMessageDirect(2002, (IntPtr)length, (IntPtr)bp);
		}


		// Function int GetStyledText(,textrange) skipped.
		unsafe public void GetStyledText(ref TextRange tr)
		{
			fixed(TextRange* trp = &tr)
				SendMessageDirect(2015, IntPtr.Zero, (IntPtr)trp);
		}

		// Function position FindText(int,findtext) skipped.
		unsafe public int FindText(int searchFlags, ref TextToFind ttf)
		{
			fixed(TextToFind* ttfp = &ttf)
				return (int)SendMessageDirect(2150, IntPtr.Zero, (IntPtr)ttfp);
		}

		// Function position FormatRange(bool,formatrange) skipped.
		unsafe public void FormatRange(bool bDraw, ref RangeToFormat pfr)
		{
			fixed(RangeToFormat* rtfp = &pfr)
				SendMessageDirect(2151, IntPtr.Zero, (IntPtr)rtfp);
		}

		// Function int GetTextRange(,textrange) skipped.
		unsafe public int GetTextRange(ref TextRange tr)
		{
			fixed(TextRange* trp = &tr)
				return (int)SendMessageDirect(2162, IntPtr.Zero, (IntPtr)trp);
		}

		public char CharAt(int position)
		{
			return (char)SendMessageDirect(2007, position, 0);
		}

		public IntPtr DocPointer()
		{
			return SendMessageDirect(2357, IntPtr.Zero, IntPtr.Zero);
		}

		public IntPtr CreateDocument()
		{
			return SendMessageDirect(2375, IntPtr.Zero, IntPtr.Zero);
		}

		public void AddRefDocument(IntPtr pDoc)
		{
			SendMessageDirect(2376, IntPtr.Zero, pDoc);
		}

		public void ReleaseDocument(IntPtr pDoc)
		{
			SendMessageDirect(2377, IntPtr.Zero, pDoc);
		}

		public void AssignCmdKey(System.Windows.Forms.Keys keyDefinition, uint sciCommand)
		{
			SendMessageDirect(2070, (int)keyDefinition, (int)sciCommand);
		}

		public void ClearCmdKey(System.Windows.Forms.Keys keyDefinition)
		{
			SendMessageDirect(2071, (int)keyDefinition, 0);
		}

        /// <summary>
        /// Retrieve all the text in the document. Returns number of characters retrieved. 
        /// </summary>
        public virtual string GetText()
        {
            string result;
            int length = SendMessageDirect(2182, 0, 0);
            this.SendMessageDirect(2182, length, out result);
            return result;
        }


        /// <summary>
        /// Get the code page used to interpret the bytes of the document as characters. 
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int CodePage
        {
            get
            {
                return this.SendMessageDirect(2137);
            }
            set
            {
                this.SendMessageDirect(2037, value);
                this._encoding = Encoding.GetEncoding(value);
            }
        }

        /// <summary>
        /// Are white space characters currently visible? Returns one of SCWS_* constants. 
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Enums.WhiteSpace ViewWhitespace
        {
            get
            {
                return (Enums.WhiteSpace)this.SendMessageDirect(2020);
            }
            set
            {
                this.SendMessageDirect(2021, (int)value);
            }
        }

        /// <summary>
        /// Retrieve the current end of line mode - one of CRLF, CR, or LF. 
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Enums.EndOfLine EndOfLineMode
        {
            get
            {
                return (Enums.EndOfLine)this.SendMessageDirect(2030);
            }
            set
            {
                this.SendMessageDirect(2031, (int)value);
            }
        }

        /// <summary>
        /// Convert all line endings in the document to one mode.
        /// </summary>
        public void ConvertEOLs(Enums.EndOfLine eolMode)
        {
            this.SendMessageDirect(2029, (int) eolMode);
        }

        /// <summary>
        /// Set the symbol used for a particular marker number.
        /// </summary>
        public void MarkerDefine(int markerNumber, Enums.MarkerSymbol markerSymbol)
        {
            this.SendMessageDirect(2040, markerNumber, (int) markerSymbol);
        }


        /// <summary>
        /// Set the character set of the font in a style.
        /// </summary>
        public void StyleSetCharacterSet(int style, Enums.CharacterSet characterSet)
        {
            this.SendMessageDirect(2066, style, (int)characterSet);
        }

        /// <summary>
        /// Set a style to be mixed case, or to force upper or lower case.
        /// </summary>
        public void StyleSetCase(int style, Enums.CaseVisible caseForce)
        {
            this.SendMessageDirect(2060, style, (int)caseForce);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public readonly Collection<IndicatorStyle> IndicatorStyle;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public readonly IntCollection IndicatorForegroundColor;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public readonly CachingIntCollection MarkerBackgroundColor;
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public readonly CachingIntCollection MarkerForegroundColor;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public readonly ReadOnlyStringCollection Line;
        
        #endregion

        #region Legacy Configuration Code

        /// <summary>
        /// Get or set the legacy configuration object
        /// </summary>
        public Scintilla.Legacy.Configuration.Scintilla LegacyConfiguration
        {
            get
            {
                return this._legacyConfiguration;
            }
            set
            {
                this._legacyConfiguration = value;
            }
        }

        /// <summary>
        /// Set the Configuration Language for the Legacy Config Support
        /// </summary>
        public String LegacyConfigurationLanguage
        {
            set
            {
                if (value == null || value.Equals(""))
                    return;

                Scintilla.Legacy.Configuration.Language lang = _legacyConfiguration.GetLanguage(value);
                if (lang == null)
                    return;

                StyleClearAll();
                System.Type enumtype = typeof(Enums.Lexer);
                try
                {
                    Lexer = (int)Enum.Parse(typeof(Enums.Lexer), lang.lexer.name, true);
                }
                catch (Exception)
                {
                    // try by key instead
                    Lexer = lang.lexer.key;
                }
                if (lang.lexer.stylebits > 0)
                    StyleBits = lang.lexer.stylebits;

                for (int j = 0; j < lang.usestyles.Length; j++)
                {
                    Scintilla.Legacy.Configuration.UseStyle usestyle = lang.usestyles[j];

                    if (usestyle.HasForegroundColor)
                        StyleSetFore(usestyle.key, usestyle.ForegroundColor);
                    if (usestyle.HasBackgroundColor)
                        StyleSetBack(usestyle.key, usestyle.BackgroundColor);
                    if (usestyle.HasFontName)
                        StyleSetFont(usestyle.key, usestyle.FontName);
                    if (usestyle.HasFontSize)
                        StyleSetSize(usestyle.key, usestyle.FontSize);
                    if (usestyle.HasBold)
                        StyleSetBold(usestyle.key, usestyle.IsBold);
                    if (usestyle.HasItalics)
                        StyleSetItalic(usestyle.key, usestyle.IsItalics);
                    if (usestyle.HasEolFilled)
                        StyleSetEOLFilled(usestyle.key, usestyle.IsEolFilled);
                }

                // clear the keywords lists	
                for (int j = 0; j < 9; j++)
                    KeyWords(j, "");

                for (int j = 0; j < lang.usekeywords.Length; j++)
                {
                    Scintilla.Legacy.Configuration.UseKeyword usekeyword = lang.usekeywords[j];
                    Scintilla.Legacy.Configuration.KeywordClass kc = _legacyConfiguration.GetKeywordClass(usekeyword.cls);
                    if (kc != null)
                        KeyWords(usekeyword.key, kc.val);
                }
            }
        }

        #endregion

        #region Configuration Code

        /// <summary>
        /// Get or set the configuration object
        /// </summary>
        public IScintillaConfig Configuration
        {
            get
            {
                return this._configuration;
            }
            set
            {
                this._configuration = value;
            }
        }

        /// <summary>
        /// Set the Configuration Language
        /// </summary>
        public String ConfigurationLanguage
        {
            get
            {
                return _configurationLanguage;
            }
            set
            {
                if ((Configuration != null) && !string.IsNullOrEmpty(value) && (_configurationLanguage != value))
                {
                    _configurationLanguage = value;

                    StyleClearAll();
                    MarginClick -= new EventHandler<MarginClickEventArgs>(ScintillaControl_MarginClick);
                    
                    IScintillaConfig conf = Configuration;
                    //if (conf.CodePage.HasValue) this.CodePage = conf.CodePage;
                    if (conf.SelectionAlpha.HasValue) this.SelectionAlpha = conf.SelectionAlpha.Value;
                    if (conf.SelectionBackColor != Color.Empty) this.SetSelectionBackground(true, Utilities.ColorToRgb(conf.SelectionBackColor));
                    if (conf.TabSize.HasValue) this.TabWidth = conf.TabSize.Value;
					if (conf.IndentSize.HasValue) this.Indent = conf.IndentSize.Value;
					if (conf.UseTabs.HasValue) this.IsUseTabs = conf.UseTabs.Value;
                    
                    // Enable line numbers
                    this.MarginWidthN(0, 40);

                    bool enableFolding = false;
                    if (conf.Fold.HasValue) enableFolding = conf.Fold.Value;
                    if (enableFolding)
                    {
                        this.Property("fold", "1");
                        if (conf.FoldCompact.HasValue) this.Property("fold.compact", (conf.FoldCompact.Value ? "1" : "0"));
                        if (conf.FoldComment.HasValue) this.Property("fold.comment", (conf.FoldComment.Value ? "1" : "0"));
                        if (conf.FoldPreprocessor.HasValue) this.Property("fold.preprocessor", (conf.FoldPreprocessor.Value ? "1" : "0"));
                        if (conf.FoldHTML.HasValue) this.Property("fold.html", (conf.FoldHTML.Value ? "1" : "0"));
                        if (conf.FoldHTMLPreprocessor.HasValue) this.Property("fold.html.preprocessor", (conf.FoldHTMLPreprocessor.Value ? "1" : "0"));

                        this.MarginWidthN(2, 0);
                        this.MarginTypeN(2, (int)MarginType.Symbol);
                        this.MarginMaskN(2, unchecked((int)0xFE000000));
                        this.MarginSensitiveN(2, true);

                        if (conf.FoldMarginWidth.HasValue) this.MarginWidthN(2, conf.FoldMarginWidth.Value);
                        else this.MarginWidthN(2, 20);

                        if (conf.FoldMarginColor != Color.Empty) this.SetFoldMarginColor(true, Utilities.ColorToRgb(conf.FoldMarginColor));
                        if (conf.FoldMarginHighlightColor != Color.Empty) this.SetFoldMarginHiColor(true, Utilities.ColorToRgb(conf.FoldMarginHighlightColor));
                        if (conf.FoldFlags.HasValue) this.SetFoldFlags(conf.FoldFlags.Value);
                        
                        this.MarkerDefine((int)MarkerOutline.Folder, MarkerSymbol.Plus);
                        this.MarkerDefine((int)MarkerOutline.FolderOpen, MarkerSymbol.Minus);
                        this.MarkerDefine((int)MarkerOutline.FolderEnd, MarkerSymbol.Empty);
                        this.MarkerDefine((int)MarkerOutline.FolderMidTail, MarkerSymbol.Empty);
                        this.MarkerDefine((int)MarkerOutline.FolderOpenMid, MarkerSymbol.Minus);
                        this.MarkerDefine((int)MarkerOutline.FolderSub, MarkerSymbol.Empty);
                        this.MarkerDefine((int)MarkerOutline.FolderTail, MarkerSymbol.Empty);

                        this.MarginClick += new EventHandler<MarginClickEventArgs>(ScintillaControl_MarginClick);
                    }

                    ILanguageConfig lang = conf.Languages[value];
                    if (lang != null)
                    {
                        if (!string.IsNullOrEmpty(lang.WhitespaceCharacters)) 
                            this.WhitespaceChars(lang.WhitespaceCharacters);

                        if (!string.IsNullOrEmpty(lang.WordCharacters)) 
                            this.WordChars(lang.WordCharacters);


                        ILexerConfig lex = lang.Lexer;

                        this.Lexer = lex.LexerID;
                        foreach (ILexerStyle style in lex.Styles.Values)
                        {
                            if (style.ForeColor != Color.Empty)
                                StyleSetFore(style.StyleIndex, Utilities.ColorToRgb(style.ForeColor));

                            if (style.BackColor != Color.Empty)
                                StyleSetBack(style.StyleIndex, Utilities.ColorToRgb(style.BackColor));

                            if (!string.IsNullOrEmpty(style.FontName))
                                StyleSetFont(style.StyleIndex, style.FontName);

                            if (style.FontSize.HasValue)
                                StyleSetSize(style.StyleIndex, style.FontSize.Value);

                            if (style.Bold.HasValue)
                                StyleSetBold(style.StyleIndex, style.Bold.Value);

                            if (style.Italics.HasValue)
                                StyleSetItalic(style.StyleIndex, style.Italics.Value);

                            if (style.EOLFilled.HasValue)
                                StyleSetEOLFilled(style.StyleIndex, style.EOLFilled.Value);

                            StyleSetCase(style.StyleIndex, style.CaseVisibility);
                        }
                        this.StyleBits = this.StyleBitsNeeded;

                        for (int j = 0; j < 9; j++)
                        {
                            if (lang.KeywordLists.ContainsKey(j))
                                KeyWords(j, lang.KeywordLists[j]);
                            else 
                                KeyWords(j, string.Empty);
                        }
                    }

                    this.Colorize(0, this.Length);
                }
            }
        }

        private void ScintillaControl_MarginClick(object sender, MarginClickEventArgs e)
        {
            if (e.Margin == 2)
            {
                int lineNumber = this.LineFromPosition(e.Position);
                ToggleFold(lineNumber);
            }
        }

        #endregion

        #region Smart indenting support
        /// <summary>
        /// Enable or disable Smart Indenting
        /// </summary>
        public bool SmartIndentingEnabled
        {
            get
            {
                return _smartIndenting != null;
            }
            set
            {
                if (value)
                {
                    if (_smartIndenting == null) 
                    {
                        _smartIndenting = new EventHandler<CharAddedEventArgs>(SmartIndenting_CharAdded);
                        CharAdded += SmartIndenting_CharAdded;
                    }
                }
                else
                {
                    if (_smartIndenting != null)
                    {
                        CharAdded -= SmartIndenting_CharAdded;
                        _smartIndenting = null;
                    }
                }
            }
        }

        /// <summary>
        /// If Smart Indenting is enabled, this delegate will be added to the CharAdded multicast event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SmartIndenting_CharAdded(object sender, CharAddedEventArgs e)
        {
            if (e.Ch == '\n')
            {
                int curLine = this.CurrentPos;
                curLine = this.LineFromPosition(curLine);

                int previousIndent = this.LineIndentation(curLine - 1);
                this.IndentLine(curLine, previousIndent);
            }
        }

        /// <summary>
        /// Smart Indenting helper method
        /// </summary>
        /// <param name="line"></param>
        /// <param name="indent"></param>
        private void IndentLine(int line, int indent)
        {
            if (indent < 0)
            {
                return;
            }

            int selStart = this.SelectionStart;
            int selEnd = this.SelectionEnd;

            int posBefore = this.LineIndentPosition(line);
            this.LineIndentation(line, indent);
            int posAfter = LineIndentPosition(line);
            int posDifference = posAfter - posBefore;

            if (posAfter > posBefore)
            {
                // Move selection on
                if (selStart >= posBefore)
                {
                    selStart += posDifference;
                }

                if (selEnd >= posBefore)
                {
                    selEnd += posDifference;
                }
            }
            else if (posAfter < posBefore)
            {
                // Move selection back
                if (selStart >= posAfter)
                {
                    if (selStart >= posBefore)
                        selStart += posDifference;
                    else
                        selStart = posAfter;
                }
                if (selEnd >= posAfter)
                {
                    if (selEnd >= posBefore)
                        selEnd += posDifference;
                    else
                        selEnd = posAfter;
                }
            }

            this.SetSelection(selStart, selEnd);
        }
        #endregion

        #region Add Shortcuts from form to Scintilla control

        public virtual void AddShortcuts(Form parentForm)
        {
            if ((parentForm != null) && (parentForm.MainMenuStrip != null))
            {
                AddShortcuts(parentForm.MainMenuStrip.Items);
            }
        }

        public virtual void AddShortcuts(ToolStripItemCollection m)
        {
            foreach (ToolStripItem tmi in m)
            {
                if (tmi is ToolStripMenuItem)
                {
                    ToolStripMenuItem mi = tmi as ToolStripMenuItem;
                    if (mi.ShortcutKeys != System.Windows.Forms.Keys.None)
                    {
                        AddIgnoredKey(mi.ShortcutKeys);
                    }

                    if (mi.DropDownItems.Count > 0)
                    {
                        AddShortcuts(mi.DropDownItems);
                    }
                }
            }
        }

        public virtual void AddIgnoredKey(System.Windows.Forms.Keys shortcutkey)
        {
            int key = (int)shortcutkey;
            this._ignoredKeys.Add(key, key);
        }

        public override bool PreProcessMessage(ref Message m)
        {
            switch (m.Msg)
            {
				case WM_SYSKEYDOWN:		// This traps F10
				case WM_KEYDOWN:
                    {
						// Let Windows Forms process the message first, before it is dispatched to the Scintilla control
						if (base.PreProcessMessage(ref m) == true)
							return true;
					}
                    break;
            }
            return false;
        }

		protected override bool ProcessCmdKey(ref Message msg, System.Windows.Forms.Keys keyData)
		{
			return base.ProcessCmdKey(ref msg, keyData);
		}

        #endregion

        #region Range
		public Range Range(int position) 
		{
			return Range(position, position);
		}

		public Range Range(int start, int end)
		{
			return new Range(start, end, this);
		}
		#endregion
    }
}
