using StaffManagement.Domain.Entities;

namespace StaffManagement.Domain.IRepositories;

public interface IEmployeeRepository
{
    Task<ICollection<Employee>> GetAllEmployeesAsync();
}
