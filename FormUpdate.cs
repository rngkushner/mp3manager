using MP3Manager.Files;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MP3Manager
{
    public partial class FormUpdate : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Title { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Artist { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Album { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Genre { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Track { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IgnoreBlanks { get; set; }

        public FormUpdate()
        {
            InitializeComponent();
            buttonGetSpotify.Visible = false;
        }

        public FormUpdate(string Title, string Artist, string Album, string Genre) : this()
        {
            buttonGetSpotify.Visible = true;

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
            Track = decimal.ToInt32(textBoxTrackNumber.Value);        

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
                textBoxTrackNumber.Value = Track;
            }
        }
    }
}
