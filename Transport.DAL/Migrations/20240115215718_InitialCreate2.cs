using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transport.DAL.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Orders_OrderId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Transports_TransportId",
                table: "Deliveries");

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("018d4f64-6dac-40a4-b19d-f0b1680470a6"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("1e9f6719-3141-4a2e-85d1-99c86dda4fdb"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("8b8e1a7e-a2c0-4a8a-917e-7e3717107298"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("9c83fa99-53d6-4700-94fb-b63a8d1e3155"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("cdb6e1af-92ac-4483-86cb-c9dcd91ab88c"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("df425d49-a189-4877-8b11-590ef87e7f82"));

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("208c1dbc-e3ff-4b20-9a46-6953d6eb3ff6"));

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("529c359d-f1d0-48a7-ae46-cae6c5727bbe"));

            migrationBuilder.DeleteData(
                table: "Transports",
                keyColumn: "Id",
                keyValue: new Guid("c2b0457f-89c9-4768-b9be-825c5266297f"));

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("207da241-d0fe-44dc-88a5-19ce82dec804"), "Витебск" },
                    { new Guid("54264651-1321-4add-9aec-eef9ceba49dc"), "Минск" },
                    { new Guid("59853324-3468-4846-8b17-a0358429d336"), "Брест" },
                    { new Guid("6d1b8cc1-f77c-46da-ba65-090925f69638"), "Гродно" },
                    { new Guid("a37686ab-4b36-4825-92b5-a75ac701061e"), "Гомель" },
                    { new Guid("c21110ce-7e51-484e-9200-df40362162f3"), "Могилев" }
                });

            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "Id", "AvailableVolume", "CurrentLoad", "PricePerKm", "Speed", "Status", "Volume" },
                values: new object[,]
                {
                    { new Guid("24758b91-7f8e-4e91-8d2d-d785303b8ff2"), 1500.0, 0.0, 25.0, 350.0, 2, 1500.0 },
                    { new Guid("4c25493c-6cff-48a6-bd96-b954b1e8df8b"), 500.0, 0.0, 15.0, 550.0, 2, 500.0 },
                    { new Guid("cf16351f-c254-4d9b-b346-444d21fe3526"), 1000.0, 0.0, 35.0, 450.0, 2, 1000.0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Orders_OrderId",
                table: "Deliveries",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Transports_TransportId",
                table: "Deliveries",
                column: "TransportId",
                principalTable: "Transports",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Orders_OrderId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Transports_TransportId",
                table: "Deliveries");

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("207da241-d0fe-44dc-88a5-19ce82dec804"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("54264651-1321-4add-9aec-eef9ceba49dc"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("59853324-3468-4846-8b17-a0358429d336"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("6d1b8cc1-f77c-46da-ba65-090925f69638"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("a37686ab-4b36-4825-92b5-a75ac701061e"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("c21110ce-7e51-484e-9200-df40362162f3"));

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

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("018d4f64-6dac-40a4-b19d-f0b1680470a6"), "Гродно" },
                    { new Guid("1e9f6719-3141-4a2e-85d1-99c86dda4fdb"), "Гомель" },
                    { new Guid("8b8e1a7e-a2c0-4a8a-917e-7e3717107298"), "Минск" },
                    { new Guid("9c83fa99-53d6-4700-94fb-b63a8d1e3155"), "Витебск" },
                    { new Guid("cdb6e1af-92ac-4483-86cb-c9dcd91ab88c"), "Брест" },
                    { new Guid("df425d49-a189-4877-8b11-590ef87e7f82"), "Могилев" }
                });

            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "Id", "AvailableVolume", "CurrentLoad", "PricePerKm", "Speed", "Status", "Volume" },
                values: new object[,]
                {
                    { new Guid("208c1dbc-e3ff-4b20-9a46-6953d6eb3ff6"), 500.0, 0.0, 15.0, 550.0, 2, 500.0 },
                    { new Guid("529c359d-f1d0-48a7-ae46-cae6c5727bbe"), 1000.0, 0.0, 35.0, 450.0, 2, 1000.0 },
                    { new Guid("c2b0457f-89c9-4768-b9be-825c5266297f"), 1500.0, 0.0, 25.0, 350.0, 2, 1500.0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Orders_OrderId",
                table: "Deliveries",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Transports_TransportId",
                table: "Deliveries",
                column: "TransportId",
                principalTable: "Transports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
