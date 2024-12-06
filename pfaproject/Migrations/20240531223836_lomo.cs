using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pfaproject.Migrations
{
    public partial class lomo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "158366a8-0320-4b57-a016-4d3058b585e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b03baf23-2f4e-4b14-bbd2-4e70ae73f2f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f56f24e7-ff73-4f25-bfb7-922f7191cec3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "48f6ce21-3c56-4f20-93d7-cf7b3c53ab01", "3", "Cooperative", "Cooperative" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9190c084-c006-4a40-b18f-b2f71c6714af", "2", "Client", "Client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dfbd3f5d-9fc2-4f11-873a-53f59409716a", "1", "Admin", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48f6ce21-3c56-4f20-93d7-cf7b3c53ab01");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9190c084-c006-4a40-b18f-b2f71c6714af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfbd3f5d-9fc2-4f11-873a-53f59409716a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "158366a8-0320-4b57-a016-4d3058b585e8", "3", "Cooperative", "Cooperative" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b03baf23-2f4e-4b14-bbd2-4e70ae73f2f6", "2", "Client", "Client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f56f24e7-ff73-4f25-bfb7-922f7191cec3", "1", "Admin", "Admin" });
        }
    }
}
