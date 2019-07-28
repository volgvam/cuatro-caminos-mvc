using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuatroCaminosMvcApplication.Models;

namespace CuatroCaminosMvcApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

 
        public ActionResult Index()
        {
            ViewBag.Message = "Добро пожаловать на внутренний сайт школы танцев \"Cuatro Caminos!\"";


 

//            RedirectToAction("Month", "Payment");
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }



        public ActionResult Birthday()
        {

            DataManager dataManager = new DataManager();
            ICollection <List_BirthDay> birthDays = dataManager.GetBirthDay();
            ViewBag.BirthDays = birthDays;

            return PartialView();
        }

    }
}
