using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuatroCaminosMvcApplication.Models
{
    public class PayGroupViewModel
    {
        List<DateTime> _month;
        List<int> _year;
//        List<decimal> _amount;
        List<string> _group;

        private IEnumerable<PayGroup> _payGroup;

        DataManager dataManager = new DataManager();


        public PayGroupViewModel(int payGroup)
        {



        }

        public PayGroupViewModel(IEnumerable<PayGroup> payGroup)
        {

            _month = payGroup.Select(e => e.Date.Date).Distinct().ToList();
            _year = payGroup.Select(e => e.Date.Year).Distinct().ToList();
            _group = payGroup.Select(e => e.Group).Distinct().ToList();

            _payGroup = payGroup;
        }

        public List<DateTime> Month
        {
            get { return _month; }
        }

        public List<int> Year
        {
            get { return _year; }
        }
        public List<string> Group
        {
            get { return _group; }
        }

    

        public decimal? this[string name, DateTime date]
        {
            get { return null; }
        }


    }
}