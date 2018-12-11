using Microsoft.EntityFrameworkCore.Migrations;

namespace WebEFMigration.Migrations
{
    public partial class v51 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "BlogID",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogID",
                table: "Posts",
                column: "BlogID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Blogs_BlogID",
                table: "Posts",
                column: "BlogID",
                principalTable: "Blogs",
                principalColumn: "NameID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Blogs_BlogID",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_BlogID",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "BlogID",
                table: "Posts");

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
    }
}
