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
    public delegate void cbEvent(int inst, List<String> kwds);
    public delegate void closedEvent();
    public delegate void dockEvent();
    public delegate void keywordChangeEvent();

    public partial class KeywordDisplay : Form
    {
        List<dispData> dispD = new List<dispData>();
        bool docking = false;
        int instance;
        KeywordsClass keyList;
        List<String> keyListChecked = null;
        List<CheckBox> keyCBs = new List<CheckBox>();
        List<TextBox> keyTBs = new List<TextBox>();
        int tbIndex;
        cbEvent cbChanged;
        closedEvent cbClosed;
        dockEvent ChangeDocking;
        keywordChangeEvent keywordChange;


        public KeywordDisplay(int inst, cbEvent cbCh, closedEvent cbCl, dockEvent dE, keywordChangeEvent kC)
        {
            InitializeComponent();
            instance = inst;
            cbChanged = cbCh;
            cbClosed = cbCl;
            ChangeDocking = dE;
            keywordChange = kC;
        }

        public void clear()
        {
            panel1.Controls.Clear();
            dispD.Clear();
            if (keyListChecked != null)
            {
                keyListChecked.Clear();
            }
            keyListChecked = null;
            keyCBs.Clear();
            keyTBs.Clear();
        }

        public void setKeywordList(KeywordsClass kc)
        {
            keyList = kc;
        }

        public void setKeywordCheckedList(List<String> kl)
        {
            keyListChecked = kl;
        }

        private void KeywordDisplay_Load(object sender, EventArgs e)
        {
        }

        public void buildDisplay()
        {
            refresh();
        }

        public void refresh() {
            CheckBox cb;
            dispData dd;
            TextBox tb;
            int ii = 0;

            foreach (Control ctrl in keyCBs)
            {
                this.Controls.Remove(ctrl);
                ctrl.Dispose();
                ii++;
            }
            foreach (Control ctrl in keyTBs)
            {
                ctrl.Leave -= new System.EventHandler(tbChanged);
                this.Controls.Remove(ctrl);
                ctrl.Dispose();
                ii++;
            }

            keyCBs.Clear();
            keyTBs.Clear();
            tbIndex = 0;
            int zz = 0;
            foreach (String val in keyList)
            {
                tb = new TextBox();
                tb.Text = val;
                tb.Width = 200;
                tb.TextAlign = HorizontalAlignment.Center;

                tb.Name = val;
                tb.Location = new Point(20, (tb.Height) * tbIndex);
                keyTBs.Add(tb);
                panel1.Controls.Add(tb);
                tb.KeyDown += new System.Windows.Forms.KeyEventHandler(tbChanged);
                tb.Leave += new System.EventHandler(tbChanged);
                dd = new dispData();
                dd.name = val;
                dd.val = val;
                dispD.Add(dd);

                cb = new CheckBox();
                cb.Width = 15;
                cb.Name = val;
                cb.Location = new Point(5, (tb.Height) * tbIndex);

                if (keyListChecked != null)
                {
                    foreach (String kc in keyListChecked)
                    {
                        if (kc == val)
                        {
                            cb.Checked = true;
                        }
                    }
                }

                cb.CheckedChanged += new System.EventHandler(key_Checked);
                keyCBs.Add(cb);
                if (zz == 0)
                {
                    panel1.Controls.Add(cb);
                }

                tbIndex++;
            }

            // Add a blank one
            tb = new TextBox();
            tb.Text = "";
            tb.Width = 200;
            tb.TextAlign = HorizontalAlignment.Center;
            tb.Name = "Blank__" + tbIndex.ToString();
            tb.Location = new Point(20, (tb.Height) * tbIndex);
            tb.KeyDown += new System.Windows.Forms.KeyEventHandler(tbChanged);
            keyTBs.Add(tb);
            panel1.Controls.Add(tb);
            tb.Leave += new System.EventHandler(tbChanged);
            dd = new dispData();
            dd.name = tb.Name;
            dd.val = "";
            dispD.Add(dd);
            panel1.Height = (tb.Height + 4) * (tbIndex + 1) + 0;
            Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
            int titleHeight = screenRectangle.Top - this.Top;
            int borderHeight = this.Bottom - screenRectangle.Bottom;
            this.Height = titleHeight + borderHeight + panel1.Top + panel1.Height + 0;
            this.tbxFlag.Text = "";
        } // private void refresh()

        public Panel getPanel()
        {
            return this.panel1;
        }

        private class dispData
        {
            public String name {get; set; }
            public String val { get; set; }
        }

        private void tbChanged(object sender, System.EventArgs e)
        {
            processEvent((TextBox)sender);
        }

        private void tbChanged(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                processEvent((TextBox)sender);
            }
        }

        private void processEvent(TextBox tb)
        {
            int ii;
            bool needsRefresh = false;

            // Get Requester's Name
            String name = tb.Name;

            ii = 0;
            if (name.Length > 6 && name.Substring(0, 7) == "Blank__")
            {
                if (tb.Text != null && tb.Text != "")
                {
                    keyList.add(tb.Text);
                    needsRefresh = true;
                }
            }
            else
            {
                foreach (TextBox tbx in keyTBs)
                {
                    if (tbx.Name == name)
                    {
                        if (name == tb.Text)
                        {
                            break;
                        }
                        else
                        {
                            if (tb.Text == null || tb.Text == "")
                            {
                                keyList.remove(tb.Name);
                                needsRefresh = true;
                            }
                            else
                            {
                                keyList.set(ii, tb.Text);
                                needsRefresh = true;
                            }
                        }
                        break;
                    }
                    ii++;
                }
            }
            if (needsRefresh)
            {
                keywordChange();
                this.tbxFlag.Text = "Refresh";
            }
        }

        public bool getDocking()
        {
            return docking;
        }

        public void setDocking(bool dock)
        {
            docking = dock;
            if (docking)
            {
                btnDock.Visible = false;
                panel1.Location = new Point(0, 0);
            }
            else
            {
                btnDock.Visible = true;
                panel1.Location = new Point(0, 50);
            }
        }

        private void btnDock_Click(object sender, EventArgs e)
        {
            ChangeDocking();
        }

        private void tbxFlag_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Refresh")
            {
                refresh();
            }
            else if (tb.Text != "")
            {
                tbxFlag.Text = "";
            }
        }

        private void key_Checked(object sender, System.EventArgs e)
        {
            List<String> kbds = new List<String>();
            foreach (CheckBox cb in keyCBs)
            {
                if (cb.Checked)
                {
                    kbds.Add(cb.Name);
                }
            }
            cbChanged(instance, kbds);
        }

        private void KeywordDisplay_FormClosed(object sender, FormClosedEventArgs e)
        {
            cbClosed();
        }
    }
}
