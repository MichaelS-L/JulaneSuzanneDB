using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Data;
using Shell32;
using System.Drawing.Imaging;

namespace JulaneSuzanneDB
{
    class ImgClass {
        bool dataChanged = false;
        String dataName;
        DataTable imagesTable;
        int imagesTableIterate;
        int maxIndex = 0;
        String tempS;
        XmlDocument xmlDoc = new XmlDocument();

        public ImgClass(String progNameReceived)
        {
            dataName = ".data_" + progNameReceived + ".xml";
        }

        public List<int> buildData(String path, String folderName = null)
        {
            //bool dtNotNew = true;
            int ii;
            List<String> imageNames = new List<String>();
            String imgName;
            List<int> indexes = new List<int>();
            List<String> imagePaths;
            List<String> kwds = new List<String>();
            String name;
            List<String> tempLs;

            XmlElement xmlElem;
            XmlElement xmlElem2;
            XmlElement xmlElem3;
            XmlNode xmlNod;
            String activeFolder;
            String savedFolder;

            // Create a table and sort
            activeFolder = Directory.GetCurrentDirectory();
            savedFolder = activeFolder;
            if (imagesTable == null)
            {
                //dtNotNew = false;
                imagesTable = new DataTable();
                imagesTable.Columns.Add("Index");
                imagesTable.Columns.Add("Name");
                imagesTable.Columns.Add("Path");
                imagesTable.Columns.Add("Keywords", typeof(List<String>));

                if (xmlDoc.DocumentElement != null)
                {
                    foreach (XmlNode xN1 in xmlDoc.DocumentElement.ChildNodes)
                    {
                        DataRow dr = imagesTable.NewRow();
                        dr[0] = xN1.Attributes["Index"].Value;
                        dr[1] = xN1.Attributes["Name"].Value;
                        dr[2] = xN1.Attributes["Path"].Value;

                        // Get Keywords
                        kwds = new List<String>();
                        foreach (XmlNode xN2 in xN1)
                        {
                            if (xN2.Name == "keys")
                            {
                                foreach (XmlNode xN3 in xN2)
                                {
                                    kwds.Add(xN3.InnerText);
                                }
                            }
                        }
                        dr[3] = kwds;

                        imagesTable.Rows.Add(dr);
                    }

                    // sort by Name column:
                    imagesTable.DefaultView.Sort = "Name ASC";
                    imagesTable = imagesTable.DefaultView.ToTable();
                }
            }
            else
            {
                // Set new folder
                activeFolder = path;
                Directory.SetCurrentDirectory(activeFolder);
            }

            List<String> imageExtensions = SupportedImageDecodersFilter();
            imageExtensions.Add("*.lnk");
            imagePaths = new List<String>();
            foreach (String tS in imageExtensions)
            {
                tempLs = new List<String>(Directory.EnumerateFiles(".", tS));
                imagePaths.AddRange(tempLs);
            }
            

            if (xmlDoc.DocumentElement != null)
            {
                // Remove unwanted files
                ii = 0;
                foreach (String imagePath in imagePaths)
                {
                    imgName = Path.GetFileName(imagePath);
                    if ("." == imgName.Substring(0, 1))
                    {
                        indexes.Add(ii);
                    }
                    else
                    {
                        // Check if Image type

                        // Check if read from file
                        foreach (DataRow dr in imagesTable.Rows)
                        {
                            if (String.Compare(imgName, (String)dr[1]) == 0)
                            {
                                indexes.Add(ii);
                                break;
                            }
                            else if (String.Compare(imgName, (String)dr[1]) < 0)
                            {
                                break;
                            }
                        }
                    }

                    ii++;
                }
                indexes.Reverse();
                foreach (int ind in indexes)
                {
                    imagePaths.RemoveAt(ind);
                }
                indexes.Clear();

                xmlNod = xmlDoc.DocumentElement;
                DateTime fileCreatedDate;
                String year;
                foreach (var imagePath in imagePaths)
                {
                    name = Path.GetFileName(imagePath);
                    if (imagePath.Substring(imagePath.Length - 4, 4).CompareTo(".lnk") == 0)
                    {
                        String newFolderName = name.Substring(0, name.Length - 4);
                        name = GetShortcutTargetFile(activeFolder + "\\" + name);
                        kwds.Add(newFolderName);
                        buildData(name, newFolderName);
                    }
                    else
                    {
                        fileCreatedDate = File.GetCreationTime(name);
                        year = fileCreatedDate.Year.ToString();
                        xmlElem = xmlDoc.CreateElement("Image");
                        xmlElem2 = xmlDoc.CreateElement("keys");
                        if (folderName != null)
                        {
                            xmlElem3 = xmlDoc.CreateElement("key");
                            xmlElem3.InnerText = folderName;
                            xmlElem2.AppendChild(xmlElem3);
                        }
                        xmlElem3 = xmlDoc.CreateElement("key");
                        xmlElem3.InnerText = year;
                        xmlElem2.AppendChild(xmlElem3);
                        xmlElem.SetAttribute("Index", (++maxIndex).ToString());
                        xmlElem.SetAttribute("Name", name);
                        tempS = activeFolder + "\\" + name;
                        xmlElem.SetAttribute("Path", tempS);
                        xmlElem.AppendChild(xmlElem2);
                        xmlNod.AppendChild(xmlElem);
                        dataChanged = true;
                    }
                }
                imagePaths = null;

                // Clean and Add all xmlDoc files to table then sort
                imagesTable.Clear();
                foreach (XmlNode xN1 in xmlDoc.DocumentElement.ChildNodes)
                {
                    DataRow dr = imagesTable.NewRow();
                    dr[0] = xN1.Attributes["Index"].Value;
                    dr[1] = xN1.Attributes["Name"].Value;
                    dr[2] = xN1.Attributes["Path"].Value;

                    // Get Keywords
                    kwds = new List<String>();
                    foreach (XmlNode xN2 in xN1)
                    {
                        if (xN2.Name == "keys")
                        {
                            foreach (XmlNode xN3 in xN2)
                            {
                                kwds.Add(xN3.InnerText);
                            }
                        }
                    }
                    dr[3] = kwds;
                    imagesTable.Rows.Add(dr);
                }

                // sort by Name column:
                imagesTable.DefaultView.Sort = "Name ASC";
                imagesTable = imagesTable.DefaultView.ToTable();
                /*
                foreach (DataRow drI in imagesTable)
                {
                    Console.WriteLine(drI[0] + " : " + drI[1] + " : " + drI[2]);
                }
                */
            }

            //if (dtNotNew)
            if (folderName != null)
            {
                Directory.SetCurrentDirectory(savedFolder);
            }
            return indexes;
        } // public List<int> buildData(String path)

