using StaffManagement.Application.JobCategories.Dtos;

namespace StaffManagement.Application.JobCategories.Queries.GetJobCategoryById;

public class GetJobCategoryByIdQuery : IRequest<Result<JobCategoryDto>>
{
    public int Id { get; set; }
}
