using MP3Manager.Files;
using MP3Manager.WebServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text.Json;

namespace MP3Manager
{    

    public partial class MainForm : Form
    {
        private readonly Crawler crawler = new Crawler();       
        private Tagger tagger = null;
        private delegate void SetGridDataDelegate(Dictionary<string, File> completeList);
        private delegate void WebServerErrorDelegate();

        private Dictionary<string, File> completeList = null;        

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnCrawl_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            string startingPath = folderBrowserDialog1.SelectedPath;

            try
            {
                if (startingPath != String.Empty)
                {
                    crawler.Crawl(startingPath);
                }
                textBoxMessages.Text = $"Found {crawler.GetFiles().Count} music files.";

                if (crawler.GetFiles() != null)
                {
                    textBoxMessages.Text = "Tagging all the files. Hang on.";

                    var resultFiles = new Dictionary<string, File>();

                    tagger = new Tagger(resultFiles);
                    tagger.RunTagJob(crawler.GetFiles(), (list) =>
                    {
                        SetGridDataDelegate d = new SetGridDataDelegate(SetGridData);
                        this.Invoke(d, new object[] { list });
                    });
                    buttonSaveAsPlaylist.Visible = true;
                }
            }
            catch(Exception ex)
            {
                ErrorLogger.LogError(ex);
            }

        }

        private void InitGrid()
        {
            songGrid.Columns.AddRange(new DataGridViewColumn[7]{
                new DataGridViewColumn(new DataGridViewTextBoxCell()),
                new DataGridViewColumn(new DataGridViewTextBoxCell()),
                new DataGridViewColumn(new DataGridViewTextBoxCell()),
                new DataGridViewColumn(new DataGridViewTextBoxCell()),
                new DataGridViewColumn(new DataGridViewTextBoxCell()),
                new DataGridViewColumn(new DataGridViewTextBoxCell()),
                new DataGridViewColumn(new DataGridViewTextBoxCell())
            });

            int i = 0;
            songGrid.Columns[i].Name = "Key";
            songGrid.Columns[i].Visible = false;

            songGrid.Columns[++i].Name = "Title";
            songGrid.Columns[i].ReadOnly = true;
            songGrid.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;

            songGrid.Columns[++i].Name = "Artist";
            songGrid.Columns[i].ReadOnly = true;
            songGrid.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;

            songGrid.Columns[++i].Name = "Album";
            songGrid.Columns[i].ReadOnly = true;
            songGrid.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;

            songGrid.Columns[++i].Name = "Genre";
            songGrid.Columns[i].ReadOnly = true;
            songGrid.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;

            songGrid.Columns[++i].Name = "Count";
            songGrid.Columns[i].ReadOnly = true;
            songGrid.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;

            songGrid.Columns[++i].Name = "FileName";
            songGrid.Columns[i].ReadOnly = true;
        }
        private void SetGridData(Dictionary<string, File> list)
        {
            try
            {
                songGrid.Rows.Clear();

                FileUtils.Transform(list);

                foreach (var key in list.Keys)
                {
                    songGrid.Rows.Add(new object[]{
                    key,
                    list[key].Title,
                    list[key].Artist,
                    list[key].Album,
                    list[key].Genre,
                    list[key].MatchCount.ToString(),
                    list[key].FileName + "|" + list[key].SoundexTag
                    });
                }

                crawler.GetFiles().Clear();
                completeList = list;

                WebService.SetRequestHandler(new MP3RequestHandler(completeList));

                textBoxMessages.Text = $"All done. {completeList.Count} songs.";
                checkBoxRunWeb.Enabled = true;                

            }
            catch(Exception ex)
            {
                ErrorLogger.LogError(ex);
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if(e.ClickedItem.Name == "toolStripMenuItemEdit")
            {

                //new FormUpdate();
                var rows = songGrid.SelectedRows;

                FormUpdate modal;
                if (rows.Count == 1)
                {
                    modal = new FormUpdate(
                        rows[0].Cells["Title"].Value?.ToString(),
                        rows[0].Cells["Artist"].Value?.ToString(),
                        rows[0].Cells["Album"].Value?.ToString(),
                        rows[0].Cells["Genre"].Value?.ToString()
                    );
                }
                else
                {
                    modal = new FormUpdate();
                }

                DialogResult result;

                result = modal.ShowDialog();

                if (result.Equals(DialogResult.OK))
                {                    

                    foreach(DataGridViewRow row in rows)
                    {
                        bool isDirty = false;

                        if ((modal.Title == "" && !modal.IgnoreBlanks) || !String.IsNullOrEmpty(modal.Title))
                        {
                            row.Cells["Title"].Value = modal.Title;
                            isDirty = true;
                        }
                        if ((modal.Artist == "" && !modal.IgnoreBlanks) || !String.IsNullOrEmpty(modal.Artist))
                        {
                            row.Cells["Artist"].Value = modal.Artist;
                            isDirty = true;
                        }
                        if ((modal.Album == "" && !modal.IgnoreBlanks) || !String.IsNullOrEmpty(modal.Album))
                        {
                            row.Cells["Album"].Value = modal.Album;
                            isDirty = true;
                        }
                        if ((modal.Genre == "" && !modal.IgnoreBlanks) || !String.IsNullOrEmpty(modal.Genre))
                        {
                            row.Cells["Genre"].Value = modal.Genre;
                            isDirty = true;
                        }

                        if(isDirty)
                        {
                            //mark for save
                            row.DefaultCellStyle.BackColor = Color.Beige;
                        }

                    }
                }
            }
            else if (e.ClickedItem.Name == "toolStripMenuItemFileLocs")
            {
                var rows = songGrid.SelectedRows;
                if (completeList != null && rows.Count > 0)
                {
                    FormFileLocs fileModal = new FormFileLocs(completeList[rows[0].Cells["Key"].Value.ToString()]);
                    if(fileModal.ShowDialog() == DialogResult.OK)
                    {
                        //reset grid with new info
                        SetGridData(completeList);
                    }
                }               
            }
        }

        private void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will apply your changes to your MP3s. Continue?", "Save Changes",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if(completeList != null)
                {
                    try
                    {
                        //do save of beige lines
                        foreach (DataGridViewRow row in songGrid.Rows)
                        {
                            if (row.DefaultCellStyle.BackColor == Color.Beige)
                            {
                                var file = completeList[row.Cells["key"].Value.ToString()];
                                WriteMP3PropertiesToFile(file, row);
                                row.DefaultCellStyle.BackColor = Color.Empty;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex);
                    }

                }
            }
        }


