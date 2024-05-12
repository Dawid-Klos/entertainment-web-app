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
          .Setup(repo => repo.GetAll())
          .ReturnsAsync(GetTestMovies());

        var service = new MovieService(repository.Object);
        var result = await service.GetAll(1, 10);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(6, result.Data?.TotalRecords);
        Assert.Equal(1, result.Data?.PageNumber);
        Assert.Equal(1, result.Data?.PageSize);
    }

    [Fact]
    public async Task GetAll_ReturnsNoMovies()
    {

        var repository = new Mock<IMovieRepository>();
        repository
          .Setup(repo => repo.GetAll())
          .ReturnsAsync(new List<Movie>());

        var service = new MovieService(repository.Object);
        var result = await service.GetAll(1, 10);

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Matches("NotFound", result.Error.Code);
        Assert.Null(result.Data);
    }

    [Theory]
    [InlineData(MediaCategory.Movies, 1, 1)]
    [InlineData(MediaCategory.Movies, 2, 1)]
    [InlineData(MediaCategory.Movies, 3, 1)]
    [InlineData(MediaCategory.TVSeries, 1, 1)]
    [InlineData(MediaCategory.TVSeries, 2, 1)]
    [InlineData(MediaCategory.TVSeries, 3, 1)]
    public async Task GetByCategory_ReturnsCategoryContent(MediaCategory category, int pageNumber, int pageSize)
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo
          .CountByCategory(category.ToString()))
          .ReturnsAsync(3);

        repository.Setup(repo => repo
          .GetByCategoryPaginated(category.ToString(), pageNumber, pageSize))
          .ReturnsAsync(GetTestMovies().Where(m => m.Category == category.ToString()));

        var service = new MovieService(repository.Object);
        var result = await service.GetByCategory(category, pageNumber, pageSize);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(pageNumber, result.Data?.PageNumber);
        Assert.Equal(pageSize, result.Data?.PageSize);
        Assert.Equal(3, result.Data?.TotalPages);
        Assert.Equal(3, result.Data?.TotalRecords);
        Assert.Equal(category.ToString(), result.Data?.Data?.FirstOrDefault()?.Category);
    }

    [Theory]
    [InlineData(MediaCategory.Movies, 4, 1)]
    [InlineData(MediaCategory.Movies, 5, 1)]
    [InlineData(MediaCategory.Movies, 6, 1)]
    [InlineData(MediaCategory.TVSeries, 4, 1)]
    [InlineData(MediaCategory.TVSeries, 5, 1)]
    [InlineData(MediaCategory.TVSeries, 6, 1)]
    public async Task GetByCategory_InvalidPageNumber_ReturnsErrorResult(MediaCategory category, int pageNumber, int pageSize)
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo
          .CountByCategory(category.ToString()))
          .ReturnsAsync(3);

        repository.Setup(repo => repo
          .GetByCategoryPaginated(category.ToString(), pageNumber, pageSize))
          .ReturnsAsync(GetTestMovies().Where(m => m.Category == category.ToString()));

        var service = new MovieService(repository.Object);
        var result = await service.GetByCategory(category, pageNumber, pageSize);

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Equal(pageNumber, result.Data?.PageNumber);
        Assert.Equal(0, result.Data?.PageSize);
        Assert.Equal(3, result.Data?.TotalPages);
        Assert.Equal(3, result.Data?.TotalRecords);
        Assert.Matches("InvalidPageNumber", result.Error.Code);
        Assert.Null(result.Data);
    }

    [Theory]
    [InlineData(MediaCategory.Movies, 1, -1)]
    [InlineData(MediaCategory.Movies, 2, 12)]
    [InlineData(MediaCategory.Movies, 3, 30)]
    [InlineData(MediaCategory.TVSeries, 1, -1)]
    [InlineData(MediaCategory.TVSeries, 2, 12)]
    [InlineData(MediaCategory.TVSeries, 3, 30)]
    public async Task GetByCategory_InvalidPageSize_ReturnsErrorResult(MediaCategory category, int pageNumber, int pageSize)
    {
        var repository = new Mock<IMovieRepository>();
        repository.Setup(repo => repo
          .CountByCategory(category.ToString()))
          .ReturnsAsync(3);

        repository.Setup(repo => repo
          .GetByCategoryPaginated(category.ToString(), pageNumber, pageSize))
          .ReturnsAsync(GetTestMovies().Where(m => m.Category == category.ToString()));

        var service = new MovieService(repository.Object);
        var result = await service.GetByCategory(category, pageNumber, pageSize);

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Equal(pageNumber, result.Data?.PageNumber);
        Assert.Equal(0, result.Data?.PageSize);
        Assert.Equal(3, result.Data?.TotalPages);
        Assert.Equal(3, result.Data?.TotalRecords);
        Assert.Matches("InvalidPageSize", result.Error.Code);
        Assert.Null(result.Data);
    }


    [Theory]
    [InlineData(MediaCategory.Movies, 1)]
    [InlineData(MediaCategory.Movies, 2)]
    [InlineData(MediaCategory.Movies, 3)]
    [InlineData(MediaCategory.TVSeries, 4)]
    [InlineData(MediaCategory.TVSeries, 5)]
    [InlineData(MediaCategory.TVSeries, 6)]
    public async Task GetById_ReturnsMovieFromCategory(MediaCategory category, int movieId)
    {
        var repository = new Mock<IMovieRepository>();
        repository
          .Setup(repo => repo
          .GetById(movieId))
          .ReturnsAsync(GetTestMovies().Where(m => m.MovieId == movieId)
          .FirstOrDefault());

        var service = new MovieService(repository.Object);
        var result = await service.GetById(category, movieId);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(movieId, result.Data?.MovieId);
        Assert.Equal(category.ToString(), result.Data?.Category);
    }

    [Theory]
    [InlineData(MediaCategory.Movies, 4)]
    [InlineData(MediaCategory.Movies, 5)]
    [InlineData(MediaCategory.Movies, 6)]
    [InlineData(MediaCategory.TVSeries, 1)]
    [InlineData(MediaCategory.TVSeries, 2)]
    [InlineData(MediaCategory.TVSeries, 3)]
    public async Task GetById_WrongCategory_ReturnsErrorResult(MediaCategory category, int movieId)
    {
        var repository = new Mock<IMovieRepository>();
        repository
          .Setup(repo => repo.GetById(movieId))
          .ReturnsAsync(GetTestMovies().Where(m => m.MovieId == movieId)
          .FirstOrDefault());

        var service = new MovieService(repository.Object);
        var result = await service.GetById(category, movieId);

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Matches("InvalidCategory", result.Error.Code);
        Assert.Null(result.Data);
    }

    [Theory]
    [InlineData(MediaCategory.Movies, 0)]
    [InlineData(MediaCategory.Movies, -1)]
    [InlineData(MediaCategory.Movies, -2)]
    [InlineData(MediaCategory.TVSeries, 7)]
    [InlineData(MediaCategory.TVSeries, 8)]
    [InlineData(MediaCategory.TVSeries, 9)]
    public async Task GetById_InvalidMovieId_ReturnsErrorResult(MediaCategory category, int movieId)
    {
        var repository = new Mock<IMovieRepository>();
        repository
          .Setup(repo => repo.GetById(movieId))
          .ReturnsAsync(GetTestMovies().Where(m => m.MovieId == movieId)
          .FirstOrDefault());

        var service = new MovieService(repository.Object);
        var result = await service.GetById(category, movieId);

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
    public async void Add_InvalidCategory_ReturnsErrorResult(MediaCategory category)
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
    [InlineData(MediaCategory.Movies)]
    [InlineData(MediaCategory.TVSeries)]
    public async Task Update_InvalidCategory_ReturnsErrorResult(MediaCategory category)
    {
        var movie = new Movie { MovieId = 1, Title = "Movie", Category = category.ToString(), Rating = "PG", Year = 2022, ImgSmall = "/images/movie7.jpg", ImgMedium = "/images/movie7.jpg", ImgLarge = "/images/movie7.jpg" };

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
