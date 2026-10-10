namespace StaffManagement.Application.Departments.Commands.UpdateDepartment;

public class UpdateDepartmentCommand : IRequest<Result>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}

public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El id es requerido.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del departamento es requerido.");
    }
}
