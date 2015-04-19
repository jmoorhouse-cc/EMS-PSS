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

        int selectedEmpType;
        protected void Page_Load(object sender, EventArgs e)
        {
            string securityLevel = Session["securitylevel"].ToString();
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
            
        }
        public void addFTUser()
        {
            FulltimeEmployee ft = new FulltimeEmployee();
            bool isAllValid = false;

            if (!ft.SetFirstName(ftfName.Text))
            {
                ftfNameError.Text += "<b>First Name</b> can only contain the following characters: [A-Za-z. -]";
                isAllValid = false;
            }
            if (!ft.SetLastName(ftlName.Text))
            {
                ftlNameError.Text += "<b>Last Name</b> can only contain the following characters: [A-Za-z. -]";
                isAllValid = false;
            }
            if (!ft.SetSIN(ftSin.Text.Replace(" ", "")))
            {
                ftSinError.Text += "<b>SIN</b> should be 9-digit number";
                isAllValid = false;
            }
            

            if (isAllValid)
            {
                SqlConnection conn = new SqlConnection();

            }
        }
    }
}