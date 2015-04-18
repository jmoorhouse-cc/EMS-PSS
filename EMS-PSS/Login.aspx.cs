﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;              // To connect to MSSql Server
using System.Data.SqlClient;

namespace EMS_PSS
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string userName;
        string userPw;
        string conString;
        int userLevel;

        protected void Page_Load(object sender, EventArgs e)
        {
            conString =
                @"server=PORTABLEGLADOS; " +
                @"initial catalog=dbEMS; " +
                @"user id=ems; " +
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
            return rtnValue;
        }

    }
}

//System.Web.HttpContext.Current.Response.Write(testUname + " " + testPword);