using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StaffManagement.Application.Abstractions;
using StaffManagement.Domain.IRepositories;
using StaffManagement.Infrastructure.Authentication;
using StaffManagement.Infrastructure.Data;
using StaffManagement.Infrastructure.Identity;
using StaffManagement.Infrastructure.Repositories;

namespace StaffManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseOracle(configuration.GetConnectionString("OracleDbConnection"));
        });

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
        
        AddAuthentication(services);

        return services;
    }

    public static void AddAuthentication(IServiceCollection services)
    {
        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<DatabaseSeeder>();
    }
}
