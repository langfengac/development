using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebEFMigration.Migrations
{
    public partial class v54 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Blogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Blogs");
        }
    }
}
