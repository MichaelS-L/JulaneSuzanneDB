using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.IO;
using System.Xml;
using System.Runtime.InteropServices; 

namespace JulaneSuzanneDB
{
    public partial class Images : Form
    {
        String configMainDir = "";
        String configMainName;
        String configName = ".config";
        String configExt = ".txt";
        String dataPath;
        List<String> dbList = new List<String>();
        int dbSelection;
        EmailClass email = new EmailClass();
        EmailConfigForm emailConfig = new EmailConfigForm();
        List<Form> iForms = new List<Form>();
        ImgClass imgs;
        int imagesPerLine = 1000;
        bool keywordsChanged;
        bool keywordsDisplayed;
        int widthOfThumbnail = 1000;
        List<int> imageIndexes = new List<int>();
        List<CheckBox> imageCBs = new List<CheckBox>();
        List<PictureBox> imagePBs = new List<PictureBox>();
        List<TextBox> imageTBs = new List<TextBox>();
        List<int> indexesFull = new List<int>();
        KeywordDisplay kd;
        KeywordsClass keyList;
        String progName;

        public Images()
        {
            InitializeComponent();
        }

        private void Images_Load(object sender, EventArgs e)
        {
            String configDir;
            List<int> indexes = new List<int>();

            // Get Executable Name
            progName = System.AppDomain.CurrentDomain.FriendlyName;
            progName = progName.Substring(0, progName.IndexOf("."));
            this.Text = progName;

            // Calculate Data Directory
            configDir = progName + "_Data";
            if (!Directory.Exists(configDir))
            {
                Directory.CreateDirectory(configDir);
            }
            Directory.SetCurrentDirectory(configDir);
            configMainDir = Directory.GetCurrentDirectory();

            configMainName = configName + "_Main_" + progName + configExt;
            dbSelection = mainConfigRead(configMainName);

            // Get Database Selection
            dataPath = "";

            if (dataPath.Length == 0)
            {
                if (dbList.Count == 0)
                {
                    dbList.Add(configMainDir);
                }

                dataPath = dbList[dbSelection];

                if (dataPath.Length > 0)
                {
                    // Verify Data Directory
                    if (!Directory.Exists(dataPath))
                    {
                        MessageBox.Show("Problem with selected folder", "Error");
                        dataPath = "";
                    }
                }
            }

            mainConfigWrite(configMainName, configMainDir);

            // Switch to Data Directory
            Directory.SetCurrentDirectory(dataPath);
            openNewData();
        }

        private void openNewData()
        {
            List<int> indexesActive = new List<int>();

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            this.Height = Screen.PrimaryScreen.Bounds.Height - 50;
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            readData();
            imgs.buildData(dataPath);
            indexesActive = imgs.getImagesTableIndexes();
            indexesFull = indexesActive;
            if (kd == null)
            {
                kd = new KeywordDisplay(1000, new cbEvent(cbChangedFromChild), new closedEvent(cbClosedFromChild),
                    new dockEvent(changeDockingFromChild), new keywordChangeEvent(kwChangedFromChild));
            }
            //kd.Close();
            kd.clear();
            displayImages(indexesActive);
            kd.setKeywordList(keyList);
            btnKeywords.Text = "Hide Keywords";
            keywordsDisplayed = true;
            displayKeywords(true);
        }

        private int mainConfigRead(String configMainName)
        {
            int result = -1;
            String tempS;
            String[] tempSA;

            // *****************************************************************
            // * Main Config File
            if (File.Exists(configMainName))
            {
                // Read Main Config file
                using (StreamReader configFile = File.OpenText(configMainName))
                {
                    while ((tempS = configFile.ReadLine()) != null)
                    {
                        tempSA = tempS.Split('>');
                        switch (tempSA[0])
                        {
                            case "DB":
                                dbList.Add(tempSA[1]);
                                break;

                            case "Selection":
                                result = Convert.ToInt16(tempSA[1]);
                                break;

                            default:
                                // This is a comment
                                break;
                        }
                    }
                }
            }

            return result;
        } // private void mainConfigRead(String configMainName)

