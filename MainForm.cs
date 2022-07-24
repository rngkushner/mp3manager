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

        private void InitGrid()
        {
            GridHelper.SetGridColumns(songGrid);

        }
        private void SetGridData(Dictionary<string, File> list)
        {
            try
            {
                songGrid.Rows.Clear();
                completeList = list;

                //FileUtils.Transform(list);

                foreach (var key in list.Keys)
                {
                    var thisRow = list[key];
                    int rowIdx;
                    rowIdx = songGrid.Rows.Add(
                        GridHelper.FileToGridRow(thisRow, key)
                    );

                    if(thisRow.fixName)
                    {
                        songGrid.Rows[rowIdx].Cells[1].Style.BackColor = Color.AliceBlue;
                    }
                }

                crawler.GetFiles().Clear();                

                WebService.SetRequestHandler(new MP3RequestHandler(completeList));

                textBoxMessages.Text = $"Loaded. {completeList.Count} songs.";
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
                        rows[0].Cells["Genre"].Value?.ToString(),
                        FileUtils.ConvertToBase64(rows[0].Cells[0].Value.ToString())
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
            if (MessageBox.Show("This will apply your changes to your MP3s.\nRemember to update your library too! Continue?", "Save Changes",
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
            GridHelper.SetAudioFile(fileData, row);

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

            if(LoadDefaultLibrary())
            {
                SetGridData(completeList);
            }           

        }

        private bool LoadDefaultLibrary()
        {
            completeList = FileUtils.LoadDefaultLibrary();
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
            else if (e.ClickedItem.Name == "toolStripMenuItemOpenLibrary")
            {

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

        private void toolStripMenuItemOpenLibrary_Click(object sender, EventArgs e)
        {
            FormOpenLibraries form = new FormOpenLibraries();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.SelectedLib != null)
                {
                    completeList = FileUtils.LoadLibrary(form.SelectedLib);
                    SetGridData(completeList);
                }
            }
        }

        private void toolStripMenuItemCrawlLibrary_Click(object sender, EventArgs e)
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

                    if(tagger.Exceptions.Count > 0)
                    {
                        //**gk to do 
                        //show crawl exceptions here.
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
            }
        }

        private void toolStripMenuItemSaveLibrary_Click(object sender, EventArgs e)
        {
            var dlg = new FormGenericInput("Library", "Name your library!", "Make default");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //if default, ignore provide name
                string fileName = dlg.BooleanResult ? "default_" : dlg.ReturnValue;

                FileUtils.SaveCrawl(fileName, completeList);
            }

            dlg.Close();
        }

        private void toolStripMenuItemNameCleanup_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in songGrid.Rows)
            {
                if(row.Cells[1].Style.BackColor == Color.AliceBlue)
                {
                    row.Cells[1].Value = row.Cells[1].Value.ToString().Substring(row.Cells[1].Value.ToString().IndexOf(' ') +1).Replace(".mp3", string.Empty);
                    row.Cells[1].Style.BackColor = Color.Empty;
                    row.DefaultCellStyle.BackColor = Color.Beige;
                }
            }
        }
    }
}
