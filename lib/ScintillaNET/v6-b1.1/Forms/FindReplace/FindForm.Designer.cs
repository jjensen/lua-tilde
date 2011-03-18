namespace Scintilla.Forms
{
    partial class FindForm
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
            this.findControl1 = new Scintilla.Forms.FindControl();
            this.SuspendLayout();
            // 
            // findControl1
            // 
            this.findControl1.Location = new System.Drawing.Point(0, 0);
            this.findControl1.Name = "findControl1";
            this.findControl1.Size = new System.Drawing.Size(416, 159);
            this.findControl1.TabIndex = 0;
            // 
            // FindForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 157);
            this.ControlBox = false;
            this.Controls.Add(this.findControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(421, 189);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(421, 189);
            this.Name = "FindForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = SearchHelper.Translations.Find;
            this.ResumeLayout(false);

        }

        #endregion

        private Scintilla.Forms.FindControl findControl1;
    }
}