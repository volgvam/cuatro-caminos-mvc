using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using CuatroCaminosMvcApplication.Models;


namespace CuatroCaminosMvcApplication.Controllers
{

    [Authorize]
    //    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    public class PeopleController : Controller
    {
        //
        // GET: /СпискиЛюдей/

        private Cuatro_Caminos_BDEntities db = new Cuatro_Caminos_BDEntities();

        //        private string[] alfavit = new string[] { "А", "Б", "В", "Г", "Д", "Е", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

        DataManager dataManager = new DataManager();

        public ActionResult Index(FormCollection collection, int? page, char? alfavit)
        {

            //            dataManager.GetPeaple();
            //int pageSize = 3;
            //int pageIndex = (page ?? 1) - 1; 

            ViewBag.Alfavit = dataManager.GetAlfavitFromФИО();


            return View(dataManager.GetPeaple(alfavit));
        }


        public ActionResult Group(int? group)
        {

            ViewBag.Название_танцев = dataManager.GetНазваниеТанцев();
            ViewBag.Имя_группы = dataManager.GetNameGroupFromId(group);
            Названия_танцев singleOrDefault = dataManager.GetНазваниеТанцев().SingleOrDefault(i => i.Код == @group);
            if (singleOrDefault != null)
                ViewBag.Description = singleOrDefault.Description;


            return View(dataManager.GetСписокФиоFromGroup(group));
        }


        public ActionResult Schools(int schools)
        {

            ICollection<Списки_людей> спискиЛюдей = dataManager.GetPeapleSchools(schools);
            Название_школ названиеШколы = dataManager.GetНазваниеШкол(schools);

            string названиеШкол = null;

            if (названиеШколы != null)
            {
                названиеШкол = названиеШколы.Школы;
            }

            ViewBag.School = String.Format(" '{0}' ", названиеШкол);

            ViewBag.Schools = dataManager.GetНазваниеШкол();


            return View(спискиЛюдей);
        }


        public ActionResult DoNotGo(int? group, int? day)
        {

            IDictionary<int, string> days = new Dictionary<int, string>();
            //            days.Add(-7, "1 неделю");
            //            days.Add(-14, "2 недели");
            //            days.Add(-21, "3 недели");
            days.Add(-30, "1 месяц");
            //            days.Add(-45, "1,5 месяца");
            days.Add(-60, "2 месяца");
            days.Add(-90, "3 месяца");

            ViewBag.Название_танцев = dataManager.GetНазваниеТанцев();
            ViewBag.Имя_группы = dataManager.GetNameGroupFromId(group);
            ViewBag.Days = days;

            return View(dataManager.GetPeapleDoNotGo(group, day));
        }




        //
        // GET: /СпискиЛюдей/Details/5

        public ActionResult Details(int id)
        {

            //            ViewData["Оплата"] = dataManager.GetОплатаFromФИО(id);
            int LoginRecId = dataManager.ListСписокВсехФио().Where(e => e.Код==id).Select(e=>e.LoginRecId).SingleOrDefault();
            Списки_людей спискиЛюдей = dataManager.GetPeapleFioFromId(LoginRecId);
            ViewBag.LoginRecFio = спискиЛюдей.ФИО;



            return View(dataManager.GetPeaple(id));
        }



        //
        // GET: /СпискиЛюдей/Create

        public ActionResult Create()
        {
            SetViewBag();
            ViewBag._Школы = dataManager.SelectListСписокВсехШкол(7);
            ViewBag._Название_SMS = dataManager.SelectListSms(1);

            return View();
        }

        //
        // POST: /СпискиЛюдей/Create

        [HttpPost]
        public ActionResult Create(Списки_людей спискиЛюдей, FormCollection collection)
        {

            //               ViewBag._Message = String.Format("Записись сохранена");

            // TODO: Add insert logic here


            dataManager.CreateНовыйКлиент(спискиЛюдей);
            return RedirectToAction("Index");


            //if (ModelState.IsValid)
            //{
            //    dataManager.CreateНовыйКлиент(спискиЛюдей);
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    SetViewBag();
            //    return View();
            //}


        }

        //
        // GET: /СпискиЛюдей/Edit/5

        public ActionResult Edit(int id)
        {
            Списки_людей спискиЛюдей = dataManager.GetPeaple(id);


            SetViewBag(спискиЛюдей);

            return View(спискиЛюдей);
        }

        //
        // POST: /СпискиЛюдей/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {



            try
            {
                // TODO: Add update logic here

                if (ModelState.IsValid)
                {
                    Списки_людей спискиЛюдей = dataManager.GetPeaple(id);

                    спискиЛюдей.DateTimeRec = DateTime.Now;
                    спискиЛюдей.LoginRecId = dataManager.GetFromLoginToId(HttpContext.User.Identity.Name);

                    //if (collection["Выбыл_из_школы"] != "false")
                    //{
                    //    if (спискиЛюдей.Дата_выбытия == null)
                    //    {
                    //        спискиЛюдей.Дата_выбытия = DateTime.Now;
                    //    }

                    //}
                    //else
                    //{
                    //    спискиЛюдей.Дата_выбытия = null;
                    //}


                    UpdateModel(спискиЛюдей);
                    dataManager.SaveChanges();



                    ViewBag._Message = String.Format("Записись сохранена");

                    return RedirectToAction("Index");


                }
                else
                {
                    return View();
                }

            }
            catch
            {
                return View();
            }

        }

        //
        // GET: /СпискиЛюдей/Delete/5

        public ActionResult Delete(int id)
        {

            return View(dataManager.GetPeaple(id));
        }

