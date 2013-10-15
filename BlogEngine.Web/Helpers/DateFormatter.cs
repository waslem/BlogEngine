using System;
using System.Globalization;

namespace BlogEngine.Web.Helpers
{
    public class DateFormatter
    {
        public static string Format(DateTime date)
        {
            string dateFormatted = date.ToString("MMM") 
                            + " " 
                            + date.Day.ToString(CultureInfo.InvariantCulture) 
                            + "," 
                            + date.ToString("yyyy") 
                            + " at " 
                            + date.ToShortTimeString();

            return dateFormatted;
        }
    }
}