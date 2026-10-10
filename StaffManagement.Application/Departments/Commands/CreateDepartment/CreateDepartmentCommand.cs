using StaffManagement.Application.Departments.Dtos;

namespace StaffManagement.Application.Departments.Commands.CreateDepartment;

public class CreateDepartmentCommand : IRequest<Result<DepartmentResponse>>
{
    public required string Name { get; set; }
}

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del departamento es requerido.");
    }
}
