using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Scintilla.Forms
{
    public partial class FindControl : UserControl
    {
        private SearchHelper searchHelper;

        public FindControl()
        {
            InitializeComponent();
        }

        public event CancelEventHandler Cancel;

        public void Initialize(ScintillaControl editControl)
        {
            searchHelper = SearchHelper.Instance(editControl);
            searchHelper.Criteria.AddFindHistory += new EventHandler(Criteria_AddFindHistory);
            BindHistory();
        }

        void Criteria_AddFindHistory(object sender, EventArgs args)
        {
            if (comboBoxFindWhat.Items.Contains(sender)) comboBoxFindWhat.Items.Remove(sender);
            comboBoxFindWhat.Items.Insert(0, sender);
            comboBoxFindWhat.SelectAll();
        }

        public void FindNext()
        {
            this.radioButtonDown.Checked = true;
            buttonFindNext_Click(this, EventArgs.Empty);
        }

        public void FindPrevious()
        {
            this.radioButtonUp.Checked = true;
            buttonFindNext_Click(this, EventArgs.Empty);
        }

        public void SetDefaultSearchText()
        {
            if (searchHelper != null)
            {
                if (searchHelper.EditControl.GetSelectedText().Length > 0)
                {
                    searchHelper.Criteria.SearchText = searchHelper.EditControl.GetSelectedText();
                    BindHistory();
                    this.comboBoxFindWhat.Text = searchHelper.Criteria.SearchText;
                    comboBoxFindWhat.SelectAll();
                }
            }
        }

        public void Reset()
        {
            this.comboBoxFindWhat.Focus();
        }

        public Button ButtonCancelPublic
        {
            get { return this.buttonCancel; }
        }

        public Button ButtonFindNextPublic
        {
            get { return this.buttonFindNext; }
        }

        public Button ButtonMarkAllPublic
        {
            get { return this.buttonMarkAll; }
        }
        
        protected void OnCancel()
        {
            if (this.Cancel != null)
            {
                this.Cancel(this, new CancelEventArgs(true));
            }
        }

        private void BindHistory()
        {
            this.comboBoxFindWhat.Items.Clear();
            foreach (string s in searchHelper.Criteria.SearchHistory)
                this.comboBoxFindWhat.Items.Add(s);
        }

        private void UpdateSearchHelper()
        {
            searchHelper.Criteria.IsCaseSensitive = this.checkBoxMatchCase.Checked;
            searchHelper.Criteria.IsRegularExpression = this.checkBoxUseRegularExpression.Checked;
            searchHelper.Criteria.ReplaceInSelectionOnly = false;
            searchHelper.Criteria.SearchDown = this.radioButtonDown.Checked;
            searchHelper.Criteria.SearchText = this.comboBoxFindWhat.Text;
            searchHelper.Criteria.TransformEscapeSequences = this.checkBoxTransformEscapeSequences.Checked;
            searchHelper.Criteria.MatchWholeWordOnly = this.checkBoxMatchWholeWord.Checked;
            searchHelper.Criteria.Wraparound = this.checkBoxWrapAround.Checked;
        }

        private void buttonFindNext_Click(object sender, EventArgs e)
        {
            if (searchHelper != null)
            {
                UpdateSearchHelper();
                if (!searchHelper.FindNextAndHighlight())
                {
                    MessageBox.Show(this,
                        string.Format(SearchHelper.Translations.CannotFindTheString0, searchHelper.Criteria.SearchText),
                        SearchHelper.Translations.MessageBoxTitle,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                else
                {
                    searchHelper.MoveFormAwayFromSelection(ParentForm);
                }
                BindHistory();

                this.comboBoxFindWhat.SelectAll();
            }
        }

        private void buttonMarkAll_Click(object sender, EventArgs e)
        {
            if (searchHelper != null)
            {
                UpdateSearchHelper();
                searchHelper.MarkAll();
            }
            BindHistory();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (searchHelper != null) UpdateSearchHelper();
            this.OnCancel();
        }
    }
}
