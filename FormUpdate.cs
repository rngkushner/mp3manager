using MP3Manager.Files;
using System;
using System.Windows.Forms;

namespace MP3Manager
{
    public partial class FormUpdate : Form
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public Int32 Track { get; set; }
        public bool IgnoreBlanks { get; set; }

        private string FileKey;

        public FormUpdate()
        {
            InitializeComponent();
            buttonGetSpotify.Visible = false;
        }

        public FormUpdate(string Title, string Artist, string Album, string Genre, string FileKey) : this()
        {
            buttonGetSpotify.Visible = true;

            this.Title = Title;
            this.Artist = Artist;
            this.Album = Album;
            this.Genre = Genre;
            this.FileKey = FileKey;
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

        private async void buttonGetSpotify_ClickAsync(object sender, EventArgs e)
        {
            var spotify = new JSONClient.JSONClient();

            string result = await spotify.Search(textBoxArtist.Text);

            FileUtils.SaveSpotifyMeta(textBoxArtist.Text, result);

            MessageBox.Show("Done!");

        }
    }
}
