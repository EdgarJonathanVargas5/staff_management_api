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

    public async Task CreateEmployeeAsync(Employee employee, CancellationToken cancellationToken)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<bool> EmployeeExists(int taxId)
    {
        return await _context.Employees.AnyAsync(e => e.TaxId == taxId);
    }

    
}
