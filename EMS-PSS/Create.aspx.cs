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
        string conString, securityLevel;

        protected void Page_Load(object sender, EventArgs e)
        {
            securityLevel = Session["securitylevel"].ToString();
            conString = Session["conString"].ToString();
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

                        ctName.DataSource = reader;
                        ctName.DataValueField = "companyName";
                        ctName.DataTextField = "companyName";
                        ctName.DataBind();
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

            }
        }


        private bool addEmpDB(string type, string cn, string fn, string ln, string sin, string dob, string activity)
        {
            bool success = false;
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
                }
            }
            catch (Exception ex)
            {
                success = false;
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

            if (!ft.SetFirstName(ftfName.Text) || ftfName.Text == "")
            {
                ftfNameError.Text += "<b>First Name</b> can only contain the following characters: [A-Za-z. -]\n";
                isAllValid = false;
            }
            if (!ft.SetLastName(ftlName.Text) || ftlName.Text == "")
            {
                ftlNameError.Text += "<b>Last Name</b> can only contain the following characters: [A-Za-z. -]\n";
                isAllValid = false;
            }
            if (!ft.SetSIN(ftSin.Text.Replace(" ", "")))
            {
                ftSinError.Text += "<b>SIN</b> should be 9-digit number";
                isAllValid = false;
            }
            if (!ft.SetDOB(ftDOB.Text.Replace(" ", "")))
            {
                ftDOBError.Text += "<b>Date Of Hire</b> should have valid date format";
                isAllValid = false;
            }
            if (!ft.SetDateOfHire(ftDateHire.Text.Replace(" ", "")))
            {
                ftDateHireError.Text += "<b>Date Of Birth</b> should have valid date format";
                isAllValid = false;
            }
            if (!ft.SetDateOfTermination(ftDateTerm.Text.Replace(" ", "")) && ftDateTerm.Text != "")
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
                        //cmd.Parameters.AddWithValue("@s", salary);
                        /*
                        SqlParameter parameter = new SqlParameter();
                        parameter.ParameterName = "@dob";
                        parameter.SqlDbType = SqlDbType.Date;
                        parameter.Value = "2007/12/1";
                        */
                        cmd.ExecuteNonQuery();
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

            if (!pt.SetFirstName(ptfName.Text) || ptfName.Text == "")
            {
                ptfNameError.Text += "<b>First Name</b> can only contain the following characters: [A-Za-z. -]\n";
                isAllValid = false;
            }
            if (!pt.SetLastName(ptlName.Text) || ptlName.Text == "")
            {
                ptlNameError.Text += "<b>Last Name</b> can only contain the following characters: [A-Za-z. -]\n";
                isAllValid = false;
            }
            if (!pt.SetSIN(ptSin.Text.Replace(" ", "")))
            {
                ptSinError.Text += "<b>SIN</b> should be 9-digit number";
                isAllValid = false;
            }
            if (!pt.SetDOB(ptDOB.Text.Replace(" ", "")))
            {
                ptDOBError.Text += "<b>Date Of Birth</b> should have valid date format";
                isAllValid = false;
            }
            if (!pt.SetDateOfHire(ptDateHire.Text.Replace(" ", "")))
            {
                ptDateHireError.Text += "<b>Date Of Hire</b> should have valid date format";
                isAllValid = false;
            }
            if (!pt.SetDateOfTermination(ptDateTerm.Text.Replace(" ", "")) && ptDateTerm.Text != "")
            {
                ptDateTermError.Text += "<b>Date Of Termination</b> should have valid date format";
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
                                    "INSERT INTO tb_ptEmp (empID, dateOfHire, dateOfTermination, hourlyRate)"
                                     + "Values (" + empId + ", @dh, @dt, " + wage + ")";

                            }
                            else
                            {
                                cmd.CommandText =
                                    "INSERT INTO tb_ptEmp (empID, dateOfHire, dateOfTermination)"
                                     + "Values (" + empId + ", @dh, @dt" + ")";
                            }
                            cmd.Parameters.AddWithValue("@dt", dateOfTermination);
                        }
                        else
                        {
                            if (wage != "")
                            {
                                cmd.CommandText =
                                    "INSERT INTO tb_ptEmp (empID, dateOfHire, hourlyRate)"
                                     + "Values (" + empId + ", @dh, " + wage + ")";
                            }
                            else
                            {
                                cmd.CommandText =
                                    "INSERT INTO tb_ptEmp (empID, dateOfHire)"
                                     + "Values (" + empId + ", @dh" + ")";
                            }
                        }
                        //cmd.Parameters.AddWithValue("@id", empId);
                        cmd.Parameters.AddWithValue("@dh", dateOfHire);
                        //cmd.Parameters.AddWithValue("@s", salary);
                        /*
                        SqlParameter parameter = new SqlParameter();
                        parameter.ParameterName = "@dob";
                        parameter.SqlDbType = SqlDbType.Date;
                        parameter.Value = "2007/12/1";
                        */
                        cmd.ExecuteNonQuery();
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

            if (!sl.SetFirstName(slfName.Text) || slfName.Text == "")
            {
                slfNameError.Text += "<b>First Name</b> can only contain the following characters: [A-Za-z. -]\n";
                isAllValid = false;
            }
            if (!sl.SetLastName(sllName.Text) || sllName.Text == "")
            {
                sllNameError.Text += "<b>Last Name</b> can only contain the following characters: [A-Za-z. -]\n";
                isAllValid = false;
            }
            if (!sl.SetSIN(slSin.Text.Replace(" ", "")))
            {
                slSinError.Text += "<b>SIN</b> should be 9-digit number";
                isAllValid = false;
            }
            if (!sl.SetDOB(slDOB.Text.Replace(" ", "")))
            {
                slDOBError.Text += "<b>Date Of Birth</b> should have valid date format";
                isAllValid = false;
            }
            if (!sl.SetSeason(slSeason.Text.Replace(" ", "")) && slSeason.Text != "")
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
                                    "INSERT INTO tb_slEmp (empID, season, seasonYear, dateStart, piecePay)"
                                     + "Values (" + empId + ", @sn, @sy, @ds, " + piecePay + ")";
                                //cmd.Parameters.AddWithValue("@sn", piecePay);
                            }
                            else
                            {
                                cmd.CommandText =
                                    "INSERT INTO tb_slEmp (empID, season, seasonYear, dateStart)"
                                     + "Values (" + empId + ", @sn, @sy, @ds" + ")";
                            }
                            cmd.Parameters.AddWithValue("@sn", season);
                        }
                        else
                        {
                            if (piecePay != "")
                            {
                                cmd.CommandText =
                                    "INSERT INTO tb_slEmp (empID, seasonYear, dateStart, piecePay)"
                                     + "Values (" + empId + ", @sy, @ds, " + piecePay + ")";
                            }
                            else
                            {
                                cmd.CommandText =
                                    "INSERT INTO tb_slEmp (empID, seasonYear, dateStart)"
                                     + "Values (" + empId + ", @sy, @ds" + ")";
                            }
                        }
                        cmd.Parameters.AddWithValue("@sy", seasonYear);
                        cmd.Parameters.AddWithValue("@ds", dateStart);
                        cmd.ExecuteNonQuery();
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