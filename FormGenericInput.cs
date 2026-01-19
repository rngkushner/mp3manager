using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace MP3Manager
{
    public partial class FormGenericInput : Form
    {
        private string instructions;
        private string booleanOption;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ReturnValue { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
                BooleanResult = checkBoxBooleanOption.Checked;
            }
        }

        private void checkBoxBooleanOption_CheckedChanged(object sender, EventArgs e)
        {
            BooleanResult = checkBoxBooleanOption.Checked;
            if(BooleanResult)
            {
                textBoxInput.Enabled = false;
            }
            else
            {
                textBoxInput.Enabled = true;
            }
        }

        private void FormGenericInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(DialogResult == DialogResult.OK && 
                textBoxInput.Text == string.Empty && 
                checkBoxBooleanOption.Checked == false)
            {
                MessageBox.Show("Please either name this library, check default or choose cancel.");
                e.Cancel = true;
            }
           
        }
    }
}
