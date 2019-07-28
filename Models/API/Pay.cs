using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuatroCaminosMvcApplication.Models.API
{
    public class Pay
    {
        private List<int> _id;
        private List<DateTime> _date;
        private List<List_IdAndName> _group;
        private List<int> _оплата;
        public List<int> _cash;

        IEnumerable<CashAndPay> _cashAndОплата;

        Cuatro_Caminos_BDEntities _cuatroCaminosBdEntities = new Cuatro_Caminos_BDEntities();

        public Pay(string term)
        {

            String _term = term ?? "вадим";

            IList<Списки_людей> listItemPeople = _cuatroCaminosBdEntities.Списки_людей
                .Where(e => e.Фамилия != null)
                .ToList();




        }


    }
}