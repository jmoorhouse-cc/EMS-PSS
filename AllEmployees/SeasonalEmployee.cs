//
// FILE: SeasonalEmployee.cs
// PROJECT: Employee Management System Project
// PROGRAMMERS: Jay Moorhouse, Jordan Poirier, Thom Taylor, Rachel Park
// DATE: 11/13/2014
// DESCRIPTION: Contains the class definition for the SeasonalEmployee child class.
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
    /// The SeasonalEmployee Class, a child class of Employee
    /// </summary>
    public sealed class SeasonalEmployee : Employee
    {
        private string season;
        private Decimal piecePay;

        /// <summary>
        /// The SeasonalEmployee() method is a Constructor for the SeasonalEmployee Class.
        /// This version of the constructor initializes all values to default (blank/0).
        /// </summary>
        public SeasonalEmployee()
            : base()
        {
            season = "";
            piecePay = 0.00M;

            SetType("SN");
        }

        public SeasonalEmployee(SeasonalEmployee prev)
            : base(prev)
        {
            season = prev.GetSeason();
            piecePay = prev.GetPiecePay();

            SetType("SN");
        }
        /// <summary>
        /// The SeasonalEmployee() method is a Constructor for the SeasonalEmployee Class.
        /// This version of the constructor initializes all values to input parameter values.
        /// </summary>
        /// <param name="first">The string to initialize the firstName variable to</param>
        /// <param name="last">The string to initialize the lastName variable to</param>
        /// <param name="SIN">The string to initialize the socialInsuranceNumber variable to</param>
        /// <param name="DOB">The date to initialize the dateOfBirth variable to</param>
        /// <param name="whatSeason">The string to initialize the season variable to</param>
        /// <param name="whatPiecePay">The string to initialize the piecePay variable to</param>
        public SeasonalEmployee(String first, String last, String SIN, DateTime? DOB, String whatSeason, Decimal whatPiecePay)
            : base(first, last, SIN, DOB)
        {
            season = whatSeason;
            piecePay = whatPiecePay;
            SetType("SN");
        }

        /// <summary>
        /// Getter for season
        /// </summary>
        /// <returns>the value of season</returns>
        public String GetSeason()
        {
            return season;
        }

        /// <summary>
        /// Getter for Piece Pay
        /// </summary>
        /// <returns>the value of piece pay</returns>
        public Decimal GetPiecePay()
        {
            return piecePay;
        }

        /// <summary>
        /// formats output for database contents
        /// </summary>
        /// <returns>returns the list</returns>
        public override List<String> DatabaseDetails()
        {
            List<String> output = new List<String>();
            output.AddRange(
                new String[] {"SN", lastName, firstName, socialInsuranceNumber, 
                                (dateOfBirth.HasValue ? dateOfBirth.Value.ToString("yyyy-MM-dd") : "N/A"), season, piecePay.ToString()});
            return output;
        }

        /// <summary>
        /// Method checks if the input season is a valid season
        /// </summary>
        /// <param name="input">input string to be checked</param>
        /// <returns></returns>
        public bool CheckSeason(string input)
        {
            bool retV = false;
            string[] validSeasons = { "spring", "summer", "fall", "winter", "" };
            foreach (string s in validSeasons)
            {
                if (input.ToLower() == s)
                {
                    retV = true;
                }
            }
            return retV;
        }

        /// <summary>
        /// The setter for the season variable
        /// </summary>
        /// <param name="whatSeason">The string indicating what value to set the season variable to</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool SetSeason(string input)
        {
            bool retV = false;
            if (CheckSeason(input) == true)
            {
                log.writeLog(produceLogString("SET", season, input, "SUCCESS"));
                season = input;
                retV = true;
            }
            else
            {
                log.writeLog(produceLogString("SET", season, input, "FAIL") + "\nDetail:Invalid season");
            }
            return retV;
        }

        /// <summary>
        /// Validate method for season object
        /// </summary>
        /// <returns>bool indicating validity</returns>
        public bool ValidateSeason()
        {
            bool retV = false;
            if (CheckSeason(season) == true)
            {
                log.writeLog(produceLogString("VALIDATE", "", season, "SUCCESS"));
                retV = true;
            }
            else
            {
                log.writeLog(produceLogString("VALIDATE", "", season, "FAILURE") + "\nDetails: Season is invalid\n");
            }
            return retV;
        }

        /// <summary>
        /// check the input piecepay for valid values
        /// </summary>
        /// <param name="input">the decimal indicating piecepay to check</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool CheckPiecePay(Decimal input)
        {
            return input > 0m;
        }

        /// <summary>
        /// check the input piecepay string for valid values
        /// </summary>
        /// <param name="input">the string idnicating piecepay to check</param>
        /// <returns></returns>
        public bool CheckPiecePay(String input)
        {
            Decimal newSal;
            return Decimal.TryParse(input, out newSal) && CheckPiecePay(newSal);
        }

        /// <summary>
        /// The setter for the piecePay variable (decimal)
        /// </summary>
        /// <param name="whatPiecePay">The string indicating what value to set the piecePay variable to</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool SetPiecePay(Decimal input)
        {
            bool retV = false;
            if (CheckPiecePay(input) == true)
            {
                log.writeLog(produceLogString("SET", piecePay.ToString("0.00"), input.ToString("0.00"), "SUCCESS"));
                piecePay = input;
                retV = true;
            }
            else
            {
                log.writeLog(produceLogString("SET", piecePay.ToString("0.00"), input.ToString("0.00"), "FAIL") + "\nDetail:Invalid piece pay format");
            }
            return retV;
        }

        /// <summary>
        /// The setter for piecepay (string)
        /// </summary>
        /// <param name="input">input value to set piecepay to</param>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool SetPiecePay(String input)
        {
            bool retV = false;
            Decimal newPiecePay;
            if (Decimal.TryParse(input, out newPiecePay))
            {
                SetPiecePay(newPiecePay);
                retV = true;
            }
            return retV;
        }

        /// <summary>
        /// validate piecepay
        /// </summary>
        /// <returns>A boolean indicating whether the setting operation was successful</returns>
        public bool ValidatePiecePay()
        {
            bool retV = false;
            if (CheckPiecePay(piecePay) && piecePay != 0m)
            {
                log.writeLog(produceLogString("VALIDATE", "", piecePay.ToString("0.00"), "SUCCESS"));
                retV = true;
            }
            return retV;
        }

        /// <summary>
        /// Seasonal employee validate method, validates all fields to determine if employee is a valid object
        /// </summary>
        /// <returns>A bool to indicate whether the employee is in fact valid</returns>
        public override String Validate()
        {
            String output = "";
            String msg = "";
            if ((msg = base.Validate()).Length > 0)
            {
                output += msg;
            }
            if (ValidatePiecePay() == false)
            {
                output += "\nIncorrect Piece Pay:\t\"" + piecePay.ToString("0.00") + "\"";
            }
            if (ValidateSeason() == false)
            {
                output += "\nIncorrect Season:\t\"" + season + "\"";
            }
            return output;
        }

        /// <summary>
        /// Formats console output for the two variables in this class
        /// </summary>
        /// <returns>the formatted string for printing</returns>
        protected override String ConsoleDetails()
        {
            String output = "";
            output += "\r\n\tSeason:\t\t\t\t\"" + season + "\"";
            output += "\r\n\tPiece Pay:\t\t\t\"" + piecePay.ToString("0.00") + "\"";
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
