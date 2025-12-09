using System.Text.Json.Serialization;

namespace EmployeeManagementProject.Models
{
    public class Employee
    {
        public Guid EmployeeId {  get; set; } = Guid.NewGuid();
        public string FullName { get; set; } = null;
        public string Email { get; set; } = null;
        public string? PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Guid DepartmentId { get; set; }

        [JsonIgnore]
        public Department? Department { get; set; }
    }
}
