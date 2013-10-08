using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogEngine.Web.Helpers
{
    public class DateFormatter
    {
        public static string Format(DateTime Date)
        {
            string date = Date.ToString("MMM") 
                            + " " 
                            + Date.Day.ToString() 
                            + "," 
                            + Date.ToString("yyyy") 
                            + " at " 
                            + Date.ToShortTimeString();

            return date;
        }
    }
}