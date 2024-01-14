using Microsoft.EntityFrameworkCore;
using Transport.DAL.Entities;

namespace Transport.DAL.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        //Database.SetInitializer(new DataBaseInitializer());
    }

	public DbSet<LocationEntity> Locations { get; set; }

    public DbSet<OrderEntity> Orders { get; set; }

    public DbSet<TransportEntity> Transports { get; set; }

    public DbSet<DeliveryEntity> Deliveries { get; set; }


	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);

	}


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		//modelBuilder.Properties<DateTime>().Configure(
		//	c => c.HasColumnType("datetime2"));

		//modelBuilder.Entity<OrderEntity>()
		//   .HasRequired(o => o.From)
		//   .WithMany()
		//   .HasForeignKey(o => o.FromId)
		//   .WillCascadeOnDelete(false);

		//    modelBuilder.Entity<OrderEntity>()
		//        .HasRequired(o => o.To)
		//        .WithMany()
		//        .HasForeignKey(o => o.ToId)
		//        .WillCascadeOnDelete(false);

		//    modelBuilder.Entity<DeliveryEntity>()
		//      .HasRequired(o => o.Order)
		//      .WithMany()
		//      .HasForeignKey(o => o.OrderId)
		//      .WillCascadeOnDelete(false);

		//    modelBuilder.Entity<DeliveryEntity>()
		//        .HasRequired(o => o.Transport)
		//        .WithMany()
		//        .HasForeignKey(o => o.TransportId)
		//        .WillCascadeOnDelete(false);
	}
}


// TODO: Move to migrations
//public class DataBaseInitializer : DropCreateDatabaseIfModelChanges<EntityContext>
//{
//    protected override void Seed(EntityContext context)
//    {
//        context.Transports.AddRange(new Transport[] {
//            new Transport(350, 1500, 25),
//            new Transport(550, 500, 15),
//            new Transport(450, 1000, 35)
//        });

//        context.Locations.AddRange(new Location[] {
//            new Location("Брест"),
//            new Location("Минск"),
//            new Location("Гомель"),
//            new Location("Могилев"),
//            new Location("Витебск"),
//            new Location("Гродно"),
//        });
//    }
//}
