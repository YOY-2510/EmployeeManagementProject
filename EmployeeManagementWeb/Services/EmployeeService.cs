using EmployeeManagementProject.DTOs;
using EmployeeManagementProject.DTOs.Employee;
using EmployeeManagementProject.Models;
using EmployeeManagementProject.Repositories.Interface;
using EmployeeManagementProject.Services.Interface;
using Serilog;

namespace EmployeeManagementProject.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<BaseResponse<EmployeeDto>> AddEmployeeAsync(CreateEmployeeDto request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Creating a new employee: {FullName LastName OtherName}", request.FirstName, request.LastName, request.OtherName );

                var employee = new Employee
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    OtherName = request.OtherName,
                    Gender = request.Gender,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    Address = request.Address,
                    Title = request.Title,
                    DateOfBirth = request.DateOfBirth,
                    DepartmentId = request.Id
                };

                var added = await _employeeRepository.AddAsync(employee, cancellationToken);

                var response = new EmployeeDto
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    OtherName = employee.OtherName,
                    Gender = employee.Gender,
                    PhoneNumber = employee.PhoneNumber,
                    Email = employee.Email,
                    Address = employee.Address,
                    Title = employee.Title,
                    DateOfBirth = employee.DateOfBirth,
                    Id = employee.DepartmentId
                };

                Log.Information("Employee created successfully: {EmployeeId}", employee.EmployeeId);
                return BaseResponse<EmployeeDto>.SuccessResponse(response, "Employee created successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error creating employee");
                return BaseResponse<EmployeeDto>.FailResponse("An error occurred while trying to create employee");
            }
        }

        public async Task<BaseResponse<bool>> DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Deleting employee {EmployeeId}", id);

                var deleted = await _employeeRepository.DeleteAsync(id, cancellationToken);

                if (!deleted)
                {
                    Log.Warning("Employee {EmployeeId} not found", id);
                    return BaseResponse<bool>.FailResponse("Employee not found");
                }

                Log.Information("Employee {EmployeeId} deleted successfully", id);
                return BaseResponse<bool>.SuccessResponse(true, "Employee deleted successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error deleting employee {EmployeeId}", id);
                return BaseResponse<bool>.FailResponse($"Error: {ex.Message}");
            }
        }

        public async Task<BaseResponse<IEnumerable<EmployeeDto>>> GetAllEmployeesAsync(CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Fetching all employees");

                var employees = await _employeeRepository.GetAllAsync(cancellationToken);

                var dtoList = employees.Select(emp => new EmployeeDto
                {
                    EmployeeId = emp.EmployeeId,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    OtherName = emp.OtherName,
                    Gender = emp.Gender,
                    PhoneNumber = emp.PhoneNumber,
                    Email = emp.Email,
                    Address = emp.Address,
                    Title = emp.Title,
                    DateOfBirth = emp.DateOfBirth,
                    Id = emp.DepartmentId
                });

                Log.Information("Retrieved {Count} employees", dtoList.Count());
                return BaseResponse<IEnumerable<EmployeeDto>>.SuccessResponse(dtoList, "Employees retrieved successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching employees");
                return BaseResponse<IEnumerable<EmployeeDto>>.FailResponse("An error occurred while retrieving employees");
            }
        }

        public async Task<BaseResponse<EmployeeDto>> GetEmployeeByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Fetching employee {EmployeeId}", id);

                var emp = await _employeeRepository.GetByIdAsync(id, cancellationToken);

                if (emp == null)
                {
                    Log.Warning("Employee {EmployeeId} not found", id);
                    return BaseResponse<EmployeeDto>.FailResponse("Employee not found");
                }

                var dto = new EmployeeDto
                {
                    EmployeeId = emp.EmployeeId,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    OtherName = emp.OtherName,
                    Gender = emp.Gender,
                    PhoneNumber = emp.PhoneNumber,
                    Email = emp.Email,
                    Address = emp.Address,
                    Title = emp.Title,
                    DateOfBirth = emp.DateOfBirth,
                    Id = emp.DepartmentId,
                    DepartmentName = emp.Department?.Name
                };

                Log.Information("Employee {EmployeeId} retrieved successfully", id);
                return BaseResponse<EmployeeDto>.SuccessResponse(dto, "Employee retrieved successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error retrieving employee {EmployeeId}", id);
                return BaseResponse<EmployeeDto>.FailResponse($"Error: {ex.Message}");
            }
        }

        public async Task<BaseResponse<EmployeeDto>> UpdateEmployeeAsync(Guid id, CreateEmployeeDto request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Updating employee {EmployeeId}", id);

                var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);

                if (employee == null)
                {
                    Log.Warning("Employee {EmployeeId} not found", id);
                    return BaseResponse<EmployeeDto>.FailResponse("Employee not found");
                }

                employee.FirstName = request.FirstName;
                employee.LastName = request.LastName;
                employee.OtherName = request.OtherName;
                employee.Gender = request.Gender;
                employee.PhoneNumber = request.PhoneNumber;
                employee.Email = request.Email;
                employee.Address = request.Address;
                employee.Title = request.Title;
                employee.DateOfBirth = request.DateOfBirth;
                employee.DepartmentId = request.Id;

                var updated = await _employeeRepository.UpdateAsync(employee, cancellationToken);

                var dto = new EmployeeDto
                {
                    EmployeeId = updated.EmployeeId,
                    FirstName = updated.FirstName,
                    LastName = updated.LastName,
                    OtherName = updated.OtherName,
                    Gender = updated.Gender,
                    PhoneNumber = updated.PhoneNumber,
                    Email = updated.Email,
                    Address = updated.Address,
                    Title = updated.Title,
                    DateOfBirth = updated.DateOfBirth,
                    Id = updated.DepartmentId
                };

                Log.Information("Employee {EmployeeId} updated successfully", id);
                return BaseResponse<EmployeeDto>.SuccessResponse(dto, "Employee updated successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error updating employee {EmployeeId}", id);
                return BaseResponse<EmployeeDto>.FailResponse($"Error: {ex.Message}");
            }
        }
    }
}
