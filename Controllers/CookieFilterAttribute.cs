using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CuatroCaminosMvcApplication.Models;


namespace CuatroCaminosMvcApplication.Controllers
{

    public class CookieFilterAttribute : ActionFilterAttribute

    {
        HttpCookie myCookie = new HttpCookie("caminos");
        DataManager dataManager = new DataManager();
        private Cuatro_Caminos_BDEntities db = new Cuatro_Caminos_BDEntities();



        int JobOfCookie(ActionExecutingContext filterContext, string requestForm)
        {
            string cookies = (filterContext.HttpContext.Request.Cookies.Get("caminos")!= null ?
                filterContext.HttpContext.Request.Cookies["caminos"][requestForm] :
                null);

            int result = 0;

            if (!String.IsNullOrEmpty(cookies))
            {
                result=  Convert.ToInt32(cookies);
            }

            return result;
        }






        int RequestFormInt(ActionExecutingContext filterContext, string requestForm)
        {

            int result = 0;

            if (filterContext.HttpContext.Request.Form.Count > 0)
            {

                var form = filterContext.HttpContext.Request.Form[requestForm];

                if (!String.IsNullOrEmpty(form))
                {
                    result = Convert.ToInt32(form);
                    myCookie[requestForm] = form;
                }
                else
                {
                    myCookie[requestForm] = form;
                    result= JobOfCookie(filterContext, requestForm);
                }

            }
            else
            {

                result = JobOfCookie(filterContext, requestForm);

            }
 
            return result;
        }



        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {


            int LoginId = dataManager.GetFromLoginToId(filterContext.HttpContext.User.Identity.Name);
//            bool Role = filterContext.HttpContext.User.IsInRole("администратор");


            FromFormData.Код_Ученика = RequestFormInt(filterContext, "Код_Ученика");
            FromFormData.month = RequestFormInt(filterContext, "month");
            FromFormData.Название_танца = RequestFormInt(filterContext, "Название_танца");
            FromFormData.Преподаватель = RequestFormInt(filterContext, "Преподаватель");

            //if (filterContext.HttpContext.Request.Cookies.Get("caminos") != null)
            //{

            //    if (!String.IsNullOrEmpty(filterContext.HttpContext.Request.Cookies["caminos"]["Дата_оплаты"]))
            //    {
            //           FromFormData.Дата_оплаты =
            //           Convert.ToDateTime(filterContext.HttpContext.Request.Cookies["caminos"]["Дата_оплаты"] ??
            //           DateTime.Now.Date.ToString());
            //    }
            //    else
            //    {
            //        FromFormData.Дата_оплаты = DateTime.Now.Date;
            //    }

            //}
            //else
 //           {
                FromFormData.Дата_оплаты = DateTime.Now.Date;
 //           }

            


////            filterContext.Controller.ViewBag.Преподаватель = new SelectList(db.Имена_преподавателей, "Код", "ФИО");
//            filterContext.Controller.ViewBag.Месяц = new SelectList(db.Название_месяцев, "Код", "Месяц");
////            filterContext.Controller.ViewBag.Название_SMS = new SelectList(db.Название_SMS, "Код", "SMS");
//////           filterContext.Controller.ViewBag.Код_Ученика = new SelectList(db.Списки_людей, "Код", "Фамилия");






////            filterContext.Controller.ViewData["Код_Ученика"] = dataManager.SelectListСписокВсехФио(FromFormData.Код_Ученика);
//            filterContext.Controller.ViewData["month"] = dataManager.SelectListСписокМесяцев(FromFormData.month);
//            filterContext.Controller.ViewData["Название_танца"] = dataManager.SelectListСписокНаправленийТанцев(FromFormData.Название_танца);
//            filterContext.Controller.ViewData["Код_Ученика"] = dataManager.SelectListСписокФиоОтОплаты(FromFormData.Код_Ученика, null);


            
////            filterContext.Controller.ViewData["Преподаватель"] = dataManager.SelectListИменаПреподавателей(FromFormData.Преподаватель);
//            filterContext.Controller.ViewBag._Преподаватель = dataManager.SelectListИменаПреподавателей(FromFormData.Преподаватель);
//            filterContext.Controller.ViewBag._Название_танца = dataManager.SelectListСписокНаправленийТанцев(FromFormData.Название_танца);
////            filterContext.HttpContext.User.IsInRole("администратор");


//            filterContext.Controller.ViewData["names1"] = dataManager.GetFromLoginToId(filterContext.HttpContext.User.Identity.Name);

//            filterContext.Controller.ViewBag.FormMonth = FromFormData.month;
//            filterContext.Controller.ViewBag.FormGruppa = FromFormData.Название_танца;

            if (filterContext.HttpContext.Request.Form.Count > 0)
            {
                myCookie.Expires = DateTime.Now.AddDays(1d);
//                myCookie["Дата_оплаты"] = filterContext.HttpContext.Request.Form["Дата_оплаты"];

                filterContext.HttpContext.Response.Cookies.Add(myCookie);
                
            }


            


            base.OnActionExecuting(filterContext);
        }

    }
}