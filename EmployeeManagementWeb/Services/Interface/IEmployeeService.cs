using EmployeeManagementProject.DTOs;
using EmployeeManagementProject.DTOs.Employee;
using EmployeeManagementProject.Models;

namespace EmployeeManagementProject.Services.Interface
{
    public interface IEmployeeService
    {
        Task<BaseResponse<IEnumerable<EmployeeDto>>> GetAllEmployeesAsync(CancellationToken cancellationToken);
        Task<BaseResponse<EmployeeDto>> GetEmployeeByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<BaseResponse<EmployeeDto>> AddEmployeeAsync(CreateEmployeeDto request, CancellationToken cancellationToken);
        Task<BaseResponse<EmployeeDto>> UpdateEmployeeAsync(Guid id, CreateEmployeeDto request, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken);
    }
}