        //
        // POST: /СпискиЛюдей/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                dataManager.DeletePeople(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Notphones()
        {
            return View(dataManager.NotPhonePeaple());
        }


        private void SetViewBag()
        {
            ViewBag._Название_SMS = dataManager.SelectListSms();
            ViewBag._Школы = dataManager.SelectListСписокВсехШкол();
            ViewBag._Пол = dataManager.SelectListStringSex();
            ViewBag._Вконтекте = dataManager.SelectListBool();
            ViewBag._Участие_в_выступлениях = dataManager.SelectListBool();

            ViewBag._Район = dataManager.SelectListСписокРайонов();
        }


        private void SetViewBag(Списки_людей спискиЛюдей)
        {
            ViewBag._Название_SMS = dataManager.SelectListSms(спискиЛюдей.Название_SMS.Код);
            ViewBag._Школы = dataManager.SelectListСписокВсехШкол(спискиЛюдей.Название_школ.Код);
            ViewBag._Пол = dataManager.SelectListStringSex(спискиЛюдей.Пол);
            ViewBag._Вконтекте = dataManager.SelectListBool(спискиЛюдей.Вконтекте);
            ViewBag._Участие_в_выступлениях = dataManager.SelectListBool(спискиЛюдей.Участие_в_выступлениях);

            ViewBag._Район = dataManager.SelectListСписокРайонов(спискиЛюдей.Район);
            ViewBag.Alfavit = dataManager.GetAlfavitFromФИО();

        }




        [HttpPost]
        public ActionResult PhoneExist(string Телефон, int? Код)
        {

            //            bool l = Код == null ? dataManager.IsPhoneExists(Телефон) : dataManager.IsPhoneExists(Телефон, Код);

            //if (Код == null)
            //{
            //            return Json(!dataManager.IsPhoneExists(Телефон), JsonRequestBehavior.AllowGet);
            //}
            //else
            //{

            List<string> str = dataManager.IsPhoneExists(Телефон, Код);

            if (str.Count == 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string s1 = "";

                foreach (string s in str)
                {
                    s1 = s + ", " + s1;
                }
                return Json(String.Format("Такой номер телефона уже есть у: {0}. ", s1), JsonRequestBehavior.AllowGet);
            }

            //            }

            //            return Json(str, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public ActionResult EmailExist(string e_mail, int? Код)
        {
            bool l = Код == null ? dataManager.IsEmailExist(e_mail) : dataManager.IsEmailExist(e_mail, Код);

            return Json(!l, JsonRequestBehavior.AllowGet);

            //            return Json(!dataManager.IsPhoneExists(e_mail, Код), JsonRequestBehavior.AllowGet);
        }

 


        [Authorize(Users = "Admin")]
        public ActionResult NotesListAdmin()
        {
            return View("_PartialNotesIndex", dataManager.GetNotes(-13));
        }


        public ActionResult NotesListIndex()
        {
            return PartialView("_PartialNotesIndex", dataManager.GetNotes());
        }


        public ActionResult NotesList(int? id)
        {
            return PartialView("_PartialNotesList", dataManager.GetNotesFromIdPeople(id));
        }


        //        [HttpPost]

        public ActionResult NotesDelete(Notes notes, int idpeople)
        {

            dataManager.DeleteNotes(notes.Id);

            return RedirectToAction("Details", new { Id = idpeople });
        }



        public ActionResult NotesCreate(int? id)
        {


            ViewBag._IdCategoryNotes = dataManager.SelectListCategoryNotes();
            ViewBag.КодУченика = id;

            return PartialView("_PartialNotesCreate");
        }


        [HttpPost]
        public ActionResult NotesCreate(Notes notes, int? id)
        {


            dataManager.CreateNotes(notes);

            ViewBag.КодУченика = id;

            return RedirectToAction("Details", new { id });
        }




        public FileResult DownloadCSV(int? group, int? pay, string sex = "q")
        {
            int groups = Convert.ToInt32(group);
            int pays = Convert.ToInt32(pay);
            //            int sexs = Convert.ToInt32(sex);


            var sw = new StreamWriter(new MemoryStream(), Encoding.GetEncoding(1251));


            List<ListNamesAndPhones> оплата = dataManager.GetListPhoneForExport(groups, pays, sex);

            foreach (ListNamesAndPhones record in оплата)
            {
                sw.WriteLine(String.Format("{0};{1};", record.Fio, record.Phone));
            }

            sw.Flush();
            sw.BaseStream.Seek(0, SeekOrigin.Begin);

            return File(sw.BaseStream, "text/csv", "PhoneEport_" + dataManager.GetNameGroupFromId(groups) + ".csv");
        }


        public ActionResult MyNextAction()
        {

            return this.Redirect(Request.UrlReferrer.AbsoluteUri);
        }


        public PartialViewResult PartialSearchPeople()
        {

//            ViewBag.Название_танцев = dataManager.GetНазваниеТанцев();
//            ViewBag.Имя_группы = dataManager.GetPeaple(name);

            ViewBag.Model = dataManager.GetSearchPeopleFromFIO(null, null);

            return PartialView("_PartialSearchPeople"); 
        }

        [HttpPost]
        public PartialViewResult PartialSearchPeople1(string fio, string phone)
        {

            //            ViewBag.Название_танцев = dataManager.GetНазваниеТанцев();
            //            ViewBag.Имя_группы = dataManager.GetPeaple(name);

            ViewBag.Model = dataManager.GetSearchPeopleFromFIO(fio, phone);

            return PartialView("_PartialIndex", dataManager.GetSearchPeopleFromFIO(fio, phone));
        }


    }

}
