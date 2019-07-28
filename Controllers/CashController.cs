using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuatroCaminosMvcApplication.Models;

namespace CuatroCaminosMvcApplication.Controllers
{
    [Authorize(Users = "admin")]
    public class CashController : Controller
    {
        //
        // GET: /Cash/

        DataManager dataManager = new DataManager();

        public ActionResult Index()
        {

            return View();
        }

        public PartialViewResult IndexPartial()
        {
            return PartialView("IndexPartial",dataManager.GetCash());
        }


        //
        // GET: /Cash/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Cash/Create

        public ActionResult Create()
        {

            SetViewBag();
            ViewBag.DataTimeCreate = DateTime.Now;

            return View();
        } 

        //
        // POST: /Cash/Create

        [HttpPost]
        public ActionResult Create(Cash cash, FormCollection collection)
        {

            try
            {
                // TODO: Add insert logic here

                dataManager.CreateCash(cash);

                return RedirectToAction("IndexPartial");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Cash/Edit/5

        public ActionResult Edit(int id)
        {
            Cash cash = dataManager.GetCash(id);

            FromFormData.Дата_оплаты=cash.DataTimePay;
            FromFormData.Код_Ученика = cash.PeopleId;
            FromFormData.Название_танца = cash.GroupId;

            SetViewBag();

            ViewBag.DataPay = cash.DataTimePay;

            return View(cash);
        }

        //
        // POST: /Cash/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Cash cash = dataManager.GetCash(id);

                cash.DataTimeCreate = DateTime.Now;
                cash.LoginRecId = dataManager.LiginRecId();
                cash.Payment_expenses = true;
                cash.CategoryId = 1;

                UpdateModel(cash);

                dataManager.SaveChanges();

                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Cash/Delete/5
 
        public ActionResult Delete(int id)
        {

            return View();
        }

        //
        // POST: /Cash/Delete/5

        [HttpPost]
        public PartialViewResult Delete(int id, FormCollection collection)
        {



            try
            {
                // TODO: Add delete logic here

                dataManager.DeleteCash(id);

                return PartialView("IndexPartial", dataManager.GetCash());
            }
            catch
            {
                return PartialView("IndexPartial", dataManager.GetCash());
            }
        }


        public ActionResult CashAndPay()
        {
            CashAndPayViewModel cashAndPayViewModel = new CashAndPayViewModel(dataManager.GetCashAndPay());

            return View(cashAndPayViewModel);
        }

        private void SetViewBag()
        {
            ViewBag.Group = dataManager.SelectListСписокНаправленийТанцев(FromFormData.Название_танца);
            ViewBag.People = dataManager.SelectListСписокФио(FromFormData.Код_Ученика);
            ViewBag.Преподаватель = dataManager.SelectListИменаПреподавателей(FromFormData.Преподаватель);
        }


    }
}
