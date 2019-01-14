using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tinastest4
{
    public partial class fmMain : Form
    {
        public fmMain()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        public void tsVisibility(bool setting)
        {
            this.tsButtons.Visible = setting;
        }

        private void fmMain_Load(object sender, EventArgs e)
        {
            tsVisibility(false);
            fmLogin login = new fmLogin();
            login.MdiParent = this;
            login.StartPosition = FormStartPosition.Manual;
            login.Location = new Point((this.ClientSize.Width - login.Width) / 2,
                (this.ClientSize.Height - login.Height) / 2);
            login.Show();
        }

        private void tsbtnCust_Click(object sender, EventArgs e)
        {
            fmCustomer cust = new fmCustomer();
            cust.MdiParent = this;
            cust.Show();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
