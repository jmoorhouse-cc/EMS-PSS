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
    public partial class Reports : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        string securityLevel;
        string userName;
        string conString;
        string company;

        protected void Page_Load(object sender, EventArgs e)
        {
            securityLevel = Session["securitylevel"].ToString();
            userName = Session["username"].ToString();
            conString = Session["conString"].ToString();
            rblReports.SelectedIndex = 0;
            if (securityLevel == "2")
            {
                rblReports.Items.Remove(rblReports.Items.FindByValue("payroll"));
                rblReports.Items.Remove(rblReports.Items.FindByValue("active"));
                rblReports.Items.Remove(rblReports.Items.FindByValue("inactive"));
            }
            else if (securityLevel == "1")
            {
                //rblReports.Items.Add(new ListItem("payroll"));
                //rblReports.Items.Add(new ListItem("active"));
                //rblReports.Items.Add(new ListItem("inactive"));
            }
            populateCompList();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
                
            if (rblReports.SelectedItem.Value == "seniority")
            {
                RunSeniorityReport();
            }
            else if (rblReports.SelectedItem.Value == "whw")
            {
                RunWhwReport();
            }
            else if (rblReports.SelectedItem.Value == "payroll")
            {
                RunPayrollReport();
            }
            else if (rblReports.SelectedItem.Value == "active")
            {
                RunActiveReport();
            }
            else  if (rblReports.SelectedItem.Value == "inactive")
            {
                RunInactiveReport();
            }
        }

        protected void populateCompList()
        {
            using (var con = new System.Data.SqlClient.SqlConnection(conString))
            {
                using (var cmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM tb_Company", con))
                {
                    try
                    {
                        con.Open();
                    }
                    catch (Exception e)
                    {
                        //lblErrorMsg.Text = e.Message;
                    }

                    var reader = cmd.ExecuteReader();

                    ftCompany.DataSource = reader;
                    ftCompany.DataValueField = "companyName";
                    ftCompany.DataTextField = "companyName";
                    ftCompany.DataBind();
                    cmd.Dispose();
                    reader.Close();
                }
            }
    }

        protected void RunSeniorityReport()
        {
            SqlConnection conn = new SqlConnection(conString);
            string cmdstring = "select * from dbo.SeniorityReport('" + company + "')";
            SqlCommand cmd = new SqlCommand(cmdstring, conn);
            cmd.CommandType = CommandType.Text;

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

        protected void RunWhwReport()
        {

        }

        protected void RunPayrollReport()
        {

        }

        protected void RunActiveReport()
        {

        }

        protected void RunInactiveReport()
        {

        }

        protected void ftCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            company = ftCompany.SelectedValue;
        }
    }
}