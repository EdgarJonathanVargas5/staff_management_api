using StaffManagement.Application.JobCategories.Dtos;

namespace StaffManagement.Application.JobCategories.Queries.GetAllJobCategories;

public class GetAllJobCategoriesQueryHandler : IRequestHandler<GetAllJobCategoriesQuery, Result<List<JobCategoryDto>>>
{
    private readonly IJobCategoryRepository _jobCategoryRepository;

    public GetAllJobCategoriesQueryHandler(IJobCategoryRepository jobCategoryRepository)
    {
        _jobCategoryRepository = jobCategoryRepository;
    }

    public async Task<Result<List<JobCategoryDto>>> Handle(GetAllJobCategoriesQuery request, CancellationToken cancellationToken)
    {
        var jobCategories = await _jobCategoryRepository.GetAllJobCategoriesAsync();
        var response = jobCategories.Adapt<List<JobCategoryDto>>();
        return Result<List<JobCategoryDto>>.Success(response);
    }
}
