using MediatR;
using StaffManagement.Application.Abstractions;
using StaffManagement.Application.Employees.Dtos;

namespace StaffManagement.Application.Employees.Queries.GetAllEmployees;

public class GetAllEmployeesQuery : IRequest<Result<List<EmployeeResponse>>>
{

}
