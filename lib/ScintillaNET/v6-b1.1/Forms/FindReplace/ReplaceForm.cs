using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Scintilla;

namespace Scintilla.Forms
{
    public partial class ReplaceForm : Form
    {
        public ReplaceForm()
        {
            InitializeComponent();

            this.CancelButton = this.replaceControl1.ButtonCancelPublic;
            this.AcceptButton = this.replaceControl1.ButtonFindNextPublic;

            this.replaceControl1.Cancel += new CancelEventHandler(replaceControl1_Cancel);

            this.VisibleChanged += new EventHandler(ReplaceForm_VisibleChanged);
        }

        void ReplaceForm_VisibleChanged(object sender, EventArgs e)
        {
            this.replaceControl1.SetDefaultSearchText();
        }

        public void Initialize(ScintillaControl control) 
        {
            this.replaceControl1.Initialize(control);
        }

        void replaceControl1_Cancel(object sender, CancelEventArgs e)
        {
            this.replaceControl1.Reset();
            this.Hide();
        }

        public void ReplaceNext()
        {
            this.replaceControl1.ReplaceNext();
        }
    }
}