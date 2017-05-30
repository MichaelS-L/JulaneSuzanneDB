using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shell32;
using IWshRuntimeLibrary;

namespace JulaneSuzanneDB
{
    public partial class AddLink : Form
    {
        String pathData;
        String pathNew = null;

        public AddLink(String p)
        {
            InitializeComponent();
            pathData = p;
        }

        private void btnFindLink_Click(object sender, EventArgs e)
        {
            String name;

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && fbd.SelectedPath != null)
                {
                    pathNew = fbd.SelectedPath;
                    tbPath.Text = pathNew;
                    name = pathNew.Substring(pathNew.LastIndexOf(@"\") + 1);
                    tbName.Text = name;
                }
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            createShortcut(tbName.Text, tbPath.Text);
        }

        // I got this from the internet
        private void createShortcut(String name, String pathNew)
        {
            IWshShell_Class wsh = new IWshShell_Class();
            IWshRuntimeLibrary.IWshShortcut shortcut = wsh.CreateShortcut(
                pathData + "\\" + name + ".lnk") as IWshRuntimeLibrary.IWshShortcut;
            shortcut.TargetPath = pathNew;
            // not sure about what this is for
            shortcut.WindowStyle = 1;
            shortcut.Save();
        }
    }
}
