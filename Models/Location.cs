using System.Collections;
using System.Collections.Generic;

namespace Abeslamidze_Kursovaya7.Models;


public class Location
{
    public Location()
    {
    }

    public Location(string name)
    {
        Name = name;
    }

    public string Name { get; }

    private Dictionary<string, int> _distanceMap = new Dictionary<string, int>()  {
            {"Могилев-Витебск", 240},
            {"Могилев-Гродно", 285},
            {"Могилев-Гомель",  180},
            {"Могилев-Брест", 370},
            {"Могилев-Минск", 200},
            {"Витебск-Могилев", 240},
            {"Витебск-Гродно", 340},
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

    public int GetDistance(Location from, Location to )
    {
        string key = from.ToString() + "-" + to.ToString();
        return _distanceMap[key];
    }
}
