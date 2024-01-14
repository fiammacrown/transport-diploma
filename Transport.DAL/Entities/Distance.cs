namespace Transport.DAL.Entities;

public class Distance
{
    public Distance(LocationEntity from, LocationEntity to)
    {
        From = from;
        To = to;
    }

    private Dictionary<string, int> _distanceMap = new Dictionary<string, int>()  {
            {"Могилев-Витебск", 240},
            {"Могилев-Гродно", 285},
            {"Могилев-Гомель",  180},
            {"Могилев-Брест", 370},
            {"Могилев-Минск", 200},
            {"Витебск-Могилев", 240},
            {"Витебск-Гродно", 340},
            {"Витебск-Гомель", 310},
            {"Витебск-Брест", 450},
            {"Витебск-Минск", 270},
            {"Гродно-Могилев",285},
            {"Гродно-Витебск", 340},
            {"Гродно-Гомель", 430},
            {"Гродно-Брест", 310},
            {"Гродно-Минск", 310},
            {"Гомель-Могилев", 180},
            {"Гомель-Витебск", 310},
            {"Гомель-Гродно", 430},
            {"Гомель-Брест", 270},
            {"Гомель-Минск", 310},
            {"Брест-Могилев", 370},
            {"Брест-Витебск", 450},
            {"Брест-Гродно", 270},
            {"Брест-Гомель", 310},
            {"Брест-Минск", 350},
            {"Минск-Могилев", 200},
            {"Минск-Витебск", 270},
            {"Минск-Гродно", 250},
            {"Минск-Гомель", 310},
            {"Минск-Брест", 350},
        };


    public LocationEntity From { get; }
    public LocationEntity To { get; }

    public int InKm
    {
        get
        {
            string key = this.ToString();
            return _distanceMap[key];
        }
    }

    public override string ToString()
    {
        return $"{From.Name}-{To.Name}";
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(From, To);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Distance distance)
        {
            return GetHashCode() == distance.GetHashCode();
        }
        return base.Equals(obj);
    }

}