using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;

namespace MP3Manager
{
    public partial class FormUpdate : Form
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }

        public bool IgnoreBlanks { get; set; }

        public FormUpdate()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Title = textBoxTitle.Text;
            Artist = textBoxArtist.Text;
            Album = textBoxAlbum.Text;
            Genre = comboBoxGenre.Text;

            IgnoreBlanks = checkBoxIgnoreBlank.Checked;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
           // this.Close();
        }
    }
}