        private void mainConfigWrite(String fn, String path)
        {
            using (StreamWriter configFile = File.CreateText(path + "\\" + fn))
            {
                foreach (String tempS in dbList)
                {
                    configFile.WriteLine("DB>" + tempS);
                }

                configFile.WriteLine("Selection>" + dbSelection.ToString());

                configFile.Flush();
            }
        } // private void mainConfigWrite(String fn, String path)

        private void readData()
        {
            String configFullName;
            String tempS;
            String[] tempSA;

            // *****************************************************************
            // * Config File
            configFullName = configName + "_" + progName + configExt;
            if (File.Exists(configFullName))
            {
                // Read Config file
                using (StreamReader configFile = File.OpenText(configFullName))
                {
                    while ((tempS = configFile.ReadLine()) != null)
                    {
                        tempSA = tempS.Split('>');
                        switch (tempSA[0])
                        {
                            case "emailFrom":
                                email.setFrom(tempSA[1]);
                                emailConfig.setFrom(tempSA[1]);
                                break;

                            case "emailTo":
                                email.setTo(tempSA[1]);
                                emailConfig.setTo(tempSA[1]);
                                break;

                            case "number":
                                // The number per line
                                // 0 indicates 'as many as possible'
                                imagesPerLine = Convert.ToInt16(tempSA[1]);
                                break;

                            case "width":
                                // The width (in pixels) of the thumbnail
                                widthOfThumbnail = Convert.ToInt16(tempSA[1]);
                                break;

                            default:
                            // This is a comment
                                break;
                        }
                    }

                    if (imagesPerLine == 1000 || widthOfThumbnail == 1000 )
                    {
                        tempS = "Config File (" + Directory.GetCurrentDirectory() + "\\" + configFullName + ")\n";
                        tempS += "corupt - Fix or Delete\n Program Closing";
                        MessageBox.Show(tempS, "Error", MessageBoxButtons.OK);
                        Application.Exit();
                    }
                }
            }
            else
            {
                // Create Config file
                using (StreamWriter configFile = File.CreateText(configFullName))
                {
                    configFile.WriteLine("number>0");
                    configFile.WriteLine("width>60");
                    configFile.WriteLine("emailFrom>mstormlevia@gmail.com");
                    configFile.WriteLine("emailTo>michaelsl@centurylink.net");
                }

                imagesPerLine = 0;
                widthOfThumbnail = 60;
                email.setTo("michaelsl@centurylink.net");
                email.setFrom("mstormlevia@gmail.com");
            }
            // *****************************************************************

            // *****************************************************************
            // * Keyword File
            keyList = new KeywordsClass(progName);
            keyList.clear();
            keyList.retrieveFileData();
            // *****************************************************************

            // *****************************************************************
            // * Data File
            imgs = new ImgClass(progName);
            imgs.retrieveFileData(keyList);
            // *****************************************************************
        } // private void readData()

