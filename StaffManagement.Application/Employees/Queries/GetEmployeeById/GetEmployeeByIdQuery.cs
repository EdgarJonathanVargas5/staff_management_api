namespace StaffManagement.Application.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdQuery : IRequest<Result<EmployeeResponse>>
{
    public int Id { get; set; }
}
