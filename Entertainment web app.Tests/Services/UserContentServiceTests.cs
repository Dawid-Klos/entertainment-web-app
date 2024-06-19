using Entertainment_web_app.Models.Content;
using Entertainment_web_app.Repositories;
using Entertainment_web_app.Models.Dto;
using Entertainment_web_app.Services;
using Entertainment_web_app.Data;

namespace Entertainment_web_app.Tests;

public class UserContentServiceTests
{

    private List<Movie> GetMovies()
    {
        return new List<Movie>
      {
        new Movie { MovieId = 1, Title = "Movie 1", Category = "Movies", Rating = "PG", Year = 1998, ImgSmall = "/images/movie1.jpg", ImgMedium = "/images/movie1.jpg", ImgLarge = "/images/movie1.jpg" },
        new Movie { MovieId = 2, Title = "Movie 2", Category = "Movies", Rating = "PG", Year = 2020, ImgSmall = "/images/movie2.jpg", ImgMedium = "/images/movie2.jpg", ImgLarge = "/images/movie2.jpg" },
        new Movie { MovieId = 3, Title = "Movie 3", Category = "Movies", Rating = "PG", Year = 2021, ImgSmall = "/images/movie3.jpg", ImgMedium = "/images/movie3.jpg", ImgLarge = "/images/movie3.jpg" },
        new Movie { MovieId = 4, Title = "Movie 4", Category = "TVSeries", Rating = "PG", Year = 2018, ImgSmall = "/images/movie4.jpg", ImgMedium = "/images/movie4.jpg", ImgLarge = "/images/movie4.jpg" },
        new Movie { MovieId = 5, Title = "Movie 5", Category = "TVSeries", Rating = "PG", Year = 2019, ImgSmall = "/images/movie5.jpg", ImgMedium = "/images/movie5.jpg", ImgLarge = "/images/movie5.jpg" },
        new Movie { MovieId = 6, Title = "Movie 6", Category = "TVSeries", Rating = "PG", Year = 2020, ImgSmall = "/images/movie6.jpg", ImgMedium = "/images/movie6.jpg", ImgLarge = "/images/movie6.jpg" },
        new Movie { MovieId = 7, Title = "Movie 7", Category = "Movies", Rating = "PG", Year = 2021, ImgSmall = "/images/movie7.jpg", ImgMedium = "/images/movie7.jpg", ImgLarge = "/images/movie7.jpg" },
        new Movie { MovieId = 8, Title = "Movie 8", Category = "Movies", Rating = "PG", Year = 2021, ImgSmall = "/images/movie8.jpg", ImgMedium = "/images/movie8.jpg", ImgLarge = "/images/movie8.jpg" },
      };
    }

    private List<Bookmark> GetBookmarks()
    {
        return new List<Bookmark>
        {
            new Bookmark {  UserId = "1", MovieId = 1 },
            new Bookmark {  UserId = "1", MovieId = 2 },
            new Bookmark {  UserId = "1", MovieId = 3 },
            new Bookmark {  UserId = "1", MovieId = 4 },
            new Bookmark {  UserId = "1", MovieId = 5 },
            new Bookmark {  UserId = "1", MovieId = 6 }
        };
    }

    [Fact]
    public async Task GetMovies_ReturnsSuccess()
    {
        var paginatonQuery = new PaginationQuery
        {
            PageNumber = 1,
            PageSize = 5
        };

        var movieRepository = new Mock<IMovieRepository>();
        var bookmarkRepository = new Mock<IBookmarkRepository>();

        bookmarkRepository
          .Setup(repo => repo.GetByCategoryAndUserId("Movies", "1"))
          .ReturnsAsync(GetBookmarks());

        var movieIds = GetBookmarks().Select(b => b.MovieId);

        movieRepository
          .Setup(repo => repo.GetByIds(movieIds))
          .ReturnsAsync(GetMovies().Where(m => movieIds.Contains(m.MovieId)));

        var service = new UserContentService(movieRepository.Object, bookmarkRepository.Object);
        var result = await service.GetMovies("1", paginatonQuery);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(6, result.Data?.TotalRecords);
    }

