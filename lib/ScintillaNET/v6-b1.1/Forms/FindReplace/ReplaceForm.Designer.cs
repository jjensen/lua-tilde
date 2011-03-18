namespace Scintilla.Forms
{
    partial class ReplaceForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.replaceControl1 = new Scintilla.Forms.ReplaceControl();
            this.SuspendLayout();
            // 
            // replaceControl1
            // 
            this.replaceControl1.Location = new System.Drawing.Point(0, 0);
            this.replaceControl1.Name = "replaceControl1";
            this.replaceControl1.Size = new System.Drawing.Size(436, 181);
            this.replaceControl1.TabIndex = 0;
            // 
            // ReplaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 178);
            this.ControlBox = false;
            this.Controls.Add(this.replaceControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(440, 210);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(440, 210);
            this.Name = "ReplaceForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = SearchHelper.Translations.Replace;
            this.ResumeLayout(false);

        }

        #endregion

        private Scintilla.Forms.ReplaceControl replaceControl1;

    }
}