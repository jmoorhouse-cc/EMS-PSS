﻿using System;
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
    public partial class Create : System.Web.UI.Page
    {
        // TO KEEP TRACK OF WHICH INFORMATION IS FILLED OUT
        //FullTime: firstName, lastName, SIN, DOH, DOT, salary
        //PartTime: firstName, lastName, SIN, DOH, DOT, hourlyRate
        //Seasonal: firstName, lastName, SIN, season, seasonYr, piecePay
        //Contract: <NULL>, contract companyName, BIN, StartDate, EndDate, contractAmt
        bool isFname, isLname, isSin, isDate1, isDate2, isMoney;
        int returnID = 0;
        int selectedEmpType;
        string conString, securityLevel, username;
        Supporting.Audit sup = new Supporting.Audit();

        protected void Page_Load(object sender, EventArgs e)
        {
            securityLevel = Session["securitylevel"].ToString();
            conString = Session["conString"].ToString();
            username = Session["username"].ToString();

            if (!IsPostBack)
            {
                if (securityLevel == "2")
                {
                    contractInput.Visible = false;
                    RadioButtonList1.Items.Remove(RadioButtonList1.Items.FindByValue("contract"));
                    ftDateTerm.Visible = false;
                    ftSalary.Visible = false;
                    ptDateTerm.Visible = false;
                    ptWage.Visible = false;
                    slPcPay.Visible = false;
                    ctAmt.Visible = false;
                }
                else if (securityLevel == "1")
                {
                    //contractInput.Visible = true;
                    // RadioButtonList1.Items.Add(new ListItem("contract"));
                    ftDateTerm.Visible = true;
                    ftSalary.Visible = true;
                    ptDateTerm.Visible = true;
                    ptWage.Visible = true;
                    slPcPay.Visible = true;
                    ctAmt.Visible = true;
                }
                populateCompList();
            }
            sucessLbl.Text = "";
        }

        protected void populateCompList()
        {
            try
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

                        ctCompany.DataSource = reader;
                        ctCompany.DataValueField = "companyName";
                        ctCompany.DataTextField = "companyName";
                        ctCompany.DataBind();
                        cmd.Dispose();
                        reader.Close();
                    }
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

                        ptCompany.DataSource = reader;
                        ptCompany.DataValueField = "companyName";
                        ptCompany.DataTextField = "companyName";
                        ptCompany.DataBind();
                        cmd.Dispose();
                        reader.Close();
                    }
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

                        slCompany.DataSource = reader;
                        slCompany.DataValueField = "companyName";
                        slCompany.DataTextField = "companyName";
                        slCompany.DataBind();
                        cmd.Dispose();
                        reader.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                //lblErrorMsg.Text = ex.Message;
            }
            catch (Exception exc)
            {
                //  lblErrorMsg.Text = exc.Message;
            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "fulltime")
            {
                fulltimeInput.Visible = true;
                parttimeInput.Visible = false;
                seasonalInput.Visible = false;
                contractInput.Visible = false;
                selectedEmpType = 1;
            }
            else if (this.RadioButtonList1.SelectedValue == "parttime")
            {
                fulltimeInput.Visible = false;
                parttimeInput.Visible = true;
                seasonalInput.Visible = false;
                contractInput.Visible = false;
                selectedEmpType = 2;
            }
            else if (this.RadioButtonList1.SelectedValue == "seasonal")
            {
                fulltimeInput.Visible = false;
                parttimeInput.Visible = false;
                seasonalInput.Visible = true;
                contractInput.Visible = false;
                selectedEmpType = 3;
            }
            else if (this.RadioButtonList1.SelectedValue == "contract")
            {
                fulltimeInput.Visible = false;
                parttimeInput.Visible = false;
                seasonalInput.Visible = false;
                contractInput.Visible = true;
                selectedEmpType = 4;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (this.RadioButtonList1.SelectedValue == "fulltime")
            {
                addFtEmp();
            }
            else if (this.RadioButtonList1.SelectedValue == "parttime")
            {
                addPtEmp();
            }
            else if (this.RadioButtonList1.SelectedValue == "seasonal")
            {
                addSlEmp();
            }
            else if (this.RadioButtonList1.SelectedValue == "contract")
            {
                addctEmp();
            }
        }


        private bool addEmpDB(string type, string cn, string fn, string ln, string sin, string dob, string activity)
        {
            bool success = false;
            if (!(cn == "" && fn == "" && ln == "" && sin == "" && dob == ""))
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(conString))
                    {
                        returnID = -1;
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandType = CommandType.Text;

                            cmd.CommandText =
                                "INSERT INTO tb_Emp (empType, companyName, firstName, lastName, socialInsNumber, dateOfBirth, activityStatus)"
                                 + "OUTPUT INSERTED.empID Values ('" + type.ToUpper() + "', @cn, @fn, @ln, @sin, @dob, @as)";
                            cmd.Parameters.AddWithValue("@cn", cn);


                            cmd.Parameters.AddWithValue("@fn", fn);
                            cmd.Parameters.AddWithValue("@ln", ln);
                            cmd.Parameters.AddWithValue("@sin", sin);
                            cmd.Parameters.AddWithValue("@dob", dob);
                            cmd.Parameters.AddWithValue("@as", activity);
                            /*
                            SqlParameter parameter = new SqlParameter();
                            parameter.ParameterName = "@dob";
                            parameter.SqlDbType = SqlDbType.Date;
                            parameter.Value = "2007/12/1";
                            */
                            returnID = (Int32)cmd.ExecuteScalar();
                            if (returnID != -1)
                            {
                                success = true;
                            }
                            else
                            {
                                success = false;
                            }
                        }
                        sup.CreateAudit(conString, username, returnID.ToString(), "empType", "N/A", type.ToUpper());
                        if (cn != "")
                        {
                            sup.CreateAudit(conString, username, returnID.ToString(), "companyName", "N/A", cn);
                        }
                        sup.CreateAudit(conString, username, returnID.ToString(), "firstName", "N/A", fn);
                        sup.CreateAudit(conString, username, returnID.ToString(), "lastName", "N/A", ln);
                        sup.CreateAudit(conString, username, returnID.ToString(), "socialInsuranceNumber", "N/A", sin);
                        sup.CreateAudit(conString, username, returnID.ToString(), "dateOfBirth", "N/A", dob);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
                return success;

        }

        public void addFtEmp()
        {
            FulltimeEmployee ft = new FulltimeEmployee();
            bool isAllValid = true;

            ftfNameError.Text = "";
            ftlNameError.Text = "";
            ftSinError.Text = "";
            ftDOBError.Text = "";
            ftDateHireError.Text = "";
            ftDateTermError.Text = "";
            ftSalaryError.Text = "";

            if (!ft.SetFirstName(ftfName.Text) && ftfName.Text != "")
            {
                ftfNameError.Text += "<b>First Name</b> can only contain the following characters: [A-Za-z. -]\n";
                isAllValid = false;
            }
            if (!ft.SetLastName(ftlName.Text) && ftlName.Text != "")
            {
                ftlNameError.Text += "<b>Last Name</b> can only contain the following characters: [A-Za-z. -]\n";
                isAllValid = false;
            }
            if (!ft.SetSIN(ftSin.Text.Replace(" ", "")) && ftSin.Text != "")
            {
                ftSinError.Text += "<b>SIN</b> should be 9-digit number";
                isAllValid = false;
            }
            if (!ft.SetDOB(ftDOB.Text.Replace(" ", "")) && ftDOB.Text != "")
            {
                ftDOBError.Text += "<b>Date Of Hire</b> should have valid date format";
                isAllValid = false;
            }
            if (!ft.SetDateOfHire(ftDateHire.Text.Replace(" ", "")) && ftDateHire.Text != "")
            {
                ftDateHireError.Text += "<b>Date Of Birth</b> should have valid date format";
                isAllValid = false;
            }
            if ((!ft.SetDateOfTermination(ftDateTerm.Text.Replace(" ", "")) && ftDateTerm.Text != "") || (ftReason.Text == "" && ftDateTerm.Text != ""))
            {
                ftDateTermError.Text += "<b>Date Of Termination</b> should have valid date format";
                isAllValid = false;
            }
            if (!ft.SetSalary(ftSalary.Text.Replace(" ", "")) && ftSalary.Text != "")
            {
                ftSalaryError.Text += "<b>Salary</b> should be a number higher than 0";
                isAllValid = false;
            }
            if (isAllValid)
            {
                string activity = "0";
                if (ftDateHire.Text != "" && ftSalary.Text != "" && ftDOB.Text != "" && ftSin.Text != "")
                {
                    activity = "1";
                    if(ftReason.Text != "")
                    {
                        activity = "2";
                    }
                }
                if (addEmpDB("FT", ftCompany.Text, ftfName.Text, ftlName.Text, ftSin.Text, ftDOB.Text, activity))
                {
                    addFtEmpDB(returnID, ftDateHire.Text, ftDateTerm.Text, ftSalary.Text);
                    sucessLbl.Text = ftfName.Text + " Has been succesfully added";
                }
            }
        }

        private void addFtEmpDB(int empId, string dateOfHire, string dateOfTermination, string salary)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        if (dateOfTermination != "")
                        {
                            if (salary != "")
                            {
                                cmd.CommandText =
                                    "INSERT INTO tb_ftEmp (empID, dateOfHire, dateOfTermination, salary)"
                                     + "Values (" + empId + ", @dh, @dt, " + salary + ")";
                            }
                            else
                            {
                                cmd.CommandText =
                                    "INSERT INTO tb_ftEmp (empID, dateOfHire, dateOfTermination)"
                                     + "Values (" + empId + ", @dh, @dt" + ")";
                            }
                            cmd.Parameters.AddWithValue("@dt", dateOfTermination);
                        }
                        else
                        {
                            if (salary != "")
                            {
                                cmd.CommandText =
                                    "INSERT INTO tb_ftEmp (empID, dateOfHire, salary)"
                                     + "Values (" + empId + ", @dh, " + salary + ")";
                            }
                            else
                            {
                                cmd.CommandText =
                                    "INSERT INTO tb_ftEmp (empID, dateOfHire)"
                                     + "Values (" + empId + ", @dh" + ")";
                            }
                        }
                        //cmd.Parameters.AddWithValue("@id", empId);
                        cmd.Parameters.AddWithValue("@dh", dateOfHire);
                        cmd.Parameters.AddWithValue("@rl", ftReason);
                        //cmd.Parameters.AddWithValue("@s", salary);
                        /*
                        SqlParameter parameter = new SqlParameter();
                        parameter.ParameterName = "@dob";
                        parameter.SqlDbType = SqlDbType.Date;
                        parameter.Value = "2007/12/1";
                        */
                        cmd.ExecuteNonQuery();

                        sup.CreateAudit(conString, username, empId.ToString(), "empID", "N/A", empId.ToString());
                        sup.CreateAudit(conString, username, empId.ToString(), "dateOfHire", "N/A", dateOfHire);
                        sup.CreateAudit(conString, username, empId.ToString(), "dateOfTermination", "N/A", dateOfTermination);
                        sup.CreateAudit(conString, username, empId.ToString(), "salary", "N/A", salary);
                    }
                }
            }
            catch (Exception ex)
            {
                //success = false;
            }
        }

        public void addPtEmp()
        {
            ParttimeEmployee pt = new ParttimeEmployee();
            bool isAllValid = true;

            ptfNameError.Text = "";
            ptlNameError.Text = "";
            ptSinError.Text = "";
            ptDateHireError.Text = "";
            ptDOBError.Text = "";
            ptDateTermError.Text = "";
            ptWageError.Text = "";

            if (!pt.SetFirstName(ptfName.Text) && ptfName.Text != "")
            {
                ptfNameError.Text += "<b>First Name</b> can only contain the following characters: [A-Za-z. -]\n";
                isAllValid = false;
            }
            if (!pt.SetLastName(ptlName.Text) && ptlName.Text != "")
            {
                ptlNameError.Text += "<b>Last Name</b> can only contain the following characters: [A-Za-z. -]\n";
                isAllValid = false;
            }
            if (!pt.SetSIN(ptSin.Text.Replace(" ", "")) && ptSin.Text != "")
            {
                ptSinError.Text += "<b>SIN</b> should be 9-digit number";
                isAllValid = false;
            }
            if (!pt.SetDOB(ptDOB.Text.Replace(" ", "")) && ptDOB.Text != "")
            {
                ptDOBError.Text += "<b>Date Of Birth</b> should have valid date format";
                isAllValid = false;
            }
            if (!pt.SetDateOfHire(ptDateHire.Text.Replace(" ", "")) && ptDateHire.Text != "")
            {
                ptDateHireError.Text += "<b>Date Of Hire</b> should have valid date format";
                isAllValid = false;
            }
            if ((!pt.SetDateOfTermination(ptDateTerm.Text.Replace(" ", "")) && ptDateTerm.Text != "") || (ptReason.Text == "" && ptDateTerm.Text != ""))
            {
                ptDateTermError.Text += "<b>Date Of Termination</b> should have valid date format and needs Reason for leaving";
                isAllValid = false;
            }
            if (!pt.SetHourlyRate(ptWage.Text.Replace(" ", "")) && ptWage.Text != "")
            {
                ptWageError.Text += "<b>Salary</b> should be a number higher than 0";
                isAllValid = false;
            }
            if (isAllValid)
            {
                string activity = "0";
                if (ptDateHire.Text != "" && ptWage.Text != "" && ptDOB.Text != "" && ptSin.Text != "")
                {
                    activity = "1";
                    if(ptReason.Text != "")
                    {
                        activity = "2";
                    }
                }
                if (addEmpDB("PT", ptCompany.Text, ptfName.Text, ptlName.Text, ptSin.Text, ptDOB.Text, activity))
                {
                    addPtEmpDB(returnID, ptDateHire.Text, ptDateTerm.Text, ptWage.Text);
                    sucessLbl.Text = ptfName.Text + " Has been succesfully added";
                }
            }
        }

        private void addPtEmpDB(int empId, string dateOfHire, string dateOfTermination, string wage)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        if (dateOfTermination != "")
                        {
                            if (wage != "")
                            {
                                cmd.CommandText =
                                    "INSERT INTO tb_ptEmp (empID, dateOfHire, dateOfTermination, reasonForLeaving, hourlyRate)"
                                     + "Values (" + empId + ", @dh, @dt, @rl, " + wage + ")";

                            }
                            else
                            {
                                cmd.CommandText =
                                    "INSERT INTO tb_ptEmp (empID, dateOfHire, dateOfTermination, reasonForLeaving)"
                                     + "Values (" + empId + ", @dh, @dt, @rl" + ")";
                            }
                            cmd.Parameters.AddWithValue("@dt", dateOfTermination);
                        }
                        else
                        {
                            if (wage != "")
                            {
                                cmd.CommandText =
                                    "INSERT INTO tb_ptEmp (empID, dateOfHire, reasonForLeaving, hourlyRate)"
                                     + "Values (" + empId + ", @dh, @rl, " + wage + ")";
                            }
                            else
                            {
                                cmd.CommandText =
                                    "INSERT INTO tb_ptEmp (empID, dateOfHire, reasonForLeaving)"
                                     + "Values (" + empId + ", @dh, @rl" + ")";
                            }
                        }
                        //cmd.Parameters.AddWithValue("@id", empId);
                        cmd.Parameters.AddWithValue("@dh", dateOfHire);
                        cmd.Parameters.AddWithValue("@rl", ptReason.Text);
                        //cmd.Parameters.AddWithValue("@s", salary);
                        /*
                        SqlParameter parameter = new SqlParameter();
                        parameter.ParameterName = "@dob";
                        parameter.SqlDbType = SqlDbType.Date;
                        parameter.Value = "2007/12/1";
                        */
                        cmd.ExecuteNonQuery();

                        sup.CreateAudit(conString, username, empId.ToString(), "empID", "N/A", empId.ToString());
                        sup.CreateAudit(conString, username, empId.ToString(), "dateOfHire", "N/A", dateOfHire);
                        sup.CreateAudit(conString, username, empId.ToString(), "dateOfTermination", "N/A", dateOfTermination);
                        sup.CreateAudit(conString, username, empId.ToString(), "hourlyRate", "N/A", wage);
                    }
                }
            }
            catch (Exception ex)
            {
                //success = false;
            }

        }

        public void addSlEmp()
        {
            SeasonalEmployee sl = new SeasonalEmployee();
            bool isAllValid = true;

            slfNameError.Text = "";
            sllNameError.Text = "";
            slSinError.Text = "";
            slDOBError.Text = "";
            slSeasonError.Text = "";
            slYearError.Text = "";
            slPcPayError.Text = "";

            if (!sl.SetFirstName(slfName.Text) && slfName.Text != "")
            {
                slfNameError.Text += "<b>First Name</b> can only contain the following characters: [A-Za-z. -]\n";
                isAllValid = false;
            }
            if (!sl.SetLastName(sllName.Text) && sllName.Text != "")
            {
                sllNameError.Text += "<b>Last Name</b> can only contain the following characters: [A-Za-z. -]\n";
                isAllValid = false;
            }
            if (!sl.SetSIN(slSin.Text.Replace(" ", "")) && slSin.Text != "")
            {
                slSinError.Text += "<b>SIN</b> should be 9-digit number";
                isAllValid = false;
            }
            if (!sl.SetDOB(slDOB.Text.Replace(" ", "")) && slDOB.Text != "")
            {
                slDOBError.Text += "<b>Date Of Birth</b> should have valid date format";
                isAllValid = false;
            }
            if (!sl.SetSeason(slSeason.Text.Replace(" ", "")) && slSeason.SelectedIndex.ToString() != "0")
            {
                slSeasonError.Text += "<b>Season</b> must be valid. It should not be possible to get this error. Look at you, you little hacker";
                isAllValid = false;
            }
            //if (!sl.SetYear(slSeason.Text.Replace(" ", "")))
            //{
            //    slSeasonError.Text += "<b>Date Of Birth</b> should have valid date format";
            //    isAllValid = false;
            //}
            if (!sl.SetPiecePay(slPcPay.Text.Replace(" ", "")) && slPcPay.Text != "")
            {
                slPcPayError.Text += "<b>Salary</b> should be a number higher than 0";
                isAllValid = false;
            }
            if (isAllValid)
            {
                string activity = "0";
                if (slSeason.Text != "" && slPcPay.Text != "" && slDOB.Text != "" && slSin.Text != "" && slYear.Text != "")
                {
                    activity = "1";
                    if(slReason.Text != "")
                    {
                        activity = "2";
                    }
                }
                if (addEmpDB("SL", slCompany.Text, slfName.Text, sllName.Text, slSin.Text, slDOB.Text, activity))
                {
                    addSlEmpDB(returnID, slSeason.Text, slYear.Text, sldateStart.Text, slPcPay.Text);
                    sucessLbl.Text = slfName.Text + " Has been succesfully added";
                }
            }
        }

        private void addSlEmpDB(int empId, string season, string seasonYear, string dateStart, string piecePay)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        if (season != "")
                        {
                            if (piecePay != "")
                            {
                                cmd.CommandText =
                                    "INSERT INTO tb_slEmp (empID, season, seasonYear, dateStart, reasonForLeaving, piecePay)"
                                     + "Values (" + empId + ", @sn, @sy, @ds, @rl, " + piecePay + ")";
                                //cmd.Parameters.AddWithValue("@sn", piecePay);
                            }
                            else
                            {
                                cmd.CommandText =
                                    "INSERT INTO tb_slEmp (empID, season, seasonYear, dateStart, reasonForLeaving)"
                                     + "Values (" + empId + ", @sn, @sy, @ds, @rl" + ")";
                            }
                            cmd.Parameters.AddWithValue("@sn", season);
                        }
                        else
                        {
                            if (piecePay != "")
                            {
                                cmd.CommandText =
                                    "INSERT INTO tb_slEmp (empID, seasonYear, dateStart, reasonForLeaving, piecePay)"
                                     + "Values (" + empId + ", @sy, @ds, @rl, " + piecePay + ")";
                            }
                            else
                            {
                                cmd.CommandText =
                                    "INSERT INTO tb_slEmp (empID, seasonYear, dateStart, reasonForLeaving)"
                                     + "Values (" + empId + ", @sy, @ds, @rl" + ")";
                            }
                        }
                        cmd.Parameters.AddWithValue("@sy", seasonYear);
                        cmd.Parameters.AddWithValue("@ds", dateStart);
                        cmd.Parameters.AddWithValue("@rl", slReason);
                        cmd.ExecuteNonQuery();

                        sup.CreateAudit(conString, username, empId.ToString(), "empID", "N/A", empId.ToString());
                        sup.CreateAudit(conString, username, empId.ToString(), "season", "N/A", season);
                        sup.CreateAudit(conString, username, empId.ToString(), "seasonYear", "N/A", seasonYear);
                        sup.CreateAudit(conString, username, empId.ToString(), "dateStart", "N/A", dateStart);
                        sup.CreateAudit(conString, username, empId.ToString(), "piecePay", "N/A", piecePay);
                    }
                }
            }
            catch (Exception ex)
            {
                //success = false;
            }
        }

        public void addctEmp()
        {
            ContractEmployee ct = new ContractEmployee();
            bool isAllValid = true;

            ctNameError.Text = "";
            //ctlNameError.Text = "";
            ctBinError.Text = "";
            ctDOIError.Text = "";
            ctStartError.Text = "";
            ctEndError.Text = "";
            ctAmtError.Text = "";

            if (!ct.SetLastName(ctName.Text) && ctName.Text != "")
            {
                ctNameError.Text += "<b>Company Name</b> can only contain the following characters: [A-Za-z. -]\n";
                isAllValid = false;
            }
            if (!ct.SetSIN(ctBin.Text.Replace(" ", "")) && ctBin.Text != "")
            {
                ctBinError.Text += "<b>BIN</b> should be 9-digit number";
                isAllValid = false;
            }
            if (!ct.SetDOB(ctDOI.Text.Replace(" ", "")) && ctDOI.Text != "")
            {
                ctDOIError.Text += "<b>Date Of Incorporation</b> should have valid date format";
                isAllValid = false;
            }
            if (!ct.SetContractStartDate(ctStart.Text.Replace(" ", "")) && ctStart.Text != "")
            {
                ctStartError.Text += "<b>Contract Start Date</b> must be valid. It should not be possible to get this error. Look at you, you little hacker";
                isAllValid = false;
            }
            if ((!ct.SetContractEndDate(ctEnd.Text.Replace(" ", "")) && ctEnd.Text != "") || (ctReason.Text == "" && ctEnd.Text != ""))
            {
                ctEndError.Text += "<b>Contract End Date</b> should have valid date format and needs Reason for leaving";
                isAllValid = false;
            }
            if (!ct.SetFixedContractAmt(ctAmt.Text.Replace(" ", "")) && ctAmt.Text != "")
            {
                ctAmtError.Text += "<b>Contract Amount</b> should be a number higher than 0";
                isAllValid = false;
            }
            if (isAllValid)
            {
                string activity = "0";
                if (ctBin.Text != "" && ctStart.Text != "" && ctEnd.Text != "" && ctAmt.Text != "")
                {
                    activity = "1";
                    if(ctReason.Text != "")
                    {
                        activity = "2";
                    }
                }
                if (addEmpDB("SL", ctCompany.Text, "", ctName.Text, ctBin.Text, ctDOI.Text, activity))
                {
                    addCtEmpDB(returnID, ctStart.Text, ctEnd.Text, ctAmt.Text);
                    sucessLbl.Text = ctName.Text + " Has been succesfully added";
                }
            }
        }

        private void addCtEmpDB(int empId, string start, string end, string amt)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        if (amt != "")
                        {
                            cmd.CommandText =
                                    "INSERT INTO tb_ctEmp (empID, dateStart, dateStop, reasonForLeaving, fixedCtAmt)"
                                     + "Values (" + empId + ", @ds, @de, @rl, " + amt + ")";
                        }
                        else
                        {
                            cmd.CommandText =
                                    "INSERT INTO tb_ctEmp (empID, dateStart, dateStop, reasonForLeaving)"
                                     + "Values (" + empId + ", @ds, @de, @rl" + ")";
                        }
                        cmd.Parameters.AddWithValue("@ds", start);
                        cmd.Parameters.AddWithValue("@de", end);
                        cmd.Parameters.AddWithValue("@rl", ctReason.Text);
                        cmd.ExecuteNonQuery();

                        sup.CreateAudit(conString, username, empId.ToString(), "empID", "N/A", empId.ToString());
                        sup.CreateAudit(conString, username, empId.ToString(), "dateStart", "N/A", start);
                        sup.CreateAudit(conString, username, empId.ToString(), "dateStop", "N/A", end);
                        sup.CreateAudit(conString, username, empId.ToString(), "fixedCtAmt", "N/A", amt);
                    }
                }
            }
            catch (Exception ex)
            {
                //success = false;
            }
        }
    }
}