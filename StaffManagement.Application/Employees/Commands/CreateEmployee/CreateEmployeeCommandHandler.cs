namespace StaffManagement.Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<EmployeeResponse>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<EmployeeResponse>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employeeExists = await _employeeRepository.EmployeeExists(request.TaxId);
        if (employeeExists)
        {
            return Result<EmployeeResponse>.Failure(EmployeeErrors.DuplicatedTaxId);
        }
        var employee = request.Adapt<Employee>();
        await _employeeRepository.CreateEmployeeAsync(employee, cancellationToken);
        var response = employee.Adapt<EmployeeResponse>();
        return Result<EmployeeResponse>.Success(response);
    }
}
