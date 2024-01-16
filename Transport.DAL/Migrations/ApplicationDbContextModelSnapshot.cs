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
                            Id = new Guid("59853324-3468-4846-8b17-a0358429d336"),
                            Name = "Брест"
                        },
                        new
                        {
                            Id = new Guid("54264651-1321-4add-9aec-eef9ceba49dc"),
                            Name = "Минск"
                        },
                        new
                        {
                            Id = new Guid("a37686ab-4b36-4825-92b5-a75ac701061e"),
                            Name = "Гомель"
                        },
                        new
                        {
                            Id = new Guid("c21110ce-7e51-484e-9200-df40362162f3"),
                            Name = "Могилев"
                        },
                        new
                        {
                            Id = new Guid("207da241-d0fe-44dc-88a5-19ce82dec804"),
                            Name = "Витебск"
                        },
                        new
                        {
                            Id = new Guid("6d1b8cc1-f77c-46da-ba65-090925f69638"),
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
                            Id = new Guid("24758b91-7f8e-4e91-8d2d-d785303b8ff2"),
                            AvailableVolume = 1500.0,
                            CurrentLoad = 0.0,
                            PricePerKm = 25.0,
                            Speed = 350.0,
                            Status = 2,
                            Volume = 1500.0
                        },
                        new
                        {
                            Id = new Guid("4c25493c-6cff-48a6-bd96-b954b1e8df8b"),
                            AvailableVolume = 500.0,
                            CurrentLoad = 0.0,
                            PricePerKm = 15.0,
                            Speed = 550.0,
                            Status = 2,
                            Volume = 500.0
                        },
                        new
                        {
                            Id = new Guid("cf16351f-c254-4d9b-b346-444d21fe3526"),
                            AvailableVolume = 1000.0,
                            CurrentLoad = 0.0,
                            PricePerKm = 35.0,
                            Speed = 450.0,
                            Status = 2,
                            Volume = 1000.0
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