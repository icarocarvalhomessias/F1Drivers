namespace Hahn.Application.Jobs;

public interface IDriverJobService
{
    Task FetchAndUpsertDriversAsync();
}
