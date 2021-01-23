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
    public partial class FormAudioUpdate : Form
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }

        public bool IgnoreBlanks { get; set; }

        public FormAudioUpdate()
        {
            InitializeComponent();
        }

        public FormAudioUpdate(string Title, string Artist, string Album, string Genre) : this()
        {
            this.Title = Title;
            this.Artist = Artist;
            this.Album = Album;
            this.Genre = Genre;
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

        private void FormUpdate_Load(object sender, EventArgs e)
        {
            if(Title != null)
            {
                textBoxTitle.Text = Title;
                textBoxArtist.Text = Artist;
                textBoxAlbum.Text = Album;
                comboBoxGenre.Text = Genre;
            }
        }

        private async void buttonGetSpotify_ClickAsync(object sender, EventArgs e)
        {
            var spotify = new JSONClient.JSONClient();

            string result = await spotify.Search(textBoxArtist.Text);

            System.Diagnostics.Trace.Write(result);

        }
    }
}
