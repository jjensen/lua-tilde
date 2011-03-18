using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Scintilla.Forms
{
    public partial class ReplaceControl : UserControl
    {
        private SearchHelper searchHelper;

        public ReplaceControl()
        {
            InitializeComponent();
        }

        public event CancelEventHandler Cancel;

        public void Initialize(ScintillaControl editControl)
        {
            searchHelper = SearchHelper.Instance(editControl);
            searchHelper.Criteria.AddFindHistory += new EventHandler(Criteria_AddFindHistory);
            searchHelper.Criteria.AddReplaceHistory += new EventHandler(Criteria_AddReplaceHistory);
            BindHistory();
        }

        void Criteria_AddFindHistory(object sender, EventArgs args)
        {
            if (comboBoxFindWhat.Items.Contains(sender)) comboBoxFindWhat.Items.Remove(sender);
            comboBoxFindWhat.Items.Insert(0, sender);
        }

        void Criteria_AddReplaceHistory(object sender, EventArgs args)
        {
            if (comboBoxReplaceWith.Items.Contains(sender)) comboBoxReplaceWith.Items.Remove(sender);
            comboBoxReplaceWith.Items.Insert(0, sender);
        }

        private void BindHistory()
        {
            this.comboBoxFindWhat.Items.Clear();
            foreach (string s in searchHelper.Criteria.SearchHistory)
                this.comboBoxFindWhat.Items.Add(s);

            this.comboBoxReplaceWith.Items.Clear();
            foreach (string r in searchHelper.Criteria.ReplaceHistory)
                this.comboBoxReplaceWith.Items.Add(r);
        }

        public void Reset()
        {
            this.comboBoxFindWhat.Focus();
        }

        public void ReplaceNext()
        {
            this.buttonReplace_Click(this, EventArgs.Empty);
        }

        public void SetDefaultSearchText()
        {
            if (searchHelper != null)
            {
                BindHistory();
                if (searchHelper.EditControl.GetSelectedText().Length > 0)
                {
                    searchHelper.Criteria.SearchText = searchHelper.EditControl.GetSelectedText();
                    BindHistory();
                    comboBoxFindWhat.Text = searchHelper.Criteria.SearchText;
                    comboBoxFindWhat.SelectAll();
                }
            }
        }

        public Button ButtonCancelPublic
        {
            get { return this.buttonCancel; }
        }

        public Button ButtonFindNextPublic
        {
            get { return this.buttonFindNext; }
        }

        public Button ButtonReplacePublic
        {
            get { return this.buttonReplace; }
        }

        public Button ButtonReplaceAllPublic
        {
            get { return this.buttonReplaceAll; }
        }

        public Button ButtonReplaceInSelectionPublic
        {
            get { return this.buttonReplaceInSelection; }
        }
        
        protected void OnCancel()
        {
            if (this.Cancel != null)
            {
                this.Cancel(this, new CancelEventArgs(true));
            }
        }

        private void UpdateSearchHelper()
        {
            UpdateSearchHelper(false);
        }

        private void UpdateSearchHelper(bool inSelectionOnly)
        {
            searchHelper.Criteria.IsCaseSensitive = this.checkBoxMatchCase.Checked;
            searchHelper.Criteria.IsRegularExpression = this.checkBoxUseRegularExpression.Checked;
            searchHelper.Criteria.ReplaceInSelectionOnly = inSelectionOnly;
            searchHelper.Criteria.ReplaceText = this.comboBoxReplaceWith.Text;
            searchHelper.Criteria.SearchDown = true;
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
            }
        }

        private void buttonReplace_Click(object sender, EventArgs e)
        {
            if (searchHelper != null)
            {
                UpdateSearchHelper();
                int count = searchHelper.ReplaceNext();
                labelCount.Text = count.ToString();
                if (count == 0)
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
            }
        }

        private void buttonReplaceAll_Click(object sender, EventArgs e)
        {
            if (searchHelper != null)
            {
                UpdateSearchHelper();
                int count = searchHelper.ReplaceAll();
                labelCount.Text = count.ToString();
                if (count == 0)
                {
                    MessageBox.Show(this,
                        string.Format(SearchHelper.Translations.NoReplacementsFoundFor0, searchHelper.Criteria.SearchText),
                        SearchHelper.Translations.MessageBoxTitle,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                BindHistory();
            }
        }

        private void buttonReplaceInSelection_Click(object sender, EventArgs e)
        {
            if (searchHelper != null)
            {
                UpdateSearchHelper(true);

                int count = searchHelper.ReplaceAll();
                labelCount.Text = count.ToString();
                if (count == 0)
                {
                    MessageBox.Show(this,
                        string.Format(SearchHelper.Translations.NoReplacementsFoundFor0, searchHelper.Criteria.SearchText),
                        SearchHelper.Translations.MessageBoxTitle,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                BindHistory();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (searchHelper != null) UpdateSearchHelper();
            this.OnCancel();
        }
    }
}
