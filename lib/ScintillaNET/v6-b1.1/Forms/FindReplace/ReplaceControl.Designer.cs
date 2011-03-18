namespace Scintilla.Forms
{
    partial class ReplaceControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelFindWhat = new System.Windows.Forms.Label();
            this.comboBoxFindWhat = new System.Windows.Forms.ComboBox();
            this.buttonFindNext = new System.Windows.Forms.Button();
            this.checkBoxMatchCase = new System.Windows.Forms.CheckBox();
            this.checkBoxUseRegularExpression = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.checkBoxWrapAround = new System.Windows.Forms.CheckBox();
            this.checkBoxMatchWholeWord = new System.Windows.Forms.CheckBox();
            this.checkBoxTransformEscapeSequences = new System.Windows.Forms.CheckBox();
            this.buttonReplaceAll = new System.Windows.Forms.Button();
            this.buttonReplaceInSelection = new System.Windows.Forms.Button();
            this.labelReplaceWith = new System.Windows.Forms.Label();
            this.comboBoxReplaceWith = new System.Windows.Forms.ComboBox();
            this.labelReplacements = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelFindWhat
            // 
            this.labelFindWhat.AutoSize = true;
            this.labelFindWhat.Location = new System.Drawing.Point(8, 12);
            this.labelFindWhat.Name = "labelFindWhat";
            this.labelFindWhat.Size = new System.Drawing.Size(56, 13);
            this.labelFindWhat.TabIndex = 0;
            this.labelFindWhat.Text = SearchHelper.Translations.FindWhat;
            // 
            // comboBoxFindWhat
            // 
            this.comboBoxFindWhat.FormattingEnabled = true;
            this.comboBoxFindWhat.Location = new System.Drawing.Point(96, 8);
            this.comboBoxFindWhat.Name = "comboBoxFindWhat";
            this.comboBoxFindWhat.Size = new System.Drawing.Size(188, 21);
            this.comboBoxFindWhat.TabIndex = 1;
            // 
            // buttonFindNext
            // 
            this.buttonFindNext.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonFindNext.Location = new System.Drawing.Point(292, 8);
            this.buttonFindNext.Name = "buttonFindNext";
            this.buttonFindNext.Size = new System.Drawing.Size(136, 23);
            this.buttonFindNext.TabIndex = 9;
            this.buttonFindNext.Text = SearchHelper.Translations.FindNext;
            this.buttonFindNext.UseVisualStyleBackColor = true;
            this.buttonFindNext.Click += new System.EventHandler(this.buttonFindNext_Click);
            // 
            // checkBoxMatchCase
            // 
            this.checkBoxMatchCase.AutoSize = true;
            this.checkBoxMatchCase.Location = new System.Drawing.Point(8, 88);
            this.checkBoxMatchCase.Name = "checkBoxMatchCase";
            this.checkBoxMatchCase.Size = new System.Drawing.Size(82, 17);
            this.checkBoxMatchCase.TabIndex = 5;
            this.checkBoxMatchCase.Text = SearchHelper.Translations.MatchCase;
            this.checkBoxMatchCase.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseRegularExpression
            // 
            this.checkBoxUseRegularExpression.AutoSize = true;
            this.checkBoxUseRegularExpression.Location = new System.Drawing.Point(8, 111);
            this.checkBoxUseRegularExpression.Name = "checkBoxUseRegularExpression";
            this.checkBoxUseRegularExpression.Size = new System.Drawing.Size(116, 17);
            this.checkBoxUseRegularExpression.TabIndex = 6;
            this.checkBoxUseRegularExpression.Text = SearchHelper.Translations.RegularExpression;
            this.checkBoxUseRegularExpression.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(292, 120);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(136, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = SearchHelper.Translations.Close;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonReplace
            // 
            this.buttonReplace.Location = new System.Drawing.Point(292, 36);
            this.buttonReplace.Name = "buttonReplace";
            this.buttonReplace.Size = new System.Drawing.Size(136, 23);
            this.buttonReplace.TabIndex = 10;
            this.buttonReplace.Text = SearchHelper.Translations.Replace;
            this.buttonReplace.UseVisualStyleBackColor = true;
            this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
            // 
            // checkBoxWrapAround
            // 
            this.checkBoxWrapAround.AutoSize = true;
            this.checkBoxWrapAround.Location = new System.Drawing.Point(8, 134);
            this.checkBoxWrapAround.Name = "checkBoxWrapAround";
            this.checkBoxWrapAround.Size = new System.Drawing.Size(88, 17);
            this.checkBoxWrapAround.TabIndex = 7;
            this.checkBoxWrapAround.Text = SearchHelper.Translations.WrapAround;
            this.checkBoxWrapAround.UseVisualStyleBackColor = true;
            // 
            // checkBoxMatchWholeWord
            // 
            this.checkBoxMatchWholeWord.AutoSize = true;
            this.checkBoxMatchWholeWord.Location = new System.Drawing.Point(8, 64);
            this.checkBoxMatchWholeWord.Name = "checkBoxMatchWholeWord";
            this.checkBoxMatchWholeWord.Size = new System.Drawing.Size(135, 17);
            this.checkBoxMatchWholeWord.TabIndex = 4;
            this.checkBoxMatchWholeWord.Text = SearchHelper.Translations.MatchWholeWordOnly;
            this.checkBoxMatchWholeWord.UseVisualStyleBackColor = true;
            // 
            // checkBoxTransformEscapeSequences
            // 
            this.checkBoxTransformEscapeSequences.AutoSize = true;
            this.checkBoxTransformEscapeSequences.Location = new System.Drawing.Point(8, 157);
            this.checkBoxTransformEscapeSequences.Name = "checkBoxTransformEscapeSequences";
            this.checkBoxTransformEscapeSequences.Size = new System.Drawing.Size(182, 17);
            this.checkBoxTransformEscapeSequences.TabIndex = 8;
            this.checkBoxTransformEscapeSequences.Text = SearchHelper.Translations.TransformBackslashExpressions;
            this.checkBoxTransformEscapeSequences.UseVisualStyleBackColor = true;
            // 
            // buttonReplaceAll
            // 
            this.buttonReplaceAll.Location = new System.Drawing.Point(292, 64);
            this.buttonReplaceAll.Name = "buttonReplaceAll";
            this.buttonReplaceAll.Size = new System.Drawing.Size(136, 23);
            this.buttonReplaceAll.TabIndex = 11;
            this.buttonReplaceAll.Text = SearchHelper.Translations.ReplaceAll;
            this.buttonReplaceAll.UseVisualStyleBackColor = true;
            this.buttonReplaceAll.Click += new System.EventHandler(this.buttonReplaceAll_Click);
            // 
            // buttonReplaceInSelection
            // 
            this.buttonReplaceInSelection.Location = new System.Drawing.Point(292, 92);
            this.buttonReplaceInSelection.Name = "buttonReplaceInSelection";
            this.buttonReplaceInSelection.Size = new System.Drawing.Size(136, 23);
            this.buttonReplaceInSelection.TabIndex = 12;
            this.buttonReplaceInSelection.Text = SearchHelper.Translations.ReplaceInSelection;
            this.buttonReplaceInSelection.UseVisualStyleBackColor = true;
            this.buttonReplaceInSelection.Click += new System.EventHandler(this.buttonReplaceInSelection_Click);
            // 
            // labelReplaceWith
            // 
            this.labelReplaceWith.AutoSize = true;
            this.labelReplaceWith.Location = new System.Drawing.Point(8, 40);
            this.labelReplaceWith.Name = "labelReplaceWith";
            this.labelReplaceWith.Size = new System.Drawing.Size(72, 13);
            this.labelReplaceWith.TabIndex = 2;
            this.labelReplaceWith.Text = SearchHelper.Translations.ReplaceWith;
            // 
            // comboBoxReplaceWith
            // 
            this.comboBoxReplaceWith.FormattingEnabled = true;
            this.comboBoxReplaceWith.Location = new System.Drawing.Point(96, 36);
            this.comboBoxReplaceWith.Name = "comboBoxReplaceWith";
            this.comboBoxReplaceWith.Size = new System.Drawing.Size(188, 21);
            this.comboBoxReplaceWith.TabIndex = 3;
            // 
            // labelReplacements
            // 
            this.labelReplacements.AutoSize = true;
            this.labelReplacements.Location = new System.Drawing.Point(288, 156);
            this.labelReplacements.Name = "labelReplacements";
            this.labelReplacements.Size = new System.Drawing.Size(78, 13);
            this.labelReplacements.TabIndex = 17;
            this.labelReplacements.Text = SearchHelper.Translations.Replacements;
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(376, 156);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(13, 13);
            this.labelCount.TabIndex = 18;
            this.labelCount.Text = "0";
            // 
            // ReplaceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.labelReplacements);
            this.Controls.Add(this.comboBoxReplaceWith);
            this.Controls.Add(this.labelReplaceWith);
            this.Controls.Add(this.buttonReplaceInSelection);
            this.Controls.Add(this.buttonReplaceAll);
            this.Controls.Add(this.checkBoxTransformEscapeSequences);
            this.Controls.Add(this.checkBoxWrapAround);
            this.Controls.Add(this.checkBoxMatchWholeWord);
            this.Controls.Add(this.buttonReplace);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.checkBoxUseRegularExpression);
            this.Controls.Add(this.checkBoxMatchCase);
            this.Controls.Add(this.buttonFindNext);
            this.Controls.Add(this.comboBoxFindWhat);
            this.Controls.Add(this.labelFindWhat);
            this.Name = "ReplaceControl";
            this.Size = new System.Drawing.Size(436, 181);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFindWhat;
        private System.Windows.Forms.ComboBox comboBoxFindWhat;
        private System.Windows.Forms.CheckBox checkBoxMatchCase;
        private System.Windows.Forms.CheckBox checkBoxUseRegularExpression;
        private System.Windows.Forms.CheckBox checkBoxWrapAround;
        private System.Windows.Forms.CheckBox checkBoxMatchWholeWord;
        private System.Windows.Forms.CheckBox checkBoxTransformEscapeSequences;
        private System.Windows.Forms.Label labelReplaceWith;
        private System.Windows.Forms.ComboBox comboBoxReplaceWith;
        private System.Windows.Forms.Label labelReplacements;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonFindNext;
        private System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.Button buttonReplaceAll;
        private System.Windows.Forms.Button buttonReplaceInSelection;
    }
}
