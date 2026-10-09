namespace StaffManagement.Application.Employees.Dtos;

public sealed record EmployeeResponse(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber
);

