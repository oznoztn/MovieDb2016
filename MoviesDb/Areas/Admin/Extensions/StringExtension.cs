using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesDb.Areas.Admin.Extensions
{
    public static class StringExtensionMethods
    {
        public static string ToImageName(this string value, string fileFormat)
        {
            var str = value.ToLower()
                .Replace(" ", "-")
                .Replace("?","")
                .Replace(":","")
                .Replace("!","") +"."+fileFormat.Split('/')[1];

            return str;
        }
    }
}