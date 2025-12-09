using EmployeeManagementProject.Models;

namespace EmployeeManagementProject.Repositories.Interface
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationtoken);
        Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationtoken);
        Task<Employee> AddAsync(Employee employee, CancellationToken cancellationtoken);
        Task<Employee?> UpdateAsync(Employee employee, CancellationToken cancellationtoken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationtoken);
    }
}
