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
    public partial class Create : System.Web.UI.Page
    {
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
            }
            else if (this.RadioButtonList1.SelectedValue == "parttime")
            {
                fulltimeInput.Visible = false;
                parttimeInput.Visible = true;
                seasonalInput.Visible = false;
                contractInput.Visible = false;
            }
            else if (this.RadioButtonList1.SelectedValue == "seasonal")
            {
                fulltimeInput.Visible = false;
                parttimeInput.Visible = false;
                seasonalInput.Visible = true;
                contractInput.Visible = false;
            }
            else if (this.RadioButtonList1.SelectedValue == "contract")
            {
                fulltimeInput.Visible = false;
                parttimeInput.Visible = false;
                seasonalInput.Visible = false;
                contractInput.Visible = true;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if(addUser())
            {

            }
            else
            {

            }
        }
        public bool addUser()
        {
            bool addUserSuccessful = false;
            


            
            return addUserSuccessful;
        }
    }
}