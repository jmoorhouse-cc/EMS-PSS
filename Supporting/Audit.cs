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
        public static void CreateAudit(string conString, string changedElement, string oldVal, string newVal, string uID, string empID)
        {
            //DateTime time = DateTime.Now;
            //string format = "yyyy-MM-d" + "T" + "HH:mm:ss";
            //string sqlTime = time.ToString(format);
            //string goodSqlTime = "'" + sqlTime + "'";
            //SqlDateTime sqlTime = (SqlDateTime)DateTime.Now;

            SqlConnection conn = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("INSERT INTO dbo.tb_Audit VALUES (@timechanged, @empID, @userID, @oldEntry, @newEntry)", conn);

            cmd.Parameters.AddWithValue("@timechanged", SqlDbType.DateTime);
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
