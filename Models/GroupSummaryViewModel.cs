using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuatroCaminosMvcApplication.Models
{
    public class GroupSummaryViewModel
    {
       ICollection<Оплата> pay;
       public IEnumerable<int> Year { get; set; }
       public IEnumerable<int> Month { get; set; }
       public IEnumerable<int> Amount { get; set; }
       public IEnumerable<string> Group { get; set; }
  


       public GroupSummaryViewModel(ICollection<Оплата> pay)
       {
           this.pay = pay;

       }

        public IList<PayGroup> Get()
        {


            return pay.Select(e =>

                              new PayGroup()
                                  {
                                      Date = e.Дата_оплаты.Date,
                                      Year = e.Дата_оплаты.Year,
                                      Group = e.Названия_танцев.Название_танца,
                                      GroupId = e.Названия_танцев.Код,
                                      GroupDescr = e.Названия_танцев.Description,
                                      GroupDateTimeRec = e.Названия_танцев.DateTimeRec,
                                      Month = e.Дата_оплаты.Month,
                                      PeopleCount = pay.
                                                    Where(c=>c.Названия_танцев.Название_танца==e.Названия_танцев.Название_танца)
                                                    .Select(оплата => оплата.Код_Ученика)
                                                    .Distinct()
                                                    .Count(),
                                      Amount = 
                                                pay
                                                .Where(r=>r.Названия_танцев.Название_танца==e.Названия_танцев.Название_танца)
                                                .Select(оплата => оплата.Сумма)
                                                .Sum()
                                  }

                )

                   .OrderBy(e=>e.Date.Month)
                   .ThenBy(e => e.Group)                   
                   .Distinct(new MyComparerPayGroup())
                      .ToList()

                ;
        }


    }
}