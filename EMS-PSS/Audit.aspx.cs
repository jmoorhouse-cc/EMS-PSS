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
    public partial class Audit : System.Web.UI.Page
    {
        string securityLevel;
        string userName;
        string conString;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            securityLevel = Session["securitylevel"].ToString();
            userName = Session["username"].ToString();
            conString = Session["conString"].ToString();
            searchFullResultGrid.Visible = false;

            SqlConnection conn = new SqlConnection(conString);
            string cmdstring = "";
            if (securityLevel == "1") cmdstring = "SELECT * FROM dbo.tb_Audit";

            SqlCommand cmd = new SqlCommand(cmdstring, conn);
            cmd.CommandType = CommandType.Text;
            dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            searchFullResultGrid.DataSource = dt;
            searchFullResultGrid.DataBind();

            if (dt.Rows.Count == 0)
            {
                selectResultLabel.Text = "No Result to Display";
                searchFullResultGrid.Visible = false;
            }
            else
            {
                selectResultLabel.Text = "";
                searchFullResultGrid.Visible = true;
            }
        }
    }
}