        private void displayImages(List<int> indexes)
        {
            int calculatedImagesPerLine;
            List<String> ch = imgs.getImagesTableLabels();
            DataRow dr;
            int ii;
            Image image;
            int index;
            int indexesIndex;
            int jj;
            int maxIndex;
            String name;
            String path;
            int row;
            TextBox tb;
            Image thumb;

            Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            ContextMenu cm = new ContextMenu();
            MenuItem miO = new MenuItem();
            miO.Text = "Open";

            MenuItem miC = new MenuItem();
            miC.Text = "Copy";

            MenuItem miCF = new MenuItem();
            miCF.Text = "Copy Filename";

            MenuItem miSE = new MenuItem();
            miSE.Text = "Send Email";

            MenuItem miCP = new MenuItem();
            miCP.Text = "Copy Filename with Path";

            cm.MenuItems.Add(miO);
            cm.MenuItems.Add(miC);
            cm.MenuItems.Add(miCF);
            cm.MenuItems.Add(miSE);
            cm.MenuItems.Add(miCP);
            miO.Click += new System.EventHandler(this.miO_Click);
            miC.Click += new System.EventHandler(this.miC_Click);
            miCF.Click += new System.EventHandler(this.miCF_Click);
            miSE.Click += new System.EventHandler(this.miSE_Click);
            miCP.Click += new System.EventHandler(this.miCP_Click);

            imageCBs.Clear();
            imagePBs.Clear();
            imageTBs.Clear();
            maxIndex = imgs.getMaxIndex();

            kd.getDocking();
            if (!kd.getDocking())
            {
                DataDisplay.SplitterDistance = DataDisplay.Width;
            }
            DataDisplay.Panel1.AutoScroll = true;
            DataDisplay.Panel2.AutoScroll = true;
            DataDisplay.FixedPanel = FixedPanel.Panel2;

            if (imagesPerLine == 0)
            {
                decimal tempD = DataDisplay.Panel1.Width / (widthOfThumbnail + 5);
                calculatedImagesPerLine = (int)Math.Truncate(tempD);
            }
            else
            {
                calculatedImagesPerLine = imagesPerLine;
            }

            imageIndexes.Clear();
            indexesIndex = 0;
            DataDisplay.Panel1.Controls.Clear();
            imgs.setImagesTableIterate();
            for (ii = 0, jj = 0, row = 0; ii <= maxIndex && indexesIndex < indexes.Count; ii++)
            {
                if (jj >= calculatedImagesPerLine)
                {
                    row++;
                    jj = 0;
                }

                dr = imgs.getImagesTableIterate();
                index = Convert.ToInt16(dr[imgs.getImagesTableColumnIndex("Index")]);
                name = (String)dr[imgs.getImagesTableColumnIndex("Name")];
                path = (String)dr[imgs.getImagesTableColumnIndex("Path")];

                if (index != indexes[indexesIndex])
                {
                    continue;
                }

                imageCBs.Add(new CheckBox());
                imageCBs[indexesIndex].Height = 13;
                imageCBs[indexesIndex].Width = 15;
                imageCBs[indexesIndex].Location = new Point((widthOfThumbnail + 5) * jj, (widthOfThumbnail + 15 + 28) * row);
                imageCBs[indexesIndex].Name = name;
                imageCBs[indexesIndex].AccessibleName = index.ToString();
                imageCBs[indexesIndex].AccessibleDescription = path;
                imageCBs[indexesIndex].MouseHover += new System.EventHandler(mouseHover);
                DataDisplay.Panel1.Controls.Add(imageCBs[indexesIndex]);

                image = Image.FromFile(path);
                thumb = image.GetThumbnailImage(widthOfThumbnail, widthOfThumbnail, callback, new IntPtr());
                thumb.Tag = name.Substring(0, name.IndexOf("."));
                imagePBs.Add(new PictureBox());
                imagePBs[indexesIndex].Image = thumb;
                imagePBs[indexesIndex].SizeMode = PictureBoxSizeMode.AutoSize;
                imagePBs[indexesIndex].Location = new Point((widthOfThumbnail + 5) * jj, (widthOfThumbnail + 15 + 28) * row + 14);
                imagePBs[indexesIndex].MouseHover += new System.EventHandler(mouseHover);
                imagePBs[indexesIndex].DoubleClick += new System.EventHandler(doubleClick);
                imagePBs[indexesIndex].Name = name;
                imagePBs[indexesIndex].ContextMenu = cm;
                DataDisplay.Panel1.Controls.Add(imagePBs[indexesIndex]);

                tb = new TextBox();
                tb.MouseHover += new System.EventHandler(mouseHover);
                if (name.Substring(0, name.IndexOf(".")).Length > 7)
                {
                    tb.Text = name.Substring(0, 7);
                }
                else
                {
                    tb.Text = name.Substring(0, name.IndexOf("."));
                }
                tb.Tag = name.Substring(0, name.IndexOf("."));
                tb.Width = widthOfThumbnail;
                tb.TextAlign = HorizontalAlignment.Center;
                tb.Name = name;
                tb.Location = new Point((widthOfThumbnail + 5) * jj, (widthOfThumbnail + 15 + 28) * row + widthOfThumbnail + 15);
                tb.Enabled = false;
                imageTBs.Add(tb);
                DataDisplay.Panel1.Controls.Add(imageTBs[indexesIndex]);

                imageIndexes.Add(index);
                jj++;
                indexesIndex++;
            }
        } // private void displayImages()

