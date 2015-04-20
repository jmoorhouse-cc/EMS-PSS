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
        public DateTime? timeCardDate;
        public decimal sunH, monH, tueH, wedH, thuH, friH, satH;
        public decimal sunP, monP, tueP, wedP, thuP, friP, satP;

        public TimeCardManager()
        {
            timeCardDate = null;
            empID = 0;
            sunH = 0; monH = 0; tueH = 0; wedH = 0; thuH = 0; friH = 0; satH = 0;
            sunP = 0; monP = 0; tueP = 0; wedP = 0; thuP = 0; friP = 0; satP = 0;
        }
        public bool SetHours(string whichDay, decimal hours)
        {
            bool validHour = (hours <= MAX_HOURS && hours >= MIN_HOURS);
            if (validHour)
            {
                switch (whichDay)
                {
                    case "SUN":

                        break;
                    case "MON":

                        break;
                }
            }
            return validHour;
        }

    }
}
