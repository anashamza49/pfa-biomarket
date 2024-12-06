using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pfaproject.Migrations
{
    public partial class uilsm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84cd02c2-99b9-4e8d-aadc-08043daf30bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af32c2c2-7ba0-40ef-954d-33aa64146588");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa59d0ae-1124-49c2-a941-15bca3e7f957");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "719777ec-1195-4e44-9e35-5371e1b66206", "3", "Cooperative", "Cooperative" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ce90219d-4209-45c9-9812-49098452f82b", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fb55c758-d585-4b12-bf1a-6f523b5e0c64", "2", "Client", "Client" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "719777ec-1195-4e44-9e35-5371e1b66206");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce90219d-4209-45c9-9812-49098452f82b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb55c758-d585-4b12-bf1a-6f523b5e0c64");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "84cd02c2-99b9-4e8d-aadc-08043daf30bf", "2", "Client", "Client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "af32c2c2-7ba0-40ef-954d-33aa64146588", "3", "Cooperative", "Cooperative" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fa59d0ae-1124-49c2-a941-15bca3e7f957", "1", "Admin", "Admin" });
        }
    }
}
