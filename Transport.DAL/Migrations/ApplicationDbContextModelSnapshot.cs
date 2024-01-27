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
                            Id = new Guid("48b8aee6-794f-4802-b8d8-76b012e41de9"),
                            Name = "Брест"
                        },
                        new
                        {
                            Id = new Guid("067b1f79-d231-4989-bbaf-d9ab9855d0d3"),
                            Name = "Минск"
                        },
                        new
                        {
                            Id = new Guid("b8e8fcd3-4737-4e89-856f-f66644d0660c"),
                            Name = "Гомель"
                        },
                        new
                        {
                            Id = new Guid("01c97ac0-b844-49d2-bb98-7390c0dd6d7b"),
                            Name = "Могилев"
                        },
                        new
                        {
                            Id = new Guid("a8ba1285-03ad-4542-b789-ab5a264e9783"),
                            Name = "Витебск"
                        },
                        new
                        {
                            Id = new Guid("fada1d5d-f309-4fc0-97ab-ead5f694039f"),
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

            modelBuilder.Entity("Transport.DAL.Entities.RoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f9147089-9b5d-4197-b39d-12764d64df6d"),
                            Name = 0
                        },
                        new
                        {
                            Id = new Guid("242a1105-d13f-4a5a-9007-77333a03269f"),
                            Name = 1
                        },
                        new
                        {
                            Id = new Guid("3a9433d1-da79-4319-942f-28bfaeeaee0c"),
                            Name = 2
                        },
                        new
                        {
                            Id = new Guid("c3ca11d0-eda4-4c09-8b91-4fbe5811fefa"),
                            Name = 3
                        });
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
                            Id = new Guid("772b5e3d-35f3-4f41-9a60-8e9f8870a810"),
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
                            Id = new Guid("ed6e8c58-07e7-4ca2-8544-9b8d6c101fa8"),
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
                            Id = new Guid("6de4a9dd-c830-4be4-aedb-021e72c6f980"),
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
                            Id = new Guid("e043938f-e34b-4fff-8b96-de3e37ce2a33"),
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
                            Id = new Guid("f81a6f7c-c567-43b2-b013-771170da749c"),
                            AvailableVolume = 754.0,
                            CurrentLoad = 0.0,
                            Name = "Truck-5 Renault Trucks",
                            PricePerKm = 35.0,
                            Speed = 95.799999999999997,
                            Status = 2,
                            Volume = 754.0
                        });
                });

            modelBuilder.Entity("Transport.DAL.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d089bf3e-0493-4158-8573-9cee2ab9919d"),
                            Password = "admin",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("UserRoleEntity", b =>
                {
                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RolesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UsersId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("UserRoleEntity");
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

            modelBuilder.Entity("UserRoleEntity", b =>
                {
                    b.HasOne("Transport.DAL.Entities.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Transport.DAL.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
