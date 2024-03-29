using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entertainmentwebapp.Migrations
{
    /// <inheritdoc />
    public partial class UserBookmarks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserMovie_Movies_UserMoviesMovieId",
                table: "ApplicationUserMovie");

            migrationBuilder.RenameColumn(
                name: "UserMoviesMovieId",
                table: "ApplicationUserMovie",
                newName: "BookmarksMovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserMovie_Movies_BookmarksMovieId",
                table: "ApplicationUserMovie",
                column: "BookmarksMovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserMovie_Movies_BookmarksMovieId",
                table: "ApplicationUserMovie");

            migrationBuilder.RenameColumn(
                name: "BookmarksMovieId",
                table: "ApplicationUserMovie",
                newName: "UserMoviesMovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserMovie_Movies_UserMoviesMovieId",
                table: "ApplicationUserMovie",
                column: "UserMoviesMovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