    [Fact]
    public async Task GetMovies_InvalidPageNumber_ReturnsError()
    {
        var paginatonQuery = new PaginationQuery
        {
            PageNumber = -1,
            PageSize = 5
        };

        var movieRepository = new Mock<IMovieRepository>();
        var bookmarkRepository = new Mock<IBookmarkRepository>();

        bookmarkRepository
          .Setup(repo => repo.GetByCategoryAndUserId("Movies", "1"))
          .ReturnsAsync(GetBookmarks());

        var movieIds = GetBookmarks().Select(b => b.MovieId);

        movieRepository
          .Setup(repo => repo.GetByIds(movieIds))
          .ReturnsAsync(GetMovies().Where(m => movieIds.Contains(m.MovieId)));

        var service = new UserContentService(movieRepository.Object, bookmarkRepository.Object);
        var result = await service.GetMovies("1", paginatonQuery);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("InvalidPageNumber", result.Error?.Code);
    }

    [Fact]
    public async Task GetMovies_InvalidPageSize_ReturnsError()
    {
        var paginatonQuery = new PaginationQuery
        {
            PageNumber = 1,
            PageSize = 0
        };

        var movieRepository = new Mock<IMovieRepository>();
        var bookmarkRepository = new Mock<IBookmarkRepository>();

        bookmarkRepository
          .Setup(repo => repo.GetByCategoryAndUserId("Movies", "1"))
          .ReturnsAsync(GetBookmarks());

        var movieIds = GetBookmarks().Select(b => b.MovieId);

        movieRepository
          .Setup(repo => repo.GetByIds(movieIds))
          .ReturnsAsync(GetMovies().Where(m => movieIds.Contains(m.MovieId)));

        var service = new UserContentService(movieRepository.Object, bookmarkRepository.Object);
        var result = await service.GetMovies("1", paginatonQuery);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("InvalidPageSize", result.Error?.Code);
    }

    [Fact]
    public async Task GetTvSeries_ReturnsSuccess()
    {
        var paginatonQuery = new PaginationQuery
        {
            PageNumber = 1,
            PageSize = 5
        };

        var movieRepository = new Mock<IMovieRepository>();
        var bookmarkRepository = new Mock<IBookmarkRepository>();

        bookmarkRepository
          .Setup(repo => repo.GetByCategoryAndUserId("TVSeries", "1"))
          .ReturnsAsync(GetBookmarks());

        var movieIds = GetBookmarks().Select(b => b.MovieId);

        movieRepository
          .Setup(repo => repo.GetByIds(movieIds))
          .ReturnsAsync(GetMovies().Where(m => movieIds.Contains(m.MovieId)));

        var service = new UserContentService(movieRepository.Object, bookmarkRepository.Object);
        var result = await service.GetTvSeries("1", paginatonQuery);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(6, result.Data?.TotalRecords);
    }

    [Fact]
    public async Task GetTvSeries_InvalidPageNumber_ReturnsError()
    {
        var paginatonQuery = new PaginationQuery
        {
            PageNumber = -1,
            PageSize = 5
        };

        var movieRepository = new Mock<IMovieRepository>();
        var bookmarkRepository = new Mock<IBookmarkRepository>();

        bookmarkRepository
          .Setup(repo => repo.GetByCategoryAndUserId("TVSeries", "1"))
          .ReturnsAsync(GetBookmarks());

        var movieIds = GetBookmarks().Select(b => b.MovieId);

        movieRepository
          .Setup(repo => repo.GetByIds(movieIds))
          .ReturnsAsync(GetMovies().Where(m => movieIds.Contains(m.MovieId)));

        var service = new UserContentService(movieRepository.Object, bookmarkRepository.Object);
        var result = await service.GetTvSeries("1", paginatonQuery);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("InvalidPageNumber", result.Error?.Code);
    }

    [Fact]
    public async Task GetTvSeries_InvalidPageSize_ReturnsError()
    {
        var paginatonQuery = new PaginationQuery
        {
            PageNumber = 1,
            PageSize = 0
        };

        var movieRepository = new Mock<IMovieRepository>();
        var bookmarkRepository = new Mock<IBookmarkRepository>();

        bookmarkRepository
          .Setup(repo => repo.GetByCategoryAndUserId("TVSeries", "1"))
          .ReturnsAsync(GetBookmarks());

        var movieIds = GetBookmarks().Select(b => b.MovieId);

        movieRepository
          .Setup(repo => repo.GetByIds(movieIds))
          .ReturnsAsync(GetMovies().Where(m => movieIds.Contains(m.MovieId)));

        var service = new UserContentService(movieRepository.Object, bookmarkRepository.Object);
        var result = await service.GetTvSeries("1", paginatonQuery);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("InvalidPageSize", result.Error?.Code);
    }

