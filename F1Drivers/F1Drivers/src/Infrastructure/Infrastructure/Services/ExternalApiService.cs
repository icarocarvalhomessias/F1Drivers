using Hahn.Application;

namespace Hahn.Infrastructure.Services;

public class ExternalApiService : IExternalApiService
{
    private readonly HttpClient _httpClient;

    public ExternalApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> FetchDataAsync()
    {
        var response = await _httpClient.GetAsync("https://api.publicapis.org/entries");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
