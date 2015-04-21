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

            ftHeader.Visible = false;
            ptHeader.Visible = false;
            slHeader.Visible = false;
            resultGrid1.Visible = false;
            resultGrid2.Visible = false;
            resultGrid3.Visible = false;
            resultLabel1.Visible = false;
            resultLabel2.Visible = false;
            resultLabel3.Visible = false;


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
            string cmdString2 = "";
            string cmdString3 = "";

            if (whichReport == "seniority")
            {
                resultGrid2.Visible = false;
                resultGrid3.Visible = false;
                resultLabel1.Visible = false;
                resultLabel2.Visible = false;
                resultLabel3.Visible = false;
                ftHeader.Visible = false;
                ptHeader.Visible = false;
                slHeader.Visible = false;
                cmdString = "select * from dbo.SeniorityReport('" + ftCompany.SelectedValue + "')";
                FillGrid(cmdString, ref resultGrid1);
            }
            else if (whichReport == "whw")
            {
                resultGrid2.Visible = true;
                resultGrid3.Visible = true;
                ftHeader.Visible = true;
                ptHeader.Visible = true;
                slHeader.Visible = true;
                cmdString = "select * from dbo.WeeklyHoursReport_FT('" + ftCompany.SelectedValue + "', '" + specifiedWeek.Text + "')";
                cmdString2 = "select * from dbo.WeeklyHoursReport_PT('" + ftCompany.SelectedValue + "', '" + specifiedWeek.Text + "')";
                cmdString3 = "select * from dbo.WeeklyHoursReport_SL('" + ftCompany.SelectedValue + "', '" + specifiedWeek.Text + "')";
                FillGrid(cmdString, ref resultGrid1);
                FillGrid(cmdString2, ref resultGrid2);
                FillGrid(cmdString3, ref resultGrid3);

            }
            else if (whichReport == "payroll")
            {
                resultGrid2.Visible = true;
                resultGrid3.Visible = true;
                ftHeader.Visible = true;
                ptHeader.Visible = true;
                slHeader.Visible = true;
                cmdString = "select * from dbo.PayrollReport_FT('" + ftCompany.SelectedValue + "', '" + specifiedWeek.Text + "')";
                cmdString2 = "select * from dbo.PayrollReport_PT('" + ftCompany.SelectedValue + "', '" + specifiedWeek.Text + "')";
                cmdString3 = "select * from dbo.PayrollReport_SL('" + ftCompany.SelectedValue + "', '" + specifiedWeek.Text + "')";
                FillGrid(cmdString, ref resultGrid1);
                FillGrid(cmdString2, ref resultGrid2);
                FillGrid(cmdString3, ref resultGrid3);
            }
            else if (whichReport == "active")
            {
                cmdString = "select * from dbo.SeniorityReport('" + ftCompany.SelectedValue + "')";
            }
            else if (whichReport == "inactive")
            {
                cmdString = "select * from dbo.SeniorityReport('" + ftCompany.SelectedValue + "')";
            }
            genDate.Text = "Date Generated: " + DateTime.Now.ToString();
            runUser.Text = "Run By: " + userName;
            genDate.Visible = true;
            runUser.Visible = true;
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

        protected void FillGrid(string cmdString, ref GridView dataGrid)
        {
            dt = new DataTable();
            SqlConnection conn = new SqlConnection(conString);
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
            
            dataGrid.DataSource = dt;
            dataGrid.DataBind();

            if (dt.Rows.Count < 1)
            {
                if (dataGrid.ID == "resultGrid1")
                {
                    resultLabel1.Visible = true;
                    resultLabel1.Text = "No Result to Display";
                    dataGrid.Visible = false;
                    ftHeader.Visible = false;
                }
                else if (dataGrid.ID == "resultGrid2")
                {
                    resultLabel2.Visible = true;
                    resultLabel2.Text = "No Result to Display";
                    dataGrid.Visible = false;
                }
                else if (dataGrid.ID == "resultGrid3")
                {
                    resultLabel3.Visible = true;
                    resultLabel3.Text = "No Result to Display";
                    dataGrid.Visible = false;
                }
            }
            else
            {
                if (dataGrid.ID == "resultGrid1")
                {
                    resultLabel1.Visible = false;
                    dataGrid.Visible = true;
                }
                else if (dataGrid.ID == "resultGrid2")
                {
                    resultLabel2.Visible = false;
                    dataGrid.Visible = true;
                }
                else if (dataGrid.ID == "resultGrid3")
                {
                    resultLabel3.Visible = false;
                    dataGrid.Visible = true;
                }
            }
        }
    }
}