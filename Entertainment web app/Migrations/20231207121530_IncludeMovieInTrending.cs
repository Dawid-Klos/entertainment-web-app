using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entertainmentwebapp.Migrations
{
    /// <inheritdoc />
    public partial class IncludeMovieInTrending : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImgSmall",
                table: "Trending",
                newName: "ImgTrendingSmall");

            migrationBuilder.RenameColumn(
                name: "ImgLarge",
                table: "Trending",
                newName: "ImgTrendingLarge");

            migrationBuilder.CreateIndex(
                name: "IX_Trending_MovieId",
                table: "Trending",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trending_Movies_MovieId",
                table: "Trending",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trending_Movies_MovieId",
                table: "Trending");

            migrationBuilder.DropIndex(
                name: "IX_Trending_MovieId",
                table: "Trending");

            migrationBuilder.RenameColumn(
                name: "ImgTrendingSmall",
                table: "Trending",
                newName: "ImgSmall");

            migrationBuilder.RenameColumn(
                name: "ImgTrendingLarge",
                table: "Trending",
                newName: "ImgLarge");
        }
    }
}
