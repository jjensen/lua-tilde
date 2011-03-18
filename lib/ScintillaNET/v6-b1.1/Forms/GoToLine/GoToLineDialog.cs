using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Scintilla.Forms {
    public partial class GoToLineDialog : Form {
        public static int Show(ScintillaControl oScintillaControl) {
            GoToLineDialog oGoToLineDialog;
            DialogResult oDialogResult;
            int iLine;

            iLine = oScintillaControl.LineFromPosition(oScintillaControl.CurrentPos) + 1;
            oGoToLineDialog = new GoToLineDialog(oScintillaControl.LineCount, iLine);
            oDialogResult = oGoToLineDialog.ShowDialog();            

            if (oDialogResult == DialogResult.OK) {
                iLine = (int) oGoToLineDialog.numericUpDown.Value;
                oScintillaControl.GotoLine(iLine - 1);
            }

            return iLine;
        }

        private GoToLineDialog(int iLineCount, int iCurrentLine) {
            InitializeComponent();

            labelMessage.Text = labelMessage.Text.Replace("max", iLineCount.ToString());

            numericUpDown.Maximum = iLineCount;
            numericUpDown.Value = iCurrentLine;
        }

        private void buttonOk_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}