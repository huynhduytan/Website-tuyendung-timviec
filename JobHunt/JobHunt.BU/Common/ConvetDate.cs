using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.BU.Common
{
    public static class ConvetDate
    {
        //convert to datetime
        public static DateTime? ParseRequestDate(string dt)
        {
            // https://stackoverflow.com/questions/2883576/how-do-you-convert-epoch-time-in-c

            CultureInfo enUS = new CultureInfo("en-US");

            //var dt = "7/25/2013 6:37:31 PM";
            //var dt = "2013-07-25 14:26:00";

            DateTime dateValue;

            //// Scenario #1
            //if (long.TryParse(dt, out dtLong))
            //    return dtLong.FromUnixTime();

            // Scenario #2 & #3
            var formatStrings = new string[] { "MM/dd/yyyy hh:mm:ss tt", "yyyy-MM-dd hh:mm:ss", "M/dd/yyyy hh:mm:ss tt", "dd-MM-yyyy", "dd-M-yyyyy", "M/d/yyyy hh:mm:ss tt", "dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd" };
            if (DateTime.TryParseExact(dt, formatStrings, enUS, DateTimeStyles.None, out dateValue))
                return dateValue;
            if (DateTime.TryParse(dt, out dateValue))
            {
                // Yay :)
                return dateValue;
            }
            else
            {
                return null;
                // Aww.. :(
            }
            //throw new SomeException("Don't know how to parse...");
        }
    }
}
