using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementWeb.Validation
{
    public class EmailValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var email = value as string;

            if (!string.IsNullOrEmpty(email) && email.EndsWith("@gmail.com"))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Email must end with @gmail.com");
        }
    }
}
