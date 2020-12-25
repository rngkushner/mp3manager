using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MP3Manager
{
    public partial class FormGenericInput : Form
    {
        private string instructions;
        private string booleanOption;
        public string ReturnValue { get; set; }
        public bool BooleanResult { get; set; }
        public FormGenericInput(string caption, string instructions, string booleanOption = "") : this()
        {
            Text = caption;
            this.instructions = instructions;
            this.booleanOption = booleanOption;
        }

        public FormGenericInput()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ReturnValue = textBoxInput.Text;            
        }

        private void FormGenericInput_Load(object sender, EventArgs e)
        {
            labelPrompt.Text = instructions;
            if(!string.IsNullOrEmpty(booleanOption))
            {
                checkBoxBooleanOption.Visible = true;
                checkBoxBooleanOption.Text = booleanOption;
            }
        }

        private void checkBoxBooleanOption_CheckedChanged(object sender, EventArgs e)
        {
            BooleanResult = checkBoxBooleanOption.Checked;
        }
    }
}
