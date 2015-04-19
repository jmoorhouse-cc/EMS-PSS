//
// FILE: Container.cs
// PROJECT: Employee Management System Project
// PROGRAMMERS: Jay Moorhouse, Jordan Poirier, Thom Taylor, Rachel Park
// DATE: 11/13/2014
// DESCRIPTION: Contains the class definition for the Container class in TheCompany class library.
//              Included are the variables, contructors, and methods.
//  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using AllEmployees;
using Supporting;

namespace TheCompany
{
    /// <summary>
    ///  The Container Class to keep track of any number and any type of 4 derived employee-type classes
    ///  The list holds only <b>validated</b> employee objects
    /// </summary>
    public class Container
    {
        string dBaseFile = Path.Combine(Directory.GetCurrentDirectory(), "DBase", "DB.csv"); // dbase filepath
        Logging log = new Logging();
        /// <summary>
        /// The container holding any number and any type of employee-type classes
        /// </summary>
        public List<Object> container;

        /// <summary>
        /// reference to database file connecting to database access layer
        /// </summary>
        private FileIO dbFile;

        /// <summary>
        /// reference to logging class object which is responsible for logging
        /// </summary>
        private Logging logFile;

        /// <summary>
        /// Default constructor used to initialize Container class attributes
        /// </summary>
        public Container()
        {
            container = new List<Object>();
            dbFile = new FileIO();
            logFile = new Logging();
        }

