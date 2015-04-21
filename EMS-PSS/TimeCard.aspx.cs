using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;              // To connect to MSSql Server
using System.Data.SqlClient;
using Supporting;
using System.Globalization;

namespace EMS_PSS
{
    public partial class TimeCard : System.Web.UI.Page
    {
        string securityLevel;
        string userName;
        string conString;
        static DataTable dt, dt2;
        static string selectedEmpType;
        static string selectedEmpId;
        TimeCardManager tcm;
        static DateTime maxDate, minDate;
        static GridView searchFullResult, searchResult;
        protected void Page_Load(object sender, EventArgs e)
        {
            securityLevel = Session["securitylevel"].ToString();
            userName = Session["username"].ToString();
            conString = Session["conString"].ToString();
            searchFullResultGrid.Visible = false;
            hideTimeCardField();
            searchResult = new GridView();
            searchFullResult = new GridView();
        }

        protected void searchSubmit_Click(object sender, EventArgs e)
        {
            searchFullResultGrid.Visible = false;
            hideTimeCardField();
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
            searchResultGrid.Visible = true;
            searchResultGrid.DataSource = dt;
            searchResultGrid.DataBind();
            searchResult.DataSource = dt;
            searchResult.DataBind();
            if (dt.Rows.Count == 0)
            {
                selectResultLabel.Text = "No Result to Display";
                hideTimeCardField();
            }
            else selectResultLabel.Text = "";
        }
        public DateTime? ReturnDateIfValid(String dateStr)
        {
            DateTime date = DateTime.MinValue;
            DateTime? dateNull = null;
            var formats = new[] {   "dd-MM-yyyy", "d-MM-yyyy", "dd-M-yyyy", "d-M-yyyy",
                                    "yyyy-MM-dd", "yyyy-MM-d", "yyyy-M-dd", "yyyy-M-d", 
                                    "dd/MM/yyyy", "d/MM/yyyy", "dd/M/yyyy", "d/M/yyyy",
                                    "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d"};
            dateStr = dateStr.Trim();
            dateStr = dateStr.Replace(" ", "");
            if (DateTime.TryParseExact(dateStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return date;
            }
            else
            {
                return dateNull;
            }

        }
        protected void tcInsert_Click(object sender, EventArgs e)
        {
            showTimeCardField();
            errorMsg.Text = "";
            DateTime temp = new DateTime();
            DateTime? test = new DateTime();
            test = ReturnDateIfValid(tcWeekDate.Text);
            if (test == null)
            {
                errorMsg.Text = "<b>Invalid date or format</b>";
            }
            else
            {
                temp = Convert.ToDateTime(test.ToString());
                tcm = new TimeCardManager(temp);
                if (minDate > tcm.CalcMonDate(temp) || maxDate < tcm.CalcSunDate(temp))
                {
                    errorMsg.Text = "<b>Input Date Out Of Range</b>: Valid Between " + minDate.ToString("yyyy-MM-dd") + " and " + maxDate.ToString("yyyy-MM-dd");

                }
                else
                {
                    DateTime dt = tcm.CalcSunDate(temp);
                    tcm.timeCardDate = dt;
                    tcm.empID = Convert.ToInt32(selectedEmpId);
                    int numErrors = setTimeCardManager();
                    if (numErrors > 0)
                    {
                        showTimeCardField();
                        errorMsg.Text = numErrors + "<b>Invalid Hour/Piece Entry(s) found.</b> Try again.";
                    }
                    else
                    {
                        int rowsReturned;
                        bool success = false;
                        try
                        {
                            using (SqlConnection conn = new SqlConnection(conString))
                            {
                                conn.Open();
                                using (SqlCommand cmd = new SqlCommand())
                                {
                                    cmd.Connection = conn;
                                    cmd.CommandType = CommandType.Text;

                                    cmd.CommandText = tcm.ToDBCheckDupString();
                                    rowsReturned = (int)cmd.ExecuteScalar();
                                    if (rowsReturned != 0)
                                    {
                                        errorMsg.Text = ("Duplicate Time Card Entry Exists; Attempt To Updated; ");
                                        cmd.CommandText = tcm.ToDBUpdateString(selectedEmpType);
                                        rowsReturned = cmd.ExecuteNonQuery();
                                        if (rowsReturned != 1)
                                        {
                                            errorMsg.Text += ("<b>Time Card Entry Has Not Been Updated</b>");
                                        }
                                        else
                                        {
                                            errorMsg.Text += ("<b>Time Card Entry Has Been Updated Successfully</b>");
                                            insertBtn.Visible = false;
                                        }
                                    }
                                    else
                                    {
                                        cmd.CommandText = tcm.ToDBAddString(selectedEmpType);

                                        rowsReturned = cmd.ExecuteNonQuery();
                                        if (rowsReturned != 1)
                                        {
                                            errorMsg.Text = ("<b>Time Card Entry Has Not Been Added</b>");
                                        }
                                        else
                                        {
                                            errorMsg.Text = ("<b>Time Card Entry Has Been Added Successfully</b>");
                                            insertBtn.Visible = false;
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                            errorMsg.Text = ("<b>Time Card Entry Has Failed");
                            hideTimeCardField();
                        }
                    }
                }
            }
        }
        
        private int setTimeCardManager()
        {
            Decimal temp = 0;
            int invalidEntry = 0;
            if (Decimal.TryParse(sunHInput.Text, out temp)) { if (!tcm.SetHours("SUN", temp)) invalidEntry++; }
            if (Decimal.TryParse(monHInput.Text, out temp)) { if (!tcm.SetHours("MON", temp)) invalidEntry++; }
            if (Decimal.TryParse(tueHInput.Text, out temp)) { if (!tcm.SetHours("TUE", temp)) invalidEntry++; }
            if (Decimal.TryParse(wedHInput.Text, out temp)) { if (!tcm.SetHours("WED", temp)) invalidEntry++; }
            if (Decimal.TryParse(thuHInput.Text, out temp)) { if (!tcm.SetHours("THU", temp)) invalidEntry++; }
            if (Decimal.TryParse(friHInput.Text, out temp)) { if (!tcm.SetHours("FRI", temp)) invalidEntry++; }
            if (Decimal.TryParse(satHInput.Text, out temp)) { if (!tcm.SetHours("SAT", temp)) invalidEntry++; }
            if(securityLevel == "1")
            {
                if (Decimal.TryParse(sunPInput.Text, out temp)) { if (!tcm.SetPieces("SUN", temp)) invalidEntry++; }
                if (Decimal.TryParse(monPInput.Text, out temp)) { if (!tcm.SetPieces("MON", temp)) invalidEntry++; }
                if (Decimal.TryParse(tuePInput.Text, out temp)) { if (!tcm.SetPieces("TUE", temp)) invalidEntry++; }
                if (Decimal.TryParse(wedPInput.Text, out temp)) { if (!!tcm.SetPieces("WED", temp)) invalidEntry++; }
                if (Decimal.TryParse(thuPInput.Text, out temp)) { if (!tcm.SetPieces("THU", temp)) invalidEntry++; }
                if (Decimal.TryParse(friPInput.Text, out temp)) { if (!tcm.SetPieces("FRI", temp)) invalidEntry++; }
                if (Decimal.TryParse(satPInput.Text, out temp)) { if (!tcm.SetPieces("SAT", temp)) invalidEntry++; }
            }
            return invalidEntry;
        }
        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                // Retrieve the CommandArgument property
                int index = Convert.ToInt32(e.CommandArgument); // or convert to other datatype
                GridViewRow row = searchResultGrid.Rows[index];
                selectedEmpId = row.Cells[1].Text;
                selectedEmpType = row.Cells[6].Text;
                SqlConnection conn = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand(getCmdString(selectedEmpType), conn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@eid", SqlDbType.Int).Value = selectedEmpId;

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
                searchFullResult.DataSource = dt2;
                searchFullResultGrid.DataBind();
                searchFullResult.DataBind();
                searchResultGrid.Visible = false;
                searchFullResultGrid.Visible = true;
                showTimeCardField();

                GridViewRow row2 = searchFullResultGrid.Rows[0];
                
                if(selectedEmpType == "FT" || selectedEmpType == "PT")
                {
                    string startDate = row2.Cells[5].Text.Replace(" 12:00:00 AM", "");
                    string endDate = row2.Cells[6].Text.Replace(" 12:00:00 AM", "");
                    minDate = Convert.ToDateTime(startDate);
                    maxDate = Convert.ToDateTime(endDate);
                }
                else if(selectedEmpType == "SL")
                {
                    string season = row2.Cells[5].Text;
                    string year = row2.Cells[6].Text;
                    string start="", end="";

                    if(season == "WINTER")
                    {
                        start = "-12-01";
                        end = "-04-30";
                    }
                    else if (season == "FALL")
                    {
                        start = "-09-01";
                        end = "-11-30";
                    }
                    else if (season == "SUMMER")
                    {
                        start = "-07-01";
                        end = "-08-31";
                    }
                    else if (season == "SPRING")
                    {
                        start = "-5-01";
                        end = "-06-30";
                    }
                    minDate = Convert.ToDateTime(year+start);
                    maxDate = Convert.ToDateTime(year+end);
                }

                searchFullResultGrid.Visible = true;
            }
        }
        private string getCmdString(string type)
        {
            string cmdstring = "";
            switch (type)
            {
                case "FT":
                    if (securityLevel == "1")
                    {
                        cmdstring = "SELECT * FROM dbo.A_DisplayFTEmp(@eid)";
                    }
                    else if (securityLevel == "2")
                    {
                        cmdstring = "SELECT * FROM dbo.G_DisplayFTEmp(@eid)";
                    }
                    break;
                case "PT":
                    if (securityLevel == "1")
                    {
                        cmdstring = "SELECT * FROM dbo.A_DisplayPTEmp(@eid)";
                    }
                    else if (securityLevel == "2")
                    {
                        cmdstring = "SELECT * FROM dbo.G_DisplayPTEmp(@eid)";
                    }
                    break;
                case "CT":
                    if (securityLevel == "1")
                    {
                        cmdstring = "SELECT * FROM dbo.A_DisplayCTEmp(@eid)";
                    }
                    break;
                case "SL":
                    if (securityLevel == "1")
                    {
                        cmdstring = "SELECT * FROM dbo.A_DisplaySLEmp(@eid)";
                    }
                    else if (securityLevel == "2")
                    {
                        cmdstring = "SELECT * FROM dbo.G_DisplaySLPTEmp(@eid)";
                    }
                    break;
            }
            return cmdstring;
        }
        private void showTimeCardField()
        {
            tcDateInputTable.Visible = true;
            hourInputTable.Visible = true;
            if (selectedEmpType.ToUpper() == "SL") pieceInputTable.Visible = true;
            else pieceInputTable.Visible = false;
            errorMsg.Text = "";
        }
        private void hideTimeCardField()
        {
            tcDateInputTable.Visible = false;
            hourInputTable.Visible = false;
            pieceInputTable.Visible = false;
            errorMsg.Text = "";
        }
    }
}