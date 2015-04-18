//
// FILE: ParttimeEmployee.cs
// PROJECT: Employee Management System Project
// PROGRAMMERS: Jay Moorhouse, Jordan Poirier, Thom Taylor, Rachel Park
// DATE: 11/13/2014
// DESCRIPTION: Contains the class definition for the ParttimeEmployee child class.
//              Included are the variables, contructors, and methods.
//              Inherited from Employee.cs
//  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllEmployees
{
    /// <summary>
    /// The ParttimeEmployee Class, a child class of Employee
    /// </summary>
    public class ParttimeEmployee : Employee
    {
        private DateTime dateOfHire;
        private DateTime dateOfTermination;
        private double hourlyRate;

        /// <summary>
        /// The ParttimeEmployee() method is a Constructor for the ParttimeEmployee Class.
        /// This version of the constructor initializes all values to default (blank/0).
        /// </summary>
        public ParttimeEmployee()
        {
            SetFirstName("");
            SetLastName("");
            SetSIN("");
            SetDOB(new DateTime());
            dateOfHire = new DateTime();
            dateOfTermination = new DateTime();
            hourlyRate = 0.0;
        }

        /// <summary>
        /// The ParttimeEmployee() method is a Constructor for the ParttimeEmployee Class.
        /// This version of the constructor initializes all values to input parameter values.
        /// </summary>
        /// <param name="first">The string to initialize the firstName variable to</param>
        /// <param name="last">The string to initialize the lastName variable to</param>
        /// <param name="SIN">The string to initialize the socialInsuranceNumber variable to</param>
        /// <param name="DOB">The date to set the dateOfBirth variable to</param>
        /// <param name="hireDate">The date to set the dateOfHire variable to</param>
        /// <param name="terminationDate">The date to set the dateOfTermination variable to</param>
        /// <param name="rate">The value to set the hourlyRate variable to</param>
        public ParttimeEmployee(string first, string last, string SIN, DateTime DOB,
            DateTime hireDate, DateTime terminationDate, double rate)
        {
            SetFirstName(first);
            SetFirstName(last);
            SetSIN(SIN);
            SetDOB(DOB);
            dateOfHire = hireDate;
            dateOfTermination = terminationDate;
            hourlyRate = rate;
        }

        /// <summary>
        /// The setter for dateOfHire
        /// </summary>
        /// <param name="date">The date to set the dateOfHire variable to</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool SetDateOfHire(DateTime date)
        {
            dateOfHire = date;
            if (dateOfHire == date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The setter for dateOfTermination
        /// </summary>
        /// <param name="date">The date to set the dateOfTermination variable to</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool SetDateOfTermination(DateTime date)
        {
            dateOfTermination = date;
            if (dateOfTermination == date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The setter for hourlyRate
        /// </summary>
        /// <param name="rate">The value to set the hourlyRate variable to</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool SetHourlyRate(double rate)
        {
            hourlyRate = rate;
            if (hourlyRate == rate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
