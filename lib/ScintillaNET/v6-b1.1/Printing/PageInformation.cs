using System;
using System.Drawing;

namespace Scintilla.Printing {
    /// <summary>
    /// Type of border to print for a Page Information section
    /// </summary>
    public enum PageInformationBorder {
        /// <summary>
        /// No border
        /// </summary>
        None,
        /// <summary>
        /// Border along the top
        /// </summary>
        Top,
        /// <summary>
        /// Border along the bottom
        /// </summary>
        Bottom,
        /// <summary>
        /// A full border around the page information section
        /// </summary>
        Box
    }

    /// <summary>
    /// Type of data to display at one of the positions in a Page Information section
    /// </summary>
    public enum InformationType {
        /// <summary>
        /// Nothing is displayed at the position
        /// </summary>
        Nothing,
        /// <summary>
        /// The page number is displayed in the format "Page #"
        /// </summary>
        PageNumber,
        /// <summary>
        /// The document name is displayed
        /// </summary>
        DocumentName
    }

    /// <summary>
    /// Class for determining how and what to print for a header or footer.
    /// </summary>
    public class PageInformation {
        /// <summary>
        /// Default font used for Page Information sections
        /// </summary>
        public static readonly Font DefaultFont = new Font("Arial", 8);

        private const int c_iBorderSpace = 2;

        private int m_iMargin;
        private Font m_oFont;
        private PageInformationBorder m_eBorder;
        private InformationType m_eLeft;
        private InformationType m_eCenter;
        private InformationType m_eRight;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public PageInformation()
            : this(PageInformationBorder.None, InformationType.Nothing, InformationType.Nothing, InformationType.Nothing) {
        }

        /// <summary>
        /// Normal Use Constructor
        /// </summary>
        /// <param name="eBorder">Border style</param>
        /// <param name="eLeft">What to print on the left side of the page</param>
        /// <param name="eCenter">What to print in the center of the page</param>
        /// <param name="eRight">What to print on the right side of the page</param>
        public PageInformation(PageInformationBorder eBorder, InformationType eLeft, InformationType eCenter, InformationType eRight)
            : this(3, DefaultFont, eBorder, eLeft, eCenter, eRight) {
        }

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="iMargin">Margin to use</param>
        /// <param name="oFont">Font to use </param>
        /// <param name="eBorder">Border style</param>
        /// <param name="eLeft">What to print on the left side of the page</param>
        /// <param name="eCenter">What to print in the center of the page</param>
        /// <param name="eRight">What to print on the right side of the page</param>
        public PageInformation(int iMargin, Font oFont, PageInformationBorder eBorder, InformationType eLeft, InformationType eCenter, InformationType eRight) {
            m_iMargin = iMargin;
            m_oFont = oFont;
            m_eBorder = eBorder;
            m_eLeft = eLeft;
            m_eCenter = eCenter;
            m_eRight = eRight;
        }

        #region Properties

        /// <summary>
        /// Space between the Page Information section and the rest of the page
        /// </summary>
        public int Margin {
            get { return m_iMargin; }
            set { m_iMargin = value; }
        }

        /// <summary>
        /// Font used in printing the Page Information section
        /// </summary>
        public Font Font {
            get { return m_oFont; }
            set { m_oFont = value; }
        }

        /// <summary>
        /// Border style used for the Page Information section
        /// </summary>
        public PageInformationBorder Border {
            get { return m_eBorder; }
            set { m_eBorder = value; }
        }

        /// <summary>
        /// Information printed on the left side of the Page Information section
        /// </summary>
        public InformationType Left {
            get { return m_eLeft; }
            set { m_eLeft = value; }
        }

        /// <summary>
        /// Information printed in the center of the Page Information section
        /// </summary>
        public InformationType Center {
            get { return m_eCenter; }
            set { m_eCenter = value; }
        }

