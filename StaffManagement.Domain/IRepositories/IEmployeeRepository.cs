using StaffManagement.Domain.Entities;

namespace StaffManagement.Domain.IRepositories;

public interface IEmployeeRepository
{
    Task<bool> EmployeeExists(int taxId, int? excludingEmployeeId = null);
    Task<bool> EmailExists(string email, int? excludingEmployeeId = null);
    Task<ICollection<Employee>> GetAllEmployeesAsync();
    Task<Employee?> GetEmployeeByIdAsync(int employeeId);
    Task<bool> CreateEmployeeAsync(Employee employee, CancellationToken cancellationToken);
    Task<bool> UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken);
}
