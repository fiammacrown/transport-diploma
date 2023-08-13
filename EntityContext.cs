using Abeslamidze_Kursovaya7.Models;
using System.Data.Entity;
using System.Windows.Controls;

namespace Abeslamidze_Kursovaya7
{
    public class EntityContext : DbContext
    {
        public EntityContext(string name) : base(name) 
        {
            Database.SetInitializer(new DataBaseInitializer());
        }

        public DbSet<Transport> Transports { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }
    }

    public class DataBaseInitializer : DropCreateDatabaseIfModelChanges<EntityContext>
    {
         
        protected override void Seed(EntityContext context)
        {   context.Transports.AddRange(new Transport[] {
                new Transport(350, 1500, 25),
                new Transport(550, 500, 15),
                new Transport(450, 1000, 35)
            });
        }
    }
}
