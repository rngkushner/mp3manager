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
            this.songGrid = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFileLocs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemLibraryFunctions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpenLibrary = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCrawlLibrary = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemNameCleanup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSaveLibrary = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonSaveChanges = new System.Windows.Forms.Button();
            this.buttonDone = new System.Windows.Forms.Button();
            this.textBoxMessages = new System.Windows.Forms.TextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxRunWeb = new System.Windows.Forms.CheckBox();
            this.buttonErrors = new System.Windows.Forms.Button();            
            ((System.ComponentModel.ISupportInitialize)(this.songGrid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCrawl
            // 
            this.btnCrawl.Location = new System.Drawing.Point(0, 0);
            this.btnCrawl.Name = "btnCrawl";
            this.btnCrawl.Size = new System.Drawing.Size(75, 23);
            this.btnCrawl.TabIndex = 8;
            // 
            // songGrid
            // 
            this.songGrid.AllowUserToAddRows = false;
            this.songGrid.AllowUserToDeleteRows = false;
            this.songGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.songGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.songGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.songGrid.Location = new System.Drawing.Point(12, 94);
            this.songGrid.Name = "songGrid";
            this.songGrid.RowHeadersWidth = 51;
            this.songGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.songGrid.Size = new System.Drawing.Size(776, 299);
            this.songGrid.TabIndex = 3;
            this.songGrid.Text = "dataGridView1";
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
            // toolStripMenuItemLibraryFunctions
            // 
            this.toolStripMenuItemLibraryFunctions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpenLibrary,
            this.toolStripMenuItemCrawlLibrary,
            this.toolStripMenuItemNameCleanup,
            this.toolStripMenuItemSaveLibrary});
            this.toolStripMenuItemLibraryFunctions.Name = "toolStripMenuItemLibraryFunctions";
            this.toolStripMenuItemLibraryFunctions.Size = new System.Drawing.Size(126, 24);
            this.toolStripMenuItemLibraryFunctions.Text = "&Manage Library";
            // 
            // toolStripMenuItemOpenLibrary
            // 
            this.toolStripMenuItemOpenLibrary.Name = "toolStripMenuItemOpenLibrary";
            this.toolStripMenuItemOpenLibrary.Size = new System.Drawing.Size(280, 26);
            this.toolStripMenuItemOpenLibrary.Text = "&Open Library";
            this.toolStripMenuItemOpenLibrary.Click += new System.EventHandler(this.toolStripMenuItemOpenLibrary_Click);
            // 
            // toolStripMenuItemCrawlLibrary
            // 
            this.toolStripMenuItemCrawlLibrary.Name = "toolStripMenuItemCrawlLibrary";
            this.toolStripMenuItemCrawlLibrary.Size = new System.Drawing.Size(280, 26);
            this.toolStripMenuItemCrawlLibrary.Text = "C&rawl Folders";
            this.toolStripMenuItemCrawlLibrary.Click += new System.EventHandler(this.toolStripMenuItemCrawlLibrary_Click);
            // 
            // toolStripMenuItemNameCleanup
            // 
            this.toolStripMenuItemNameCleanup.Name = "toolStripMenuItemNameCleanup";
            this.toolStripMenuItemNameCleanup.Size = new System.Drawing.Size(280, 26);
            this.toolStripMenuItemNameCleanup.Text = "Clean up &Names (Light Blue)";
            this.toolStripMenuItemNameCleanup.Click += new System.EventHandler(this.toolStripMenuItemNameCleanup_Click);
            // 
            // toolStripMenuItemSaveLibrary
            // 
            this.toolStripMenuItemSaveLibrary.Name = "toolStripMenuItemSaveLibrary";
            this.toolStripMenuItemSaveLibrary.Size = new System.Drawing.Size(280, 26);
            this.toolStripMenuItemSaveLibrary.Text = "&Save Library";
            this.toolStripMenuItemSaveLibrary.Click += new System.EventHandler(this.toolStripMenuItemSaveLibrary_Click);
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
            this.toolStripMenuItemLibraryFunctions,
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonErrors);
            this.Controls.Add(this.checkBoxRunWeb);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.textBoxMessages);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.buttonSaveChanges);
            this.Controls.Add(this.songGrid);
            this.Controls.Add(this.btnCrawl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "MP3 Main Panel";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.songGrid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCrawl;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.DataGridView songGrid;
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
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLibraryFunctions;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenLibrary;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCrawlLibrary;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSaveLibrary;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNameCleanup;
    }
}

