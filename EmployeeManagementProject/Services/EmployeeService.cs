using Azure.Core;
using EmployeeManagementProject.DTOs;
using EmployeeManagementProject.DTOs.Employee;
using EmployeeManagementProject.Models;
using EmployeeManagementProject.Repositories.Interface;
using EmployeeManagementProject.Services.Interface;
using Microsoft.EntityFrameworkCore;
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
                Log.Information("Creating a new employee: {FullName}", request.FullName);

                var employee = new Employee
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    DepartmentId = request.DepartmentId
                };

                var added = await _employeeRepository.AddAsync(employee, cancellationToken);

                var response = new EmployeeDto
                {
                    EmployeeId = employee.EmployeeId,
                    FullName = employee.FullName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    DepartmentId = employee.DepartmentId
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
                    FullName = emp.FullName,
                    Email = emp.Email,
                    PhoneNumber = emp.PhoneNumber,
                    DepartmentId = emp.DepartmentId
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
                    FullName = emp.FullName,
                    Email = emp.Email,
                    PhoneNumber = emp.PhoneNumber,
                    DepartmentId = emp.DepartmentId
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

                employee.FullName = request.FullName;
                employee.Email = request.Email;
                employee.PhoneNumber = request.PhoneNumber;
                employee.DepartmentId = request.DepartmentId;

                var updated = await _employeeRepository.UpdateAsync(employee, cancellationToken);

                var dto = new EmployeeDto
                {
                    EmployeeId = updated.EmployeeId,
                    FullName = updated.FullName,
                    Email = updated.Email,
                    PhoneNumber = updated.PhoneNumber,
                    DepartmentId = updated.DepartmentId
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
