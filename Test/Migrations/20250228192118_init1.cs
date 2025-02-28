using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("246a4bd0-5cde-4f98-a9ec-b866e9318acf"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "IsAdmin", "PasswordHash", "UserName" },
                values: new object[] { new Guid("246a4bd0-5cde-4f98-a9ec-b866e9318acf"), true, "AQAAAAIAAYagAAAAEG45aB/m2ADiBtA5qkG1Oy+FXTEGGNrVvvq6Rtgj6iu3YdD5ExMbINtARjj01WQphQ==", "Admin" });
        }
    }
}
