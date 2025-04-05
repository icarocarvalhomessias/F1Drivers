
namespace Hahn.Application.DTOs;

public class ApiResponse
{
    public string Api { get; set; }
    public string Url { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
    public int Total { get; set; }
    public List<DriverDto> Drivers { get; set; }
}
