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
    public partial class EmailConfigForm : Form
    {
        String from = "";
        String pw = "";
        String to = "";

        public EmailConfigForm()
        {
            InitializeComponent();
        }

        private void EmailConfigForm_Load(object sender, EventArgs e)
        {
            tbFrom.Text = from;
            tbPW.Text = pw;
            tbTo.Text = to;
        }

        private void EmailConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            checkBox1.Checked = false;
            tbPW.UseSystemPasswordChar = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            from = tbFrom.Text;
            pw = tbPW.Text;
            to = tbTo.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public String getFrom()
        {
            return from;
        }

        public void setFrom(String val)
        {
            from = val;
        }

        public String getPw()
        {
            return pw;
        }

        public void setPw(String val)
        {
            pw = val;
        }

        public String getTo()
        {
            return to;
        }

        public void setTo(String val)
        {
            to = val;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                tbPW.UseSystemPasswordChar = false;
            }
            else
            {
                tbPW.UseSystemPasswordChar = true;
            }
        }
    }
}
