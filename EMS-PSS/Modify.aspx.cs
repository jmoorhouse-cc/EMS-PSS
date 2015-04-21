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
        Supporting.Audit sup = new Supporting.Audit();
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
            if (!IsPostBack && Session["type"] != null)
            {
                if (Session["type"].ToString() != "FT")
                {
                    ftfName.Text = "";
                    ftlName.Text = "";
                    ftSin.Text = "";
                    ftDOB.Text = "";
                    ftDateHire.Text = "";
                    ftDateTerm.Text = "";
                    ftSalary.Text = "";
                }

                if (Session["type"].ToString() != "SL")
                {
                    slfName.Text = "";
                    sllName.Text = "";
                    slSin.Text = "";
                    slDOB.Text = "";
                    sldateStart.Text = "";
                    slYear.Text = "";
                    slPcPay.Text = "";
                }

                if (Session["type"].ToString() != "PT")
                {
                    ptfName.Text = "";
                    ptlName.Text = "";
                    ptSin.Text = "";
                    ptDOB.Text = "";
                    ptDateHire.Text = "";
                    ptDateTerm.Text = "";
                    ptWage.Text = "";
                }

                if (Session["type"].ToString() != "CT")
                {
                    ctName.Text = "";
                    ctBin.Text = "";
                    ctDOI.Text = "";
                    ctStart.Text = "";
                    ctEnd.Text = "";
                    ctAmt.Text = "";
                }
            }
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
            List<string> specificUpdates = new List<string>();
            string updateString = "UPDATE tb_Emp SET";
            string updateSpecific = "UPDATE tb_";
            int empID = 0;
            string empType = Session["type"].ToString();
            if (empType == "FT")
            {
                updateSpecific += "Ft";
            }
            else if (empType == "PT")
            {
                updateSpecific += "Pt";
            }
            else if (empType == "SL")
            {
                updateSpecific += "Sl";
            }
            else if (empType == "CT")
            {
                updateSpecific += "Ct";
            }

            updateSpecific += "Emp SET ";

            string endUpdate = " OUTPUT INSERTED.empID WHERE socialInsNumber=" + Session["sin"] + " and firstName='" + Session["fname"] + "' and lastName='" + Session["lname"] + "' and companyName='" + Session["company"] + "';";
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
                    specificUpdates.Add("dateOfHire='" + ftDateHire.Text + "'");
            }
            if (ftDateTerm.Text != "")
            {
                if (!ft.SetDateOfTermination(ftDateTerm.Text.Replace(" ", "")) && ftDateTerm.Text != "")
                {
                    ftDateTermError.Text += "<b>Date Of Termination</b> should have valid date format";
                    isAllValid = false;
                }
                specificUpdates.Add("dateOfTermination='" + ftDateTerm.Text + "'");
            }
            if (ftSalary.Text != "")
            {
                if (!ft.SetSalary(ftSalary.Text.Replace(" ", "")) && ftSalary.Text != "")
                {
                    ftSalaryError.Text += "<b>Salary</b> should be a number higher than 0";
                    isAllValid = false;
                }
                specificUpdates.Add("salary=" + ftSalary.Text + "");
            }

            //for part time
            if (ptfName.Text != "")
            {
                if (!pt.SetFirstName(ptfName.Text))
                {
                    ptfNameError.Text += "<b>First Name</b> can only contain the following characters: [A-Za-z. -]\n";
                    isAllValid = false;
                }
                normalUpdates.Add("firstName='" + ptfName.Text + "'");
            }
            if (ptlName.Text != "")
            {
                if (!pt.SetLastName(ptlName.Text) || ptlName.Text == "")
                {
                    ptlNameError.Text += "<b>Last Name</b> can only contain the following characters: [A-Za-z. -]\n";
                    isAllValid = false;
                }
                normalUpdates.Add("lastName='" + ptlName.Text + "'");
            }
            if(ptSin.Text != "")
            {
                if (!pt.SetSIN(ptSin.Text.Replace(" ", "")))
                {
                    ptSinError.Text += "<b>SIN</b> should be 9-digit number";
                    isAllValid = false;
                }
                normalUpdates.Add("socialInsNumber=" + ptSin.Text);
            }
            if (ptDOB.Text != "")
            {
                if (!pt.SetDOB(ptDOB.Text.Replace(" ", "")))
                {
                    ptDOBError.Text += "<b>Date Of Birth</b> should have valid date format";
                    isAllValid = false;
                }
                normalUpdates.Add("dateOfBirth='" + ptDOB.Text + "'");
            }
            if (ptDateHire.Text != "")
            {
                if (!pt.SetDateOfHire(ptDateHire.Text.Replace(" ", "")))
                {
                    ptDateHireError.Text += "<b>Date Of Hire</b> should have valid date format";
                    isAllValid = false;
                }
                specificUpdates.Add("dateOfHire='" + ptDateHire.Text + "'");
            }
            if (ptDateTerm.Text != "")
            {
                if (!pt.SetDateOfTermination(ptDateTerm.Text.Replace(" ", "")) && ptDateTerm.Text != "")
                {
                    ptDateTermError.Text += "<b>Date Of Termination</b> should have valid date format";
                    isAllValid = false;
                }
                specificUpdates.Add("dateOfTermination='" + ptDateTerm.Text + "'");
            }
            if (ptWage.Text != "")
            {
                if (!pt.SetHourlyRate(ptWage.Text.Replace(" ", "")) && ptWage.Text != "")
                {
                    ptWageError.Text += "<b>Salary</b> should be a number higher than 0";
                    isAllValid = false;
                }
                specificUpdates.Add("hourlyRate=" + ptWage.Text);
            }

            //for seasonal
            if (slfName.Text != "")
            {
                if (!sl.SetFirstName(slfName.Text) || slfName.Text == "")
                {
                    slfNameError.Text += "<b>First Name</b> can only contain the following characters: [A-Za-z. -]\n";
                    isAllValid = false;
                }
                normalUpdates.Add("firstName='" + slfName.Text + "'");
            }
            if (sllName.Text != "")
            {
                if (!sl.SetLastName(sllName.Text) || sllName.Text == "")
                {
                    sllNameError.Text += "<b>Last Name</b> can only contain the following characters: [A-Za-z. -]\n";
                    isAllValid = false;
                }
                normalUpdates.Add("lastName='" + sllName.Text + "'");
            }
            if (slSin.Text != "")
            {
                if (!sl.SetSIN(slSin.Text.Replace(" ", "")))
                {
                    slSinError.Text += "<b>SIN</b> should be 9-digit number";
                    isAllValid = false;
                }
                normalUpdates.Add("socialInsNumber=" + ftfName.Text);
            }
            if (slDOB.Text != "")
            {
                if (!sl.SetDOB(slDOB.Text.Replace(" ", "")))
                {
                    slDOBError.Text += "<b>Date Of Birth</b> should have valid date format";
                    isAllValid = false;
                }
                normalUpdates.Add("dateOfBirth='" + slDOB.Text + "'");
            }
            if (slSeason.Text != "" && Session["type"].ToString() == "SL")
            {
                if (!sl.SetSeason(slSeason.Text.Replace(" ", "")) && slSeason.Text != "")
                {
                    slSeasonError.Text += "<b>Season</b> must be valid. It should not be possible to get this error. Look at you, you little hacker";
                    isAllValid = false;
                }
                specificUpdates.Add("season='" + slSeason.Text + "'");
            }
            if (slPcPay.Text != "")
            {
                if (!sl.SetPiecePay(slPcPay.Text.Replace(" ", "")) && slPcPay.Text != "")
                {
                    slPcPayError.Text += "<b>Salary</b> should be a number higher than 0";
                    isAllValid = false;
                }
                specificUpdates.Add("piecePay='" + slPcPay.Text + "'");
            }

            //for contract
            if (ctName.Text != "")
            {
                if (!ct.SetLastName(ctName.Text))
                {
                    ctNameError.Text += "<b>Company Name</b> can only contain the following characters: [A-Za-z. -]\n";
                    isAllValid = false;
                }
                normalUpdates.Add("lastName='" + ctName.Text + "'");
            }
            if (ctBin.Text != "")
            {
                if (!ct.SetSIN(ctBin.Text.Replace(" ", "")))
                {
                    ctBinError.Text += "<b>BIN</b> should be 9-digit number";
                    isAllValid = false;
                }
                normalUpdates.Add("socialInsNumber='" + ctBin.Text + "'");
            }
            if (ctDOI.Text != "")
            {
                if (!ct.SetDOB(ctDOI.Text.Replace(" ", "")))
                {
                    ctDOIError.Text += "<b>Date Of Incorporation</b> should have valid date format";
                    isAllValid = false;
                }
                normalUpdates.Add("dateOfBirth='" + ctDOI.Text + "'");
            }
            if (ctStart.Text != "")
            {
                if (!ct.SetContractStartDate(ctStart.Text.Replace(" ", "")))
                {
                    ctStartError.Text += "<b>Contract Start Date</b> must be valid. It should not be possible to get this error. Look at you, you little hacker";
                    isAllValid = false;
                }
                specificUpdates.Add("dateStart='" + ctStart.Text + "'");
            }
            if (ctEnd.Text != "")
            {
                if (!ct.SetContractEndDate(ctEnd.Text.Replace(" ", "")))
                {
                    ctEndError.Text += "<b>Contract End Date</b> should have valid date format";
                    isAllValid = false;
                }
                specificUpdates.Add("dateStop='" + ctEnd.Text + "'");
            }
            if (ctAmt.Text != "")
            {
                if (!ct.SetFixedContractAmt(ctAmt.Text.Replace(" ", "")))
                {
                    ctAmtError.Text += "<b>Contract Amount</b> should be a number higher than 0";
                    isAllValid = false;
                }
                specificUpdates.Add("fixedCtAmt='" + ctAmt.Text + "'");
            }
            if (isAllValid)
            {
                int emp = 0;
                int.TryParse(Session["empID"].ToString(), out emp);
                bool first = true;
                if (normalUpdates.Count < 1)
                {
                    normalUpdates.Add("lastName='" + Session["lname"] + "'");
                }
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
                    string fieldValue = s.Substring(0, s.IndexOf('='));
                    string oldVal = sup.GetPreviousValue(conString, emp, @s, "");
                    string newVal = s;
                    newVal = newVal.Replace(fieldValue + @"='", "");
                    sup.CreateAudit(conString, userName, emp.ToString(), fieldValue, oldVal, newVal);
                }
                updateString += endUpdate;
                
                first = true;
                foreach (string s in specificUpdates)
                {
                    if (first)
                    {
                        updateSpecific += " " + s;
                        first = false;
                    }
                    else
                    {
                        updateSpecific += ", " + s;
                    }
                    string oldVal = sup.GetPreviousValue(conString, emp, @s, empID.ToString());
                }
                
                
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
                            empID = (Int32)changeCmd.ExecuteScalar();
                            changeCmd.Dispose();
                        }
                        updateSpecific += " WHERE empID=" + empID;
                        using (SqlCommand changeCmd = new SqlCommand())
                        {
                            changeCmd.Connection = connect;
                            changeCmd.CommandType = CommandType.Text;
                            changeCmd.CommandText = updateSpecific;
                            changeCmd.ExecuteNonQuery();
                            changeCmd.Dispose();
                        }
                    }
                }
                catch (Exception exce)
                {
                    
                }
            }
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                // Retrieve the CommandArgument property
                int index = Convert.ToInt32(e.CommandArgument); // or convert to other datatype
                GridViewRow row = searchResultGrid.Rows[index];
                string empId = row.Cells[1].Text;
                sin = row.Cells[2].Text;
                fname = row.Cells[3].Text;
                lname = row.Cells[4].Text;
                company = row.Cells[5].Text;
                type = row.Cells[6].Text;

                Session["empId"] = empId;
                Session["sin"] = sin;
                Session["fname"] = fname;
                Session["lname"] = lname;
                Session["company"] = company;
                Session["type"] = type;

                SqlConnection conn = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand(getCmdString(type), conn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@ep", SqlDbType.VarChar).Value = empId;
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

                   
                }
                else if(type == "PT")
                {
                    parttimeInput.Visible = true;

                  
                }
                else if(type == "CT")
                {
                    contractInput.Visible = true;

                    
                }
                else if(type == "SL")
                {
                    seasonalInput.Visible = true;

                    
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
                        cmdstring = "SELECT * FROM dbo.A_DisplayFTEmp(@ep)";
                    }
                    ///else if user is general
                    else if (securityLevel == "2")
                    {
                        cmdstring = "SELECT * FROM dbo.G_DisplayFTEmp(@ep)";
                    }
                    break;
                ///case for Partime if user is admin
                case "PT":
                    if (securityLevel == "1")
                    {
                        cmdstring = "SELECT * FROM dbo.A_DisplayPTEmp(@ep)";
                    }
                    ///else user is general
                    else if (securityLevel == "2")
                    {
                        cmdstring = "SELECT * FROM dbo.G_DisplayPTEmp(@ep)";
                    }
                    break;
                case "CT":
                    if (securityLevel == "1")
                    {
                        cmdstring = "SELECT * FROM dbo.A_DisplayCTEmp(@ep)";
                    }
                    break;
                ///Case for seasonal employee if user is admin
                case "SL":
                    if (securityLevel == "1")
                    {
                        cmdstring = "SELECT * FROM dbo.A_DisplaySLEmp(@ep)";
                    }
                    ///else if user is general
                    else if (securityLevel == "2")
                    {
                        cmdstring = "SELECT * FROM dbo.G_DisplaySLPTEmp(@ep)";
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