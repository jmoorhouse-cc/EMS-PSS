using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;              // To connect to MSSql Server
using System.Data.SqlClient;
using EMS;
using AllEmployees;
using TheCompany;

namespace EMS_PSS
{
    public partial class Modify : System.Web.UI.Page
    {
        //Decleration of variables
        string securityLevel;
        string userName;
        string conString;
        string type = "";

        string sin;
        string fname;
        string lname;
        string company;

        DataTable dt, dt2;
        protected void Page_Load(object sender, EventArgs e)
        {
            securityLevel = Session["securitylevel"].ToString();
            userName = Session["username"].ToString();
            conString = Session["conString"].ToString();
           if(!IsPostBack)
           {
               sin = "";
               fname = "";
               lname = "";
               company = "";
           }

        }

        protected void searchSubmit_Click(object sender, EventArgs e)
        {
            fulltimeInput.Visible = false;
            parttimeInput.Visible = false;
            seasonalInput.Visible = false;
            contractInput.Visible = false;
            btnModify.Visible = false;
            string fn = fnameSearch.Text;
            string ln = lnameSearch.Text;
            string sins = sinSearch.Text;

            SqlConnection conn = new SqlConnection(conString);
            string cmdstring = "";
            if (securityLevel == "1") cmdstring = "SELECT * FROM dbo.A_SearchEmp(@fName, @lName, @sin)";
            else if (securityLevel == "2") cmdstring = "SELECT * FROM dbo.G_SearchEmp(@fName, @lName, @sin)";

            SqlCommand cmd = new SqlCommand(cmdstring, conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@fName", SqlDbType.VarChar).Value = fn;
            cmd.Parameters.Add("@lName", SqlDbType.VarChar).Value = ln;
            cmd.Parameters.Add("@sin", SqlDbType.VarChar).Value = sins;
            dt = new DataTable();
            ///Trys to open a connection to the data base
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
            ///Makes the search results visible to the user
            searchResultGrid.Visible = true;
            searchFullResultGrid.Visible = false;
            searchResultGrid.DataSource = dt;
            searchResultGrid.DataBind();

            if (dt.Rows.Count == 0) selectResultLabel.Text = "No Result to Display";
            else selectResultLabel.Text = "";
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            bool isAllValid = true;
            ParttimeEmployee pt = new ParttimeEmployee();
            FulltimeEmployee ft = new FulltimeEmployee();
            SeasonalEmployee sl = new SeasonalEmployee();
            ContractEmployee ct = new ContractEmployee();
            List<string> normalUpdates = new List<string>();
            string updateString = "UPDATE tb_Emp SET";

            string updateFt = "";
            string updatePt = "";
            string updateCt = "";
            string updateSl = "";


            string endUpdate = " WHERE socialInsNumber=" + Session["sin"] + " and firstName='" + Session["fname"] +"' and lastName='" + Session["lname"] + "' and companyName='" + Session["company"] + "';";
          //string updateSpecificString = "UPDATE " + 

            //for fulltime
            if (ftfName.Text != "")
            {
                if (!ft.SetFirstName(ftfName.Text))
                {
                    ftfNameError.Text += "<b>First Name</b> can only contain the following characters: [A-Za-z. -]\n";
                    isAllValid = false;
                }
                normalUpdates.Add("firstName='" + ftfName.Text + "'");
            }
            if (ftlName.Text != "")
            {
                if (!ft.SetLastName(ftlName.Text))
                {
                    ftlNameError.Text += "<b>Last Name</b> can only contain the following characters: [A-Za-z. -]\n";
                    isAllValid = false;
                }
                normalUpdates.Add("lastName='" + ftlName.Text + "'");
            }
            if (ftSin.Text != "")
            {
                if (!ft.SetSIN(ftSin.Text.Replace(" ", "")))
                {
                    ftSinError.Text += "<b>SIN</b> should be 9-digit number";
                    isAllValid = false;
                }
                normalUpdates.Add("socialInsNumber=" + ftSin.Text);
            }
            if (ftDOB.Text != "")
            {
                if (!ft.SetDOB(ftDOB.Text.Replace(" ", "")))
                {
                    ftDOBError.Text += "<b>Date Of Hire</b> should have valid date format";
                    isAllValid = false;
                }
                normalUpdates.Add("dateOfBirth'" + ftDOB.Text + "'");
            }
            if (ftDateHire.Text != "")
            {
                if (!ft.SetDateOfHire(ftDateHire.Text.Replace(" ", "")))
                {
                    ftDateHireError.Text += "<b>Date Of Birth</b> should have valid date format";
                    isAllValid = false;
                }
            }
            if (ftDateTerm.Text != "")
            {
                if (!ft.SetDateOfTermination(ftDateTerm.Text.Replace(" ", "")) && ftDateTerm.Text != "")
                {
                    ftDateTermError.Text += "<b>Date Of Termination</b> should have valid date format";
                    isAllValid = false;
                }
            }
            if (ftSalary.Text != "")
            {
                if (!ft.SetSalary(ftSalary.Text.Replace(" ", "")) && ftSalary.Text != "")
                {
                    ftSalaryError.Text += "<b>Salary</b> should be a number higher than 0";
                    isAllValid = false;
                }
            }

            ////for part time
            //if (!pt.SetFirstName(ptfName.Text) || ptfName.Text == "")
            //{
            //    ptfNameError.Text += "<b>First Name</b> can only contain the following characters: [A-Za-z. -]\n";
            //    isAllValid = false;
            //}
            //if (!pt.SetLastName(ptlName.Text) || ptlName.Text == "")
            //{
            //    ptlNameError.Text += "<b>Last Name</b> can only contain the following characters: [A-Za-z. -]\n";
            //    isAllValid = false;
            //}
            //if (!pt.SetSIN(ptSin.Text.Replace(" ", "")))
            //{
            //    ptSinError.Text += "<b>SIN</b> should be 9-digit number";
            //    isAllValid = false;
            //}
            //if (!pt.SetDOB(ptDOB.Text.Replace(" ", "")))
            //{
            //    ptDOBError.Text += "<b>Date Of Birth</b> should have valid date format";
            //    isAllValid = false;
            //}
            //if (!pt.SetDateOfHire(ptDateHire.Text.Replace(" ", "")))
            //{
            //    ptDateHireError.Text += "<b>Date Of Hire</b> should have valid date format";
            //    isAllValid = false;
            //}
            //if (!pt.SetDateOfTermination(ptDateTerm.Text.Replace(" ", "")) && ptDateTerm.Text != "")
            //{
            //    ptDateTermError.Text += "<b>Date Of Termination</b> should have valid date format";
            //    isAllValid = false;
            //}
            //if (!pt.SetHourlyRate(ptWage.Text.Replace(" ", "")) && ptWage.Text != "")
            //{
            //    ptWageError.Text += "<b>Salary</b> should be a number higher than 0";
            //    isAllValid = false;
            //}

            ////for seasonal
            //if (!sl.SetFirstName(slfName.Text) || slfName.Text == "")
            //{
            //    slfNameError.Text += "<b>First Name</b> can only contain the following characters: [A-Za-z. -]\n";
            //    isAllValid = false;
            //}
            //if (!sl.SetLastName(sllName.Text) || sllName.Text == "")
            //{
            //    sllNameError.Text += "<b>Last Name</b> can only contain the following characters: [A-Za-z. -]\n";
            //    isAllValid = false;
            //}
            //if (!sl.SetSIN(slSin.Text.Replace(" ", "")))
            //{
            //    slSinError.Text += "<b>SIN</b> should be 9-digit number";
            //    isAllValid = false;
            //}
            //if (!sl.SetDOB(slDOB.Text.Replace(" ", "")))
            //{
            //    slDOBError.Text += "<b>Date Of Birth</b> should have valid date format";
            //    isAllValid = false;
            //}
            //if (!sl.SetSeason(slSeason.Text.Replace(" ", "")) && slSeason.Text != "")
            //{
            //    slSeasonError.Text += "<b>Season</b> must be valid. It should not be possible to get this error. Look at you, you little hacker";
            //    isAllValid = false;
            //}
            //if (!sl.SetPiecePay(slPcPay.Text.Replace(" ", "")) && slPcPay.Text != "")
            //{
            //    slPcPayError.Text += "<b>Salary</b> should be a number higher than 0";
            //    isAllValid = false;
            //}

            ////for contract
            //if (!ct.SetLastName(ctName.Text) || ctName.Text == "")
            //{
            //    ctNameError.Text += "<b>Company Name</b> can only contain the following characters: [A-Za-z. -]\n";
            //    isAllValid = false;
            //}
            //if (!ct.SetSIN(ctBin.Text.Replace(" ", "")))
            //{
            //    ctBinError.Text += "<b>BIN</b> should be 9-digit number";
            //    isAllValid = false;
            //}
            //if (!ct.SetDOB(ctDOI.Text.Replace(" ", "")))
            //{
            //    ctDOIError.Text += "<b>Date Of Incorporation</b> should have valid date format";
            //    isAllValid = false;
            //}
            //if (!ct.SetContractStartDate(ctStart.Text.Replace(" ", "")) && ctStart.Text != "")
            //{
            //    ctStartError.Text += "<b>Contract Start Date</b> must be valid. It should not be possible to get this error. Look at you, you little hacker";
            //    isAllValid = false;
            //}
            //if (!ct.SetContractEndDate(ctEnd.Text.Replace(" ", "")))
            //{
            //    ctEndError.Text += "<b>Contract End Date</b> should have valid date format";
            //    isAllValid = false;
            //}
            //if (!ct.SetFixedContractAmt(ctAmt.Text.Replace(" ", "")) && ctAmt.Text != "")
            //{
            //    ctAmtError.Text += "<b>Contract Amount</b> should be a number higher than 0";
            //    isAllValid = false;
            //}

            bool first = true;
            foreach (string s in normalUpdates)
            {
                if (first)
                {
                    updateString += " " + s;
                    first = false;
                }
                else
                {
                    updateString += ", " + s;
                }
            }

            updateString += " " + endUpdate;
            try
            {
                using (SqlConnection connect = new SqlConnection(conString))
                {
                    connect.Open();
                    using (SqlCommand changeCmd = new SqlCommand())
                    {
                        changeCmd.Connection = connect;
                        changeCmd.CommandType = CommandType.Text;
                        changeCmd.CommandText = updateString;
                        changeCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exce)
            {

            }
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                // Retrieve the CommandArgument property
                int index = Convert.ToInt32(e.CommandArgument); // or convert to other datatype
                GridViewRow row = searchResultGrid.Rows[index];
                sin = row.Cells[1].Text;
                fname = row.Cells[2].Text;
                lname = row.Cells[3].Text;
                company = row.Cells[4].Text;
                type = row.Cells[5].Text;

                Session["sin"] = sin;
                Session["fname"] = fname;
                Session["lname"] = lname;
                Session["company"] = company;

                SqlConnection conn = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand(getCmdString(type), conn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@fn", SqlDbType.VarChar).Value = fname;
                cmd.Parameters.Add("@ln", SqlDbType.VarChar).Value = lname;
                cmd.Parameters.Add("@sin", SqlDbType.VarChar).Value = sin;
                cmd.Parameters.Add("@cn", SqlDbType.VarChar).Value = company;
                dt2 = new DataTable();

                //ModifyStuff.SelectCommand = cmd.ToString();
                //ModifyStuff.UpdateCommand = 
              //  ModifyStuff.ConnectionString = conn.ToString();

                
                
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
                if(type == "FT")
                {
                    fulltimeInput.Visible = true;

                    try
                    {
                        using (var con = new System.Data.SqlClient.SqlConnection(conString))
                        {
                            using (var newCmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM tb_Company", con))
                            {
                                try
                                {
                                    con.Open();
                                }
                                catch (Exception ex)
                                {
                                    //lblErrorMsg.Text = e.Message;
                                }

                                var reader = newCmd.ExecuteReader();

                                ftCompany.DataSource = reader;
                                ftCompany.DataValueField = "companyName";
                                ftCompany.DataTextField = "companyName";
                                ftCompany.DataBind();
                                newCmd.Dispose();
                                reader.Close();
                            }
                        }
                    }
                    catch (Exception exc)
                    {

                    }
                }
                else if(type == "PT")
                {
                    parttimeInput.Visible = true;

                    try
                    {
                        using (var con = new System.Data.SqlClient.SqlConnection(conString))
                        {
                            using (var newCmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM tb_Company", con))
                            {
                                try
                                {
                                    con.Open();
                                }
                                catch (Exception ex)
                                {
                                    //lblErrorMsg.Text = e.Message;
                                }

                                var reader = newCmd.ExecuteReader();

                                ptCompany.DataSource = reader;
                                ptCompany.DataValueField = "companyName";
                                ptCompany.DataTextField = "companyName";
                                ptCompany.DataBind();
                                newCmd.Dispose();
                                reader.Close();
                            }
                        }
                    }
                    catch (Exception exc)
                    {

                    }
                }
                else if(type == "CT")
                {
                    contractInput.Visible = true;

                    try
                    {
                        using (var con = new System.Data.SqlClient.SqlConnection(conString))
                        {
                            using (var newCmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM tb_Company", con))
                            {
                                try
                                {
                                    con.Open();
                                }
                                catch (Exception ex)
                                {
                                    //lblErrorMsg.Text = e.Message;
                                }

                                var reader = newCmd.ExecuteReader();

                                ctCompany.DataSource = reader;
                                ctCompany.DataValueField = "companyName";
                                ctCompany.DataTextField = "companyName";
                                ctCompany.DataBind();
                                newCmd.Dispose();
                                reader.Close();
                            }
                        }
                    }
                    catch (Exception exc)
                    {

                    }
                }
                else if(type == "SL")
                {
                    seasonalInput.Visible = true;

                    try
                    {
                        using (var con = new System.Data.SqlClient.SqlConnection(conString))
                        {
                            using (var newCmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM tb_Company", con))
                            {
                                try
                                {
                                    con.Open();
                                }
                                catch (Exception ex)
                                {
                                    //lblErrorMsg.Text = e.Message;
                                }

                                var reader = newCmd.ExecuteReader();

                                slCompany.DataSource = reader;
                                slCompany.DataValueField = "companyName";
                                slCompany.DataTextField = "companyName";
                                slCompany.DataBind();
                                newCmd.Dispose();
                                reader.Close();
                            }
                        }
                    }
                    catch (Exception exc)
                    {

                    }
                }
                btnModify.Visible = true;
            }
        }



        private string getCmdString(string type)
        {
            string cmdstring = "";
            switch (type)
            {
                ///case for Full time if user is admin
                case "FT":
                    if (securityLevel == "1")
                    {
                        cmdstring = "SELECT * FROM dbo.A_DisplayFTEmp(@fn, @ln, @sin, @cn)";
                    }
                    ///else if user is general
                    else if (securityLevel == "2")
                    {
                        cmdstring = "SELECT * FROM dbo.G_DisplayFTEmp(@fn, @ln, @sin, @cn)";
                    }
                    break;
                ///case for Partime if user is admin
                case "PT":
                    if (securityLevel == "1")
                    {
                        cmdstring = "SELECT * FROM dbo.A_DisplayPTEmp(@fn, @ln, @sin, @cn)";
                    }
                    ///else user is general
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
                ///Case for seasonal employee if user is admin
                case "SL":
                    if (securityLevel == "1")
                    {
                        cmdstring = "SELECT * FROM dbo.A_DisplaySLEmp(@fn, @ln, @sin, @cn)";
                    }
                    ///else if user is general
                    else if (securityLevel == "2")
                    {
                        cmdstring = "SELECT * FROM dbo.G_DisplaySLPTEmp(@fn, @ln, @sin, @cn)";
                    }
                    break;
            }
            return cmdstring;
        }

        protected void searchFullResultGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
    }

}