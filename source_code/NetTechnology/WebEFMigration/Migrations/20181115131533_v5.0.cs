using Microsoft.EntityFrameworkCore.Migrations;

namespace WebEFMigration.Migrations
{
    public partial class v50 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogNameID",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogNameID",
                table: "Posts",
                column: "BlogNameID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Blogs_BlogNameID",
                table: "Posts",
                column: "BlogNameID",
                principalTable: "Blogs",
                principalColumn: "NameID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Blogs_BlogNameID",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_BlogNameID",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "BlogNameID",
                table: "Posts");
        }
    }
}
