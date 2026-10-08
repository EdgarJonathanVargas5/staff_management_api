using StaffManagement.Domain.Abstractions;

namespace StaffManagement.Domain.Entities;

public class Employee : BaseEntity
{
    public required string FirstName { get; set; } 
    public required string LastName { get; set; } 
    public required string Email { get; set; } 
    public string? PhoneNumber { get; set; }
    public DateTime HireDate { get; set; } = DateTime.UtcNow;
    public int TaxId { get; set; } 

    public int? JobCategoryId { get; set; }
    public int? DepartmentId { get; set; }


    public JobCategory? JobCategory { get; set; }
    public Department? Department { get; set; }
}