        private void displayImagesRefresh()
        {
            int calculatedImagesPerLine;
            List<String> ch = imgs.getImagesTableLabels();
            int ii;
            int jj;
            int maxIndex;
            int row;

            maxIndex = imgs.getMaxIndex();
            maxIndex = imageCBs.Count - 1;
            if (imagesPerLine == 0)
            {
                decimal tempD = DataDisplay.Panel1.Width / (widthOfThumbnail + 5);
                calculatedImagesPerLine = (int)Math.Truncate(tempD);
            }
            else
            {
                calculatedImagesPerLine = imagesPerLine;
            }

            for (ii = 0, jj = 0, row = 0; ii <= maxIndex; ii++, jj++)
            {
                if (jj >= calculatedImagesPerLine)
                {
                    row++;
                    jj = 0;
                }

                imageCBs[ii].Location = new Point((widthOfThumbnail + 5) * jj, (widthOfThumbnail + 15 + 28) * row);
                imagePBs[ii].Location = new Point((widthOfThumbnail + 5) * jj, (widthOfThumbnail + 15 + 28) * row + 14);
                imageTBs[ii].Location = new Point((widthOfThumbnail + 5) * jj, (widthOfThumbnail + 15 + 28) * row + widthOfThumbnail + 15);
            }
        } // private void displayImagesRefresh()

        /*
        private class ImageClass : /* Make sortable -> IEquatable<ImageClass>,* / IComparable<ImageClass> /*<- Make sortable* /
        {
            public String name { get; set; }
            public List<String> keys = new List<String>();

            // The following is to make the class sortable *****
            public int CompareTo(ImageClass compareImage)
            {
                // A null value means that this object is greater.
                if (compareImage == null)
                    return 1;

                else
                    return this.name.CompareTo(compareImage.name);
            }

            /* Keeping the following for further sort info
            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                ImageClass objAsImage = obj as ImageClass;
                if (objAsImage == null) return false;
                else return Equals(objAsImage);
            }
            public int SortByPathAscending(string path1, string path2)
            {

                return path1.CompareTo(path2);
            }

             * public override int GetHashCode()
            {
                // Need to return name which is type String
                // Don't know how to do it
                return 1; // name;
            }
            public bool Equals(ImageClass other)
            {
                if (other == null) return false;
                return (this.name.Equals(other.name));
            }
            // Should also override == and != operators.
            * /
            // The above is to make the class sortable *********
        }*/

        public bool ThumbnailCallback()
        {
            return true;
        }

        private void mouseHover(object sender, System.EventArgs e)
        {
            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTips text
            foreach (var imageCB in imageCBs)
            {
                toolTip1.SetToolTip(imageCB, imageCB.Name);
                toolTip1.SetToolTip(imageCB, imageCB.Name);
                toolTip1.SetToolTip(imageCB, imageCB.Name);
            }
            foreach (var imagePB in imagePBs)
            {
                toolTip1.SetToolTip(imagePB, imagePB.Name);
                toolTip1.SetToolTip(imagePB, imagePB.Name);
                toolTip1.SetToolTip(imagePB, imagePB.Name);
            }
        }

        private void miO_Click(object sender, System.EventArgs e)
        {
            // Display Picture
            var item1 = sender as MenuItem;

            // Get Requester's Name
            while (item1.Parent is MenuItem)
            {
                item1 = (MenuItem)item1.Parent;
            }
            var item2 = item1.Parent as ContextMenu;
            String name = item2.SourceControl.Name;
            name = name.Substring(name.LastIndexOf("\\")+1);
            //Console.WriteLine("Looking for: " + name);
            imageOpen(name);
        }

