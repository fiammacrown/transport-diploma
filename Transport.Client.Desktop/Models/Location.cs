using System;
using System.ComponentModel.DataAnnotations;

namespace Abeslamidze_Kursovaya7.Models;

public class Location
{
    public Location()
    {
    }

    public Location(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    [Key]
    public Guid Id { get; set; }
    public string Name { get; set;  }

    public override string? ToString()
    {
        return Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
