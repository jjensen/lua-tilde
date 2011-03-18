namespace Scintilla.Forms
{
    partial class FindControl
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
            this.buttonMarkAll = new System.Windows.Forms.Button();
            this.checkBoxWrapAround = new System.Windows.Forms.CheckBox();
            this.checkBoxMatchWholeWord = new System.Windows.Forms.CheckBox();
            this.checkBoxTransformEscapeSequences = new System.Windows.Forms.CheckBox();
            this.groupBoxDirection = new System.Windows.Forms.GroupBox();
            this.radioButtonUp = new System.Windows.Forms.RadioButton();
            this.radioButtonDown = new System.Windows.Forms.RadioButton();
            this.groupBoxDirection.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelFindWhat
            // 
            this.labelFindWhat.AutoSize = true;
            this.labelFindWhat.Location = new System.Drawing.Point(3, 13);
            this.labelFindWhat.Name = "labelFindWhat";
            this.labelFindWhat.Size = new System.Drawing.Size(56, 13);
            this.labelFindWhat.TabIndex = 0;
            this.labelFindWhat.Text = SearchHelper.Translations.FindWhat;
            // 
            // comboBoxFindWhat
            // 
            this.comboBoxFindWhat.FormattingEnabled = true;
            this.comboBoxFindWhat.Location = new System.Drawing.Point(65, 10);
            this.comboBoxFindWhat.Name = "comboBoxFindWhat";
            this.comboBoxFindWhat.Size = new System.Drawing.Size(223, 21);
            this.comboBoxFindWhat.TabIndex = 2;
            // 
            // buttonFindNext
            // 
            this.buttonFindNext.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonFindNext.Location = new System.Drawing.Point(307, 8);
            this.buttonFindNext.Name = "buttonFindNext";
            this.buttonFindNext.Size = new System.Drawing.Size(102, 23);
            this.buttonFindNext.TabIndex = 10;
            this.buttonFindNext.Text = SearchHelper.Translations.FindNext;
            this.buttonFindNext.UseVisualStyleBackColor = true;
            this.buttonFindNext.Click += new System.EventHandler(this.buttonFindNext_Click);
            // 
            // checkBoxMatchCase
            // 
            this.checkBoxMatchCase.AutoSize = true;
            this.checkBoxMatchCase.Location = new System.Drawing.Point(6, 65);
            this.checkBoxMatchCase.Name = "checkBoxMatchCase";
            this.checkBoxMatchCase.Size = new System.Drawing.Size(82, 17);
            this.checkBoxMatchCase.TabIndex = 4;
            this.checkBoxMatchCase.Text = SearchHelper.Translations.MatchCase;
            this.checkBoxMatchCase.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseRegularExpression
            // 
            this.checkBoxUseRegularExpression.AutoSize = true;
            this.checkBoxUseRegularExpression.Location = new System.Drawing.Point(6, 88);
            this.checkBoxUseRegularExpression.Name = "checkBoxUseRegularExpression";
            this.checkBoxUseRegularExpression.Size = new System.Drawing.Size(116, 17);
            this.checkBoxUseRegularExpression.TabIndex = 5;
            this.checkBoxUseRegularExpression.Text = SearchHelper.Translations.RegularExpression;
            this.checkBoxUseRegularExpression.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(307, 65);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(102, 23);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = SearchHelper.Translations.Cancel;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonMarkAll
            // 
            this.buttonMarkAll.Location = new System.Drawing.Point(307, 37);
            this.buttonMarkAll.Name = "buttonMarkAll";
            this.buttonMarkAll.Size = new System.Drawing.Size(102, 23);
            this.buttonMarkAll.TabIndex = 11;
            this.buttonMarkAll.Text = SearchHelper.Translations.MarkAll;
            this.buttonMarkAll.UseVisualStyleBackColor = true;
            this.buttonMarkAll.Click += new System.EventHandler(this.buttonMarkAll_Click);
            // 
            // checkBoxWrapAround
            // 
            this.checkBoxWrapAround.AutoSize = true;
            this.checkBoxWrapAround.Checked = true;
            this.checkBoxWrapAround.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWrapAround.Location = new System.Drawing.Point(6, 111);
            this.checkBoxWrapAround.Name = "checkBoxWrapAround";
            this.checkBoxWrapAround.Size = new System.Drawing.Size(88, 17);
            this.checkBoxWrapAround.TabIndex = 6;
            this.checkBoxWrapAround.Text = SearchHelper.Translations.WrapAround;
            this.checkBoxWrapAround.UseVisualStyleBackColor = true;
            // 
            // checkBoxMatchWholeWord
            // 
            this.checkBoxMatchWholeWord.AutoSize = true;
            this.checkBoxMatchWholeWord.Location = new System.Drawing.Point(6, 41);
            this.checkBoxMatchWholeWord.Name = "checkBoxMatchWholeWord";
            this.checkBoxMatchWholeWord.Size = new System.Drawing.Size(135, 17);
            this.checkBoxMatchWholeWord.TabIndex = 3;
            this.checkBoxMatchWholeWord.Text = SearchHelper.Translations.MatchWholeWordOnly;
            this.checkBoxMatchWholeWord.UseVisualStyleBackColor = true;
            // 
            // checkBoxTransformEscapeSequences
            // 
            this.checkBoxTransformEscapeSequences.AutoSize = true;
            this.checkBoxTransformEscapeSequences.Location = new System.Drawing.Point(6, 134);
            this.checkBoxTransformEscapeSequences.Name = "checkBoxTransformEscapeSequences";
            this.checkBoxTransformEscapeSequences.Size = new System.Drawing.Size(182, 17);
            this.checkBoxTransformEscapeSequences.TabIndex = 7;
            this.checkBoxTransformEscapeSequences.Text = SearchHelper.Translations.TransformBackslashExpressions;
            this.checkBoxTransformEscapeSequences.UseVisualStyleBackColor = true;
            // 
            // groupBoxDirection
            // 
            this.groupBoxDirection.Controls.Add(this.radioButtonUp);
            this.groupBoxDirection.Controls.Add(this.radioButtonDown);
            this.groupBoxDirection.Location = new System.Drawing.Point(203, 37);
            this.groupBoxDirection.Name = "groupBoxDirection";
            this.groupBoxDirection.Size = new System.Drawing.Size(85, 68);
            this.groupBoxDirection.TabIndex = 12;
            this.groupBoxDirection.TabStop = false;
            this.groupBoxDirection.Text = SearchHelper.Translations.Direction;
            // 
            // radioButtonUp
            // 
            this.radioButtonUp.AutoSize = true;
            this.radioButtonUp.Location = new System.Drawing.Point(6, 19);
            this.radioButtonUp.Name = "radioButtonUp";
            this.radioButtonUp.Size = new System.Drawing.Size(39, 17);
            this.radioButtonUp.TabIndex = 8;
            this.radioButtonUp.Text = SearchHelper.Translations.Up;
            this.radioButtonUp.UseVisualStyleBackColor = true;
            // 
            // radioButtonDown
            // 
            this.radioButtonDown.AutoSize = true;
            this.radioButtonDown.Checked = true;
            this.radioButtonDown.Location = new System.Drawing.Point(6, 42);
            this.radioButtonDown.Name = "radioButtonDown";
            this.radioButtonDown.Size = new System.Drawing.Size(53, 17);
            this.radioButtonDown.TabIndex = 9;
            this.radioButtonDown.TabStop = true;
            this.radioButtonDown.Text = SearchHelper.Translations.Down;
            this.radioButtonDown.UseVisualStyleBackColor = true;
            // 
            // FindControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxDirection);
            this.Controls.Add(this.checkBoxTransformEscapeSequences);
            this.Controls.Add(this.checkBoxWrapAround);
            this.Controls.Add(this.checkBoxMatchWholeWord);
            this.Controls.Add(this.buttonMarkAll);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.checkBoxUseRegularExpression);
            this.Controls.Add(this.checkBoxMatchCase);
            this.Controls.Add(this.buttonFindNext);
            this.Controls.Add(this.comboBoxFindWhat);
            this.Controls.Add(this.labelFindWhat);
            this.Name = "FindControl";
            this.Size = new System.Drawing.Size(416, 159);
            this.groupBoxDirection.ResumeLayout(false);
            this.groupBoxDirection.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBoxDirection;
        private System.Windows.Forms.RadioButton radioButtonDown;
        private System.Windows.Forms.RadioButton radioButtonUp;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonFindNext;
        private System.Windows.Forms.Button buttonMarkAll;
    }
}
