using System.Text.Json.Serialization;

namespace EmployeeManagementProject.Models
{
    public class Employee
    {
        public Guid EmployeeId {  get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string OtherName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public Guid DepartmentId { get; set; }

        [JsonIgnore]
        public Department? Department { get; set; }
    }
}
