using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ResumeApp.Model
{
    public class Validations
    {
        public static readonly Regex EmailRegex = new Regex(
               "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$",
               RegexOptions.Compiled);

        public  const string mobileRegex = "^\\d{10}$";
        public class EmailValidationAttribute : ValidationAttribute
        {
           

            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {
                if (value is string email && EmailRegex.IsMatch(email))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Invalid email format");
            }
        }
    }
}
