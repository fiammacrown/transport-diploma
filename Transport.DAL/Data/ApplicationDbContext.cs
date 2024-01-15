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

		modelBuilder.Entity<OrderEntity>()
		   .HasOne(o => o.From)
		   .WithMany()
		   .HasForeignKey(o => o.FromId)
		   .OnDelete(DeleteBehavior.ClientSetNull);

		modelBuilder.Entity<OrderEntity>()
			.HasOne(o => o.To)
			.WithMany()
			.HasForeignKey(o => o.ToId)
			.OnDelete(DeleteBehavior.ClientSetNull);

		modelBuilder.Entity<DeliveryEntity>()
			.HasOne(o => o.Order)
			.WithMany()
			.HasForeignKey(o => o.OrderId)
			.OnDelete(DeleteBehavior.ClientSetNull);

		modelBuilder.Entity<DeliveryEntity>()
			.HasOne(o => o.Transport)
			.WithMany()
			.HasForeignKey(o => o.TransportId)
			.OnDelete(DeleteBehavior.ClientSetNull);

		modelBuilder.Entity<TransportEntity>().HasData(
			new TransportEntity(350, 1500, 25),
			new TransportEntity(550, 500, 15),
			new TransportEntity(450, 1000, 35)
		);

		modelBuilder.Entity<LocationEntity>().HasData(
			new LocationEntity("Брест"),
			new LocationEntity("Минск"),
			new LocationEntity("Гомель"),
			new LocationEntity("Могилев"),
			new LocationEntity("Витебск"),
			new LocationEntity("Гродно")
		);


	}
}

