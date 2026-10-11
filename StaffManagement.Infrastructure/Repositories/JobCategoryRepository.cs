using Microsoft.EntityFrameworkCore;
using StaffManagement.Domain.Entities;
using StaffManagement.Domain.IRepositories;

namespace StaffManagement.Infrastructure.Repositories;

public class JobCategoryRepository : IJobCategoryRepository
{
    private readonly ApplicationDbContext _context;

    public JobCategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<JobCategory>> GetAllJobCategoriesAsync()
    {
        return await _context.JobCategories.OrderBy(jc => jc.Id).ToListAsync();
    }

    public async Task<JobCategory?> GetJobCategoryById(int id)
    {
        return await _context.JobCategories.FindAsync(id);
    }
}
