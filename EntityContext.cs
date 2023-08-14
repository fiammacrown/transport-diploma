using Abeslamidze_Kursovaya7.Models;
using System.Configuration;
using System.Data.Entity;
using System.Reflection.Metadata;
using System.Windows.Controls;

namespace Abeslamidze_Kursovaya7
{
    public class EntityContext : DbContext
    {
        public EntityContext(string name) : base(name)
        {
            Database.SetInitializer(new DataBaseInitializer());
        }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Transport> Transports { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Order>()
            //    .HasKey(o => o.FromId);

            //modelBuilder.Entity<Order>()
            //    .HasRequired(o => o.From)
            //    .WithOptional(l => l.Order);

            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.From)
            //    .WithMany()
            //    .HasForeignKey(o => o.FromId);

            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.To)
            //    .WithMany()
            //    .HasForeignKey(o => o.ToId);

            //modelBuilder.Entity<Delivery>()
            //    .HasOne(o => o.From)
            //    .WithMany()
            //    .HasForeignKey(o => o.FromId);

            //modelBuilder.Entity<Delivery>()
            //    .HasOne(o => o.To)
            //    .WithMany()
            //    .HasForeignKey(o => o.ToId);
        }
    }

    public class DataBaseInitializer : DropCreateDatabaseIfModelChanges<EntityContext>
    {
        protected override void Seed(EntityContext context)
        {
            context.Transports.AddRange(new Transport[] {
                new Transport(350, 1500, 25),
                new Transport(550, 500, 15),
                new Transport(450, 1000, 35)
            });

            context.Locations.AddRange(new Location[] {
                new Location("Брест"),
                new Location("Минск"),
                new Location("Гомель"),
                new Location("Могилев"),
                new Location("Минск"),
                new Location("Витебск"),
                new Location("Гродно"),
            });
        }
    }
}
