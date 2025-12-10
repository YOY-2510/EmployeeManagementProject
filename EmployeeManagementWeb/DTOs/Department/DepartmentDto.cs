using EmployeeManagementProject.DTOs.Employee;

namespace EmployeeManagementProject.DTOs.Department
{
    public class DepartmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<EmployeeDto>? Employees { get; set; }
    }
}
