using Microsoft.EntityFrameworkCore.Migrations;

namespace WebEFMigration.Migrations
{
    public partial class v46 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlogID",
                table: "Blogs",
                newName: "NameID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameID",
                table: "Blogs",
                newName: "BlogID");
        }
    }
}
