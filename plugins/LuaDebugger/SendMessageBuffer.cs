
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
	public class SendMessageBuffer
	{
		public SendMessageBuffer(int size)
		{
			mData = new byte[size];
			mPosition = 0;
			mMessageStart = 0;
			mSizeofObjectID = 0;
			mSizeofNumber = 0;
		}

		public byte [] Data
		{
			get { return mData; }
		}

		public int Length
		{
			get { return mPosition; }
			set { mPosition = value; }
		}

		public int BytesRemaining
		{
			get { return mData.Length - mPosition; }
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

		public void Begin()
		{
			mMessageStart = mPosition;
			mPosition += 4;
		}

		public int End()
		{
			Write((int)mPosition - mMessageStart, mMessageStart);
			return mPosition;
		}

		public void WriteValue(LuaValue value)
		{
			Write((int)value.Type);
			if (value.Type == LuaValueType.NUMBER)
				WriteNumber(value.AsNumber());
			else
				WriteObjectID(value.Value);
		}

		public void WriteNumber(double value)
		{
			switch (mSizeofNumber)
			{
				case 4: Write((float)value); break;
				case 8: Write((double)value); break;
				default: throw new ProtocolException(String.Format("Unknown size specified for lua_Number type: {0}", mSizeofNumber));
			}
		}

		public void WriteObjectID(Int64 value)
		{
			switch (mSizeofObjectID)
			{
				case 4: Write((Int32)value); break;
				case 8: Write((Int64)value); break;
				default: throw new ProtocolException(String.Format("Unknown size specified for LuaDebuggerObjectID type: {0}", mSizeofObjectID));
			}
		}

		public void Write(double value)
		{
			if (BytesRemaining < 8)
				throw new ProtocolException("Output buffer overflow");
			if (!IsAligned(mPosition, 4))
				throw new ProtocolException("Output buffer alignment is not suitable for writing 'double'");

			byte[] bytes = BitConverter.GetBytes(value);
			Array.Copy(bytes, 0, mData, mPosition, bytes.Length);
			mPosition += bytes.Length;
		}

		public void Write(float value)
		{
			if (BytesRemaining < 4)
				throw new ProtocolException("Output buffer overflow");
			if (!IsAligned(mPosition, 4))
				throw new ProtocolException("Output buffer alignment is not suitable for writing 'float'");

			byte[] bytes = BitConverter.GetBytes(value);
			Array.Copy(bytes, 0, mData, mPosition, bytes.Length);
			mPosition += bytes.Length;
		}

		public void Write(Int64 value)
		{
			if (BytesRemaining < 8)
				throw new ProtocolException("Output buffer overflow");
			if (!IsAligned(mPosition, 4))
				throw new ProtocolException("Output buffer alignment is not suitable for writing 'Int64'");

			byte[] bytes = BitConverter.GetBytes(value);
			Array.Copy(bytes, 0, mData, mPosition, bytes.Length);
			mPosition += bytes.Length;
		}

		public void Write(int value)
		{
			if (BytesRemaining < 4)
				throw new ProtocolException("Output buffer overflow");
			if (!IsAligned(mPosition, 4))
				throw new ProtocolException("Output buffer alignment is not suitable for writing 'int'");

			byte[] bytes = BitConverter.GetBytes(value);
			Array.Copy(bytes, 0, mData, mPosition, bytes.Length);
			mPosition += bytes.Length;
		}

		public void Write(uint value)
		{
			if (BytesRemaining < 4)
				throw new ProtocolException("Output buffer overflow");
			if (!IsAligned(mPosition, 4))
				throw new ProtocolException("Output buffer alignment is not suitable for writing 'uint'");

			byte[] bytes = BitConverter.GetBytes(value);
			Array.Copy(bytes, 0, mData, mPosition, bytes.Length);
			mPosition += bytes.Length;
		}

		public void Write(short value)
		{
			if (BytesRemaining < 2)
				throw new ProtocolException("Output buffer overflow");
			if (!IsAligned(mPosition, 2))
				throw new ProtocolException("Output buffer alignment is not suitable for writing 'short'");

			byte[] bytes = BitConverter.GetBytes(value);
			Array.Copy(bytes, 0, mData, mPosition, bytes.Length);
			mPosition += bytes.Length;
		}

		public void Write(ushort value)
		{
			if (BytesRemaining < 2)
				throw new ProtocolException("Output buffer overflow");
			if (!IsAligned(mPosition, 2))
				throw new ProtocolException("Output buffer alignment is not suitable for writing 'ushort'");

			byte[] bytes = BitConverter.GetBytes(value);
			Array.Copy(bytes, 0, mData, mPosition, bytes.Length);
			mPosition += bytes.Length;
		}

		public void Write(char value)
		{
			if (BytesRemaining < 1)
				throw new ProtocolException("Output buffer overflow");

			byte[] bytes = BitConverter.GetBytes(value);
			Array.Copy(bytes, 0, mData, mPosition, bytes.Length);
			mPosition += bytes.Length;
		}

		public void Write(byte value)
		{
			if (BytesRemaining < 1)
				throw new ProtocolException("Output buffer overflow");

			byte[] bytes = BitConverter.GetBytes(value);
			Array.Copy(bytes, 0, mData, mPosition, bytes.Length);
			mPosition += bytes.Length;
		}

		public void Write(string value)
		{
			Write((int)value.Length);
			if (value.Length > 0)
			{
				char[] chars = value.ToCharArray();

				if (BytesRemaining < RoundUp(chars.Length, 4))
					throw new ProtocolException("Output buffer overflow");

				for (int index = 0; index < chars.Length; ++index)
					mData[mPosition + index] = (byte)(chars[index]);
				mPosition += RoundUp(chars.Length, 4);
			}
		}

		public void Write(byte [] value)
		{
			Write((int)value.Length);
			if (value.Length > 0)
			{
				if (BytesRemaining < RoundUp(value.Length, 4))
					throw new ProtocolException("Output buffer overflow");

				for (int index = 0; index < value.Length; ++index)
					mData[mPosition + index] = (byte)(value[index]);
				mPosition += RoundUp(value.Length, 4);
			}
		}


		public void Write(int value, int position)
		{
			if (position < 0 || position > mData.Length - 4)
				throw new ProtocolException("Output buffer overflow");
			if (!IsAligned(position, 4))
				throw new ProtocolException("Output buffer alignment is not suitable for writing 'int'");

			byte[] bytes = BitConverter.GetBytes(value);
			Array.Copy(bytes, 0, mData, position, bytes.Length);
		}

		public void Write(uint value, int position)
		{
			if (position < 0 || position > mData.Length - 4)
				throw new ProtocolException("Output buffer overflow");
			if (!IsAligned(position, 4))
				throw new ProtocolException("Output buffer alignment is not suitable for writing 'uint'");

			byte[] bytes = BitConverter.GetBytes(value);
			Array.Copy(bytes, 0, mData, position, bytes.Length);
		}

		public void Write(short value, int position)
		{
			if (position < 0 || position > mData.Length - 2)
				throw new ProtocolException("Output buffer overflow");
			if (!IsAligned(position, 2))
				throw new ProtocolException("Output buffer alignment is not suitable for writing 'short'");

			byte[] bytes = BitConverter.GetBytes(value);
			Array.Copy(bytes, 0, mData, position, bytes.Length);
		}

		public void Write(ushort value, int position)
		{
			if (position < 0 || position > mData.Length - 2)
				throw new ProtocolException("Output buffer overflow");
			if (!IsAligned(position, 2))
				throw new ProtocolException("Output buffer alignment is not suitable for writing 'ushort'");

			byte[] bytes = BitConverter.GetBytes(value);
			Array.Copy(bytes, 0, mData, position, bytes.Length);
		}

		public void Write(char value, int position)
		{
			if (position < 0 || position > mData.Length - 1)
				throw new ProtocolException("Output buffer overflow");

			byte[] bytes = BitConverter.GetBytes(value);
			Array.Copy(bytes, 0, mData, position, bytes.Length);
		}

		public void Write(byte value, int position)
		{
			if (position < 0 || position > mData.Length - 1)
				throw new ProtocolException("Output buffer overflow");

			byte[] bytes = BitConverter.GetBytes(value);
			Array.Copy(bytes, 0, mData, position, bytes.Length);
		}

		public static bool IsAligned(int value, int align)
		{
			return value % align == 0;
		}

		public static int RoundUp(int value, int align)
		{
			int diff = value % align;
			if (diff != 0)
				return value + (align - diff);
			else
				return value;
		}

		private byte[] mData;
		private int mPosition;
		private int mMessageStart;

		int mSizeofObjectID;
		int mSizeofNumber;

	}
}
