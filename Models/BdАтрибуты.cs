using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using CuatroCaminosMvcApplication.Attributes.Validation;
using CuatroCaminosMvcApplication.Models;


namespace CuatroCaminosMvcApplication.Models
{


    //    [System.ComponentModel.DataAnnotations.]
    //public class BdАтрибуты : ActionFilterAttribute
    //{

    //}



    public class AllPeopleFormEnter
    {
        /// <summary>
        /// Фамилия
        /// </summary>
 
        [Required]
        [DisplayName("Фамилия")]
        [StringLength(50, ErrorMessage = "Значение {0} должно содержать не менее {2} и не более {1} символов.",
    MinimumLength = 2)]
        public string LastName { get; set; }
        
        /// <summary>
        /// Имя
        /// </summary>
 
        [Required]
        [DisplayName("Имя")]
        [StringLength(50, ErrorMessage = "Значение {0} должно содержать не менее {2} и не более {1} символов.",
    MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
 
        [Required]
        [DisplayName("Отчество")]
        [StringLength(50, ErrorMessage = "Значение {0} должно содержать не менее {2} и не более {1} символов.",
    MinimumLength = 2)]
        public string FirstName { get; set; }

        
        /// <summary>
        /// Телефон
        /// </summary>
 
        [Required]
        [DisplayName("Телефон (без 8 и +7)")]
        [MaxLength(10)]
        [StringLength(10, ErrorMessage = "Телефон должен содержать {2} цифр (без 8 и +7).", MinimumLength = 10)]
        //        [Range(10, 10)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }


    }





    public class ListItemPeople
    {
        public int Id { get; set; }
        public String DateCreate { get; set; }
        public String DateLastPay { get; set; }
        public String DateBirthDay { get; set; }
        public String Fi { get; set; }
        public String MiddleName { get; set; }
        public Byte? Img { get; set; }
        public String Phone { get; set; }
        public String DatePeopleOver { get; set; }
        public String Email { get; set; }
        public String Sex { get; set; }
        public bool? Retired { get; set; }


    }




    public class ListItemPeoplePay
    {

        public int Id { get; set; }
        public int IdPeople { get; set; }
        public string DateCreate { get; set; }
        public string DatePay { get; set; }
        public int PayValue { get; set; }
        public String Fi { get; set; }
        public String Group { get; set; }
        public String Trainer { get; set; }


    }

    public class PayGroup
    {


        public DateTime Date { get; set; }

        [DisplayName("Месяц")]
        public int Month { get; set; }

        [DisplayName("Год")]
        public int Year { get; set; }

        [DisplayName("Сумма")]
        public decimal Amount { get; set; }

        [DisplayName("Группа")]
        public string Group { get; set; }
        public int GroupId { get; set; }
        public string GroupDescr { get; set; }

        public int PeopleCount { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        [DisplayName("Дата создания")]
        public DateTime? GroupDateTimeRec { get; set; }

    }



    public class ListForGoogleExport
    {

        public string Name { get; set; }
        public string Given_Name { get; set; }
        public string Additional_Name { get; set; }
        public string Family_Name { get; set; }
        public string Yomi_Name { get; set; }
        public string Given_Name_Yomi { get; set; }
        public string Additional_Name_Yomi { get; set; }
        public string Family_Name_Yomi { get; set; }
        public string Name_Prefix { get; set; }
        public string Name_Suffix { get; set; }
        public string Initials { get; set; }
        public string Nickname { get; set; }
        public string Short_Name { get; set; }
        public string Maiden_Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public string Location { get; set; }
        public string Billing_Information { get; set; }
        public string Directory_Server { get; set; }
        public string Mileage { get; set; }
        public string Occupation { get; set; }
        public string Hobby { get; set; }
        public string Sensitivity { get; set; }
        public string Priority { get; set; }
        public string Subject { get; set; }
        public string Notes { get; set; }
        public string Group_Membership { get; set; }
        public string E__mail_1__Type { get; set; }
        public string E__mail_1__Value { get; set; }
        public string E__mail_2__Type { get; set; }
        public string E__mail_2__Value { get; set; }
        public string IM_1__Type { get; set; }
        public string IM_1__Service { get; set; }
        public string IM_1__Value { get; set; }
        public string Phone_1__Type { get; set; }
        public string Phone_1__Value { get; set; }
        public string Phone_2__Type { get; set; }
        public string Phone_2__Value { get; set; }
        public string Phone_3__Type { get; set; }
        public string Phone_3__Value { get; set; }
        public string Address_1__Type { get; set; }
        public string Address_1__Formatted { get; set; }
        public string Address_1__Street { get; set; }
        public string Address_1__City { get; set; }
        public string Address_1__PO_Box { get; set; }
        public string Address_1__Region { get; set; }
        public string Address_1__Postal_Code { get; set; }
        public string Address_1__Country { get; set; }
        public string Address_1__Extended_Address { get; set; }
        public string Organization_1__Type { get; set; }
        public string Organization_1__Name { get; set; }
        public string Organization_1__Yomi_Name { get; set; }
        public string Organization_1__Title { get; set; }
        public string Organization_1__Department { get; set; }
        public string Organization_1__Symbol { get; set; }
        public string Organization_1__Location { get; set; }
        public string Organization_1__Job_Description { get; set; }
        public string Website_1__Type { get; set; }
        public string Website_1__Value { get; set; }
        public string Custom_Field_1__Type { get; set; }
        public string Custom_Field_1__Value { get; set; }
        public string Custom_Field_2__Type { get; set; }
        public string Custom_Field_2__Value { get; set; }

    }



    public class ListNamesAndPhones
    {
        public string Fio { get; set; }
        public string Phone { get; set; }

    }


    public class List_BirthDay
    {
        public int Id { get; set; }
        public string Names { get; set; }
        public int? Day { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        [DisplayName("Дата рождения")]
        public DateTime? Birthday { get; set; }


    }


    public class List_IdAndName
    {
        public int Id { get; set; }
        public string Names { get; set; }
    }

    public class ListTanec : List_IdAndName
    {
        public int Tanec { get; set; }
    }

    public class Sex
    {
        public string YesNo { get; set; }
    }

    public class BoolTrueFalse
    {
        public bool TrueFalse { get; set; }
        public string Name { get; set; }
    }


    [MetadataType(typeof (NotesMetaData))]
    public partial class Notes
    {
    }

    public class NotesMetaData
    {
        [UIHint("TextBoxDataTime")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DisplayName("Дата создания")]
        public int DataCreate { get; set; }



        [UIHint("TextBoxDataTime")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DisplayName("Дата напоминания")]
        public int DatePlan { get; set; }


        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        [StringLength(1000, ErrorMessage = "Значение {0} должно содержать не менее {2} и не более {1} символов.",
            MinimumLength = 2)]
        [DisplayName("Текст заметки")]
        public string Message { get; set; }

        [UIHint("DropDownList")]
        [Required]
        [DisplayName("Категория заметки")]
        public string IdCategoryNotes { get; set; }



    }





    [MetadataType(typeof (СписокНаМероприятиеMetaData))]
    public partial class СписокНаМероприятие
    {


        [DisplayName("В каких группах занимаются")]
        public virtual List<string> Group { get; set; }

    }

    public class СписокНаМероприятиеMetaData
    {


        [Required]
        [UIHint("AutoComplete")]
        [DisplayName("Имя")]
        public int PeopleId { get; set; }

        [Range(11, 3500, ErrorMessage = "{0} может быть от {1} до {2} рублей.")]
        [DisplayName("Сумма оплаты")]
        public int Pay { get; set; }

        [DisplayName("На авто? (при выездных мероприятиях)")]
        public bool Avto { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Комментарий")]
        public string Description { get; set; }


        [DisplayName("Флажок можно использовать на свое усмотрение")]
        public bool Ok { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        [DisplayName("Дата внесения")]
        public DateTime DataTimeCreate { get; set; }

        [DisplayName("Кто внес запись")]
        public int LiginRecId { get; set; }




    }


    [MetadataType(typeof (Название_мероприятийMetaData))]
    public partial class Название_мероприятий
    {
    }

    public class Название_мероприятийMetaData
    {
        //        [Required]
        //        [DisplayName("Ученик")]
        ////        [Remote("People", "Activity", HttpMethod = "GET", ErrorMessage = "Такой номер телефона уже есть в базе", AdditionalFields = "birds")]
        //        public int Участники { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        [UIHint("TextBoxDataTime")]
        [DisplayName("Дата мероприятия")]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(250)]
        [StringLength(250, ErrorMessage = "Значение {0} должно содержать не менее {2} и не более {1} символов.",
            MinimumLength = 2)]
        [DisplayName("Название мероприятия")]
        public string Мероприятие { get; set; }

        [MaxLength(5000)]
        [StringLength(5000, ErrorMessage = "Значение {0} должно содержать не менее {2} и не более {1} символов.",
            MinimumLength = 2)]
        [DataType(DataType.MultilineText)]
        [DisplayName("Описание мероприятия")]
        public string Примечание { get; set; }


        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        [DisplayName("Дата записи")]
        public DateTime DataRec { get; set; }


        [DisplayName("Закрыть мероприятие?")]
        public bool Close { get; set; }


    }



    [MetadataType(typeof (Списки_людейMetaData))]
    public partial class Списки_людей
    {

        public string ФИО
        {
            get { return Фамилия + " " + Имя + (!String.IsNullOrEmpty(Отчество) ? " " + Отчество : ""); }
        }

        public string ФИ
        {
            get { return Фамилия + " " + Имя; }
        }

    }



    public class Списки_людейMetaData
    {



        [Required]
        [UIHint("TextBoxDataTime")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        [DisplayName("Дата регистрации")]
        public DateTime Дата_регистрации { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Фамилия")]
        [UIHint("AutoCompleteLastName")]
        [StringLength(50, ErrorMessage = "Значение {0} должно содержать не менее {2} и не более {1} символов.",
            MinimumLength = 2)]
        public string Фамилия { get; set; }

        [Required]
        [MaxLength(50)]
        [UIHint("AutoCompleteName")]
        [StringLength(50, ErrorMessage = "Значение {0} должно содержать не менее {2} и не более {1} символов.",
            MinimumLength = 2)]
        public string Имя { get; set; }


        [UIHint("AutoCompleteMiddleName")]
        [DisplayName("Отчество")]
        public string Отчество { get; set; }

        [DataType(DataType.Date)]
        [UIHint("TextBoxDataTime")]
        [DisplayName("Дата рождения")]
        public DateTime Дата_рождения { get; set; }


        [UIHint("DropDownList")]
        [DisplayName("Район")]
        public int Район { get; set; }

        //        [UIHint("TextBoxPhone")]
        [DisplayName("Телефон (без 8 и +7)")]
        [MaxLength(10)]
        [StringLength(10, ErrorMessage = "Телефон должен содержать {2} цифр (без 8 и +7).", MinimumLength = 10)]
        //        [Range(10, 10)]
        [DataType(DataType.PhoneNumber)]
        [Remote("PhoneExist", "People", HttpMethod = "POST", ErrorMessage = "Такой номер телефона уже есть в базе",
            AdditionalFields = "Код")]
        public string Телефон { get; set; }

        [UIHint("DropDownList")]
        [Required]
        [DisplayName("Рассылка SMS")]
        public int SMS { get; set; }



        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail")]
        [RegularExpression(@"[\w\.-]*[a-zA-Z0-9_]@[\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]",
            ErrorMessage = "Введен не корректный е-майл адрес")]
        [Remote("EmailExist", "People", HttpMethod = "POST", ErrorMessage = "Такой e-mail уже существет",
            AdditionalFields = "Код")]
        public string e_mail { get; set; }

        [DisplayName("Состоит в группе vkontakte.ru")]
        public bool Вконтекте { get; set; }



        [DataType(DataType.MultilineText)]
        [DisplayName("Откуда узнали о нас")]
        [StringLength(1000, ErrorMessage = "Значение {0} должно содержать не менее {2} и не более {1} символов.",
            MinimumLength = 2)]
        public string От_куда_узнал_а_ { get; set; }


        //       [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[Null]")]
        [StringLength(1000, ErrorMessage = "Значение {0} должно содержать не менее {2} и не более {1} символов.",
            MinimumLength = 2)]
        [DataType(DataType.MultilineText)]
        [DisplayName("Хобби")]
        public string Хобби { get; set; }


        [DisplayName("Участие в выступлениях")]
        public bool Участие_в_выступлениях { get; set; }



        [DataType(DataType.MultilineText)]
        [DisplayName("Примечание")]
        [StringLength(1000, ErrorMessage = "Значение {0} должно содержать не менее {2} и не более {1}символов.",
            MinimumLength = 2)]
        public string Примечание { get; set; }


        [UIHint("DropDownList")]
        [Required]
        [DisplayName("Название школы")]
        public int Школы { get; set; }


        [UIHint("DropDownList")]
        [Required]
        [DisplayName("Пол")]
        public string Пол { get; set; }



        [DisplayName("Выбыл из школы")]
        public bool Выбыл_из_школы { get; set; }



        [DataType(DataType.Date)]
        [UIHint("TextBoxDataTime")]
        [DisplayName("Дата выбытия")]
        public DateTime Дата_выбытия { get; set; }


        [DisplayName("Наличие авто, да/нет")]
        public bool Наличие_авто { get; set; }

        [DisplayName("Ф.И.О.")]
        public string ФИО { get; set; }


        [DisplayName("Ф.И.")]
        public string ФИ { get; set; }

    }


    [MetadataType(typeof (Имена_преподавателейMetaData))]
    public partial class Имена_преподавателей
    {
    }

    public class Имена_преподавателейMetaData
    {
        public int Код { get; set; }
    }



    [MetadataType(typeof (ОплатаMetaData))]
    public partial class Оплата
    {
    }


    [DisplayName("Форма редактирования оплаты")]
    public class ОплатаMetaData
    {

        [UIHint("TextBoxDataTime")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DisplayName("Дата оплаты")]
        //        [DateGreaterThan]
        public DateTime Дата_оплаты { get; set; }


        [UIHint("AutoComplete")]
        //        [UIHint("DropDownList")]
        [Required]
        [DisplayName("ФИО")]
        public int Код_Ученика { get; set; }



        [Required]
        [Range(1, 12)]
        [DisplayName("Месяц")]
        [ScaffoldColumn(false)]
        public int Месяц { get; set; }



        [Required]
        //        [DisplayFormat(DataFormatString = "{0:C}")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        //        [Range(typeof(int), "0", "49")]
        [Range(11, 3500, ErrorMessage = "{0} может быть от {1} до {2} рублей.")]
        [DisplayName("Сумма оплаты")]
        public int Сумма { get; set; }


        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} и не более {1} символов.",
            MinimumLength = 2)]
        [DataType(DataType.MultilineText)]
        [DisplayName("Примечание")]
        public string Примечание { get; set; }


        [UIHint("DropDownList")]
        [Required]
        [DisplayName("Преподаватель")]
        public int Преподаватель { get; set; }


        [UIHint("DropDownList")]
        [Required]
        [DisplayName("Название группы")]
        public int Название_танца { get; set; }


    }





    public class CashAndPay
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List_IdAndName Group { get; set; }
        public int Оплата { get; set; }
        public int Cash { get; set; }
    }



    public class ОтчетПоОплате
    {
        public string fio { get; set; }

        public string ДатыОплаты { get; set; }
        public string Месяц { get; set; }
    }


    public class ДатаОплаты
    {
        public string датаОплаты { get; set; }
        public virtual ОтчетПоОплате ОтчетПоОплате { get; set; }
    }


    [MetadataType(typeof (CashMetaData))]
    public partial class Cash
    {
    }

    [DisplayName("Форма редактирования кассы")]
    public class CashMetaData
    {

        [UIHint("TextBoxDataTime")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DisplayName("Дата внесения")]
        public DateTime DataTimePay { get; set; }

        [Required]
        [DisplayName("Кто внес запись")]
        public int LoginRecId { get; set; }


        [Required]
        [UIHint("AutoComplete")]
        [DisplayName("Имя")]
        public int PeopleId { get; set; }


        [UIHint("DropDownList")]
        [Required]
        [DisplayName("Название танца")]
        public int GroupId { get; set; }


        [Required]
        //        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(11, 35000, ErrorMessage = "{0} может быть от {1} до {2} рублей.")]
        [DisplayName("Сумма оплаты")]
        public int Money { get; set; }



        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} и не более {1} символов.",
            MinimumLength = 2)]
        [DataType(DataType.MultilineText)]
        [DisplayName("Комментарий")]
        public string Comments { get; set; }

