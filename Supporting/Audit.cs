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
        public static void CreateAudit(string conString, string userName, string empID, string changedElement, string oldVal, string newVal)
        {
            SqlDateTime sqlTime = (SqlDateTime)DateTime.Now;

            SqlConnection conn = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("INSERT INTO dbo.tb_Audit VALUES (@userName, @timechanged, @empID, @oldEntry, @newEntry)", conn);

            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Parameters.AddWithValue("@timechanged", sqlTime);
            cmd.Parameters.AddWithValue("@empID", empID);
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
