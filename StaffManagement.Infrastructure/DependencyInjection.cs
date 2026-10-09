using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StaffManagement.Domain.IRepositories;
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
        
        return services;
    }
}
