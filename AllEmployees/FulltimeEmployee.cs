//
// FILE: FulltimeEmployees.cs
// PROJECT: Employee Management System Project
// PROGRAMMERS: Jay Moorhouse, Jordan Poirier, Thom Taylor, Rachel Park
// DATE: 11/13/2014
// DESCRIPTION: Contains the class definition for the FulltimeEmployees child class.
//              Included are the variables, contructors, and methods.
//              Inherited from Employee.cs
//  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supporting;

namespace AllEmployees
{
    /// <summary>
    /// The FulltimeEmployee Class, a child class of Employee
    /// </summary>
    public sealed class FulltimeEmployee : Employee
    {
        private DateTime? dateOfHire;
        private DateTime? dateOfTermination;
        private Decimal salary;

        /// <summary>
        /// The FulltimeEmployee() method is a Constructor for the FulltimeEmployee Class.
        /// This version of the constructor initializes all values to default (blank/0).
        /// </summary>
        public FulltimeEmployee()
            : base()
        {
            dateOfHire = null;
            dateOfTermination = null;
            salary = 0.00M;

            SetType("FT");
        }

        public FulltimeEmployee(FulltimeEmployee prev)
            : base(prev)
        {
            dateOfHire = prev.GetDateOfHire();
            dateOfTermination = prev.GetDateOfTermination();
            salary = prev.GetSalary();

            SetType("FT");
        }

        /// <summary>
        /// Overloaded  FulltimeEmployee constructor.
        /// This version of the constructor sets all variables to parameter values
        /// </summary>
        /// <param name="first">The string to initialize the firstName variable to</param>
        /// <param name="last">The string to initialize the lastName variable to</param>
        /// <param name="SIN">The string to initialize the socialInsuranceNumber variable to</param>
        /// <param name="DOB">The date to set the dateOfBirth variable to</param>
        /// <param name="hDate">The date to set the dateOfHire variable to</param>
        /// <param name="tDate">The date to set the dateOfTermination variable to</param>
        /// <param name="inSalary">The string to set the Salary variable to</param>
        public FulltimeEmployee(String first, String last, String SIN, DateTime? DOB, DateTime? hDate,
            DateTime? tDate, Decimal inSalary)
            : base(first, last, SIN, DOB)
        {
            dateOfTermination = tDate;
            dateOfHire = hDate;
            salary = inSalary;
            SetType("FT");
        }

        /// <summary>
        /// getter for dateOfHire
        /// </summary>
        /// <returns>the dateOfHire value</returns>
        public DateTime? GetDateOfHire()
        {
            return dateOfHire;
        }

        /// <summary>
        /// getter for dateOfTermination
        /// </summary>
        /// <returns>the dateOfTermination variable</returns>
        public DateTime? GetDateOfTermination()
        {
            return dateOfTermination;
        }

        /// <summary>
        /// getter for salary
        /// </summary>
        /// <returns>salary variable</returns>
        public Decimal GetSalary()
        {
            return salary;
        }

        /// <summary>
        /// The setter for dateOfHire
        /// </summary>
        /// <param name="hDate">The date to set the dateOfHire variable to</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool SetDateOfHire(DateTime? hDate)
        {
            bool retV = false;
            log.writeLog(
                produceLogString("SET",
                                (dateOfHire.HasValue ? dateOfHire.Value.ToString("yyyy-MM-dd") : "N/A"),
                                (hDate.HasValue ? hDate.Value.ToString("yyyy-MM-dd") : "N/A"),
                                "SUCCESS"));
            dateOfHire = hDate;
            retV = true;
            return retV;
        }

        /// <summary>
        /// setter for dateOfHire
        /// </summary>
        /// <param name="hDateStr">the date in string format to set</param>
        /// <returns></returns>
        public bool SetDateOfHire(String hDateStr)
        {
            bool retV = false;
            DateTime? date = null;
            if (hDateStr == "N/A")
            {
                retV = SetDateOfHire(date);
                retV = true;
            }
            /// validate it first
            else if ((date = ReturnDateIfValid(hDateStr)) != null)
            {
                retV = SetDateOfHire(date);
                retV = true;
            }
            return retV;
        }

        /// <summary>
        /// The setter for dateOfTermination (datetime)
        /// </summary>
        /// <param name="tDate">The date to set the dateOfTermination variable to in DateTime format</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool SetDateOfTermination(DateTime? tDate)
        {
            bool retV = false;
            log.writeLog(
                produceLogString("SET",
                                (dateOfTermination.HasValue ? dateOfTermination.Value.ToString("yyyy-MM-dd") : "N/A"),
                                (tDate.HasValue ? tDate.Value.ToString("yyyy-MM-dd") : "N/A"),
                                "SUCCESS"));
            dateOfTermination = tDate;
            retV = true;
            return retV;
        }

