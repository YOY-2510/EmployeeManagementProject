namespace EmployeeManagementProject.DTOs.Employee
{
    public class EmployeeDto
    {
        public Guid EmployeeId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Guid Id { get; set; }
        public string? DepartmentName { get; set; }
    }
}
