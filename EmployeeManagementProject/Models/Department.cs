using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeManagementProject.Models
{
    public class Department
    {
        public Guid DepartmentId { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string? Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}
