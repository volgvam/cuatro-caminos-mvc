using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuatroCaminosMvcApplication.Models;

namespace CuatroCaminosMvcApplication.Controllers
{
    [Authorize]
    public class VisitsController : Controller
    {
        //
        // GET: /Visits/
        DataManager dataManager = new DataManager();
        VisitsDataManager visitsDataManager = new VisitsDataManager();


        public ActionResult _Index()
        {
            IEnumerable<PayViewModel> model = null;

            return View(model);
        }




        public ActionResult List(int? group)
        {

            
            //            IEnumerable<VisitsViewModel> model = VisitsViewModel.GetPeople();

            return PartialView(visitsDataManager.PartialVisitModels(group??19));
        }


        

        [HttpPost]
        public ActionResult Index(int group, FormCollection collection)
        {

            visitsDataManager.SaveVisit(group, collection);



            return RedirectToAction("Index");
        }



        public ActionResult Index1()
        {
            
            VisitsViewModel visitsViewModel = new VisitsViewModel();

//            IEnumerable<VisitsViewModel> model = VisitsViewModel.GetPeople();

            return View(visitsViewModel);
        }


        public ActionResult Create1()
        {

            Visits visitsViewModel = new Visits();


            //            IEnumerable<VisitsViewModel> model = VisitsViewModel.GetPeople();

            return View(visitsViewModel);
        }


        public ActionResult Create2()
        {

//            VisitsModel visitsModel = new VisitsModel();


            //            IEnumerable<VisitsViewModel> model = VisitsViewModel.GetPeople();

            return View(new Visits());
        }





        public ActionResult Index(int? group)
        {
//            IEnumerable<Visits> model = dataManager.GetVisits();

            ViewData["Название_танцев"] = dataManager.GetНазваниеТанцев();
            ViewBag.Имя_группы = dataManager.GetNameGroupFromIdAndDescriptoin(group);


 
            return View(new Visits());
        }


        //
        // GET: /Visits/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Visits/Create

        public ActionResult Create()
        {


            return View();
        } 

        //
        // POST: /Visits/Create

        [HttpPost]
        public ActionResult Create(Visits visits, int group, FormCollection collection)
        {

            dataManager.CreateVisits(visits);

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
        // GET: /Visits/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Visits/Edit/5

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
        // GET: /Visits/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Visits/Delete/5

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
    }
}
