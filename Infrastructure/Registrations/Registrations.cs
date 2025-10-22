using CQRS_App.Domain.Interfaces;
using CQRS_App.Infrastructure.Data;
using CQRS_App.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS_App.Infrastructure.Registrations;

public static class Registrations
{
    public static void AddInfrastructureRegistrations(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DBcontext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnString"))
                   .EnableSensitiveDataLogging()
                   .EnableDetailedErrors();
        });

        builder.Services.AddScoped<IReadDbContext>(sp =>
        {
            var baseOptions = sp.GetRequiredService<DbContextOptions<DBcontext>>();

            // Build a new instance so we don't alter the write context tracking behavior.
            var readContext = new DBcontext(baseOptions);
            return new EfReadDbContext(readContext);
        });

        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    }
}
