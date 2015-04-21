/*======================================*/
/*File: Search.aspx                     */
/*Purpose: This file will contain all   */
/*  the methods that will allow a user  */
/*  to search the attached database.    */
/*======================================*/

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
    //Class: Search
    //Purpose: to contain the searching methods
    public partial class Search : System.Web.UI.Page
    {
        //Decleration of variables
        string securityLevel;
        string userName;
        string conString;
        DataTable dt, dt2;
        ///Methods: Page_load()
        /// <summary>
        /// this method will set the security level
        /// Username and connection string as specified by
        /// the user apon loading of the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            securityLevel = Session["securitylevel"].ToString();
            userName = Session["username"].ToString();
            conString = Session["conString"].ToString();
        }

        ///Method:serchSubit_Click
        /// <summary>
        ///  when the user presses the search button 
        /// this method will take in the fields specified
        /// //of the user and make a query to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void searchSubmit_Click(object sender, EventArgs e)
        {

            string fn = fnameSearch.Text;
            string ln = lnameSearch.Text;
            string sin = sinSearch.Text;
            
            SqlConnection conn = new SqlConnection(conString);
            string cmdstring = "";
            if (securityLevel == "1") cmdstring = "SELECT * FROM dbo.A_SearchEmp(@fName, @lName, @sin)";
            else if (securityLevel == "2") cmdstring = "SELECT * FROM dbo.G_SearchEmp(@fName, @lName, @sin)";

            SqlCommand cmd = new SqlCommand(cmdstring, conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@fName", SqlDbType.VarChar).Value = fn;
            cmd.Parameters.Add("@lName", SqlDbType.VarChar).Value = ln;
            cmd.Parameters.Add("@sin", SqlDbType.VarChar).Value = sin;
            dt = new DataTable();
            ///Trys to open a connection to the data base
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            ///Makes the search results visible to the user
            searchResultGrid.Visible = true;
            searchFullResultGrid.Visible = false;
            searchResultGrid.DataSource = dt;
            searchResultGrid.DataBind();

            if(dt.Rows.Count == 0) selectResultLabel.Text = "No Result to Display";
            else selectResultLabel.Text = "";
        }
        /// Method:GridView_RowCommand
        /// <summary>
        /// Displays 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                // Retrieve the CommandArgument property
                int index = Convert.ToInt32(e.CommandArgument); // or convert to other datatype
                GridViewRow row = searchResultGrid.Rows[index];
                string sin = row.Cells[1].Text;
                string fname = row.Cells[2].Text;
                string lname = row.Cells[3].Text;
                string company = row.Cells[4].Text;
                string type = row.Cells[5].Text;

                SqlConnection conn = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand(getCmdString(type), conn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@fn", SqlDbType.VarChar).Value = fname;
                cmd.Parameters.Add("@ln", SqlDbType.VarChar).Value = lname;
                cmd.Parameters.Add("@sin", SqlDbType.VarChar).Value = sin;
                cmd.Parameters.Add("@cn", SqlDbType.VarChar).Value = company;
                dt2 = new DataTable();

                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt2);
                }
                catch (SqlException exp)
                {
                    selectResultLabel.Text = "ERROR: " + exp.Message;
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                
                if (dt2.Rows.Count == 0) selectResultLabel.Text += "No Result to Display";
                else selectResultLabel.Text = "";

                searchFullResultGrid.DataSource = dt2;
                searchFullResultGrid.DataBind();
                searchResultGrid.Visible = false;
                searchFullResultGrid.Visible = true;
            }
        }
     /// Method: getCmdString
     /// <summary>
     ///  Depending on the security level 
     ///  of the user, this method will 
     ///  get and display data based on the search string 
     /// </summary>
     /// <param name="type"></param>
     /// <returns></returns>
        private string getCmdString(string type)
        {
            string cmdstring = "";
            switch (type)
            {
                    ///case for Full time if user is admin
                case "FT":
                    if (securityLevel == "1")
                    {
                        cmdstring = "SELECT * FROM dbo.A_DisplayFTEmp(@fn, @ln, @sin, @cn)";
                    }
                        ///else if user is general
                    else if (securityLevel == "2")
                    {
                        cmdstring = "SELECT * FROM dbo.G_DisplayFTEmp(@fn, @ln, @sin, @cn)";
                    }
                    break;
                    ///case for Partime if user is admin
                case "PT":
                    if (securityLevel == "1")
                    {
                        cmdstring = "SELECT * FROM dbo.A_DisplayPTEmp(@fn, @ln, @sin, @cn)";
                    }
                        ///else user is general
                    else if (securityLevel == "2")
                    {
                        cmdstring = "SELECT * FROM dbo.G_DisplayPTEmp(@fn, @ln, @sin, @cn)";
                    }
                    break;
                case "CT":
                    if (securityLevel == "1")
                    {
                        cmdstring = "SELECT * FROM dbo.A_DisplayCTEmp(@fn, @ln, @sin, @cn)";
                    }
                    break;
                    ///Case for seasonal employee if user is admin
                case "SL":
                    if (securityLevel == "1")
                    {
                        cmdstring = "SELECT * FROM dbo.A_DisplaySLEmp(@fn, @ln, @sin, @cn)";
                    }
                     ///else if user is general
                    else if (securityLevel == "2")
                    {
                        cmdstring = "SELECT * FROM dbo.G_DisplaySLPTEmp(@fn, @ln, @sin, @cn)";
                    }
                    break;
            }
            return cmdstring;
        }
    }
}