using StaffManagement.Domain.Entities;

namespace StaffManagement.Domain.IRepositories;

public interface IEmployeeRepository
{
    Task<bool> EmployeeExists(int taxId);
    Task<ICollection<Employee>> GetAllEmployeesAsync();
    Task<Employee?> GetEmployeeByIdAsync(int employeeId);
    Task CreateEmployeeAsync(Employee employee, CancellationToken cancellationToken);
}
