using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;              // To connect to MSSql Server
using System.Data.SqlClient;
using Supporting;

namespace EMS_PSS
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string userName;
        string userPw;
        string conString;
        int userLevel;
        Supporting.Audit sup = new Supporting.Audit();

        protected void Page_Load(object sender, EventArgs e)
        {
            conString =
                @"server=localhost; " +
                @"initial catalog=dbEMS; " +
                @"user id=sa; " +
                @"password=Conestoga1 ";
        }

        protected void SubmitLogin(object sender, EventArgs e)
        {
            userName = tbxUsername.Value.ToString();
            userPw = tbxPassword.Value.ToString();

            if (!VerifyUser(userName, userPw))
            {
                lblErrorMsg.Text = "Invalid credentials";
            }
            else
            {
                Session["username"] = userName;
                Session["securitylevel"] = userLevel;
                Session["conString"] = conString;
                Session["loginTime"] = DateTime.Now.ToString();

                Server.Transfer("Home.aspx", true);
            }
        }

        public bool VerifyUser(string user, string pw)
        {
            bool rtnValue = false;
            string dbUsername = "";
            string dbPW = "";
            int dbSecurityLevel = 0; 

            try
            {
                using (var con = new System.Data.SqlClient.SqlConnection(conString))
                {
                    using (var cmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM tb_User", con))
                    {
                        try
                        {
                            con.Open();
                        }
                        catch (Exception e)
                        {
                            lblErrorMsg.Text = e.Message;
                        }
                        
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            dbUsername = reader.GetValue(0).ToString();
                            dbPW = reader.GetValue(1).ToString();
                            dbSecurityLevel = reader.GetInt32(4);

                            if (dbUsername == user && dbPW == pw)
                            {
                                lblErrorMsg.Text = "";
                                userLevel = dbSecurityLevel;
                                rtnValue = true;
                                break;
                            }
                        }
                        reader.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
            catch (Exception exc)
            {
                lblErrorMsg.Text = exc.Message;
            }
            return rtnValue;
        }

    }
}

//System.Web.HttpContext.Current.Response.Write(testUname + " " + testPword);