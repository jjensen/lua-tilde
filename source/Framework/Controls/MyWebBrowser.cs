
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Tilde.Framework.Controls
{
	public partial class MyWebBrowser : WebBrowser
	{
		private class MyWebBrowserSiteBase : WebBrowser.WebBrowserSite
		{
			public MyWebBrowserSiteBase(WebBrowser host)
				: base(host)
			{

			}
		}

		public MyWebBrowser()
		{
			InitializeComponent();
		}

		protected override void WndProc(ref Message m)
		{
			switch(m.Msg)
			{
				case Win32.WM_KEYDOWN:
					return;

				default:
					break;
			}

			base.WndProc(ref m);
		}

		/*
		public IDocHostUIHandler BrowserSite
		{
			get { return ((ICustomDoc) this.Document.DomDocument). }
		}

		protected override WebBrowserSiteBase CreateWebBrowserSiteBase()
		{
			m_browserSite = new MyWebBrowserSiteBase(this);
			return m_browserSite;
		}

		public void SetUIHandler(IDocHostUIHandler handler)
		{
			ICustomDoc doc = (ICustomDoc) this.Document.DomDocument;
			doc.SetUIHandler(handler);
		}

		#region IDocHostUIHandler Members

		void IDocHostUIHandler.EnableModeless(int fEnable)
		{
			//			throw new Exception("The method or operation is not implemented.");
		}

		void IDocHostUIHandler.FilterDataObject(MsHtmHstInterop.IDataObject pDO, out MsHtmHstInterop.IDataObject ppDORet)
		{
			//			throw new Exception("The method or operation is not implemented.");
			ppDORet = null;
		}

		void IDocHostUIHandler.GetDropTarget(MsHtmHstInterop.IDropTarget pDropTarget, out MsHtmHstInterop.IDropTarget ppDropTarget)
		{
			//			throw new Exception("The method or operation is not implemented.");
			ppDropTarget = null;
		}

		void IDocHostUIHandler.GetExternal(out object ppDispatch)
		{
			//			throw new Exception("The method or operation is not implemented.");
			ppDispatch = null;
		}

		void IDocHostUIHandler.GetHostInfo(ref _DOCHOSTUIINFO pInfo)
		{
			//			throw new Exception("The method or operation is not implemented.");
		}

		void IDocHostUIHandler.GetOptionKeyPath(out string pchKey, uint dw)
		{
			//			throw new Exception("The method or operation is not implemented.");
			pchKey = null;
		}

		void IDocHostUIHandler.HideUI()
		{
			outputBrowser.BrowserSite.HideUI();
		}

		void IDocHostUIHandler.OnDocWindowActivate(int fActivate)
		{
			//			throw new Exception("The method or operation is not implemented.");
		}

		void IDocHostUIHandler.OnFrameWindowActivate(int fActivate)
		{
			//			throw new Exception("The method or operation is not implemented.");
		}

		void IDocHostUIHandler.ResizeBorder(ref tagRECT prcBorder, IOleInPlaceUIWindow pUIWindow, int fRameWindow)
		{
			//			throw new Exception("The method or operation is not implemented.");
		}

		void IDocHostUIHandler.ShowContextMenu(uint dwID, ref tagPOINT ppt, object pcmdtReserved, object pdispReserved)
		{
			//			throw new Exception("The method or operation is not implemented.");
		}

		void IDocHostUIHandler.ShowUI(uint dwID, IOleInPlaceActiveObject pActiveObject, IOleCommandTarget pCommandTarget, IOleInPlaceFrame pFrame, IOleInPlaceUIWindow pDoc)
		{
			outputBrowser.BrowserSite.ShowUI(dwID, pActiveObject, pCommandTarget, pFrame, pDoc);
		}

		void IDocHostUIHandler.TranslateAccelerator(ref tagMSG lpmsg, ref Guid pguidCmdGroup, uint nCmdID)
		{
			//			throw new Exception("The method or operation is not implemented.");
		}

		void IDocHostUIHandler.TranslateUrl(uint dwTranslate, ref ushort pchURLIn, IntPtr ppchURLOut)
		{
			//			throw new Exception("The method or operation is not implemented.");
		}

		void IDocHostUIHandler.UpdateUI()
		{
			//			throw new Exception("The method or operation is not implemented.");
		}

		#endregion
*/
	}

}
