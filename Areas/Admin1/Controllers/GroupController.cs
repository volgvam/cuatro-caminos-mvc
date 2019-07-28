using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CuatroCaminosMvcApplication.Models;

namespace CuatroCaminosMvcApplication.Controllers
{
    [Authorize(Users = "admin")]
    public class GroupController : Controller
    {
        //
        // GET: /Group/

        DataManager dataManager = new DataManager();


        [CookieFilter(Order = 1)]
        public ActionResult Index()
        {

            SetViewBag();

            ViewBag.Название_танца = dataManager.SelectListСписокНаправленийТанцев(FromFormData.Название_танца);
            return View();
        }


        private void SetViewBag()
        {
            //            ViewBag.month = dataManager.SelectListСписокМесяцев(FromFormData.month);
            ViewBag.Название_танцев = dataManager.GetНазваниеТанцев();
            //            ViewBag.Код_Ученика = dataManager.SelectListСписокФиоОтОплаты(FromFormData.Код_Ученика, null);
            ViewBag.Преподаватель = dataManager.SelectListИменаПреподавателей(FromFormData.Преподаватель);
            ViewBag.Sms = dataManager.GetSms(null);
            ViewBag.Sex=dataManager.GetПол();
        }

        [HttpPost]
        [CookieFilter(Order = 1)]
        public ActionResult Index(FormCollection collection)
        {

            ViewBag.Название_танца = dataManager.SelectListСписокНаправленийТанцев(FromFormData.Название_танца);
            ViewBag.FormGruppa = collection["Название_танца"];
            return View();
        }



        //
        // GET: /Group/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Group/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Group/Create

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
        // GET: /Group/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Group/Edit/5

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
        // GET: /Group/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Group/Delete/5

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



        public FileResult DownloadCSV(string gruppa)
        {
            int group = Convert.ToInt32(gruppa);

            var sw = new StreamWriter(new MemoryStream(), Encoding.GetEncoding(1251));


            List<ListNamesAndPhones> оплата = dataManager.GetListPhoneForExport(group);

            foreach (ListNamesAndPhones record in оплата)
            {
                sw.WriteLine(String.Format("{0};{1};", record.Fio, record.Phone));
            }

            sw.Flush();
            sw.BaseStream.Seek(0, SeekOrigin.Begin);

            return File(sw.BaseStream, "text/csv", "PhoneEport_" + dataManager.GetNameGroupFromId(group) + ".csv");
        }




        public FileResult DownloadForGoogleCSV()
        {

            StreamWriter sw = dataManager.GetListPhoneForGoogleExport();


//            List<ListForGoogleExport> спискиЛюдей = dataManager.GetListPhoneForGoogleExport();

            //foreach (ListForGoogleExport record in спискиЛюдей)
            //{
            //    sw.WriteLine(String.Format("{0};{1};", record.Name, record.Phone));
            //}

            sw.Flush();
            sw.BaseStream.Seek(0, SeekOrigin.Begin);

            return File(sw.BaseStream, "text/csv", "PhoneEportForGoogle.csv");
        }


    }
}
