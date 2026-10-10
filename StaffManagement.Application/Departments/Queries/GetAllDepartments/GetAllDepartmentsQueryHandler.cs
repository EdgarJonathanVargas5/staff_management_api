using StaffManagement.Application.Departments.Dtos;

namespace StaffManagement.Application.Departments.Queries.GetAllDepartments;

public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, Result<List<DepartmentResponse>>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public GetAllDepartmentsQueryHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Result<List<DepartmentResponse>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var departments = await _departmentRepository.GetAllDepartmentsAsync();
        var response = departments.Adapt<List<DepartmentResponse>>();
        return Result<List<DepartmentResponse>>.Success(response);
    }
}
