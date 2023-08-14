using Abeslamidze_Kursovaya7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abeslamidze_Kursovaya7.Interfaces
{
    public interface ILocationsRepo
    {
        List<Location> GetAll();
        Location? GetById(Guid id);
    }
}