        private void doubleClick(object sender, System.EventArgs e)
        {
            // Display Picture
            var item1 = sender as PictureBox;
            String name = item1.Name;
            name = name.Substring(name.LastIndexOf("\\") + 1);
            //Console.WriteLine("Looking for: " + name);
            imageOpen(name);
        }

        private void imageOpen(String name)
        {
            int ii;

            ii = 0;
            foreach (CheckBox img in imageCBs)
            {
                //Console.WriteLine("Checking: "+ img.name);
                if (img.Name == name)
                {
                    displayImage(Convert.ToInt16(img.AccessibleName), img.AccessibleDescription, img.Name);
                    break;
                }
                ii++;
            }
        }

        private void displayImage(int imageIndex, String path, String name)
        {
            Button btn;
            int ii;
            Form iFo = new Form();
            int iFormsIndex = 0;
            Image img;
            List<String> kwl;
            bool match = false;
            Panel panI;
            PictureBox pb;
            TextBox tb;
            
            // Search for 'null' or image already displayed
            ii = 0;
            foreach (Form cf in iForms)
            {
                if (cf == null)
                {
                    if (!match)
                    {
                        match = true;
                        iFormsIndex = ii;
                    }
                }
                else if (cf.Name == name)
                {
                    // If already displayed, exit
                    return;
                }
                ii++;
            }

            iFo.Name = name;

            if (match)
            {
                // If 'null' found, reuse index
                iForms[iFormsIndex] = iFo;
            }
            else
            {
                // If 'null' not found, add new
                iForms.Add(iFo);
            }

            iFo.StartPosition = FormStartPosition.Manual;
            iFo.Location = new Point(0, 0);
            iFo.Height = Screen.PrimaryScreen.Bounds.Height - 50;
            iFo.Width = Screen.PrimaryScreen.Bounds.Width;
            iFo.FormClosing += new FormClosingEventHandler(ImageDisplay_FormClosing);

            KeywordDisplay kdI = new KeywordDisplay(imageIndex, new cbEvent(cbChangedFromChildI),
                new closedEvent(cbClosedFromChild), null, new keywordChangeEvent(kwChangedFromChild));
            kdI.setKeywordList(keyList);
            kwl = imgs.getImagesTableKeywords(imageIndex);
            kdI.setKeywordCheckedList(kwl);
            kdI.buildDisplay();
            panI = kdI.getPanel();
            kdI.TopLevel = false;
            kdI.FormBorderStyle = FormBorderStyle.None;
            //kdI.FormClosing += new FormClosingEventHandler(ImageDisplay_FormClosing);

            System.Windows.Forms.SplitContainer sc = new System.Windows.Forms.SplitContainer();
            sc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top 
                | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            sc.Location = new Point(0, 0);
            sc.Height = iFo.Height - 40;
            sc.Width = iFo.Width;
            sc.SplitterDistance = sc.Width - panI.Width - 4;
            sc.Panel1.AutoScroll = true;
            sc.Panel2.AutoScroll = true;
            sc.FixedPanel = FixedPanel.Panel2;

            pb = new PictureBox();
            pb.Location = new Point(0, 0);
            img = Image.FromFile(path);
            pb.Height = img.Height;
            pb.Width = img.Width;
            pb.Image = img;

            tb = new TextBox();
            tb.Location = new Point(0,0);
            tb.Text = name;
            tb.Enabled = false;
            sc.Panel2.Controls.Add(tb);

            btn = new Button();
            btn.Name = path;
            btn.Text = "Go to file";
            btn.Location = new Point(0,20);
            btn.Click += new System.EventHandler(goToFile_Click);

            sc.Panel2.Controls.Add(btn);

            sc.Panel1.AutoScroll = true;
            iFo.Controls.Clear();
            iFo.Controls.Add(sc);
            sc.Panel1.Controls.Add(pb);
            sc.Panel2.Controls.Add(panI);

            iFo.Show();
        }