        private void WriteMP3PropertiesToFile(File fileData, DataGridViewRow row)
        {
            fileData.Album = row.Cells["Album"].Value == null ? null : row.Cells["Album"].Value.ToString(); 
            fileData.Artist = row.Cells["Artist"].Value == null ? null : row.Cells["Artist"].Value.ToString();
            fileData.Title = row.Cells["Title"].Value == null ? null : row.Cells["Title"].Value.ToString();
            fileData.Genre = row.Cells["Genre"].Value == null ? null : row.Cells["Genre"].Value.ToString();
                            
            var mp3File = TagLib.File.Create(fileData.Paths[0]);

            mp3File.Tag.Album = fileData.Album;

            mp3File.Tag.Performers = new[] { fileData.Artist ?? String.Empty };
            mp3File.Tag.AlbumArtists = mp3File.Tag.Performers;
            mp3File.Tag.Title = fileData.Title;
            mp3File.Tag.Genres = new[] { fileData.Genre ?? String.Empty };

            mp3File.Save();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ErrorLogger.ErrorHappenedEvent += ErrorLogger_ErrorHappenedEvent;
            InitGrid();

            if(LoadDefaultPlaylist())
            {
                SetGridData(completeList);
            }           

        }

        private bool LoadDefaultPlaylist()
        {
            completeList = FileUtils.LoadDefaultPlaylist();
            return (completeList != null);
        }

        private void ErrorLogger_ErrorHappenedEvent(object sender, EventArgs e)
        {
            WebServerErrorDelegate d= new WebServerErrorDelegate(WebServerError);
            buttonErrors.Invoke(d);
            
        }

        private void WebServerError()
        {
            buttonErrors.Visible = true;
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            WebService.ShutDown();
            Close();
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "toolStripMenuItemAbout")
            {
                FormAbout form = new FormAbout();
                form.ShowDialog();
            }
        }

        private void checkBoxRunWeb_CheckedChanged(object sender, EventArgs e)
        {
            if(((CheckBox) sender).Checked == true)
            {
                WebService.StartWebHost(new MP3RequestHandler(completeList));
            }
            else
            {
                WebService.ShutDown();
            }
        }

        private void buttonErrors_Click(object sender, EventArgs e)
        {
            var errorForm = new FormErrors();
            errorForm.ShowDialog();
            buttonErrors.Visible = false;
        }

        private void buttonSaveAsPlaylist_Click(object sender, EventArgs e)
        {
            var dlg = new FormGenericInput("Library", "Name your library!", "Make default");
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                //if default, ignore provide name
                string fileName = dlg.BooleanResult ? "default_" : "" + dlg.ReturnValue;

                FileUtils.SaveCrawl(fileName, completeList);
                buttonSaveAsPlaylist.Visible = false;
            }
            
            dlg.Close();            

        }
    }
}
