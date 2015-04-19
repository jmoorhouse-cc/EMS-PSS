using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS_PSS
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void searchSubmit_Click(object sender, EventArgs e)
        {
            string parameter = tbxSearch.Text;

            if (rblSearch.SelectedValue == "fname")
            {
                // search by First Name : parameter
            }
            else if (rblSearch.SelectedValue == "lname")
            {
                // search by Last Name : parameter
            }
            else if (rblSearch.SelectedValue == "sin")
            {
                // search by SIN Number : parameter
            }
        }
    }
}