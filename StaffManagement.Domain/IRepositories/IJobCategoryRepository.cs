using StaffManagement.Domain.Entities;

namespace StaffManagement.Domain.IRepositories;

public interface IJobCategoryRepository
{
    Task<ICollection<JobCategory>> GetAllJobCategoriesAsync();
    Task<JobCategory?> GetJobCategoryById(int id);
}
