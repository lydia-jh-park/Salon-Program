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
    public partial class fmCustomer : Form
    {
        public fmCustomer()
        {
            InitializeComponent();
        }

        private void RefreshLists()
        {
            DBProcess.FillCustomers(dgvCustomers);
        }
        private void fmCustomer_Load(object sender, EventArgs e)
        {
            RefreshLists();
        }

        private class CustInfo
        {
            string[] CustArray = new string[18];
        }

        private void dgaCust_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCustomers.CurrentCell == null) return;

            tbClientID.Text = BSLibs.CheckString(dgvCustomers.CurrentRow.Cells["ClientID"].Value);
            tbCustNo.Text = BSLibs.CheckString(dgvCustomers.CurrentRow.Cells["CustNo"].Value);
            tbFirstName.Text = dgvCustomers.CurrentRow.Cells["FirstName"].Value.ToString();
            tbLastName.Text = dgvCustomers.CurrentRow.Cells["LastName"].Value.ToString();
            tbNickname.Text = dgvCustomers.CurrentRow.Cells["Nickname"].Value.ToString();
            tbBPoint.Text = dgvCustomers.CurrentRow.Cells["BPOINT"].Value.ToString();
            tbLColor.Text = dgvCustomers.CurrentRow.Cells["LCOLORS"].Value.ToString();
            dtpLVDate.Text = dgvCustomers.CurrentRow.Cells["LVDATE"].Value.ToString();
            tbPhone.Text = dgvCustomers.CurrentRow.Cells["PhoneNo"].Value.ToString();
            cbCarrier.Text = dgvCustomers.CurrentRow.Cells["Carrier"].Value.ToString();
            cbActive.Text = dgvCustomers.CurrentRow.Cells["Active"].Value.ToString();
            dtpDOB.Text = dgvCustomers.CurrentRow.Cells["DOB"].Value.ToString();
            tbEmail.Text = dgvCustomers.CurrentRow.Cells["Email"].Value.ToString();
            tbAddr.Text = dgvCustomers.CurrentRow.Cells["Address"].Value.ToString();
            cbCity.Text = dgvCustomers.CurrentRow.Cells["City"].Value.ToString();
            dtpRegDate.Text = dgvCustomers.CurrentRow.Cells["RegDate"].Value.ToString();
            tbRemarks.Text = dgvCustomers.CurrentRow.Cells["Remarks"].Value.ToString();
        }

        private void btnClientID_Click(object sender, EventArgs e)
        {
            tbClientID.Text = "";
        }

        private void ButtonManipulate(object sender, EventArgs e)
        {
            Button btnCurrent = (Button)sender;
            btnNew.Enabled = false;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;

            if (btnCurrent.Text.ToUpper().Equals("SAVE"))
            {
                btnNew.Enabled = true;
                btnModify.Enabled = true;
                btnDelete.Enabled = true;

                string szCustNo = tbCustNo.Text;
                if (szCustNo.Length <= 0)
                {
                    MessageBox.Show("Please fill in CustNo");
                    return;
                }
                string szClientID = tbClientID.Text;
                if (szClientID.Length != 10)
                {
                    MessageBox.Show("Please fill in ClientID");
                    return;
                }
                string szFName = tbFirstName.Text;
                string szLName = tbLastName.Text;
                string szNickname = tbNickname.Text;
                string szPhoneNo = tbPhone.Text;
                string szCarrier = cbCarrier.Text;
                string szActive = cbActive.Text;
                DateTime szDOB = dtpDOB.Value;
                string szEmail = tbEmail.Text;
                string szAddr = tbAddr.Text;
                string szCity = cbCity.Text;
                string szState = tbState.Text;
                decimal decBPoint;
                if (tbBPoint.Text == "")
                    decBPoint = 0.00m;
                else
                    decBPoint = Convert.ToDecimal(tbBPoint.Text);
                DateTime dtRegDate = dtpRegDate.Value;
                DateTime dtLVDate = dtpLVDate.Value;
                string szLColors = tbLColor.Text;
                string szRemarks = tbRemarks.Text;

                string szSQL = null;
                btnCurrent.Text = btnCurrent.Tag.ToString();

                if (btnCurrent == btnNew)
                {
                    szSQL = string.Format("INSERT INTO CUSTINFO VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}')",
                        szCustNo, szClientID, szFName, szLName, szNickname, szPhoneNo, szCarrier, szActive, szDOB, szEmail, szAddr, szCity, szState, decBPoint, dtRegDate, dtLVDate, szLColors, szRemarks);
                }
                else if (btnCurrent == btnModify)
                {
                    szSQL = string.Format("UPDATE CUSTINFO SET CLIENTID = '{0}', FIRSTNAME ='{1}', LASTNAME='{2}', NICKNAME='{3}', PHONENO ='{4}', CARRIER ='{5}', ACTIVE='{6}', BPOINT = '{17}', DOB ='{7}', EMAIL='{8}', ADDR='{9}', CITY='{10}', STATE ='{11}', REGDATE='{12}', LVDATE='{13}', LCOLORS='{14}', REMARKS ='{15}' WHERE CUSTNO = '{16}'",
                        szClientID, szFName, szLName, szNickname, szPhoneNo, szCarrier, szActive, szDOB, szEmail, szAddr, szCity, szState, dtRegDate, dtLVDate, szLColors, szRemarks, szCustNo, decBPoint);
                }

                DBProcess.SaveToDB(szSQL);
                RefreshLists();
            }
            else if (btnCurrent.Text.ToUpper().Equals("NEW"))
            {
                btnNew.Enabled = true;
                dtpRegDate.Text = DateTime.Today.ToString();
                BSLibs.SetEnable(tlpInfo, true, false);
                btnCurrent.Text = "Save";
            }
            else if (btnCurrent.Text.ToUpper().Equals("MODIFY"))
            {
                if (dgvCustomers.CurrentRow.Selected == false)
                {
                    btnNew.Enabled = true;
                    btnNew.Text = btnNew.Tag.ToString();
                    btnModify.Enabled = true;
                    btnModify.Text = btnModify.Tag.ToString();
                    btnDelete.Enabled = true;
                    BSLibs.SetEnable(tlpInfo, false, false);
                }

                btnModify.Enabled = true;
                BSLibs.SetEnable(tlpInfo, true, true);
                btnCurrent.Text = "Save";
            }
            else if (btnCurrent.Text.ToUpper().Equals("CANCEL"))
            {
                btnNew.Enabled = true;
                btnNew.Text = btnNew.Tag.ToString();
                btnModify.Enabled = true;
                btnModify.Text = btnModify.Tag.ToString();
                btnDelete.Enabled = true;
                BSLibs.SetEnable(tlpInfo, false, false);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            BSLibs.SetEnable(tlpInfo, true, true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string szSQL = null;
            int CustNo = Convert.ToInt32(tbCustNo.Text);
            string szName = tbFirstName.Text + tbLastName.Text;
            string Message = string.Format("Are you sure you want to delete {0} ?", szName);


            DialogResult dlgResult = MessageBox.Show(Message, "Info", MessageBoxButtons.YesNo);

            if (dlgResult == DialogResult.Yes)
            {
                SqlConnection conn = DBProcess.DBConnection();

                szSQL = string.Format("DELETE FROM CUSTINFO WHERE CUSTNO = {0}", CustNo);
                DBProcess.SaveToDB(szSQL);
                RefreshLists();
            }
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            RefreshLists();
        }

        private void tbFNameSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string szSQL = string.Format("SELECT * FROM CUSTINFO WHERE FIRSTNAME = '{0}'", tbFNameSearch.Text);
                DBProcess.FillCustomers(dgvCustomers, szSQL);
            }
        }

        private void tbLNameSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string szSQL = string.Format("SELECT * FROM CUSTINFO WHERE LASTNAME = '{0}'", tbLNameSearch.Text);
                DBProcess.FillCustomers(dgvCustomers, szSQL);
            }
        }

        private void tbNNameSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string szSQL = string.Format("SELECT * FROM CUSTINFO WHERE NICKNAME = '{0}'", tbNNameSearch.Text);
                DBProcess.FillCustomers(dgvCustomers, szSQL);
            }
        }

        private void tbCIDSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string szSQL = string.Format("SELECT * FROM CUSTINFO WHERE CLIENTID = '{0}'", tbCIDSearch.Text);
                DBProcess.FillCustomers(dgvCustomers, szSQL);
            }
        }

        private void tbPhoneSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string szSQL = string.Format("SELECT * FROM CUSTINFO WHERE PHONENO = '{0}'", tbPhoneSearch.Text);
                DBProcess.FillCustomers(dgvCustomers, szSQL);
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            if (tbClientID.Text.Length <= 0)
            {
                MessageBox.Show("Please select somebody.");
                return;
            }

            decimal decBPoint;
            if (tbBPoint.Text == "")
                decBPoint = 0.00m;
            else
                decBPoint = Convert.ToDecimal(tbBPoint.Text);

            if (btnAdmin.Text.ToUpper().Equals("ADMIN"))
            {
                tbBPoint.Enabled = true;
                btnAdmin.Text = "Save";
            }
            else if (btnAdmin.Text.ToUpper().Equals("SAVE"))
            {
                tbBPoint.Enabled = false;

                string szSQL = string.Format("UPDATE CUSTINFO SET BPOINT = '{0}' WHERE CUSTNO = '{1}'",
                        decBPoint, tbCustNo.Text);
                DBProcess.SaveToDB(szSQL);

                btnAdmin.Text = "Admin";

                MessageBox.Show("Modification successful!");

                RefreshLists();
            }
        }
    }
}
