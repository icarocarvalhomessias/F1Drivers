namespace Hahn.Application;

public interface IExternalApiService
{
    Task<string> FetchDataAsync();
}