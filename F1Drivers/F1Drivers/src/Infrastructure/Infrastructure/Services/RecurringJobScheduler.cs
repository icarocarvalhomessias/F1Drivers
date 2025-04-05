using Hahn.Application.Jobs;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hahn.Infrastructure.Services;

public class RecurringJobScheduler : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RecurringJobScheduler> _logger;

    public RecurringJobScheduler(IServiceProvider serviceProvider, ILogger<RecurringJobScheduler> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var jobService = scope.ServiceProvider.GetRequiredService<IDriverJobService>();
                RecurringJob.AddOrUpdate(() => jobService.FetchAndUpsertDriversAsync(), Cron.Hourly);
            }

            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while scheduling the recurring job.");
        }
    }
}
