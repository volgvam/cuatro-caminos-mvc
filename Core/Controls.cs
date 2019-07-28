using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuatroCaminosMvcApplication.Models;

namespace MVC3Controls_H.Core
{
    public static class Controls
    {
        public static MvcHtmlString AutoComplite(this HtmlHelper helper, IDictionary<string, int> items)
        {
            return new MvcHtmlString("Hello, i'm your CheckBoxList!");
        }
    }
}