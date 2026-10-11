using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StaffManagement.Domain.Entities;
using StaffManagement.Infrastructure.Identity;

namespace StaffManagement.Infrastructure.Data;

public sealed class DatabaseSeeder(
    ApplicationDbContext db,
    RoleManager<IdentityRole> roleManager,
    UserManager<ApplicationUser> userManager,
    IConfiguration configuration)
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        await EnsureRoleAsync("Admin");
        await EnsureRoleAsync("User");

        await EnsureUserAsync(
            sectionName: "Seed:Admin",
            roleName: "Admin");

        await EnsureUserAsync(
            sectionName: "Seed:User",
            roleName: "User");

        var departmentNames = new[]
        {
            "Human Resources",
            "Finance",
            "Engineering",
            "Operations",
            "Sales"
        };

        foreach (var name in departmentNames)
        {
            if (!await db.Departments.AnyAsync(
                    department => department.Name == name,
                    cancellationToken))
            {
                db.Departments.Add(new Department { Name = name });
            }
        }

        var categories = new[]
        {
            new JobCategory
            {
                Description = "Administrative",
                MinSalary = 30000,
                MaxSalary = 50000
            },
            new JobCategory
            {
                Description = "Financial Analyst",
                MinSalary = 45000,
                MaxSalary = 75000
            },
            new JobCategory
            {
                Description = "Software Engineer",
                MinSalary = 60000,
                MaxSalary = 120000
            },
            new JobCategory
            {
                Description = "Operations Specialist",
                MinSalary = 35000,
                MaxSalary = 65000
            },
            new JobCategory
            {
                Description = "Sales Representative",
                MinSalary = 35000,
                MaxSalary = 80000
            }
        };

        foreach (var category in categories)
        {
            if (!await db.JobCategories.AnyAsync(
                    existing => existing.Description == category.Description,
                    cancellationToken))
            {
                db.JobCategories.Add(category);
            }
        }

        await db.SaveChangesAsync(cancellationToken);

        var departmentsByName = await db.Departments
            .Where(department => departmentNames.Contains(department.Name))
            .ToDictionaryAsync(
                department => department.Name,
                department => department.Id,
                cancellationToken);

        var categoriesByDescription = await db.JobCategories
            .Where(category => categories.Select(c => c.Description)
                .Contains(category.Description))
            .ToDictionaryAsync(
                category => category.Description!,
                category => category.Id,
                cancellationToken);

        var employees = new[]
        {
            ("Ana", "Lopez", "ana.lopez@example.test", 100001, "Human Resources", "Administrative"),
            ("Luis", "Garcia", "luis.garcia@example.test", 100002, "Finance", "Financial Analyst"),
            ("Marta", "Perez", "marta.perez@example.test", 100003, "Engineering", "Software Engineer"),
            ("Carlos", "Ruiz", "carlos.ruiz@example.test", 100004, "Operations", "Operations Specialist"),
            ("Elena", "Diaz", "elena.diaz@example.test", 100005, "Sales", "Sales Representative"),
            ("Diego", "Torres", "diego.torres@example.test", 100006, "Engineering", "Software Engineer"),
            ("Sofia", "Romero", "sofia.romero@example.test", 100007, "Finance", "Financial Analyst"),
            ("Pablo", "Navarro", "pablo.navarro@example.test", 100008, "Operations", "Operations Specialist"),
            ("Laura", "Castro", "laura.castro@example.test", 100009, "Human Resources", "Administrative"),
            ("Jorge", "Morales", "jorge.morales@example.test", 100010, "Sales", "Sales Representative")
        };

        foreach (var employee in employees)
        {
            var (firstName, lastName, email, taxId, departmentName, categoryDescription) = employee;

            if (await db.Employees.AnyAsync(
                    existing => existing.Email == email,
                    cancellationToken))
            {
                continue;
            }

            db.Employees.Add(new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                TaxId = taxId,
                HireDate = DateTime.UtcNow,
                DepartmentId = departmentsByName[departmentName],
                JobCategoryId = categoriesByDescription[categoryDescription]
            });
        }

        await db.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureRoleAsync(string roleName)
    {
        if (await roleManager.RoleExistsAsync(roleName))
        {
            return;
        }

        var result = await roleManager.CreateAsync(new IdentityRole(roleName));

        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(error => error.Description));
            throw new InvalidOperationException(
                $"No se pudo crear el rol '{roleName}': {errors}");
        }
    }

    private async Task EnsureUserAsync(string sectionName, string roleName)
    {
        var section = configuration.GetSection(sectionName);
        var email = section["Email"];
        var password = section["Password"];
        var fullName = section["FullName"];

        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(fullName))
        {
            throw new InvalidOperationException(
                $"Falta configurar Email, Password o FullName para '{sectionName}'.");
        }

        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName
            };

            var createResult = await userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
            {
                var errors = string.Join(
                    "; ",
                    createResult.Errors.Select(error => error.Description));

                throw new InvalidOperationException(
                    $"No se pudo crear el usuario '{email}': {errors}");
            }
        }

        if (!await userManager.IsInRoleAsync(user, roleName))
        {
            var roleResult = await userManager.AddToRoleAsync(user, roleName);

            if (!roleResult.Succeeded)
            {
                var errors = string.Join(
                    "; ",
                    roleResult.Errors.Select(error => error.Description));

                throw new InvalidOperationException(
                    $"No se pudo asignar el rol '{roleName}' a '{email}': {errors}");
            }
        }
    }
}
