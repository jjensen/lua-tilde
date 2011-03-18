
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
using System.Threading;
using System.Reflection;

namespace Tilde.LuaDebugger
{
	public class MessageQueue
	{
		public class Message
		{
			protected Delegate m_delegate;

			public Message(Delegate del)
			{
				m_delegate = del;
			}

			public virtual void Invoke()
			{
				try
				{
					m_delegate.DynamicInvoke(null);
				}
				catch (TargetInvocationException ex)
				{
					throw (ex.InnerException);
				}
			}
		}

		public class BlockingMessage : Message
		{
			AutoResetEvent m_waitHandle;

			bool m_complete;
			object m_result;
			Exception m_exception;

			public BlockingMessage(Delegate del)
				: base(del)
			{
				m_waitHandle = new AutoResetEvent(false);
				m_complete = false;
				m_result = null;
				m_exception = null;
			}

			public WaitHandle AsyncWaitHandle
			{
				get { return m_waitHandle; }
			}

			public bool IsCompleted
			{
				get { return m_complete; }
			}

			public object Result
			{
				get { return m_result; }
			}

			public Exception Exception
			{
				get { return m_exception; }
			}

			public override void Invoke()
			{
				try
				{
					m_result = m_delegate.DynamicInvoke(null);
				}
				catch (TargetInvocationException ex)
				{
					m_exception = ex.InnerException;
				}

				m_complete = true;
				m_waitHandle.Set();
			}
		}

		public MessageQueue()
		{
			mMessages = new Queue<Message>();
		}

		public void Push(Message message)
		{
			lock(this)
			{
				mMessages.Enqueue(message);
			}
		}

		public bool Empty
		{
			get
			{
				lock (this)
				{
					return mMessages.Count == 0;
				}
			}
		}

		public Message Pop()
		{
			lock (this)
			{
				return mMessages.Dequeue();
			}
		}

		private Queue<Message> mMessages;
	}
}
