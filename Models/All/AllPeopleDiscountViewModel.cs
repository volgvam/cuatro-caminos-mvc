using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuatroCaminosMvcApplication.Models.All
{
    public class AllPeopleDiscountViewModel
    {

        List<DateTime> _month;
        List<int> _year;
        List<decimal> _amount;
        List<string> _group;

        private AllPeople allPeople;
        private IEnumerable<PayGroup> _payPeople;
        private int id;


        public AllPeopleDiscountViewModel(int id)
        {
            allPeople = new AllPeople();
        }

        public AllPeopleDiscountViewModel(IEnumerable<PayGroup> payPeople)
        {

            _month = payPeople.Select(e => e.Date.Date).Distinct().ToList();
            _year = payPeople.Select(e => e.Date.Year).Distinct().ToList();
            _group = payPeople.Select(e => e.Group).Distinct().ToList();

            _payPeople = payPeople;

            
            allPeople = new AllPeople();
        }

   


        public double Discount
        {
            get { return allPeople.ShowDiscount(1); }
        }



    }



}