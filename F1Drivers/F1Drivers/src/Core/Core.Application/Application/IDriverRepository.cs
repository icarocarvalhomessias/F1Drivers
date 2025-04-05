using Hahn.Domain.Entities;

namespace Hahn.Application;

public interface IDriverRepository
{
    Task<IEnumerable<Driver>> GetAllAsync();
    Task<Driver?> GetByDriverIdAsync(string driverId);
    Task AddAsync(Driver driver);
    Task UpdateAsync(Driver driver);
    Task DeleteAsync(string driverId);
}