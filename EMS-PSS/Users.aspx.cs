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
        string uName = "";     // username
        string userPw = "";        // pw
        string firstName = "";  // first name
        string lastName = "";   // last name
        int secLevel = 0;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            securityLevel = Session["securitylevel"].ToString();
            userName = Session["username"].ToString();
            conString = Session["conString"].ToString();
            displayUsers();
        }

        private void displayUsers()
        {
            SqlConnection conn = new SqlConnection(conString);
            string cmdString = "SELECT * FROM dbo.tb_User";
            SqlCommand cmd = new SqlCommand(cmdString, conn);
            cmd.CommandType = CommandType.Text;
            dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (SqlException exp)
            {
                userDisplayResultLabel.Text = "ERROR: " +exp.Message;
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            if (dt.Rows.Count == 0) userDisplayResultLabel.Text += "No Result to Display";
            else userDisplayResultLabel.Text = "";

            userDisplayGrid.DataSource = dt;
            userDisplayGrid.DataBind();
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            uName = tbxUserName.Text;     // username
            userPw = tbxUserPw.Text;         // pw
            firstName = tbxUserfName.Text;   // first name
            lastName = tbxUserlName.Text;    // last name
            secLevel = 0;
            
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
                "INSERT INTO tb_User VALUES ('" + uName + "', '" + userPw + "', '" + firstName + "', '" + lastName + "', " + secLevel + ");";

            SqlCommand cmd = new SqlCommand(cmdstring, conn);
            cmd.CommandType = CommandType.Text;
            try
            {
                conn.Open();
                cmd.CommandText = cmdstring;
                queryStatus = cmd.ExecuteNonQuery();
            }
            catch(SqlException)
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
                userAdditionResultLabel.Text = 
                    "User [userName:" + uName + "|userPassword:" + userPw + "|firstName:" + firstName + 
                    "|lastName:" + lastName + "|secLevel:" + tbxUsersLevel.Text + "] Addition Successful";
                tbxUserName.Text = "";     // username
                tbxUserPw.Text = "";         // pw
                tbxUserfName.Text = "";   // first name
                tbxUserlName.Text = "";    // last name
                tbxUsersLevel.Text = "";    // sec level
            }
            else
            {
                userAdditionResultLabel.Text += "User Addition Failed";
            }
        }

    }
}