using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Scintilla
{

    #region CharAddedEventArgs

    //public delegate void CharAddedEventHandler(object sender, CharAddedEventArgs e);
    public class CharAddedEventArgs : EventArgs
    {
        private char _ch;

        public char Ch
        {
            get { return _ch; }
        }

        public CharAddedEventArgs(char ch)
        {
            _ch = ch;
        }

        internal CharAddedEventArgs(SCNotification eventSource) 
			: this(eventSource.ch){}
    }

    #endregion

    #region FoldChangedEventArgs

    //public delegate void FoldChangedEventHandler(object sender, FoldChangedEventArgs e);
    public class FoldChangedEventArgs : ModifiedEventArgs
    {
        private int _newFoldLevel;
        private int _previousFoldLevel;

        public int NewFoldLevel
        {
            get { return _newFoldLevel; }
            set { _newFoldLevel = value; }
        }

        public int PreviousFoldLevel
        {
            get { return _previousFoldLevel; }
            set { _previousFoldLevel = value; }
        }

        public FoldChangedEventArgs(int line, int newFoldLevel, int previousFoldLevel, int modificationType)
            : base(modificationType)
        {
            _newFoldLevel = newFoldLevel;
            _previousFoldLevel = previousFoldLevel;
        }

		internal FoldChangedEventArgs(SCNotification eventSource) 
			: this(eventSource.line, eventSource.foldLevelNow, eventSource.foldLevelPrev, eventSource.modificationType){}
    }

    #endregion

    #region LinesNeedShownEventArgs

    //public delegate void LinesNeedShownEventHandler(object sender, LinesNeedShownEventArgs e);
    public class LinesNeedShownEventArgs : EventArgs
    {
        private int _firstLine;
        private int _lastLine;

        public int FirstLine
        {
            get { return _firstLine; }
            set { _firstLine = value; }
        }

        public int LastLine
        {
            get { return _lastLine; }
            set { _lastLine = value; }
        }

        public LinesNeedShownEventArgs(int startLine, int endLine)
        {
            _firstLine = startLine;
            _lastLine = endLine;
        }
    }

    #endregion

    #region MarkerChangedEventArgs

    //public delegate void MarkerChangedEventHandler(object sender, MarkerChangedEventArgs e);
    public class MarkerChangedEventArgs : ModifiedEventArgs
    {
        public MarkerChangedEventArgs(int line, int modificationType)
            : base(modificationType)
        {
        }


    }

    #endregion

    #region ModifiedEventArgs

    public  class ModifiedEventArgs : EventArgs
    {
        private UndoRedoFlags _undoRedoFlags;
        private int _modificationType;
		private bool _startAction;
		private bool _isUserChange;
		private int _position;
		private int _length;
		private int _linesAddedCount;
		private string _text;
		private int _line;
		private int _foldLevelNow;
		private int _foldLevelPrev;

		public int FoldLevelNow
		{
			get
			{
				return _foldLevelNow;
			}
			set
			{
				_foldLevelNow = value;
			}
		}
		
		public int FoldLevelPrev
		{
			get
			{
				return _foldLevelPrev;
			}
			set
			{
				_foldLevelPrev = value;
			}
		}

		public int Line
		{
			get { return _line; }
			set { _line = value; }
		}

		public int Position
		{
			get { return _position; }
			set { _position = value; }
		}

		public int Length
		{
			get { return _length; }
			set { _length = value; }
		}

		public int LinesAddedCount
		{
			get { return _linesAddedCount; }
			set { _linesAddedCount = value; }
		}


		public string Text
		{
			get { return _text; }
			set { _text = value; }
		}

		public bool IsUserChange
		{
			get { return _isUserChange; }
			set { _isUserChange = value; }
		}


		public bool StartAction
		{
			get
			{
				return _startAction;
			}
			set
			{
				_startAction = value;
			}
		}

        public int ModificationType
        {
            get { return _modificationType; }
            set { _modificationType = value; }
        }

        public UndoRedoFlags UndoRedoFlags
        {
            get { return _undoRedoFlags; }
            set { _undoRedoFlags = value; }
        }

        protected ModifiedEventArgs(int modificationType)
        {
            _modificationType = modificationType;
            _undoRedoFlags = new UndoRedoFlags(modificationType);

			//	Ah yes more magic numbers. I'g going to get these refactored someday...
			_isUserChange = (modificationType & 0x10) != 0;
			_startAction = (modificationType & 0x2000) != 0;
        }

		internal ModifiedEventArgs(SCNotification eventSource)
			: this(eventSource.modificationType)
		{
			_foldLevelNow = eventSource.foldLevelNow;
			_foldLevelPrev = eventSource.foldLevelPrev;
			_length = eventSource.length;
			_line = eventSource.line;
			_linesAddedCount = eventSource.linesAdded;
			_position = eventSource.position;
			_text = Utilities.PtrToStringUtf8(eventSource.text, eventSource.length);
		}
    }

    #endregion

    #region NativeScintillaEventArgs

    //	All events fired from the INativeScintilla Interface use this
    //	delegate whice passes NativeScintillaEventArgs. Msg is a copy
    //	of the Notification Message sent to Scintilla's Parent WndProc
    //	and SCNotification is the SCNotification Struct pointed to by 
    //	Msg's lParam.
    public class NativeScintillaEventArgs : EventArgs
    {
        private Message _msg;
        private SCNotification _notification;

        public Message Msg
        {
            get { return _msg; }
        }

        internal SCNotification SCNotification
        {
            get { return _notification; }
        }

        internal NativeScintillaEventArgs(Message Msg, SCNotification notification)
        {
            _msg = Msg;
            _notification = notification;
        }
    }

    #endregion

    #region ScintillaMouseEventArgs

    //public delegate void ScintillaMouseEventHandler(object sender, ScintillaMouseEventArgs e);

    public class ScintillaMouseEventArgs : EventArgs
    {
        private int _x;
        private int _y;
        private int _position;

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public ScintillaMouseEventArgs(int x, int y, int position)
        {
            _x = x;
            _y = y;
            _position = position;
        }
    }

    #endregion

    #region StyleChangedEventArgs

    public class StyleChangedEventArgs : ModifiedEventArgs
    {
		internal StyleChangedEventArgs(SCNotification eventSource)
			: base(eventSource){}
    }

    #endregion

    #region StyleNeededEventArgs

    //public delegate void StyleNeededEventHandler(object sender, StyleNeededEventArgs e);

    public class StyleNeededEventArgs : EventArgs
    {
		private int _position;
		public int Position
		{
			get
			{
				return _position;
			}
			set
			{
				_position = value;
			}
		}

        public StyleNeededEventArgs(int position)
        {
			_position = position;
        }

        internal StyleNeededEventArgs(SCNotification eventSource)
        {
			_position = eventSource.position;
        }
    }

    #endregion

    #region UndoRedoFlags

    //	Used by TextModifiedEventArgs, StyeChangedEventArgs and FoldChangedEventArgs
    //	this provides a friendly wrapper around the SCNotification's modificationType
    //	flags having to do with Undo and Redo
    public struct UndoRedoFlags
    {
        public bool IsUndo;
        public bool IsRedo;
        public bool IsMultiStep;
        public bool IsLastStep;
        public bool IsMultiLine;

        private const string STRING_FORMAT =
            "IsUndo\t\t\t\t:{0}\r\nIsRedo\t\t\t\t:{1}\r\nIsMultiStep\t\t\t:{2}\r\nIsLastStep\t\t\t:{3}\r\nIsMultiLine\t\t\t:{4}";

        public override string ToString()
        {
            return string.Format(STRING_FORMAT, IsUndo, IsRedo, IsMultiStep, IsLastStep, IsMultiLine);
        }

        public UndoRedoFlags(int modificationType)
        {
			IsLastStep = (modificationType & (int)Scintilla.Enums.ModificationFlags.StepInUndoRedo) > 0;
			/** FIXME : 
			 * GENERATION ISSUE -- The Scintilla.iface breaks the pattern with the following three flags:
			 *  val SC_MULTISTEPUNDOREDO=0x80
			 *  val SC_MULTILINEUNDOREDO=0x1000
			 *  val SC_MODEVENTMASKALL=0x1FFF
			 **/

			IsMultiLine = (modificationType & /* SC_MULTILINEUNDOREDO */ 0x1000) > 0;
			IsMultiStep = (modificationType & /* SC_MULTISTEPUNDOREDO*/ 0x80) > 0;
			IsRedo = (modificationType & (int)Scintilla.Enums.ModificationFlags.Redo) > 0;
			IsUndo = (modificationType & (int)Scintilla.Enums.ModificationFlags.Undo) > 0;
		}
	}
	#endregion

    #region UriDroppedEventArgs

    //public delegate void UriDroppedEventHandler(object sender, UriDroppedEventArgs e);

    public class UriDroppedEventArgs : EventArgs
    {
        //	I decided to leave it a string because I can't really
        //	be sure it is a Uri.
        private string _uriText;

        public string UriText
        {
            get { return _uriText; }
            set { _uriText = value; }
        }

        public UriDroppedEventArgs(string uriText)
        {
            _uriText = uriText;
        }
        internal UriDroppedEventArgs(SCNotification eventSource)
        {
        }
        
    }

    #endregion

    //public delegate void AutoCSelectionEventHandler( object sender, AutoCSelectionEventArgs eventArgs);

    public class AutoCSelectionEventArgs : EventArgs
    {
        private string _text;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

		public AutoCSelectionEventArgs(string text)
		{
			_text = text;
		}

		private int _wordStartPosition;
		public int WordStartPosition
		{
			get
			{
				return _wordStartPosition;
			}
			set
			{
				_wordStartPosition = value;
			}
		}

		internal AutoCSelectionEventArgs(SCNotification eventSource)
		{
			_wordStartPosition = (int)eventSource.lParam;

			//	I'm pretty sure this is bad, but not positive. I don't quite know which of the
			//	which of Scintilla's strings support non-ANSI besides the main document text
			_text = Marshal.PtrToStringAnsi(eventSource.text);
		}
    }

    public class SavePointReachedEventArgs : EventArgs
    {
        internal SavePointReachedEventArgs(SCNotification eventSource)
        {
        }
    }

    public class SavePointLeftEventArgs : EventArgs
    {
        internal SavePointLeftEventArgs(SCNotification eventSource)
        {
        }
    }

    public class ModifyAttemptROEventArgs : EventArgs
    {
        internal ModifyAttemptROEventArgs(SCNotification eventSource)
        {
        }
    }

    public class SCDoubleClickEventArgs : EventArgs
    {
		private int _position;
		public int Position
		{
			get
			{
				return _position;
			}
			set
			{
				_position = value;
			}
		}
		private int _line;
		public int Line
		{
			get
			{
				return _line;
			}
			set
			{
				_line = value;
			}
		}

		public SCDoubleClickEventArgs(int position, int line)
		{
			_position = position;
			_line = line;
		}

        internal SCDoubleClickEventArgs(SCNotification eventSource)
			: this(eventSource.position, eventSource.line){}
    }
    public class SCKeyEventArgs : EventArgs
    {
		private char _ch;
		public char Ch
		{
			get
			{
				return _ch;
			}
			set
			{
				_ch = value;
			}
		}

		private int _modifiers;
		public int Modifiers
		{
			get
			{
				return _modifiers;
			}
			set
			{
				_modifiers = value;
			}
		}

		public SCKeyEventArgs(char ch, int modifiers)
		{
			_ch = ch;
			_modifiers = modifiers;
		}

        internal SCKeyEventArgs(SCNotification eventSource)
			: this(eventSource.ch, eventSource.modifiers){}
    }

 
    public class UpdateUIEventArgs : EventArgs
    {
        internal UpdateUIEventArgs(SCNotification eventSource)
        {
        }
    }

    public class MacroRecordEventArgs : EventArgs
    {
		private Message _message;
		public Message Message
		{
			get
			{
				return _message;
			}
			set
			{
				_message = value;
			}
		}
        internal MacroRecordEventArgs(SCNotification eventSource)
        {
			_message		= new Message();
			_message.HWnd	= eventSource.nmhdr.hwndFrom;
			_message.LParam = eventSource.lParam;
			_message.WParam = eventSource.wParam;
        }
    }

    public class MarginClickEventArgs : EventArgs
    {
		private int _modifiers;
		public int Modifiers
		{
			get
			{
				return _modifiers;
			}
			set
			{
				_modifiers = value;
			}
		}
		private int _position;
		public int Position
		{
			get
			{
				return _position;
			}
			set
			{
				_position = value;
			}
		}
		private int _margin;
		public int Margin
		{
			get
			{
				return _margin;
			}
			set
			{
				_margin = value;
			}
		}

		public MarginClickEventArgs(int modifiers, int position, int margin)
		{
			_modifiers	= modifiers;
			_position	= position;
			_margin		= margin;
        }

        internal MarginClickEventArgs(SCNotification eventSource)
			: this(eventSource.modifiers, eventSource.position, eventSource.margin){}
    }

    public class NeedShownEventArgs : EventArgs
    {
		private int _position;
		public int Position
		{
			get
			{
				return _position;
			}
			set
			{
				_position = value;
			}
		}

		private int _length;
		public int Length
		{
			get
			{
				return _length;
			}
			set
			{
				_length = value;
			}
		}

        internal NeedShownEventArgs(SCNotification eventSource)
        {
			_position	= eventSource.position;
			_length		= eventSource.length;
        }
    }

    public class PaintedEventArgs : EventArgs
    {
        internal PaintedEventArgs(SCNotification eventSource)
        {

        }
    }

	public class UserListSelectionEventArgs : AutoCSelectionEventArgs
    {
		private int _listType;
		public int ListType
		{
			get
			{
				return _listType;
			}
			set
			{
				_listType = value;
			}
		}

		public UserListSelectionEventArgs(int listType, string text) : base(text)
		{
			_listType = listType;
		}

        internal UserListSelectionEventArgs(SCNotification eventSource) : base(eventSource)
        {
			_listType = (int)eventSource.wParam;
        }
    }

	public class DwellEventArgs : EventArgs
	{
		private int _position;
		public int Position
		{
			get
			{
				return _position;
			}
			set
			{
				_position = value;
			}
		}

		private int _x;
		public int X
		{
			get
			{
				return _x;
			}
			set
			{
				_x = value;
			}
		}

		private int _y;
		public int Y
		{
			get
			{
				return _y;
			}
			set
			{
				_y = value;
			}
		}

		public DwellEventArgs(int position, int x, int y)
		{
			_position	= position;
			_x			= x;
			_y			= y;
		}

		internal DwellEventArgs(SCNotification eventSource) 
			: this(eventSource.position, eventSource.x, eventSource.y) { }
	}


	public class DwellStartEventArgs : DwellEventArgs
    {
		internal DwellStartEventArgs(SCNotification eventSource) : base(eventSource) { }
    }

	public class DwellEndEventArgs : DwellEventArgs
    {
		internal DwellEndEventArgs(SCNotification eventSource) : base(eventSource) { }
    }

    public class SCZoomEventArgs : EventArgs
    {
        internal SCZoomEventArgs(SCNotification eventSource)
        {
        }
    }

    public class HotspotClickEventArgs : EventArgs
    {
		private int _position;
		public int Position
		{
			get
			{
				return _position;
			}
			set
			{
				_position = value;
			}
		}

		private int _modifiers;
		public int Modifiers
		{
			get
			{
				return _modifiers;
			}
			set
			{
				_modifiers = value;
			}
		}

		public HotspotClickEventArgs(int modifiers, int position)
		{
			_modifiers = modifiers;
			_position = position;
		}

        internal HotspotClickEventArgs(SCNotification eventSource)
			: this(eventSource.modifiers, eventSource.position){}
    }

	public class HotspotDoubleClickEventArgs : HotspotClickEventArgs
    {
		internal HotspotDoubleClickEventArgs(SCNotification eventSource) : base(eventSource) { }
    }

    public class CallTipClickEventArgs : EventArgs
    {
		private int _position;
		public int Position
		{
			get
			{
				return _position;
			}
			set
			{
				_position = value;
			}
		}

		public CallTipClickEventArgs(int position)
		{
			_position = position;
		}

        internal CallTipClickEventArgs(SCNotification eventSource)
	       : this(eventSource.position){}
    }
}