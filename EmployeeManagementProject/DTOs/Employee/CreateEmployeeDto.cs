namespace EmployeeManagementProject.DTOs.Employee
{
    public class CreateEmployeeDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
