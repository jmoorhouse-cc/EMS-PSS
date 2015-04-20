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

            if (!IsPostBack)
            {
                rblReports.SelectedIndex = 0;
                populateCompList();
            }
            
            if (securityLevel == "2")
            {
                rblReports.Items.Remove(rblReports.Items.FindByValue("payroll"));
                rblReports.Items.Remove(rblReports.Items.FindByValue("active"));
                rblReports.Items.Remove(rblReports.Items.FindByValue("inactive"));
            }

        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
                
            if (rblReports.SelectedItem.Value == "seniority")
            {
                RunReport("seniority");
            }
            else if (rblReports.SelectedItem.Value == "whw")
            {
                RunReport("whw");
            }
            else if (rblReports.SelectedItem.Value == "payroll")
            {
                RunReport("payroll");
            }
            else if (rblReports.SelectedItem.Value == "active")
            {
                RunReport("active");
            }
            else  if (rblReports.SelectedItem.Value == "inactive")
            {
                RunReport("inactive");
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

        protected void RunReport(string whichReport)
        {
            SqlConnection conn = new SqlConnection(conString);
            string cmdString = "";

            if (whichReport == "seniority")
            {
                cmdString = "select * from dbo.SeniorityReport('" + ftCompany.SelectedValue + "')";
            }
            else if (whichReport == "whw")
            {
                cmdString = "select * from dbo.WeeklyHoursReport_FT('" + ftCompany.SelectedValue + "', '" + specifiedWeek.Text + "')";
            }
            else if (whichReport == "payroll")
            {
                cmdString = "select * from dbo.SeniorityReport('" + ftCompany.SelectedValue + "')";
            }
            else if (whichReport == "active")
            {
                cmdString = "select * from dbo.SeniorityReport('" + ftCompany.SelectedValue + "')";
            }
            else if (whichReport == "inactive")
            {
                cmdString = "select * from dbo.SeniorityReport('" + ftCompany.SelectedValue + "')";
            }

            SqlCommand cmd = new SqlCommand(cmdString, conn);
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

        protected void rblReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblReports.SelectedValue == "whw" || rblReports.SelectedValue == "payroll")
            {
                specifiedWeek.Visible = true;
            }
            else
            {
                specifiedWeek.Visible = false;
            }
        }
    }
}