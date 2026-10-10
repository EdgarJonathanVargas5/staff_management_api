using Microsoft.EntityFrameworkCore;
using StaffManagement.Domain.Entities;
using StaffManagement.Domain.IRepositories;

namespace StaffManagement.Infrastructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _context;

    public DepartmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Department>> GetAllDepartmentsAsync()
    {
        return await _context.Departments.OrderBy(d => d.Id).ToListAsync();
    }

    public async Task<Department?> GetDepartmentByIdAsync(int id)
    {
        return await _context.Departments.FindAsync(id);
    }

    public async Task<bool> CreateDepartmentAsync(Department department, CancellationToken cancellationToken)
    {
        _context.Departments.Add(department);
        return await SaveAsync(cancellationToken);
    }

    public async Task<bool> UpdateDepartmentAsync(Department department, CancellationToken cancellationToken)
    {
        _context.Departments.Update(department);
        return await SaveAsync(cancellationToken);
    }

    public async Task<bool> DeleteDepartmentAsync(Department department, CancellationToken cancellationToken)
    {
        _context.Departments.Remove(department);
        return await SaveAsync(cancellationToken);
    }
    
    public async Task<bool> DepartmentExists(string departmentName, int? excludingDepartmentId = null)
    {
        var normalizedName = departmentName.Trim().ToLowerInvariant();
        return await _context.Departments.AnyAsync(d => 
                            d.Name == normalizedName  &&
                           (excludingDepartmentId == null || d.Id != excludingDepartmentId));
    }

    public async Task<bool> SaveAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
