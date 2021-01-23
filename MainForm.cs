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
        
        private bool musicMode = true;

        private Dictionary<string, File> completeList = null;        

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnCrawl_Click(object sender, EventArgs e)
        {
            try
            {
                if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    string startingPath = folderBrowserDialog1.SelectedPath;

                    if (startingPath != String.Empty)
                    {
                        crawler.Crawl(startingPath, musicMode ? MediaType.Audio : MediaType.Video);
                    }
                    textBoxMessages.Text = $"Found {crawler.GetFiles().Count} files.";

                    if (crawler.GetFiles() != null)
                    {
                        textBoxMessages.Text = "Tagging all the files. Hang on.";

                        var resultFiles = new Dictionary<string, File>();

                        tagger = new Tagger(resultFiles, musicMode);
                        tagger.RunTagJob(crawler.GetFiles(), (list) =>
                        {
                            SetGridDataDelegate d = new SetGridDataDelegate(SetGridData);
                            this.Invoke(d, new object[] { list });
                        });
                        buttonSaveAsPlaylist.Visible = true;
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorLogger.LogError(ex);
            }
        }

        private void InitGrid()
        {
            GridHelper.SetGridColumns(mainGrid, musicMode);  
        }
        private void SetGridData(Dictionary<string, File> list)
        {
            try
            {
                mainGrid.Rows.Clear();

                FileUtils.Transform(list);

                foreach (var key in list.Keys)
                {
                    if (musicMode)
                    { 
                        mainGrid.Rows.Add(GridHelper.FileToGridRow((AudioFile)list[key], key));
                    }
                    else
                    {
                        mainGrid.Rows.Add(GridHelper.FileToGridRow((VideoFile)list[key], key));
                    }
                    
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
                var rows = mainGrid.SelectedRows;

                FormAudioUpdate modal;
                if (rows.Count == 1)
                {
                    modal = new FormAudioUpdate(
                        rows[0].Cells["Title"].Value?.ToString(),
                        rows[0].Cells["Artist"].Value?.ToString(),
                        rows[0].Cells["Album"].Value?.ToString(),
                        rows[0].Cells["Genre"].Value?.ToString()
                    );
                }
                else
                {
                    modal = new FormAudioUpdate();
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
                var rows = mainGrid.SelectedRows;
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
            if (MessageBox.Show("This will apply your changes to the files. Continue?", "Save Changes",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if(completeList != null)
                {
                    try
                    {
                        //do save of beige lines
                        foreach (DataGridViewRow row in mainGrid.Rows)
                        {
                            if (row.DefaultCellStyle.BackColor == Color.Beige)
                            {
                                if (musicMode)
                                {
                                    var file = completeList[row.Cells["key"].Value.ToString()] as AudioFile;
                                    WriteMusicPropertiesToFile(file, row);
                                }
                                else
                                {
                                }
                                 
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


        private void WriteMusicPropertiesToFile(AudioFile fileData, DataGridViewRow row)
        {
            GridHelper.SetAudioFile(fileData, row);
                            
            var mediaFile = TagLib.File.Create(fileData.Paths[0]);

            mediaFile.Tag.Album = fileData.Album;

            mediaFile.Tag.Performers = new[] { fileData.Artist ?? String.Empty };
            mediaFile.Tag.AlbumArtists = mediaFile.Tag.Performers;
            mediaFile.Tag.Title = fileData.Title;
            mediaFile.Tag.Genres = new[] { fileData.Genre ?? String.Empty };

            mediaFile.Save();
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
            completeList = FileUtils.LoadDefaultPlaylist(musicMode);
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
                string fileName = dlg.BooleanResult ? "default_" : dlg.ReturnValue;

                FileUtils.SaveCrawl(fileName, completeList);
                buttonSaveAsPlaylist.Visible = false;
            }
            
            dlg.Close();            

        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            var button = (RadioButton)sender;
            if(button.Checked)
            {
                if (button.Name == "radioButtonAudio")
                {
                    btnCrawl.Text = "Crawl &Music Directories";
                    checkBoxRunWeb.Text = "Launch Music Web";
                    musicMode = true;
                }
                else
                {
                    btnCrawl.Text = "Crawl &Video Directories";
                    checkBoxRunWeb.Text = "Launch Video Web";
                    musicMode = false;
                }
            }

        }
    }
}
