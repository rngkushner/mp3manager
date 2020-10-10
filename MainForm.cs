using MP3Manager.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP3Manager
{
    public partial class MainForm : Form
    {
        private Crawler crawler = new Crawler();       
        private Tagger tagger = null;
        private delegate void SetGridDataDelegate(Dictionary<string, File> completeList);

        public MainForm()
        {
            InitializeComponent();
            InitGrid();
        }

        private void btnCrawl_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            string startingPath = folderBrowserDialog1.SelectedPath;

            if(startingPath != String.Empty)
            {
                crawler.Crawl(startingPath);
            }
            textBoxMessages.Text = $"Found {crawler.GetFiles().Count.ToString()} music files.";
            
        }

        private void btnMP3Tag_Click(object sender, EventArgs e)
        {
            if (crawler != null && crawler.GetFiles() != null)
            {
                var resultFiles = new Dictionary<string, File>();

                tagger = new Tagger(resultFiles);
                tagger.RunTagJob(crawler.GetFiles(), (list) =>
                {
                    SetGridDataDelegate d = new SetGridDataDelegate(SetGridData);
                    this.Invoke(d, new object[] { list });
                });

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

            songGrid.Columns[++i].Name = "Artist";
            songGrid.Columns[i].ReadOnly = true;

            songGrid.Columns[++i].Name = "Album";
            songGrid.Columns[i].ReadOnly = true;

            songGrid.Columns[++i].Name = "Genre";
            songGrid.Columns[i].ReadOnly = true;

            songGrid.Columns[++i].Name = "Count";
            songGrid.Columns[i].ReadOnly = true;

            songGrid.Columns[++i].Name = "FileName";
            songGrid.Columns[i].ReadOnly = true;
        }
        private void SetGridData(Dictionary<string, File> list)
        {
            List<string> dataSource = new List<string>();
            foreach(var key in list.Keys)
            {
                songGrid.Rows.Add(new object[]{
                key,
                list[key].Title,
                list[key].Artist,
                list[key].Album,
                list[key].Genre,
                list[key].MatchCount.ToString(),
                list[key].FileName
                });
            }

            crawler.GetFiles().Clear();
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if(e.ClickedItem.Name == "toolStripMenuItemEdit")
            {
                var modal = new FormUpdate();
                var result = modal.ShowDialog();

                if (result.Equals(DialogResult.OK))
                {
                    var rows = songGrid.SelectedRows;

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
        }

        private void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will apply your changes to your MP3s. Continue?", "Save Changes",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                //do save of beige lines
            }
        }
        
    }
}
