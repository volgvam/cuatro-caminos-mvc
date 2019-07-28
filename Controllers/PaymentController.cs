using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuatroCaminosMvcApplication.Models;
using System.Data.Entity;

namespace CuatroCaminosMvcApplication.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        //
        // GET: /payment/

        private Cuatro_Caminos_BDEntities db = new Cuatro_Caminos_BDEntities();


        DataManager dataManager = new DataManager();


        public ActionResult PartialОплата(int id)
        {

            return PartialView("_PartialОплата", dataManager.GetОплатаFromФИО(id));
        }

        public ActionResult Group()
        {
            return View(dataManager.GetPayGroup());
        }


        [CookieFilter]
        public ActionResult List()
        {
            SetViewBag();
            return View(dataManager.GetОплатаData());
        }




        [CookieFilter(Order = 1)]
        [RoleFilter(Order = 10)]
        public ActionResult Index()
        {
            SetViewBag();
            return View(dataManager.GetОплата());
        }


        
        [HttpPost]
        [CookieFilter(Order = 1)]
        [RoleFilter(Order = 10)]
        public ActionResult Index(FormCollection collection)
        {


            SetViewBag();
            return View(dataManager.GetОплата());
        }


        //
        // GET: /payment/Details/5

        public ActionResult GroupSummary(int? month)
        {

            if (month == 13)
            {
                month = DateTime.Now.AddMonths(-1).Month;
            }

            GroupSummaryViewModel groupSummaryViewModel = new GroupSummaryViewModel(dataManager.GetОплата(month));

            return View(groupSummaryViewModel.Get());
        }

        public ActionResult Details(int id)
        {


//            int loginRecId = dataManager.GetIdFromОплатаLoginRecId(id);
            Списки_людей спискиЛюдей = dataManager.GetPeapleFioFromId(dataManager.GetIdFromОплатаLoginRecId(id));

            ViewBag.LoginRecFio = спискиЛюдей.ФИО;

            return View(dataManager.GetОплата(id));

        
        }

        //
        // GET: /payment/Create

//        [CookieFilter]
        public ActionResult Create()
        {


//            ViewBag._Преподаватель = dataManager.SelectListИменаПреподавателей(FromFormData.Преподаватель);
//            ViewBag.ФИО = dataManager.ListСписокВсехФио();
            ViewBag._Код_Ученика = dataManager.SelectListСписокФио(FromFormData.Код_Ученика); 
            ViewBag._Название_танца = dataManager.SelectListСписокНаправленийТанцев(FromFormData.Название_танца);
            ViewBag._Преподаватель = dataManager.SelectListИменаПреподавателей(FromFormData.Преподаватель);


            ViewBag.Дата = FromFormData.Дата_оплаты;

            return View();
        } 

        //
        // POST: /payment/Create

//        [CookieFilter]
        [HttpPost]
        public ActionResult Create(Оплата оплата, FormCollection collection)
        {

            dataManager.CreateОплата(оплата);
            
            // TODO: Add insert logic here


            try
            {
               
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /payment/Edit/5


//        [Authorize(Users = "admin")]
        public ActionResult Edit(int id)
        {

            Оплата оплата = dataManager.GetОплата(id);
            ViewBag._Преподаватель = dataManager.SelectListИменаПреподавателей(оплата.Преподаватель);
            ViewBag.ФИО = dataManager.ListСписокВсехФио();
//            ViewBag._Код_Ученика = dataManager.SelectListСписокФио(оплата.Код_Ученика);
//            ViewBag.Fio = dataManager.GetСписокФиоFromGroup() оплата.Код_Ученика;
            ViewBag._Название_танца = dataManager.SelectListВсехСписокНаправленийТанцев(оплата.Названия_танцев.Код);

            ViewBag.Дата = оплата.Дата_оплаты.Date;

            Списки_людей спискиЛюдей = dataManager.GetPeapleFioFromId(dataManager.GetIdFromОплатаLoginRecId(id));
            ViewBag.LoginRecFio = спискиЛюдей.ФИО;





            return View(dataManager.GetОплата(id));
        }

        //
        // POST: /payment/Edit/5

        [HttpPost]
        [CookieFilter]
        public ActionResult Edit(int id, FormCollection collection)
        {


            Оплата оплата = dataManager.GetОплата(id);

            оплата.Месяц = Convert.ToDateTime(collection["Дата_оплаты"]).Month;
            оплата.DateTimeRec = DateTime.Now;
            оплата.LoginRecId = dataManager.GetFromLoginToId(HttpContext.User.Identity.Name); 

            UpdateModel(оплата);
            
            dataManager.SaveChanges();


            try
            {
                // TODO: Add update logic here


                return Redirect("/payment/details/"+id);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /payment/Delete/5
 
        public ActionResult Delete(int id)
        {


            return View(dataManager.GetОплата(id));
        }

        //
        // POST: /payment/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {

            dataManager.DeleteОплата(id);
 

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

        public ActionResult Month(int? id, int? month, int? gruppa, FormCollection collection)
        {

            VisitsDataManager visitsDataManager = new VisitsDataManager();
            MyEntityViewModel viewModel = new MyEntityViewModel(dataManager.GetMonthОплата(month, gruppa), visitsDataManager.GetSumm());
            ViewBag.NameGroup = dataManager.GetNameGroupFromIdAndDescriptoin(gruppa);

//            ViewBag.Оплата = dataManager.GetMonthОплата(DateTime.Now.Month);
            return View(viewModel);
        }

        
        
        [CookieFilter]
        public PartialViewResult ListPayPartial()
        {

            ICollection<Оплата> оплата = dataManager.GetОплатаListPartial(DateTime.Now);
            return PartialView("_PartialОплата", оплата);
        }



        [HttpPost]
//        [CookieFilter]
        public PartialViewResult CreateAjax(Оплата оплата, FormCollection collection)
        {

            dataManager.CreateОплата(оплата);

            // TODO: Add insert logic here


            try
            {
                ICollection<Оплата> оплата1 = dataManager.GetОплатаListPartial(оплата.Дата_оплаты, оплата.Названия_танцев.Код);
                return PartialView("_PartialОплата", оплата1);
            }
            catch
            {
                return PartialView();
            }
        }





        private void SetViewBag()
        {
            ViewBag.month = dataManager.SelectListСписокМесяцев(FromFormData.month);
            ViewBag.Название_танца = dataManager.SelectListСписокНаправленийТанцев(FromFormData.Название_танца);
            ViewBag.Код_Ученика = dataManager.SelectListСписокФиоОтОплаты(FromFormData.Код_Ученика, null);
            ViewBag.Преподаватель = dataManager.SelectListИменаПреподавателей(FromFormData.Преподаватель);
        }




    }
}
