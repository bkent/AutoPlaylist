using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.Odbc;
using TagLib;

namespace AutoPlaylist
{
    public partial class Form1 : Form
    {
        String DSN = "storytapes";
        List<string> exceptions = new List<string> { "_playlists"
            , "_playlistsexternal"
            , "Unknown artist"
            , "Paul Mckenna - Success For Life"
            , "Star Wars Audiobooks"
            , "CS Lewis"
            , "JK Rowling"
            , "Learning French"
            , "Sir Arthur Conan Doyle"};

        bool firstRun = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void bGo_Click(object sender, EventArgs e)
        {
            tb.Clear();
            ProcessDir(cbSource.Text);
        }

        public void ProcessDir(string sourceDir)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbPlayList = new StringBuilder();
            List<string> ls = new List<string>();
            
            string title = "";
            string playListTitle = "";
            string oldPlayListTitle = "";
            int ct = 0;
            int track = 1;
            string artist = "Unknown";
            string album = "Unknown";
            string oldArtist = "Unknown";
            string oldAlbum = "Unknown";
            string alreadyTagged = "N";

            sbPlayList.AppendLine("#EXTM3U"); // add this to the beginning of the first file

            foreach (string fileName in Directory.EnumerateFiles(sourceDir, "*.mp3", SearchOption.AllDirectories).OrderBy(filename => filename))
            {
                title = Path.GetDirectoryName(fileName).Replace(sourceDir,"");

               // if (ct == 10)
               //     break;

                if ((System.IO.File.GetLastWriteTime(Path.GetDirectoryName(fileName)).Date > dtp.Value.Date)
                    || (cbUpdateAllPlaylists.Checked))
                {
                    artist = "Unknown";
                    album = "Unknown";

                    string[] words = title.Split('\\');
                    if (words.Count() > 2)
                    {
                        playListTitle = words[2]; // the title eg. \author\title
                        album = words[2];
                        artist = words[1];
                    }
                    else
                    {
                        playListTitle = words[1]; // just the authors name - no title subfolders
                        artist = words[1];
                    }

                    if (cbUpdateAllPlaylists.Checked)
                        alreadyTagged = "N";
                    else
                    {
                        if (System.IO.File.GetCreationTime(fileName).Date > dtp.Value.Date)
                            alreadyTagged = CheckMP3Tag(fileName, artist, album, track);
                        else
                            alreadyTagged = "Y";
                    }

                    title = title.Replace(" ", "%20");
                    title = title.Replace("\\", "/");
                    if (!ls.Contains(title))
                    {
                        ls.Add(title);
                        if (ct > 0) // don't write an empty playlist on the first one
                        {
                            //if (update == "N") // i.e. the title requires an update
                            if (alreadyTagged == "N")
                            {
                                //sb.AppendLine("Adding " + oldPlayListTitle + ".m3u");
                                Log("Adding " + oldPlayListTitle + ".m3u");
                                StreamWriter playListFile = new StreamWriter(cbDestination.Text + oldPlayListTitle + ".m3u");
                                playListFile.Write(sbPlayList.ToString());
                                playListFile.Close();
                                SetTagsUpdated(oldArtist, oldAlbum);
                            }
                            //sb.AppendLine("Contents of sbPlayList are: " + sbPlayList.ToString());
                            //if (alreadyTagged != "X")
                            //SetTagsUpdated(oldArtist, oldAlbum); // this line - should never update id3tags 'X'
                            sbPlayList.Clear();
                            sbPlayList.AppendLine("#EXTM3U");
                            // track = 1; // reset the track number
                        }
                        //sb.AppendLine(playListTitle.ToUpper());
                        track = 1; // reset the track number   
                        Log(playListTitle.ToUpper());
                        playListTitle = playListTitle.Replace(" ", "_");
                        oldPlayListTitle = playListTitle.ToLower();
                        oldArtist = artist;
                        oldAlbum = album;
                        ct++;
                    }
                    /* sb.AppendLine(cbPrefix.Text
                         + title + "/"
                         + Path.GetFileName(fileName).Replace(" ", "%20"));*/
                    if (alreadyTagged != "Y")
                    {
                        Log(cbPrefix.Text
                            + title + "/"
                            + Path.GetFileName(fileName).Replace(" ", "%20"));

                        sbPlayList.AppendLine("#EXTINF:-1," + track.ToString() + " " + album + " - " + artist);

                        sbPlayList.AppendLine(cbPrefix.Text
                            + title + "/"
                            + Path.GetFileName(fileName).Replace(" ", "%20"));

                        track++;
                    }
                }
                else
                {
                    Log(title);
                }
            }
            //tb.Text = sb.ToString();
        }

