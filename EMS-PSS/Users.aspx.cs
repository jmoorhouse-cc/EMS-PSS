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
    public partial class Users : System.Web.UI.Page
    {
        string securityLevel;
        string userName;
        string conString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            securityLevel = Session["securitylevel"].ToString();
            userName = Session["username"].ToString();
            conString = Session["conString"].ToString();
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            string userName = tbxUserName.Text;     // username
            string userPw = tbxUserPw.Text;         // pw
            string firstName = tbxUserfName.Text;   // first name
            string lastName = tbxUserlName.Text;    // last name
            int secLevel = 0;
            if ((tbxUsersLevel.Text == "admin") || (tbxUsersLevel.Text == "a") || (tbxUsersLevel.Text == "administrator"))
            {
                secLevel = 1;
            }
            else if ((tbxUsersLevel.Text == "general") || (tbxUsersLevel.Text == "g") || (tbxUsersLevel.Text == "gen"))
            {
                secLevel = 2;
            } 

            // add user
            SqlConnection conn = new SqlConnection(conString);
            int queryStatus = 0;
            string cmdstring = "";
            if (securityLevel == "1") cmdstring = 
                "INSERT INTO tb_User VALUES ('" + userName + "', '" + userPw + "', '" + firstName + "', '" + lastName + "', " + secLevel + ");";

            SqlCommand cmd = new SqlCommand(cmdstring, conn);
            cmd.CommandType = CommandType.Text;
            try
            {
                conn.Open();
                cmd.CommandText = cmdstring;
                queryStatus = cmd.ExecuteNonQuery();
            }
            catch(SqlException exp)
            {
                userAdditionResultLabel.Text = "Duplicate User Name; ";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            if (queryStatus > 0)
            {
                userAdditionResultLabel.Text = "User Addition Successful";
            }
            else
            {
                userAdditionResultLabel.Text += "User Addition Failed";
            }
        }

    }
}