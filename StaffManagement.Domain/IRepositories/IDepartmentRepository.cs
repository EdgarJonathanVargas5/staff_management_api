using StaffManagement.Domain.Entities;

namespace StaffManagement.Domain.IRepositories;

public interface IDepartmentRepository
{
    Task<ICollection<Department>> GetAllDepartmentsAsync();
    Task<Department?> GetDepartmentByIdAsync(int id);
}
