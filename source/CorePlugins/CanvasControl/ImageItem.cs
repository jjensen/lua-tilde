
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
using System.Drawing;
using System.IO;

namespace Tilde.CorePlugins.CanvasControl
{
	public class ImageItem : CanvasItem
	{
		Image mImage;
		string mSourceFileName;

		public ImageItem(string filename)
		{
			mImage = null;
			mSourceFileName = null;

			SetImage(filename);
		}

		public ImageItem(string filename, Point position)
			: base(position)
		{
			mImage = null;
			mSourceFileName = null;

			SetImage(filename);
		}

		public string SourceFileName
		{
			get { return mSourceFileName; }
			set	{ SetImage(value); }
		}

		public override void Paint(Graphics graphics)
		{
			if (mImage != null)
				graphics.DrawImage(mImage, Position);
		}

		public override Rectangle BoundingBox
		{
			get 
			{
				if (mImage == null)
					return new Rectangle();
				else
					return new Rectangle(Position, mImage.Size);
			}
		}

		private void SetImage(string fileName)
		{
			if (mSourceFileName != fileName)
			{
				mSourceFileName = fileName;
				if (mImage != null)
				{
					mImage.Dispose();
					mImage = null;
				}

				try
				{
					mImage = Image.FromFile(fileName);
				}
				catch(OutOfMemoryException)
				{

				}
				catch(FileNotFoundException)
				{

				}
			}

		}
	}
}
