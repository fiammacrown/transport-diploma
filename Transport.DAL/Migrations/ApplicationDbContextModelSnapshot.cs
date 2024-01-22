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
                            Id = new Guid("383e8016-2f45-4bae-9344-16b153cd7f45"),
                            Name = "Брест"
                        },
                        new
                        {
                            Id = new Guid("fdb82f8e-36b5-45c8-92c0-943762cc85d2"),
                            Name = "Минск"
                        },
                        new
                        {
                            Id = new Guid("f6b9a76c-973f-4a30-9fe1-299ee03475db"),
                            Name = "Гомель"
                        },
                        new
                        {
                            Id = new Guid("5e37877d-6a88-4e09-a650-334f1c5a3af0"),
                            Name = "Могилев"
                        },
                        new
                        {
                            Id = new Guid("15644615-8d14-40bb-8cd6-fd1c1f50d8f4"),
                            Name = "Витебск"
                        },
                        new
                        {
                            Id = new Guid("8713736b-ce01-4dfa-aaf9-e1af23c3e5ad"),
                            Name = "Гродно"
                        });
                });

            modelBuilder.Entity("Transport.DAL.Entities.OrderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FromId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("ToId")
                        .HasColumnType("uniqueidentifier");

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
                            Id = new Guid("c0b054a5-fd6b-4030-8c5f-277d531f5fe9"),
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
                            Id = new Guid("5da2c095-c979-401e-8bef-701eae3724d5"),
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
                            Id = new Guid("a1c7b92d-20da-4c83-88ad-016e56185739"),
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
                            Id = new Guid("520c3286-c70c-46e2-833e-062d05a40997"),
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
                            Id = new Guid("bbd04688-8955-4aa4-a5f3-d2cc8bccae28"),
                            AvailableVolume = 754.0,
                            CurrentLoad = 0.0,
                            Name = "Truck-5 Renault Trucks",
                            PricePerKm = 35.0,
                            Speed = 95.799999999999997,
                            Status = 2,
                            Volume = 754.0
                        });
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
