using StaffManagement.Domain.Entities;

namespace StaffManagement.Domain.IRepositories;

public interface IDepartmentRepository
{
    Task<ICollection<Department>> GetAllDepartmentsAsync();
    Task<Department?> GetDepartmentByIdAsync(int id);
    Task<bool> CreateDepartmentAsync(Department department, CancellationToken cancellationToken);
    Task<bool> UpdateDepartmentAsync(Department department, CancellationToken cancellationToken);
    Task<bool> DeleteDepartmentAsync(Department department, CancellationToken cancellationToken);
    Task<bool> DepartmentExists(string departmentName, int? excludingDepartmentId = null);
    Task<bool> SaveAsync(CancellationToken cancellationToken);
}
