using Hahn.Infrastructure.Mappings;
using Hahn.WorkerService.Configurations;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);
var hostEnvironment = builder.Environment;

builder.Configuration
            .SetBasePath(hostEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

// Configure Hangfire services
builder.Services.ConfigureHangfire(builder.Configuration);

// Register the job service
builder.Services.RegisterJobServices(builder.Configuration);

builder.Services.AddAutoMapper(typeof(InfrastructureMappingProfile));

var app = builder.Build();

app.UseHangfireDashboard();

app.Run();
