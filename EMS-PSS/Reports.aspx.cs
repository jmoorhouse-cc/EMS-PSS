﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS_PSS
{
    public partial class Reports : System.Web.UI.Page
    {
        string securityLevel;
        string userName;
        string conString;

        protected void Page_Load(object sender, EventArgs e)
        {
            securityLevel = Session["securitylevel"].ToString();
            userName = Session["username"].ToString();
            conString = Session["conString"].ToString();

            if (securityLevel == "2")
            {
                rblReports.Items.Remove(rblReports.Items.FindByValue("payroll"));
                rblReports.Items.Remove(rblReports.Items.FindByValue("active"));
                rblReports.Items.Remove(rblReports.Items.FindByValue("inactive"));
            }
            else if (securityLevel == "1")
            {
                rblReports.Items.Add(new ListItem("payroll"));
                rblReports.Items.Add(new ListItem("active"));
                rblReports.Items.Add(new ListItem("inactive"));
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
                
            if (rblReports.SelectedItem.Value == "seniority")
            {
                // run seniority report
            }
            else if (rblReports.SelectedItem.Value == "whw")
            {
                // run whw report
            }
            else if (rblReports.SelectedItem.Value == "payroll")
            {
                // run payroll report
            }
            else if (rblReports.SelectedItem.Value == "active")
            {
                // run active report
            }
            else  if (rblReports.SelectedItem.Value == "inactive")
            {
                // run inactive report
            }
        }
    }
}