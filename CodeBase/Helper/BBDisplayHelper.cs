using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Web.WebPages.Html;
using CodeBase.Models;

namespace CodeBase.Helper
{
    public static class Helpers
    {
        public static String BBCode(this HtmlHelper helper, string content)
        {
            return BBCodeHelper.Format(content);
            
        }
    }
}