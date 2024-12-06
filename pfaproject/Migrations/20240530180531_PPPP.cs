using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pfaproject.Migrations
{
    public partial class PPPP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17358964-0c4a-4911-a9a5-6e9b692082b7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "411a34c1-56d7-4f99-b9a4-e186ffb40d22");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abdb041b-684f-4a48-ae5f-7996f1cd4f69");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "17358964-0c4a-4911-a9a5-6e9b692082b7", "2", "Client", "Client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "411a34c1-56d7-4f99-b9a4-e186ffb40d22", "3", "Cooperative", "Cooperative" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "abdb041b-684f-4a48-ae5f-7996f1cd4f69", "1", "Admin", "Admin" });
        }
    }
}
