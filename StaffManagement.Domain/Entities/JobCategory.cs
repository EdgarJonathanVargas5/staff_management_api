using StaffManagement.Domain.Abstractions;

namespace StaffManagement.Domain.Entities;

public class JobCategory : BaseEntity
{
    public string? Description { get; set; } 
    public decimal? MinSalary { get; set; }
    public decimal? MaxSalary { get; set; }

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