        /// <summary>
        /// Information printed on the right side of the Page Information section
        /// </summary>
        public InformationType Right {
            get { return m_eRight; }
            set { m_eRight = value; }
        }

        #endregion

        /// <summary>
        /// Whether there is a need to display this item, true if left, center, or right are not nothing.
        /// </summary>
        public bool Display {
            get {  
                return (m_eLeft != InformationType.Nothing) || 
                    (m_eCenter != InformationType.Nothing) || 
                    (m_eRight != InformationType.Nothing); 
            }
        }

        /// <summary>
        /// Height required to draw the Page Information section based on the options selected.
        /// </summary>
        public int Height {
            get {
                int iHeight = Font.Height;

                switch (m_eBorder) {
                    case PageInformationBorder.Top:
                    case PageInformationBorder.Bottom:
                        iHeight += c_iBorderSpace;
                        break;

                    case PageInformationBorder.Box:
                        iHeight += 2 * c_iBorderSpace;
                        break;

                    case PageInformationBorder.None:
                    default:
                        break;
                }

                return iHeight;
            }
        }

        /// <summary>
        /// Draws the page information section in the specified rectangle
        /// </summary>
        /// <param name="oGraphics"></param>
        /// <param name="oBounds"></param>
        /// <param name="strDocumentName"></param>
        /// <param name="iPageNumber"></param>
        public void Draw(Graphics oGraphics, Rectangle oBounds, String strDocumentName, int iPageNumber) {
            StringFormat oFormat = new StringFormat(StringFormat.GenericDefault);
            Pen oPen = Pens.Black;
            Brush oBrush = Brushes.Black;

            // Draw border
            switch (m_eBorder) {
                case PageInformationBorder.Top:
                    oGraphics.DrawLine(oPen, oBounds.Left, oBounds.Top, oBounds.Right, oBounds.Top);
                    break;
                case PageInformationBorder.Bottom:
                    oGraphics.DrawLine(oPen, oBounds.Left, oBounds.Bottom, oBounds.Right, oBounds.Bottom);
                    break;
                case PageInformationBorder.Box:
                    oGraphics.DrawRectangle(oPen, oBounds);
                    oBounds = new Rectangle(oBounds.Left + c_iBorderSpace, oBounds.Top, oBounds.Width - (2 * c_iBorderSpace), oBounds.Height);
                    break;
                case PageInformationBorder.None:
                default:
                    break;
            }

            // Center vertically
            oFormat.LineAlignment = StringAlignment.Center;

            // Draw left side
            oFormat.Alignment = StringAlignment.Near;
            switch (m_eLeft) {
                case InformationType.DocumentName:
                    oGraphics.DrawString(strDocumentName, m_oFont, oBrush, oBounds, oFormat);
                    break;
                case InformationType.PageNumber:
                    oGraphics.DrawString("Page " + iPageNumber, m_oFont, oBrush, oBounds, oFormat);
                    break;
                case InformationType.Nothing:
                default:
                    break;
            }

            // Draw center
            oFormat.Alignment = StringAlignment.Center;
            switch (m_eCenter) {
                case InformationType.DocumentName:
                    oGraphics.DrawString(strDocumentName, m_oFont, oBrush, oBounds, oFormat);
                    break;
                case InformationType.PageNumber:
                    oGraphics.DrawString("Page " + iPageNumber, m_oFont, oBrush, oBounds, oFormat);
                    break;
                case InformationType.Nothing:
                default:
                    break;
            }

            // Draw right side
            oFormat.Alignment = StringAlignment.Far;
            switch (m_eRight) {
                case InformationType.DocumentName:
                    oGraphics.DrawString(strDocumentName, m_oFont, oBrush, oBounds, oFormat);
                    break;
                case InformationType.PageNumber:
                    oGraphics.DrawString("Page " + iPageNumber, m_oFont, oBrush, oBounds, oFormat);
                    break;
                case InformationType.Nothing:
                default:
                    break;
            }
        }
    }
}
