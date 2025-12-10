using EmployeeManagementProject.DTOs;
using EmployeeManagementProject.DTOs.Department;
using EmployeeManagementProject.DTOs.Employee;
using EmployeeManagementProject.Models;
using EmployeeManagementProject.Repositories.Interface;
using EmployeeManagementProject.Services.Interface;
using Serilog;

namespace EmployeeManagementProject.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<BaseResponse<DepartmentDto>> AddDepartmentAsync(CreateDepartmentDto request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Creating department: {Name}", request.Name);

                var dept = new Department
                {
                    Name = request.Name,
                    Description = request.Description  
                };
                await _departmentRepository.AddAsync(dept, cancellationToken);
                await _departmentRepository.SaveChangesAsync(cancellationToken);

                var dto = new DepartmentDto
                {
                    Id = dept.Id,
                    Name = dept.Name,
                    Description = dept.Description
                };

                Log.Information("Department {DepartmentId} created successfully", dept.Id);
                return BaseResponse<DepartmentDto>.SuccessResponse(dto, "Department created successfully");
            }
            //catch (Exception ex)
            //{
            //    Log.Error(ex, "Error creating department");
            //    return BaseResponse<DepartmentDto>.FailResponse("An error occurred while creating department");
            //}
            catch (Exception ex)
            {
                Log.Error(ex, "Error creating department");

                // TEMPORARY: show the real issue
                return BaseResponse<DepartmentDto>.FailResponse(ex.Message);
            }
        }

        public async Task<BaseResponse<bool>> DeleteDepartmentAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Deleting department {DepartmentId}", id);

                var result = await _departmentRepository.DeleteAsync(id, cancellationToken);

                if (!result)
                {
                    Log.Warning("Department with ID {DepartmentId} not found", id);
                    return BaseResponse<bool>.FailResponse("Department not found");
                }

                Log.Information("Department {DepartmentId} deleted", id);
                return BaseResponse<bool>.SuccessResponse(true, "Department deleted successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error deleting department {DepartmentId}", id);
                return BaseResponse<bool>.FailResponse($"Error: {ex.Message}");
            }
        }

        public async Task<BaseResponse<IEnumerable<DepartmentDto>>> GetAllDepartmentsAsync(CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Fetching all departments");

                var departments = await _departmentRepository.GetAllAsync(cancellationToken);

                var dtoList = departments.Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description
                });

                return BaseResponse<IEnumerable<DepartmentDto>>
                    .SuccessResponse(dtoList, "Departments retrieved successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching all departments");
                return BaseResponse<IEnumerable<DepartmentDto>>
                    .FailResponse("An error occurred while retrieving departments");
            }
        }

        public async Task<BaseResponse<DepartmentDto?>> GetDepartmentByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Fetching department with ID {DepartmentId}", id);

                var dept = await _departmentRepository.GetByIdAsync(id, cancellationToken);

                if (dept == null)
                {
                    return BaseResponse<DepartmentDto?>.FailResponse("Department not found");
                }

                var dto = new DepartmentDto
                {
                    Id = dept.Id,
                    Name = dept.Name,
                    Description = dept.Description,
                    Employees = dept.Employees.Select(e => new EmployeeDto
                    {
                        EmployeeId = e.EmployeeId,
                        FullName = e.FullName,
                        Email = e.Email,
                        PhoneNumber = e.PhoneNumber
                    }).ToList()
                };

                return BaseResponse<DepartmentDto?>.SuccessResponse(dto, "Department retrieved successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error retrieving department {DepartmentId}", id);
                return BaseResponse<DepartmentDto?>.FailResponse($"Error: {ex.Message}");
            }
        }

        public async Task<BaseResponse<DepartmentDto?>> UpdateDepartmentAsync(Guid id, UpdateDepartmentDto request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Updating department {DepartmentId}", id);

                var dept = await _departmentRepository.GetByIdAsync(id, cancellationToken);

                if (dept == null)
                {
                    Log.Warning("Department {DepartmentId} not found", id);
                    return BaseResponse<DepartmentDto>.FailResponse("Department not found");
                }

                dept.Name = request.Name;
                dept.Description = request.Description;

                await _departmentRepository.UpdateAsync(dept, cancellationToken);
                await _departmentRepository.SaveChangesAsync(cancellationToken);

                var dto = new DepartmentDto
                {
                    Id = dept.Id,
                    Name = dept.Name,
                    Description = dept.Description
                };

                Log.Information("Department {DepartmentId} updated successfully", id);
                return BaseResponse<DepartmentDto>.SuccessResponse(dto, "Department updated");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error updating department {DepartmentId}", id);
                return BaseResponse<DepartmentDto>.FailResponse($"Error: {ex.Message}");
            }
        }
    }
}
