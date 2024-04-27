using Moq;
using Xunit;

using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Repositories;
using Entertainment_web_app.Services;

namespace Entertainment_web_app.Tests;

public class MovieServiceTests
{
    private List<Movie> GetTestMovies()
    {
        var movies = new List<Movie>
      {
        new Movie { MovieId = 1, Title = "Movie 1", Category = "Movie", Rating = "PG", Year = 2021, ImgSmall = "/images/movie1.jpg", ImgMedium = "/images/movie1.jpg", ImgLarge = "/images/movie1.jpg" },
        new Movie { MovieId = 2, Title = "Movie 2", Category = "Movie", Rating = "PG", Year = 2021, ImgSmall = "/images/movie2.jpg", ImgMedium = "/images/movie2.jpg", ImgLarge = "/images/movie2.jpg" },
        new Movie { MovieId = 3, Title = "Movie 3", Category = "Movie", Rating = "PG", Year = 2021, ImgSmall = "/images/movie3.jpg", ImgMedium = "/images/movie3.jpg", ImgLarge = "/images/movie3.jpg" }
      };

        return movies;
    }

    [Fact]
    public async Task GetAll_ReturnsAllMovies()
    {
        //Arrange
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.GetAll()).ReturnsAsync(GetTestMovies());

        var service = new MovieService(repository.Object);

        //Act
        var result = await service.GetAll();

        //Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count());
    }

}
