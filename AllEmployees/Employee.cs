//
// FILE: Employee.cs
// PROJECT: Employee Management System Project
// PROGRAMMERS: Jay Moorhouse, Jordan Poirier, Thom Taylor, Rachel Park
// DATE: 11/13/2014
// DESCRIPTION: Contains the class definition for the Employee parent class.
//              Included are the variables, contructors, and methods.
//              
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllEmployees
{
    /// <summary>
    ///  The Employee Class, to be the parent of several sub-types of employees
    /// </summary>
    public class Employee
    {
        private string firstName; /// Employee First Name
        private string lastName; /// Employee Last Name
        private string socialInsuranceNumber; /// Employee Social Insurance Number
        private DateTime dateOfBirth; /// Employee Date of Birth

        /// <summary>
        /// The Employee() method is a Constructor for the Employee Class.
        /// This version of the constructor initializes all values to default (blank/0).
        /// </summary>
        public Employee()
        {
            firstName = "";
            lastName = "";
            socialInsuranceNumber = "";
            dateOfBirth = new DateTime();
        }

        /// <summary>
        /// Overloaded Employee constructor.
        /// This version of the constructor sets the first and last name variables
        /// to the input parameter values, and sets the other fields to default (blank/0).
        /// </summary>
        /// <param name="first">The string to initialize the firstName variable to</param>
        /// <param name="last">The string to initialize the lastName variable to</param>
        public Employee(string first, string last)
        {
            firstName = first;
            lastName = last;
        }

        /// <summary>
        /// Overloaded Employee constructor.
        /// This version of the constructor sets all variables to the input parameter values.
        /// </summary>
        /// <param name="first">The string to initialize the firstName variable to</param>
        /// <param name="last">The string to initialize the lastName variable to</param>
        /// <param name="SIN">The string to set initialize socialInsuranceNumber variable to</param>
        /// <param name="DOB">The string to initialize the dateOfBirth variable to</param>
        public Employee(string first, string last, string SIN, DateTime DOB)
        {
            firstName = first;
            lastName = last;
            socialInsuranceNumber = SIN;
            dateOfBirth = DOB;
        }

        /// <summary>
        /// The setter for firstName
        /// </summary>
        /// <param name="fName">The string to set the firstName variable to</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool SetFirstName(string fName)
        {
            firstName = fName;
            if (firstName == fName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The setter for lastName
        /// </summary>
        /// <param name="lName">The string to set the lastName variable to</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool SetLastName(string lName)
        {
            lastName = lName;
            if (lastName == lName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The setter for socialInsuranceNumber
        /// </summary>
        /// <param name="SIN">The string to set the socialInsuranceNumber to</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool SetSIN(string SIN)
        {
            socialInsuranceNumber = SIN;
            if (socialInsuranceNumber == SIN)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The setter for dateOfbirth
        /// </summary>
        /// <param name="dob">The string to set the dateOfBirth to</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool SetDOB(DateTime dob)
        {
            dateOfBirth = dob;
            if (dateOfBirth == dob)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method is called upon to output (to the screen) all attribute values for the class.
        /// </summary>
        public void Details()
        {

        }
    }
}
