using StaffManagement.Application.Departments.Dtos;

namespace StaffManagement.Application.Departments.Queries.GetDepartmentById;

public class GetDepartmentByIdQuery : IRequest<Result<DepartmentResponse>>
{
    public int Id { get; set; }
}
