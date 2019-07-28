using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CuatroCaminosMvcApplication.Models;

namespace CuatroCaminosMvcApplication.Controllers
{

    [Authorize]
    public class ActivityController : Controller
    {
        //
        // GET: /Аctivity/

        DataManager dataManager = new DataManager();

        public ActionResult Index()
        {

            ICollection<Название_мероприятий> названиеМероприятий = dataManager.GetНазваниеМероприятий();

            return View(названиеМероприятий);
        }

        //
        // GET: /Аctivity/Details/5

        public ActionResult Details(int id)
        {
            ViewBag.NameActivity = dataManager.GetНазваниеМероприятий(id).Select(e => e.Мероприятие).SingleOrDefault();
            ViewBag.Description = dataManager.GetНазваниеМероприятий(id).Select(e => e.Примечание).SingleOrDefault();
            ViewBag.ActivityId = id;
            

            ICollection<СписокНаМероприятие> списокНаМероприятие = dataManager.GetСписокНаМероприятие(id);

            return View(списокНаМероприятие);
        }

        //
        // GET: /Аctivity/Create

        public ActionResult Create()
        {


            return View();
        } 

        //
        // POST: /Аctivity/Create

        [HttpPost]
        public ActionResult Create(Название_мероприятий названиеМероприятий)
        {
            

            try
            {

                if (ModelState.IsValid)
                {

                    dataManager.CreateActivity(названиеМероприятий);
                }

                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Аctivity/Edit/5
 

        public ActionResult Edit(int id)
        {

            Название_мероприятий названиеМероприятий = dataManager.GetНазваниеМероприятия(id);

            return View(названиеМероприятий);
        }

        //
        // POST: /Аctivity/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {

            
            try
            {
                if (ModelState.IsValid)
                {


                    Название_мероприятий названиеМероприятий = dataManager.GetНазваниеМероприятия(id);
                    названиеМероприятий.DataRec = DateTime.Now;
                    названиеМероприятий.LiginRecId = dataManager.GetFromLoginToId(HttpContext.User.Identity.Name);

                    UpdateModel(названиеМероприятий);

                    dataManager.SaveChanges();
                }

                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Аctivity/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Аctivity/Delete/5

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

        public ActionResult People(int id)
        {
            ICollection<СписокНаМероприятие> списокНаМероприятия = dataManager.GetСписокНаМероприятие(id);

            return View(списокНаМероприятия);
        }

        public ActionResult searchJson(string term, int? birds)
        {
            Cuatro_Caminos_BDEntities bd = new Cuatro_Caminos_BDEntities();

            var r = bd.Списки_людей
                .Select(e => new { id = e.Код, value = e.Фамилия + " " + e.Имя + " " + e.Отчество })
                .Where(e => e.value.Contains(term));

            //var result = db.kullanicis
            //    .Where(x => x.adi.Contains(term) || x.soyAdi.Contains(term))
            //    .Select(x => new { id = x.id, value = x.adi }); 
            
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ListActivityDetailsFromPeopleId(int idPeaple, int? id)
        {

            ViewBag.ActivityId = id;


            ICollection<СписокНаМероприятие> списокНаМероприятие1 = dataManager.GetСписокНаМероприятиеFromPeopleId(idPeaple);
            return PartialView("ListActivityDetailsLight", списокНаМероприятие1);
        }


        public PartialViewResult CreateActivityDetails(int id)
        {

            ViewBag.ActivityId = id;
            


            ICollection<СписокНаМероприятие> списокНаМероприятие1 = dataManager.GetСписокНаМероприятие(id);
            ViewBag.CloseActivity = dataManager.GetНазваниеМероприятия(id).Close;
            SetViewBag();

            return PartialView();
        }




        [HttpPost]
        public PartialViewResult CreateActivityDetails(СписокНаМероприятие списокНаМероприятие, int id)
        {
            dataManager.CreateActivityDetails(списокНаМероприятие);

            ICollection<СписокНаМероприятие> списокНаМероприятие1 = dataManager.GetСписокНаМероприятие(id);

            ViewBag.CloseActivity = dataManager.GetНазваниеМероприятия(id).Close;

            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("CreateActivityDetails", списокНаМероприятие1);
            //}

            return  PartialView("ListActivityDetails", списокНаМероприятие1);
        }



        public PartialViewResult ListActivityDetails(СписокНаМероприятие списокНаМероприятие, int id)
        {

 
            ICollection<СписокНаМероприятие> списокНаМероприятие1 = dataManager.GetСписокНаМероприятие(id);
            ViewBag.CloseActivity = dataManager.GetНазваниеМероприятия(id).Close;


            return PartialView(списокНаМероприятие1);
        }


        [HttpPost]
        public PartialViewResult DeleteActivityDetails(int idActivity, int id)
        {


            dataManager.DeleteActivityDetails(idActivity);
           

            ICollection<СписокНаМероприятие> списокНаМероприятие1 = dataManager.GetСписокНаМероприятие(id);
            ViewBag.CloseActivity = dataManager.GetНазваниеМероприятия(id).Close;

            return PartialView("ListActivityDetails", списокНаМероприятие1);
        }


        public FileResult ExportCsv(int? id=null)
        {

            var sw = new StreamWriter(new MemoryStream(), Encoding.GetEncoding(1251));


            List<ListNamesAndPhones> phoneses = dataManager.GetСписокНаМероприятие(id??0)
                .Select(e => 
                    new ListNamesAndPhones{Fio = e.Списки_людей.ФИ, Phone = e.Списки_людей.Телефон}
                        )
                .OrderBy(e=>e.Fio)
                .Distinct()
                .ToList();


            foreach (ListNamesAndPhones record in phoneses)
            {
                sw.WriteLine(String.Format("{0};+7{1};", record.Fio, record.Phone));
            }

            sw.Flush();
            sw.BaseStream.Seek(0, SeekOrigin.Begin);

            // , new{id=Model.Select(e=>e.Код)}
            // dataManager.GetНазваниеМероприятий(id).Select(e => e.Мероприятие).SingleOrDefault() +

            return File(sw.BaseStream, "text/csv", "PhoneEport_activity" +  ".csv");
        }


        public void SetViewBag()
        {
            ViewBag._PeopleId = dataManager.SelectListСписокФио(FromFormData.Код_Ученика); 
        }

    
    
    }
}
