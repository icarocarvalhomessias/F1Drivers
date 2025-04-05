using Hahn.Application;
using Hahn.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hahn.Infrastructure.Repositories;

public class DriverRepository : IDriverRepository, IRepository<Driver>
{
    private readonly AppDbContext _context;

    public DriverRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Driver>> GetAllAsync()
    {
        return await _context.Drivers.ToListAsync();
    }

    public async Task<Driver?> GetByDriverIdAsync(string driverId)
    {
        return await _context.Drivers.Where(d => d.DriverId == driverId).FirstOrDefaultAsync();
    }

    public async Task AddAsync(Driver driver)
    {
        await _context.Drivers.AddAsync(driver);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Driver driver)
    {
        _context.Drivers.Update(driver);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string driverId)
    {
        var driver = await GetByDriverIdAsync(driverId);
        _context.Drivers.Remove(driver);
        await _context.SaveChangesAsync();
    }

    public Task<Driver> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}