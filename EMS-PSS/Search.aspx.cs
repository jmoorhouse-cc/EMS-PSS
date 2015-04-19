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
            string fName = tbxSearchfName.Text;
            string lName = tbxSearchlName.Text;
            string sNum = tbxSearchsNum.Text;

            // search
        }
    }
}