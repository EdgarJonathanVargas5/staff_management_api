namespace StaffManagement.Application.Employees.Dtos;

public sealed record EmployeeResponse(
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber,
    DateTime HireDate,
    int TaxId
);

