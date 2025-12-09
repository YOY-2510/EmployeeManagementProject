using EmployeeManagementProject.Models;

namespace EmployeeManagementProject.Repositories.Interface
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync(CancellationToken cancellationtoken);
        Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationtoken);
        Task<Department> AddAsync(Department department, CancellationToken cancellationtoken);
        Task<Department?> UpdateAsync(Department department, CancellationToken cancellationtoken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationtoken);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
