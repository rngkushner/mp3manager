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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnCrawl = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.mainGrid = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFileLocs = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonSaveChanges = new System.Windows.Forms.Button();
            this.buttonDone = new System.Windows.Forms.Button();
            this.textBoxMessages = new System.Windows.Forms.TextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxRunWeb = new System.Windows.Forms.CheckBox();
            this.buttonErrors = new System.Windows.Forms.Button();
            this.buttonSaveAsPlaylist = new System.Windows.Forms.Button();
            this.radioButtonMusic = new System.Windows.Forms.RadioButton();
            this.radioButtonVideo = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.mainGrid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCrawl
            // 
            this.btnCrawl.Location = new System.Drawing.Point(12, 36);
            this.btnCrawl.Name = "btnCrawl";
            this.btnCrawl.Size = new System.Drawing.Size(182, 29);
            this.btnCrawl.TabIndex = 0;
            this.btnCrawl.Text = "Crawl &Music Directories";
            this.btnCrawl.UseVisualStyleBackColor = true;
            this.btnCrawl.Click += new System.EventHandler(this.btnCrawl_Click);
            // 
            // mainGrid
            // 
            this.mainGrid.AllowUserToAddRows = false;
            this.mainGrid.AllowUserToDeleteRows = false;
            this.mainGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.mainGrid.Location = new System.Drawing.Point(12, 94);
            this.mainGrid.Name = "mainGrid";
            this.mainGrid.RowHeadersWidth = 51;
            this.mainGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mainGrid.Size = new System.Drawing.Size(776, 299);
            this.mainGrid.TabIndex = 3;
            this.mainGrid.Text = "dataGridView1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemEdit,
            this.toolStripMenuItemFileLocs});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(202, 52);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // toolStripMenuItemEdit
            // 
            this.toolStripMenuItemEdit.Name = "toolStripMenuItemEdit";
            this.toolStripMenuItemEdit.Size = new System.Drawing.Size(201, 24);
            this.toolStripMenuItemEdit.Text = "Edit";
            // 
            // toolStripMenuItemFileLocs
            // 
            this.toolStripMenuItemFileLocs.Name = "toolStripMenuItemFileLocs";
            this.toolStripMenuItemFileLocs.Size = new System.Drawing.Size(201, 24);
            this.toolStripMenuItemFileLocs.Text = "See file location(s)";
            // 
            // buttonSaveChanges
            // 
            this.buttonSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveChanges.Location = new System.Drawing.Point(478, 411);
            this.buttonSaveChanges.Name = "buttonSaveChanges";
            this.buttonSaveChanges.Size = new System.Drawing.Size(145, 29);
            this.buttonSaveChanges.TabIndex = 4;
            this.buttonSaveChanges.Text = "&Commit Changes";
            this.buttonSaveChanges.UseVisualStyleBackColor = true;
            this.buttonSaveChanges.Click += new System.EventHandler(this.buttonSaveChanges_Click);
            // 
            // buttonDone
            // 
            this.buttonDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDone.Location = new System.Drawing.Point(643, 409);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(145, 29);
            this.buttonDone.TabIndex = 5;
            this.buttonDone.Text = "I\'m &Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // textBoxMessages
            // 
            this.textBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxMessages.Location = new System.Drawing.Point(12, 411);
            this.textBoxMessages.Name = "textBoxMessages";
            this.textBoxMessages.Size = new System.Drawing.Size(405, 27);
            this.textBoxMessages.TabIndex = 3;
            this.textBoxMessages.TabStop = false;
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAbout});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 28);
            this.menuStrip.TabIndex = 6;
            this.menuStrip.Text = "menuStrip";
            this.menuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip_ItemClicked);
            // 
            // toolStripMenuItemAbout
            // 
            this.toolStripMenuItemAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
            this.toolStripMenuItemAbout.Size = new System.Drawing.Size(64, 24);
            this.toolStripMenuItemAbout.Text = "&About";
            // 
            // checkBoxRunWeb
            // 
            this.checkBoxRunWeb.AutoSize = true;
            this.checkBoxRunWeb.Enabled = false;
            this.checkBoxRunWeb.Location = new System.Drawing.Point(411, 39);
            this.checkBoxRunWeb.Name = "checkBoxRunWeb";
            this.checkBoxRunWeb.Size = new System.Drawing.Size(153, 24);
            this.checkBoxRunWeb.TabIndex = 2;
            this.checkBoxRunWeb.Text = "Launch Music Web";
            this.checkBoxRunWeb.UseVisualStyleBackColor = true;
            this.checkBoxRunWeb.CheckedChanged += new System.EventHandler(this.checkBoxRunWeb_CheckedChanged);
            // 
            // buttonErrors
            // 
            this.buttonErrors.Location = new System.Drawing.Point(575, 36);
            this.buttonErrors.Name = "buttonErrors";
            this.buttonErrors.Size = new System.Drawing.Size(213, 29);
            this.buttonErrors.TabIndex = 7;
            this.buttonErrors.Text = "Houston, we got a problem";
            this.buttonErrors.UseVisualStyleBackColor = true;
            this.buttonErrors.Visible = false;
            this.buttonErrors.Click += new System.EventHandler(this.buttonErrors_Click);
            // 
            // buttonSaveAsPlaylist
            // 
            this.buttonSaveAsPlaylist.Location = new System.Drawing.Point(200, 36);
            this.buttonSaveAsPlaylist.Name = "buttonSaveAsPlaylist";
            this.buttonSaveAsPlaylist.Size = new System.Drawing.Size(182, 29);
            this.buttonSaveAsPlaylist.TabIndex = 1;
            this.buttonSaveAsPlaylist.Text = "&Save as Library";
            this.buttonSaveAsPlaylist.UseVisualStyleBackColor = true;
            this.buttonSaveAsPlaylist.Visible = false;
            this.buttonSaveAsPlaylist.Click += new System.EventHandler(this.buttonSaveAsPlaylist_Click);
            // 
            // radioButtonMusic
            // 
            this.radioButtonMusic.AutoSize = true;
            this.radioButtonMusic.Checked = true;
            this.radioButtonMusic.Location = new System.Drawing.Point(12, 64);
            this.radioButtonMusic.Name = "radioButtonMusic";
            this.radioButtonMusic.Size = new System.Drawing.Size(68, 24);
            this.radioButtonMusic.TabIndex = 8;
            this.radioButtonMusic.TabStop = true;
            this.radioButtonMusic.Text = "Music";
            this.radioButtonMusic.UseVisualStyleBackColor = true;
            this.radioButtonMusic.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButtonVideo
            // 
            this.radioButtonVideo.AutoSize = true;
            this.radioButtonVideo.Location = new System.Drawing.Point(126, 64);
            this.radioButtonVideo.Name = "radioButtonVideo";
            this.radioButtonVideo.Size = new System.Drawing.Size(69, 24);
            this.radioButtonVideo.TabIndex = 8;
            this.radioButtonVideo.Text = "Video";
            this.radioButtonVideo.UseVisualStyleBackColor = true;
            this.radioButtonVideo.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.radioButtonVideo);
            this.Controls.Add(this.radioButtonMusic);
            this.Controls.Add(this.buttonSaveAsPlaylist);
            this.Controls.Add(this.buttonErrors);
            this.Controls.Add(this.checkBoxRunWeb);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.textBoxMessages);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.buttonSaveChanges);
            this.Controls.Add(this.mainGrid);
            this.Controls.Add(this.btnCrawl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Media Manager Main Panel";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainGrid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCrawl;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.DataGridView mainGrid;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEdit;
        private System.Windows.Forms.Button buttonSaveChanges;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.TextBox textBoxMessages;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileLocs;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout;
        private System.Windows.Forms.CheckBox checkBoxRunWeb;
        private System.Windows.Forms.Button buttonErrors;
        private System.Windows.Forms.Button buttonSaveAsPlaylist;
        private System.Windows.Forms.RadioButton radioButtonMusic;
        private System.Windows.Forms.RadioButton radioButtonVideo;
    }
}

