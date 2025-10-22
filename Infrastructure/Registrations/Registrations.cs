using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using test_ci_cd_for_cqrs.Domain.Interfaces;
using test_ci_cd_for_cqrs.Infrastructure.Data;
using test_ci_cd_for_cqrs.Infrastructure.Repositories;

namespace test_ci_cd_for_cqrs.Infrastructure.Registrations
{
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
}
