using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuatroCaminosMvcApplication.Models;

namespace CuatroCaminosMvcApplication.Areas.Admin.Controllers
{
//    [Authorize]
    public class ServiceController : Controller
    {
        private DataManager dataManager;

        //
        // GET: /Admin/Service/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/Service/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Service/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Service/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Admin/Service/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Service/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Service/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Service/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult searchJsonPeople(string term, int? birds)
        {

            DataManager dataManager = new DataManager();
            
            return Json(dataManager.searchJsonPeople(term, birds), JsonRequestBehavior.AllowGet);
        }


        public ActionResult searchJsonPeopleAll(string term, int? birds)
        {

            DataManager dataManager = new DataManager();

            return Json(dataManager.searchJsonPeopleAll(term, birds), JsonRequestBehavior.AllowGet);
        }



        public ActionResult searchJsonPeopleLastName(string term, int? birds)
        {

            DataManager dataManager = new DataManager();

            return Json(dataManager.searchJsonPeopleLastName(term, birds), JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchJsonPeopleName(string term, int? birds)
        {

            DataManager dataManager = new DataManager();

            return Json(dataManager.searchJsonPeopleName(term, birds), JsonRequestBehavior.AllowGet);
        }


        public ActionResult searchJsonPeopleMiddleName(string term, int? birds)
        {

            dataManager = new DataManager();

            return Json(dataManager.searchJsonPeopleMiddleName(term, birds), JsonRequestBehavior.AllowGet);
        }


//        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("file.json")]
        public ActionResult searchJsonPeopleMiddleName1(string term = "ва")
        {

            dataManager = new DataManager();

            return Json(dataManager.searchJsonPeopleMiddleName(term, null), "application/json", System.Text.Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }


//        [AcceptVerbs(HttpVerbs.Post)]
//        [HttpPost]
        [ActionName("itempeople1")]
        public ActionResult searchJsonItemPeople(string user, string term, string group)
        {

            dataManager = new DataManager();

            return Json(dataManager.searchJsonItemPeople(user, term, group), "text/json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }


        //        [AcceptVerbs(HttpVerbs.Post)]
        //        [HttpPost]

        [ActionName("listgroup1")]
        public ActionResult GetGroup(string user)
        {

            dataManager = new DataManager();

            return Json(dataManager.searchJsonGetGroup(user, null), "text/json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }


        [ActionName("pay1")]
        public ActionResult GetPayFromId(string user, string id)
        {

            dataManager = new DataManager();

            return Json(dataManager.searchJsonGetPayFromId(user, id), "text/json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }



    }

}
