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
        var emailExists = await _employeeRepository.EmailExists(request.Email);
        if(emailExists)
        {
            return Result<EmployeeResponse>.Failure(EmployeeErrors.DuplicatedEmail);
        }
        var employee = request.Adapt<Employee>();

        var isSuccess = await _employeeRepository.CreateEmployeeAsync(employee, cancellationToken);
        if (!isSuccess)
        {
            return Result<EmployeeResponse>.Failure(EmployeeErrors.FailCreate);
        }
        var response = employee.Adapt<EmployeeResponse>();

        return Result<EmployeeResponse>.Success(response);
    }
}
