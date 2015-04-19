//
// FILE: FIleIO.cs
// PROJECT: Employee Management System Project
// PROGRAMMERS: Jay Moorhouse, Jordan Poirier, Thom Taylor, Rachel Park
// DATE: 11/13/2014
// DESCRIPTION: Class contains methods to read and write from/to the database file.
//   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Reflection;


namespace Supporting
{
    /// <summary>
    /// This class is meant to read and write to the Employee database.
    /// </summary>
    public class FileIO
    {
        Logging log = new Logging();
        string dbasePath = Directory.GetCurrentDirectory();
        string dbFilePath = Path.Combine(Directory.GetCurrentDirectory(), "DBase", "DB.csv");

        /// <summary>
        /// Opens a file from the DBase folder of the application and read in each 
        /// pipe-delimited value to a string array to return to caller until newline.
        /// </summary>
        /// <param name="path">A string containing the path of the file to open</param>
        public string[] dBaseOpen_R()
        {
            // if the file exists, open it
            if (File.Exists(dbFilePath))
            {
                string[] fileLines;
                char[] newLine = new char[2] {'\n','\r'};

                // open the file for reading and
                // extract dbase info 
                using (StreamReader dBase_R = new StreamReader(File.OpenRead(dbFilePath)))
                {
                    fileLines = dBase_R.ReadToEnd().Split(newLine);
                    dBase_R.Close();
                }
                return fileLines;

            }
            else
            {
                //file doesn't exist
                log.writeLog("OPEN DBFile - FAILED : File not found");
                return null;
            }
        }

        /// <summary>
        /// Opens a file for writing in the DBase folder of the application directory, and write
        /// the parameter string array elements to the file, delimited by pipes.
        /// </summary>
        /// <param name="path">A string containing the path of the file to open</param>
        public void dBaseOpen_W(string[] employeeData, bool overwrite)
        {
            // create the DBase folder if it doesn't already exist
            if (!Directory.Exists(Path.Combine(dbasePath, "DBase")))
            {
                Directory.CreateDirectory(Path.Combine(dbasePath, "DBase"));
            }

            if (System.IO.File.Exists(dbFilePath) || overwrite == true)
            {
                System.IO.File.Delete(dbFilePath); //try/catch exception handling needs to be implemented
            }

            // write to dbase file (formatted)
            using (StreamWriter dBase_W = File.AppendText(dbFilePath))
            {
                // write each string in the array to the file, separated by pipes
                foreach (string s in employeeData)
                {
                    dBase_W.Write(s + "|");
                }
                dBase_W.Write("\n"); // end of current entry, add new line
                dBase_W.Close();
            }
        }
        public void dBaseEmpty()
        {
            // create the DBase folder if it doesn't already exist
            if (!Directory.Exists(Path.Combine(dbasePath, "DBase")))
            {
                Directory.CreateDirectory(Path.Combine(dbasePath, "DBase"));
            }

            if (System.IO.File.Exists(dbFilePath))
            {
                System.IO.File.Delete(dbFilePath); //try/catch exception handling needs to be implemented
                System.IO.File.Create(dbFilePath);
            }
        }
    }
}
