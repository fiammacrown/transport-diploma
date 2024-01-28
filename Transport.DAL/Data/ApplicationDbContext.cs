﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Transport.DAL.Entities;

namespace Transport.DAL.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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
			new TransportEntity("Truck-1 Mercedes-Benz", 100.5, 602, 25),
			new TransportEntity("Truck-2 Volvo Trucks", 90.2, 357, 15),
			new TransportEntity("Truck-3 Scania", 105.7, 406, 35),
			new TransportEntity("Truck-4 Kamaz", 110.3, 551, 35),
			new TransportEntity("Truck-5 Renault Trucks", 95.8, 754, 35)
		);

		modelBuilder.Entity<LocationEntity>().HasData(
			new LocationEntity("Брест"),
			new LocationEntity("Минск"),
			new LocationEntity("Гомель"),
			new LocationEntity("Могилев"),
			new LocationEntity("Витебск"),
			new LocationEntity("Гродно")
		);

		// Seed admin user 
		string ADMIN_ID = "1D6FCC45-2BBB-4AC5-821C-E034B87384E1";
		string ROLE_ID = "AA5684EA-E8BD-4D3B-B4B1-373180E21CD2";

		modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
		{
			Id = ROLE_ID,
			Name = "Admin",
			ConcurrencyStamp = ROLE_ID
		});

		var adminUser = new ApplicationUser
		{
			Id = ADMIN_ID,
			UserName = "admin",
			NormalizedUserName = "admin",
			Name = "Administrator"
		};

		PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
		adminUser.PasswordHash = ph.HashPassword(adminUser, "admin");

		modelBuilder.Entity<ApplicationUser>().HasData(adminUser);
		modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
		{
			RoleId = ROLE_ID,
			UserId = ADMIN_ID
		});
	}
}

