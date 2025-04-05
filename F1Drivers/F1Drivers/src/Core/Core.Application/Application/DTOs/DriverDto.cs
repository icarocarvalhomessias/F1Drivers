namespace Hahn.Application.DTOs;

public class DriverDto
{
    public string DriverId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Nationality { get; set; }
    public DateTime Birthday { get; set; }
    public int? Number { get; set; } // Permitir valores nulos
    public string? ShortName { get; set; } // Permitir valores nulos
    public string Url { get; set; }
}
