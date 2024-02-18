using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transport.DAL.Migrations
{
    public partial class TransportImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("36fb6627-bf85-452b-bf35-a95cc04abf97"));

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("6df941ac-6faf-4f0b-81cc-2e4c1f12d8ec"));

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("7fe2f26c-a709-481e-904a-dba164295a40"));

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("94f44e7e-ffec-4cc6-b531-a96c3494ac49"));

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("b7213ef7-afc8-497c-aa7c-960e65f4d5ef"));

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Transports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");


            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "Id", "AvailableVolume", "CurrentLoad", "ImageURL", "Name", "PricePerKm", "Speed", "Status", "Volume" },
                values: new object[,]
                {
                    { new Guid("525c2c28-9f50-4142-b2d0-9ba557aced82"), 3000.0, 0.0, "https://sabeslamidze-transport-api.azurewebsites.net/images/truck-5.jpg", "Truck-5 Renault Trucks", 55.5, 200.0, 2, 3000.0 },
                    { new Guid("97998bda-16a4-4503-ad1c-b905aee33311"), 2000.0, 0.0, "https://sabeslamidze-transport-api.azurewebsites.net/images/truck-4.jpg", "Truck-4 Kamaz", 45.700000000000003, 250.0, 2, 2000.0 },
                    { new Guid("9e23e904-56cf-4921-b1e8-782ee828bf7c"), 500.0, 0.0, "https://sabeslamidze-transport-api.azurewebsites.net/images/truck-3.jpeg", "Truck-3 Scania", 35.799999999999997, 150.0, 2, 500.0 },
                    { new Guid("b4bbb917-d7c2-4626-8a8e-4db1ef3ab1f0"), 1500.0, 0.0, "https://sabeslamidze-transport-api.azurewebsites.net/images/truck-2.jpg", "Truck-2 Volvo Trucks", 25.399999999999999, 200.0, 2, 1500.0 },
                    { new Guid("b5df3b73-95d1-4726-98c0-2a87580d8ee5"), 1000.0, 0.0, "https://sabeslamidze-transport-api.azurewebsites.net/images/truck-1.png", "Truck-1 Mercedes-Benz", 15.6, 250.0, 2, 1000.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("525c2c28-9f50-4142-b2d0-9ba557aced82"));

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("97998bda-16a4-4503-ad1c-b905aee33311"));

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("9e23e904-56cf-4921-b1e8-782ee828bf7c"));

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("b4bbb917-d7c2-4626-8a8e-4db1ef3ab1f0"));

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("b5df3b73-95d1-4726-98c0-2a87580d8ee5"));

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Transports");

            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "Id", "AvailableVolume", "CurrentLoad", "Name", "PricePerKm", "Speed", "Status", "Volume" },
                values: new object[,]
                {
                    { new Guid("36fb6627-bf85-452b-bf35-a95cc04abf97"), 3000.0, 0.0, "Truck-5 Renault Trucks", 55.5, 200.0, 2, 3000.0 },
                    { new Guid("6df941ac-6faf-4f0b-81cc-2e4c1f12d8ec"), 2000.0, 0.0, "Truck-4 Kamaz", 45.700000000000003, 250.0, 2, 2000.0 },
                    { new Guid("7fe2f26c-a709-481e-904a-dba164295a40"), 1500.0, 0.0, "Truck-2 Volvo Trucks", 25.399999999999999, 350.0, 2, 1500.0 },
                    { new Guid("94f44e7e-ffec-4cc6-b531-a96c3494ac49"), 1000.0, 0.0, "Truck-1 Mercedes-Benz", 15.6, 400.0, 2, 1000.0 },
                    { new Guid("b7213ef7-afc8-497c-aa7c-960e65f4d5ef"), 500.0, 0.0, "Truck-3 Scania", 35.799999999999997, 300.0, 2, 500.0 }
                });
        }
    }
}
