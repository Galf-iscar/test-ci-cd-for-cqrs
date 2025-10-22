using CQRS_App.Application.Registrations;
using CQRS_App.API.Registrations;
using CQRS_App.API.Startup;
using CQRS_App.Infrastructure.Registrations; 

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