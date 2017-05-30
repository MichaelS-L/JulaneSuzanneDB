using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JulaneSuzanneDB
{
    public partial class SelectDataBase : Form
    {
        List<String> dbList;
        Panel panDBs;
        int selectedIndex = -1;

        public SelectDataBase(List<String> db)
        {
            InitializeComponent();
            dbList = db;
        }

        private void refresh()
        {
            int ii;
            int height = 0;

            if (dbList.Count > 0)
            {
                if (panDBs != null)
                {
                    this.Controls.Remove(panDBs);
                }
                panDBs = new Panel();
                panDBs.Location = new Point(0,findHeight(this));
                panDBs.Width = 900;
                panDBs.Name = "PanDB";

                /*
                Label lbl1 = new Label();
                lbl1.Text = "Name";
                lbl1.Location = new Point(20, 0);
                lbl1.Height = 15;
                lbl1.Width = 100;
                lbl1.Controls.Add(new Label() { Height = 1, Dock = DockStyle.Bottom, BackColor = Color.Black });
                panDBs.Controls.Add(lbl1);

                Label lbl2 = new Label();
                lbl2.Text = "Path";
                lbl2.Location = new Point(125, 0);
                lbl2.Height = 15;
                lbl2.Width = 880;
                lbl2.Controls.Add(new Label() { Height = 1, Dock = DockStyle.Bottom, BackColor = Color.Black });
                panDBs.Controls.Add(lbl2);
                */

                height = findHeight(panDBs);
                ii = 0;
                foreach (String db in dbList)
                {
                    CheckBox cb = new CheckBox();
                    cb.Name = ii.ToString();
                    cb.Width = 15;
                    cb.Location = new Point(0, height + (21 * ii));

                    if (ii == selectedIndex)
                    {
                        cb.Checked = true;
                    }

                    cb.CheckedChanged += new System.EventHandler(this.db_Checked);
                    panDBs.Controls.Add(cb);

                    TextBox tbP = new TextBox();
                    tbP.Text = db;
                    tbP.Width = 784;
                    tbP.Name = ii.ToString() +"_P";
                    tbP.Location = new Point(16, height + (21 * ii));
                    panDBs.Controls.Add(tbP);


                    Button btn = new Button();
                    btn.Name = ii.ToString();
                    btn.Text = "Delete";
                    btn.Width = 100;
                    btn.Name = ii.ToString();
                    btn.Location = new Point(800, height + (21 * ii));
                    btn.Click += new System.EventHandler(this.delete_Click);
                    panDBs.Controls.Add(btn);

                    ii++;
                }
                this.Controls.Add(panDBs);
            }
            this.tbxFlag.Text = "";
            btnSelect.Focus();
        } // private void refresh()

        public void setDBlist(List<String> val)
        {
            dbList = val;
        }

        public List<String> getDBlist()
        {
            return dbList;
        }

        public void setSelectedIndex(int sel)
        {
            selectedIndex = sel;
        }

        public int getSelectedIndex()
        {
            return selectedIndex;
        }

        public String getSelectedPath()
        {
            String result;

            if (selectedIndex == -1)
            {
                result = "";
            }
            else
            {
                result = dbList[selectedIndex];
            }

            return result;
        }

        private void btnAddDB_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.Cancel;

            using (var fbd = new FolderBrowserDialog())
            {
                result = fbd.ShowDialog();

                if (result == DialogResult.OK && fbd.SelectedPath != null)
                {
                    dbList.Add(fbd.SelectedPath);
                    refresh();
                }
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            int ii;
            Button thisBtn = sender as Button;
            String name = thisBtn.Name;
            Type ctrlType;

            ii = 0;
            foreach (Control ctrl in panDBs.Controls)
            {
                ctrlType = ctrl.GetType();
                if (ctrl.GetType() == typeof(Button))
                {
                    if (ctrl.Name == name)
                    {
                        if (selectedIndex == ii)
                        {
                            selectedIndex = -1;
                        }
                        dbList.RemoveAt(ii);
                        this.Close();
                        break;
                    }
                    ii++;
                }
            }
        }

        private void SelectDataBase_Shown(object sender, EventArgs e)
        {
            refresh();
        }

        private void tbxFlag_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.CompareTo("Refresh") == 0)
            {
                refresh();
            }
        }

        private int findHeight(Control cont)
        {
            int height = 0;
            int top = -1;

            foreach (Control ctrl in cont.Controls)
            {
                if (ctrl.Left < 21)
                {
                    if ((ctrl.Top + ctrl.Height) > (top + height))
                    {
                        top = ctrl.Top;
                        height = ctrl.Top + ctrl.Height;
                    }
                }
            }
            height += 1;
            return height;
        }

        private void db_Checked(object sender, System.EventArgs e)
        {
            // Display Picture
            CheckBox cb;
            CheckBox thisCb = sender as CheckBox;
            String name = thisCb.Name;
            Type ctrlType;

            if (thisCb.Checked)
            {
                foreach (Control ctrl in panDBs.Controls)
                {
                    ctrlType = ctrl.GetType();
                    if (ctrl.GetType() == typeof(CheckBox))
                    {
                        cb = (CheckBox)ctrl;
                        cb.CheckedChanged -= new System.EventHandler(this.db_Checked);
                        if (cb.Name == name)
                        {
                            selectedIndex = Convert.ToInt16(name);
                        }
                        else
                        {
                            cb.Checked = false;
                        }
                        cb.CheckedChanged += new System.EventHandler(this.db_Checked);
                    }
                }
            }
            else
            {
                selectedIndex = -1;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (selectedIndex == -1)
            {
                MessageBox.Show("Need to make a selection", "Alert");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
