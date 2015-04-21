using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Data;              // To connect to MSSql Server
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace Supporting
{
    public class Audit
    {
        public void CreateAudit(string conString, string userName, string empID, string changedElement, string oldVal, string newVal)
        {
            SqlDateTime sqlTime = (SqlDateTime)DateTime.Now;

            SqlConnection conn = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("INSERT INTO dbo.tb_Audit VALUES (@userName, @timechanged, @empID, @fieldChanged, @oldEntry, @newEntry)", conn);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@timechanged";
            parameter.SqlDbType = SqlDbType.DateTime2;
            parameter.Value = DateTime.Parse(DateTime.Now.ToString());

            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Parameters.Add(parameter);
            cmd.Parameters.AddWithValue("@empID", empID);
            cmd.Parameters.AddWithValue("@fieldChanged", changedElement);
            cmd.Parameters.AddWithValue("@oldEntry", oldVal);
            cmd.Parameters.AddWithValue("@newEntry", newVal);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            } 
        }

        public string GetPreviousValue(string conString, int empID, string column, string empType)
        {
            int index = column.IndexOf('=');
            string fieldValue = column.Substring(0, column.IndexOf('='));
            string result;

            // empID, string "test='value'"
            
            string table = "";
            switch (empType)
            {
                case "FT":
                    table = "dbo.tb_FtEmp";
                    break;

                case "PT":
                    table = "dbo.tb_PtEmp";
                    break;

                case "SL":
                    table = "dbo.tb_SlEmp";
                    break;

                case "CT":
                    table = "dbo.tb_CtEmp";
                    break;

                default:
                    table = "dbo.tb_Emp";
                    break;
            }

            SqlConnection conn = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("SELECT " + fieldValue + " FROM " + table + " WHERE empID= " + empID, conn);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                result = Convert.ToString(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            } 
            return result;
        }
    }
}
