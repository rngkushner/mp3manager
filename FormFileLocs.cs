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
                bool showDelete = (file.Paths.Count > 1);

                Image deleteImage = (Image) Properties.Resources.ResourceManager.GetObject("deletebutton");   

                foreach (string path in file.Paths)
                {                    
                    TextBox textBox = new TextBox();
                    textBox.Name = $"path{counter}";
                    textBox.Text = path;
                    textBox.Name = "txtBox" + counter.ToString();
                    textBox.Location = new Point(50, 55 + 50 * counter);
                    textBox.Size = new Size(650, 27);
                    textBox.ReadOnly = true;

                    Controls.Add(textBox);

                    if (showDelete)
                    {
                        Button button = new Button();
                        button.Name = counter.ToString();
                        button.BackgroundImage = deleteImage;
                        button.BackgroundImageLayout = ImageLayout.Stretch;
                        button.Size = new Size(27, 27);
                        button.Location = new Point(710, 55 + 50 * counter);
                        button.Click += click;

                        Controls.Add(button);
                    }
                    counter++;
                }
                
            }
        }

        private void click(object sender, EventArgs e)
        {
            TextBox textBoxSelected = Controls["txtBox" + (sender as Button).Name] as TextBox;

            if(MessageBox.Show("Are you sure you want to delete " +textBoxSelected.Text, 
                "Delete Duplicate", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                System.IO.File.Delete(textBoxSelected.Text);

                for(var i = 0; i < file.Paths.Count; i++)
                {
                    if(file.Paths[i] == textBoxSelected.Text)
                    {
                        file.Paths.RemoveAt(i);
                        textBoxSelected.BackColor = Color.DarkGray;
                        (sender as Button).Visible = false;
                        break;
                    }
                }
            }            
        }
    }
}
