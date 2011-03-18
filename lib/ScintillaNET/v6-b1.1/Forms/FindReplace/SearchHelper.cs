using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;

namespace Scintilla.Forms
{
    public class SearchHelper
    {
        #region Singleton
        private static SearchHelper helper = new SearchHelper();
        public static SearchHelper Instance(ScintillaControl editControl)
        {
            if (helper.EditControl != editControl)
            {
                helper.EditControl = editControl;

                // Define the Find Marker and set its color
                helper.EditControl.MarkerDefine(FIND_MARKER, Scintilla.Enums.MarkerSymbol.RoundRectangle);
                helper.EditControl.MarkerSetBackgroundColor(FIND_MARKER, (255 | (255 << 8) | (0 << 16)));
                helper.EditControl.MarkerSetForegroundColor(FIND_MARKER, 0);
                helper.EditControl.SetVisiblePolicy((int)Scintilla.Enums.CaretPolicy.Strict, 0);
                helper.EditControl.SetXCaretPolicy((int)(Scintilla.Enums.CaretPolicy.Jumps | Scintilla.Enums.CaretPolicy.Even), 0);
            }

            return helper;
        }
        #endregion

        private const int FIND_MARKER = 13;
        private ScintillaControl editControl;
        private SearchCriteria criteria = new SearchCriteria();
        
        private SearchHelper() {}

        public SearchCriteria Criteria
        {
            get { return criteria; }
        }

        public ScintillaControl EditControl
        {
            get { return editControl; }
            set { editControl = value; }
        }

        public int ReplaceNext()
        {
            int i = 0;
            if (FindNextAndHighlight())
            {
                ReplaceHighlightedText();
                ++i;
            }
            return i;
        }

        public int ReplaceAll()
        {
            int i = 0,
                min = 0,
                max = editControl.Length,
                diff = Criteria.ReplaceTextTransformed.Length - Criteria.SearchTextTransformed.Length,
                savedPos = editControl.CurrentPos;

            if (Criteria.ReplaceInSelectionOnly)
            {
                min = (editControl.SelectionStart < editControl.SelectionEnd) ? editControl.SelectionStart : editControl.SelectionEnd;
                max = (editControl.SelectionStart < editControl.SelectionEnd) ? editControl.SelectionEnd : editControl.SelectionStart;
            }

            editControl.CurrentPos =
                editControl.SelectionStart = 
                editControl.SelectionEnd = min;

            while (FindNextAndHighlight(min, max))
            {
                ReplaceHighlightedText();
                max = max + diff;

                ++i;
            }

            editControl.CurrentPos = savedPos;
            editControl.SelectionStart = savedPos;
            editControl.SelectionEnd = savedPos;
            return i;
        }

        public int MarkAll()
        {
            int i = 0;
            int prevStartPos = (editControl.SelectionStart < editControl.SelectionEnd) ? editControl.SelectionStart : editControl.SelectionEnd,
                prevEndPos = (editControl.SelectionStart < editControl.SelectionEnd) ? editControl.SelectionEnd : editControl.SelectionStart;

            editControl.CurrentPos = 0;
            editControl.MarkerDeleteAll(13);
            while (FindNextAndMark()) { i++; }

            editControl.CurrentPos = prevEndPos;
            editControl.SelectionStart = prevStartPos;
            editControl.SelectionEnd = prevEndPos;
            return i;
        }

        public void ReplaceHighlightedText()
        {
            if (editControl.GetSelectedText().Length > 0)
            {
                int startPos = (editControl.SelectionStart < editControl.SelectionEnd) ? editControl.SelectionStart : editControl.SelectionEnd;

                editControl.ReplaceSelection(Criteria.ReplaceTextTransformed);

                editControl.CurrentPos = startPos + Criteria.ReplaceTextTransformed.Length;
                editControl.SelectionStart = editControl.CurrentPos;
                editControl.SelectionEnd = editControl.CurrentPos;
            }
        }

        public bool FindNextAndMark()
        {
            return FindNextAndMark(0, editControl.Length);
        }

        public bool FindNextAndMark(int min, int max)
        {
            bool foundMatch = FindNextAndHighlight(min, max);

            if (foundMatch)
            {
                editControl.MarkerAdd(editControl.LineFromPosition(editControl.CurrentPos), FIND_MARKER);
            }

            return foundMatch;
        }

        public bool FindNextAndHighlight()
        {
            bool found = FindNextAndHighlight(0, editControl.Length);
            if (!found && Criteria.Wraparound)
            {
                int savedPos = editControl.CurrentPos;

                this.editControl.CurrentPos =
                    this.editControl.SelectionStart =
                    this.editControl.SelectionEnd = Criteria.SearchDown ? 0 : (editControl.Length - 1);

                found = FindNextAndHighlight(0, editControl.Length);

                if (!found)
                {
                    this.editControl.CurrentPos =
                    this.editControl.SelectionStart =
                    this.editControl.SelectionEnd = savedPos;
                }
            }

            return found;
        }

