using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementWeb.Validation
{
    public class PhoneNumberValidation : ValidationAttribute
    {
        private readonly int _length;

        public PhoneNumberValidation(int length)
        {
            _length = length;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var phone = value as string;

            if (!string.IsNullOrEmpty(phone) && phone.Length == _length)
                return ValidationResult.Success;

            return new ValidationResult($"Phone number must be exactly {_length} digits");
        }
    }
}
