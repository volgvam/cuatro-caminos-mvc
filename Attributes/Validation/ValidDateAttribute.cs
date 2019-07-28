namespace CuatroCaminosMvcApplication.Attributes.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

		    public sealed class DateGreaterThanAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "'{0}' must be greater than '{1}'";
        private string _basePropertyName;
 
        public DateGreaterThanAttribute() : base(_defaultErrorMessage)
        {
            _basePropertyName = DateTime.Now.AddMonths(-1).ToString();
        }
 
        //Override default FormatErrorMessage Method
        public override string FormatErrorMessage(string name)
        {
            return string.Format(_defaultErrorMessage, name, _basePropertyName);
        }
 
        //Override IsValid
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Get PropertyInfo Object
            var basePropertyInfo = validationContext.ObjectType.GetProperty(_basePropertyName);
 
            //Get Value of the property
//            var startDate = (DateTime)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);
            DateTime startDate = DateTime.Now.AddMonths(-1);
  
            
            var thisDate = (DateTime)value;
 
            //Actual comparision
            if (thisDate <= startDate)
            {
                var message = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(message);
            }
 
            //Default return - This means there were no validation error
            return null;
        }
 
    }

	
}