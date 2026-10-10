using System;

namespace StaffManagement.Application.Employees.Dtos;

public class UpdateEmployeeRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public required int TaxId { get; set; }
}
