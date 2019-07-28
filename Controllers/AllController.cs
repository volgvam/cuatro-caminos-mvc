using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuatroCaminosMvcApplication.Models;
using CuatroCaminosMvcApplication.Models.All;

namespace CuatroCaminosMvcApplication.Controllers
{
    public class AllController : Controller
    {
        //
        // GET: /OpenPeople/

        private AllPeopleFormEnter allPeopleFormEnter;
        DataManager dataManager = new DataManager();


        public ActionResult Index()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Index(AllPeopleFormEnter allPeopleFormEnter)
        {

            this.allPeopleFormEnter = allPeopleFormEnter;

            int id;

            if(ModelState.IsValid)
            {

                id = dataManager.ListСписокВсехФио()
                                .Where(e => e.Фамилия == allPeopleFormEnter.LastName
                                            && e.Отчество == allPeopleFormEnter.FirstName
                                            && e.Имя == allPeopleFormEnter.Name
                                            && e.Телефон == allPeopleFormEnter.Phone
                    )
                                .Select(e => e.Код)
                                .SingleOrDefault();



                if (id > 0)
                {
                    AllPeopleDiscountViewModel allPeopleDiscountViewModel = new AllPeopleDiscountViewModel(id);
                    return RedirectToAction("Details");
                }

                    

                }
            return View();
        }

 
  
        public ActionResult Details(AllPeopleDiscountViewModel allPeopleDiscountViewModel)
        {


            return View(allPeopleDiscountViewModel);
        }


        //
        // GET: /OpenPeople/Details/5

        public ActionResult Details1(int id)
        {


            return View();
        }

        //
        // GET: /OpenPeople/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /OpenPeople/Create

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
        // GET: /OpenPeople/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /OpenPeople/Edit/5

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
        // GET: /OpenPeople/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /OpenPeople/Delete/5

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
