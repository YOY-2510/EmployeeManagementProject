using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementProject.Models
{
    public class Department
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<Employee>? Employees { get; set; } = new List<Employee>();
    }
}
