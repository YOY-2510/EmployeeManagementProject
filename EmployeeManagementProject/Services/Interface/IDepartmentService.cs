using EmployeeManagementProject.DTOs;
using EmployeeManagementProject.DTOs.Department;
using EmployeeManagementProject.Models;

namespace EmployeeManagementProject.Services.Interface
{
    public interface IDepartmentService
    {
        Task<BaseResponse<IEnumerable<DepartmentDto>>> GetAllDepartmentsAsync(CancellationToken cancellationToken);
        Task<BaseResponse<DepartmentDto?>> GetDepartmentByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<BaseResponse<DepartmentDto>> AddDepartmentAsync(CreateDepartmentDto request, CancellationToken cancellationToken);
        Task<BaseResponse<DepartmentDto?>> UpdateDepartmentAsync(Guid id, UpdateDepartmentDto request, CancellationToken cancellationToken );
        Task<BaseResponse<bool>> DeleteDepartmentAsync(Guid id, CancellationToken cancellationToken);
    }
}
