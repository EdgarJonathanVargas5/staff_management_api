namespace StaffManagement.Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommand : IRequest<Result<EmployeeResponse>>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public required int TaxId { get; set; }
}

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(20).WithMessage("El nombre no puede superar los 20 caracteres.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("El apellido es obligatorio.")
            .MaximumLength(25).WithMessage("El apellido no puede superar los 25 caracteres.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("El email no tiene un formato válido.")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.TaxId)
            .GreaterThan(0).WithMessage("El TaxId debe ser mayor que cero.");
    }
}
