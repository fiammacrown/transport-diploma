using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transport.DAL.Migrations
{
    public partial class InitialCreate8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "AA5684EA-E8BD-4D3B-B4B1-373180E21CD2",
                column: "NormalizedName",
                value: "Admin");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "EC0ED5BF-56E0-4C33-90F2-BB327CF2F1D3", "EC0ED5BF-56E0-4C33-90F2-BB327CF2F1D3", "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1D6FCC45-2BBB-4AC5-821C-E034B87384E1",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5fd0ae2f-49e1-4c02-ba03-449408a19a71", "admin", "AQAAAAEAACcQAAAAEDhfslrgvUMf2A1i24Hf9FDuPKJlSRAEaLva8y4GmAltHVc+g6x7H8xEQhL8CMtypA==", "f97728fc-3c6c-48a5-a753-fef83bd08480" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "EC0ED5BF-56E0-4C33-90F2-BB327CF2F1D3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "AA5684EA-E8BD-4D3B-B4B1-373180E21CD2",
                column: "NormalizedName",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1D6FCC45-2BBB-4AC5-821C-E034B87384E1",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba058d4d-67b6-47fd-9312-e16a2f15699a", null, "AQAAAAEAACcQAAAAENaFAQDpRIHQ/bTu1sATBaTVQFYPWSlUw/ulxjxMwpH/zZwi9dUGhuCphxWzyUA8DA==", "9894a14c-f913-4510-94b4-890a330be7df" });
        }
    }
}