        public void setImagesTableIterate(int val = 0)
        {
            imagesTableIterate = val;
        }

        public DataRow getImagesTableIterate()
        {
            return imagesTable.Rows[imagesTableIterate++];
        }

        public List<String> getImagesTableLabels()
        {
            List<String> temp = new List<String>();

            foreach (DataColumn column in imagesTable.Columns)
            {
                temp.Add(column.ColumnName);
            }
            return temp;
        }

        public int getImagesTableColumnIndex(String val)
        {
            int ii;

            List<String> ch = getImagesTableLabels();
            ii = 0;
            foreach (String cv in ch)
            {
                if (cv == val)
                {
                    break;
                }
                ii++;
            }
            return ii;
        }

        public int getImagesTableIndex(int ii)
        {
            return Convert.ToInt16(imagesTable.Rows[ii][0]);
        }

        public String getImagesTableName(int ii)
        {
            return (String)imagesTable.Rows[ii][1];
        }

        public String getImagesTablePath(int ii)
        {
            return (String)imagesTable.Rows[ii][2];
        }

        public DataRow getImagesTableRow(int ii)
        {
            return imagesTable.Rows[ii];
        }

        public List<int> getImagesTableIndexes()
        {
            List<int> inds = new List<int>();

            foreach (DataRow dr in imagesTable.Rows)
            {
                inds.Add(Convert.ToInt16(dr[0]));
            }
            return inds;
        }

        public List<String> getImagesTableKeywords(int index)
        {
            int ii;
            String indexS = index.ToString();
            List<String> kwds = null;

            ii = 0;
            foreach (DataRow dr in imagesTable.Rows)
            {
                if ((String)dr[0] == indexS)
                {
                    kwds = (List<String>)dr[3];
                    break;
                }
                ii++;
            }

            return kwds;
        }

        public int getMaxIndex()
        {
            return maxIndex;
        } // public int getMaxIndex()

        public void retrieveFileData(KeywordsClass kwds) {
            int ii;
            String tempS;
            XmlNode xmlNod;

            if (File.Exists(dataName))
            {
                // Read Data file
                xmlDoc.Load(dataName);
                ii = 0;
                foreach (XmlNode xN1 in xmlDoc.DocumentElement.ChildNodes)
                {
                    if ((String)xN1.Attributes["Index"].Value != ii.ToString())
                    {
                        xN1.Attributes["Index"].Value = ii.ToString();
                        dataChanged = true;
                    }

                    // If missing, add keywords to list
                    foreach (XmlNode xN2 in xN1.ChildNodes)
                    {
                        if (xN2.Name == "keys")
                        {
                            foreach (XmlNode xN3 in xN2.ChildNodes)
                            {
                                int qq = 0;
                                tempS = xN3.InnerText;
                                kwds.add(tempS);
                                if (qq == 1)
                                {
                                    kwds.remove(tempS);
                                }

                            }
                        }
                    }

                    ii++;
                }
                maxIndex = ii - 1;
            }
            else
            {
                // Build XML Data
                xmlNod = xmlDoc.CreateElement("Images");
                xmlDoc.AppendChild(xmlNod);

                maxIndex = -1;
                dataChanged = true;
                save();
            }
        } // public void retrieveFileData(KeywordsClass kwds)

