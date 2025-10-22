using Horizon.Utils.Exceptions.ExceptionMiddleware;

namespace test_ci_cd_for_cqrs.API.Startup
{
    public static class AppConfig
    {
        public static void ConfigureApp(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseExceptionHandlerMiddleware();
            app.UseHttpsRedirection();

            app.MapControllers();
        }
    }
}