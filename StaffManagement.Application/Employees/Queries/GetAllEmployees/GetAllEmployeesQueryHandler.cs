using Mapster;
using MediatR;
using StaffManagement.Application.Abstractions;
using StaffManagement.Application.Employees.Dtos;
using StaffManagement.Domain.IRepositories;

namespace StaffManagement.Application.Employees.Queries.GetAllEmployees;

public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, Result<List<EmployeeResponse>>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<List<EmployeeResponse>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.GetAllEmployeesAsync();
        var response = employees.Adapt<List<EmployeeResponse>>();
        
        return Result<List<EmployeeResponse>>.Success(response);
    }
}
