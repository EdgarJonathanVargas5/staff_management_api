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
}
