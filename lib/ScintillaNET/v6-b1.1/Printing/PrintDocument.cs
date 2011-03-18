using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace Scintilla.Printing {
    /// <summary>
    /// ScintillaNET derived class for handling printing of source code from a Scintilla control.
    /// </summary>
    public class PrintDocument : System.Drawing.Printing.PrintDocument {
        private ScintillaControl m_oScintillaControl;

        private int m_iTextLength;
        private int m_iLastPrintPosition;
        private int m_iCurrentPrintPage;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="oScintillaControl">Scintilla control being printed</param>
        public PrintDocument(ScintillaControl oScintillaControl) {
            m_oScintillaControl = oScintillaControl;
        }

        protected override void OnBeginPrint(PrintEventArgs e) {
            base.OnBeginPrint(e);

            m_iTextLength = m_oScintillaControl.TextLength;
            m_iLastPrintPosition = 0;
            m_iCurrentPrintPage = 1;
        }

        protected override void OnEndPrint(PrintEventArgs e) {
            base.OnEndPrint(e);
        }

        protected override void OnPrintPage(PrintPageEventArgs e) {
            base.OnPrintPage(e);

            PageInformation oHeader = PageSettings.DefaultHeader;
            PageInformation oFooter = PageSettings.DefaultFooter;
            Rectangle oPrintBounds = e.MarginBounds;
            bool bIsPreview = this.PrintController.IsPreview;

            // When not in preview mode, adjust graphics to account for hard margin of the printer
            if (!bIsPreview) {
                e.Graphics.TranslateTransform(-e.PageSettings.HardMarginX, -e.PageSettings.HardMarginY);
            }

            // Draw border for test purposes
            e.Graphics.DrawRectangle(Pens.LightBlue, e.MarginBounds);

            // Get the header and footer provided if using Scintilla.Printing.PageSettings
            if (e.PageSettings is PageSettings) {
                oHeader = (e.PageSettings as PageSettings).Header;
                oFooter = (e.PageSettings as PageSettings).Footer;
            }

            // Draw the header and footer and get remainder of page bounds
            oPrintBounds = DrawHeader(e.Graphics, oPrintBounds, oHeader);
            oPrintBounds = DrawFooter(e.Graphics, oPrintBounds, oFooter);

            // When not in preview mode, adjust page bounds to account for hard margin of the printer
            if (!bIsPreview) {
                oPrintBounds.Offset((int) -e.PageSettings.HardMarginX, (int) -e.PageSettings.HardMarginY);
            }
            DrawCurrentPage(e.Graphics, oPrintBounds);
                        
            // Increment the page count and determine if there are more pages to be printed
            m_iCurrentPrintPage++;
            e.HasMorePages = (m_iLastPrintPosition < m_iTextLength);
        }

        private Rectangle DrawHeader(Graphics oGraphics, Rectangle oBounds, PageInformation oHeader) {
            if (oHeader.Display) {
                Rectangle oHeaderBounds = new Rectangle(oBounds.Left, oBounds.Top, oBounds.Width, oHeader.Height);

                oHeader.Draw(oGraphics, oHeaderBounds, this.DocumentName, m_iCurrentPrintPage);

                return new Rectangle(
                    oBounds.Left, oBounds.Top + oHeaderBounds.Height + oHeader.Margin,
                    oBounds.Width, oBounds.Height - oHeaderBounds.Height - oHeader.Margin
                    );
            }
            else {
                return oBounds;
            }
        }

        private Rectangle DrawFooter(Graphics oGraphics, Rectangle oBounds, PageInformation oFooter) {
            if (oFooter.Display) {
                int iHeight = oFooter.Height;
                Rectangle oFooterBounds = new Rectangle(oBounds.Left, oBounds.Bottom - iHeight, oBounds.Width, iHeight);

                oFooter.Draw(oGraphics, oFooterBounds, this.DocumentName, m_iCurrentPrintPage);

                return new Rectangle(
                    oBounds.Left, oBounds.Top,
                    oBounds.Width, oBounds.Height - oFooterBounds.Height - oFooter.Margin
                    );
            }
            else {
                return oBounds;
            }
        }

        private void DrawCurrentPage(Graphics oGraphics, Rectangle oBounds) {
            Point[] oPoints = {
                new Point(oBounds.Left, oBounds.Top),
                new Point(oBounds.Right, oBounds.Bottom)
                };
            oGraphics.TransformPoints(CoordinateSpace.Device, CoordinateSpace.Page, oPoints);

            PrintRectangle oPrintRectangle = new PrintRectangle(oPoints[0].X, oPoints[0].Y, oPoints[1].X, oPoints[1].Y);

            RangeToFormat oRangeToFormat = new RangeToFormat();
            oRangeToFormat.hdc = oRangeToFormat.hdcTarget = oGraphics.GetHdc();
            oRangeToFormat.rc = oRangeToFormat.rcPage = oPrintRectangle;
            oRangeToFormat.chrg.cpMin = m_iLastPrintPosition;
            oRangeToFormat.chrg.cpMax = m_iTextLength;

            m_iLastPrintPosition += FormatRange(oRangeToFormat);
        }

        internal unsafe int FormatRange(RangeToFormat oRangeToFormat) {
            return (int) m_oScintillaControl.SendMessageDirect(2151, (IntPtr) 1, (IntPtr) (&oRangeToFormat));
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RangeToFormat {
            public IntPtr hdc; // The HDC (device context) we print to
            public IntPtr hdcTarget; // The HDC we use for measuring (may be same as hdc)
            public PrintRectangle rc; // Rectangle in which to print
            public PrintRectangle rcPage; // Physically printable page size
            public CharacterRange chrg; // Range of characters to print
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct PrintRectangle {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public PrintRectangle(int iLeft, int iTop, int iRight, int iBottom) {
                Left = iLeft;
                Top = iTop;
                Right = iRight;
                Bottom = iBottom;
            }
        }
    }
}
