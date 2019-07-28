using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuatroCaminosMvcApplication.Models
{
    public enum VisitStaticVariable
    {

        PriceAbonement = 1200,
        NumberOfLessons = 8,
        //        ddf = Math.Abs(PriceAbonement / NumberOfLessons),



    }

    public enum VisitFreezing
    {
        v = 1,       // Пришел
        Б = 2,     // Заморожен
        Null = 3,         // Не выбрано
    };


    public class VisitsMetaData
    {
        [UIHint("TextBoxDataTime")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DisplayName("Дата оплаты")]
        public DateTime DataTimePay { get; set; }


        [UIHint("AutoComplete")]
        [DisplayName("Имя")]
        public int PeopleId { get; set; }

//        public int GroupId { get; set; }

        public VisitFreezing Value { get; set; }


//        public bool Freezing { get; set; }


        [Required]
//        [DataType(DataType.MultilineText)]
        [MaxLength(250)]
        [StringLength(250, ErrorMessage = "Значение {0} должно содержать не менее {2} и не более {1} символов.",
                MinimumLength = 2)]
        [DisplayName("Комментарий")]
        public string Comments { get; set; }
        

    }

/*    Расчет визитов
 *      
 *    Общее кол-во занятий -  
 * 
 * 
 * 
 * 
 */


/// <summary>
/// Объявление начальных значений
/// </summary>


    [MetadataType(typeof(VisitsMetaData))]
    public partial class Visits
    {

        //public virtual Оплата Pay { get; set; }
        //public virtual Списки_людей People { get; set; }

        //public DateTime DatePay { get; set; }

        ////public virtual ICollection<YesNo> LoginRec { get; set; }

        ////public int LoginRecId { get; set; }

        //public VisitFreezing VisitFreezing { get; set; }

        //public Visits Visit{ get; set; }

        public virtual ICollection<PartialVisit> PartialVisitModels { get; set; }


    }


    public class PartialVisit
    {

        public List_IdAndName ListIdAndName { get; set; }
        public string Comment { get; set; }
        public VisitFreezing VisitFreezing { get; set; }
        public Visits Visits { get; set; }
    }





    public class VisitsViewModel
    {

        VisitsDataManager visitsDataManager = new VisitsDataManager();

        List<DateTime> datePay;

//        private IEnumerable<VisitsModel> visitsModels;

        private IEnumerable<Visits> visitsModel;

        public VisitsViewModel()
        {
            visitsModel = visitsDataManager.GetPeople();

//            datePay = visitsModel.Select(e => e.DatePay).ToList();

        }



        public List<DateTime> DatePay
        {
            get { return datePay; }
        }



    }



/// <summary>
/// Класс для работы с БД модели Visit
/// </summary>

    class VisitsDataManager
    {
        DataManager dataManager = new DataManager();
        Cuatro_Caminos_BDEntities _caminosBdEntities = new Cuatro_Caminos_BDEntities();
        Visits visitsModel = new Visits();


        public IEnumerable<Visits> GetPeople()
        {

            return _caminosBdEntities.Оплата.Select(e =>

                        new Visits()
                        {
//                            DatePay = e.Дата_оплаты
                        }
                    )
                    .ToList();
        }





        public IEnumerable<PartialVisit> PartialVisitModels1(int? group=0)
        {

            return _caminosBdEntities.Оплата
                .Where(e => e.Названия_танцев.Название_танца == "сон")
                .Select(e =>
                    new PartialVisit()
                    {
                        //                                Pay = new Оплата(){Код = e.Код_Ученика}, 
                        ListIdAndName = new List_IdAndName { Id = e.Код_Ученика, Names = (e.Списки_людей.Имя + " " + e.Списки_людей.Фамилия) },
                        //                                VisitFreezing = VisitFreezing.Visits

                    }

                        )
               .Distinct()
                ;


        }

//        GetExportСписокОплаты

        public IEnumerable<List_IdAndName> PartialVisitModels(int group = 0)
        {

            return dataManager.GetExportСписокОплаты(group);

            //return _caminosBdEntities.Оплата
            //     .Where(e => e.Названия_танцев.Название_танца == "сон")
            //     .Select(e =>
            //              new List_IdAndName { Id = e.Код_Ученика, Names = (e.Списки_людей.Имя + " " + e.Списки_людей.Фамилия) }
            //             )
            //    .Distinct()
            //     ;


        } 


        public void SaveVisit(int group, FormCollection collection)
        {

            int id = 0;
            Visits visits;

            Cuatro_Caminos_BDEntities cuatroCaminosBdEntities = new Cuatro_Caminos_BDEntities();



            int count = collection.GetValues("People").Count();



            for (int i = 0; i < count; i++)
            {


                id = Int32.Parse(collection.GetValues("People").GetValue(i).ToString());


                if (Int32.Parse(collection["Radio" + id]) != 3)
                {

                    visits = new Visits();
                    visits.DataTimeRec = DateTime.Now;
                    visits.DataTimePay = collection["DataTimePay"] != null ? DateTime.Parse(collection["DataTimePay"]) : DateTime.Now;
//                    visits.DataTimePay = DateTime.Parse(collection["DataTimePay"]);
                    visits.GroupId = group;
                    visits.LoginRecId = 151;
                    visits.PeopleId = id;
//                    visits.Freezing = false;

                    visits.Value = Int32.Parse(collection["Radio" + id]);
                    visits.Comments = collection["Coomment" + id];

                    //                visits.Value = 1;
                    //                visits.Comments = "2";


                    cuatroCaminosBdEntities.Visits.AddObject(visits);

//                    ModelState.AddModelError("", " radio: " + collection["Radio" + id] + "  Комментарий: " + collection["Coomment" + id]);
                }


                //                ModelState.AddModelError("", " Get: " + collection.Get(i) + "  GetKey: " + collection.GetKey(i) + "  radio: " + idPeoole);
            }


            cuatroCaminosBdEntities.SaveChanges();

            
        }

        public IEnumerable<SummVisitAndPay> GetSumm(int? months = null, int? gruppa = 0)
        {

            Cuatro_Caminos_BDEntities _cuatroCaminosBdEntities = new Cuatro_Caminos_BDEntities();

            DateTime dateTime2012 = DateTime.Parse("01.01.2011", CultureInfo.CreateSpecificCulture("ru-RU"));
//            dateTime2012 = DateTime.Now.AddYears(0);

            IEnumerable<SummVisitAndPay> summListTemp = _cuatroCaminosBdEntities.Оплата
//                    .Where(e=>e.Месяц == id && e.Название_танца==1)
                    .Where(e => e.Дата_оплаты > dateTime2012 && e.Сумма > 0)
//                    .Where(e => e.Название_танца == gruppa)
                    .GroupBy(e=>e.Код_Ученика)
                    .Select(
                        e =>
                        new SummVisitAndPay()
                        {
                            Name = new List_IdAndName{Id = e.Key },
                            SummPay = e.Sum(a=>a.Сумма),

                        }
                    )
                    .ToList();

            IEnumerable<SummVisitAndPay> summList = summListTemp
                    .Join(_cuatroCaminosBdEntities.Списки_людей,
                          summList1 => summList1.Name.Id,
                          people => people.Код,
                          (summList1, people) => new SummVisitAndPay()
                                                  {
                                                      Name = new List_IdAndName
                                                      {
                                                            Id = summList1.Name.Id,
                                                            Names = people.Фамилия + " " + people.Имя
                                                      },
                                                          SummPay = summList1.SummPay
                                                  }
                        )
                    .ToList();


            return summList;
        }

//        public int? Get


/// <summary>
/// Определение последних цен
/// </summary>
/// <param name="group">Id группы</param>
/// <returns>цена абонемента</returns>


        public int GetLastPrice(int group)
        {

            PriceClass priceClass = new PriceClass(group);

            return priceClass.PriceAbonement8;

        }



        public string GetLastPrice11(int group)
        {

            PriceClass priceClass = new PriceClass(group);

            //            return priceClass.PriceAbonement8;
            DateTime dateTime = new DateTime();
            return dateTime.Date.DayOfWeek.ToString();

        }


    
    }




}