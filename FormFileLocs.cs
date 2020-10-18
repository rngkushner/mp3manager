using MP3Manager.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MP3Manager
{
    public partial class FormFileLocs : Form
    {
        private File file;
        private bool selectMode;
        public FormFileLocs(File file, bool selectMode = false)
        {
            this.file = file;
            this.selectMode = selectMode;

            InitializeComponent();
        }

        private void FormFileLocs_Load(object sender, EventArgs e)
        {
            if(file != null)
            {
                int counter = 0;
                foreach(string path in file.Paths)
                {                    
                    TextBox textBox = new TextBox();
                    textBox.Text = path;
                    textBox.Name = "txtBox" + counter.ToString();
                    textBox.Location = new Point(50, 55 + 95 * counter);
                    textBox.Size = new Size(650, 95);
                    textBox.ReadOnly = true;

                    Controls.Add(textBox);

                    counter++;
                }
                
            }
        }
    }
}
