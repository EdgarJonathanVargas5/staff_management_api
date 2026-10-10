namespace StaffManagement.Application.Departments.Commands.DeleteDepartment;

public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Result>
{
    private readonly IDepartmentRepository _departmentRepository;

    public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Result> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetDepartmentByIdAsync(request.Id);
        if(department is null)
        {
            return Result.Failure(DepartmentErrors.NotFound);
        }
        var isSuccess = await _departmentRepository.DeleteDepartmentAsync(department, cancellationToken);
        if (!isSuccess)
        {
            return Result.Failure(DepartmentErrors.FailDelete);
        }
        return Result.Success();
    }
}
