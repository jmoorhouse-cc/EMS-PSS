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

namespace Supporting
{
    class Audit
    {
        public static void CreateAudit(string conString, string changedElement, string oldVal, string newVal, string uID, string empID)
        {
            DateTime time = DateTime.Now;
            string dateTimeStamp = time.ToString("yyy-MM-dd hh:mm:ss");

            SqlConnection conn = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("INSERT INTO dbo.tb_Audit VALUES (@timechanged, @empID, @userID, @oldEntry, @newEntry)", conn);
            cmd.Parameters.AddWithValue("@timechanged", dateTimeStamp);
            cmd.Parameters.AddWithValue("@empID", empID);
            cmd.Parameters.AddWithValue("@userID", uID);
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
    }
}
