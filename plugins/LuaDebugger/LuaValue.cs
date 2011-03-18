
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

namespace Tilde.LuaDebugger
{

	public enum LuaValueType
	{
		// Lua 5.1.3 public types
		NONE = -1,		// #define LUA_TNONE			(-1)
		NIL,			// #define LUA_TNIL				0
		BOOLEAN,		// #define LUA_TBOOLEAN			1
		LIGHTUSERDATA,	// #define LUA_TLIGHTUSERDATA	2
		NUMBER,			// #define LUA_TNUMBER			3
		STRING,			// #define LUA_TSTRING			4
		TABLE,			// #define LUA_TTABLE			5
		FUNCTION,		// #define LUA_TFUNCTION		6
		USERDATA,		// #define LUA_TUSERDATA		7
		THREAD,			// #define LUA_TTHREAD			8

		// Special Tilde types
		TILDE_METATABLE		= NONE - 1,
		TILDE_ENVIRONMENT	= NONE - 2,
		TILDE_UPVALUES		= NONE - 3,
	}

	public class LuaValue : IComparable<LuaValue>
	{
		public LuaValue(Int64 value, LuaValueType type)
		{
			m_value = value;
			m_type = type;
		}

		public static LuaValue nil = new LuaValue(0, LuaValueType.NIL);

		public Int64 Value
		{
			get { return m_value; }
			set { m_value = value; }
		}

		public LuaValueType Type
		{
			get { return m_type; }
			set { m_type = value; }
		}

		public double AsNumber()
		{
			System.Diagnostics.Debug.Assert(m_type == LuaValueType.NUMBER);
			return BitConverter.Int64BitsToDouble(m_value);
		}

		public bool AsBoolean()
		{
			System.Diagnostics.Debug.Assert(m_type == LuaValueType.BOOLEAN);
			return m_value != 0;
		}

		public Int64 AsIdentifier()
		{
			System.Diagnostics.Debug.Assert(m_type == LuaValueType.LIGHTUSERDATA || m_type == LuaValueType.STRING || m_type == LuaValueType.TABLE || m_type == LuaValueType.USERDATA || m_type == LuaValueType.THREAD);
			return m_value;
		}

		public bool IsNil()
		{
			return m_type == LuaValueType.NIL;
		}

		public override bool Equals(object obj)
		{
			if(obj is LuaValue)
			{
				LuaValue rhs = (LuaValue) obj;
				return this.m_value == rhs.m_value && this.m_type == rhs.m_type;
			}
			else
				return base.Equals(obj);
		}

		public override string ToString()
		{
			return m_value.ToString() + ":" + m_type.ToString();
		}

		public override int GetHashCode()
		{
			return m_value.GetHashCode() ^ m_type.GetHashCode();
		}

		public int CompareTo(LuaValue other)
		{
			int result = m_value.CompareTo(other.m_value);
			if (result == 0)
				return m_type.CompareTo(other.m_type);
			else
				return result;
		}

		private Int64 m_value;
		private LuaValueType m_type;
	};
}
