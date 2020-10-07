namespace MP3Manager
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnMP3Tag = new System.Windows.Forms.Button();
            this.btnCrawl = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.songGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.songGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMP3Tag
            // 
            this.btnMP3Tag.Location = new System.Drawing.Point(240, 33);
            this.btnMP3Tag.Name = "btnMP3Tag";
            this.btnMP3Tag.Size = new System.Drawing.Size(209, 29);
            this.btnMP3Tag.TabIndex = 0;
            this.btnMP3Tag.Text = "Tag";
            this.btnMP3Tag.UseVisualStyleBackColor = true;
            this.btnMP3Tag.Click += new System.EventHandler(this.btnMP3Tag_Click);
            // 
            // btnCrawl
            // 
            this.btnCrawl.Location = new System.Drawing.Point(12, 33);
            this.btnCrawl.Name = "btnCrawl";
            this.btnCrawl.Size = new System.Drawing.Size(209, 29);
            this.btnCrawl.TabIndex = 1;
            this.btnCrawl.Text = "Crawl Directory";
            this.btnCrawl.UseVisualStyleBackColor = true;
            this.btnCrawl.Click += new System.EventHandler(this.btnCrawl_Click);
            // 
            // songGrid
            // 
            this.songGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.songGrid.Location = new System.Drawing.Point(12, 94);
            this.songGrid.Name = "songGrid";
            this.songGrid.RowHeadersWidth = 51;
            this.songGrid.Size = new System.Drawing.Size(776, 299);
            this.songGrid.TabIndex = 2;
            this.songGrid.Text = "dataGridView1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.songGrid);
            this.Controls.Add(this.btnCrawl);
            this.Controls.Add(this.btnMP3Tag);
            this.Name = "MainForm";
            this.Text = "MP3 Main Panel";
            ((System.ComponentModel.ISupportInitialize)(this.songGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMP3Tag;
        private System.Windows.Forms.Button btnCrawl;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.DataGridView songGrid;
    }
}

