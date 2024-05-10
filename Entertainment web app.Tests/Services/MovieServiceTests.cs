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
        repository
          .Setup(repo => repo.GetByCategory("Movies"))
          .ReturnsAsync(GetTestMovies().Where(m => m.Category == "Movies"));

        var service = new MovieService(repository.Object);
        var result = await service.GetAll();

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.Data?.Count());
    }

    [Fact]
    public async Task GetAll_ReturnsNoMovies()
    {
        var repository = new Mock<IMovieRepository>();
        repository
          .Setup(repo => repo.GetByCategory("Movies"))
          .ReturnsAsync(new List<Movie>());

        var service = new MovieService(repository.Object);
        var result = await service.GetAll();

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Matches("NotFound", result.Error.Code);
        Assert.Null(result.Data);
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
        Assert.True(result.IsSuccess);
        Assert.Equal(pageNumber, result.Data?.PageNumber);
        Assert.Equal(pageSize, result.Data?.PageSize);
        Assert.Equal(3, result.Data?.TotalPages);
    }

    [Theory]
    [InlineData(-1, 3)]
    [InlineData(0, 3)]
    [InlineData(4, 3)]
    public async Task GetAllPaginated_InvalidPageNumber_ReturnsErrorResult(int pageNumber, int pageSize)
    {
        var repository = new Mock<IMovieRepository>();
        repository
          .Setup(repo => repo.CountByCategory("Movies"))
          .ReturnsAsync(3);

        repository
          .Setup(repo => repo.GetByCategoryPaginated("Movies", pageNumber, pageSize))
          .ReturnsAsync(new List<Movie>());

        var service = new MovieService(repository.Object);
        var result = await service.GetAllPaginated(pageNumber, pageSize);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Matches("InvalidPageNumber", result.Error.Code);
        Assert.Null(result.Data);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(1, -1)]
    [InlineData(1, 21)]
    public async Task GetAllPaginated_InvalidPageSize_ReturnsErrorResult(int pageNumber, int pageSize)
    {
        var repository = new Mock<IMovieRepository>();
        repository
          .Setup(repo => repo.CountByCategory("Movies"))
          .ReturnsAsync(3);

        repository
          .Setup(repo => repo.GetByCategoryPaginated("Movies", pageNumber, pageSize))
          .ReturnsAsync(new List<Movie>());

        var service = new MovieService(repository.Object);
        var result = await service.GetAllPaginated(pageNumber, pageSize);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Matches("InvalidPageSize", result.Error.Code);
        Assert.Null(result.Data);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetById_ReturnsMovie(int movieId)
    {
        var repository = new Mock<IMovieRepository>();
        repository
          .Setup(repo => repo
          .GetById(movieId))
          .ReturnsAsync(GetTestMovies().FirstOrDefault(m => m.MovieId == movieId)!);

        var service = new MovieService(repository.Object);
        var result = await service.GetById(movieId);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(movieId, result.Data?.MovieId);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    public async Task GetById_NotAMovie_ReturnsErrorResult(int movieId)
    {
        var repository = new Mock<IMovieRepository>();
        repository
          .Setup(repo => repo.GetById(movieId))
          .ReturnsAsync(GetTestMovies().Where(m => m.MovieId == movieId).FirstOrDefault());

        var service = new MovieService(repository.Object);
        var result = await service.GetById(movieId);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Matches("NotFound", result.Error.Code);
        Assert.Null(result.Data);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(7)]
    [InlineData(8)]
    public async Task GetById_InvalidMovieId_ReturnsErrorResult(int movieId)
    {
        var repository = new Mock<IMovieRepository>();
        repository
          .Setup(repo => repo.GetById(movieId))
          .ReturnsAsync(GetTestMovies().Where(m => m.MovieId == movieId).FirstOrDefault());

        var service = new MovieService(repository.Object);
        var result = await service.GetById(movieId);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Matches("NotFound", result.Error.Code);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task Add_AddsMovie()
    {
        var repository = new Mock<IMovieRepository>();
        var movie = new Movie { MovieId = 7, Title = "Movie 7", Category = "Movies", Rating = "PG", Year = 2022, ImgSmall = "/images/movie7.jpg", ImgMedium = "/images/movie7.jpg", ImgLarge = "/images/movie7.jpg" };

        var service = new MovieService(repository.Object);
        var result = await service.Add(movie);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async void Add_MovieAlreadyExists_ReturnsErrorResult(int movieId)
    {
        var movie = new Movie { MovieId = movieId, Title = "Movie", Category = "Movies", Rating = "PG", Year = 1998, ImgSmall = "/images/movie1.jpg", ImgMedium = "/images/movie1.jpg", ImgLarge = "/images/movie1.jpg" };

        var repository = new Mock<IMovieRepository>();
        repository
          .Setup(repo => repo.GetById(movie.MovieId))
          .ReturnsAsync(GetTestMovies().FirstOrDefault(m => m.MovieId == movieId));

        var service = new MovieService(repository.Object);
        var result = await service.Add(movie);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Matches("AlreadyExists", result.Error.Code);
    }

    [Theory]
    [InlineData("TV Series")]
    [InlineData("TV Shows")]
    [InlineData("Documentaries")]
    public async void Add_InvalidCategory_ReturnsErrorResult(string category)
    {
        var movie = new Movie { MovieId = 10, Title = "TV Series", Category = category, Rating = "PG", Year = 1998, ImgSmall = "/images/movie1.jpg", ImgMedium = "/images/movie1.jpg", ImgLarge = "/images/movie1.jpg" };

        var repository = new Mock<IMovieRepository>();
        repository
          .Setup(repo => repo.GetById(movie.MovieId))
          .ReturnsAsync(GetTestMovies().FirstOrDefault(m => m.MovieId == movie.MovieId));

        var service = new MovieService(repository.Object);
        var result = await service.Add(movie);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Matches("InvalidCategory", result.Error.Code);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task Update_UpdatesMovie(int movieId)
    {
        var movie = new Movie { MovieId = movieId, Title = "Updated", Category = "Movies", Rating = "PG", Year = 1998, ImgSmall = "/images/movie1.jpg", ImgMedium = "/images/movie1.jpg", ImgLarge = "/images/movie1.jpg" };

        var repository = new Mock<IMovieRepository>();
        repository
          .Setup(repo => repo.GetById(movie.MovieId))
          .ReturnsAsync(GetTestMovies().FirstOrDefault(m => m.MovieId == movieId));

        var service = new MovieService(repository.Object);
        var result = await service.Update(movie);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Matches("Updated", movie.Title);
    }

    [Theory]
    [InlineData("TV Series")]
    [InlineData("TV Shows")]
    [InlineData("Documentaries")]
    public async Task Update_InvalidCategory_ReturnsErrorResult(string category)
    {
        var movie = new Movie { MovieId = 1, Title = "Movie", Category = category, Rating = "PG", Year = 2022, ImgSmall = "/images/movie7.jpg", ImgMedium = "/images/movie7.jpg", ImgLarge = "/images/movie7.jpg" };

        var repository = new Mock<IMovieRepository>();
        repository
          .Setup(repo => repo.GetById(movie.MovieId))
          .ReturnsAsync(GetTestMovies().FirstOrDefault(m => m.MovieId == movie.MovieId));

        var service = new MovieService(repository.Object);
        var result = await service.Update(movie);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Matches("InvalidCategory", result.Error.Code);
    }

    [Theory]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(-1)]
    public async Task Update_DoesNotExist_ReturnsErrorResult(int movieId)
    {
        var movie = new Movie { MovieId = movieId, Title = "Movie", Category = "Movies", Rating = "PG", Year = 2022, ImgSmall = "/images/movie7.jpg", ImgMedium = "/images/movie7.jpg", ImgLarge = "/images/movie7.jpg" };

        var repository = new Mock<IMovieRepository>();
        repository
          .Setup(repo => repo.GetById(movie.MovieId))
          .ReturnsAsync(GetTestMovies().FirstOrDefault(m => m.MovieId == movieId));

        var service = new MovieService(repository.Object);
        var result = await service.Update(movie);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Matches("NotFound", result.Error.Code);
    }


    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task Delete_DeletesMovie(int movieId)
    {
        var repository = new Mock<IMovieRepository>();
        repository
          .Setup(repo => repo.GetById(movieId))
          .ReturnsAsync(GetTestMovies().FirstOrDefault(m => m.MovieId == movieId));

        var service = new MovieService(repository.Object);
        var result = await service.Delete(movieId);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    public async void Delete_MovieDoesNotExist_ThrowsException(int movieId)
    {
        var repository = new Mock<IMovieRepository>();
        repository
          .Setup(repo => repo.GetById(movieId))
          .ReturnsAsync(GetTestMovies().FirstOrDefault(m => m.MovieId == movieId));

        var service = new MovieService(repository.Object);
        var result = await service.Delete(movieId);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Matches("NotFound", result.Error.Code);
    }
}
