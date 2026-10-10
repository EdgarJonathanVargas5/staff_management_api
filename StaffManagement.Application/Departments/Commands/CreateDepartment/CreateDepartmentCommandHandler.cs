using StaffManagement.Application.Departments.Dtos;

namespace StaffManagement.Application.Departments.Commands.CreateDepartment;

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result<DepartmentResponse>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Result<DepartmentResponse>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var departmentExists = await _departmentRepository.DepartmentExists(request.Name);
        if (departmentExists)
        {
            return Result<DepartmentResponse>.Failure(DepartmentErrors.DuplicatedName);
        }
        var department = request.Adapt<Department>();
        var isSuccess = await _departmentRepository.CreateDepartmentAsync(department, cancellationToken);
        if (!isSuccess)
        {
            return Result<DepartmentResponse>.Failure(DepartmentErrors.FailCreate);
        }
        var response = department.Adapt<DepartmentResponse>();
        return Result<DepartmentResponse>.Success(response);
    }
}
