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
        return new List<Movie>
      {
        new Movie { MovieId = 1, Title = "Movie 1", Category = "Movie", Rating = "PG", Year = 1998, ImgSmall = "/images/movie1.jpg", ImgMedium = "/images/movie1.jpg", ImgLarge = "/images/movie1.jpg" },
        new Movie { MovieId = 2, Title = "Movie 2", Category = "Movie", Rating = "PG", Year = 2020, ImgSmall = "/images/movie2.jpg", ImgMedium = "/images/movie2.jpg", ImgLarge = "/images/movie2.jpg" },
        new Movie { MovieId = 3, Title = "Movie 3", Category = "Movie", Rating = "PG", Year = 2021, ImgSmall = "/images/movie3.jpg", ImgMedium = "/images/movie3.jpg", ImgLarge = "/images/movie3.jpg" },
        new Movie { MovieId = 4, Title = "Movie 4", Category = "TV Series", Rating = "PG", Year = 2018, ImgSmall = "/images/movie4.jpg", ImgMedium = "/images/movie4.jpg", ImgLarge = "/images/movie4.jpg" },
        new Movie { MovieId = 5, Title = "Movie 5", Category = "TV Series", Rating = "PG", Year = 2019, ImgSmall = "/images/movie5.jpg", ImgMedium = "/images/movie5.jpg", ImgLarge = "/images/movie5.jpg" },
        new Movie { MovieId = 6, Title = "Movie 6", Category = "TV Series", Rating = "PG", Year = 2020, ImgSmall = "/images/movie6.jpg", ImgMedium = "/images/movie6.jpg", ImgLarge = "/images/movie6.jpg" }
      };
    }

    [Fact]
    public async Task GetAll_ReturnsAllMovies()
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.GetByCategory("Movie")).ReturnsAsync(GetTestMovies().Where(m => m.Category == "Movie"));

        var service = new MovieService(repository.Object);
        var result = await service.GetAll();

        Assert.NotNull(result);
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GetAll_ReturnsNoMovies()
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.GetByCategory("Movie")).ReturnsAsync(new List<Movie>());

        var service = new MovieService(repository.Object);
        var result = await service.GetAll();

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Theory]
    [InlineData(1, 3)]
    [InlineData(2, 3)]
    public async Task GetAllPaginated_ReturnsMovies(int pageNumber, int pageSize)
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.CountByCategory("Movie")).ReturnsAsync(3);
        repository.Setup(repo => repo.GetByCategoryPaginated("Movie", pageNumber, pageSize)).ReturnsAsync(GetTestMovies().Where(m => m.Category == "Movie"));

        var service = new MovieService(repository.Object);
        var result = await service.GetByCategoryPaginated("Movie", pageNumber, pageSize);

        Assert.NotNull(result);
        Assert.Equal(3, result.Data.Count());
        Assert.Equal(pageNumber, result.PageNumber);
        Assert.Equal(pageSize, result.PageSize);
        Assert.Equal(1, result.TotalPages);
    }

    public async Task GetAllPaginated_ReturnsAllMovies()
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.CountByCategory("Movie")).ReturnsAsync(3);
        repository.Setup(repo => repo.GetByCategoryPaginated("Movie", 1, 3)).ReturnsAsync(GetTestMovies().Where(m => m.Category == "Movie"));

        var service = new MovieService(repository.Object);
        var result = await service.GetByCategoryPaginated("Movie", 1, 3);

        Assert.NotNull(result);
        Assert.Equal(3, result.Data.Count());
        Assert.Equal(1, result.PageNumber);
        Assert.Equal(3, result.PageSize);
        Assert.Equal(1, result.TotalPages);
    }

    [Fact]
    public async Task GetAllPaginated_ThrowsException()
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.CountByCategory("Movie")).ReturnsAsync(0);
        repository.Setup(repo => repo.GetByCategoryPaginated("Movie", 0, 3)).ThrowsAsync(new Exception("Invalid page number"));

        var service = new MovieService(repository.Object);

        await Assert.ThrowsAsync<Exception>(() => service.GetByCategoryPaginated("Movie", 0, 3));
    }

    [Fact]
    public async Task GetAllPaginated_InvalidPageSize_ThrowsException()
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.CountByCategory("Movie")).ReturnsAsync(3);
        repository.Setup(repo => repo.GetByCategoryPaginated("Movie", 1, 0)).ThrowsAsync(new Exception("Invalid page size"));

        var service = new MovieService(repository.Object);

        await Assert.ThrowsAsync<Exception>(() => service.GetAllPaginated(1, 0));
    }
}
