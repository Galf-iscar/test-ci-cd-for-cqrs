using System.Text.Json.Serialization;
using Azure.Identity;
using Horizon.Utils.Commands.Abstractions.Mediator;
using Horizon.Utils.Output;
using Serilog;

namespace test_ci_cd_for_cqrs.API.Registrations
{
    public static class Registrations
    {
        public static void SetEnvironemnt(this WebApplicationBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder); // Validate that 'builder' is not null  
            builder.Configuration
                  .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                  .AddEnvironmentVariables();
            Log.Information("Environment: {Environment}", builder.Environment.EnvironmentName);
        }

        public static void AddAPIRegistrations(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers()
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
             });
            builder.Services.AddSingleton<IOutputFactory, OutputFactory>();
        }

        public static void ConnectToKeyVault(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;
            bool isKeyVaultEnabled = configuration.GetValue<bool>("KeyVault:Enabled");
            string? keyVaultUrl = configuration["KeyVault:Url"];

            if (builder.Environment.IsProduction() && isKeyVaultEnabled && !string.IsNullOrEmpty(keyVaultUrl))
            {
                DotNetEnv.Env.Load();
                var credential = new ClientSecretCredential(
                   Environment.GetEnvironmentVariable("AZURE_TENANT_ID"),
                   Environment.GetEnvironmentVariable("AZURE_CLIENT_ID"),
                   Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET"));
                builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), credential);
            }
        }

        public static void AddEndpoints(this WebApplicationBuilder builder)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
