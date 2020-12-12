using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MP3Manager
{
    public partial class FormErrors : Form
    {
        public FormErrors()
        {
            InitializeComponent();
        }

        private void FormErrors_Shown(object sender, EventArgs e)
        {
            if (ErrorLogger.HasErrors())
            {
                textBoxErrors.Text = ErrorLogger.GetErrorLog();
                ErrorLogger.ClearErrors();
            }
        }
    }
}
