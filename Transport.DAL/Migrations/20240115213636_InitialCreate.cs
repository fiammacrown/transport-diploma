using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transport.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Speed = table.Column<double>(type: "float", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    CurrentLoad = table.Column<double>(type: "float", nullable: false),
                    AvailableVolume = table.Column<double>(type: "float", nullable: false),
                    PricePerKm = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    FromId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Locations_FromId",
                        column: x => x.FromId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Locations_ToId",
                        column: x => x.ToId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deliveries_Transports_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_OrderId",
                table: "Deliveries",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_TransportId",
                table: "Deliveries",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FromId",
                table: "Orders",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ToId",
                table: "Orders",
                column: "ToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Transports");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
