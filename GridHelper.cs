using MP3Manager.Files;
using System;
using System.Windows.Forms;

namespace MP3Manager
{
    public static class GridHelper
    {
        public static void SetGridColumns(DataGridView mainGrid)
        {

            var columns = new DataGridViewColumn[7];

            columns[0] = new DataGridViewColumn { Name = "Key", Visible = false, CellTemplate = new DataGridViewTextBoxCell() };

            var arr = new string[] { "Title", "Artist", "Album", "Genre", "Count" };

            int i = 0;
            foreach (var name in arr)
            {
                columns[++i] = new DataGridViewColumn
                {
                    Name = name,
                    ReadOnly = true,
                    SortMode = DataGridViewColumnSortMode.Automatic,
                    CellTemplate = new DataGridViewTextBoxCell()
                };
            }

            columns[++i] = new DataGridViewColumn
            {
                Name = "FileName",
                ReadOnly = true,
                CellTemplate = new DataGridViewTextBoxCell()
            };

            mainGrid.Columns.AddRange(columns);


        }

        public static object[] FileToGridRow(File file, String key)
        {
            return new object[]{
                key,
                file.Title,
                file.Artist,
                file.Album,
                file.Genre,
                file.MatchCount.ToString(),
                file.FileName + "|" + file.SoundexTag
            };
        }

        public static void SetAudioFile(File fileData, DataGridViewRow row)
        {
            fileData.Album = row.Cells["Album"].Value == null ? null : row.Cells["Album"].Value.ToString();
            fileData.Artist = row.Cells["Artist"].Value == null ? null : row.Cells["Artist"].Value.ToString();
            fileData.Title = row.Cells["Title"].Value == null ? null : row.Cells["Title"].Value.ToString();
            fileData.Genre = row.Cells["Genre"].Value == null ? null : row.Cells["Genre"].Value.ToString();
        }
    }
}
