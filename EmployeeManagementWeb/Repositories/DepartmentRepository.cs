using EmployeeManagementProject.Data;
using EmployeeManagementProject.Models;
using EmployeeManagementProject.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementProject.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Department> AddAsync(Department department, CancellationToken cancellationtoken)
        {
            await _dbContext.Departments.AddAsync(department, cancellationtoken);
            await _dbContext.SaveChangesAsync(cancellationtoken);
            return department;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationtoken)
        {
            var dept = await _dbContext.Departments.FindAsync(new object[] { id }, cancellationtoken);
            if (dept == null) return false;

            _dbContext.Departments.Remove(dept);
            await _dbContext.SaveChangesAsync(cancellationtoken);
            return true;
        }

        public async Task<IEnumerable<Department>> GetAllAsync(CancellationToken cancellationtoken)
        {
            return await _dbContext.Departments
                .Include(d => d.Employees)
                .ToListAsync(cancellationtoken);
        }

        public async Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationtoken)
        {
            return await _dbContext.Departments
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.Id == id, cancellationtoken);

        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Department?> UpdateAsync(Department department, CancellationToken cancellationtoken)
        {
            var existing = await _dbContext.Departments.FindAsync(new object[] { department.Id }, cancellationtoken);
            if (existing == null) return null;

            existing.Name = department.Name;

            _dbContext.Departments.Update(existing);
            await _dbContext.SaveChangesAsync(cancellationtoken);
            return existing;
        }
    }
}
