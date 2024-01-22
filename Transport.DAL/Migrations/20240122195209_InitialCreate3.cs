using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transport.DAL.Migrations
{
    public partial class InitialCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("24758b91-7f8e-4e91-8d2d-d785303b8ff2"));

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("4c25493c-6cff-48a6-bd96-b954b1e8df8b"));

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("cf16351f-c254-4d9b-b346-444d21fe3526"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Transports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "Id", "AvailableVolume", "CurrentLoad", "Name", "PricePerKm", "Speed", "Status", "Volume" },
                values: new object[,]
                {
                    { new Guid("520c3286-c70c-46e2-833e-062d05a40997"), 551.0, 0.0, "Truck-4 Kamaz", 35.0, 110.3, 2, 551.0 },
                    { new Guid("5da2c095-c979-401e-8bef-701eae3724d5"), 357.0, 0.0, "Truck-2 Volvo Trucks", 15.0, 90.200000000000003, 2, 357.0 },
                    { new Guid("a1c7b92d-20da-4c83-88ad-016e56185739"), 406.0, 0.0, "Truck-3 Scania", 35.0, 105.7, 2, 406.0 },
                    { new Guid("bbd04688-8955-4aa4-a5f3-d2cc8bccae28"), 754.0, 0.0, "Truck-5 Renault Trucks", 35.0, 95.799999999999997, 2, 754.0 },
                    { new Guid("c0b054a5-fd6b-4030-8c5f-277d531f5fe9"), 602.0, 0.0, "Truck-1 Mercedes-Benz", 25.0, 100.5, 2, 602.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("520c3286-c70c-46e2-833e-062d05a40997"));

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("5da2c095-c979-401e-8bef-701eae3724d5"));

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("a1c7b92d-20da-4c83-88ad-016e56185739"));

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("bbd04688-8955-4aa4-a5f3-d2cc8bccae28"));

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("c0b054a5-fd6b-4030-8c5f-277d531f5fe9"));

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Transports");

            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "Id", "AvailableVolume", "CurrentLoad", "PricePerKm", "Speed", "Status", "Volume" },
                values: new object[,]
                {
                    { new Guid("24758b91-7f8e-4e91-8d2d-d785303b8ff2"), 1500.0, 0.0, 25.0, 350.0, 2, 1500.0 },
                    { new Guid("4c25493c-6cff-48a6-bd96-b954b1e8df8b"), 500.0, 0.0, 15.0, 550.0, 2, 500.0 },
                    { new Guid("cf16351f-c254-4d9b-b346-444d21fe3526"), 1000.0, 0.0, 35.0, 450.0, 2, 1000.0 }
                });
        }
    }
}
