﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Transport.DAL.Data;

#nullable disable

namespace Transport.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "AA5684EA-E8BD-4D3B-B4B1-373180E21CD2",
                            ConcurrencyStamp = "AA5684EA-E8BD-4D3B-B4B1-373180E21CD2",
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "1D6FCC45-2BBB-4AC5-821C-E034B87384E1",
                            RoleId = "AA5684EA-E8BD-4D3B-B4B1-373180E21CD2"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Transport.DAL.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1D6FCC45-2BBB-4AC5-821C-E034B87384E1",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ba058d4d-67b6-47fd-9312-e16a2f15699a",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "Administrator",
                            PasswordHash = "AQAAAAEAACcQAAAAENaFAQDpRIHQ/bTu1sATBaTVQFYPWSlUw/ulxjxMwpH/zZwi9dUGhuCphxWzyUA8DA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "9894a14c-f913-4510-94b4-890a330be7df",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("Transport.DAL.Entities.DeliveryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("TransportId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("TransportId");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("Transport.DAL.Entities.LocationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cbf7f715-ced2-4ff4-8124-845903ae2a2f"),
                            Name = "Брест"
                        },
                        new
                        {
                            Id = new Guid("559af1cc-543f-4c4b-8169-43151cc20e99"),
                            Name = "Минск"
                        },
                        new
                        {
                            Id = new Guid("8132b124-ece3-4be7-809f-7a76832392fe"),
                            Name = "Гомель"
                        },
                        new
                        {
                            Id = new Guid("d8b2e7e5-6d54-4de8-ac43-0882f710e2b8"),
                            Name = "Могилев"
                        },
                        new
                        {
                            Id = new Guid("d91a4e67-d515-441b-9ae0-315f9950724a"),
                            Name = "Витебск"
                        },
                        new
                        {
                            Id = new Guid("60b2741b-2602-4faf-afa4-9e39d65428ff"),
                            Name = "Гродно"
                        });
                });

            modelBuilder.Entity("Transport.DAL.Entities.OrderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeliveredDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FromId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("ToId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FromId");

                    b.HasIndex("ToId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Transport.DAL.Entities.TransportEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("AvailableVolume")
                        .HasColumnType("float");

                    b.Property<double>("CurrentLoad")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PricePerKm")
                        .HasColumnType("float");

                    b.Property<double>("Speed")
                        .HasColumnType("float");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<double>("Volume")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Transports");

                    b.HasData(
                        new
                        {
                            Id = new Guid("61995412-eaa3-4660-b67e-36ba0b1688ce"),
                            AvailableVolume = 602.0,
                            CurrentLoad = 0.0,
                            Name = "Truck-1 Mercedes-Benz",
                            PricePerKm = 25.0,
                            Speed = 100.5,
                            Status = 2,
                            Volume = 602.0
                        },
                        new
                        {
                            Id = new Guid("9c2c0c58-364a-4613-835a-79c82d2cfa52"),
                            AvailableVolume = 357.0,
                            CurrentLoad = 0.0,
                            Name = "Truck-2 Volvo Trucks",
                            PricePerKm = 15.0,
                            Speed = 90.200000000000003,
                            Status = 2,
                            Volume = 357.0
                        },
                        new
                        {
                            Id = new Guid("1d6ddb6f-74ba-4a36-95c7-3a8dc21013fe"),
                            AvailableVolume = 406.0,
                            CurrentLoad = 0.0,
                            Name = "Truck-3 Scania",
                            PricePerKm = 35.0,
                            Speed = 105.7,
                            Status = 2,
                            Volume = 406.0
                        },
                        new
                        {
                            Id = new Guid("3239f807-1c83-408d-915d-7adbd4d1214f"),
                            AvailableVolume = 551.0,
                            CurrentLoad = 0.0,
                            Name = "Truck-4 Kamaz",
                            PricePerKm = 35.0,
                            Speed = 110.3,
                            Status = 2,
                            Volume = 551.0
                        },
                        new
                        {
                            Id = new Guid("5deaf023-c5cc-4177-bcb4-96e96fede26c"),
                            AvailableVolume = 754.0,
                            CurrentLoad = 0.0,
                            Name = "Truck-5 Renault Trucks",
                            PricePerKm = 35.0,
                            Speed = 95.799999999999997,
                            Status = 2,
                            Volume = 754.0
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Transport.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Transport.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Transport.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Transport.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Transport.DAL.Entities.DeliveryEntity", b =>
                {
                    b.HasOne("Transport.DAL.Entities.OrderEntity", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .IsRequired();

                    b.HasOne("Transport.DAL.Entities.TransportEntity", "Transport")
                        .WithMany()
                        .HasForeignKey("TransportId")
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Transport");
                });

            modelBuilder.Entity("Transport.DAL.Entities.OrderEntity", b =>
                {
                    b.HasOne("Transport.DAL.Entities.LocationEntity", "From")
                        .WithMany()
                        .HasForeignKey("FromId")
                        .IsRequired();

                    b.HasOne("Transport.DAL.Entities.LocationEntity", "To")
                        .WithMany()
                        .HasForeignKey("ToId")
                        .IsRequired();

                    b.Navigation("From");

                    b.Navigation("To");
                });
#pragma warning restore 612, 618
        }
    }
}
