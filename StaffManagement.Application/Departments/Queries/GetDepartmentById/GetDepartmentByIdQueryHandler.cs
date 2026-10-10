using StaffManagement.Application.Departments.Dtos;

namespace StaffManagement.Application.Departments.Queries.GetDepartmentById;

public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, Result<DepartmentResponse>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public GetDepartmentByIdQueryHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Result<DepartmentResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetDepartmentByIdAsync(request.Id);
        if(department is null)
        {
            return Result<DepartmentResponse>.Failure(DepartmentErrors.NotFound);
        }
        var departmentResponse = department.Adapt<DepartmentResponse>();
        return Result<DepartmentResponse>.Success(departmentResponse);
    }
}
