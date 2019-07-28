using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuatroCaminosMvcApplication.Models
{

    public class PriceClass
    {

/// <summary>
/// Цена абонемента
/// </summary>

        private int priceAbonement4;
        private int priceAbonement8;
        private int priceAbonement16;

/// <summary>
/// Цена разового занятия
/// </summary>
        private int priceOne;

/// <summary>
/// Скидка
/// </summary>
        private int discount;


        private int numberOfLessons;

        private int _group ;
        private Cuatro_Caminos_BDEntities _caminosBdEntities;



        private PriceClass()
        {
            priceAbonement4 = 700;
            priceAbonement8 = 1200;
            priceAbonement16 = 2200;

            /// Цена разового занятия
            priceOne = 200;

            /// Скидка
            discount=0;

            numberOfLessons = 8;

        }


        /// <summary>
        /// Определение последних цен
        /// </summary>
        /// <param name="group">Id группы</param>
        /// <returns>цена абонемента</returns>

        public PriceClass(int group) : this ()
        {
         


            _caminosBdEntities = new Cuatro_Caminos_BDEntities();
            _group = group;

        }

/// <summary>
/// Стоимость абонемента на 8 занятий
/// </summary>
/// <returns></returns>
        public int PriceAbonement8
        {
            get
            {

                int price = _caminosBdEntities.PriceHistory
                    .AsQueryable().Where(e => e.GroupId == _group)
                    .OrderByDescending(e => e.Date)
                    .Select(e => e.Price.Subscription_8)
                    .FirstOrDefault();

                return price != 0 ? price : priceAbonement8;

            }



        }

/// <summary>
/// Стоимость абонемента на 4 занятия
/// </summary>
/// <returns></returns>
        public int PriceAbonement4
        {
            get
            {
                int? price = _caminosBdEntities.PriceHistory
                    .AsQueryable().Where(e => e.GroupId == _group)
                    .OrderByDescending(e => e.Date)
                    .Select(e => e.Price.Subscription_4)
                    .FirstOrDefault();

                return price != 0 ? price??0 : priceAbonement4;
            }
        }


        /// <summary>
        /// Стоимость абонемента на 16 занятий
        /// </summary>
        /// <returns></returns>
        public int PriceAbonement16
        {
            get
            {

                int? price = _caminosBdEntities.PriceHistory
                    .AsQueryable().Where(e => e.GroupId == _group)
                    .OrderByDescending(e => e.Date)
                    .Select(e => e.Price.Subscription_16)
                    .FirstOrDefault();

                return price != 0 ? price ?? 0 : priceAbonement16;
            }
        }


/// <summary>
/// Стоимость разового занятия
/// </summary>
/// <returns></returns>
        public int PriceOne
        {
            get
            {


                int price = _caminosBdEntities.PriceHistory
                    .AsQueryable()
                    .Where(e => e.GroupId == _group)
                    .OrderByDescending(e => e.Date)
                    .Select(e => e.Price.Single)
                    .FirstOrDefault();

                return price != 0 ? price : priceOne;
            }

        }


/// <summary>
/// Узнаем расписание занятий (по каким дням недели)
/// </summary>
        public IList<int> DayOfWeekLessons
        {
            get
            {

                IList<int> dayOfWeek = new List<int>();        // = new int[] { };

                string dayOfWeek_str = _caminosBdEntities.Календарь_занятий
                    .Where(e => e.GroupId == _group)
                    .OrderByDescending(e => e.Date)
                    .Select(e => e.DayOfWeek)
                    .FirstOrDefault();

                string[] dayOfWeek_char = !String.IsNullOrEmpty(dayOfWeek_str) ? dayOfWeek_str.Split(';') : new string[]{};

                foreach (string s in dayOfWeek_char)
                {
                    dayOfWeek.Add(Convert.ToInt32(s));
                }

                return dayOfWeek;
            }
        } 





    }
}