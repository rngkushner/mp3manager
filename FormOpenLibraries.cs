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
    public partial class FormOpenLibraries : Form
    {
        public string SelectedLib { get; set; }

        public FormOpenLibraries()
        {
            InitializeComponent();
        }

        private void FormOpenLibraries_Load(object sender, EventArgs e)
        {
            listBoxLibraries.Items.Clear();

            foreach (var file in FileUtils.GetFileList(FileType.Library))
            {
                if(file == "lib_default_")
                {
                    listBoxLibraries.Items.Add("Default Library");
                }
                else
                {
                    listBoxLibraries.Items.Add(file.Replace("lib_", ""));
                }                
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (listBoxLibraries.SelectedItem != null)
            {
                SelectedLib = listBoxLibraries.SelectedItem.ToString();
            }
        }       
           
    }
}