    [Theory]
    [InlineData(MediaCategory.Movies, "Movie 1")]
    [InlineData(MediaCategory.Movies, "Movie 2")]
    [InlineData(MediaCategory.Movies, "Movie 3")]
    [InlineData(MediaCategory.TVSeries, "Movie 4")]
    [InlineData(MediaCategory.TVSeries, "Movie 5")]
    [InlineData(MediaCategory.TVSeries, "Movie 6")]
    public async Task SearchContent_ReturnsSearchResult(MediaCategory category, string title)
    {
        var movieRepository = new Mock<IMovieRepository>();
        var bookmarkRepository = new Mock<IBookmarkRepository>();

        bookmarkRepository
          .Setup(repo => repo.GetByCategoryAndUserId(category.ToString(), "1"))
          .ReturnsAsync(GetBookmarks());

        var movieIds = GetBookmarks().Select(b => b.MovieId);

        movieRepository
          .Setup(repo => repo.GetByIds(movieIds))
          .ReturnsAsync(GetMovies().Where(m => movieIds.Contains(m.MovieId)));

        SearchQuery query = new SearchQuery
        {
            Title = title
        };

        var service = new UserContentService(movieRepository.Object, bookmarkRepository.Object);
        var result = await service.Search("1", category, query);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Data?.Count());
    }

    [Theory]
    [InlineData(MediaCategory.Movies, "Movie 8")]
    [InlineData(MediaCategory.TVSeries, "Movie 9")]
    public async Task SearchContent_ReturnsEmptySearchResult(MediaCategory category, string title)
    {
        var movieRepository = new Mock<IMovieRepository>();
        var bookmarkRepository = new Mock<IBookmarkRepository>();

        bookmarkRepository
          .Setup(repo => repo.GetByCategoryAndUserId(category.ToString(), "1"))
          .ReturnsAsync(GetBookmarks());

        var movieIds = GetBookmarks().Select(b => b.MovieId);

        movieRepository
          .Setup(repo => repo.GetByIds(movieIds))
          .ReturnsAsync(GetMovies().Where(m => movieIds.Contains(m.MovieId)));

        SearchQuery query = new SearchQuery
        {
            Title = title
        };

        var service = new UserContentService(movieRepository.Object, bookmarkRepository.Object);
        var result = await service.Search("1", category, query);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Data);
    }

    [Fact]
    public async Task SearchContent_ReturnsAscendingOrder()
    {
        var movieRepository = new Mock<IMovieRepository>();
        var bookmarkRepository = new Mock<IBookmarkRepository>();

        bookmarkRepository
          .Setup(repo => repo.GetByCategoryAndUserId("Movies", "1"))
          .ReturnsAsync(GetBookmarks());

        var movieIds = GetBookmarks().Select(b => b.MovieId);

        movieRepository
          .Setup(repo => repo.GetByIds(movieIds))
          .ReturnsAsync(GetMovies().Where(m => movieIds.Contains(m.MovieId)));

        SearchQuery query = new SearchQuery
        {
            Title = "Movie",
            SortBy = "Title",
            IsAscending = true
        };

        var service = new UserContentService(movieRepository.Object, bookmarkRepository.Object);
        var result = await service.Search("1", MediaCategory.Movies, query);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal("Movie 1", result.Data?.First().Title);
        Assert.Equal("Movie 6", result.Data?.Last().Title);
    }

    [Fact]
    public async Task SearchContent_ReturnsDescendingOrder()
    {
        var movieRepository = new Mock<IMovieRepository>();
        var bookmarkRepository = new Mock<IBookmarkRepository>();

        bookmarkRepository
          .Setup(repo => repo.GetByCategoryAndUserId("Movies", "1"))
          .ReturnsAsync(GetBookmarks());

        var movieIds = GetBookmarks().Select(b => b.MovieId);

        movieRepository
          .Setup(repo => repo.GetByIds(movieIds))
          .ReturnsAsync(GetMovies().Where(m => movieIds.Contains(m.MovieId)));

        SearchQuery query = new SearchQuery
        {
            Title = "Movie",
            SortBy = "Title",
            IsAscending = false
        };

        var service = new UserContentService(movieRepository.Object, bookmarkRepository.Object);
        var result = await service.Search("1", MediaCategory.Movies, query);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal("Movie 6", result.Data?.First().Title);
        Assert.Equal("Movie 1", result.Data?.Last().Title);
    }
}
