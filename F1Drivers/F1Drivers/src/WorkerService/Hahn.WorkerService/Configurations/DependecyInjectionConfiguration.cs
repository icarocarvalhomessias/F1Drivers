using Hahn.Application.Jobs;
using Hahn.Application;
using Hahn.Infrastructure.Jobs;
using Hahn.Infrastructure.Repositories;
using Hahn.Infrastructure.Services;
using Hahn.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Hahn.WorkerService.Configurations;

public static class DependecyInjectionConfiguration
{
    public static void RegisterJobServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("HahnData")));

        services.AddScoped<IJobService, JobService>();
        services.AddHttpClient<IDriverJobService, DriverJobService>();
        services.AddScoped<IExternalApiService, ExternalApiService>();
        services.AddScoped<IDriverRepository, DriverRepository>();

        // Add a hosted service to schedule the recurring job
        services.AddHostedService<RecurringJobScheduler>();
    }
}
