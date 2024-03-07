using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class AddRolesToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "58124d04-1106-44f2-b579-aa9b36b470ac", "46d627b5-8801-4a27-87bd-6593bf21131f", "Editor", "EDITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ea347f4c-5e7a-45de-bc20-348d00749269", "1626fabf-8555-4ca9-9ad9-0b1a95998d21", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f9ae4c69-bb52-4c1f-b3e5-aa9c6cb0bd20", "75ad38a9-4f66-4d7f-a723-732ebcaa1b06", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58124d04-1106-44f2-b579-aa9b36b470ac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea347f4c-5e7a-45de-bc20-348d00749269");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9ae4c69-bb52-4c1f-b3e5-aa9c6cb0bd20");
        }
    }
}
