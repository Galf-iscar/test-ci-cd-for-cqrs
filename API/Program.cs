using test_ci_cd_for_cqrs.API.Registrations;
using test_ci_cd_for_cqrs.API.Startup;
using test_ci_cd_for_cqrs.Application.Registrations;
using test_ci_cd_for_cqrs.Infrastructure.Registrations;

var builder = WebApplication.CreateBuilder(args);
builder.SetEnvironemnt();

builder.ConfigureSerilog();
builder.ConnectToKeyVault();

builder.AddInfrastructureRegistrations();
builder.AddApplicationRegistrations();
builder.AddAPIRegistrations();

builder.AddEndpoints();
var app = builder.Build();

app.ConfigureApp();
app.Run();