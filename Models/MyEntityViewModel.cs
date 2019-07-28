using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CuatroCaminosMvcApplication.Models;

namespace CuatroCaminosMvcApplication.Models
{

    public class PriceList
    {
         
    }

/// <summary>
/// Класс для расчетов визитов
/// </summary>
    public class SummVisitAndPay
    {

       public List_IdAndName Name { get; set; }
       public  int PeopleId { get; set; }

/// <summary>
/// Сумма всех визитов
/// </summary>
       public int SummVisit { get; set; }

/// <summary>
/// Сумма платежей
/// </summary>
       public int SummPay { get; set; }

/// <summary>
/// Пропуск по причине
/// </summary>
       public int SummCause { get; set; }

    }


/// <summary>
/// Назначение начальных значений
/// </summary>
    public enum StaticVariable
    {

        СтоимостьАбонемента = 1200,
        КоличествоЗанятий = 8,
        СтоимостьРазовогоЗанятия = 150,
        Разовое = СтоимостьАбонемента / КоличествоЗанятий

    }

    //ваша сущность
    public class PayMonthEntity
    {

        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public List_IdAndName Name { get; set; }
//        public int? Value { get; set; }

        public int Pay { get; set; }
        public int PaySumm { get; set; }

        public SummVisitAndPay SummVisitAndPay { get; set; }

        public int Group { get; set; }
//        public int? Visit { get; set; }
        public int VisitFreezing { get; set; }


    }



/// <summary>
/// Отчет по оплате в развернутом виде
/// </summary>
    public class MyEntityViewModel
    {

        List<int> _id;
        List<List_IdAndName> _names;
        List<DateTime> _dates;
        List<int> _visitFreezing;
        List<int> Pay;


        private int _countVisit;
        private List<int> _paySumm;
        




        IEnumerable<PayMonthEntity> _myEntities;
        IEnumerable<SummVisitAndPay> _summVisitAndPay;
        

        VisitsDataManager visitsDataManager = new VisitsDataManager();


        public MyEntityViewModel(IEnumerable<PayMonthEntity> myEntities, IEnumerable<SummVisitAndPay> summVisitAndPay)
        {
            _id = myEntities.Select(s => s.Id).Distinct().ToList();
            _dates = myEntities.Select(s => s.Date).OrderBy(s => s.Date).Distinct(new MyComparerDate()).ToList();
            _names = myEntities.Select(s => s.Name).Distinct(new MyComparerList_IdAndName()).ToList();

//            _paySumm = summVisitAndPay.Select(s => s.SummPay).ToList();

//            _visitFreezing = myEntities.Select(s => s.VisitFreezing).ToList();

            _summVisitAndPay = summVisitAndPay;
            _myEntities = myEntities;
        }

/// <summary>
/// Общая сумма по оплате
/// </summary>
/// <param name="fio">ФИО</param>
/// <returns>итогова сумма по оплате</returns>
        public int? PaySumm(string fio)
        {

            return _summVisitAndPay.Where(e => e.Name.Names == fio).Select(e => e.SummPay).SingleOrDefault();
        }
        
/// <summary>
///  Количество визитов (посещений)
/// </summary>
/// <param name="fio"></param>
/// <returns></returns>
        public int? Visit(string fio)
        {

            return _myEntities.Where(e => e.Name.Names == fio && e.VisitFreezing==1).Select(e => e.VisitFreezing).Count();
        }


        public int LastPrice8()
        {


            return visitsDataManager.GetLastPrice(11);
        }


/// <summary>
///     Количество пропусков по уважительной причине
/// </summary>
/// <param name="fio"></param>
/// <returns></returns>
        public int? Freezing(string fio)
        {

            return _myEntities.Where(e => e.Name.Names == fio && e.VisitFreezing == 2).Select(e => e.VisitFreezing).Count();
        }

        public int Week()
        {
            return 1;// _myEntities.Where(e=> e.Date).
        }

        public int? hghg(string fio)
        {

            return PaySumm(fio) - Visit(fio) - Freezing(fio);
        }

        public List<int> VisitFreezing
        {
            get { return _visitFreezing; }
        } 

        public List<int> Id
        {
            get { return _id; }
        }


    public string DayOfWeekLocal()
    {
        int day = 0;

        for (int i = 1; i < 30; i++)
        {
            if((int)DateTime.Now.AddDays(-i).DayOfWeek==2)
            {
                day++;
            }
        }

        return DateTime.Now.AddDays(0).DayOfWeek.ToString();
    }


        public List<List_IdAndName> Names
        {
            get { return _names; }
        }

        public List<DateTime> Dates
        {
            get { return _dates; }
        }


        public int this[string name]
        {

            get
            {

                var result =
                    _myEntities
                        .Where(w => w.Name.Names == name)
                        .Select(w => w.Name.Id)
                        .Distinct()
                        .SingleOrDefault();


                //                    if (result != null)
                return result;

                //                return null;
            }
        }


        public int? this[string name, DateTime date]
        {

            get
            {

                var result = _myEntities.Where(w => w.Name.Names == name && w.Date == date).Sum(w => w.Pay);
                //                    if (result != null)
                return result;

                //                return null;
            }
        }


        //public int _countVisit()
        //{
        //    return 0;
        //}



        public VisitFreezing GetIdVisitCount(string name, DateTime date)
        {
            VisitFreezing visitFreezing = (VisitFreezing)_myEntities.Where(w =>
                                     w.Name.Names == name && w.Date == date)
                              .Select(w => w.VisitFreezing)
                              .SingleOrDefault();

            return visitFreezing;
        }




        public int? GetIdPaySumm(string name, DateTime date)
        {


            return _summVisitAndPay.Where(w => w.Name.Names == name).Sum(w => w.SummPay);

        }


        public int? GetIdPayCount(string name, DateTime date)
        {
            return _myEntities.Where(w => w.Name.Names == name && w.Date == date).Select(w => w.Id).Count();

        }

        public int GetIdPay(string name, DateTime date)
        {

            int result = _myEntities.Where(w => w.Name.Names == name && w.Date == date).Select(w => w.Id).SingleOrDefault();
//                    if (result != null)
                        return result;
        }
    }

}








namespace CuatroCaminosMvcApplication.MyHtmlHelpers
{
    public static class PayHtmlHelpers
    {

        

        public static string TestHelper5(this HtmlHelper htmlHelper, VisitFreezing visitFreezing)
        {

            StringBuilder stringBuilder = new StringBuilder();

            if (visitFreezing!=0)
            {
                stringBuilder.Append("(");
                stringBuilder.Append(visitFreezing.ToString());
                stringBuilder.Append(")");
            }



            return stringBuilder.ToString();
        }



        public static string GetVisit(this HtmlHelper htmlHelper, string fio, DateTime dateTimes)
        {
            VisitsDataManager visitsDataManager = new VisitsDataManager();

            StringBuilder stringBuilder = new StringBuilder();

            //if (visitFreezing != 0)
            //{
                stringBuilder.Append("(");
                stringBuilder.Append("PaySumm");
                stringBuilder.Append(")");
//            }



            return stringBuilder.ToString();
        }




        public static string GetIdVisitOstatok(this HtmlHelper htmlHelper, List<DateTime> dateTimes, string fio)
        {

            

            StringBuilder stringBuilder = new StringBuilder();

            if (dateTimes.Count > 0)
            {
                stringBuilder.Append("(");
                stringBuilder.Append(dateTimes.Count);
                stringBuilder.Append(fio);
            }



            return stringBuilder.ToString();
        }
    
    
    
    
    
    
    }




}