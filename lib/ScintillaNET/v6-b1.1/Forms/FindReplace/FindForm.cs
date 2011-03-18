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
    public partial class FindForm : Form
    {
        public FindForm()
        {
            InitializeComponent();

            this.CancelButton = this.findControl1.ButtonCancelPublic;
            this.AcceptButton = this.findControl1.ButtonFindNextPublic;
        
            this.findControl1.Cancel += new CancelEventHandler(findControl1_Cancel);

            this.VisibleChanged += new EventHandler(FindForm_VisibleChanged);
        }

        void FindForm_VisibleChanged(object sender, EventArgs e)
        {
            this.findControl1.SetDefaultSearchText();
        }

        public void Initialize(ScintillaControl control) 
        {
            this.findControl1.Initialize(control);
        }

        void findControl1_Cancel(object sender, CancelEventArgs e)
        {
            this.findControl1.Reset();
            this.Hide();
        }

        public void FindNext()
        {
            this.findControl1.FindNext();
        }

        public void FindPrevious()
        {
            this.findControl1.FindPrevious();
        }
    }
}