using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;              // To connect to MSSql Server
using System.Data.SqlClient;
namespace EMS_PSS
{
    public partial class Maintenance : System.Web.UI.Page
    {
        string securityLevel;
        string userName;
        string conString;
        string cName = "";
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            securityLevel = Session["securitylevel"].ToString();
            userName = Session["username"].ToString();
            conString = Session["conString"].ToString();
            displayCompany();
        }

        private void displayCompany()
        {
            SqlConnection conn = new SqlConnection(conString);
            string cmdString = "SELECT * FROM dbo.tb_Company";
            SqlCommand cmd = new SqlCommand(cmdString, conn);
            cmd.CommandType = CommandType.Text;
            dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (SqlException exp)
            {
                cmpyDisplayResultLabel.Text = "ERROR: " + exp.Message;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            if (dt.Rows.Count == 0) cmpyDisplayResultLabel.Text += "No Result to Display";
            else cmpyDisplayResultLabel.Text = "";

            cmpyDisplayGrid.DataSource = dt;
            cmpyDisplayGrid.DataBind();
        }

        protected void btnAddCmpy_Click(object sender, EventArgs e)
        {
            cName = tbxCmpyName.Text;     // username

            // add user
            SqlConnection conn = new SqlConnection(conString);
            int queryStatus = 0;
            string cmdstring = 
            "INSERT INTO tb_Company VALUES ('" + cName + "');";

            SqlCommand cmd = new SqlCommand(cmdstring, conn);
            cmd.CommandType = CommandType.Text;
            try
            {
                conn.Open();
                cmd.CommandText = cmdstring;
                queryStatus = cmd.ExecuteNonQuery();
            }
            catch (SqlException exp)
            {
                cmpyAdditionResultLabel.Text = "Duplicate Company Name; " + exp.Message;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            if (queryStatus > 0)
            {
                cmpyAdditionResultLabel.Text =
                    "Company [companyName:" + cName + "] Addition Successful";
                tbxCmpyName.Text = "";
                displayCompany();
            }
            else
            {
                cmpyAdditionResultLabel.Text += "Company Addition Failed";
            }
        }
    }
}