        private void miC_Click(object sender, System.EventArgs e)
        {
            // Copy Image
            var item1 = sender as MenuItem;

            // Get Requester's Name
            while (item1.Parent is MenuItem)
            {
                item1 = (MenuItem)item1.Parent;
            }
            var item2 = item1.Parent as ContextMenu;
            String name = item2.SourceControl.Name;
            name = name.Substring(name.LastIndexOf("\\") + 1);
            //Console.WriteLine("Looking for: " + name);

            foreach (CheckBox img in imageCBs)
            {
                //Console.WriteLine("Checking: " + img.name);
                if (img.Name == name)
                {
                    Image image = Image.FromFile(img.Name);
                    Clipboard.SetImage(image);
                    break;
                }
            }
        }

        private void miCF_Click(object sender, System.EventArgs e)
        {
            // Copy Filename
            var item1 = sender as MenuItem;

            // Get Requester's Name
            while (item1.Parent is MenuItem)
            {
                item1 = (MenuItem)item1.Parent;
            }
            var item2 = item1.Parent as ContextMenu;
            String name = item2.SourceControl.Name;
            name = name.Substring(name.LastIndexOf("\\") + 1);
            //Console.WriteLine("Looking for: " + name);

            foreach (CheckBox img in imageCBs)
            {
                //Console.WriteLine("Checking: " + img.name);
                if (img.Name == name)
                {
                    Clipboard.SetText(img.Name);
                    break;
                }
            }
        }

        private void miSE_Click(object sender, System.EventArgs e)
        {
            int ii;
            DialogResult msgResult;
            String pw;
            String[] result = new String[2];

            // Copy Image
            var item1 = sender as MenuItem;

            // Get Requester's Name
            while (item1.Parent is MenuItem)
            {
                item1 = (MenuItem)item1.Parent;
            }
            var item2 = item1.Parent as ContextMenu;
            String name = item2.SourceControl.Name;
            name = name.Substring(name.LastIndexOf("\\") + 1);
            //Console.WriteLine("Looking for: " + name);

            ii = 0;
            foreach (CheckBox img in imageCBs)
            {
                //Console.WriteLine("Checking: " + img.name);
                if (img.Name == name)
                {
                    // If first time, get email information
                    pw = email.getPw();
                    if (pw == null)
                    {
                        msgResult = emailConfig.ShowDialog();
                        if (msgResult == System.Windows.Forms.DialogResult.OK) {
                            email.setFrom(emailConfig.getFrom());
                            pw = emailConfig.getPw();
                            email.setPw(pw);
                            email.setTo(emailConfig.getTo());
                        }
                    }
                    if (pw != null && pw.CompareTo("") == 0)
                    {
                        pw = null;
                        email.setPw(pw);
                    }

                    if (pw != null) {
                        msgResult = MessageBox.Show("Are you sure you want to send\n" + name + "\nin an email:\nFrom:" + email.getFrom() +
                        "\nTo:  " + email.getTo() + "?\n\n(Use Cancel to change password on next send)",
                        "Verify", MessageBoxButtons.YesNoCancel);
                        if (msgResult == DialogResult.Yes)
                        {
                            result = email.sendMessage(name, imgs.getImagesTablePath(imageIndexes[ii]));
                            if (result[0].CompareTo("F") == 0)
                            {
                                if (MessageBox.Show("Failed Message Send\n\nDo you want more info?", "Error", MessageBoxButtons.YesNo)
                                    == DialogResult.Yes)
                                {
                                    //Console.WriteLine("Failed Message Send:\n\n" + ee.ToString());
                                    MessageBox.Show("Failed Message Send:\n\n" + result[1]);
                                }
                            }
                        }
                        else if (msgResult == DialogResult.Cancel)
                        {
                            email.setPw(null);
                            emailConfig.setPw(null);
                        }
                    }

                    break;
                }
                ii++;
            }
        }

