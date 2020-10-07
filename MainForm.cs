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

               
                //songGrid.Columns.Add(new DataGridViewColumn { DataPropertyName = "FileName", CellTemplate = new DataGridViewTextBoxCell() });

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
            songGrid.Columns[++i].Name = "Artist";
            songGrid.Columns[++i].Name = "Album";
            songGrid.Columns[++i].Name = "Genre";
            songGrid.Columns[++i].Name = "Count";
            songGrid.Columns[++i].Name = "FileName";
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
        }
    }
}
