namespace EmployeeManagementProject.DTOs.Employee
{
    public class EmployeeDto
    {
        public Guid EmployeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string OtherName {  get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public Guid Id { get; set; }
        public string? DepartmentName { get; set; }
    }
}