        private void miCP_Click(object sender, System.EventArgs e)
        {
            // Display Picture
            var item1 = sender as MenuItem;

            // Get Requester's Name
            while (item1.Parent is MenuItem)
            {
                item1 = (MenuItem)item1.Parent;
            }
            var item2 = item1.Parent as ContextMenu;
            String name = item2.SourceControl.Name;
            name = name.Substring(name.LastIndexOf("\\") + 1);
            //Console.WriteLine("Looking for: " + name);

            foreach (CheckBox img in imageCBs)
            {
                //Console.WriteLine("Checking: "+ img.name);
                if (img.Name == name)
                {
                    Clipboard.SetText(dataPath + "\\" + name);
                    break;
                }
            }
        }

        private void Images_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Write out updated Keyword file
            keyList.save();

            // Update data file
            imgs.save();
        }

        private void btnKeywords_Click(object sender, EventArgs e)
        {
            kd.clear();
            kd.setKeywordList(keyList);
            if (keywordsDisplayed)
            {
                btnKeywords.Text = "View Keywords";
                keywordsDisplayed = false;
                btnUndock.Visible = false;
                DataDisplay.Panel2.Controls.Clear();
                DataDisplay.SplitterDistance = DataDisplay.Width;
                displayImagesRefresh();
            }
            else
            {
                btnKeywords.Text = "Hide Keywords";
                keywordsDisplayed = true;
                displayKeywords(keywordsDisplayed);
            }
        }

        private void displayKeywords(bool docking)
        {
            kd.setDocking(docking);
            kd.buildDisplay();
            if (docking)
            {
                kd.TopLevel = false;
                kd.FormBorderStyle = FormBorderStyle.None;
                btnUndock.Visible = true;
                Panel pan = kd.getPanel();
                DataDisplay.SplitterDistance = DataDisplay.Width - pan.Width - 25;
                DataDisplay.Panel2.Controls.Add(pan);
            }
            else
            {
                btnUndock.Visible = false;
                kd.TopLevel = true;
                kd.FormBorderStyle = FormBorderStyle.Sizable;
                DataDisplay.SplitterDistance = DataDisplay.Width;
            }
            displayImagesRefresh();

            kd.Show();
            kd.Focus();
        }

        private void changeDockingFromChild()
        {
            //kd.Close();
            kd.clear();
            kd.setKeywordList(keyList);
            btnKeywords.Text = "Hide Keywords";
            keywordsDisplayed = true;
            displayKeywords(true);
        }

        private void cbChangedFromChild(int instance, List<String> kwds)
        {
            List<String> ch = imgs.getImagesTableLabels();
            DataRow dr;
            int ii;
            int inI;
            List<int> indexes = new List<int>();
            List<int> indexesActive = new List<int>();
            int kwI;
            int maxIndex = imgs.getMaxIndex();
            bool noMatch;
            List<String> tempSList;

            if (kwds.Count == 0)
            {
                displayImages(indexesFull);
            }
            else
            {
                inI = imgs.getImagesTableColumnIndex("Index");
                kwI = imgs.getImagesTableColumnIndex("Keywords");
                imgs.setImagesTableIterate();
                noMatch = true;
                ii = 0;
                for (ii = 0; ii <= maxIndex; ii++)
                {
                    noMatch = true;
                    dr = imgs.getImagesTableIterate();
                    tempSList = (List<String>)dr[kwI];
                    foreach (String kwd in kwds)
                    {
                        foreach (String kwd2 in tempSList)
                        {
                            if (kwd.CompareTo(kwd2) == 0)
                            {
                                noMatch = false;
                                break;
                            }
                        }
                    }

                    if (!noMatch)
                    {
                        int ind = Convert.ToInt16(dr[inI]);
                        indexesActive.Add(ind);
                    }
                }
                displayImages(indexesActive);
                //displayKeywords(kd.getDocking());
            }
        } // private void cbChangedFromChild(int instance, List<String> kwds)

