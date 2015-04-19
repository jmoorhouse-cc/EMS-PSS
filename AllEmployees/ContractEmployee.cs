//
// FILE: ContractEmployee.cs
// PROJECT: Employee Management System Project
// PROGRAMMERS: Jay Moorhouse, Jordan Poirier, Thom Taylor, Rachel Park
// DATE: 11/13/2014
// DESCRIPTION: Contains the class definition for the ContractEmployee child class.
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
    /// The ContractEmployee Class, a child class of Employee
    /// </summary>
    public sealed class ContractEmployee : Employee
    {
        private DateTime? contractStartDate;
        private DateTime? contractEndDate;
        private Decimal fixedContractAmt;

        /// <summary>
        /// The ContractEmployee() method is a Constructor for the ContractEmployee Class.
        /// This version of the constructor initializes all values to default (blank/0).
        /// </summary>
        public ContractEmployee()
            : base()
        {
            contractStartDate = null;
            contractEndDate = null;
            fixedContractAmt = 0.00M;
            SetType("CT");
        }

        public ContractEmployee(ContractEmployee prev)
            : base(prev)
        {
            contractStartDate = prev.GetContractStartDate();
            contractEndDate = prev.GetContractEndDate();
            fixedContractAmt = prev.GetFixedContractAmt();

            SetType("CT");
        }

        /// <summary>
        /// The ContractEmployee() method is a Constructor for the ContractEmployee Class.
        /// This version of the constructor initializes all values to input parameter values.
        /// </summary>
        /// <param name="first">The string to initialize the firstName variable to</param>
        /// <param name="last">The string to initialize the lastName variable to</param>
        /// <param name="SIN">The string to initialize the socialInsuranceNumber variable to</param>
        /// <param name="DOB">The date to set the dateOfBirth variable to</param>
        /// <param name="startDate">The date to set the contractStartDate variable to</param>
        /// <param name="terminationDate">The date to set the dateOfTermination variable to</param>
        /// <param name="rate">The value to set the hourlyRate variable to</param>
        public ContractEmployee(string first, string last, string SIN, DateTime DOB,
            DateTime startDate, DateTime endDate, Decimal rate)
            : base(first, last, SIN, DOB)
        {
            contractStartDate = startDate;
            contractEndDate = endDate;
            fixedContractAmt = rate;
        }

        /// <summary>
        /// getter for contractStartDate
        /// </summary>
        /// <returns> contractStartDate variable value</returns>
        public DateTime? GetContractStartDate()
        {
            return contractStartDate;
        }

        /// <summary>
        /// getter for contractEndDate
        /// </summary>
        /// <returns>contractEndDate value</returns>
        public DateTime? GetContractEndDate()
        {
            return contractEndDate;
        }

        /// <summary>
        /// getter for fixedContractAmount
        /// </summary>
        /// <returns>fixedContractAmount variable value</returns>
        public Decimal GetFixedContractAmt()
        {
            return fixedContractAmt;
        }

        /// <summary>
        /// setter for contractStartDate (datetime)
        /// </summary>
        /// <param name="cStartDate"></param>
        /// <returns>contractStartDate variable value</returns>
        public bool SetContractStartDate(DateTime? cStartDate)
        {
            bool retV = false;
            log.writeLog(
                produceLogString("SET",
                                (contractStartDate.HasValue ? contractStartDate.Value.ToString("yyyy-MM-dd") : "N/A"),
                                (cStartDate.HasValue ? cStartDate.Value.ToString("yyyy-MM-dd") : "N/A"),
                                "SUCCESS"));
            contractStartDate = cStartDate;
            retV = true;
            return retV;
        }

        /// <summary>
        /// setter for contractStartDate (string)
        /// </summary>
        /// <param name="cStartDateStr"></param>
        /// <returns></returns>
        public bool SetContractStartDate(String cStartDateStr)
        {
            bool retV = false;
            DateTime? date = null;
            if (cStartDateStr == "N/A")
            {
                retV = SetContractStartDate(date);
                retV = true;
            }
            else if ((date = ReturnDateIfValid(cStartDateStr)) != null)
            {
                retV = SetContractStartDate(date);
                retV = true;
            }
            return retV;
        }

        /// <summary>
        /// setter for contractEndDate variable
        /// </summary>
        /// <param name="cEndDate">takes in a datetime to set contractEndDate to</param>
        /// <returns></returns>
        public bool SetContractEndDate(DateTime? cEndDate)
        {
            bool retV = false;
            log.writeLog(
                produceLogString("SET",
                                (contractEndDate.HasValue ? contractEndDate.Value.ToString("yyyy-MM-dd") : "N/A"),
                                (cEndDate.HasValue ? cEndDate.Value.ToString("yyyy-MM-dd") : "N/A"),
                                "SUCCESS"));
            contractEndDate = cEndDate;
            retV = true;
            return retV;
        }

        /// <summary>
        /// loads the details into the database format
        /// </summary>
        /// <returns>list that was previously pipe delimited</returns>
        public override List<String> DatabaseDetails()
        {
            List<String> output = new List<String>();
            output.AddRange(
                new String[] {"CT", lastName, firstName, socialInsuranceNumber, 
                                (dateOfBirth.HasValue ? dateOfBirth.Value.ToString("yyyy-MM-dd") : "N/A"), (contractStartDate.HasValue ? contractStartDate.Value.ToString("yyyy-MM-dd") : "N/A")
                                , (contractEndDate.HasValue ? contractEndDate.Value.ToString("yyyy-MM-dd") : "N/A"), fixedContractAmt.ToString()});
            return output;
        }

        /// <summary>
        /// setter for contractEndDate
        /// </summary>
        /// <param name="cEndDateStr"></param>
        /// <returns>bool indicating success or failure</returns>
        public bool SetContractEndDate(String cEndDateStr)
        {
            bool retV = false;
            DateTime? date = null;
            if (cEndDateStr == "N/A")
            {
                retV = SetContractEndDate(date);
                retV = true;
            }
            else if ((date = ReturnDateIfValid(cEndDateStr)) != null)
            {
                retV = SetContractEndDate(date);
                retV = true;
            }
            return retV;
        }

        /// <summary>
        /// check for fixedContractAmount (decimal)
        /// </summary>
        /// <param name="fAmt"></param>
        /// <returns>bool indicating success or failure</returns>
        public bool CheckFixedContractAmt(Decimal fAmt)
        {
            return fAmt > 0;
        }

        /// <summary>
        /// check for fixedContractAmount (string)
        /// </summary>
        /// <param name="fAmtStr"></param>
        /// <returns>bool indicating success or failure</returns>
        public bool CheckFixedContractAmt(String fAmtStr)
        {
            Decimal newFixedAmount;
            return Decimal.TryParse(fAmtStr, out newFixedAmount) && CheckFixedContractAmt(newFixedAmount);
        }

        /// <summary>
        /// setter for fixedContractAmount
        /// </summary>
        /// <param name="fAmt"></param>
        /// <returns>bool indicating success or failure</returns>
        public bool SetFixedContractAmt(Decimal fAmt)
        {
            bool retV = false;
            if (CheckFixedContractAmt(fAmt) == true)
            {
                log.writeLog(produceLogString("SET", fixedContractAmt.ToString("0.00"), fAmt.ToString("0.00"), "SUCCESS"));
                fixedContractAmt = fAmt;
                retV = true;
            }
            else
            {
                log.writeLog(produceLogString("SET", fixedContractAmt.ToString("0.00"), fAmt.ToString("0.00"), "FAIL")
                    + "\nDetail: Fixed Contract Amoung cannot be lower than zero");
            }
            return retV;
        }

        /// <summary>
        /// setter for fixedContractAmount
        /// </summary>
        /// <param name="fAmtStr"></param>
        /// <returns>bool indicating success or failure</returns>
        public bool SetFixedContractAmt(String fAmtStr)
        {
            bool retV = false;
            Decimal newFixedContractAmt;
            if (Decimal.TryParse(fAmtStr, out newFixedContractAmt))
            {
                retV = SetFixedContractAmt(newFixedContractAmt);
            }
            return retV;
        }

        /// <summary>
        /// validate constractStartDate method
        /// </summary>
        /// <returns>bool indicating validity</returns>
        private bool validateCSD()
        {
            bool retV = false;
            if (contractStartDate == null)
            {
                log.writeLog(produceLogString("VALIDATE", "", "N/A", "FAIL")
                                            + "\nDetails: Contract Start Date cannot be NULL");
            }
            else
            {
                DateTime newCSD = (DateTime)contractStartDate;
                DateTime CSD_MinDate = (DateTime)dateOfBirth;
                DateTime CSD_MaxDate = DateTime.MaxValue;
                if (contractEndDate != null)
                {
                    CSD_MaxDate = (DateTime)contractEndDate;
                }

                if (CheckDateRange(CSD_MinDate, CSD_MaxDate, newCSD) == true)
                {
                    log.writeLog(
                        produceLogString("VALIDATE", "", newCSD.ToString("yyyy-MM-dd"), "SUCCESS")
                                        + "\nDetails: Contract Start Date comes after " + CSD_MinDate.ToString("yyyy-MM-dd") + "& before " + CSD_MaxDate.ToString("yyyy-MM-dd") + "\n");
                    retV = true;
                }
                else
                {
                    log.writeLog(
                        produceLogString("VALIDATE", "", DateTime.Now.ToString("yyyy-MM-dd"), "FAIL")
                                        + "\nDetails: Contract Start Date must come after " + CSD_MinDate.ToString("yyyy-MM-dd") + ", before " + CSD_MaxDate.ToString("yyyy-MM-dd") + "\n");
                }
            }
            return retV;
        }

        /// <summary>
        /// validation for contractEndDate
        /// </summary>
        /// <returns>bool indicating validity</returns>
        private bool validateCED()
        {
            bool retV = false;
            if (contractEndDate == null)
            {
                log.writeLog(produceLogString("VALIDATE", "", "N/A", "FAIL")
                                            + "\nDetails: Contract End Date cannot be NULL\n");
            }
            else
            {
                DateTime newCED = (DateTime)contractEndDate;
                DateTime CED_MinDate = (DateTime)contractStartDate;
                DateTime CED_MaxDate = DateTime.MaxValue;

                if (CheckDateRange(CED_MinDate, CED_MaxDate, newCED) == true)
                {
                    log.writeLog(
                        produceLogString("VALIDATE", "", newCED.ToString("yyyy-MM-dd"), "SUCCESS")
                                        + "\nDetails: Contract End Date comes after " + CED_MinDate.ToString("yyyy-MM-dd") + "& before " + CED_MaxDate.ToString("yyyy-MM-dd") + "\n");
                    retV = true;
                }
                else
                {
                    log.writeLog(
                        produceLogString("VALIDATE", "", DateTime.Now.ToString("yyyy-MM-dd"), "FAIL")
                                        + "\nDetails: Contract End Date must come after " + CED_MinDate.ToString("yyyy-MM-dd") + ", before " + CED_MaxDate.ToString("yyyy-MM-dd") + "\n");
                }
            }
            return retV;
        }

        /// <summary>
        /// validation for fixedContractAmount
        /// </summary>
        /// <returns>bool indicating validity</returns>
        private bool validateFixedContractAmt()
        {
            bool retV = false;

            if (CheckFixedContractAmt(fixedContractAmt) && fixedContractAmt != 0)
            {
                log.writeLog(produceLogString("VALIDATE", "", fixedContractAmt.ToString("0.00"), "SUCCESS"));
                retV = true;
            }
            else
            {
                log.writeLog(produceLogString("VALIDATE", "", fixedContractAmt.ToString("0.00"), "FAIL")
                                            + "\nDetails: Fixed Contract Amount must be bigger than 0\n");

            }
            return retV;
        }

        /// <summary>
        /// validate businessNumber
        /// </summary>
        /// <returns>bool indicating validity</returns>
        public bool validateBusinessNumber()
        {
            bool retV = false;
            string num = this.GetSIN(); // get sin 
            string year;
            DateTime date = (DateTime)this.GetDOB();
            year = String.Format("{0: yy}", date); // store year (yy)

            if ((num[0] == year[2] && num[1] == year[3]) && (CheckSIN(num) == true))
            {
                log.writeLog(produceLogString("VALIDATE", "", GetSIN(), "SUCCESS"));
                retV = true;
            }
            else
            {
                log.writeLog(produceLogString("VALIDATE", "", GetSIN(), "FAIL"));
            }
            return retV;
        }

        public bool validateBusinessNumber(String BN, DateTime? incorp)
        {
            bool retV = false;
            string num = BN; // get sin 
            string year;
            if (incorp != null)
            {
                DateTime? date = incorp;
                year = String.Format("{0: yy}", date); // store year (yy)
                year = year.Trim();
                if ((num[0] == year[0] && num[1] == year[1]) && (CheckSIN(num) == true))
                {
                    log.writeLog(produceLogString("VALIDATE", "", GetSIN(), "SUCCESS"));
                    retV = true;
                }
                else
                {
                    log.writeLog(produceLogString("VALIDATE", "", GetSIN(), "FAIL"));
                }
            }
            else
            {
                retV = CheckSIN(num);
            }
            return retV;
        }

        /// <summary>
        /// Validate method validates all fields to determine if entire employee is valid
        /// </summary>
        /// <returns>the validity string prudent to the current scenario</returns>
        public override String Validate()
        {
            String output = "";
            if (firstName != "")
            {
                output += "\nInvalid First Name:\t\"" + firstName + "\"";
            }
            if (base.validateLastName() == false)
            {
                output += "\nInvalid Last Name:\t\"" + lastName + "\"";
            }
            if (base.validateSIN() == false || validateBusinessNumber() == false)
            {
                output += "\nInvalid SIN:\t\"" + socialInsuranceNumber + "\"";
            }
            if (base.validateDOB() == false)
            {
                output += "\nInvalid Date of Birth:\t\"" + (dateOfBirth.HasValue ? ((DateTime)dateOfBirth).ToString("yyyy-MM-dd") : "N/A") + "\"";
            }
            if (validateCSD() == false)
            {
                output += "\nIncorrect Contract Start Date:\t\"" + (contractStartDate.HasValue ? ((DateTime)contractStartDate).ToString("yyyy-MM-dd") : "N/A") + "\"";
            }
            if (validateCED() == false)
            {
                output += "\nIncorrect Contract End Date:\t\"" + (contractEndDate.HasValue ? ((DateTime)contractEndDate).ToString("yyyy-MM-dd") : "N/A") + "\"";
            }
            if (validateFixedContractAmt() == false)
            {
                output += "\nIncorrect Fixed Contract Amount:\t\"" + fixedContractAmt.ToString("0.00") + "\"";
            }
            return output;
        }
        
        /// <summary>
        /// format the output for the console details
        /// </summary>
        /// <returns>the string output</returns>
        protected override String ConsoleDetails()
        {
            String output = "";
            output += "\r\n\tContract Start Date:\t\t\"" + (contractStartDate.HasValue ? ((DateTime)contractStartDate).ToString("yyyy-MM-dd") : "N/A") + "\"";
            output += "\r\n\tContract End Date:\t\t\"" + (contractEndDate.HasValue ? ((DateTime)contractEndDate).ToString("yyyy-MM-dd") : "N/A") + "\"";
            output += "\r\n\tFixed Contract Amount:\t\t\"" + fixedContractAmt.ToString("0.00") + "\"";
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
