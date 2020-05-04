using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebAppNetCore.Infrastructure
{
    public static class UrlExtensions
    {
        //nowy URL + nalezy dodac namespace do _ViewImports.cshtml
        public static string PathAndQuery(this HttpRequest request) =>
             request.QueryString.HasValue
                 ? $"{request.Path}{request.QueryString}"
                 : request.Path.ToString();
    }
}
