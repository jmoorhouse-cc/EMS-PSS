using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS_PSS
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            string userName = tbxUserName.Text; // username
            string userPw = tbxUserPw.Text; // pw
            string firstName = tbxUserfName.Text; // first name
            string lastName = tbxUserlName.Text; // last name
            string secLevel = tbxUsersLevel.Text; // security level -- 1 = Admin; 2 = General

            // add user
        }
    }
}