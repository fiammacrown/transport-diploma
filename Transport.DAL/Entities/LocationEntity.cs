using System;
using System.ComponentModel.DataAnnotations;

namespace Transport.DAL.Entities;

public class LocationEntity
{
    public LocationEntity()
    {
    }

    public LocationEntity(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

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
