using StaffManagement.Domain.Entities;

namespace StaffManagement.Domain.IRepositories;

public interface IEmployeeRepository
{
    Task<ICollection<Employee>> GetAllEmployeesAsync();
    Task<Employee?> GetEmployeeByIdAsync(int employeeId);
    Task<bool> CreateEmployeeAsync(Employee employee, CancellationToken cancellationToken);
    Task<bool> UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken);
    Task<bool> DeleteEmployeeAsync(Employee employee, CancellationToken cancellationToken);
    Task<bool> EmployeeExists(int taxId, int? excludingEmployeeId = null);
    Task<bool> EmailExists(string email, int? excludingEmployeeId = null);
}
