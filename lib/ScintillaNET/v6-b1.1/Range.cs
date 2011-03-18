using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace Scintilla
{
	/// <summary>
	/// A range within the editor. Start and End are both Positions.
	/// </summary>
	public class Range : IComparable
	{
		private ScintillaControl _scintillaControl;

		public bool Collapsed
		{
			get { return _start == _end; }
		}
		private int _end;
		public virtual int End
		{
			get
			{
				return _end;
			}
			set
			{
				_end = value;
			}
		}

		private int _start;
		public virtual int Start
		{
			get
			{
				return _start;
			}
			set
			{
				_start = value;
			}
		}


		internal Range(int start, int end, ScintillaControl scintillaControl)
		{
			_scintillaControl = scintillaControl;

			if(start < end)
			{
				_start	= start;
				_end	= end;
			}
			else
			{
				_start	= end;
				_end	= start;
			}
		}

		public int Length
		{
			get
			{
				return _end - _start;
			}
		}

		public string Text
		{
			get
			{
				if(Start < 0 || End < 0 || _scintillaControl == null)
					return string.Empty;

				TextRange rng	= new TextRange();
				rng.lpstrText	= Marshal.AllocHGlobal(Length);
				rng.chrg.cpMin	= _start;
				rng.chrg.cpMax	= _end;
				int len = (int)_scintillaControl.GetTextRange(ref rng);

				string ret = Utilities.PtrToStringUtf8(rng.lpstrText, len);
				Marshal.FreeHGlobal(rng.lpstrText);
				return ret;

			}
			set
			{
				_scintillaControl.TargetStart = _start;
				_scintillaControl.TargetEnd = _end;
				_scintillaControl.ReplaceTarget(value);
			}
		}

		public byte[] StyledText
		{
			get
			{
				if(Start < 0 || End < 0 || _scintillaControl == null)
					return new byte[0];

				int bufferLength	= (Length * 2) + 2;
				TextRange rng		= new TextRange();
				rng.lpstrText		= Marshal.AllocHGlobal(bufferLength);
				rng.chrg.cpMin		= _start;
				rng.chrg.cpMax		= _end;

				_scintillaControl.GetStyledText(ref rng);

				byte[] ret = new byte[bufferLength];
				Marshal.Copy(rng.lpstrText, ret, 0, bufferLength);

				Marshal.FreeHGlobal(rng.lpstrText);
				return ret;
			}
		}


		public void Copy()
		{
			_scintillaControl.CopyRange(_start, _end);
		}

		public void Select()
		{
			_scintillaControl.SetSelection(_start, _end);
		}

		public bool PositionInRange(int position)
		{
			return position >= _start && position <= _end;
		}

		public bool IntersectsWith(Range otherRange)
		{
			return otherRange.PositionInRange(_start) | otherRange.PositionInRange(_end) | PositionInRange(otherRange.Start) | PositionInRange(otherRange.End);
		}

		public void SetStyle(int style)
		{
			SetStyle(0xff, style);
		}

		public void SetStyle(byte styleMask, int style)
		{
			_scintillaControl.StartStyling(_start, (int)styleMask);
			_scintillaControl.SetStyling(Length, style);
		}

		public void SetIndicators(bool indic0, bool indic1, bool indic2)
		{
			int style = 0 | (indic0 ? 0x20 : 0) | 
			    (indic1 ? 0x40 : 0) |
			    (indic2 ? 0x80 : 0);

			SetStyle(0xE0, style);
		}

		public override bool Equals(object obj)
		{
			Range r = obj as Range;
			if(r == null)
				return false;

			return r._start == _start && r._end == _end;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		#region IComparable Members

		public int CompareTo(object otherObj)
		{
			Range other = otherObj as Range;

			if(other == null)
				return 1;

			if(other._start < _start)
				return 1;
			else if(other._start > _start)
				return -1;

			//	Starts must equal, lets try ends
			if(other._end < _end)
				return 1;
			else if(other._end > _end)
				return -1;

			//	Start and End equal. Comparitavely the same
			return 0;
		}
		#endregion
	}

}
