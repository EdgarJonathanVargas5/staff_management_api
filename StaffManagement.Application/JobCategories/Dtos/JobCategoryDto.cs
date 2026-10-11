namespace StaffManagement.Application.JobCategories.Dtos;

public class JobCategoryDto
{
    public int Id { get; set; }
    public required string Description { get; set; } 
    public decimal? MinSalary { get; set; }
    public decimal? MaxSalary { get; set; }
}   
