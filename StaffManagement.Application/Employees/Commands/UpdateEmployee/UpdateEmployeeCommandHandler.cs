namespace StaffManagement.Application.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Result>
{
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(request.Id);
        if(employee is null)
        {
            return Result.Failure(EmployeeErrors.NotFound);
        }
        if(await _employeeRepository.EmployeeExists(request.TaxId, request.Id))
        {
            return Result.Failure(EmployeeErrors.DuplicatedTaxId);
        }
        if(await _employeeRepository.EmailExists(request.Email, request.Id))
        {
            return Result.Failure(EmployeeErrors.DuplicatedEmail);
        }
        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Email = request.Email;
        employee.TaxId = request.TaxId;
        employee.PhoneNumber = request.PhoneNumber;
        var isSuccess = await _employeeRepository.UpdateEmployeeAsync(employee, cancellationToken);
        if (isSuccess)
        {
            return Result.Success();
        }
        return Result.Failure(EmployeeErrors.FailUpdate);
    }
}
