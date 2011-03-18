using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Scintilla.Forms
{
    public class SearchCriteria
    {
        private bool isCaseSensitive; //false
        private bool isRegularExpression; //false
        private bool replaceInSelectionOnly; //false
        private bool transformEscapeSequences; //false
        private bool matchWholeWordOnly; //false
        private bool grabFocusAfterSearch; //false
        private bool moveFormAwayFromSelection = true;
        private bool searchDown = true;
        private bool wraparound = true;
        private string searchText = string.Empty;
        private string replaceText = string.Empty;
        private string searchTextTransformed;
        private string replaceTextTransformed;
        private List<string> searchHistory = new List<string>();
        private List<string> replaceHistory = new List<string>();

        public event EventHandler AddFindHistory;
        public event EventHandler AddReplaceHistory;

        internal SearchCriteria() { }

        protected void OnAddFindHistory(string searchText)
        {
            if (this.AddFindHistory != null)
            {
                if (searchHistory.Contains(searchText)) searchHistory.Remove(searchText);
                searchHistory.Insert(0, searchText);
                this.AddFindHistory(searchText, EventArgs.Empty);
            }
        }

        protected void OnAddReplaceHistory(string replaceText)
        {
            if (this.AddReplaceHistory != null)
            {
                if (replaceHistory.Contains(replaceText)) replaceHistory.Remove(replaceText);
                replaceHistory.Insert(0, replaceText);

                this.AddReplaceHistory(replaceText, EventArgs.Empty);
            }
        }

        public bool MatchWholeWordOnly
        {
            get { return matchWholeWordOnly; }
            set { matchWholeWordOnly = value; }
        }

        public bool Wraparound
        {
            get { return wraparound; }
            set { wraparound = value; }
        }

        public bool SearchDown
        {
            get { return searchDown; }
            set { searchDown = value; }
        }

        public bool IsCaseSensitive
        {
            get { return isCaseSensitive; }
            set { isCaseSensitive = value; }
        }

        public bool IsRegularExpression
        {
            get { return isRegularExpression; }
            set { isRegularExpression = value; }
        }

        public bool TransformEscapeSequences
        {
            get { return transformEscapeSequences; }
            set { transformEscapeSequences = value; }
        }

        public bool ReplaceInSelectionOnly
        {
            get { return replaceInSelectionOnly; }
            set 
            {
                if (replaceInSelectionOnly != value)
                {
                    replaceInSelectionOnly = value;
                    searchTextTransformed = null;
                    replaceTextTransformed = null;
                }
            }
        }

        public bool GrabFocusAfterSearch
        {
            get { return grabFocusAfterSearch; }
            set { grabFocusAfterSearch = value; }
        }

        public bool MoveFormAwayFromSelection
        {
            get { return moveFormAwayFromSelection; }
            set { moveFormAwayFromSelection = value; }
        }

        public string SearchText
        {
            get { return searchText; }
            set 
            {
                if (!String.IsNullOrEmpty(searchText))
                {
                    if (searchHistory.Contains(value)) searchHistory.Remove(value);
                    searchHistory.Insert(0, value);
                    OnAddFindHistory(value);
                }

                searchTextTransformed = null;
                searchText = value; 
            }
        }

        public string ReplaceText
        {
            get { return replaceText; }
            set 
            {
                if (!String.IsNullOrEmpty(replaceText))
                {
                    OnAddReplaceHistory(value);
                }

                replaceTextTransformed = null;
                replaceText = value; 
            }
        }

        public ReadOnlyCollection<string> SearchHistory
        {
            get { return searchHistory.AsReadOnly(); }
        }

        public ReadOnlyCollection<string> ReplaceHistory
        {
            get { return replaceHistory.AsReadOnly(); }
        }

        public string SearchTextTransformed
        {
            get
            {
                if (searchTextTransformed == null)
                {
                    if (transformEscapeSequences)
                        searchTextTransformed = searchText.Replace("\\t", "\t").Replace("\\r", "\r").Replace("\\n", "\n").Replace("\\v", "\v").Replace("\\f", "\f").Replace("\\'", "\'").Replace("\\\\", "\\");
                    else
                        searchTextTransformed = searchText;
                }

                return searchTextTransformed;
            }
        }

        public string ReplaceTextTransformed
        {
            get
            {
                if (replaceTextTransformed == null)
                {
                    if (transformEscapeSequences)
                        replaceTextTransformed = replaceText.Replace("\\t", "\t").Replace("\\r", "\r").Replace("\\n", "\n").Replace("\\v", "\v").Replace("\\f", "\f").Replace("\\'", "\'").Replace("\\\\", "\\");
                    else
                        replaceTextTransformed = replaceText;
                }

                return replaceTextTransformed;
            }
        }

    }
}
