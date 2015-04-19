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
using System.Globalization;
using Supporting;


namespace AllEmployees
{
    /// <summary>
    ///  The Employee Class, to be the parent of several sub-types of employees
    /// </summary>
    public class Employee
    {
        protected string firstName; /// Employee First Name
        protected string lastName; /// Employee Last Name
        protected string socialInsuranceNumber; /// Employee Social Insurance Number
        protected DateTime? dateOfBirth; /// Employee Date of Birth
        private String employeeType { set; get; } /// Employee Type

        public Logging log;

        /// <summary>
        /// Default Constructor for the Employee Class.
        /// This version of the constructor initializes all values to default (blank).
        /// </summary>
        public Employee()
        {
            employeeType = "";
            firstName = "";
            lastName = "";
            socialInsuranceNumber = "";
            dateOfBirth = null;

            log = new Logging();
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

            log = new Logging();
        }

        /// <summary>
        /// Employee Copy constructor.
        /// This version of the constructor copies the value of passed Employee object
        /// </summary>
        /// <param name="employee">Employee object to copy from</param>
        public Employee(Employee employee)
        {
            firstName = employee.firstName;
            lastName = employee.lastName;
            dateOfBirth = employee.dateOfBirth;
            socialInsuranceNumber = employee.socialInsuranceNumber;

            SetType(employee.employeeType);

            log = new Logging();
        }

        /// <summary>
        /// Overloaded Employee constructor.
        /// This version of the constructor sets all variables to the input parameter values.
        /// </summary>
        /// <param name="first">The string to initialize the firstName variable to</param>
        /// <param name="last">The string to initialize the lastName variable to</param>
        /// <param name="SIN">The string to set initialize socialInsuranceNumber variable to</param>
        /// <param name="DOB">The string to initialize the dateOfBirth variable to</param>
        public Employee(string first, string last, string SIN, DateTime? DOB)
            : this(first, last)
        {
            socialInsuranceNumber = SIN;
            dateOfBirth = DOB;

            log = new Logging();
        }

        /// <summary>
        /// getter for firstName
        /// </summary>
        /// <returns>firstName value</returns>
        public String GetFirstName()
        {
            return firstName;
        }

        /// <summary>
        /// getter for lastName
        /// </summary>
        /// <returns>lastName value</returns>
        public String GetLastName()
        {
            return lastName;
        }

        /// <summary>
        /// getter for dateOfBirth
        /// </summary>
        /// <returns>dateOfBirth value</returns>
        public DateTime? GetDOB()
        {
            return dateOfBirth;
        }

        /// <summary>
        /// getter for socialInsuranceNumber
        /// </summary>
        /// <returns>value of socialInsuranceNumber</returns>
        public String GetSIN()
        {
            return socialInsuranceNumber;
        }

        /// <summary>
        /// A method checking if the input string only contains valid characters A-Za-z, hyphen, and apostrophe
        /// </summary>
        /// <param name="s">The input string to be examined</param>
        /// <returns>bool - whether the input is valid or not</returns>
        public bool CheckName(String s)
        {
            bool retV = true;
            if (s != null)
            {
                /// check the name to make sure it only has letters, '\', and '-'
                for (int i = 0; i < s.Length; i++)
                {
                    if (!(Char.IsLetter(s[i]) || s[i] == '\'' || s[i] == '-'))
                    {
                        retV = false;
                        break;
                    }
                }
            }
            return retV;
        }

