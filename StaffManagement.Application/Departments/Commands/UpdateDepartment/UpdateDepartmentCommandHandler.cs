using System;

namespace StaffManagement.Application.Departments.Commands.UpdateDepartment;

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Result>
{
    private readonly IDepartmentRepository _departmentRepository;

    public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Result> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetDepartmentByIdAsync(request.Id);
        if(department is null)
        {
            return Result.Failure(DepartmentErrors.NotFound);
        }
        var departmentExists = await _departmentRepository.DepartmentExists(request.Name, request.Id);
        if (departmentExists)
        {
            return Result.Failure(DepartmentErrors.DuplicatedName);
        }
        
        department.Name = request.Name;

        var isSuccess = await _departmentRepository.UpdateDepartmentAsync(department, cancellationToken);
        if (!isSuccess)
        {
            return Result.Failure(DepartmentErrors.FailUpdate);
        }
        return Result.Success();
    }
}
