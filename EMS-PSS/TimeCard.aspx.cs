using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;              // To connect to MSSql Server
using System.Data.SqlClient;
using Supporting;

namespace EMS_PSS
{
    public partial class TimeCard : System.Web.UI.Page
    {
        string securityLevel;
        string userName;
        string conString;
        DataTable dt, dt2;
        string selectedEmpType;
        protected void Page_Load(object sender, EventArgs e)
        {
            securityLevel = Session["securitylevel"].ToString();
            userName = Session["username"].ToString();
            conString = Session["conString"].ToString();
            searchFullResultGrid.Visible = false;
            hideTimeCardField();
        }

        protected void searchSubmit_Click(object sender, EventArgs e)
        {
            searchFullResultGrid.Visible = false;
            hideTimeCardField();
            string fn = fnameSearch.Text;
            string ln = lnameSearch.Text;
            string sin = sinSearch.Text;

            SqlConnection conn = new SqlConnection(conString);
            string cmdstring = "";
            if (securityLevel == "1") cmdstring = "SELECT * FROM dbo.A_SearchEmp(@fName, @lName, @sin)";
            else if (securityLevel == "2") cmdstring = "SELECT * FROM dbo.G_SearchEmp(@fName, @lName, @sin)";

            SqlCommand cmd = new SqlCommand(cmdstring, conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@fName", SqlDbType.VarChar).Value = fn;
            cmd.Parameters.Add("@lName", SqlDbType.VarChar).Value = ln;
            cmd.Parameters.Add("@sin", SqlDbType.VarChar).Value = sin;
            dt = new DataTable();

            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            searchResultGrid.Visible = true;
            searchFullResultGrid.Visible = false;
            searchResultGrid.DataSource = dt;
            searchResultGrid.DataBind();

            if (dt.Rows.Count == 0)
            {
                selectResultLabel.Text = "No Result to Display";
                hideTimeCardField();
            }
            else selectResultLabel.Text = "";
        }

        protected void tcInsert_Click(object sender, EventArgs e)
        {
            DateTime temp = new DateTime();
            TimeCardManager tcm;
            try
            {
                temp = Convert.ToDateTime(tcWeekDate.Text);
                tcm = new TimeCardManager(temp);
            }
            catch
            {
                errorMsg.Text = "Invalid date or format";
            }
            //
        }
        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                // Retrieve the CommandArgument property
                int index = Convert.ToInt32(e.CommandArgument); // or convert to other datatype
                GridViewRow row = searchResultGrid.Rows[index];
                string sin = row.Cells[1].Text;
                string fname = row.Cells[2].Text;
                string lname = row.Cells[3].Text;
                string company = row.Cells[4].Text;
                selectedEmpType = row.Cells[5].Text;
                
                SqlConnection conn = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand(getCmdString(selectedEmpType), conn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@fn", SqlDbType.VarChar).Value = fname;
                cmd.Parameters.Add("@ln", SqlDbType.VarChar).Value = lname;
                cmd.Parameters.Add("@sin", SqlDbType.VarChar).Value = sin;
                cmd.Parameters.Add("@cn", SqlDbType.VarChar).Value = company;
                dt2 = new DataTable();

                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt2);
                }
                catch (SqlException exp)
                {
                    selectResultLabel.Text = "ERROR: " + exp.Message;
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    conn.Close();
                }

                if (dt2.Rows.Count == 0) selectResultLabel.Text += "No Result to Display";
                else selectResultLabel.Text = "";

                searchFullResultGrid.DataSource = dt2;
                searchFullResultGrid.DataBind();
                searchResultGrid.Visible = false;
                searchFullResultGrid.Visible = true;
                showTimeCardField();

            }
        }
        private string getCmdString(string type)
        {
            string cmdstring = "";
            switch (type)
            {
                case "FT":
                    if (securityLevel == "1")
                    {
                        cmdstring = "SELECT * FROM dbo.A_DisplayFTEmp(@fn, @ln, @sin, @cn)";
                    }
                    else if (securityLevel == "2")
                    {
                        cmdstring = "SELECT * FROM dbo.G_DisplayFTEmp(@fn, @ln, @sin, @cn)";
                    }
                    break;
                case "PT":
                    if (securityLevel == "1")
                    {
                        cmdstring = "SELECT * FROM dbo.A_DisplayPTEmp(@fn, @ln, @sin, @cn)";
                    }
                    else if (securityLevel == "2")
                    {
                        cmdstring = "SELECT * FROM dbo.G_DisplayPTEmp(@fn, @ln, @sin, @cn)";
                    }
                    break;
                case "CT":
                    if (securityLevel == "1")
                    {
                        cmdstring = "SELECT * FROM dbo.A_DisplayCTEmp(@fn, @ln, @sin, @cn)";
                    }
                    break;
                case "SL":
                    if (securityLevel == "1")
                    {
                        cmdstring = "SELECT * FROM dbo.A_DisplaySLEmp(@fn, @ln, @sin, @cn)";
                    }
                    else if (securityLevel == "2")
                    {
                        cmdstring = "SELECT * FROM dbo.G_DisplaySLPTEmp(@fn, @ln, @sin, @cn)";
                    }
                    break;
            }
            return cmdstring;
        }
        private void showTimeCardField()
        {
            tcDateInputTable.Visible = true;
            hourInputTable.Visible = true;
            if (selectedEmpType.ToUpper() == "SL") pieceInputTable.Visible = true;
            else pieceInputTable.Visible = false;
        }
        private void hideTimeCardField()
        {
            tcDateInputTable.Visible = false;
            hourInputTable.Visible = false;
            pieceInputTable.Visible = false;
        }
    }
}