        private void cbChangedFromChildI(int instance, List<String> kwds)
        {
            imgs.setKeywords(instance, kwds);
        } // private void cbChangedFromChildI(int instance, List<String> kwds)

        private void kwChangedFromChild()
        {
            keywordsChanged = true;
        }

        private void cbClosedFromChild()
        {
            DataDisplay.Panel2.Controls.Clear();
            btnKeywords.Text = "View Keywords";
            btnKeywords.Visible = true;
            keywordsDisplayed = false;
            displayKeywords(false);
        }

        private void btnUndock_Click(object sender, EventArgs e)
        {
            DataDisplay.Panel2.Controls.Clear();
            //kd.Close();
            kd.clear();
            kd.setKeywordList(keyList);
            btnKeywords.Visible = false;
            keywordsDisplayed = false;
            displayKeywords(false);
        }

        private void Images_ResizeEnd(object sender, EventArgs e)
        {
            if (imagesPerLine == 0)
            {
                displayImagesRefresh();
            }
        }

        public void ImageDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form tempF = (Form)sender;
            int ii = 0;

            foreach (Form ifo in iForms)
            {
                if (sender == ifo)
                {
                    ifo.Hide();
                    e.Cancel = true; // this cancels the close event.
                    iForms[ii]=null;
                    //kdi
                    break;
                }
                ii++;
            }
            
            if (keywordsChanged)
            {
                kd.refresh();
                keywordsChanged = false;
            }
        }

        private void goToFile_Click(object sender, EventArgs e)
        {
            Button btn;

            btn = (Button)sender;
            System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", btn.Name));
        }

        private void btnAddLink_Click(object sender, EventArgs e)
        {
            AddLink aL = new AddLink(dataPath);
            aL.ShowDialog();
        }

        private void btnSelectData_Click(object sender, EventArgs e)
        {
            bool configWrite = false;
            System.Drawing.Font currentFont;
            int dbSelectionNew;
            int dbSelectionOld;
            List<String> dbListOld = new List<String>();
            DialogResult result;
            RichTextBox rtb = new RichTextBox();

            rtb.Clear();
            rtb.Name = "rtbWelcome";
            rtb.Text = "Welcome to the" +
                "\nJulane and Suzanne Data Base Program" +
                "\n" +
                "\nPlease add another database (using 'Add Database') or " +
                "Select a database (with checkmark) and click 'Select'";
            rtb.SelectAll();
            rtb.SelectionAlignment = HorizontalAlignment.Center;
            rtb.Find("Julane and Suzanne Data Base Program", RichTextBoxFinds.MatchCase);
            currentFont = rtb.SelectionFont;
            rtb.SelectionAlignment = HorizontalAlignment.Center;
            rtb.SelectionFont = new Font(
                currentFont.FontFamily,
                14,
                FontStyle.Bold
            );
            rtb.DeselectAll();
            rtb.Width = 900;

            SelectDataBase sdb = new SelectDataBase(dbList);
            sdb.setSelectedIndex(dbSelection);
            sdb.Controls.Add(rtb);

            listCopy(dbList, dbListOld);
            dbSelectionOld = dbSelection;
            result = sdb.ShowDialog();
            dbSelectionNew = sdb.getSelectedIndex();
            if (!dbListOld.SequenceEqual(dbList))
            {
                configWrite = true;
            }

            if (result == DialogResult.OK)
            {
                if (dbSelectionOld != dbSelectionNew)
                {
                    configWrite = true;
                    dbSelection = dbSelectionNew;
                    dataPath = dbList[dbSelection];
                    Directory.SetCurrentDirectory(dataPath);
                    openNewData();
                }
            }

            if (configWrite)
            {
                mainConfigWrite(configMainName, configMainDir);
            }
        }

        private void listCopy<T>(List<T> source, List<T> dest)
        {
            int ct = source.Count;
            int ii;

            for (ii = 0; ii < ct; ii++)
            {
                dest.Add(source[ii]);
            }
        }
    }
}
