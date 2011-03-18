
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
	public class ReceiveMessageBuffer
	{
		public ReceiveMessageBuffer(int size)
		{
			mData = new byte[size];
			mPosition = 0;
			mDataLength = 0;
			mMessageStart = 0;
			mMessageLength = 0;
			mSizeofObjectID = 0;
			mSizeofNumber = 0;
		}

		public int DataLength
		{
			get { return mDataLength; }
			set { mDataLength = value;  }
		}

		public int SizeofObjectID
		{
			get { return mSizeofObjectID; }
			set { mSizeofObjectID = value; }
		}

		public int SizeofNumber
		{
			get { return mSizeofNumber; }
			set { mSizeofNumber = value; }
		}
		
		public int Position
		{
			get { return mPosition; }
		}

		public byte [] Buffer
		{
			get { return mData; }
		}

		public int Length
		{
			get { return mData.Length; }
		}

		public int DataAvailable
		{
			get { return mDataLength - mPosition;  }
		}

		public int MessageLength
		{
			get { return mMessageLength; }
		}

		public int MessageAvailable
		{
			get { return mMessageLength + mMessageStart - mPosition; }
		}

		public void BeginRead()
		{
			mPosition = 0;
			mMessageStart = -1;
			mMessageLength = -1;
		}

		public void BeginMessage()
		{
			mMessageStart = mPosition;
			mMessageLength = ReadInt32();
		}

		public void EndMessage()
		{
			// Make sure the message was properly parsed (all the bytes were read)
			if(mPosition < mMessageStart + mMessageLength)
				throw new ProtocolException(String.Format("The message was not correctly parsed; {0} bytes were not read", (mMessageStart + mMessageLength) - mPosition));
			else if (mPosition > mMessageStart + mMessageLength)
				throw new ProtocolException(String.Format("The message was not correctly parsed; {0} excess bytes were read", mPosition - (mMessageStart + mMessageLength)));

			mMessageStart = -1;
			mMessageLength = -1;
		}

		public void EndRead()
		{
			// Shift any incomplete messages back down to the front of the buffer
			if (mDataLength > mPosition && mPosition > 0)
				Array.Copy(mData, mPosition, mData, 0, mDataLength - mPosition);

			mDataLength -= mPosition;
			mPosition = 0;
		}

		public void Skip(int count)
		{
			mPosition += count;
		}

		public byte [] ReadBytes(int count)
		{
			if(mMessageLength >= 0 && MessageAvailable < count)
				throw new ProtocolException("Buffer overflow");

			byte[] result = new byte[count];
			Array.Copy(mData, mPosition, result, 0, count);
			mPosition += RoundUp(count, 4);
			return result;
		}

		public LuaValue ReadValue()
		{
			LuaValueType type = (LuaValueType)ReadInt32();
			Int64 value;
			if(type == LuaValueType.NUMBER)
				value = ReadNumberRaw();
			else
				value = ReadObjectID();

			return new LuaValue(value, type);
		}


		public Int64 ReadNumberRaw()
		{
			switch (mSizeofNumber)
			{
				case 4: return ReadInt32();
				case 8: return ReadInt64();
				default: throw new ProtocolException(String.Format("Unknown size specified for lua_Number type: {0}", mSizeofNumber));
			}
		}

		public double ReadNumber()
		{
			switch (mSizeofNumber)
			{
				case 4: return ReadFloat();
				case 8: return ReadDouble();
				default: throw new ProtocolException(String.Format("Unknown size specified for lua_Number type: {0}", mSizeofNumber));
			}
		}

		public Int64 ReadObjectID()
		{
			switch (mSizeofObjectID)
			{
				case 4: return ReadInt32();
				case 8: return ReadInt64();
				default: throw new ProtocolException(String.Format("Unknown size specified for LuaDebuggerObjectID type: {0}", mSizeofObjectID));
			}
		}

		public Int16 ReadInt16()
		{
			if (mMessageLength >= 0 && MessageAvailable < 2)
				throw new ProtocolException("Buffer overflow");

			Int16 result = BitConverter.ToInt16(mData, mPosition);
			mPosition += 2;
			return result;
		}

		public int ReadInt32()
		{
			if (mMessageLength >= 0 && MessageAvailable < 4)
				throw new ProtocolException("Buffer overflow");

			int result = BitConverter.ToInt32(mData, mPosition);
			mPosition += 4;
			return result;
		}

		public Int64 ReadInt64()
		{
			if (mMessageLength >= 0 && MessageAvailable < 8)
				throw new ProtocolException("Buffer overflow");

			Int64 result = BitConverter.ToInt64(mData, mPosition);
			mPosition += 8;
			return result;
		}

		public int PeekInt32()
		{
			if (mMessageLength >= 0 && MessageAvailable < 4)
				throw new ProtocolException("Buffer overflow");

			int result = BitConverter.ToInt32(mData, mPosition);
			return result;
		}

		public float ReadFloat()
		{
			if (mMessageLength >= 0 && MessageAvailable < 4)
				throw new ProtocolException("Buffer overflow");

			float result = BitConverter.ToSingle(mData, mPosition);
			mPosition += 4;
			return result;
		}

		public double ReadDouble()
		{
			if (mMessageLength >= 0 && MessageAvailable < 8)
				throw new ProtocolException("Buffer overflow");

			double result = BitConverter.ToDouble(mData, mPosition);
			mPosition += 8;
			return result;
		}

		public string ReadString()
		{
			if (mMessageLength >= 0 && MessageAvailable < 4)
				throw new ProtocolException("Buffer overflow");
			int len = BitConverter.ToInt32(mData, mPosition);
			if (mMessageLength >= 0 && MessageAvailable < 4 + len)
				throw new ProtocolException("Buffer overflow");

			string str = len == 0 ? null : new String(Encoding.ASCII.GetChars(mData, mPosition + 4, len - 1));
			mPosition += 4 + RoundUp(len, 4);
			return str;
		}

		private bool IsAligned(int value, int align)
		{
			return value % align == 0;
		}

		private int RoundUp(int value, int align)
		{
			int diff = value % align;
			if (diff != 0)
				return value + (align - diff);
			else
				return value;
		}


		byte[] mData;
		int mPosition;
		int mDataLength;
		int mMessageStart;
		int mMessageLength;

		int mSizeofObjectID;
		int mSizeofNumber;


	}
}
