using Hahn.Application.Jobs;
using Hahn.Application;
using Hahn.Domain.Entities;
using AutoMapper;
using System.Text.Json;
using Hahn.Application.DTOs;

namespace Hahn.Infrastructure.Jobs;

public class DriverJobService : IDriverJobService
{
    private readonly HttpClient _httpClient;
    private readonly IDriverRepository _driverRepository;
    private readonly IMapper _mapper;

    public DriverJobService(HttpClient httpClient, IDriverRepository driverRepository, IMapper mapper)
    {
        _httpClient = httpClient;
        _driverRepository = driverRepository;
        _mapper = mapper;
    }

    public async Task FetchAndUpsertDriversAsync()
    {
        int limit = 5;
        int offset = 0;
        bool hasMoreData = true;

        while (hasMoreData)
        {
            var response = await _httpClient.GetAsync($"https://f1connectapi.vercel.app/api/drivers?limit={limit}&offset={offset}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (apiResponse?.Drivers == null || apiResponse.Drivers.Count == 0)
            {
                hasMoreData = false;
                break;
            }

            var drivers = _mapper.Map<List<Driver>>(apiResponse.Drivers);

            try
            {
                foreach (var driver in drivers)
                {
                    var existingDriver = await _driverRepository.GetByDriverIdAsync(driver.DriverId);
                    if (existingDriver == null)
                    {
                        await _driverRepository.AddAsync(driver);
                    }
                    else
                    {
                        existingDriver.Update(driver);
                        await _driverRepository.UpdateAsync(existingDriver);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            offset += limit;
        }
    }
}
