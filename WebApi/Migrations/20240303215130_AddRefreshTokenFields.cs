using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class AddRefreshTokenFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "032b87a7-3092-4e28-97a1-5f9a43c130fc", "c8a25a99-c255-4933-8c88-db32d1830f63", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7c1d0039-e929-44ff-9520-8dcb3bb7fc6d", "fcf15d6f-4546-41a1-b037-cdd52b185ab2", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "beb626d1-d876-45e0-bf8c-3ae4a3b83408", "0e41c1da-78d8-43fc-a70b-caa4a92ee9d1", "Editor", "EDITOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "032b87a7-3092-4e28-97a1-5f9a43c130fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c1d0039-e929-44ff-9520-8dcb3bb7fc6d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "beb626d1-d876-45e0-bf8c-3ae4a3b83408");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

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
    }
}