        /// <summary>
        /// setter for dateOfTermination (string)
        /// </summary>
        /// <param name="tDateStr">the date to set the dateOfTermination variable to in string format</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool SetDateOfTermination(String tDateStr)
        {
            bool retV = false;
            DateTime? date = null;
            if (tDateStr == "N/A")
            {
                retV = SetDateOfTermination(date);
                retV = true;
            }
            else if ((date = ReturnDateIfValid(tDateStr)) != null)
            {
                retV = SetDateOfTermination(date);
                retV = true;
            }
            return retV;
        }

        /// <summary>
        /// check method for the salary field - field validation (decimal)
        /// </summary>
        /// <param name="sal">the decimal value of salary to check</param>
        /// <returns>A boolean indicating whether the field is valid</returns>
        public bool CheckSalary(Decimal sal)
        {
            return sal > 0;
        }

        /// <summary>
        /// check method for salary field - field validation (string)
        /// </summary>
        /// <param name="sal">the string value of salary to check</param>
        /// <returns>A boolean indicating whether the field is valid</returns>
        public bool CheckSalary(String sal)
        {
            Decimal newSal;
            return Decimal.TryParse(sal, out newSal) && CheckSalary(newSal);
        }

        /// <summary>
        /// The setter for salary
        /// </summary>
        /// <param name="inSalary">The string to set the salary variable to</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool SetSalary(Decimal inSalary)
        {
            bool retV = false;
            if (CheckSalary(inSalary) == true)
            {
                log.writeLog(produceLogString("SET", salary.ToString("0.00"), inSalary.ToString("0.00"), "SUCCESS"));
                salary = inSalary;
                retV = true;
            }
            else
            {
                log.writeLog(produceLogString("SET", salary.ToString("0.00"), inSalary.ToString("0.00"), "FAIL")
                    + "\nDetail: Salary cannot be lower than zero");
            }
            return retV;
        }

        /// <summary>
        /// The setter for salary
        /// </summary>
        /// <param name="inSalary">The string to set the salary variable to</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool SetSalary(String inSalary)
        {
            bool retV = false;
            Decimal newSal;
            if (Decimal.TryParse(inSalary, out newSal))
            {
                SetSalary(newSal);
                retV = true;
            }
            return retV;
        }

        /// <summary>
        /// validation method for dateOfHire
        /// </summary>
        /// <returns>A boolean indicating whether the field is valid</returns>
        private bool validateDOH()
        {
            bool retV = false;
            /// field can't be null
            if (dateOfHire == null)
            {
                log.writeLog(produceLogString("VALIDATE", "", "N/A", "FAIL")
                                            + "\nDetails: Date of Hire cannot be NULL\n");
            }
            else
            {
                DateTime newDOH = (DateTime)dateOfHire;
                DateTime DOH_MinDate = (dateOfBirth.HasValue ? (DateTime)dateOfBirth : DateTime.Now);
                DateTime DOH_MaxDate = (dateOfTermination.HasValue ? (DateTime)dateOfTermination : DateTime.Now);
                /// check the range on the date
                if (CheckDateRange(DOH_MinDate, DOH_MaxDate, newDOH) == true)
                {
                    /// log
                    log.writeLog(
                        produceLogString("VALIDATE", "", newDOH.ToString("yyyy-MM-dd"), "SUCCESS")
                                        + "\nDetails: Date Of Hire comes after " + DOH_MinDate.ToString("yyyy-MM-dd") + "& before " + DOH_MaxDate.ToString("yyyy-MM-dd") + "\n");
                    retV = true;
                }
                else
                {
                    /// log
                    log.writeLog(
                        produceLogString("VALIDATE", "", DateTime.Now.ToString("yyyy-MM-dd"), "FAIL")
                                        + "\nDetails: Date Of Hire must come after " + DOH_MinDate.ToString("yyyy-MM-dd") + ", before " + DOH_MaxDate.ToString("yyyy-MM-dd") + "\n");
                }
            }
            return retV;
        }

