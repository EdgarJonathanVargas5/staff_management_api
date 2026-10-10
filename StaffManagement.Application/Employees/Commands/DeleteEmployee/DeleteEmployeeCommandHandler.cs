namespace StaffManagement.Application.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Result>
{
    private readonly IEmployeeRepository _employeeRepository;

    public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(request.Id);
        if(employee is null)
        {
            return Result.Failure(EmployeeErrors.NotFound);
        }
        var isSuccess = await _employeeRepository.DeleteEmployeeAsync(employee, cancellationToken);
        if (!isSuccess)
        {
            return Result.Failure(EmployeeErrors.FailDelete);
        }
        return Result.Success();
    }
}
