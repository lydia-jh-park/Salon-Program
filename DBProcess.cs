using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tinastest4
{
    static class DBProcess
    {
        public static SqlConnection DBConnection()
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                string szConStr = null;
                using (StreamReader sr = File.OpenText("DBInfo.txt"))
                {
                    string s = String.Empty;
                    string szTag = "ConStr:";

                    while ((s = sr.ReadLine()) != null)
                    {
                        if (s.Contains(szTag))
                            szConStr = s.Substring(szTag.Length, s.Length - szTag.Length);
                    }
                }

                conn = new SqlConnection(szConStr);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("There was a problem with the database.\n\n" + ex.Message);
            }
            catch (IOException ex)
            {
                MessageBox.Show("There was a problem with the file.\n\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n\n" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return conn;
        }

        public static void FillCustomers(DataGridView dgaCurrent, string szSQL= "SELECT * FROM CUSTINFO")
        {
            SqlConnection conn = DBConnection();
            //string szSQL = "SELECT * FROM CUSTINFO";

            SqlDataAdapter sda = new SqlDataAdapter(szSQL, conn);
            DataTable dt = new DataTable();

            sda.Fill(dt);

            dgaCurrent.DataSource = dt;
        }

        public static void SaveToDB(string szSQL)
        {
            SqlConnection conn = DBConnection();

            try
            {
                SqlCommand cmd = new SqlCommand(szSQL, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An error occurred with the database.\n\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n\n" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            MessageBox.Show("Database has been processed successfully.", "INFO");
        }
    }
}
