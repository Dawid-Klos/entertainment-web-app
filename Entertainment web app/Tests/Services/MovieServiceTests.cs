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
        new Movie { MovieId = 1, Title = "Movie 1", Category = "Movies", Rating = "PG", Year = 1998, ImgSmall = "/images/movie1.jpg", ImgMedium = "/images/movie1.jpg", ImgLarge = "/images/movie1.jpg" },
        new Movie { MovieId = 2, Title = "Movie 2", Category = "Movies", Rating = "PG", Year = 2020, ImgSmall = "/images/movie2.jpg", ImgMedium = "/images/movie2.jpg", ImgLarge = "/images/movie2.jpg" },
        new Movie { MovieId = 3, Title = "Movie 3", Category = "Movies", Rating = "PG", Year = 2021, ImgSmall = "/images/movie3.jpg", ImgMedium = "/images/movie3.jpg", ImgLarge = "/images/movie3.jpg" },
        new Movie { MovieId = 4, Title = "Movie 4", Category = "TV Series", Rating = "PG", Year = 2018, ImgSmall = "/images/movie4.jpg", ImgMedium = "/images/movie4.jpg", ImgLarge = "/images/movie4.jpg" },
        new Movie { MovieId = 5, Title = "Movie 5", Category = "TV Series", Rating = "PG", Year = 2019, ImgSmall = "/images/movie5.jpg", ImgMedium = "/images/movie5.jpg", ImgLarge = "/images/movie5.jpg" },
        new Movie { MovieId = 6, Title = "Movie 6", Category = "TV Series", Rating = "PG", Year = 2020, ImgSmall = "/images/movie6.jpg", ImgMedium = "/images/movie6.jpg", ImgLarge = "/images/movie6.jpg" }
      };
    }

    [Fact]
    public async Task GetAll_ReturnsAllMovies()
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.GetByCategory("Movies")).ReturnsAsync(GetTestMovies().Where(m => m.Category == "Movies"));

        var service = new MovieService(repository.Object);
        var result = await service.GetAll();

        Assert.NotNull(result);
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GetAll_ReturnsNoMovies()
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.GetByCategory("Movies")).ReturnsAsync(new List<Movie>());

        var service = new MovieService(repository.Object);
        var result = await service.GetAll();

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAll_ThrowsException()
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.GetByCategory("Movies")).ThrowsAsync(new Exception("No movies found"));

        var service = new MovieService(repository.Object);

        await Assert.ThrowsAsync<Exception>(() => service.GetAll());
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 1)]
    [InlineData(3, 1)]
    public async Task GetAllPaginated_ReturnsMovies(int pageNumber, int pageSize)
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.CountByCategory("Movies")).ReturnsAsync(3);
        repository.Setup(repo => repo.GetByCategoryPaginated("Movies", pageNumber, pageSize)).ReturnsAsync(GetTestMovies().Where(m => m.Category == "Movies"));

        var service = new MovieService(repository.Object);
        var result = await service.GetAllPaginated(pageNumber, pageSize);

        Assert.NotNull(result);
        Assert.Equal(pageNumber, result.PageNumber);
        Assert.Equal(pageSize, result.PageSize);
        Assert.Equal(3, result.TotalPages);
    }

    [Theory]
    [InlineData(-1, 3)]
    [InlineData(0, 3)]
    [InlineData(4, 3)]
    public async Task GetAllPaginated_InvalidPageNumber_ThrowsException(int pageNumber, int pageSize)
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.CountByCategory("Movies")).ReturnsAsync(0);
        repository.Setup(repo => repo.GetByCategoryPaginated("Movies", pageNumber, pageSize)).ThrowsAsync(new ArgumentException("Invalid page number"));

        var service = new MovieService(repository.Object);

        await Assert.ThrowsAsync<ArgumentException>(() => service.GetAllPaginated(pageNumber, pageSize));
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(2, 0)]
    [InlineData(3, 0)]
    public async Task GetAllPaginated_InvalidPageSize_ThrowsException(int pageNumber, int pageSize)
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.CountByCategory("Movies")).ReturnsAsync(3);
        repository.Setup(repo => repo.GetByCategoryPaginated("Movies", pageNumber, pageSize)).ThrowsAsync(new ArgumentException("Invalid page size"));

        var service = new MovieService(repository.Object);

        await Assert.ThrowsAsync<ArgumentException>(() => service.GetAllPaginated(pageNumber, pageSize));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetById_ReturnsMovie(int movieId)
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.GetById(movieId))
          .ReturnsAsync(GetTestMovies().FirstOrDefault(m => m.MovieId == movieId)!);

        var service = new MovieService(repository.Object);
        var result = await service.GetById(movieId);

        Assert.NotNull(result);
        Assert.Equal(movieId, result.MovieId);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    public async Task GetById_NotAMovie_ThrowsException(int movieId)
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.GetById(movieId))
          .ThrowsAsync(new ArgumentException($"Movie with ID = {movieId} does not exist"));
        var service = new MovieService(repository.Object);

        await Assert.ThrowsAsync<ArgumentException>(() => service.GetById(movieId));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(7)]
    [InlineData(8)]
    public async Task GetById_InvalidMovieId_ThrowsException(int movieId)
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.GetById(movieId))
          .ThrowsAsync(new ArgumentException($"Movie with ID = {movieId} does not exist"));

        var service = new MovieService(repository.Object);

        await Assert.ThrowsAsync<ArgumentException>(() => service.GetById(movieId));
    }

    [Fact]
    public void Add_AddsMovie()
    {
        var repository = new Mock<IMovieRepository>();
        var movie = new Movie { MovieId = 7, Title = "Movie 7", Category = "Movies", Rating = "PG", Year = 2022, ImgSmall = "/images/movie7.jpg", ImgMedium = "/images/movie7.jpg", ImgLarge = "/images/movie7.jpg" };

        var service = new MovieService(repository.Object);
        service.Add(movie);

        repository.Verify(repo => repo.Add(movie), Times.Once);
        repository.Verify(repo => repo.GetById(movie.MovieId), Times.Once);
    }

    [Fact]
    public async void Add_MovieAlreadyExists_ThrowsException()
    {
        var movie = new Movie { MovieId = 1, Title = "Movie 1", Category = "Movies", Rating = "PG", Year = 1998, ImgSmall = "/images/movie1.jpg", ImgMedium = "/images/movie1.jpg", ImgLarge = "/images/movie1.jpg" };

        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.GetById(movie.MovieId))
          .ThrowsAsync(new ArgumentException("Movie already exists"));

        var service = new MovieService(repository.Object);

        await Assert.ThrowsAsync<ArgumentException>(() => service.GetById(movie.MovieId));
    }

    [Fact]
    public void Update_UpdatesMovie()
    {
        var movie = new Movie { MovieId = 1, Title = "Updated", Category = "Movies", Rating = "PG", Year = 1998, ImgSmall = "/images/movie1.jpg", ImgMedium = "/images/movie1.jpg", ImgLarge = "/images/movie1.jpg" };
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.GetById(movie.MovieId)).ReturnsAsync(movie);

        var service = new MovieService(repository.Object);
        service.Update(movie);

        repository.Verify(repo => repo.Update(movie), Times.Once);
        repository.Verify(repo => repo.GetById(movie.MovieId), Times.Once);
        Assert.Equal("Updated", movie.Title);
    }

    [Fact]
    public async void Update_MovieDoesNotExist_ThrowsException()
    {
        var movie = new Movie { MovieId = 7, Title = "Movie 7", Category = "Movies", Rating = "PG", Year = 2022, ImgSmall = "/images/movie7.jpg", ImgMedium = "/images/movie7.jpg", ImgLarge = "/images/movie7.jpg" };

        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.GetById(movie.MovieId))
          .ThrowsAsync(new ArgumentException("Movie does not exist"));

        var service = new MovieService(repository.Object);

        await Assert.ThrowsAsync<ArgumentException>(() => service.GetById(movie.MovieId));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Delete_DeletesMovie(int movieId)
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.GetById(movieId)).ReturnsAsync(GetTestMovies().FirstOrDefault(m => m.MovieId == movieId)!);

        var service = new MovieService(repository.Object);
        service.Delete(movieId);

        repository.Verify(repo => repo.Delete(movieId), Times.Once);
        repository.Verify(repo => repo.GetById(movieId), Times.Once);
    }

    [Theory]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    public async void Delete_MovieDoesNotExist_ThrowsException(int movieId)
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo.GetById(movieId))
          .ThrowsAsync(new ArgumentException("Movie does not exist"));

        var service = new MovieService(repository.Object);

        await Assert.ThrowsAsync<ArgumentException>(() => service.GetById(movieId));
    }
}
