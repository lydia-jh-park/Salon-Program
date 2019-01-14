using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tinastest4
{
    public partial class fmLogin : Form
    {
        public fmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection conn = DBProcess.DBConnection();

            string szUsername = tbUsername.Text;
            string szPassword = tbPassword.Text;
            string szSQL = string.Format("SELECT COUNT(*) FROM LOGIN WHERE USERNAME = '{0}' AND PASSWORD = '{1}'", szUsername, szPassword);

            SqlCommand cmd = new SqlCommand(szSQL, conn);

            conn.Open();
            int iRet = (int)cmd.ExecuteScalar();
            if (iRet > 0)
            {
                ((fmMain)(this.MdiParent)).tsVisibility(true);
                this.Close();
            }
            else
                MessageBox.Show("Please check your username and/or password.");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ((fmMain)(this.MdiParent)).Close();
        }
    }
}