        /// <summary>
        /// A method checking if the input string only contains valid digits 0-9
        /// </summary>
        /// <param name="s">The input string to be examined</param>
        /// <returns>bool - whether the input is valid or not</returns>
        public bool CheckDigit(String s)
        {
            bool retV = true;
            if (s != null)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (!Char.IsDigit(s[i]))
                    {
                        retV = false;
                        break;
                    }
                }
            }
            return retV;
        }

        /// <summary>
        /// method to check the SIN for validity (calling validateSIN)
        /// </summary>
        /// <param name="s">string s for the input SIN for checking</param>
        /// <returns>a bool indicating success or failure</returns>
        public bool CheckSIN(String s)
        {
            bool retV = false;
            retV = validateSIN(s);
            return retV;
        }


        /// <summary>
        /// A method checking if the input string valid Canadian SIN nubmer format
        /// <see href="http://www.fakenamegenerator.com/social-insurance-number.php">Reference 1</see>
        /// <see href="http://www.ryerson.ca/JavaScript/lectures/forms/textValidation/sinProject.html">Reference 2</see>
        /// <see href="http://stackoverflow.com/questions/17953014/c-sharp-algorithm-to-create-fake-canadian-social-secutiry-number-sin-for-unit">Reference 3</see>
        /// </summary>
        /// <param name="s">The input string to be examined</param>
        /// <returns>bool - whether the input is valid or not</returns>
        private bool validateSIN(String inSin)
        {
            bool returnVal = false;
            bool even = false;
            int[] sin;
            int[] doubled;
            doubled = new int[4];
            sin = new int[8];
            string sum1 = "";
            int sum2 = 0;
            int total = 0;
            int subtractNum = 0;
            int checksum;
            int i = 0;
            int j = 0;
            if (inSin == "")
            {
                returnVal = false;
            }
            else
            {
                for (i = 0; i < (inSin.Length - 1); i++)
                {
                    /// make sure the first 8 are digits
                    if (char.IsDigit(inSin[i]) && j < 8)
                    {
                        sin[j++] = (int)Char.GetNumericValue(inSin[i]);
                    }
                }
                if (sin.Length == 8)
                {
                    checksum = (int)Char.GetNumericValue(inSin[inSin.Length - 1]);
                    foreach (int x in sin)
                    {
                        if (even == false)
                        {
                            sum2 += x;
                            even = true;
                        }
                        else
                        {
                            sum1 += x.ToString();
                            even = false;
                        }
                    }
                    for (i = 0; i < sum1.Length; i++)
                    {
                        doubled[i] = (2 * (int)Char.GetNumericValue(sum1[i]));
                    }
                    sum1 = "";
                    foreach (int y in doubled)
                    {
                        sum1 += y.ToString();
                    }
                    for (i = 0; i < sum1.Length; i++)
                    {
                        total += (int)Char.GetNumericValue(sum1[i]);
                    }
                    total += sum2;
                    if (total % 10 == 0)
                    {
                        subtractNum = total;
                    }
                    else
                    {
                        while ((total + subtractNum) % 10 != 0)
                        {
                            subtractNum++;
                        }
                        subtractNum += total;
                    }
                    if ((subtractNum - total) == checksum)
                    {
                        returnVal = true;
                    }
                }
            }
            return returnVal;
        }

        /// <summary>
        /// A method checking if the input date is within valid date range
        /// </summary>
        /// <param name="startDate">The earliest date that the input date can have to be valid</param>
        /// <param name="endDate">The latest date that the input date can have to be valid</param>
        /// <param name="inputDate">The input date to be examined</param>
        /// <returns>bool - whether the input is valid or not</returns>
        public bool CheckDateRange(DateTime startDate, DateTime endDate, DateTime inputDate)
        {
            bool retV = false;
            if (startDate <= endDate)
            {
                retV = ((inputDate >= startDate) && (inputDate <= endDate));
            }
            return retV;
        }


        /// <summary>
        /// Takes in the string that contains date info (NOT "N/A") and parse into DateTime? variable
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns>Return DateTime variable if contain valid DateTime information; otherwise null</returns>
        public DateTime? ReturnDateIfValid(String dateStr)
        {
            DateTime date = DateTime.MinValue;
            DateTime? dateNull = null;
            var formats = new[] {   "dd-MM-yyyy", "d-MM-yyyy", "dd-M-yyyy", "d-M-yyyy",
                                    "yyyy-MM-dd", "yyyy-MM-d", "yyyy-M-dd", "yyyy-M-d", 
                                    "dd/MM/yyyy", "d/MM/yyyy", "dd/M/yyyy", "d/M/yyyy",
                                    "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d"};
            dateStr = dateStr.Trim();
            dateStr = dateStr.Replace(" ", "");
            if (DateTime.TryParseExact(dateStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return date;
            }
            else
            {
                return dateNull;
            }

        }

        /// <summary>
        /// setter for dateOfBirth
        /// </summary>
        /// <param name="dateStr">the date in string form to set</param>
        /// <returns>a bool indicating success or failure</returns>
        public bool SetDOB(String dateStr)
        {
            bool retV = false;
            DateTime? date = null;
            if (dateStr == "N/A")
            {
                retV = SetDOB(date);
                retV = true;
            }
            else if ((date = ReturnDateIfValid(dateStr)) != null)
            {
                retV = SetDOB(date);
                retV = true;
            }
            return retV;
        }

        /// <summary>
        /// A method checking if the input date is within valid date range
        /// </summary>
        /// <param name="action">Action that was done to call logging: SET, VALIDATE, DETAILS</param>
        /// <param name="iniValue">Previous/initial value of attribute</param>
        /// <param name="endValue">Input value</param>
        /// <param name="result">FAIL or SUCCESS</param>
        /// <returns>bool - whether the input is valid or not</returns>
        public String produceLogString(String action, String iniValue, String endValue, String result)
        {
            String logStr = "";
            if (action.ToUpper() == "SET")
            {
                logStr = "Action: " + action.ToUpper() + "_" + result.ToUpper() + "\nPrevious: " + iniValue + "\tInput: " + endValue;
            }
            else if (action.ToUpper() == "VALIDATE")
            {
                logStr = "Action: " + action.ToUpper() + "_" + result.ToUpper() + "\nInput: " + endValue;
            }
            else if (action.ToUpper() == "DETAILS")
            {
                logStr = "Action: " + action.ToUpper() + "_" + result.ToUpper();
            }
            return logStr;
        }


        /// <summary>
        /// The setter for firstName
        /// </summary>
        /// <param name="fName">The string to set the firstName variable to</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool SetFirstName(string fName)
        {
            bool retV = false;

            if (this.employeeType.ToUpper() == "CT")
            {
                if (fName != "")
                {
                    log.writeLog(
                        produceLogString("SET", firstName, fName, "FAIL")
                                        + "\nDetail: Contract Employee's first name must be blank");
                }
                else
                {
                    try
                    {
                        log.writeLog(
                            produceLogString("SET", firstName, fName, "SUCCESS")
                                             + "\nDetail: Called to set type to Contract Employee thus blank first name");
                    }
                    catch(Exception)
                    {
                        
                    }
                    firstName = fName;
                    retV = true;
                }
            }
            else //Other employee types than CT
            {
                if (CheckName(fName) == true)
                {
                    log.writeLog(produceLogString("SET", firstName, fName, "SUCCESS"));
                    firstName = fName;
                    retV = true;
                }
                else
                {
                    log.writeLog(produceLogString("SET", firstName, fName, "FAIL") + "\nDetail: Name Contains invalid characters");
                }
            }
            return retV;
        }


        /// <summary>
        /// The setter for lastName
        /// </summary>
        /// <param name="lName">The string to set the lastName variable to</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool SetLastName(string lName)
        {
            bool retV = false;
            if (CheckName(lName) == true)
            {
                log.writeLog(produceLogString("SET", lastName, lName, "SUCCESS"));
                lastName = lName;
                retV = true;
            }
            else
            {
                log.writeLog(produceLogString("SET", lastName, lName, "FAIL") + "\nDetail: Name Contains invalid characters");
            }
            return retV;
        }

        /// <summary>
        /// The setter for socialInsuranceNumber
        /// </summary>
        /// <param name="SIN">The string to set the socialInsuranceNumber to</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool SetSIN(string SIN)
        {
            bool retV = false;
            SIN = SIN.Trim();
            SIN = SIN.Replace(" ", "");

            if (CheckSIN(SIN) == true)
            {
                log.writeLog(produceLogString("SET", socialInsuranceNumber, SIN, "SUCCESS"));
                socialInsuranceNumber = SIN;
                retV = true;
            }
            else
            {
                log.writeLog(produceLogString("SET", socialInsuranceNumber, SIN, "FAIL")
                                            + "\nDetails: SIN Should be blank or 9 digits\n");
            }
            return retV;
        }

        /// <summary>
        /// The setter for dateOfbirth
        /// </summary>
        /// <param name="dob">The string to set the dateOfBirth to</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool SetDOB(DateTime? dob)
        {
            bool retV = false;
            if (dob == null)
            {
                log.writeLog(
                    produceLogString("SET",
                                    (dateOfBirth.HasValue ? dateOfBirth.Value.ToString("yyyy-MM-dd") : "N/A"),
                                    "N/A",
                                    "SUCCESS"));
                dateOfBirth = null;
                retV = true;
            }
            else
            {
                DateTime newDOB = (DateTime)dob;

                log.writeLog(
                    produceLogString("SET",
                                    (dateOfBirth.HasValue ? dateOfBirth.Value.ToString("yyyy-MM-dd") : "N/A"),
                                    newDOB.ToString("yyyy-MM-dd"),
                                    "SUCCESS"));
                dateOfBirth = dob;
                retV = true;
            }
            return retV;
        }

        /// <summary>
        /// The setter for employeeType
        /// </summary>
        /// <param name="type">The string to set the employee type to</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        protected bool SetType(String type)
        {
            employeeType = type;
            if (type == "CT")
            {
                SetFirstName("");
            }
            return true;
        }


        /// <summary>
        /// A virtual method to validate the employee object attributes.
        /// Ultimate validation that is called before the object is added to the container.
        /// </summary>
        /// <returns>A boolean indicating whether the obejct is valid employee object or not</returns>

        public virtual string Validate()
        {
            String output = "";
            if (validateFirstName() == false)
            {
                output += "\nInvalid First Name:\t\"" + firstName + "\"";
            }
            if (validateLastName() == false)
            {
                output += "\nInvalid Last Name:\t\"" + lastName + "\"";
            }
            if (validateSIN() == false)
            {
                output += "\nInvalid SIN:\t\"" + socialInsuranceNumber + "\"";
            }
            if (validateDOB() == false)
            {
                output += "\nInvalid Date of Birth:\t\"" + (dateOfBirth.HasValue ? ((DateTime)dateOfBirth).ToString("yyyy-MM-dd") : "N/A") + "\"";
            }
            return output;
        }

        /// <summary>
        /// First name validating method that is called by Validate() function.
        /// </summary>
        /// <returns>A boolean indicating whether the obejct has valid first name attribute or not.</returns>
        private bool validateFirstName()
        {
            bool retV = false;
            if (firstName != null)
            {
                if (firstName == "")
                {
                    if (employeeType.ToUpper() == "CT")
                    {
                        log.writeLog(produceLogString("VALIDATE", "", firstName, "SUCCESS"));
                        retV = true;
                    }
                    else
                    {
                        log.writeLog(produceLogString("VALIDATE", "", firstName, "FAIL") + "\nDetail: Name cannot be blank");
                    }
                }
                else //First Name not empty string
                {
                    if (CheckName(firstName) == true)
                    {
                        log.writeLog(produceLogString("VALIDATE", "", firstName, "SUCCESS"));
                        retV = true;
                    }
                    else
                    {
                        log.writeLog(produceLogString("VALIDATE", "", firstName, "FAIL") + "\nDetail: Name contains invalid characters");
                    }
                }
            }
            else //First Name NULL
            {
                log.writeLog(produceLogString("VALIDATE", "", firstName, "FAIL") + "\nDetail: Name cannot be NULL");
            }
            return retV;
        }

        /// <summary>
        /// Last name validating method that is called by Validate() function.
        /// </summary>
        /// <returns>A boolean indicating whether the obejct has valid last name attribute or not.</returns>
        public bool validateLastName()
        {
            bool retV = false;
            if (lastName != null)
            {
                if (lastName == "")
                {
                    log.writeLog(produceLogString("VALIDATE", "", lastName, "FAIL") + "\nDetail: Name cannot be blank");
                }
                else
                {
                    if (CheckName(lastName) == true)
                    {
                        log.writeLog(produceLogString("VALIDATE", "", lastName, "SUCCESS"));
                        retV = true;
                    }
                    else
                    {
                        log.writeLog(produceLogString("VALIDATE", "", lastName, "FAIL") + "\nDetail: Name contains invalid characters");
                    }
                }
            }
            else
            {
                log.writeLog(produceLogString("VALIDATE", "", lastName, "FAIL") + "\nDetail: Name cannot be NULL");
            }
            return retV;
        }

        /// <summary>
        /// DOB validating method that is called by Validate() function.
        /// </summary>
        /// <returns>A boolean indicating whether the obejct has valid dateofBirth attribute or not.</returns>
        public bool validateDOB()
        {
            bool retV = false;

            if (dateOfBirth != null)
            {
                if (CheckDateRange(DateTime.MinValue, DateTime.Now, (DateTime)dateOfBirth) == true)
                {
                    log.writeLog(produceLogString("VALIDATE", "", ((DateTime)dateOfBirth).ToString("yyyy-MM-dd"), "SUCCESS"));
                    retV = true;
                }
                else
                {
                    log.writeLog(produceLogString("VALIDATE", "", ((DateTime)dateOfBirth).ToString("yyyy-MM-dd"), "FAIL")
                                    + "\nDetails: Date of Birth must come before Now\n");
                }
            }
            else
            {
                log.writeLog(produceLogString("VALIDATE", "", "N/A", "FAIL") + "\nDetail: Date of Birth cannot be NULL\n");
            }
            return retV;
        }

        /// <summary>
        /// SIN validating method that is called by Validate() function.
        /// </summary>
        /// <returns>A boolean indicating whether the obejct has valid SIN attribute or not.</returns>
        public bool validateSIN()
        {
            bool retV = false;
            if (validateSIN(socialInsuranceNumber) == true)
            {
                log.writeLog(produceLogString("VALIDATE", "", socialInsuranceNumber, "SUCCESS"));
                retV = true;
            }
            else
            {
                log.writeLog(produceLogString("VALIDATE", "", socialInsuranceNumber, "FAIL") + "\nDetails: Invalid SIN number\n");
            }
            return retV;
        }

        /// <summary>
        /// A virtual function that will be called to produce a string containing details of employee object to console.
        /// </summary>
        /// <returns>A String containinng the detailed information of the employee object that will be printed to the console</returns>
        protected virtual String ConsoleDetails()
        {
            String output = "";
            if (employeeType != "CT")
            {
                switch (employeeType)
                {
                    case "FT":
                        output += "<Full Time Employee>\n";
                        break;
                    case "PT":
                        output += "<Part Time Employee>\n";
                        break;
                    case "SN":
                        output += "<Seasonal Employee>\n";
                        break;
                }
                        output += "\tLast Name:\t\t\t" + "\"" + lastName + "\"";
                        output += "\n\tFirst Name:\t\t\t" + "\"" + firstName + "\"";
                        output += "\n\tSIN:\t\t\t\t" + "\"" + (socialInsuranceNumber != "" ? socialInsuranceNumber.Insert(3, " ").Insert(7, " ") : "") + "\"";
                        output += "\n\tDate of Birth:\t\t\t" + "\"" + (dateOfBirth.HasValue ? dateOfBirth.Value.ToString("yyyy-MM-dd") : "N/A") + "\"";              
            }
            else
            {
                output += "<Contract Employee>";
                output += "\n\tBusiness Name:\t\t\t\"" + lastName + "\"";
                output += "\n\tBusiness Number:\t\t" + "\"" + (socialInsuranceNumber != "" ? socialInsuranceNumber.Insert(4, " ") : "") + "\"";
                output += "\n\tDate of Incorporation:\t\t" + "\"" + (dateOfBirth.HasValue ? dateOfBirth.Value.ToString("yyyy-MM-dd") : "N/A") + "\"";
            }
            return output;
        }

        public String ConsoleDetails(String type)
        {
            String output = "";
            if (type != "CT")
            {
                switch (type)
                {
                    case "FT":
                        output += "<Full Time Employee>\n";
                        break;
                    case "PT":
                        output += "<Part Time Employee>\n";
                        break;
                    case "SN":
                        output += "<Seasonal Employee>\n";
                        break;
                }
                output += "\tLast Name:\t\t\t" + "\"" + lastName + "\"";
                output += "\n\tFirst Name:\t\t\t" + "\"" + firstName + "\"";
                output += "\n\tSIN:\t\t\t\t" + "\"" + (socialInsuranceNumber != "" ? socialInsuranceNumber.Insert(3, " ").Insert(7, " ") : "") + "\"";
                output += "\n\tDate of Birth:\t\t\t" + "\"" + (dateOfBirth.HasValue ? dateOfBirth.Value.ToString("yyyy-MM-dd") : "N/A") + "\"";
            }
            else
            {
                output += "<Contract Employee>";
                output += "\n\tBusiness Name:\t\t\t\"" + lastName + "\"";
                output += "\n\tBusiness Number:\t\t" + "\"" + (socialInsuranceNumber != "" ? socialInsuranceNumber.Insert(4, " ") : "") + "\"";
                output += "\n\tDate of Incorporation:\t\t" + "\"" + (dateOfBirth.HasValue ? dateOfBirth.Value.ToString("yyyy-MM-dd") : "N/A") + "\"";
            }
            return output;
        }

        /// <summary>
        /// A virtual function that will be called to print details of employee object to database file.
        /// </summary>
        /// <returns>A String containinng the detailed information of the employee object that will be printed to the database file</returns>
        public virtual List<String> DatabaseDetails()
        {
            List<String> output = new List<String>();

            if (employeeType == "FT")
            {
                output.AddRange(
                    new String[] {"FT", lastName, firstName, socialInsuranceNumber, 
                                (dateOfBirth.HasValue ? dateOfBirth.Value.ToString("yyyy-MM-dd") : "N/A") });
            }
            else if (employeeType == "PT")
            {
                output.AddRange(
                    new String[] {"PT", lastName, firstName, socialInsuranceNumber, 
                                (dateOfBirth.HasValue ? dateOfBirth.Value.ToString("yyyy-MM-dd") : "N/A") });
            }
            else if (employeeType == "CT")
            {
                output.AddRange(
                    new String[] {"CT", lastName, firstName, socialInsuranceNumber, 
                                (dateOfBirth.HasValue ? dateOfBirth.Value.ToString("yyyy-MM-dd") : "N/A") });
            }
            else if (employeeType == "SN")
            {
                output.AddRange(
                    new String[] {"SN", lastName, firstName, socialInsuranceNumber, 
                                (dateOfBirth.HasValue ? dateOfBirth.Value.ToString("yyyy-MM-dd") : "N/A") });
            }

            return output;
        }


        /// <summary>
        /// A virtual Details function that will be called to print details of base employee object to the console.
        /// </summary>
        public virtual void Details(bool logging)
        {
            String consoleOutput = ConsoleDetails();
            Console.WriteLine(consoleOutput);
        }
    }
}
