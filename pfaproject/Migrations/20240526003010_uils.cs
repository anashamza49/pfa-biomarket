using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pfaproject.Migrations
{
    public partial class uils : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4817bfb8-3c70-4c1c-ba04-47da3ccf05ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ca65079-b055-4a92-8fae-9238a0f2dc80");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d47f3df1-b76e-41d9-ae50-156ff9cdb7d1");

            migrationBuilder.CreateTable(
                name: "Cooperatives",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomCooperative = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecteurCooperative = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiegeCooperative = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CertificatPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsValidated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cooperatives", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Cooperatives_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cooperatives");

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
                values: new object[] { "4817bfb8-3c70-4c1c-ba04-47da3ccf05ad", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5ca65079-b055-4a92-8fae-9238a0f2dc80", "2", "Client", "Client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d47f3df1-b76e-41d9-ae50-156ff9cdb7d1", "3", "Cooperative", "Cooperative" });
        }
    }
}