        public void save()
        {
            if (dataChanged)
            {
                // Update data file
                xmlDoc.Save(dataName);
                dataChanged = false;
            }
        } // public void save()

        public void setKeywords(int inst, List<String> kwds)
        {
            DataRow dr;
            XmlElement elem;
            int ii;
            String indexS;
            String instS = inst.ToString();
            int index = this.getImagesTableColumnIndex("Index");
            bool noMatch;
            XmlNode nodeImage;
            XmlNode nodeKeys;
            XmlNodeList nodes;
            List<XmlNode> removeList = new List<XmlNode>();

            this.setImagesTableIterate();
            for (ii = 0; ii <= maxIndex; ii++)
            {
                dr = this.getImagesTableIterate();
                if ((String)dr[index] == instS)
                {
                    dr[3] = kwds;
                }
            }

            // Now update the XML
            indexS = "/Images/Image[@Index='" + instS + "']";
            nodes = xmlDoc.SelectNodes(indexS);  // Should ONLY be one
            nodeImage = nodes[0];

            //indexS = "/Images/Image[@Index='" + instS + "']";
            nodes = nodeImage.ChildNodes;
            nodeKeys = nodes[0];
            nodes = nodeKeys.ChildNodes;

            // First Check for Add
            foreach (String kwd in kwds)
            {
                noMatch = true;
                foreach (XmlNode nod in nodes)
                {
                    if (nod.InnerText == kwd)
                    {
                        noMatch = false;
                        break;
                    }
                }

                if (noMatch)
                {
                    elem = xmlDoc.CreateElement("key");
                    elem.InnerText = kwd;
                    nodeKeys.AppendChild(elem);
                    dataChanged = true;
                }
            }

            // Now Check for Remove
            foreach (XmlNode nod in nodes)
            {
                noMatch = true;
                foreach (String kwd in kwds)
                {
                    if (nod.InnerText == kwd)
                    {
                        noMatch = false;
                        break;
                    }
                }

                if (noMatch)
                {
                    // Build RemoveList so not to mess up Node Iteration
                    removeList.Add(nod);
                }
            }

            foreach (XmlNode rI in removeList)
            {
                nodeKeys.RemoveChild(rI);
                dataChanged = true;
            }
        }

        // The following is from the Internet
        public static string GetShortcutTargetFile(string shortcutFilename)
        {
            string pathOnly = System.IO.Path.GetDirectoryName(shortcutFilename);
            string filenameOnly = System.IO.Path.GetFileName(shortcutFilename);

            Shell shell = new Shell();
            Folder folder = shell.NameSpace(pathOnly);
            FolderItem folderItem = folder.ParseName(filenameOnly);
            if (folderItem != null)
            {
                Shell32.ShellLinkObject link = (Shell32.ShellLinkObject)folderItem.GetLink;
                return link.Path;
            }

            return string.Empty;
        } // public static string GetShortcutTargetFile(string shortcutFilename)

        // The following is from the Internet but modified to return only extensions
        //MSL - Edit Start
        //private static string SupportedImageDecodersFilter()
        private static List<String> SupportedImageDecodersFilter()
        //MSL - Edit End
        {
            String[] mslSA; //MSL - Added
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();

            string allExtensions = encoders
                .Select(enc => enc.FilenameExtension)
                .Aggregate((current, next) => current + ";" + next)
                .ToLowerInvariant();
            mslSA = allExtensions.Split(';'); //MSL - Added
            var sb = new StringBuilder(500)
                .AppendFormat("Image files  ({0})|{1}", allExtensions.Replace(";", ", "),
                              allExtensions);
            foreach (ImageCodecInfo encoder in encoders)
            {
                string ext = encoder.FilenameExtension.ToLowerInvariant();
                // ext = "*.bmp;*.dib;*.rle"           descr = BMP
                // ext = "*.jpg;*.jpeg;*.jpe;*.jfif"   descr = JPEG
                // ext = "*.gif"                       descr = GIF
                // ext = "*.tif;*.tiff"                descr = TIFF
                // ext = "*.png"                       descr = PNG

                string caption;
                switch (encoder.FormatDescription)
                {
                    case "BMP":
                        caption = "Windows Bitmap";
                        break;
                    case "JPEG":
                        caption = "JPEG file";
                        break;
                    case "GIF":
                        caption = "Graphics Interchange Format";
                        break;
                    case "TIFF":
                        caption = "Tagged Image File Format";
                        break;
                    case "PNG":
                        caption = "Portable Network Graphics";
                        break;
                    default:
                        caption = encoder.FormatDescription;
                        break;
                }
                sb.AppendFormat("|{0}  ({1})|{2}", caption, ext.Replace(";", ", "), ext);
            }
            //MSL - Remove:return sb.ToString();
            return mslSA.ToList<String>(); //MSL - Added
        } // private static List<String> SupportedImageDecodersFilter()
    }
}
