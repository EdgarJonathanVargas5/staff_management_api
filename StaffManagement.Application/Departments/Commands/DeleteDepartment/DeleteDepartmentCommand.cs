namespace StaffManagement.Application.Departments.Commands.DeleteDepartment;

public class DeleteDepartmentCommand : IRequest<Result>
{
    public int Id { get; set; }
}

public class DeleteDepartmentCommandValidator : AbstractValidator<DeleteDepartmentCommand>
{
    public DeleteDepartmentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El id es requerido.");
    }
}