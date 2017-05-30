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
    public partial class EnterNameForPath : Form
    {
        public EnterNameForPath()
        {
            InitializeComponent();
        }

        public void setPath(String val)
        {
            tbPath.Text = val;
        }

        public String getName()
        {
            return tbName.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
