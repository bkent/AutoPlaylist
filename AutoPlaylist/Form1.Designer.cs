namespace AutoPlaylist
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.cbSource = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbDestination = new System.Windows.Forms.ComboBox();
            this.tb = new System.Windows.Forms.TextBox();
            this.bGo = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPrefix = new System.Windows.Forms.ComboBox();
            this.bDatabaseTest = new System.Windows.Forms.Button();
            this.bListSourceDirs = new System.Windows.Forms.Button();
            this.bDiff = new System.Windows.Forms.Button();
            this.bTitles = new System.Windows.Forms.Button();
            this.bQuickGo = new System.Windows.Forms.Button();
            this.bTagTest = new System.Windows.Forms.Button();
            this.cbTags = new System.Windows.Forms.CheckBox();
            this.cbUpdateAllPlaylists = new System.Windows.Forms.CheckBox();
            this.bRunTagging = new System.Windows.Forms.Button();
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.bNewGo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbIgnoreDateUseTags = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source Directory:";
            // 
            // cbSource
            // 
            this.cbSource.FormattingEnabled = true;
            this.cbSource.Items.AddRange(new object[] {
            "R:\\Story Tapes",
            "C:\\Users\\Ben\\Music"});
            this.cbSource.Location = new System.Drawing.Point(126, 12);
            this.cbSource.Name = "cbSource";
            this.cbSource.Size = new System.Drawing.Size(175, 21);
            this.cbSource.TabIndex = 1;
            this.cbSource.Text = "C:\\Users\\Ben\\Music";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Destination Directory:";
            // 
            // cbDestination
            // 
            this.cbDestination.FormattingEnabled = true;
            this.cbDestination.Items.AddRange(new object[] {
            "R:\\Story Tapes\\_playlists\\",
            "R:\\Story Tapes\\_playlistsexternal\\",
            "C:\\Users\\Ben\\Music\\_playlists\\",
            "C:\\Users\\Ben\\Music\\_playlistsexternal\\"});
            this.cbDestination.Location = new System.Drawing.Point(126, 39);
            this.cbDestination.Name = "cbDestination";
            this.cbDestination.Size = new System.Drawing.Size(175, 21);
            this.cbDestination.TabIndex = 3;
            this.cbDestination.Text = "C:\\Users\\Ben\\Music\\_playlistsexternal\\";
            // 
            // tb
            // 
            this.tb.Location = new System.Drawing.Point(12, 66);
            this.tb.Multiline = true;
            this.tb.Name = "tb";
            this.tb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb.Size = new System.Drawing.Size(715, 256);
            this.tb.TabIndex = 4;
            // 
            // bGo
            // 
            this.bGo.Location = new System.Drawing.Point(483, 37);
            this.bGo.Name = "bGo";
            this.bGo.Size = new System.Drawing.Size(102, 23);
            this.bGo.TabIndex = 5;
            this.bGo.Text = "Create Playlists";
            this.bGo.UseVisualStyleBackColor = true;
            this.bGo.Visible = false;
            this.bGo.Click += new System.EventHandler(this.bGo_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(315, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Web prefix:";
            // 
            // cbPrefix
            // 
            this.cbPrefix.FormattingEnabled = true;
            this.cbPrefix.Items.AddRange(new object[] {
            "http://192.168.0.99/st",
            "http://benkent.servehttp.com/st",
            "http://listener:mHmrAXlK2GO8clVY@benkent.servehttp.com/st"});
            this.cbPrefix.Location = new System.Drawing.Point(382, 10);
            this.cbPrefix.Name = "cbPrefix";
            this.cbPrefix.Size = new System.Drawing.Size(185, 21);
            this.cbPrefix.TabIndex = 7;
            this.cbPrefix.Text = "http://benkent.servehttp.com/st";
            // 
            // bDatabaseTest
            // 
            this.bDatabaseTest.Location = new System.Drawing.Point(307, 37);
            this.bDatabaseTest.Name = "bDatabaseTest";
            this.bDatabaseTest.Size = new System.Drawing.Size(26, 23);
            this.bDatabaseTest.TabIndex = 8;
            this.bDatabaseTest.Text = "Db Test";
            this.bDatabaseTest.UseVisualStyleBackColor = true;
            this.bDatabaseTest.Visible = false;
            this.bDatabaseTest.Click += new System.EventHandler(this.bDatabaseTest_Click);
            // 
            // bListSourceDirs
            // 
            this.bListSourceDirs.Location = new System.Drawing.Point(339, 37);
            this.bListSourceDirs.Name = "bListSourceDirs";
            this.bListSourceDirs.Size = new System.Drawing.Size(24, 23);
            this.bListSourceDirs.TabIndex = 9;
            this.bListSourceDirs.Text = "List SourceDir";
            this.bListSourceDirs.UseVisualStyleBackColor = true;
            this.bListSourceDirs.Visible = false;
            this.bListSourceDirs.Click += new System.EventHandler(this.bListSourceDirs_Click);
            // 
            // bDiff
            // 
            this.bDiff.Location = new System.Drawing.Point(369, 37);
            this.bDiff.Name = "bDiff";
            this.bDiff.Size = new System.Drawing.Size(27, 23);
            this.bDiff.TabIndex = 10;
            this.bDiff.Text = "Differences";
            this.bDiff.UseVisualStyleBackColor = true;
            this.bDiff.Visible = false;
            this.bDiff.Click += new System.EventHandler(this.bDiff_Click);
            // 
            // bTitles
            // 
            this.bTitles.Location = new System.Drawing.Point(591, 37);
            this.bTitles.Name = "bTitles";
            this.bTitles.Size = new System.Drawing.Size(85, 23);
            this.bTitles.TabIndex = 11;
            this.bTitles.Text = "Add to DB";
            this.bTitles.UseVisualStyleBackColor = true;
            this.bTitles.Visible = false;
            this.bTitles.Click += new System.EventHandler(this.bTitles_Click);
            // 
            // bQuickGo
            // 
            this.bQuickGo.Location = new System.Drawing.Point(568, 5);
            this.bQuickGo.Name = "bQuickGo";
            this.bQuickGo.Size = new System.Drawing.Size(56, 23);
            this.bQuickGo.TabIndex = 12;
            this.bQuickGo.Text = "Quick Go";
            this.bQuickGo.UseVisualStyleBackColor = true;
            this.bQuickGo.Visible = false;
            this.bQuickGo.Click += new System.EventHandler(this.bQuickGo_Click);
            // 
            // bTagTest
            // 
            this.bTagTest.Location = new System.Drawing.Point(402, 37);
            this.bTagTest.Name = "bTagTest";
            this.bTagTest.Size = new System.Drawing.Size(75, 23);
            this.bTagTest.TabIndex = 13;
            this.bTagTest.Text = "Tag Test";
            this.bTagTest.UseVisualStyleBackColor = true;
            this.bTagTest.Visible = false;
            this.bTagTest.Click += new System.EventHandler(this.bTagTest_Click);
            // 
            // cbTags
            // 
            this.cbTags.AutoSize = true;
            this.cbTags.Checked = true;
            this.cbTags.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTags.Location = new System.Drawing.Point(126, 331);
            this.cbTags.Name = "cbTags";
            this.cbTags.Size = new System.Drawing.Size(104, 17);
            this.cbTags.TabIndex = 14;
            this.cbTags.Text = "Update ID3 tags";
            this.cbTags.UseVisualStyleBackColor = true;
            this.cbTags.Visible = false;
            // 
            // cbUpdateAllPlaylists
            // 
            this.cbUpdateAllPlaylists.AutoSize = true;
            this.cbUpdateAllPlaylists.Location = new System.Drawing.Point(15, 331);
            this.cbUpdateAllPlaylists.Name = "cbUpdateAllPlaylists";
            this.cbUpdateAllPlaylists.Size = new System.Drawing.Size(113, 17);
            this.cbUpdateAllPlaylists.TabIndex = 15;
            this.cbUpdateAllPlaylists.Text = "Update all playlists";
            this.cbUpdateAllPlaylists.UseVisualStyleBackColor = true;
            this.cbUpdateAllPlaylists.Visible = false;
            // 
            // bRunTagging
            // 
            this.bRunTagging.Location = new System.Drawing.Point(258, 324);
            this.bRunTagging.Name = "bRunTagging";
            this.bRunTagging.Size = new System.Drawing.Size(75, 23);
            this.bRunTagging.TabIndex = 16;
            this.bRunTagging.Text = "Run Tags";
            this.bRunTagging.UseVisualStyleBackColor = true;
            this.bRunTagging.Visible = false;
            this.bRunTagging.Click += new System.EventHandler(this.bRunTagging_Click);
            // 
            // dtp
            // 
            this.dtp.Location = new System.Drawing.Point(591, 328);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(136, 20);
            this.dtp.TabIndex = 17;
            // 
            // bNewGo
            // 
            this.bNewGo.Location = new System.Drawing.Point(672, 5);
            this.bNewGo.Name = "bNewGo";
            this.bNewGo.Size = new System.Drawing.Size(55, 50);
            this.bNewGo.TabIndex = 18;
            this.bNewGo.Text = "Go";
            this.bNewGo.UseVisualStyleBackColor = true;
            this.bNewGo.Click += new System.EventHandler(this.bNewGo_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(332, 331);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(253, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Process title (tags and playlist) if any files older than: ";
            // 
            // cbIgnoreDateUseTags
            // 
            this.cbIgnoreDateUseTags.AutoSize = true;
            this.cbIgnoreDateUseTags.Location = new System.Drawing.Point(559, 352);
            this.cbIgnoreDateUseTags.Name = "cbIgnoreDateUseTags";
            this.cbIgnoreDateUseTags.Size = new System.Drawing.Size(168, 17);
            this.cbIgnoreDateUseTags.TabIndex = 20;
            this.cbIgnoreDateUseTags.Text = "Ignore dates - go by untagged";
            this.cbIgnoreDateUseTags.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 373);
            this.Controls.Add(this.cbIgnoreDateUseTags);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bNewGo);
            this.Controls.Add(this.dtp);
            this.Controls.Add(this.bRunTagging);
            this.Controls.Add(this.cbUpdateAllPlaylists);
            this.Controls.Add(this.cbTags);
            this.Controls.Add(this.bTagTest);
            this.Controls.Add(this.bQuickGo);
            this.Controls.Add(this.bTitles);
            this.Controls.Add(this.bDiff);
            this.Controls.Add(this.bListSourceDirs);
            this.Controls.Add(this.bDatabaseTest);
            this.Controls.Add(this.cbPrefix);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bGo);
            this.Controls.Add(this.tb);
            this.Controls.Add(this.cbDestination);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbSource);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Playlist Generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbDestination;
        private System.Windows.Forms.TextBox tb;
        private System.Windows.Forms.Button bGo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbPrefix;
        private System.Windows.Forms.Button bDatabaseTest;
        private System.Windows.Forms.Button bListSourceDirs;
        private System.Windows.Forms.Button bDiff;
        private System.Windows.Forms.Button bTitles;
        private System.Windows.Forms.Button bQuickGo;
        private System.Windows.Forms.Button bTagTest;
        private System.Windows.Forms.CheckBox cbTags;
        private System.Windows.Forms.CheckBox cbUpdateAllPlaylists;
        private System.Windows.Forms.Button bRunTagging;
        private System.Windows.Forms.DateTimePicker dtp;
        private System.Windows.Forms.Button bNewGo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbIgnoreDateUseTags;

    }
}