        /// <summary>
        /// Method Name: AddEmployee
        /// This function is called to add the passed-in employee object to the container.
        /// The container must only hold validated employee objects, and
        /// every successful/failed attempt should be logged to the logFile.
        /// </summary>
        /// <param name="newEmployee">This parameter is an object passed in that can be any 1 of the 4 types of employees.</param>
        /// <returns>A boolean indicating whether the adding operation was successful</returns>
        public bool AddEmployee(Object newEmployee)
        {
            bool result = true;
            try
            {
                container.Add(newEmployee);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Method Name: AddFullTimeEmployee
        /// This function is called to add the FullTimeEmployee object that is newly created from the data
        /// the user has put in. 
        /// </summary>
        /// <returns>A boolean indicating whether the adding operation was successful</returns>
        public bool AddFullTimeEmployee(FulltimeEmployee ft)
        {
            bool result = AddEmployee(ft);
            // To do
            return result;
        }

        /// <summary>
        /// Method Name: AddPartTimeEmployee
        /// This function is called to add the PartTimeEmployee object that is newly created from the data
        /// the user has put in. 
        /// </summary>
        /// <returns>A boolean indicating whether the adding operation was successful</returns>
        public bool AddPartTimeEmployee(ParttimeEmployee pt)
        {
            bool result = AddEmployee(pt);
            // To do
            return result;
        }

        /// <summary>
        /// Method Name: AddSeasonalEmployee
        /// This function is called to add the SeasonalEmployee object that is newly created from the data
        /// the user has put in. 
        /// </summary>
        /// <returns>A boolean indicating whether the adding operation was successful</returns>
        public bool AddSeasonalEmployee(SeasonalEmployee sn)
        {
            bool result = AddEmployee(sn);
            // To do
            return result;
        }

        /// <summary>
        /// Method Name: AddContractEmployee
        /// This function is called to add the ContractEmployee object that is newly created from the data
        /// the user has put in. 
        /// </summary>
        /// <returns>A boolean indicating whether the adding operation was successful</returns>
        public bool AddContractEmployee(ContractEmployee ct)
        {
            bool result = AddEmployee(ct);
            // To do
            return result;
        }

        /// <summary>
        /// Method Name: FindEmployee
        /// This function is called to find all the employee objects that matches the passed in filter criteria.
        /// </summary>
        /// <param name="type">the type of attribute that will be searched for: firstName, lastName, dateOfBirth, SIN, employeeType</param>
        /// <param name="value">the value of attribute that will be searched for</param>
        /// <returns>A list of Employee-type objects that matches the filter criteria (if no match, empty list); otherwise NULL</returns>
        public List<Object> FindEmployee(String type, String value)
        {
            List<Object> results = new List<Object>();
            Employee e = new Employee();
            if (type == "firstName")
            {
                foreach (Object r in container)
                {
                    e = (Employee)r;
                    if (e.GetFirstName() == value)
                    {
                        results.Add(r);
                    }
                }
            }
            else if (type == "lastName")
            {
                foreach (Object r in container)
                {
                    e = (Employee)r;
                    if (e.GetLastName() == value)
                    {
                        results.Add(r);
                    }
                }
            }
            else if (type == "SIN")
            {
                foreach (Object r in container)
                {
                    e = (Employee)r;
                    if (e.GetSIN() == value)
                    {
                        results.Add(r);
                    }
                }
            }
            return results;
        }

        /// <summary>
        /// Method Name: FindAndDisplay
        /// This function is called to find and display data of one or more employee objects of a specific type
        /// that the user is interested in.
        /// </summary>
        /// <param name="type">the type of attribute that will be searched for</param>
        /// <param name="value">the value of attribute that will be searched for</param>
        /// <returns>A boolean indicating whether the adding operation was successful</returns>
        public bool FindAndDisplay(String type, String value)
        {
            bool result = false;
            // To do
            return result;
        }

        /// <summary>
        /// Display function that list all the employee object details inside the container
        /// </summary>
        public Int32 Display()
        {
            Console.Clear();
            int count = 0;
            Employee e = new Employee();
            foreach (Object emp in container)
            {
                e = (Employee)emp;
                e.Details(true);
                count++;
            }
            return count;
        }

        /// <summary>
        /// Method Name: SaveContainer
        /// The function is called to save the data inside the the container object to a file. 
        /// </summary>
        /// <param name="fileName">the Name of the database file</param>
        /// <returns>A boolean indicating whether the operation was successful</returns>
        public bool SaveContainer()
        {
            bool result = false;
            Employee e = new Employee();
            List<String[]> list = new List<String[]>();
            // TODO: fill the string[] with the contents of the employee object
            foreach (Object emp in container)
            {
                e = (Employee)emp;
                
                list.Add(e.DatabaseDetails().ToArray());
            }
            if (container.Count != 0)
            {
                foreach (var emp in list)
                {
                    dbFile.dBaseOpen_W(emp, true);
                }
                result = (list.Count == container.Count);
            }
            return result;
        }

        /// <summary>
        /// Method Name: LoadContainer
        /// The function is called to populate the container object with the data from the database file 
        /// </summary>
        /// <param name="fileName">the Name of the database file</param>
        /// <returns>A boolean indicating whether the operation was successful</returns>
        public bool LoadContainer()
        {
            bool result = true;
            string[] fileContents; // create string to hold file contents
            string[] input;
            Int32 validCount = 0;
            Int32 lineCount = 0;
            Int32 entryCount = 0;
            Employee e = new Employee();
            bool duplicate = false;
            fileContents = dbFile.dBaseOpen_R(); // fill the string with file contents
            container.Clear();
            Console.Clear();
            foreach (string s in fileContents)
            {
                lineCount++;
                if (s != "")
                {
                    input = s.Split('|');
                    if (s[0] == 'F')
                    {
                        entryCount++;
                        FulltimeEmployee f = new FulltimeEmployee();
                        if (!f.SetLastName(input[1]) || !f.SetFirstName(input[2]) || !f.SetSIN(input[3]) || !f.SetDOB(input[4]) || !f.SetDateOfHire(input[5]) || !f.SetDateOfTermination(input[6]) || !f.SetSalary(input[7]))
                        {
                            log.writeLog(f.produceLogString("LOAD", "1 entry corrupt", s, "FAILED"));
                            result = false;
                        }
                        else
                        {
                            foreach (Object o in container)
                            {
                                e = (Employee)o;
                                if (e.GetSIN() == f.GetSIN())
                                {
                                    duplicate = true;
                                }
                            }
                            if (duplicate == false)
                            {
                                container.Add(f);
                                validCount++;
                            }
                            else
                            {
                                log.writeLog(f.produceLogString("LOAD", "1 entry corrupt", s, "FAILED"));
                                duplicate = false;
                                result = false;
                            }
                        }

                    }
                    else if (s[0] == 'P')
                    {
                        entryCount++;
                        ParttimeEmployee p = new ParttimeEmployee();
                        if (!p.SetLastName(input[1]) || !p.SetFirstName(input[2]) || !p.SetSIN(input[3]) || !p.SetDOB(input[4]) || !p.SetDateOfHire(input[5]) || !p.SetDateOfTermination(input[6]) || !p.SetHourlyRate(input[7]))
                        {
                            log.writeLog(p.produceLogString("LOAD", "1 entry corrupt", s, "FAILED"));
                            result = false;
                        }
                        else
                        {
                            foreach (Object o in container)
                            {
                                e = (Employee)o;
                                if (e.GetSIN() == p.GetSIN())
                                {
                                    duplicate = true;
                                }
                            }
                            if (duplicate == false)
                            {
                                container.Add(p);
                                validCount++;
                            }
                            else
                            {
                                log.writeLog(p.produceLogString("LOAD", "1 entry corrupt", s, "FAILED"));
                                duplicate = false;
                                result = false;
                            }
                        }
                    }
                    else if (s[0] == 'C')
                    {
                        entryCount++;
                        ContractEmployee c = new ContractEmployee();
                        if (!c.SetLastName(input[1]) || !c.SetFirstName(input[2]) || !c.SetSIN(input[3]) || !c.SetDOB(input[4]) || !c.SetContractStartDate(input[5]) || !c.SetContractEndDate(input[6]) || !c.SetFixedContractAmt(input[7]))
                        {
                            log.writeLog(c.produceLogString("LOAD", "1 entry corrupt", s, "FAILED"));
                            result = false;
                        }
                        else
                        {
                             foreach (Object o in container)
                            {
                                e = (Employee)o;
                                if (e.GetSIN() == c.GetSIN())
                                {
                                    duplicate = true;
                                }
                            }
                             if (duplicate == false)
                             {
                                 container.Add(c);
                                 validCount++;
                             }
                             else
                             {
                                 log.writeLog(c.produceLogString("LOAD", "1 entry corrupt", s, "FAILED"));
                                 duplicate = false;
                                 result = false;
                             }
                        }
                    }
                    else if (s[0] == 'S')
                    {
                        entryCount++;
                        SeasonalEmployee n = new SeasonalEmployee();
                        if (!n.SetLastName(input[1]) || !n.SetFirstName(input[2]) || !n.SetSIN(input[3]) || !n.SetDOB(input[4]) || !n.SetSeason(input[5]) || !n.SetPiecePay(input[6]))
                        {
                            log.writeLog(n.produceLogString("LOAD", "1 entry corrupt", s, "FAILED"));
                            result = false;
                        }
                        else
                        {
                            foreach (Object o in container)
                            {
                                e = (Employee)o;
                                if (e.GetSIN() == n.GetSIN())
                                {
                                    duplicate = true;
                                }
                            }
                            if (duplicate == false)
                            {
                                container.Add(n);
                                validCount++;
                            }
                            else
                            {
                                log.writeLog(n.produceLogString("LOAD", "1 entry corrupt", s, "FAILED"));
                                duplicate = false;
                                result = false;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("\n\n>>>>>>>>>>>>>>>> TOTAL LINES READ           : " + lineCount);
            Console.WriteLine(    ">>>>>>>>>>>>>>>> TOTAL ENTRIES FOUND        : " + entryCount);
            Console.WriteLine(    ">>>>>>>>>>>>>>>> TOTAL VALID ENTRIES LOADED : " + validCount);
            return result;
        }
    }
}
