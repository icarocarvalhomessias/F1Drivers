using Hahn.Domain.Common;
using Hahn.Domain.Validators;

namespace Hahn.Domain.Entities;

public class Driver : Entity , IAggregateRoot
{
    public string DriverId { get; private set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Nationality { get; private set; }
    public DateTime Birthday { get; private set; }
    public int? Number { get; private set; }
    public string? ShortName { get; private set; }
    public string Url { get; private set; }

    public Driver(string driverId, string name, string surname, string nationality, DateTime birthday, int? number, string? shortName, string url)
    {
        DriverId = driverId;
        Name = name;
        Surname = surname;
        Nationality = nationality;
        Birthday = birthday;
        Number = number;
        ShortName = shortName;
        Url = url;

        Validate();
    }

    public void Update(Driver driver)
    {
        DriverId = driver.DriverId;
        Name = driver.Name;
        Surname = driver.Surname;
        Nationality = driver.Nationality;
        Birthday = driver.Birthday;
        Number = driver.Number;
        ShortName = driver.ShortName;
        Url = driver.Url;

        Validate();
    }


    public ValidationResultDetail Validate()
    {
        var validator = new DriverValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

}