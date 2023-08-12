namespace Abeslamidze_Kursovaya7.Models;

public class Location
{
    public Location(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public override string? ToString()
    {
        return Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