        private void bDatabaseTest_Click(object sender, EventArgs e)
        {
            tb.Clear();
            GetDBAuthors();
        }

        private void bListSourceDirs_Click(object sender, EventArgs e)
        {
            tb.Clear();
            GetDirAuthors(cbSource.Text);
        }

        private DataTable GetDirAuthors(string sourceDir)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("author");
            StringBuilder sb = new StringBuilder();

            // count the slashes in the file path
            int slashCount = sourceDir.Count(x => x == '\\');

            foreach (string fileName in Directory.EnumerateDirectories(sourceDir).OrderBy(filename => filename))
            {
                string[] words = fileName.Split('\\'); // split the path up at the \ characters
                if (!exceptions.Contains(words[slashCount + 1]))
                {
                    sb.AppendLine(words[slashCount + 1]); // the author is at slashCount + 1  i.e. R:\storytapes\author (2 slashes)
                    dt.Rows.Add(words[slashCount + 1]);  // C:\Users\Ben\Music\author (4 slashes)
                }

            }
            //tb.Text = sb.ToString();
            return dt;
        }

        private DataTable GetDBAuthors()
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();

            using (ODBCClass o = new ODBCClass(DSN))
            {
                OdbcCommand oCommand = o.GetCommand("SELECT * FROM authors");
                OdbcDataReader oReader = oCommand.ExecuteReader();
                dt.Load(oReader);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        sb.AppendLine(row["author"].ToString());
                    }
                }
                //tb.Text = sb.ToString();
                return dt;
            }
        }

        private void bDiff_Click(object sender, EventArgs e)
        {
            tb.Clear();
            DataTable dtMaster = new DataTable(); // dtMaster is the list of directories
            DataTable dtSlave = new DataTable();  // the list from the database

            bool addToDb = false;
            int ct = 0;

            dtMaster = GetDirAuthors(cbSource.Text);
            dtSlave = GetDBAuthors();

            foreach (DataRow dirAuthor in dtMaster.Rows)
            {
                addToDb = true;
                //check if the authour exists in the db
                foreach (DataRow dbAuthor in dtSlave.Rows)
                {
                    if (dirAuthor["author"].ToString() == dbAuthor["author"].ToString())
                    {
                        addToDb = false;
                        break;
                    }
                }
                if (addToDb)
                {
                    InsertAuthorToDb(dirAuthor["author"].ToString());
                    ct++;
                }
            }
            Log("Done - " + Convert.ToInt32(ct) + " authors added.");
        }

        private void SyncAuthors()
        {
            tb.Clear();
            Log("Checking for new authors:");
            DataTable dtMaster = new DataTable(); // dtMaster is the list of directories
            DataTable dtSlave = new DataTable();  // the list from the database

            bool addToDb = false;
            int ct = 0;

            dtMaster = GetDirAuthors(cbSource.Text);
            dtSlave = GetDBAuthors();

            foreach (DataRow dirAuthor in dtMaster.Rows)
            {
                addToDb = true;
                //check if the authour exists in the db
                foreach (DataRow dbAuthor in dtSlave.Rows)
                {
                    if (dirAuthor["author"].ToString() == dbAuthor["author"].ToString())
                    {
                        addToDb = false;
                        break;
                    }
                }
                if (addToDb)
                {
                    InsertAuthorToDb(dirAuthor["author"].ToString());
                    ct++;
                }
            }
            Log("Done - " + Convert.ToInt32(ct) + " authors added.");
        }

        private void InsertAuthorToDb(string author)
        {
            //MessageBox.Show("Need to add " + author);
            using (ODBCClass o = new ODBCClass(DSN))
            {
                OdbcCommand oCommand = o.GetCommand("INSERT INTO authors(author) VALUES('" + author + "')");
                oCommand.ExecuteNonQuery();
            }
            Log(author);
        }

        private void Log(string s, bool toFile=false)
        {
            if (!toFile)
                tb.AppendText(s + Environment.NewLine);
            else
            {
                StreamWriter playListFile = new StreamWriter(@"C:\Playlist_log.txt");
                playListFile.WriteLine(s + Environment.NewLine);
                playListFile.Close();

            }
        }

        private void bTitles_Click(object sender, EventArgs e)
        {
            SyncAuthors();
            Log("");
            Log("Checking for new titles:");
            //tb.Clear();
            DataTable dt = GetDBAuthors();

            string sourceDir = cbSource.Text;
            int slashCount = sourceDir.Count(x => x == '\\');

            StringBuilder sb = new StringBuilder();
            List<string> folderTitles = new List<string>();
            List<string> dbTitles = new List<string>();
            StringBuilder tooLong = new StringBuilder();

            int authorID = 0;

            foreach (string fileName in Directory.EnumerateDirectories(sourceDir).OrderBy(filename => filename))
            {
                folderTitles.Clear();
                dbTitles.Clear();
                authorID = 0;
                string[] words = fileName.Split('\\'); // split the path up at the \ characters
                if (!exceptions.Contains(words[slashCount + 1]))
                {
                    //Log("AUTHOR: " + words[slashCount + 1].ToUpper());
                    //Log("FOLDER TITLES: ");
                    foreach (string authorName in Directory.EnumerateDirectories(fileName).OrderBy(authorName => authorName))
                    {                    
                        string[] words2 = authorName.Split('\\');
                        //Log(words2[slashCount + 2]);
                        //Log(authorName);
                        folderTitles.Add(words2[slashCount + 2]);
                    }
                    //Log("DB TITLES: ");
                    // find the database id of the current author
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["author"].ToString().ToLower() == words[slashCount + 1].ToLower())
                        {
                            authorID = Convert.ToInt32(row["id"]);
                            dbTitles = GetTitlesForAuthor(authorID);
                            break;
                        }
                    }

                   /* foreach (string s in dbTitles)
                    {
                        Log(s);
                    }*/

                    foreach (string s in folderTitles)
                    {
                        if (s.Length < 35)
                        {
                            if (!dbTitles.Contains(s, StringComparer.CurrentCultureIgnoreCase))
                            {
                                Log("Adding " + s);
                                InsertTitleToDb(s, authorID);
                            }
                            // else
                            //Log("OK " + s);
                        }
                        else
                            tooLong.AppendLine(s);
                    }
                }
                else
                    sb.AppendLine(words[slashCount + 1]);

            }
            Log("");
            Log("TITLE TOO LONG - CHECK MANUALLY");
            Log(tooLong.ToString());

            Log("");
            Log("EXCLUDED AUTHORS (as defined in the exclusions list)");
            Log(sb.ToString());
        }

        public List<string> GetTitlesForAuthor(int authorID)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();

            List<string> titles = new List<string>();

            using (ODBCClass o = new ODBCClass(DSN))
            {
                OdbcCommand oCommand = o.GetCommand("SELECT title FROM storytapes"
                    + " WHERE authorid=" + authorID.ToString());
                OdbcDataReader oReader = oCommand.ExecuteReader();
                dt.Load(oReader);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        titles.Add(row["title"].ToString());
                    }
                }
                return titles;
            }
        }

        private void InsertTitleToDb(string title, int authorID)
        {
            //MessageBox.Show("Need to add " + author);
            string sSQL = "INSERT INTO storytapes(authorid,title,addeddt,seriesorder,filepath,id3tags) VALUES("
                + authorID.ToString() + ",'" + title + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") 
                + "',0,'" + title.Replace(" ", "_").ToLower() +"','X')"; 
            using (ODBCClass o = new ODBCClass(DSN))
            {
                OdbcCommand oCommand = o.GetCommand(sSQL);
                oCommand.ExecuteNonQuery();
            }
            //Log(sSQL);
        }

        private void bQuickGo_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("SyncToy should have been run first. Proceed?"
                , "Warning", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // set the source dir
                cbSource.Text = "R:\\Story Tapes";
                // add the new stuff to the db
                bTitles_Click(this, e);
                // set the destination and web prefix
                /*Log("");
                Log("**** local playlists ****");
                Log("");
                cbPrefix.Text = "http://192.168.1.99/st";
                cbDestination.Text = "R:\\Story Tapes\\_playlists\\";
                ProcessDir(cbSource.Text);
                // set the destination and web prefix
                 */
                Log("");
                Log("**** Remote playlists ****");
                Log("");
                //
                firstRun = false;
                cbPrefix.Text = "http://benkent.servehttp.com/st";
                cbDestination.Text = "R:\\Story Tapes\\_playlistsexternal\\";
                ProcessDir(cbSource.Text);
                Log("DONE");
            }
        }

        private void bTagTest_Click(object sender, EventArgs e)
        {
            /*// load an mp3 file
            TagLib.File tagFile = TagLib.File.Create("C:\\Users\\Ben\\Music\\Agatha Christie\\Death in the Clouds\\Poirot_-_Death_in_the_Clouds_b009z4c6_default.mp3");

            // read a tag
            string album = tagFile.Tag.Album;

            List<string> sl = new List<string>();

            sl.Add("Agatha Christie");

            tagFile.Tag.AlbumArtists = sl.ToArray();

            //tagFile.Tag.AlbumArtists[0] = "Agatha Christie"; // can't set an array element like this

            // show it
            MessageBox.Show(album + Environment.NewLine
                + tagFile.Tag.Artists[0] + Environment.NewLine // artists is depreciated
            );

            tagFile.Tag.Track = 01;

            // set a tag
            tagFile.Tag.Year = 2013;

            // save it
            tagFile.Save();*/

            string sourceDir = "C:\\Users\\Ben\\Music\\Agatha Christie\\The Mysterious Affair at Styles";
            int track = 0;

            //foreach (string fileName in Directory.EnumerateFiles(sourceDir, "*.mp3", SearchOption.AllDirectories).OrderBy(filename => filename))
            foreach (string fileName in Directory.EnumerateFiles(sourceDir).OrderBy(filename => filename))
            {
                string title = Path.GetDirectoryName(fileName).Replace("C:\\Users\\Ben\\Music", "");

                string artist = "Unknown";
                string album = "Unknown";

                string[] words = title.Split('\\');
                if (words.Count() > 2)
                {
                    string playListTitle = words[2]; // the title eg. \author\title
                    album = words[2];
                    artist = words[1];
                }
                else
                {
                    string playListTitle = words[1]; // just the authors name - no title subfolders
                    artist = words[1];
                }
                track++;

                //TestCheckMP3Tag(fileName, artist, album, track);
            }
        }

        private void oldCheckMP3Tag(string fileName, string author, string album, int track)
        {
            TagLib.File tagFile = TagLib.File.Create(fileName);
            if (tagFile.Tag.Album != "")
                Log(fileName + " already has tags." + " Author: " + author + " Album: " + album + " Track: " + track.ToString());
            else
                Log(fileName + " TAGS REQUIRED" + " Author: " + author + " Album: " + album + " Track: " + track.ToString());
        }

        private string CheckMP3Tag(string fileName, string author, string album, int track)
        {
            // sanitise album string - pesky george's marvellous medcine!!
            album = album.Replace("'", "''");
            // check the value of id3tag for the author/album
            DataTable dt = new DataTable();
            using (ODBCClass o = new ODBCClass(DSN))
            {
                OdbcCommand oCommand = o.GetCommand("SELECT id3tags FROM storytapes" 
                    + " WHERE title = '" + album + "'"
                    + " AND authorid=(SELECT id FROM `authors` WHERE author='" + author + "')");
                OdbcDataReader oReader = oCommand.ExecuteReader();
                dt.Load(oReader);
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["id3tags"].ToString() != "N") // tagged - so exit // changed to if not untagged - value can be Y (tagged) N (not tagged) or X (not complete)
                    {
                        return row["id3tags"].ToString(); // Y or X - don't bother
                    }
                }
            }

            if (cbTags.Checked)
            {
                TagLib.File tagFile = TagLib.File.Create(fileName);
                /*if (tagFile.Tag.Album != null)
                {
                    Log(fileName + " already has tags." + " Author: " + author + " Album: " + album + " Track: " + track.ToString());
                    // update id3tag field
                    //SetTagsUpdated(author, album);
                }
                else
                {*/
                Log(fileName + " TAGS REQUIRED" + " Author: " + author + " Album: " + album + " Track: " + track.ToString());

                List<string> sl = new List<string>();
                sl.Add(author);

                tagFile.Tag.AlbumArtists = sl.ToArray();
                tagFile.Tag.Artists = sl.ToArray();
                tagFile.Tag.Track = (uint)track;
                tagFile.Tag.Album = album;

                tagFile.Save();
            }
            return "N";

                //}
            
        }

        private void SetTagsUpdated(string author, string album)
        {
            if (!firstRun)
            {
                album = album.Replace("'", "");
                using (ODBCClass o = new ODBCClass(DSN))
                {
                    OdbcCommand oCommand = o.GetCommand("UPDATE storytapes SET"
                        + " id3tags='Y'"
                        + " WHERE title = '" + album + "'"
                        + " AND authorid=(SELECT id FROM `authors` WHERE author='" + author + "')");
                    oCommand.ExecuteNonQuery();
                }
            }
        }

        private void bRunTagging_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbPlayList = new StringBuilder();
            List<string> ls = new List<string>();

            string sourceDir = @"C:\Users\Ben\Music";

            string title = "";
            string playListTitle = "";
            string oldPlayListTitle = "";
            int ct = 0;
            int track = 1;
            string artist = "Unknown";
            string album = "Unknown";
            string oldArtist = "Unknown";
            string oldAlbum = "Unknown";
            string alreadyTagged = "N";

            sbPlayList.AppendLine("#EXTM3U"); // add this to the beginning of the first file

            foreach (string fileName in Directory.EnumerateFiles(sourceDir, "*.mp3", SearchOption.AllDirectories).OrderBy(filename => filename))
            {
                title = Path.GetDirectoryName(fileName).Replace(sourceDir, "");
                
                artist = "Unknown";
                album = "Unknown";

                string[] words = title.Split('\\');
                if (words.Count() > 2)
                {
                    playListTitle = words[2]; // the title eg. \author\title
                    album = words[2];
                    artist = words[1];
                }
                else
                {
                    playListTitle = words[1]; // just the authors name - no title subfolders
                    artist = words[1];
                }

                if (!ls.Contains(title))
                {
                    ls.Add(title);
                    //sb.AppendLine(playListTitle.ToUpper());
                    track = 1; // reset the track number   
                    oldArtist = artist;
                    oldAlbum = album;
                    ct++;
                }

                if (System.IO.File.GetCreationTime(fileName).Date > dtp.Value.Date)
                    alreadyTagged = CheckMP3Tag(fileName, artist, album, track);

                track++;
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtp.Value = DateTime.Now.AddDays(-2);
        }
    }
}