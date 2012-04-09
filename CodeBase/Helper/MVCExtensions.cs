using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace CodeBase.Helper
{

    public static class UriExtensions
    {
        public static Uri SetPort(this Uri uri, int newPort)
        {
            var builder = new UriBuilder(uri);
            builder.Port = newPort;
            return builder.Uri;
        }
    }
    public static class MCVExtentions
    {

        public static List<SelectListItem> ToSelectList<T>(
            this IEnumerable<T> enumerable,
            Func<T, string> text,
            Func<T, string> value,
            string defaultOption)
        {
            var items = enumerable.Select(f => new SelectListItem()
            {
                Text = text(f),
                Value = value(f)
            }).ToList();
            items.Insert(0, new SelectListItem()
            {
                Text = defaultOption,
                Value = "-1"
            });
            return items;
        }
    }

    public static class UrlEncoder 
    { 
        public static string ToFriendlyUrl (this UrlHelper helper, 
            string urlToEncode) 
        { 
            urlToEncode = (urlToEncode ?? "").Trim().ToLower(); 

            StringBuilder url = new StringBuilder(); 

            foreach (char ch in urlToEncode) 
            { 
                switch (ch) 
                { 
                    case ' ': 
                        url.Append('-'); 
                        break; 
                    case '&': 
                        url.Append("and"); 
                        break; 
                    case '"': 
                        break; 
                    default: 
                        if ((ch >= '0' && ch <= '9') || 
                            (ch >= 'a' && ch <= 'z')) 
                        { 
                            url.Append(ch); 
                        } 
                        else 
                        { 
                            url.Append('-'); 
                        } 
                        break; 
                } 
            } 

            return url.ToString(); 
        }

        public static string ToFriendlyUrl(
    string urlToEncode)
        {
            urlToEncode = (urlToEncode ?? "").Trim().ToLower();

            StringBuilder url = new StringBuilder();

            foreach (char ch in urlToEncode)
            {
                switch (ch)
                {
                    case ' ':
                        url.Append('-');
                        break;
                    case '&':
                        url.Append("and");
                        break;
                    case '"':
                        break;
                    default:
                        if ((ch >= '0' && ch <= '9') ||
                            (ch >= 'a' && ch <= 'z'))
                        {
                            url.Append(ch);
                        }
                        else
                        {
                            url.Append('-');
                        }
                        break;
                }
            }

            return url.ToString();
        } 
    }
}
