
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
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

using Tilde.LuaDebugger;
using Tilde.Framework.Model;
using Tilde.Framework.Controller;
using System.Net;

namespace Tilde.LuaSocketConnection
{
	

	[TransportClassAttribute("TCP/IP")]
	public class SocketTransport : ITransport
	{
		DebugManager mDebugger;
		List<SocketHostInfo> mBookmarkedHosts = new List<SocketHostInfo>();
		string mAutoConnectTCPHost;
		Socket mTargetListener;

		private object mLock = new Object();

		#region ITransport Members

		public SocketTransport(DebugManager manager)
		{
			mDebugger = manager;
			mDebugger.Manager.ProjectOpened += new ProjectOpenedEventHandler(Manager_ProjectOpened);
			mDebugger.Manager.ProjectClosed += new ProjectClosedEventHandler(Manager_ProjectClosed);
			mDebugger.MainWindow.FormClosed += new FormClosedEventHandler(MainWindow_FormClosed);

			mDebugger.Options.OptionsChanged += new OptionsChangedDelegate(Options_OptionsChanged);
		}

		void Options_OptionsChanged(IOptions sender, string option)
		{
			if(option == "PingbackPort")
				InitialisePingbackListener(mDebugger.Options.PingbackPort);
		}

		/// <summary>
		/// Set up a socket for listening to a remote target starting up.
		/// </summary>
		/// <param name="manager"></param>
		private void InitialisePingbackListener(int port)
		{
			if(mTargetListener != null)
			{
				mTargetListener.Close();
				mTargetListener = null;
			}

			if (port <= 0)
				return;

			mTargetListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			mTargetListener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);
			IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);
			while (true)
			{
				try
				{
					mTargetListener.Bind(ep);
					mTargetListener.Listen(1);
					mTargetListener.BeginAccept(new AsyncCallback(OnTargetListenerConnected), new object[] { this, mTargetListener });
					break;
				}
				catch (SocketException ex)
				{
					DialogResult result = MessageBox.Show(
						"A listening socket could not be created on port " + port.ToString() + ". Tilde will not be able to automatically connect when the target resets.\r\n\r\n" + ex.ToString(),
						"TCP error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

					if (result == DialogResult.Cancel)
						break;
				}
			}
		}
		void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
		{
			lock (mLock)
			{
				if (mTargetListener != null)
				{
					mTargetListener.Close();
					mTargetListener = null;
				}
			}
		}

		public DebugManager DebugManager
		{
			get { return mDebugger; }
		}

		static void OnTargetListenerConnected(IAsyncResult ar)
		{
			object[] args = (object[])ar.AsyncState;
			SocketTransport transport = (SocketTransport)args[0];
			Socket listener = (Socket)args[1];
			lock (transport.mLock)
			{
				if (listener == transport.mTargetListener)
				{
					Socket client = listener.EndAccept(ar);
					IPEndPoint endpoint = (IPEndPoint)client.RemoteEndPoint;
					HostInfo hostInfo = new SocketHostInfo(transport, endpoint.Address.ToString(), 51337);
					client.Close();

					try
					{
						transport.DebugManager.MainWindow.Invoke(new CallbackDelegate(Callback), new object[] { transport, hostInfo });
						listener.BeginAccept(new AsyncCallback(OnTargetListenerConnected), new object[] { transport, listener } );
					}
					catch(InvalidOperationException)
					{
						// It's possible the main window hasn't finished creating here
					}
				}
			}
		}

		delegate void CallbackDelegate(SocketTransport transport, HostInfo hostInfo);

		static void Callback(SocketTransport transport, HostInfo hostInfo)
		{
			transport.DebugManager.Disconnect(false, false);
			transport.DebugManager.Connect(hostInfo);
		}

		void Manager_ProjectOpened(object sender, Project project)
		{
			mBookmarkedHosts.Clear();
			string hostString = project.GetUserConfiguration("TCPHosts");
			string[] currHosts = hostString.Split(new char[] { ',' });
			foreach (string hostinfo in currHosts)
			{
				string[] pair = hostinfo.Split(new char[] { ':' });
				if(pair.Length == 2)
					mBookmarkedHosts.Add(new SocketHostInfo(this, pair[0], Int32.Parse(pair[1])));
			}
			mAutoConnectTCPHost = project.GetUserConfiguration("AutoConnectTCPHost");
			if (mAutoConnectTCPHost != "")
			{
				string[] pair = mAutoConnectTCPHost.Split(new char[] { ':' });
				if (pair.Length == 2)
					mDebugger.Connect(new SocketHostInfo(this, pair[0], Int32.Parse(pair[1])));
			}
		}

		void Manager_ProjectClosed(object sender)
		{
			mBookmarkedHosts.Clear();
			mAutoConnectTCPHost = "";
		}

		public HostInfo[] EnumerateDevices()
		{
			List<HostInfo> result = new List<HostInfo>();

			result.Add(new HostInfo(this, "Add New Connection..."));

			foreach(SocketHostInfo info in mBookmarkedHosts)
			{
				result.Add(info);
			}

			return result.ToArray();
		}

		public virtual IConnection Connect(HostInfo hostInfo)
		{
            SocketHostInfo tcpHostInfo;

			if (hostInfo.Name == "Add New Connection...")
				tcpHostInfo = AddNewConnection();
			else
				tcpHostInfo = hostInfo as SocketHostInfo;

			if (tcpHostInfo == null)
			{
				mDebugger.Manager.Project.SetUserConfiguration("AutoConnectTCPHost", "");
				return null;
			}
			else
			{
				mDebugger.Manager.Project.SetUserConfiguration("AutoConnectTCPHost", tcpHostInfo.mHost + ":" + tcpHostInfo.mPort);
				return new SocketConnection(this, tcpHostInfo);
			}
		}

		public virtual void DisableAutoConnect()
		{
			mDebugger.Manager.Project.SetUserConfiguration("AutoConnectTCPHost", "");
		}

		protected SocketHostInfo AddNewConnection()
		{
			SocketHostInfo tcpHostInfo = null;

			AddNewConnectionDialog dialog = new AddNewConnectionDialog();
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				tcpHostInfo = new SocketHostInfo(this, dialog.Host, dialog.Port);

				mBookmarkedHosts.Add(tcpHostInfo);

				string[] hostList = mBookmarkedHosts.ConvertAll<string>(delegate(SocketHostInfo value) { return value.ToString(); }).ToArray();
				string hostString = String.Join(",", hostList);
				if (mDebugger.Manager.Project != null)
				{
					mDebugger.Manager.Project.SetUserConfiguration("TCPHosts", hostString);
				}

			}

			return tcpHostInfo;
		}

		#endregion
	}

}
