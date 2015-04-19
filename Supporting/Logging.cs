using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Supporting
{
    /// <summary>
    /// Logs events from other classes in this project
    /// </summary>
    public class Logging
    {
        /// <summary>
        /// Writes an entry to the log
        /// </summary>
        /// <param name="logEvent">string containing event to write</param>
        /// <returns>Bool indicating success or failure</returns>
        public bool writeLog(string logEvent)
        {
            //DateTime time = DateTime.Now;
            //StackFrame frame = new StackFrame(1); // note the stack layout
            //string currentDate = DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.GetCultureInfo("en-US")); // formatted current time
            //string fileName = "ems." + currentDate + ".log"; // formatted filename to open (create)
            //string timeStamp = time.ToString("yyy-MM-dd hh:mm:ss"); // formatted timestamp for in the log file
            //string callingMethod = frame.GetMethod().Name; // name of calling method
            //string callingClass = frame.GetMethod().DeclaringType.ToString(); // name of calling class
            //string entry = ""; // the entry written
            //bool succeeded = false; // return value

            //using (StreamWriter w = File.AppendText(fileName))
            //{
            //    entry = "\r\n\r\n" + timeStamp + " " + "[" + callingClass + "." + callingMethod + "] " + "\r\n" + logEvent;
            //    w.Write(entry);
            //    w.Close();
            //}

            //// check the file just written to for the entry to ensure it was successfully written
            //using (StreamReader sr = new StreamReader(fileName))
            //{
            //    // check each line
            //    foreach (string line in File.ReadLines(fileName))
            //    {
            //        // entry was found, write successful
            //        if (line.Contains(logEvent))
            //        {
            //            succeeded = true;
            //            break;
            //        }
            //    }
            //}
            //return succeeded;
            return true;
        }
    }
}
