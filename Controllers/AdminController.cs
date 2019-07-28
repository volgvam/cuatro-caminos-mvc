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
    public class AdminController : Controller
    {

        DataManager dataManager = new DataManager();

        //
        // GET: /Admin/

        public ActionResult Index()
        {
//            DownloadCSV();

            return View(dataManager.GetAdministrator());
        }

        //
        // GET: /Admin/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Create

        [CookieFilter]
        public ActionResult Create()
        {

            SetViewBag();

            return View();
        } 

        //
        // POST: /Admin/Create

        [CookieFilter]
        [HttpPost]
        public ActionResult Create(Administrators administrators)
        {

            dataManager.CreateAdministrators(administrators);
            
            
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
        // GET: /Admin/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Edit/5

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
        // GET: /Admin/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(dataManager.GetAdministrator(id));
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                dataManager.DeleteAdministrator(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void SetViewBag()
        {
            ViewBag.month = dataManager.SelectListСписокМесяцев(FromFormData.month);
            ViewBag.Название_танца = dataManager.SelectListСписокНаправленийТанцев(FromFormData.Название_танца);
            ViewBag.Код_Ученика = dataManager.SelectListСписокФиоОтОплаты(FromFormData.Код_Ученика, null);
        }


//        private void ExportAsCSV(IEnumerable<Списки_людей> filterRecords)
 
        private void ExportAsCSV()
         {
            Cuatro_Caminos_BDEntities bd = new Cuatro_Caminos_BDEntities();

            var sw = new StringWriter();
         
            //write the header 
            sw.WriteLine(String.Format("{0},{1},{2},{3}", 1, 2, 2, 4));

            //write every subscriber to the file 
//            var resourceManager = new ResourceManager(typeof(CMSMessages));
            foreach (Списки_людей record in bd.Списки_людей)
            {
                sw.WriteLine(String.Format("{0},{1},{2},{3}", "тест", record.ФИО, record.Телефон, record.Пол));
            }

            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=adressenbestand.csv");
            Response.ContentType = "text/csv";
            Response.Write(sw);
            Response.End();

        }


        public ActionResult EportGroupFileCsv()
        {
            ICollection<Оплата> оплата = dataManager.GetОплата();
//             оплата.Select(e => new {e.Названия_танцев.Код, e.Названия_танцев.Название_танца});
            

            return View();
        }


 
    }



}
