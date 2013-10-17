using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEngine.Web.Helpers
{
    public static class CustomHelpers
    {
        public static MvcHtmlString Timeago(this HtmlHelper helper, DateTime dateTime)
        {
            return MvcHtmlString.Create(dateTime.ToRelativeDate());
        }
    }
}