using Microsoft.EntityFrameworkCore.Migrations;

namespace WebEFMigration.Migrations
{
    public partial class v45 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Blogs",
                newName: "BlogID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlogID",
                table: "Blogs",
                newName: "ID");
        }
    }
}