        public int CategoryId { get; set; }

    }

    public enum VisitsEnum
    {
        Cash,
        CreditCard,
    }

    public class ТестMyView
    {
        public VisitsEnum VisitsEnum { get; set; }
    }

    public class IdIntStringModel
    {
        public int Id { get; set; }
        public int Payment { get; set; }
        public string Fio { get; set; }
    }







    //public class MyComparerPayMonthEntity : IEqualityComparer<PayMonthEntity>
    //{
    //    public bool Equals(PayMonthEntity x, PayMonthEntity y)
    //    {
    //        return (x.Name.Names == y.Name.Names);
    //    }

    //    public int GetHashCode(PayMonthEntity obj)
    //    {
    //        return obj.Name.Names.GetHashCode();
    //    }
    //}


    public class MyComparerPayGroup : IEqualityComparer<PayGroup>
    {
        public bool Equals(PayGroup x, PayGroup y)
        {

            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;



            return (x.Group == y.Group && x.Month == y.Month && x.Year == y.Year);
        }

        public int GetHashCode(PayGroup obj)
        {

            //Check whether the object is null
            if (Object.ReferenceEquals(obj, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashProductName = obj.Group == null ? 0 : obj.Group.GetHashCode();

            //Get hash code for the Code field.
            int hashProductCode = obj.Amount.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductName ^ hashProductCode;


//            return obj.Group.GetHashCode();
        }
    }


    public class MyComparerList_IdAndName : IEqualityComparer<List_IdAndName>
    {
        public bool Equals(List_IdAndName x, List_IdAndName y)
        {
            return (x.Names == y.Names);
        }

        public int GetHashCode(List_IdAndName obj)
        {
            return obj.Names.GetHashCode();
        }
    }

    public class MyComparerDate : IEqualityComparer<DateTime>
    {

        public bool Equals(DateTime x, DateTime y)
        {
            return (x.Date == y.Date);
        }

        public int GetHashCode(DateTime obj)
        {
            return obj.Date.GetHashCode();
        }
    }
}