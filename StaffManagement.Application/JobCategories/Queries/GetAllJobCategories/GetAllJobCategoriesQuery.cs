using StaffManagement.Application.JobCategories.Dtos;

namespace StaffManagement.Application.JobCategories.Queries.GetAllJobCategories;

public class GetAllJobCategoriesQuery : IRequest<Result<List<JobCategoryDto>>>
{

}
