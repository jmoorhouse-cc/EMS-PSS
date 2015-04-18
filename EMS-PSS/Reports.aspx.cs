using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS_PSS
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
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