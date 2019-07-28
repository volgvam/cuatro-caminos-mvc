using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using CuatroCaminosMvcApplication.Models;
using System.Web.Mvc.Html;


namespace CuatroCaminosMvcApplication.MyHtmlHelpers
{
    public static class MyHtmlHelpers
    {
 
        public static string TestHelper (this HtmlHelper htmlHelper, string test)
        {
            return test;
        }

        public static SelectList TestHelper1(this HtmlHelper htmlHelper, int id)
        {       
            DataManager dataManager = new DataManager();

            return dataManager.SelectListСписокМесяцев(0);
        }

/// <summary>
/// Возвращает список групп
/// </summary>
/// <param name="htmlHelper"></param>
/// <param name="group">Имя параметра "названия" группы</param>
/// <returns></returns>
        public static MvcHtmlString ListGroup(this HtmlHelper htmlHelper, string group)
        {
            DataManager dataManager = new DataManager();

            IEnumerable<Названия_танцев> nameDancer =dataManager.GetНазваниеТанцев();

            var builder = new StringBuilder();
            
            string groupIdstr = htmlHelper.ViewContext.HttpContext.Request.QueryString[group];
            string uri = htmlHelper.ViewContext.HttpContext.Request.Url.Query;
            string qq = HttpUtility.ParseQueryString(htmlHelper.ViewContext.HttpContext.Request.Url.Query)["group"];

            int groupId = !String.IsNullOrEmpty(groupIdstr)? int.Parse(groupIdstr): 0 ;
            

            foreach (var NameDance in nameDancer)
            {

                if (NameDance.Код != groupId)
                {
                    builder.Append(htmlHelper.ActionLink(NameDance.Название_танца, null, new { Group = NameDance.Код }));
                }
                else
                {
                    builder.Append("<strong>");
                    builder.Append(NameDance.Название_танца);
                 
//                    builder.Append(groupIdstr.GetHashCode());
//                    builder.Append(qq);
                    
                    builder.Append("</strong>");
                } 
                    
                    builder.Append(" | ");
            }   

//            MvcHtmlString mvcHtmlString = ;
            

            return new MvcHtmlString(builder.ToString());
        }
        
    }
}