        /// <summary>
        /// validateion method for dateOfTermination
        /// </summary>
        /// <returns>A boolean indicating whether the field is valid</returns>
        private bool validateDOT()
        {
            bool retV = false;
            /// field can be null
            if (dateOfTermination == null)
            {
                log.writeLog(produceLogString("VALIDATE", "", "N/A", "SUCCESS"));
                retV = true;
            }
            else
            {
                DateTime DOT_MinDate = (DateTime)dateOfHire;
                /// check the range of the date
                if (CheckDateRange(DOT_MinDate, DateTime.MaxValue, (DateTime)dateOfTermination) == true)
                {
                    /// log
                    log.writeLog(
                        produceLogString("VALIDATE", "", ((DateTime)dateOfTermination).ToString("yyyy-MM-dd"), "SUCCESS")
                                        + "\nDetails: Date Of Termination comes after " + DOT_MinDate.ToString("yyyy-MM-dd") + "\n");
                    retV = true;
                }
                else
                {
                    /// log
                    log.writeLog(
                        produceLogString("VALIDATE", "", ((DateTime)dateOfTermination).ToString("yyyy-MM-dd"), "FAIL")
                                        + "\nDetails: Date Of Termination must come after " + DOT_MinDate.ToString("yyyy-MM-dd") + "\n");
                }
            }
            return retV;
        }

        /// <summary>
        /// validation method for Salary
        /// </summary>
        /// <returns>A boolean indicating whether the field is valid</returns>
        private bool validateSalary()
        {
            bool retV = false;

            // check it and compare it to 0
            if (CheckSalary(salary) && salary != 0)
            {
                log.writeLog(produceLogString("VALIDATE", "", salary.ToString("0.00"), "SUCCESS"));
                retV = true;
            }
            else
            {
                log.writeLog(produceLogString("VALIDATE", "", salary.ToString("0.00"), "FAIL") + "\nDetails: Salary must be bigger than 0\n");

            }
            return retV;
        }

        /// <summary>
        /// A function that will be called to print details of employee object to database file.
        /// </summary>
        /// <returns>A String containinng the detailed information of the employee object that will be printed to the database file</returns>
        public override List<String> DatabaseDetails()
        {
            List<String> output = new List<String>();
                output.AddRange(
                    new String[] {"FT", lastName, firstName, socialInsuranceNumber, 
                                (dateOfBirth.HasValue ? dateOfBirth.Value.ToString("yyyy-MM-dd") : "N/A"), (dateOfHire.HasValue ? dateOfHire.Value.ToString("yyyy-MM-dd") : "N/A")
                                , (dateOfTermination.HasValue ? dateOfTermination.Value.ToString("yyyy-MM-dd") : "N/A"), salary.ToString()});
            return output;
        }

        /// <summary>
        /// method to validate the fulltime employee object
        /// </summary>
        /// <returns>A boolean indicating whether the field is valid</returns>
        public override String Validate()
        {
            String output = "";
            String msg = "";
            if ((msg = base.Validate()).Length > 0)
            {
                output += msg;
            }
            if (validateDOH() == false)
            {
                output += "\nIncorrect Date of Hire:\t\"" + (dateOfHire.HasValue ? ((DateTime)dateOfHire).ToString("yyyy-MM-dd") : "N/A") + "\"";
            }
            if (validateDOT() == false)
            {
                output += "\nIncorrect Date of Termination:\t\"" + (dateOfTermination.HasValue ? ((DateTime)dateOfTermination).ToString("yyyy-MM-dd") : "N/A") + "\"";
            }
            if (validateSalary() == false)
            {
                output += "\nIncorrect Salary:\t\"" + salary.ToString("0.00") + "\"";
            }
            return output;
        }

        /// <summary>
        /// method formats the console output for the class information
        /// </summary>
        /// <returns>returns the formatted string of data</returns>
        protected override String ConsoleDetails()
        {
            String output = "";
            output += "\r\n\tDate of Hire:\t\t\t" + "\"" + (dateOfHire.HasValue ? dateOfHire.Value.ToString("yyyy-MM-dd") : "N/A") + "\"";
            output += "\r\n\tDate of Termination:\t\t" + "\"" + (dateOfTermination.HasValue ? dateOfTermination.Value.ToString("yyyy-MM-dd") : "N/A") + "\"";
            output += "\r\n\tSalary:\t\t\t\t" + "\"" + salary.ToString("0.00") + "\"";
            return output;
        }

        /// <summary>
        /// Method is called upon to output (to the screen) all attribute values for the class.
        /// </summary>
        public override void Details(bool logging)
        {
            String consoleOutput = base.ConsoleDetails() + this.ConsoleDetails();
            Console.WriteLine(consoleOutput);
            if (logging)
            {
                log.writeLog(produceLogString("DETAILS", "", "", "") + "\nInput: \n" + consoleOutput);
            }
        }
    }
}
