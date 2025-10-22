using System.Reflection;
using Horizon.Utils.Commands.Abstractions.Mediator;
using Horizon.Utils.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace test_ci_cd_for_cqrs.Application.Registrations
{
    public static class Registrations
    {
        public static void AddApplicationRegistrations(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IMappingRegistry>(_ => new MappingRegistry(Assembly.GetExecutingAssembly()));

            // Register mapper as scoped or transient
            builder.Services.AddScoped<IMappingService, MapperWrapper>();

            RegisterMediatorServices(builder);
        }

        public static void RegisterMediatorServices(WebApplicationBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);
            builder.Services.AddCommands(
            builder.Configuration,                // for telemetry overrides
            opts =>
            {
                opts.UseValidation = true;
                opts.UseIdempotency = false;
                opts.UseAuthorization = false;
                opts.UseRetry = false;
                opts.UseMetrics = false;
                opts.UseLogging = true;
                opts.UseFieldAuthorization = false;

                // Scan the Application assembly for handlers, validators, authorizers, and policies:
                opts.ScanAssemblies = new[] { typeof(Registrations).Assembly };
            });
        }
    }
}
