using StaffManagement.Application.Departments.Dtos;

namespace StaffManagement.Application.Departments.Queries.GetAllDepartments;

public class GetAllDepartmentsQuery : IRequest<Result<List<DepartmentResponse>>>
{

}
