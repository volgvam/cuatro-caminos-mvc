using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuatroCaminosMvcApplication.Models
{
    public class CashAndPayViewModel
    {
        private List<int> _id;
        private List<DateTime> _date;
        private List<List_IdAndName> _group;
        private List<int> _оплата;
        public List<int> _cash;

        IEnumerable<CashAndPay> _cashAndОплата;




        public CashAndPayViewModel(IEnumerable<CashAndPay> cashAndОплата)
        {
            _id = cashAndОплата.Select(s => s.Id).ToList();
            _date = cashAndОплата.Select(s => s.Date).OrderByDescending(s => s.Date).Distinct().ToList();
            _group = cashAndОплата.Select(s => s.Group).ToList().Distinct(new  MyComparerList_IdAndName()).ToList();
            _оплата = cashAndОплата.Select(s => s.Оплата).ToList();
            _cash = cashAndОплата.Select(s => s.Cash).ToList();
            _cashAndОплата = cashAndОплата;
        }


        //public List<int> Id
        //{
        //    get { return _id; }
        //}



        public List<List_IdAndName> Group
        {
            get{return _group;} 
        }



        public List<int> Оплата
        {
            get { return _оплата; }
        }

        public List<DateTime> Date
        {
            get { return _date; }
        }

        public int Pay(DateTime dateTime, string group)
        {

                int result =
                    _cashAndОплата
                        .Where(w => w.Date == dateTime && w.Group.Names == group)

                        .Select(w => w.Оплата)
                        .Sum();
                return result;

        }


        public int Id(DateTime dateTime, string group, int cash)
        {

              int result =
                _cashAndОплата
                .Where(w => w.Date == dateTime && w.Group.Names == group && w.Cash == cash)

                .Select(w => w.Id)
                .Sum();
                return result;


            
        }



        public int Cash(DateTime dateTime, string group)
        {

            var result =
                _cashAndОплата
                    .Where(w => w.Date == dateTime && w.Group.Names == group)
                    .Select(w => w.Cash)
                    .Sum();
 
            return result;

        }

        public int PaySumm()
        {

            var result =
                _cashAndОплата
                //                    .Where(w => w.Date == dateTime && w.Group == group)
                    .Select(w => w.Оплата)
                    .Sum();
            return result;

        }

        public int CashSumm()
        {

            var result =
                _cashAndОплата
                //                    .Where(w => w.Date == dateTime && w.Group == group)
                    .Select(w => w.Cash)
                    .Sum();

            return result;

        }

        public int  AllSummMoney()
        {

            return _cashAndОплата.Select(e => e.Cash).Sum();
        }


//        public int this[DateTime dateTime]
//        {

//            get
//            {

//                var result =
//                    _cashAndОплата
//                        .Where(w => w.Date == dateTime)
//                        .Select(w => w.Оплата)
//                        .Sum();
////                        .Distinct()
////                        .SingleOrDefault();


//                //                    if (result != null)
//                return result;

//                //                return null;
//            }
//        }

        //public int? [DateTime dateTime]
        //{
            
        //    get { return null; }
        //}


        //public int? this[string name, DateTime date]
        //{

        //    get
        //    {

        //        var result = _myEntities.Where(w => w.Name == name && w.Date == date).OrderBy(w => w.Name).Sum(w => w.Value);
        //        //                    if (result != null)
        //        return result;

        //        //                return null;
        //    }
        //}




    }
}