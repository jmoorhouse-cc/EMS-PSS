using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Supporting
{
    /// <summary>
    /// This class is meant to read and write to the Employee database.
    /// </summary>
    public class FileIO
    {
        /// <summary>
        /// Opens a file for reading based on a path provided
        /// </summary>
        /// <param name="path">A string containing the path of the file to open</param>
        /// <returns>StreamReader pointing to an opened file based on the path given</returns>
        public StreamReader dBaseOpen_R(string path)
        {
            //open the file for reading, making sure it exists TODO
            StreamReader dataFile = new StreamReader(File.OpenRead(path));
            return dataFile;
        }

        /// <summary>
        /// Opens a file for writing based on a path provided
        /// </summary>
        /// <param name="path">A string containing the path of the file to open</param>
        /// <returns>StreamWriter pointing to an opened file based on the path given</returns>
        public StreamWriter dBaseOpen_W(string path)
        {
            //open file for writing, creating it if it doesnt exist TODO
            StreamWriter dataFile = new StreamWriter(File.OpenWrite(path));
            return dataFile;
        }

        /// <summary>
        /// Read the database into an arraylist of strings
        /// </summary>
        /// <param name="file">file to read from</param>
        /// <returns>returns arrayList of entries to the database class</returns>
        public ArrayList readDBase(StreamReader file)
        {
            //TODO - incorporate with dbase class, and make an arraylist of the employee object
            ArrayList dBase = new ArrayList();
            return dBase;
        }

        /// <summary>
        /// Writes the given database to a file
        /// </summary>
        /// <param name="dBase">ArrayList of the database to write</param>
        /// <param name="file">file to write the database to</param>
        /// <returns>bool indicating success</returns>
        public bool writeDbase(ArrayList dBase, StreamWriter file)
        {
            //TODO write the arraylist to a file in a way where it can be read later :)
            return true;
        }
    }
}
