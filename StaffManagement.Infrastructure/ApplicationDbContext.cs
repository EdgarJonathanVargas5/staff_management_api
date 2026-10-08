using Microsoft.EntityFrameworkCore;
using StaffManagement.Domain.Entities;

namespace StaffManagement.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {}
    
    public DbSet<Department> Departments { get; set; }
    public DbSet<JobCategory> JobCategories { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Department configuration
        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("DEPARTMENTS");

            entity.HasKey(d => d.Id);
            entity.Property(d => d.Id)
                .HasColumnName("DEPARTMENT_ID")
                .ValueGeneratedOnAdd();

            entity.Property(d => d.Name)
                .HasColumnName("NAME")
                .HasMaxLength(30)
                .IsRequired(false);
        });

        // JobCategory configuration
        modelBuilder.Entity<JobCategory>(entity =>
        {
            entity.ToTable("JOB_CATEGORIES");

            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id)
                .HasColumnName("CATEGORY_ID")
                .ValueGeneratedOnAdd();

            entity.Property(c => c.Description)
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(60)
                .IsRequired();

            entity.Property(c => c.MinSalary)
                .HasColumnName("MIN_SALARY")
                .HasPrecision(10, 2);

            entity.Property(c => c.MaxSalary)
                .HasColumnName("MAX_SALARY")
                .HasPrecision(10, 2);
        });

        // Employee configuration & relationships
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("EMPLOYEES");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasColumnName("EMPLOYEE_ID")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.FirstName)
                .HasColumnName("FIRST_NAME")
                .HasMaxLength(20)
                .IsRequired(false);

            entity.Property(e => e.LastName)
                .HasColumnName("LAST_NAME")
                .HasMaxLength(25)
                .IsRequired(false);

            entity.Property(e => e.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(25)
                .IsRequired(false);

            entity.Property(e => e.PhoneNumber)
                .HasColumnName("PHONE_NUMBER")
                .HasMaxLength(20)
                .IsRequired(false);

            entity.Property(e => e.HireDate)
                .HasColumnName("HIRE_DATE");

            entity.Property(e => e.TaxId)
                .HasColumnName("TAX_ID"); 

            entity.Property(e => e.JobCategoryId)
                .HasColumnName("JOB_CATEGORY_ID");

            entity.Property(e => e.DepartmentId)
                .HasColumnName("DEPARTMENT_ID");

            // Relationships
            entity.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.JobCategory)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.JobCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
