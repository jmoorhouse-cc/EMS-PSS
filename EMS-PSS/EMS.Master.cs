using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS_PSS
{
    public partial class EMS : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string securityLevel = Session["securitylevel"].ToString();
            string userName = Session["username"].ToString();

            if (securityLevel == "1")
            {
                Menu1.Visible = true;
                Menu2.Visible = false;
            }
            else if (securityLevel == "2")
            {
                Menu1.Visible = false;
                Menu2.Visible = true;
            }
        }
    }
}