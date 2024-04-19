using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entertainmentwebapp.Migrations
{
    /// <inheritdoc />
    public partial class FixBookmarkEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_AspNetUsers_ApplicationUserId",
                table: "Bookmarks");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Bookmarks",
                newName: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_AspNetUsers_UserId",
                table: "Bookmarks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_AspNetUsers_UserId",
                table: "Bookmarks");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Bookmarks",
                newName: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_AspNetUsers_ApplicationUserId",
                table: "Bookmarks",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