        public bool FindNextAndHighlight(int min, int max)
        {
            bool foundMatch = false;
            int flags = 0,
                lineIndex = 0,
                startPos = 0,
                endPos = 0,
                prevStartPos = (editControl.SelectionStart < editControl.SelectionEnd) ? editControl.SelectionStart : editControl.SelectionEnd,
                prevEndPos = (editControl.SelectionStart < editControl.SelectionEnd) ? editControl.SelectionEnd : editControl.SelectionStart;

            if (Criteria.IsCaseSensitive)
                flags = flags | (int)Scintilla.Enums.FindOption.MatchCase;
            if (Criteria.IsRegularExpression)
                flags = flags | (int)Scintilla.Enums.FindOption.RegularExpression;
            if (Criteria.MatchWholeWordOnly)
                flags = flags | (int)Scintilla.Enums.FindOption.WholeWord;

            if (Criteria.SearchDown)
            {
                editControl.CurrentPos =
                editControl.SelectionStart =
                editControl.SelectionEnd =
                (editControl.SelectionStart < editControl.SelectionEnd) ? editControl.SelectionEnd : editControl.SelectionStart;

                editControl.SearchAnchor();

                startPos = this.editControl.SearchNext(flags, criteria.SearchTextTransformed);
            }
            else
            {
                editControl.CurrentPos =
                editControl.SelectionStart =
                editControl.SelectionEnd =
                (editControl.SelectionStart < editControl.SelectionEnd) ? editControl.SelectionStart : editControl.SelectionEnd;

                editControl.SearchAnchor();

                startPos = this.editControl.SearchPrevious(flags, criteria.SearchText);
            }

            foundMatch = (startPos >= 0);

            if (foundMatch)
            {
                startPos = (editControl.SelectionStart < editControl.SelectionEnd) ? editControl.SelectionStart : editControl.SelectionEnd;
                endPos = (editControl.SelectionStart < editControl.SelectionEnd) ? editControl.SelectionEnd : editControl.SelectionStart;

                if (startPos >= min && endPos <= max)
                {
                    lineIndex = editControl.LineFromPosition(editControl.CurrentPos);
                    if (Criteria.GrabFocusAfterSearch)
                    {
                        editControl.GrabFocus();
                    }
                    editControl.EnsureVisibleEnforcePolicy(lineIndex);
                    editControl.ScrollCaret();

                    editControl.CurrentPos = endPos;
                    editControl.SelectionStart = startPos;
                    editControl.SelectionEnd = endPos;
                }
                else
                {
                    foundMatch = false;
                    editControl.CurrentPos = prevEndPos;
                    editControl.SelectionStart = prevStartPos;
                    editControl.SelectionEnd = prevEndPos;
                }
            }

            return foundMatch;
        }

        public void MoveFormAwayFromSelection(Form form)
        {
            if (criteria.MoveFormAwayFromSelection && (form != null))
            {
                int pos = editControl.CurrentPos;
                int x = editControl.PointXFromPosition(pos);
                int y = editControl.PointYFromPosition(pos);

                Point cursorPoint = editControl.PointToScreen(new Point(x, y));

                Point formLocation = form.Location;
                Rectangle r = new Rectangle(formLocation, form.Size);
                if (r.Contains(cursorPoint))
                {
                    Point newLocation;
                    if (cursorPoint.Y < (Screen.PrimaryScreen.Bounds.Height / 2))
                    {
                        // Top half of the screen
                        newLocation = editControl.PointToClient(
                            new Point(formLocation.X, cursorPoint.Y + editControl.Font.Height)
                            );
                    }
                    else
                    {
                        // Bottom half of the screen
                        newLocation = editControl.PointToClient(
                            new Point(formLocation.X, cursorPoint.Y - form.Height)
                            );
                    }
                    newLocation = editControl.PointToScreen(newLocation);
                    form.Location = newLocation;
                }
            }
        }

        public static class Translations
        {
            public static string Cancel = "Cancel";
            public static string CannotFindTheString0 = "Cannot find the string '{0}'.";
            public static string Close = "Close";
            public static string Direction = "Direction";
            public static string Down = "&Down";
            public static string Find = "Find";
            public static string FindNext = "&Find Next";
            public static string FindWhat = "Find what:";
            public static string MarkAll = "&Mark All";
            public static string MatchCase = "Match &case";
            public static string MatchWholeWordOnly = "Match &whole word only";
            public static string MessageBoxTitle = "SCide";
            public static string NoReplacementsFoundFor0 = "No replacements found for '{0}'.";
            public static string RegularExpression = "Regular &expression";
            public static string Replace = "Replace";
            public static string ReplaceAll = "Replace &All";
            public static string ReplaceInSelection = "Replace in &Selection";
            public static string ReplaceWith = "Re&place with:";
            public static string Replacements = "Replacements:";
            public static string TransformBackslashExpressions = "Transform &backslash expressions";
            public static string Up = "&Up";
            public static string WrapAround = "Wrap ar&ound";
            }
    }
}
