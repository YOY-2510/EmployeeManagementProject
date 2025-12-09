using EmployeeManagementProject.Data;
using EmployeeManagementProject.Models;
using EmployeeManagementProject.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementProject.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<Employee> AddAsync(Employee employee, CancellationToken cancellationtoken)
        {
            await _dbContext.Employees.AddAsync(employee, cancellationtoken);
            await _dbContext.SaveChangesAsync(cancellationtoken);
            return employee;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationtoken)
        {
            var emp = await _dbContext.Employees.FindAsync(new object[] { id }, cancellationtoken);
            if (emp == null) 
                return false;

            _dbContext.Employees.Remove(emp);
            await _dbContext.SaveChangesAsync(cancellationtoken);
            return true;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationtoken)
        {
            return await _dbContext.Employees
                .Include(emp => emp.Department)
                .ToListAsync(cancellationtoken);
        }
        public async Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationtoken)
        {
            return await _dbContext.Employees
                .Include(emp => emp.Department)
                .FirstOrDefaultAsync(emp => emp.EmployeeId == id, cancellationtoken);
        }
        public async Task<Employee?> UpdateAsync(Employee employee, CancellationToken cancellationtoken)
        {
            var existing = await _dbContext.Employees.FindAsync(new object[] { employee.EmployeeId }, cancellationtoken);
            if (existing == null) 
                return null;

            existing.FullName = employee.FullName;
            existing.Email = employee.Email;
            existing.DepartmentId = employee.DepartmentId;

            _dbContext.Employees.Update(existing);
            await _dbContext.SaveChangesAsync(cancellationtoken);
            return existing;
        }
    }
}
