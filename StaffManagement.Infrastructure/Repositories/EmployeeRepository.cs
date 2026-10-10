using Microsoft.EntityFrameworkCore;
using StaffManagement.Domain.Entities;
using StaffManagement.Domain.IRepositories;

namespace StaffManagement.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Employee>> GetAllEmployeesAsync()
    {
        return await _context.Employees.OrderBy(e => e.Id).ToListAsync();
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
    {
        return await _context.Employees.FindAsync(employeeId);
    }

    public async Task<bool> CreateEmployeeAsync(Employee employee, CancellationToken cancellationToken)
    {
        _context.Employees.Add(employee);
        return await SaveAsync(cancellationToken);
    }

    public async Task<bool> UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken)
    {
        _context.Employees.Update(employee);
        return await SaveAsync(cancellationToken);
    }

    public async Task<bool> DeleteEmployeeAsync(Employee employee, CancellationToken cancellationToken)
    {
        _context.Employees.Remove(employee);
        return await SaveAsync(cancellationToken);
    }
    
    public async Task<bool> EmployeeExists(int taxId, int? excludingEmployeeId = null)
    {
        return await _context.Employees.AnyAsync(e =>
                            e.TaxId == taxId &&
                            (excludingEmployeeId == null || e.Id != excludingEmployeeId));
    }

    public async Task<bool> EmailExists(string email, int? excludingEmployeeId = null)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();

        return await _context.Employees.AnyAsync(e =>
                            e.Email == normalizedEmail &&
                            (excludingEmployeeId == null || e.Id != excludingEmployeeId));
    }

    public async Task<bool> SaveAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
