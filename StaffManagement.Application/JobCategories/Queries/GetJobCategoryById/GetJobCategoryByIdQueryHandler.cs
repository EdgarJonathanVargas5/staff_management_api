using StaffManagement.Application.JobCategories.Dtos;

namespace StaffManagement.Application.JobCategories.Queries.GetJobCategoryById;

public class GetJobCategoryByIdQueryHandler : IRequestHandler<GetJobCategoryByIdQuery, Result<JobCategoryDto>>
{
    private readonly IJobCategoryRepository _jobCategoryRepository;

    public GetJobCategoryByIdQueryHandler(IJobCategoryRepository jobCategoryRepository)
    {
        _jobCategoryRepository = jobCategoryRepository;
    }

    public async Task<Result<JobCategoryDto>> Handle(GetJobCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var jobCategory = await _jobCategoryRepository.GetJobCategoryById(request.Id);
        if(jobCategory is null)
        {
            return Result<JobCategoryDto>.Failure(JobCategoryErrors.NotFound);
        }
        var response = jobCategory.Adapt<JobCategoryDto>();
        return Result<JobCategoryDto>.Success(response);
    }
}
