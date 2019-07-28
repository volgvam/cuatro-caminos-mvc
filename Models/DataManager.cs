using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Objects;
using System.Data.Objects.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace CuatroCaminosMvcApplication.Models
{


    public static class FromFormData
    {
        public static int month { get; set; }
        public static int Код_Ученика { get; set; }
        public static int Название_танца { get; set; }
        public static int Преподаватель { get; set; }
        public static DateTime Дата_оплаты { get; set; }



    }


    public class DataManager
    {
        private Cuatro_Caminos_BDEntities _cuatroCaminosBdEntities;


        public DataManager()
        {
            _cuatroCaminosBdEntities = new Cuatro_Caminos_BDEntities();

        }

        public ICollection<Название_школ> ListНазваниеШкол()
        {
            return _cuatroCaminosBdEntities
                .Название_школ
                .ToList();
        }


        public Название_школ GetНазваниеШкол(int id)
        {
            return (Название_школ) _cuatroCaminosBdEntities
                                       .Название_школ
                                       .SingleOrDefault(l => l.Код == id);
        }

        public ICollection<Оплата> GetОплатаData()
        {

            return _cuatroCaminosBdEntities.Оплата

//                .Where(e => (FromFormData.month != 0 ? e.Месяц == FromFormData.month : true))
                //                .Where(e => (FromFormData.fio != 0 ? e.Код_Ученика == FromFormData.fio : true))
                //                .Where(e => (FromFormData.gruppa != 0 ? e.Название_танца == FromFormData.gruppa : true))


                //                        .Where(e => e.Дата_оплаты >= DateTime.Today)
                .OrderBy(e => e.Дата_оплаты)
                .ToList();
        }





        public List<ListNamesAndPhones> GetListPhoneForExport(int? idGroup = 0, int? pay = 1, string sex = "q", int? scholl=0)
        {

//            int scholl = ;


                        IEnumerable<int> groups = СписокГруппВЗависимостиОтЛогина();


//            int _group = group ?? 0;
            DateTime day = DateTime.Now.AddDays(-75);
            DateTime date2011 = DateTime.Parse("01.11.2010");
            DateTime birthDay = DateTime.Now.AddYears(-30);


            IList<int> peipleFromОплата1 =
                _cuatroCaminosBdEntities.Оплата
                        .Where(e => e.Дата_оплаты > day  &&
                                    groups.Contains(e.Название_танца))
                         .Select(e => e.Код_Ученика)
                         .Distinct()
                         .ToList();

            IList<int> peipleFromОплата2 =
                _cuatroCaminosBdEntities.Оплата
                        .Where(e => 
                                    !peipleFromОплата1.Contains(e.Код_Ученика) &&
//                                    e.Дата_оплаты <= day &&
                                    e.Дата_оплаты>=date2011 &&
                                    groups.Contains(e.Название_танца))
                         .Select(e => e.Код_Ученика)
                         .Distinct()
                         .ToList();



//          Не по оплате и без групп
            if (pay != 1 && idGroup == 0)
            {
                return _cuatroCaminosBdEntities.Списки_людей
                    .Where(e => 
                            peipleFromОплата2.Contains(e.Код) &&
                        (sex != "q" ? e.Пол == sex : true) &&
                        e.Код != 151 
                        && (e.Школы == 7) 
                        && e.Телефон!=null
                        && e.Выбыл_из_школы == false 
                        && (e.Дата_рождения == null || e.Дата_рождения <= birthDay)
                        && e.Название_SMS.Код != 7)

                    .Select(
                        e =>
                        new ListNamesAndPhones {Fio = e.Фамилия + " " + e.Имя, Phone = "+7" + e.Телефон})
                    .Distinct()
                    .OrderBy(e => e.Fio)
                    .ToList()
                    ;

            }

//          Не по оплате и с группами
            if (pay != 1 && idGroup != 0)
            {
//                DateTime dateTime1 = DateTime.Now.AddMonths(-3);

                return _cuatroCaminosBdEntities.Оплата
                    .Where(e => 
//                        !peipleFromОплата1.Contains(e.Код) &&
                        peipleFromОплата2.Contains(e.Код) &&
//                        e.Дата_оплаты >= dateTime1 &&
                        (idGroup != 0 ? e.Название_танца == idGroup : true) &&
                        (sex != "q" ? e.Списки_людей.Пол == sex : true) &&
                        (scholl != 0 ? e.Списки_людей.Школы == scholl : true) &&
                        e.Списки_людей.Код != 151 &&
                        e.Списки_людей.Выбыл_из_школы == false &&
                        e.Списки_людей.Название_SMS.Код != 7)

                    .Select(
                        e =>
                        new ListNamesAndPhones
                            {
                                Fio = e.Списки_людей.Фамилия + " " + e.Списки_людей.Имя,
                                Phone = "+7" + e.Списки_людей.Телефон
                            })
                    .Distinct()
                    .OrderBy(e => e.Fio)
                    .ToList()
                    ;
            }


//          По оплате             
            if (pay == 1)
            {
                DateTime dateTime1 = DateTime.Now.AddMonths(-3);

                return _cuatroCaminosBdEntities.Оплата
                    .Where(
                        e =>
                        e.Дата_оплаты >= dateTime1 &&
                        (idGroup != 0 ? e.Название_танца == idGroup : true) &&
                        (sex != "q" ? e.Списки_людей.Пол == sex : true) &&
                        (scholl != 0 ? e.Списки_людей.Школы == scholl : true) &&
                        e.Списки_людей.Код != 151 &&
                        e.Списки_людей.Выбыл_из_школы == false &&
                        e.Списки_людей.Название_SMS.Код != 7)

                    .Select(
                        e =>
                        new ListNamesAndPhones
                            {
                                Fio = e.Списки_людей.Фамилия + " " + e.Списки_людей.Имя,
                                Phone = "+7" + e.Списки_людей.Телефон
                            })
                    .Distinct()
                    .OrderBy(e => e.Fio)
                    .ToList()
                    ;
            }

            return null;
        }


        public StreamWriter GetListPhoneForGoogleExport()
        {

            //            DateTime dateTime1 = DateTime.Now.AddMonths(-3);

//            DateTimeStyles

//            string formatDateTime = 

            IEnumerable<ListForGoogleExport> listForGoogleExports = _cuatroCaminosBdEntities.Списки_людей
                .Where(e => e.Название_школ.Код == 7 && e.Код != 151 && e.Телефон != null)
                .Select(
                    e => new ListForGoogleExport
                    {

                        Name = e.Фамилия + " " + e.Имя,
                        Given_Name = e.Имя,
                        Additional_Name = e.Отчество,
                        Family_Name = e.Фамилия,
                        Phone_1__Type = "Mobile",
                        Phone_1__Value = "+7" + e.Телефон,
                        Group_Membership = e.Название_танца,
                        Birthday = e.Дата_рождения,
                        E__mail_1__Value= e.e_mail,
                        Hobby = "\"" + e.Хобби + "\"",
                        Address_1__City = "Волгоград",
                        Address_1__Formatted = e.Название_района.Название_района1
//                        Notes = e.Наличие_авто ? "авто: есть" + ". Примечание: \"" + e.Примечание + "\"" : "авто: нет" + ". Примечание: \"" + e.Примечание + "\""

                    })
                .Distinct()
                .OrderBy(e => e.Name)
                .ToList()
                ;


            //            IDictionary w = listForGoogleExports.ToDictionary();



            var sw = new StreamWriter(new MemoryStream(), Encoding.GetEncoding(1251));
            sw.WriteLine(String.Format("Name," +
                                       "Given Name," +
                                       "Additional Name," +
                                       "Family Name," +
                                       "Yomi Name," +
                                       "Given Name Yomi," +
                                       "Additional Name Yomi," +
                                       "Family Name Yomi," +
                                       "Name Prefix," +
                                       "Name Suffix," +
                                       "Initials," +
                                       "Nickname," +
                                       "Short Name," +
                                       "Maiden Name," +
                                       "Birthday," +
                                       "Gender," +
                                       "Location," +
                                       "Billing Information," +
                                       "Directory Server," +
                                       "Mileage," +
                                       "Occupation," +
                                       "Hobby," +
                                       "Sensitivity," +
                                       "Priority," +
                                       "Subject," +
                                       "Notes," +
                                       "Group Membership," +
                                       "E-mail 1 - Type," +
                                       "E-mail 1 - Value," +
                                       "E-mail 2 - Type," +
                                       "E-mail 2 - Value," +
                                       "IM 1 - Type," +
                                       "IM 1 - Service," +
                                       "IM 1 - Value," +
                                       "Phone 1 - Type," +
                                       "Phone 1 - Value," +
                                       "Phone 2 - Type," +
                                       "Phone 2 - Value," +
                                       "Phone 3 - Type," +
                                       "Phone 3 - Value," +
                                       "Address 1 - Type," +
                                       "Address 1 - Formatted," +
                                       "Address 1 - Street," +
                                       "Address 1 - City," +
                                       "Address 1 - PO Box," +
                                       "Address 1 - Region," +
                                       "Address 1 - Postal Code," +
                                       "Address 1 - Country," +
                                       "Address 1 - Extended Address," +
                                       "Organization 1 - Type," +
                                       "Organization 1 - Name," +
                                       "Organization 1 - Yomi Name," +
                                       "Organization 1 - Title," +
                                       "Organization 1 - Department," +
                                       "Organization 1 - Symbol," +
                                       "Organization 1 - Location," +
                                       "Organization 1 - Job Description," +
                                       "Website 1 - Type," +
                                       "Website 1 - Value," +
                                       "Custom Field 1 - Type," +
                                       "Custom Field 1 - Value," +
                                       "Custom Field 2 - Type," +
                                       "Custom Field 2 - Value"));


            string birthday = "";

            foreach (var record in listForGoogleExports)
            {


                if (record.Birthday != null)
                {
                    birthday = record.Birthday.Value.Date.ToString("yyyy.MM.dd");
                }

                sw.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},"+
                                           "{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},"+
                                           "{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},"+
                                           "{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},"+
                                           "{40},{41},{42},{43},{44},{45},{46},{47},{48},{49},"+
                                           "{50},{51},{52},{53},{54},{55},{56},{57},{58},{59},"+
                                           "{60},{61},{62}",
                    record.Name,
                    record.Given_Name,
                    record.Additional_Name,
                    record.Family_Name,
                    record.Yomi_Name,
                    record.Given_Name_Yomi,
                    record.Additional_Name_Yomi,
                    record.Family_Name_Yomi,
                    record.Name_Prefix,
                    record.Name_Suffix,
                    record.Initials,
                    record.Nickname,
                    record.Short_Name,
                    record.Maiden_Name,
                    birthday,
                    record.Gender,
                    record.Location,
                    record.Billing_Information,
                    record.Directory_Server,
                    record.Mileage,
                    record.Occupation,
                    record.Hobby,
                    record.Sensitivity,
                    record.Priority,
                    record.Subject,
                    record.Notes,
                    record.Group_Membership,
                    record.E__mail_1__Type,
                    record.E__mail_1__Value,
                    record.E__mail_2__Type,
                    record.E__mail_2__Value,
                    record.IM_1__Type,
                    record.IM_1__Service,
                    record.IM_1__Value,
                    record.Phone_1__Type,
                    record.Phone_1__Value,
                    record.Phone_2__Type,
                    record.Phone_2__Value,
                    record.Phone_3__Type,
                    record.Phone_3__Value,
                    record.Address_1__Type,
                    record.Address_1__Formatted,
                    record.Address_1__Street,
                    record.Address_1__City,
                    record.Address_1__PO_Box,
                    record.Address_1__Region,
                    record.Address_1__Postal_Code,
                    record.Address_1__Country,
                    record.Address_1__Extended_Address,
                    record.Organization_1__Type,
                    record.Organization_1__Name,
                    record.Organization_1__Yomi_Name,
                    record.Organization_1__Title,
                    record.Organization_1__Department,
                    record.Organization_1__Symbol,
                    record.Organization_1__Location,
                    record.Organization_1__Job_Description,
                    record.Website_1__Type,
                    record.Website_1__Value,
                    record.Custom_Field_1__Type,
                    record.Custom_Field_1__Value,
                    record.Custom_Field_2__Type,
                    record.Custom_Field_2__Value
        ));

            }

            return sw;
        }



        public ICollection<PayGroup> GetPayGroup()
        {

            if (HttpContext.Current.User.Identity.Name == StaticsVariables.ЛогинАдминистратора)
            {

                List<PayGroup> payGroup = _cuatroCaminosBdEntities.Оплата
//                    .AsQueryable()
                    .Select(e => new PayGroup() { 
///                        Month = e.Дата_оплаты.Month, 
///                        Year = e.Дата_оплаты.Year , 
                        Date = e.Дата_оплаты,
                        Group = e.Названия_танцев.Название_танца, 
                        Amount = e.Сумма })
//                    .GroupBy(e=>e.Month.Month)
                    .ToList();


                var query = payGroup.AsQueryable().GroupBy(

                    pet => pet.Month,
                    pet => pet.Amount,

                    (month, ages) => new
                    {
                        Month = month,
                        Count = ages.Sum()
                    });



                List<PayGroup> list = query.Select(e => new PayGroup()
                                        {
                                           Amount = e.Count,
                                            Group = e.Count.ToString(),
                                            Month = e.Month
                                        })
                                        .ToList();

                return list;

            }

            return null;
        }

        public ICollection<Оплата> GetОплатаListPartial(DateTime date, int? group = 0 )
        {
            DateTime dateTime1 = DateTime.Now.AddMonths(-1);

            return _cuatroCaminosBdEntities.Оплата
//                    .Where(e => (FromFormData.month != 0 ? e.Месяц == FromFormData.month : true))
//                    .Where(e => (FromFormData.Код_Ученика != 0 ? e.Код_Ученика == FromFormData.Код_Ученика : true))
                    .Where(e => e.Название_танца == group)

//                    .Where(e => (FromFormData.Преподаватель != 0 ? e.Преподаватель == FromFormData.Преподаватель : true))

                    .Where(e => e.Дата_оплаты == date)
                    .OrderByDescending(e => e.DateTimeRec)
                    .ToList();
        }




        public ICollection<Оплата> GetОплата(int? days=0)
        {



            if (FromFormData.month == 0 && FromFormData.Код_Ученика == 0 && FromFormData.Название_танца == 0 && FromFormData.Преподаватель == 0)
            {
                FromFormData.month = 13;
            }

            DateTime dateTime1 = DateTime.Now.AddMonths(-3);

            if (HttpContext.Current.User.Identity.Name == StaticsVariables.ЛогинАдминистратора)
            {

                dateTime1 = DateTime.Now.AddMonths(-10);


                return _cuatroCaminosBdEntities.Оплата
                    .Where(e => (FromFormData.month != 0 ? e.Месяц == FromFormData.month : true))
                    .Where(e => (FromFormData.Код_Ученика != 0 ? e.Код_Ученика == FromFormData.Код_Ученика : true))
                    .Where(e => (FromFormData.Название_танца != 0 ? e.Название_танца == FromFormData.Название_танца : true))
                    .Where(e => (FromFormData.Преподаватель != 0 ? e.Преподаватель == FromFormData.Преподаватель : true))


                    .Where(e => e.Дата_оплаты >= dateTime1)
                    .OrderBy(e => e.Дата_оплаты)
                    .ToList();


            }
            else
            {

                IEnumerable<int> gr = СписокГруппВЗависимостиОтЛогина();

                return _cuatroCaminosBdEntities.Оплата
                    .Where(e => (FromFormData.month != 0 ? e.Месяц == FromFormData.month : true))
                    .Where(e => (FromFormData.Код_Ученика != 0 ? e.Код_Ученика == FromFormData.Код_Ученика : true))
                    .Where(e => (FromFormData.Название_танца != 0 ? e.Название_танца == FromFormData.Название_танца : true))
//                    .Where(e => (FromFormData.Название_танца == 23 ? e.Преподаватель == FromFormData.Преподаватель  : true))
                    .Where(e => e.Дата_оплаты >= dateTime1 && gr.Contains(e.Название_танца))


                    .OrderBy(e => e.Дата_оплаты)

                    .ToList();

            }
        }

        public IEnumerable<Оплата> GetОплатаFromФИО(int id)
        {

//            IEnumerable<int> gr = СписокГруппВЗависимостиОтЛогина();

            return _cuatroCaminosBdEntities.Оплата
                .Where(e => e.Код_Ученика == id)
//                .Where(e => gr.Contains(e.Название_танца))
                .OrderBy(e => e.Дата_оплаты)

                .ToList();



        }


        public IEnumerable<CashAndPay> GetCashAndPay()
        {

            DateTime dateTime = DateTime.Now.AddDays(-80);

            IEnumerable<CashAndPay> pays = _cuatroCaminosBdEntities.Оплата
                .Where(e => e.Сумма > 1 && e.Дата_оплаты > dateTime)
//                .Where(e=>e.Название_танца==14)
                .Select(e => 
                            new CashAndPay()
                                {
                                    Id = e.Код, 
                                    Date = e.Дата_оплаты, 
                                    Оплата = e.Сумма,
                                    Group = new List_IdAndName() { Id = e.Названия_танцев.Код, Names = e.Названия_танцев.Название_танца }
                                })
                .ToList();


            IEnumerable<CashAndPay> cash = _cuatroCaminosBdEntities.Cash
                .Where(e => e.Money > 1 && e.DataTimePay > dateTime)
//                .Where(e => e.GroupId == 14)
                .Select(e => new CashAndPay() { Id = e.Id, Date = e.DataTimePay, Group = new List_IdAndName() { Id = e.Названия_танцев.Код, Names = e.Названия_танцев.Название_танца }, Cash = e.Money })
                .ToList();

            IEnumerable<CashAndPay> cashAndPays = pays.Concat(cash).Distinct();


            return cashAndPays;
        }





        public IEnumerable<PayMonthEntity> GetMonthОплата(int? months = null, int? gruppa = 0)
        {
            //            DateTime test = DateTime.Parse("30.03.2010", CultureInfo.CurrentCulture);
            DateTime dateTime1 = DateTime.Now.AddMonths(-3);

            int month = months ?? DateTime.Now.Month;

            if (HttpContext.Current.User.Identity.Name == StaticsVariables.ЛогинАдминистратора)
            {

                dateTime1 = DateTime.Now.AddMonths(-11);

                IEnumerable<PayMonthEntity> myEntities = _cuatroCaminosBdEntities.Оплата
                    //                .Where(e=>e.Месяц == id && e.Название_танца==1)
                    .Where(e => e.Дата_оплаты > dateTime1 && e.Месяц == month && e.Сумма > 0)
                    .Where(e => e.Название_танца == gruppa)
                    .Select(
                        e =>
                        new PayMonthEntity()
                            {
                                Id = e.Код,
                                Date = e.Дата_оплаты,
                                Name = new List_IdAndName() {Id = e.Списки_людей.Код, Names = e.Списки_людей.Фамилия + " " + e.Списки_людей.Имя},
                                Pay = e.Сумма,
                                Group = e.Название_танца
                            }
                    )

                    .OrderBy(e => e.Date)
                    .OrderBy(e => e.Name)
//                    .Distinct(new MyComparerPayMonthEntity())
                    .ToList();




                IEnumerable<PayMonthEntity> visits = _cuatroCaminosBdEntities.Visits
                    .Where(e => e.DataTimePay.Month == months)
                    .Where(e => e.GroupId == gruppa)
                    .Select(
                        e =>
                        new PayMonthEntity()
                            {
                                Date = e.DataTimePay,
                                Name = new List_IdAndName() {Id = e.PeopleId, Names = e.Списки_людей.Фамилия + " " + e.Списки_людей.Имя },
                                VisitFreezing = e.Value
                            }
                    )
                    .OrderBy(e => e.Date)
                    .OrderBy(e => e.Name)
//                    .Distinct(new MyComparerPayMonthEntity())
                    .ToList();


                //IEnumerable<PayMonthEntity> SummPay1 = _cuatroCaminosBdEntities.Оплата
                //    .Select( e=>
                //        new PayMonthEntity()
                //            {
                //                ListSummVisitAndPay = new SummVisitAndPay {SummPay = e.Сумма}
                //            }
                //    )
                //    .ToList();



                //IEnumerable<PayMonthEntity> SummPay = _cuatroCaminosBdEntities.Оплата
                //    .Select(e =>
                        
                //        new PayMonthEntity
                //            {
                //                ListSummVisitAndPay = new SummVisitAndPay
                //                                          {
                //                                             SummPay  =  e.Сумма, PeopleId = e.Код_Ученика 
                //                                          }
                //            }
                        
                //        )
                //    .GroupBy(c=>c.ListSummVisitAndPay.PeopleId)
                //    .Select(g => new PayMonthEntity
                //                     {
                //                         ListSummVisitAndPay = new SummVisitAndPay{ SummPay =g.Key, PeopleId  = g.Sum(r=>r.Value)}
                //                     })
                //    ;


//                IEnumerable<PayMonthEntity> monthEntities = visits.Concat(myEntities).ToArray();

                //var kjkj = from payMonthEntity in SummPay
                //           group payMonthEntity.ListSummVisitAndPay.PeopleId by
                //               payMonthEntity.ListSummVisitAndPay.SummPay
                //           into summPay
                //           select new
                //                      {
                //                          People = summPay.Key,
                //                          Summa = summPay.Sum()
                //                      };



                return visits.Concat(myEntities).OrderBy(e=>e.Name.Names);


            }
            else
            {


                IEnumerable<int> gr = СписокГруппВЗависимостиОтЛогина();


                IEnumerable<PayMonthEntity> myEntities = _cuatroCaminosBdEntities.Оплата
                    //                .Where(e=>e.Месяц == id && e.Название_танца==1)
                    .Where(e => e.Дата_оплаты > dateTime1 && e.Месяц == month)
                    .Where(e => e.Название_танца == gruppa &&  gr.Contains(e.Название_танца) && e.Сумма > 0)
                    
                    .Select(
                        e =>
                        new PayMonthEntity()
                            {
                                Id = e.Код,
                                Date = e.Дата_оплаты,
                                Name = new List_IdAndName() { Id = e.Списки_людей.Код, Names = e.Списки_людей.Фамилия + " " + e.Списки_людей.Имя },
                                Pay = e.Сумма
                            }
                    )
//                    .Distinct(new MyComparerPayMonthEntity())
                    .OrderBy(e => e.Date)
                    .OrderBy(e => e.Name)
                    .ToList();


                return myEntities.OrderBy(e => e.Name.Names);
            }


        }


        public Оплата GetОплата(int id)
        {

            return _cuatroCaminosBdEntities.Оплата
                .SingleOrDefault(e => e.Код == id);
        }


        public ICollection<Название_SMS> GetSms(int? id)
        {
            return _cuatroCaminosBdEntities.Название_SMS
                .Where(e => id!= null? e.Код == id: true)
                .ToList();

        }

        public ICollection<Administrators> GetAdministrator()
        {

            return _cuatroCaminosBdEntities.Administrators
//                .Where(e=> (id != -1 ? e.Id== id : e.Id != -1))
                .ToList();

        }

        public Administrators GetAdministrator(int id)
        {

            return _cuatroCaminosBdEntities.Administrators
                .SingleOrDefault(e => e.Id == id);

        }


        public ICollection<Visits> GetVisits(int? group = 0)
        {
            return _cuatroCaminosBdEntities.Visits.ToList();
        }

        public void CreateVisits(Visits visits)
        {
            _cuatroCaminosBdEntities.Visits.AddObject(visits);
            SaveChanges();
        }




        public ICollection<Списки_людей> GetСписокФиоFromGroup(int? group=0)
        {

            IEnumerable<int> gr = СписокГруппВЗависимостиОтЛогина();

            IList<int> id = _cuatroCaminosBdEntities.Оплата.Where(e => 
                gr.Contains(e.Названия_танцев.Код) && e.Названия_танцев.Код == group)
                  .Select(e => e.Списки_людей.Код)
                  .ToList();

            return _cuatroCaminosBdEntities.Списки_людей
                .Where(e => 
                    id.Contains(e.Код) &&
                       (group == 0 ? e.Название_школ.Код == 7 : true) && 
//                    e.Название_школ.Код==7 && 
                    e.Код!=151)
                .OrderBy(e => e.Фамилия + " " + e.Имя)

                .ToList();
        }




        public void DeleteAdministrator(int id)
        {
            Administrators administrators = GetAdministrator(id);
            _cuatroCaminosBdEntities.Administrators.DeleteObject(administrators);
            SaveChanges();
        }





        public List<Списки_людей> ListСписокВсехФио()
        {

            //var l = _cuatroCaminosBdEntities.Списки_людей
            //   .Select(e => new
            //   {
            //       Id = e.Код,
            //       Names = e.Фамилия + " " + e.Имя + (e.Отчество != null ? " " + e.Отчество : ""),
            //       e.Название_школ,
            //       e.Выбыл_из_школы,
            //       e.Код
            //   })
            //    .Where(e => e.Names != null)
            //    .Where(e => e.Название_школ.Школы == "Cuatro Caminos")
            //    .Where(e => e.Выбыл_из_школы == false)
            //   .OrderBy(e => e.Names);



            return _cuatroCaminosBdEntities.Списки_людей
                .ToList();
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Выводит все ФИО наших людей 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public SelectList SelectListСписокВсехФио(int id, int loginId = 0)
        {



            var l = new SelectList(_cuatroCaminosBdEntities.Списки_людей
                                       .Select(e => new
                                                        {
                                                            Id = e.Код,
                                                            Names =
                                                        e.Фамилия + " " + e.Имя +
                                                        (e.Отчество != null ? " " + e.Отчество : ""),
                                                            e.Название_школ,
                                                            e.Выбыл_из_школы,
                                                            e.Код
                                                        })
                                       .Where(e => e.Names != null)
                                       .Where(e => e.Название_школ.Школы == "Cuatro Caminos")
                                       .Where(e => e.Выбыл_из_школы == false)

                                       .OrderBy(e => e.Names)

                                   , "Id", "Names", id);



            return l;
        }


 
        public SelectList SelectListСписокФио(int? id)
        {
            return new SelectList(GetСписокФио(), "Id", "Names", id);
        }


        public SelectList SelectListСписокФиоОтОплаты(int? id, int?[] grups)
        {
            return new SelectList(GetСписокФиоОтОплаты(), "Id", "Names", id);
        }


 


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        public SelectList SelectListCategoryNotes(int? id = null)
        {
            return new SelectList(_cuatroCaminosBdEntities.NotesCategory
                                      .Select(e => new { Id = e.Id, Names = e.Category })
                                      .OrderBy(e => e.Names)

                                  , "Id", "Names", id);
        }




        public SelectList SelectListSms(int? id = null)
        {
            return new SelectList(_cuatroCaminosBdEntities.Название_SMS
                                      .Select(e => new { Id = e.Код, Names = e.SMS })
                                      .OrderBy(e => e.Names)

                                  , "Id", "Names", id);
        }


        public SelectList SelectListСписокВсехШкол(int? id = null)
        {

            return new SelectList(_cuatroCaminosBdEntities.Название_школ
                //                .Where(e=>e.Visible== true)
                                      .Select(e => new { Id = e.Код, Names = e.Школы })
                                      .OrderBy(e => e.Names)

                                  , "Id", "Names", id);

        }



        public SelectList SelectListСписокНаправленийТанцев(int id, int? loginId = 0)
        {


            IEnumerable<int> gr = СписокГруппВЗависимостиОтЛогина();


            var l = new SelectList(_cuatroCaminosBdEntities.Названия_танцев
                //                .Where(e => (loginId != 0 ? e.Код_Ученика == loginId : true))
                                       .Where(e => gr.Contains(e.Код))
                                       .Select(e => new { Id = e.Код, Names = 
                                           
                                           e.Description==null? 
                                                e.Название_танца : e.Название_танца + " ("+ e.Description + ")"
                                           
                                           })
                                       .OrderBy(e => e.Names)

                                   , "Id", "Names", id);


            return l;

        }

        public SelectList SelectListВсехСписокНаправленийТанцев(int id, int? loginId = 0)
        {
            var l = new SelectList(_cuatroCaminosBdEntities.Названия_танцев
                //                .Where(e => (loginId != 0 ? e.AdministratorID. == loginId : true))
                                       .Select(e => new { Id = e.Код, Names = e.Название_танца })
                                       .OrderBy(e => e.Names)

                                   , "Id", "Names", id);


            //var l = new SelectList(_cuatroCaminosBdEntities.Названия_танцев
            //    .Select(e => new { Id = e.Код, Names = e.Название_танца })
            //    .Where(e => (Код!=0 ? e.Id == Код : true))
            //    .OrderBy(e => e.Names)

            //    , "Id", "Names", id);

            return l;

        }



        public SelectList SelectListИменаПреподавателей(int? id = null)
        {

            IEnumerable<int> pr = СписокПреподавателейВЗависимостиОтЛогина();

            //            СписокПреподавателейВЗависимостиОтЛогина

            var l = new SelectList(_cuatroCaminosBdEntities.Имена_преподавателей
                                       .Where(e => e.Visible != false)
                //                .Where(e=> pr.Contains(e.Код))
                                       .Select(e => new { Id = e.Код, Names = e.ФИО })
                                       .OrderBy(e => e.Names)

                                   , "Id", "Names", id);

            return l;

        }


        public SelectList SelectListСписокМесяцев(int id)
        {


            var l = new SelectList(_cuatroCaminosBdEntities.Название_месяцев
                                       .Select(e => new { Id = e.Код, Names = e.Месяц })
                                       .OrderBy(e => e.Id)
                                   , "Id", "Names", id);

            return l;

        }


        public SelectList SelectListСписокРайонов(int? id = null)
        {


            var l = new SelectList(_cuatroCaminosBdEntities.Название_района
                                       .Select(e => new { Id = e.Код, Names = e.Название_района1 })
                                       .OrderBy(e => e.Id)
                                   , "Id", "Names", id);

            return l;

        }

        public IList<Sex> GetПол(string id = null)
        {

            var sex = new List<Sex>
                          {
                              new Sex {YesNo = "Ж"},
                              new Sex {YesNo = "М"}
                          };

            return sex;
        }

        public SelectList SelectListStringSex(string id = null)
        {

            var sex = new List<Sex>
                          {
                              new Sex {YesNo = "Ж"},
                              new Sex {YesNo = "М"}
                          };


            return new SelectList(sex
                                      .Select(e => new { Id = e.YesNo, Names = e.YesNo })
                                      .OrderBy(e => e.Id)
                                  , "Id", "Names", id);


        }

        public SelectList SelectListBool(bool? id = null)
        {

            var sex = new List<BoolTrueFalse>
                          {
                              new BoolTrueFalse {TrueFalse = true, Name = "Да"},
                              new BoolTrueFalse {TrueFalse = false, Name = "Нет"}
                          };


            return new SelectList(sex
                                      .Select(e => new { Id = e.TrueFalse, Names = e.Name })
                                      .OrderBy(e => e.Id)
                                  , "Id", "Names", id);


        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public ICollection<Списки_людей> GetPeaple(char? alfavit)
        {
            IEnumerable<int> pe = СписокЛюдейВЗависимостиОтЛогина();
            string _alfavit = alfavit.ToString();

            switch (_alfavit)
            {
 
                case "0":
                    _alfavit = "";
                    break;

                case "":
                    _alfavit = "Ь";
                    break;

            }


            return _cuatroCaminosBdEntities.Списки_людей
                
                .Where(e => e.Название_школ.Школы == "Cuatro Caminos" &&
                    (e.Фамилия + " " + e.Имя + (e.Отчество != null ? " " + e.Отчество : "")).StartsWith(_alfavit) &&
                    pe.Contains(e.Код))
                .OrderBy(e => e.Фамилия + " " + e.Имя)
                .ToList();
        }


        public IDictionary<int, string> GetGroupFromPay(int days)
        {
            DateTime day = DateTime.Now.AddDays(days);

            IList dance = _cuatroCaminosBdEntities.Оплата
                .Where(e => e.Дата_оплаты < day)
                .Select(e => new List<Названия_танцев>
                                 {
                                     new Названия_танцев()
                                         {
                                             Код = e.Названия_танцев.Код,
                                             Название_танца = e.Названия_танцев.Название_танца
                                         }
                                 })
                .Distinct()
                .ToList()
                ;

            //Dictionary<int, string> dictionary =
            //   dance.t .ToDictionary(p => p.);


            
//            .ToDictionary();

 
            return null;

        } 

        public IEnumerable<Списки_людей> GetSearchPeopleFromFIO(string fio, string phone)
        {

            IEnumerable<Списки_людей> спискиЛюдей = null;

            if (fio != null)
            {

            Array fio_split = fio.Trim().Split(new Char[]{' '});

            IEnumerable<int> pe = СписокЛюдейВЗависимостиОтЛогина();
            int i;


            //if (fio.Trim().Split(new Char[] { ' ' }).Count() == 3)
            //{

            //    спискиЛюдей = _cuatroCaminosBdEntities.Списки_людей
            //      .Where(e => pe.Contains(e.Код))
            //      .Where(e => new string {}

            //          (e.Фамилия.Contains(fio_str ?? "") 
            //          && e.Имя.Contains(fio_str ?? "")
            //          && e.Отчество.Contains(fio_str ?? "")
            //          ))
            //      .ToList()
            //      ;


            //}



                foreach (string fio_str in fio_split)
                {
                    //спискиЛюдей = _cuatroCaminosBdEntities.Списки_людей
                    //    .Where(e => pe.Contains(e.Код))
                    //    .Where(e => 
                    //        (  e.Фамилия!=null?  e.Фамилия.Contains(fio_str) : false
                    //        || e.Имя !=null? e.Имя.Contains(fio_str)  : false
                    //        || e.Отчество !=null?  e.Отчество.Contains(fio_str) : false
                    //        ))
                    //    .ToList()
                    //    ;


                        

                    спискиЛюдей = _cuatroCaminosBdEntities.Списки_людей
                        .Where(e => pe.Contains(e.Код))
                        .Where(e =>
                            (  e.Фамилия.Contains(fio_str ??"")
                            || e.Имя.Contains(fio_str ?? "")
                            || e.Отчество.Contains(fio_str ?? "")
                            || e.Телефон.Contains((phone != null ? phone : null)) 
                            ))
                        .OrderBy(e=>e.Фамилия + e.Имя)
                        .ToList()
                        ;

                    
                

                }
            }
            else
            {
                спискиЛюдей = _cuatroCaminosBdEntities.Списки_людей
                        .Where(e => e.Код==0)
//                        .Where(e => (e.Фамилия.Contains(fio_str.Trim()) || e.Имя.Contains(fio_str.Trim()) || e.Отчество.Contains(fio_str.Trim())))
                        .ToList()
                        ;
            }


            return спискиЛюдей;

        } 

        public ICollection<Списки_людей> GetPeapleDoNotGo(int? group = null, int? days = null)
        {
//            IEnumerable<int> peoples = СписокЛюдейВЗависимостиОтЛогина();
            IEnumerable<int> groups = СписокГруппВЗависимостиОтЛогина();


            int _group = group ?? 0;
            DateTime day = DateTime.Now.AddDays(days ?? 0);


            IList<int> peipleFromОплата1 =
                _cuatroCaminosBdEntities.Оплата
                        .Where(e => e.Дата_оплаты > day  &&
                                    groups.Contains(e.Название_танца))
                         .Select(e => e.Код_Ученика)
                         .Distinct()
                         .ToList();

            IList<int> peipleFromОплата2 =
                _cuatroCaminosBdEntities.Оплата
                        .Where(e => e.Дата_оплаты <= day &&
                                    groups.Contains(e.Название_танца) &&
                                    e.Название_танца == group)
                         .Select(e => e.Код_Ученика)
                         .Distinct()
                         .ToList();
            

//            return _cuatroCaminosBdEntities.Оплата
////                    .Where(e => (FromFormData.month != 0 ? e.Месяц == FromFormData.month : true))
////                    .Where(e => (FromFormData.Код_Ученика != 0 ? e.Код_Ученика == FromFormData.Код_Ученика : true))
//                    .Where(e => e.Название_танца == group)

////                    .Where(e => (FromFormData.Преподаватель != 0 ? e.Преподаватель == FromFormData.Преподаватель : true))

//                    .Where(e => e.Дата_оплаты == date)
//                    .OrderByDescending(e => e.DateTimeRec)
//                    .ToList();


            return _cuatroCaminosBdEntities.Списки_людей.Where(e => !peipleFromОплата1.Contains(e.Код) && peipleFromОплата2.Contains(e.Код) && e.Код!=151).ToList();
        }

        // 
        public Списки_людей GetPeaple(int id)
        {
            IEnumerable<int> pe = СписокЛюдейВЗависимостиОтЛогина();
            return _cuatroCaminosBdEntities.Списки_людей
                .SingleOrDefault(e => e.Код == id && pe.Contains(e.Код));
        }



        public ICollection<Списки_людей> GetPeapleSchools(int schools)
        {
//            schools = 1;
            //            IEnumerable<int> pe = СписокЛюдейВЗависимостиОтЛогина();
            return _cuatroCaminosBdEntities.Списки_людей
                .Where(e => e.Название_школ.Код == schools)
                .OrderBy(e => e.Фамилия + " " + e.Имя)
                .ToList();
        }




        public void DeleteNotes(int id)
        {
            Notes notes = _cuatroCaminosBdEntities.Notes.SingleOrDefault(e => e.Id == id);
            _cuatroCaminosBdEntities.Notes.DeleteObject(notes);
            SaveChanges();
        }


        public ICollection<Notes> GetNotes(int? all=0)
        {

            DateTime dateTime1 = DateTime.Now.AddDays(-3);
            DateTime dateTime2 = DateTime.Now.AddDays(+3);

            IEnumerable<int> pe = СписокЛюдейВЗависимостиОтЛогина();


            if (all==-13)
            {
                return _cuatroCaminosBdEntities.Notes.Where(e => pe.Contains(e.КодУченика)).ToList();
            }
            else
            {

                return _cuatroCaminosBdEntities.Notes.Where(e => pe.Contains(e.КодУченика) && e.DatePlan >= dateTime1 && e.DatePlan <= dateTime2).ToList();
            }


        }


        public ICollection<Notes> GetNotesFromIdPeople(int? idPeople=0)
        {

            return _cuatroCaminosBdEntities.Notes.Where(e => e.КодУченика == idPeople).ToList();
        }

        public void CreateNotes(Notes notes)
        {

            notes.DataCreate = DateTime.Now;
            notes.LoginRecId = GetFromLoginToId(HttpContext.Current.User.Identity.Name);

            _cuatroCaminosBdEntities.Notes.AddObject(notes);
            SaveChanges();
        } 



        public ICollection<Списки_людей> NotPhonePeaple()
        {
            IEnumerable<int> pe = СписокЛюдейВЗависимостиОтЛогина();

            return _cuatroCaminosBdEntities.Списки_людей
                .Where(e => e.Телефон == null)
                .Where(e => e.Название_школ.Школы == "Cuatro Caminos" && pe.Contains(e.Код))
                .OrderBy(e => e.Фамилия + " " + e.Имя)
                .ToList();
        }

        public ICollection<Название_школ> GetНазваниеШкол()
        {
            return _cuatroCaminosBdEntities.Название_школ
//                .Where(e => e.Школы == "Cuatro Caminos")
                .OrderBy(e => e.Школы)
                .ToList();
        }

        public int GetFromLoginToId(string login)
        {

            return _cuatroCaminosBdEntities.Списки_людей
                .Where(e => e.Login == login)
                .Select(e => e.Код)
                .SingleOrDefault();

        }


        public int GetIdFromОплатаLoginRecId(int id)
        {

            return _cuatroCaminosBdEntities.Оплата
                .Where(e => e.Код == id)
                .Select(e => e.LoginRecId)
                .SingleOrDefault();
        }


        public Списки_людей GetPeapleFioFromId(int id)
        {

            return _cuatroCaminosBdEntities.Списки_людей
                .SingleOrDefault(e => e.Код == id);
        }


        public void DeleteОплата(int id)
        {
            Оплата оплата = GetОплата(id);
            if (оплата!= null)
            {
                _cuatroCaminosBdEntities.Оплата.DeleteObject(оплата);
                SaveChanges();
            }
        }


        public void UpdateОплата(Оплата оплата)
        {
            SaveChanges();
        }


        public bool CreateОплата(Оплата оплата)
        {

            оплата.Месяц = оплата.Дата_оплаты.Month;
            оплата.DateTimeRec = DateTime.Now;
            оплата.LoginRecId = GetFromLoginToId(HttpContext.Current.User.Identity.Name);


            _cuatroCaminosBdEntities.Оплата.AddObject(оплата);
            SaveChanges();

            return true;
        }


        public void CreateНовыйКлиент(Списки_людей спискиЛюдей)
        {


            спискиЛюдей.DateTimeRec = DateTime.Now;
            спискиЛюдей.LoginRecId = GetFromLoginToId(HttpContext.Current.User.Identity.Name);

            _cuatroCaminosBdEntities.Списки_людей.AddObject(спискиЛюдей);


            SaveChanges();

        }


        public void SaveChanges()
        {
            _cuatroCaminosBdEntities.SaveChanges();
        }

        public void CreateAdministrators(Administrators administrators)
        {
            _cuatroCaminosBdEntities.Administrators.AddObject(administrators);
            SaveChanges();
        }


        private IEnumerable<int> СписокГруппВЗависимостиОтЛогина()
        {
            int кодУченика = GetFromLoginToId(HttpContext.Current.User.Identity.Name);


            if (HttpContext.Current.User.Identity.Name == StaticsVariables.ЛогинАдминистратора)
            {
                return _cuatroCaminosBdEntities.Названия_танцев
                    .Select(e => e.Код)
                    .Distinct()
                    .ToArray();
            }
            else
            {
                return _cuatroCaminosBdEntities.Administrators
                    .Where(e => e.Код_Ученика == (кодУченика != 0 ? кодУченика : 0))
                    .Select(e => e.Название_танца)
                    .Distinct()
                    .ToArray();
            }


        }


        public ICollection<List_IdAndName> GetExportСписокОплаты(int gruppa)
        {

            IEnumerable<int> pe = СписокЛюдейВЗависимостиОтЛогина();
            DateTime dateTime = DateTime.Now.AddDays(-75);


            return _cuatroCaminosBdEntities.Оплата
                    .Where(e => pe.Contains(e.Списки_людей.Код) &&
                    e.Названия_танцев.Код == gruppa &&
                    e.Дата_оплаты > dateTime &&
                    e.Списки_людей.Код != 151 &&
                    e.Сумма > 100
                 && e.Списки_людей.Выбыл_из_школы != true
                    
                    )
                    
                    .Select(e => new List_IdAndName()
                    {
                        Id = e.Списки_людей.Код,
                        Names = e.Списки_людей.Фамилия + " " + e.Списки_людей.Имя,
                    })

                    .Where(e => e.Names != null)
                    .Distinct()
                    .OrderBy(e => e.Names)
                    .ToList();

        }

        private ICollection<List_IdAndName> GetСписокФиоAll()
        {

//            IEnumerable<int> pe = СписокЛюдей();

            return _cuatroCaminosBdEntities.Списки_людей

//                    .Where(e => pe.Contains(e.Код))
                    .Select(e => new List_IdAndName()
                    {
                        Id = e.Код,
                        Names = e.Фамилия + " " + e.Имя + (e.Отчество != null ? " " + e.Отчество : ""),
                    })
                    .Where(e=> e.Names!=null)
                    .Distinct()
                    .OrderBy(e => e.Names)
                    .ToList();

        }



        private ICollection<List_IdAndName> GetСписокФио()
        {

            IEnumerable<int> pe = СписокЛюдей();

            return _cuatroCaminosBdEntities.Списки_людей

                    .Where(e => pe.Contains(e.Код))
                    .Select(e => new List_IdAndName()
                    {
                        Id = e.Код,
                        Names = e.Фамилия + " " + e.Имя + (e.Отчество != null ? " " + e.Отчество : ""),
                    })
                    .Distinct()
                    .OrderBy(e => e.Names)
                    .ToList();

        }


        private ICollection<List_IdAndName> GetСписокФиоОтОплаты()
        {

            IEnumerable<int> pe = СписокЛюдейВЗависимостиОтЛогина();
            
            return _cuatroCaminosBdEntities.Списки_людей
                    .Where(e => pe.Contains(e.Код))
                    .Select(e => new List_IdAndName()
                    {
                        Id = e.Код,
                        Names = e.Фамилия + " " + e.Имя + (e.Отчество != null ? " " + e.Отчество : ""),
                    })

                    .Where(e => e.Names != null)
                    .Distinct()
                    .OrderBy(e => e.Names)
                    .ToList();

        }


//        private ICollection<List_IdAndName> GetСписокФиоОтОплаты111111111111111()
//        {
//            //            IEnumerable<int> кодУченика = _cuatroCaminosBdEntities.Оплата.Where(e => e.Списки_людей.Выбыл_из_школы == false).Select(e => e.Код_Ученика).AsQueryable().Distinct();




//            if (HttpContext.Current.User.Identity.Name == StaticsVariables.ЛогинАдминистратора)
//            {

//                List<List_IdAndName> m = _cuatroCaminosBdEntities.Списки_людей
//                    //    .Where(e => e.Дата_регистрации > dateTimeReg)
//                    .Select(e => new List_IdAndName()
//                    {
//                        Id = e.Код,
//                        Names = e.Фамилия + " " + e.Имя + (e.Отчество != null ? " " + e.Отчество : ""),
//                    })

//                    .Where(e => e.Names != null)
//                    .Distinct()
//                    .OrderBy(e => e.Names)
//                    .ToList();


//                return m;


//            }
//            else
//            {



//                DateTime dateTime1 = DateTime.Now.AddMonths(-6);
//                DateTime dateTimeReg = DateTime.Now.AddMonths(-1);
//                IEnumerable<int> pe = СписокЛюдейВЗависимостиОтЛогина();


//                List<List_IdAndName> n = _cuatroCaminosBdEntities.Оплата
//                    .Where(e => e.Списки_людей.Дата_регистрации > dateTimeReg)
//                    .Where(e => pe.Contains(e.Код_Ученика))
//                    .Select(e => new List_IdAndName()
//                    {
//                        Id = e.Списки_людей.Код,
//                        Names =
//                            e.Списки_людей.Фамилия + " " + e.Списки_людей.Имя +
//                            (e.Списки_людей.Отчество != null ? " " + e.Списки_людей.Отчество : ""),
//                    })

//                    .Where(e => e.Names != null)
//                    .Distinct()
//                    .OrderBy(e => e.Names)
//                    .ToList();


//                List<List_IdAndName> l = _cuatroCaminosBdEntities.Оплата
//                    .Where(e => e.Списки_людей.Название_школ.Школы == "Cuatro Caminos")
//                    .Where(e => e.Списки_людей.Выбыл_из_школы == false)
//                    .Where(e => e.Дата_оплаты > dateTime1)
////                    .Where(e => gr.Contains(e.Название_танца))

//                    .Select(e => new List_IdAndName()
//                    {
//                        Id = e.Списки_людей.Код,
//                        Names =
//                            e.Списки_людей.Фамилия + " " + e.Списки_людей.Имя +
//                            (e.Списки_людей.Отчество != null ? " " + e.Списки_людей.Отчество : ""),
//                    })

//                    .Where(e => e.Names != null)
//                    .Distinct()
//                    .OrderBy(e => e.Names)
//                    .ToList();

//                var login = HttpContext.Current.User.Identity.Name.ToString();

//                List<List_IdAndName> m = n.Concat(l).Select(e => new List_IdAndName() { Id = e.Id, Names = e.Names }).OrderBy(e => e.Names).Distinct().ToList();

//                return m;


//            }


//        }

//        GetСписокФио

        public ICollection searchJsonPeopleAll(string term, int? birds)
        {

            ICollection<List_IdAndName> listIdAndNames = GetСписокФиоAll();

            return listIdAndNames
                     .Where(e => e.Names.ToLower().Contains(term.ToLower()))
                     .Select(e => new { id = e.Id, value = e.Names })
                     .ToArray();
        }



        public ICollection searchJsonPeople(string term, int? birds)
        {

            ICollection<List_IdAndName> listIdAndNames = GetСписокФио();

           return listIdAndNames
                    .Where(e => e.Names.ToLower().Contains(term.ToLower()))
                    .Select(e => new { id = e.Id, value = e.Names })
                    .ToArray();
        }


        public ICollection searchJsonPeopleLastName(string term, int? birds)
        {

            ICollection<List_IdAndName> listIdAndNames = _cuatroCaminosBdEntities
                .Списки_людей
                .Where(e=> e.Фамилия!=null)
                .Select(e => new List_IdAndName()
                    {
                            Names = e.Фамилия
                    })
                 .Distinct()
                 .ToArray();

            return listIdAndNames
                     .Where(e => e.Names.ToLower().Contains(term.ToLower()))
                     .Select(e => new { id = e.Id, value = e.Names })
                     .ToArray();
        }


        public ICollection searchJsonPeopleMiddleName(string term, int? birds)
        {

            ICollection<List_IdAndName> listIdAndNames = _cuatroCaminosBdEntities
                .Списки_людей
                .Where(e => e.Отчество != null)
                .Select(e => new List_IdAndName()
                {
                    Names = e.Отчество
                })
                 .Distinct()
                 .ToArray();

            return listIdAndNames
                     .Where(e => e.Names.ToLower().Contains(term.ToLower()))
                     .Select(e => new { id = e.Id, value = e.Names })
                     .ToArray();
        }


        public IList<List_IdAndName> searchJsonGetGroup(string user, int? birds)
        {
            return _cuatroCaminosBdEntities
                .Названия_танцев
//                .Where(e => e.Отчество != null)
                .Select(e => new List_IdAndName()
               {
                    
                    Id = e.Код,
                    Names = e.Название_танца
                })
                .OrderBy(e=>e.Names)
//                 .Distinct()
                 .ToList();


        }



        public ICollection<ListItemPeoplePay> searchJsonGetPayFromId(string user, string id)
        {

            int idPeople = id!= null? int.Parse(id): 0;

            //            IEnumerable<int> gr = СписокГруппВЗависимостиОтЛогина();

            return _cuatroCaminosBdEntities.Оплата
                .Where(e => e.Код_Ученика == idPeople)
                .OrderByDescending(e => e.Дата_оплаты)
                .AsQueryable()
                .ToList()
                //                .Where(e => gr.Contains(e.Название_танца))
                .Select(e => new ListItemPeoplePay()
            {
                DateCreate = e.DateTimeRec.ToString(),
                DatePay = e.Дата_оплаты.ToString(),
                PayValue = e.Сумма,
                Trainer = e.Имена_преподавателей.ФИО,
                Group = e.Названия_танцев.Название_танца,
                Id = e.Код,
                IdPeople = e.Код_Ученика
                })


               .ToArray();



        }


        String LastPayFromId(int id)
        {
            var singleOrDefault = _cuatroCaminosBdEntities.Оплата
                .Where(с => с.Код_Ученика == id)
                .OrderByDescending(c => c.Дата_оплаты)
                .Select(e => e.Дата_оплаты)
                .FirstOrDefault()
           

                ;


            if (singleOrDefault != null) return singleOrDefault.ToString();

            return null;
        }

        

        int GetGroupIdGromName(string group)
        {

            return _cuatroCaminosBdEntities.Названия_танцев
                .Where(e => e.Название_танца == group)
                .Select(e => e.Код)
                .SingleOrDefault();
        }





        public IList<ListItemPeople> searchJsonItemPeople(string user, string term, string group)
        {


            int groupId = GetGroupIdGromName(group);
//            groupId = 30;
            String _term = term;

            IList<Списки_людей> listItemPeople=new List<Списки_людей>();
            IList<List_IdAndName> idPeople = new List<List_IdAndName>();
            IList<List_IdAndName> idPeoplePay = new List<List_IdAndName>();


            if (term != null|| term!="")
            {


                idPeople = _cuatroCaminosBdEntities.Списки_людей
                    .Where(e => e.Фамилия != null && e.Школы == 7)
                    .Where(e => e.Фамилия.ToLower().Contains(_term.ToLower()) || e.Имя.ToLower().Contains(_term.ToLower()))
                    .Select(e=> new List_IdAndName()
                                    {
                                        Id = e.Код
                                    }
                    )
                    .Distinct()
                    .ToList();
            }


            if (group != null || group != "")
                {
                    idPeoplePay = _cuatroCaminosBdEntities.Оплата
                                            .Where(e => e.Названия_танцев.Код == groupId)
                                            .Select(e => new List_IdAndName()
                                            {
                                                Id = e.Код_Ученика
                                            })
                                            .Distinct()
                                            .ToList();
                }

            IEnumerable<int> idPeopleConcat = idPeople.Concat(idPeoplePay).Select(e => e.Id).Distinct();

            listItemPeople = _cuatroCaminosBdEntities.Списки_людей.Where(e => idPeopleConcat.Contains(e.Код)).ToList();

//                        IEnumerable<int> gr = СписокГруппВЗависимостиОтЛогина();
//                        IList<int> id = _cuatroCaminosBdEntities.Оплата.Where(e => gr.Contains(e.Названия_танцев.Код) && e.Названия_танцев.Код == group).Select(e => e.Списки_людей.Код).ToList();

                                                     

                      /*
                                   _cuatroCaminosBdEntities.Списки_людей
                                      .Where(e => 
                                          id.Contains(e.idGroupList) &&
                                             (group == 0 ? e.Название_школ.Код == 7 : true) && 
                      //                    e.Название_школ.Код==7 && 
                                          e.Код!=151)
                                      .OrderBy(e => e.Фамилия + " " + e.Имя)

                                      .ToList();
                      */




//            IList<Оплата> listItemPeoplePay =  _cuatroCaminosBdEntities.Оплата.ToList();


            IList<ListItemPeople> listItemPeople1 = listItemPeople
                        .Where(e=>e.Код!=151  )
                          .Select(e => new ListItemPeople()
                          {

                              Id = e.Код,
                              DateCreate =  e.Дата_регистрации.ToString(),
                              //                    DateLastPay =  new string(e.Оплата.Where(с => с.Код_Ученика == с.Списки_людей.Код).OrderByDescending(c=>c.Дата_оплаты).First(c => c.Дата_оплаты)),
                              DateLastPay = LastPayFromId(e.Код),
                              DatePeopleOver = e.Дата_выбытия.ToString(),
                              DateBirthDay = e.Дата_рождения.ToString(),
                              Fi = e.Фамилия + " " + e.Имя,
                              MiddleName = e.Отчество,
                              Img = null,
                              Phone = e.Телефон,
                              Sex = e.Пол,
                              Email = e.e_mail,
                              Retired = e.Выбыл_из_школы

                          })
//                          .AsEnumerable()
                          
//                          .Where(e => idPeopleConcat.Contains(e.Id))
                          //                .Where(e => e.Fi.ToLower().Contains(_term.ToLower()))
                          .OrderBy(e => e.Fi)
                          .ToList();






            return listItemPeople1;
        }



        public ICollection searchJsonPeopleName(string term, int? birds)
        {

            ICollection<List_IdAndName> listIdAndNames = _cuatroCaminosBdEntities
                .Списки_людей
                .Where(e => e.Имя != null)
                .Select(e => new List_IdAndName()
                {
                    Names = e.Имя
                })
                 .Distinct()
                 .ToArray();

            return listIdAndNames
                     .Where(e => e.Names.ToLower().Contains(term.ToLower()))
                     .Select(e => new { id = e.Id, value = e.Names })
                     .ToArray();
        }



        private IEnumerable<int> СписокЛюдей()
        {
//            DateTime dateTime = DateTime.Now.AddMonths(-2);

            if (HttpContext.Current.User.Identity.Name == StaticsVariables.ЛогинАдминистратора)
            {
                return _cuatroCaminosBdEntities.Списки_людей
//                    .Where(e => e.Название_школ.Код == 7 && e.Выбыл_из_школы != true)

                   .Select(e => new List_IdAndName()
                   {
                       Id = e.Код,
                       Names = e.Фамилия + " " + e.Имя + (e.Отчество != null ? " " + e.Отчество : ""),
                   })

                    .Where(e => e.Names != null)
                    .Select(e => e.Id)
                    .Distinct()
                    .ToArray();

            }
            else
            {
                

                    return _cuatroCaminosBdEntities.Списки_людей
                            .Where(e=>e.Название_школ.Код==7 && e.Выбыл_из_школы!=true)

                           .Select(e => new List_IdAndName()
                           {
                               Id = e.Код,
                               Names = e.Фамилия + " " + e.Имя + (e.Отчество != null ? " " + e.Отчество : ""),
                           })
 
                            .Where(e=>e.Names!=null)
                            .Select(e => e.Id)
                            .Distinct()
                            .ToArray();

            }

        }



        private IEnumerable<int> СписокЛюдейВЗависимостиОтЛогина(int? days=0)
        {
            DateTime dateTime = DateTime.Now.AddDays(-60);


//            dateTime = DateTime.Now.AddDays(days);


            IEnumerable<int> peolpe;

            IList<int> pay = _cuatroCaminosBdEntities.Оплата.Select(e => e.Код_Ученика).Distinct().ToList(); 

            IEnumerable<int> peolpeNew = _cuatroCaminosBdEntities.Списки_людей
                .Where(e => e.Дата_регистрации > dateTime)
                .Where(e=>!pay.Contains(e.Код))
                .Select(e => e.Код)
                .ToArray();


            if (HttpContext.Current.User.Identity.Name == StaticsVariables.ЛогинАдминистратора)
            {
                return _cuatroCaminosBdEntities.Списки_людей
                    .Select(e => e.Код)
                    .Distinct()
                    .ToArray();

            }
            else
            {

                IEnumerable<int> gr = СписокГруппВЗависимостиОтЛогина();

                peolpe = _cuatroCaminosBdEntities.Оплата
                    .Where(e => gr.Contains(e.Название_танца) && e.Списки_людей.Школы==7)
                    .Select(e => e.Код_Ученика)
                    .Distinct()
                    .ToArray();

                return peolpeNew.Concat(peolpe).Distinct().ToArray();

            }



        }


        private IEnumerable<int> СписокПреподавателейВЗависимостиОтЛогина()
        {
            IEnumerable<int> gr = СписокГруппВЗависимостиОтЛогина();

            return _cuatroCaminosBdEntities.Оплата
                .Where(e => gr.Contains(e.Название_танца))
                .Select(e => e.Преподаватель)
                .Distinct()
                .ToArray();

        }

        public bool IsPhoneExists(string phone)
        {
            return
                _cuatroCaminosBdEntities.Списки_людей.Where(e => e.Телефон == phone).AsQueryable().Any();
        }


        public List<string> IsPhoneExists(string phone, int? Код)
        {
            Код = Код ?? 0;

            return
                _cuatroCaminosBdEntities.Списки_людей
                .Where(e => e.Телефон == phone && e.Код != Код)
                        .AsQueryable()
                        .Select(e => e.Фамилия + " " + e.Имя + (e.Отчество != null ? " " + e.Отчество : ""))
                        .ToList();
        }

        public bool IsEmailExist(string email)
        {
            return
                _cuatroCaminosBdEntities.Списки_людей.Where(e => e.e_mail == email).AsQueryable().Any();

        }

 

        public bool IsEmailExist(string email, int? Код)
        {
            return
                _cuatroCaminosBdEntities.Списки_людей.Where(e => e.e_mail == email && e.Код != Код).AsQueryable().Any();

        }


        public string GetNameGroupFromIdAndDescriptoin(int? id = 0)
        {
            if (id != 0)
            {
                return
                    _cuatroCaminosBdEntities.Названия_танцев.Where(e => e.Код == id).Select(e =>
                             e.Description == null ? e.Название_танца : e.Название_танца + " (" + e.Description + ")"
                            ).
                        SingleOrDefault();
            }
            else
            {
                return "";
            }
        }


        public string GetNameGroupFromId(int? id=0)
        {
            if (id != 0){
            return
                _cuatroCaminosBdEntities.Названия_танцев.Where(e => e.Код == id).Select(e => e.Название_танца).
                    SingleOrDefault();
            }
            else
            {
                return "";
            }
        }

        public string GetDescGroupFromId(int? id = 0)
        {
            if (id != 0)
            {
                return
                    _cuatroCaminosBdEntities.Названия_танцев.Where(e => e.Код == id).Select(e => e.Description).
                        SingleOrDefault();
            }
            else
            {
                return "";
            }
        }

        public Название_мероприятий GetНазваниеМероприятия(int id)
        {
            return _cuatroCaminosBdEntities.Название_мероприятий.SingleOrDefault(e => e.Код == id);

        }


        public ICollection<Название_мероприятий> GetНазваниеМероприятий(int? id=0)
        {
            return _cuatroCaminosBdEntities.Название_мероприятий.Where(e => id != 0? e.Код == id: true).OrderByDescending(e=>e.Date).ToList();

        }

        public IEnumerable<string> GetНазваниеТанцевFromIdPeople(int people)
        {

            return
                _cuatroCaminosBdEntities.Оплата
                    .Where(e => e.Код_Ученика == people)
                    .Select(e => e.Названия_танцев.Название_танца)
                    .Distinct();


        }


        public ICollection<Названия_танцев> GetНазваниеТанцев()
        {
            IEnumerable<int> gr = СписокГруппВЗависимостиОтЛогина();
            return _cuatroCaminosBdEntities.Названия_танцев.Where(e=>gr.Contains(e.Код)).OrderBy(e=>e.Название_танца).ToList();

        }

        public ICollection<СписокНаМероприятие> GetСписокНаМероприятиеFromPeopleId(int peopleId)
        {
            return _cuatroCaminosBdEntities.СписокНаМероприятие.Where(e => e.PeopleId == peopleId).ToList();
        }


        public ICollection<СписокНаМероприятие> GetСписокНаМероприятие(int id)
        {


//            IList<СписокНаМероприятие>= _cuatroCaminosBdEntities.Оплата.Where()
            return _cuatroCaminosBdEntities.СписокНаМероприятие.Where(e => e.МероприятеID == id).ToList();
        }

        public ICollection<List_BirthDay> GetBirthDay()
        {
            DateTime dateTime = DateTime.Now.Date.AddDays(30);
            DateTime dateTimeOfPay = DateTime.Now.AddDays(-90);

            IEnumerable<int> pe = СписокЛюдейВЗависимостиОтЛогина();
            IEnumerable<int> pay = _cuatroCaminosBdEntities.Оплата
                                                           .Where(e => e.Дата_оплаты > dateTimeOfPay)
                                                           .Select(e => e.Код_Ученика);



            

            IEnumerable<List_BirthDay> l1 = _cuatroCaminosBdEntities.Списки_людей
//                .Join(_cuatroCaminosBdEntities.Оплата, e => e, r=>r,(e,r) => new List_BirthDay())
                .Where(e => 
                    pe.Contains(e.Код) 
                    && e.Дата_рождения != null && e.Выбыл_из_школы!=true
                    && pay.Contains(e.Код)
                    
                )
                
//                .Where(e=>e.Оплата.Where(e=>  e.Код_Ученика==e.Код && e.Дата_оплаты>dateTimeOfPay))
                .Select(e=> new List_BirthDay{Id = e.Код, Birthday = e.Дата_рождения, Names = e.Фамилия + " " + e.Имя})
                .OrderBy(e => e.Birthday)
                .ToList();

            Collection<List_BirthDay> l2 = new Collection<List_BirthDay>();



            foreach (var listBirthDay in l1)
            {

                if(listBirthDay.Birthday.Value.Month== DateTime.Now.Date.Month)
                {
                    
                    l2.Add(new List_BirthDay{Id = listBirthDay.Id, Birthday = listBirthDay.Birthday, Names = listBirthDay.Names, Day = listBirthDay.Birthday.Value.Day});
                }
            }
            
            return l2.OrderBy(e=>e.Day).ToList();
        }


        public IEnumerable<string> GetAlfavitFromФИО()
        {

            IEnumerable<int> pe = СписокЛюдейВЗависимостиОтЛогина();


            var list1 = _cuatroCaminosBdEntities.Списки_людей
                .Where(e => pe.Contains(e.Код))
                .Select(e => (e.Фамилия + " " + e.Имя + (e.Отчество != null ? " " + e.Отчество : "")))
                
                .ToList();

            List<string> list2= new List<string>();

            foreach (string alfavit in list1)
            {
                if (!String.IsNullOrEmpty(alfavit))
                {
                    list2.Add(alfavit.First().ToString());
                    
                }

            }
            return list2.Distinct().OrderBy(e=>e);
        }


    public int LiginRecId()
    {

        return GetFromLoginToId(HttpContext.Current.User.Identity.Name);
    }



    public void CreateCash(Cash cash)
    {
        cash.DataTimeCreate = DateTime.Now;
        cash.LoginRecId = LiginRecId();
        cash.Payment_expenses = true;
        cash.CategoryId = 1;

        _cuatroCaminosBdEntities.Cash.AddObject(cash);
        SaveChanges();
    }





    public void CreateActivity(Название_мероприятий названиеМероприятий)
    {
        названиеМероприятий.DataRec = DateTime.Now;
        названиеМероприятий.LiginRecId = LiginRecId();
        _cuatroCaminosBdEntities.Название_мероприятий.AddObject(названиеМероприятий);
        SaveChanges();
    }


        public void CreateActivityDetails(СписокНаМероприятие списокНаМероприятие)
    {


        списокНаМероприятие.DataTimeCreate = DateTime.Now;
            списокНаМероприятие.LiginRecId = LiginRecId();
        _cuatroCaminosBdEntities.СписокНаМероприятие.AddObject(списокНаМероприятие);
        SaveChanges();

    }


    public void DeleteActivityDetails(int idActivity)
    {

        СписокНаМероприятие списокНаМероприятие = _cuatroCaminosBdEntities.СписокНаМероприятие.SingleOrDefault(e => e.Id == idActivity);
        _cuatroCaminosBdEntities.СписокНаМероприятие.DeleteObject(списокНаМероприятие);
        SaveChanges();

    }


    public void DeletePeople(int id)
    {
        Списки_людей спискиЛюдей = GetPeaple(id);


        ICollection<Оплата> оплата = _cuatroCaminosBdEntities.Оплата.Where(e => e.Код_Ученика == id).ToList();

        if(оплата.Count==0)
        {
            _cuatroCaminosBdEntities.Списки_людей.DeleteObject(спискиЛюдей);
            SaveChanges();
        }
        else
        {
            foreach (Оплата item in оплата)
            {
                _cuatroCaminosBdEntities.Оплата.DeleteObject(item);
            }
            
            SaveChanges();
        }


    }
    
        public Cash GetCash(int id)
        {
            return _cuatroCaminosBdEntities.Cash.SingleOrDefault(e => e.Id == id);
        } 


        public ICollection<Cash> GetCash()
        {
            DateTime dateTime = DateTime.Now.AddDays(-60);
            return _cuatroCaminosBdEntities.Cash.Where(e=>e.DataTimePay>dateTime).OrderByDescending(e=>e.DataTimeCreate).ToList();
        } 




        public void DeleteCash(int id)
        {

            _cuatroCaminosBdEntities.Cash.DeleteObject(GetCash(id));
            SaveChanges();
            
        }


        public bool FindString(string string1, string string2)
        {
            if (string1.IndexOf(string2) > -1)
            {
                return true;
            }

            return false;
        }



    }



}


