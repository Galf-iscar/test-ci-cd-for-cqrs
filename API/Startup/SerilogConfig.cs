using Serilog;

namespace test_ci_cd_for_cqrs.API.Startup
{
    public static class SerilogConfig
    {
        public static void ConfigureSerilog(this WebApplicationBuilder builder)
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog(); // Use Serilog for logging
        }
    }
}
