using EmployeeManagementWeb.Validation;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementProject.DTOs.Employee
{
    public class EmployeeDto
    {
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters")]
        public string LastName { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Other Name cannot be longer than 50 characters")]
        public string OtherName {  get; set; } = string.Empty;

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [PhoneNumberValidation(11, ErrorMessage = "Phone number must be exactly 11 digits")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [EmailValidation(ErrorMessage = "Email must end with @gmail.com")]
        public string Email { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Address cannot be longer than 1000 characters")]
        public string Address { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Title cannot be longer than 50 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        public Guid Id { get; set; }
        public string? DepartmentName { get; set; }
    }
}
