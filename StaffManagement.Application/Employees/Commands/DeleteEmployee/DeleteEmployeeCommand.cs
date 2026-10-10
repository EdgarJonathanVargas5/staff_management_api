namespace StaffManagement.Application.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommand : IRequest<Result>
{
    public int Id { get; set; }
}

public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El id es requerido.");
    }
}