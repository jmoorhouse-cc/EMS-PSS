using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supporting
{
    public class TimeCardManager
    {
        private const int MAX_HOURS = 24;
        private const int MIN_HOURS = 0;
        private const int MIN_PIECES = 0;
        public int empID;
        public DateTime timeCardDate;
        public decimal sunH, monH, tueH, wedH, thuH, friH, satH;
        public decimal sunP, monP, tueP, wedP, thuP, friP, satP;

        public TimeCardManager(DateTime dateData)
        {
            timeCardDate = DateTime.MinValue;
            empID = 0;
            sunH = 0; monH = 0; tueH = 0; wedH = 0; thuH = 0; friH = 0; satH = 0;
            sunP = 0; monP = 0; tueP = 0; wedP = 0; thuP = 0; friP = 0; satP = 0;
            DateTime firstDay = CalcSunDate(dateData);
        }
        public bool SetHours(string whichDay, decimal hours)
        {
            bool validHour = (hours <= MAX_HOURS && hours >= MIN_HOURS);
            if (validHour)
            {
                switch (whichDay)
                {
                    case "SUN":
                        sunH = hours;
                        break;
                    case "MON":
                        monH = hours;
                        break;
                    case "TUE":
                        tueH = hours;
                        break;
                    case "WED":
                        wedH = hours;
                        break;
                    case "THU":
                        thuH = hours;
                        break;
                    case "FRI":
                        friH = hours;
                        break;
                    case "SAT":
                        satH = hours;
                        break;
                }
            }
            return validHour;
        }

        public bool SetPieces(string whichDay, int pieces)
        {
            bool validPieces = (pieces > MIN_PIECES);
            if (validPieces)
            {
                switch (whichDay)
                {
                    case "SUN":
                        sunP = pieces;
                        break;
                    case "MON":
                        monP = pieces;
                        break;
                    case "TUE":
                        tueP = pieces;
                        break;
                    case "WED":
                        wedP = pieces;
                        break;
                    case "THU":
                        thuP = pieces;
                        break;
                    case "FRI":
                        friP = pieces;
                        break;
                    case "SAT":
                        satP = pieces;
                        break;
                }
            }
            return validPieces;
        }

        private DateTime CalcSunDate(DateTime dateData)
        {
            DateTime dateSunday = dateData;

            switch (dateData.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    dateData = dateSunday;
                    break;
                case DayOfWeek.Monday:
                    dateData = dateSunday.AddDays(-6);
                    break;
                case DayOfWeek.Tuesday:
                    dateData = dateSunday.AddDays(-5);
                    break;
                case DayOfWeek.Wednesday:
                    dateData = dateSunday.AddDays(-4);
                    break;
                case DayOfWeek.Thursday:
                    dateData = dateSunday.AddDays(-3);
                    break;
                case DayOfWeek.Friday:
                    dateData = dateSunday.AddDays(-2);
                    break;
                case DayOfWeek.Saturday:
                    dateData = dateSunday.AddDays(-1);
                    break;
            }
            return dateSunday;
        }

        

        public string ToDBString()
        {
            string dbString = "'" ;

            return dbString;
        }
    }
}
