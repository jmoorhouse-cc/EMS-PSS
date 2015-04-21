//
// FILE: Logging.cs
// PROJECT: Employee Management System Project
// PROGRAMMERS: Jay Moorhouse, Jordan Poirier, Thom Taylor, Rachel Park
// DATE: 11/13/2014
// DESCRIPTION: Contains Validation methods used throughout the EMS application.
//   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Supporting
{
    /// <summary>
    /// Validation Class to validate all input
    /// </summary>
    public class Validation
    {
        /// <summary>
        /// Gets set each time the validation fails, detailing why it failed
        /// </summary>
        public string errorMsg { get; private set; }

        /// <summary>
        /// Validates a name string to ensure that it has no numbers and only accepted symbols
        /// </summary>
        /// <param name="inName">String to be validated</param>
        /// <returns>Bool indicating success</returns>
        public bool name(string inName)
        {
            bool returnVal = false;
            if(inName.Any(c => char.IsDigit(c)))
            {
                errorMsg = "A name cannot contain any numbers or symbols";
            }
            else if (inName == "")
            {
                errorMsg = "A name cannot be blank";
            }
            else
            {
                returnVal = true;
            }

            foreach (char c in inName)
            {
                if(char.IsPunctuation(c) && c!= '-' && c!= '\'')
                {
                    errorMsg = "Cannot contain punctuation other than \"'\" or \"=\"";
                    returnVal = false;
                    break;
                }
            }
            return returnVal;
        }

        public bool date(string inDate)
        {
            bool returnVal = false;
            DateTime newDate = default(DateTime);
            if (DateTime.TryParseExact(inDate, "yyyy-mm-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out newDate))
            {
                returnVal = true;
            }
            else
            {
                errorMsg = "The date must be entered in valid YYYY-MM-DD format";
            }
            return returnVal;
        }

        public bool rate(string inRate)
        {
            bool returnVal = false;
            Decimal newRate;
            if(inRate.Any(c => char.IsSymbol(c) || char.IsLetter(c)))
            {
                errorMsg = "The rate must only contain a numeric value";
            }
            else
            {
                if (Decimal.TryParse(inRate, out newRate))
                {
                    if (newRate > 0)
                    {
                        returnVal = true;
                    }
                    else if (newRate == 0)
                    {
                        errorMsg = "The rate cannot be 0";
                    }
                    else
                    {
                        errorMsg = "The rate cannot be less than 0";
                    }
                }
                else
                {
                    errorMsg = "The string provided is not decimal";
                } 
            }
            return returnVal;
        }

        /// <summary>
        /// Validaes a sin number
        /// </summary>
        /// <param name="inSin">string containing sin to validate</param>
        /// <returns>bool indicating whether sin is valid</returns>
        public bool sin(string inSin)
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
                returnVal = true;
            }
            else
            {
                for (i = 0; i < (inSin.Length - 1); i++)
                {
                    if (char.IsDigit(inSin[i]) && j < 8)
                    {
                        sin[j++] = (int)Char.GetNumericValue(inSin[i]);
                    }
                }
                if (sin.Length != 8)
                {
                    errorMsg = "There can only be 9 digits in an int. Got: " + (sin.Length + 1).ToString();
                }
                else
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
                    else
                    {
                        errorMsg = "The sin provided is not valid";
                    }
                }
            }
            return returnVal;
        }
    }
}
