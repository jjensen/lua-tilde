
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
using System.Net.Sockets;
using System.Threading;
using System.IO;

using Tilde.LuaDebugger;
using Tilde.Framework;

namespace Tilde.LuaSocketConnection
{
	/// <summary>
	/// 
	/// </summary>
	/// <note>
	/// Most of the functions in this class should be run from the debugger thread only. The exceptions
	/// are the constructor which runs in the main thread, and the two socket callback functions which
	/// are called from a .NET pooled thread.
	/// </note>
	public class SocketConnection : IConnection
	{
		SocketTransport m_transport;
		SocketHostInfo m_info;
		Socket m_socket;
//		ThreadedTarget m_target;

		private object m_lock = new Object();

		byte[] m_asyncReadBuffer;
		AutoResetEvent m_asyncConnect;
//		AutoResetEvent m_asyncDataAvailable;

//		ReceiveMessageBuffer m_readBuffer;
//		SendMessageBuffer m_writeBuffer;


		public SocketConnection(SocketTransport transport, SocketHostInfo info)
		{
			m_transport = transport;
			m_info = info;

			m_asyncConnect = new AutoResetEvent(false);

			m_asyncReadBuffer = new byte[512 * 1024];
//			m_asyncDataAvailable = new AutoResetEvent(false);

//			m_readBuffer = new ReceiveMessageBuffer(512 * 1024);
//			m_writeBuffer = new SendMessageBuffer(4 * 1024);
		}

		public event ConnectionClosedEventHandler ConnectionClosed;
		public event ConnectionAbortedEventHandler ConnectionAborted;
		public event ConnectionDataReceivedEventHandler DataReceived;

        public HostInfo HostInfo
        {
            get { return m_info; }
        }

		public void Send(byte[] buffer, int pos, int len)
		{
			lock (m_lock)
			{
				m_socket.Send(buffer, pos, len, SocketFlags.None);
			}
		}

		public bool DownloadFile(string fileName)
		{
			FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			int len = (int)stream.Length;
			byte[] buffer = new byte[len];
			try
			{
				stream.Read(buffer, 0, len);
			}
			finally
			{
				stream.Close();
			}

			int sendlen = SendMessageBuffer.RoundUp(len, 4);
			string normName = m_transport.DebugManager.FindTargetFile(fileName);
			//string normName = PathUtils.NormaliseFileName(fileName, m_transport.DebugManager.Manager.Project.BaseDirectory);

			TcpClient socket = new TcpClient(m_info.mHost, m_info.mPort + 2);
			char[] charBuffer = normName.ToCharArray();
			byte[] nameBuffer = new byte[charBuffer.Length];
			for (int index = 0; index < charBuffer.Length; ++index)
				nameBuffer[index] = (byte)charBuffer[index];
			socket.GetStream().Write(nameBuffer, 0, nameBuffer.Length);
			socket.GetStream().WriteByte((byte)'\n');
			socket.GetStream().Write(buffer, 0, len);
			socket.Close();

			return true;
		}

		// This function is called from a pooled thread
		private static void ConnectCallback(IAsyncResult ar)
		{
			SocketConnection connection = (SocketConnection)ar.AsyncState;

			lock (connection.m_lock)
			{
//				if (connection.m_target.IsShutdown)
				if (connection.m_socket == null)
					return;

				try
				{
					connection.m_socket.EndConnect(ar);
				}
				catch (SocketException /*ex*/)
				{
//					connection.m_target.Abort(String.Format("Network connection to {0}:{1} failed!\r\n{2}", connection.m_info.mHost, connection.m_info.mPort, ex.ToString()));
//					connection.OnConnectionAborted(String.Format("Network connection to {0}:{1} failed!\r\n{2}", connection.m_info.mHost, connection.m_info.mPort, ex.ToString()));
					connection.m_socket = null;
				}
				connection.m_asyncConnect.Set();
			}
		}

		// This function is called from a pooled thread
		private static void ReceiveCallback(IAsyncResult ar)
		{
			SocketConnection connection = (SocketConnection)ar.AsyncState;

			lock (connection.m_lock)
			{
//				if (connection.m_target.IsShutdown)
				if (connection.m_socket == null)
					return;

				try
				{
					// Process any messages received over the socket
					int bytes = connection.m_socket.EndReceive(ar);

					// The socket is closed, so don't ask for any more
					if (bytes == 0)
					{
						//connection.m_target.Abort();
						connection.OnConnectionClosed();
					}
					else
					{
						connection.OnDataReceived(connection.m_asyncReadBuffer, bytes);
						//						connection.m_target.Receive(connection.m_asyncReadBuffer, bytes);
						//					Array.Copy(connection.m_asyncReadBuffer, 0, connection.m_readBuffer.Buffer, connection.m_readBuffer.DataLength, bytes);
						//					connection.m_readBuffer.DataLength += bytes;
						//					connection.m_asyncDataAvailable.Set();

						// Start a new asynchronous read
//						if (!connection.m_target.IsShutdown)
							connection.m_socket.BeginReceive(connection.m_asyncReadBuffer, 0, connection.m_asyncReadBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), connection);
					}
				}
				catch (SocketException ex)
				{
//					connection.m_target.Abort(ex.ToString());
					connection.OnConnectionAborted(ex.ToString());
				}
			}
		}

		public void Connect(/*ThreadedTarget target*/)
		{
			lock (m_lock)
			{
//				m_target = target;
				if (m_socket == null)
				{
					m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

					// Start an asynchronous connect
					m_socket.BeginConnect(m_info.mHost, m_info.mPort, new AsyncCallback(ConnectCallback), this);
				}
			}
		}

		public WaitHandle [] Handles
		{
			get { return new WaitHandle[] { m_asyncConnect, /* m_asyncDataAvailable */ }; }
		}

		public void OnSignalled(WaitHandle handle)
		{
			lock (m_lock)
			{
				if (handle == m_asyncConnect)
				{
					// Start an asynchronous read
					if (m_socket != null)
						m_socket.BeginReceive(m_asyncReadBuffer, 0, m_asyncReadBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), this);
				}

				/*
			else if(handle == m_asyncDataAvailable)
			{
				// Process any messages that have been sent from the target
				m_target.ProcessMessages();
			}
				 */
			}
		}

		public void Shutdown()
		{
			lock (m_lock)
			{
				if (m_socket.Connected)
				{
					m_socket.Shutdown(SocketShutdown.Both);
					m_socket.Disconnect(false);
				}

				m_socket.Close();
				m_socket = null;
			}
		}

		private void OnConnectionClosed()
		{
			if (ConnectionClosed != null)
				ConnectionClosed(this);
		}

		private void OnConnectionAborted(string message)
		{
			if (ConnectionAborted != null)
				ConnectionAborted(this, message);
		}

		private void OnDataReceived(byte [] buffer, int bytes)
		{
			if (DataReceived != null)
				DataReceived(this, buffer, bytes);
		}
	}
}
