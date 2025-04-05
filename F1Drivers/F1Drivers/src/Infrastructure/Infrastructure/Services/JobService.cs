using Hahn.Application;

namespace Hahn.Infrastructure.Services;

public class JobService : IJobService
{
    private readonly IExternalApiService _externalApiService;
    //private readonly IRepository<YourEntity> _repository;

    public JobService(IExternalApiService externalApiService)
    {
        _externalApiService = externalApiService;
        //_repository = repository;
    }

    public async Task ExecuteAsync()
    {
        var data = await _externalApiService.FetchDataAsync();
        // Parse and upsert data into the database
    }
}