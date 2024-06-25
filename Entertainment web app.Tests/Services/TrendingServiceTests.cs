using Microsoft.AspNetCore.Http;

using Entertainment_web_app.Repositories;
using Entertainment_web_app.Services;
using Entertainment_web_app.Data;

namespace Entertainment_web_app.Tests.Services;

public class TrendingServiceTests
{
    private List<Trending> GetTestTrending()
    {
        return new List<Trending>
      {
        new Trending { TrendingId = 1, ImgTrendingSmall = "img1", ImgTrendingLarge = "img1",
          Movie = new Movie { MovieId = 1, Title = "title1", Year = 2021, Category = "Movies", Rating = "PG" } },
        new Trending { TrendingId = 2, ImgTrendingSmall = "img2", ImgTrendingLarge = "img2",
          Movie = new Movie { MovieId = 2, Title = "title2", Year = 2022, Category = "Movies", Rating = "PG" } },
        new Trending { TrendingId = 3, ImgTrendingSmall = "img3", ImgTrendingLarge = "img3",
          Movie = new Movie { MovieId = 3, Title = "title3", Year = 2023, Category = "Movies", Rating = "PG" } },
        new Trending { TrendingId = 4, ImgTrendingSmall = "img4", ImgTrendingLarge = "img4",
          Movie = new Movie { MovieId = 4, Title = "title4", Year = 2024, Category = "TV Series", Rating = "PG" } },
        new Trending { TrendingId = 5, ImgTrendingSmall = "img5", ImgTrendingLarge = "img5",
          Movie = new Movie { MovieId = 5, Title = "title5", Year = 2025, Category = "TV Series", Rating = "PG" } },
        new Trending { TrendingId = 6, ImgTrendingSmall = "img6", ImgTrendingLarge = "img6",
          Movie = new Movie { MovieId = 6, Title = "title6", Year = 2026, Category = "TV Series", Rating = "PG" } }
      };
    }

    [Fact]
    public async Task GetAll_ReturnsSuccess()
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor
          .Setup(hca => hca.HttpContext!.User.IsInRole("Admin"))
          .Returns(false);

        var bookmarkRepository = new Mock<IBookmarkRepository>();

        var repository = new Mock<ITrendingRepository>();
        repository
          .Setup(repo => repo.GetAll())
          .ReturnsAsync(GetTestTrending());

        var service = new TrendingService(repository.Object, bookmarkRepository.Object, httpContextAccessor.Object);
        var result = await service.GetAll();

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(6, result.Data?.Count());
    }

    [Fact]
    public async Task GetAll_ReturnsNotFound()
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor
          .Setup(hca => hca.HttpContext!.User.IsInRole("Admin"))
          .Returns(false);

        var bookmarkRepository = new Mock<IBookmarkRepository>();

        var repository = new Mock<ITrendingRepository>();
        repository
          .Setup(repo => repo.GetAll())
          .ReturnsAsync(() => null!);

        var service = new TrendingService(repository.Object, bookmarkRepository.Object, httpContextAccessor.Object);
        var result = await service.GetAll();

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Matches("NotFound", result.Error?.Code);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetById_ReturnsTrending(int trendingId)
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor
          .Setup(hca => hca.HttpContext!.User.IsInRole("Admin"))
          .Returns(false);

        var bookmarkRepository = new Mock<IBookmarkRepository>();

        var repository = new Mock<ITrendingRepository>();
        repository
          .Setup(repo => repo.GetById(trendingId))
          .ReturnsAsync(GetTestTrending().Where(t => t.TrendingId == trendingId).FirstOrDefault());

        var service = new TrendingService(repository.Object, bookmarkRepository.Object, httpContextAccessor.Object);
        var result = await service.GetById(trendingId);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(trendingId, result.Data?.MovieId);
    }


    [Theory]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    public async Task GetById_ReturnsNotFound(int trendingId)
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor
          .Setup(hca => hca.HttpContext!.User.IsInRole("Admin"))
          .Returns(false);

        var bookmarkRepository = new Mock<IBookmarkRepository>();

        var repository = new Mock<ITrendingRepository>();
        repository
          .Setup(repo => repo.GetById(trendingId))
          .ReturnsAsync(() => null!);

        var service = new TrendingService(repository.Object, bookmarkRepository.Object, httpContextAccessor.Object);
        var result = await service.GetById(trendingId);

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Matches("NotFound", result.Error?.Code);
        Assert.Matches($"Trending movie with ID = {trendingId} does not exist", result.Error?.Description);
    }

    [Fact]
    public async Task Add_ReturnsSuccess()
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor
          .Setup(hca => hca.HttpContext!.User.IsInRole("Admin"))
          .Returns(false);

        var bookmarkRepository = new Mock<IBookmarkRepository>();

        var trending = new Trending
        {
            TrendingId = 7,
            ImgTrendingSmall = "img7",
            ImgTrendingLarge = "img7",
            Movie = new Movie { MovieId = 7, Title = "title7", Year = 2027, Category = "Movies", Rating = "PG" }
        };

        var repository = new Mock<ITrendingRepository>();
        repository
          .Setup(repo => repo.GetById(trending.TrendingId))
          .ReturnsAsync(() => null!);

        var service = new TrendingService(repository.Object, bookmarkRepository.Object, httpContextAccessor.Object);
        var result = await service.Add(trending);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task Add_ReturnsAlreadyExists(int trendingId)
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor
          .Setup(hca => hca.HttpContext!.User.IsInRole("Admin"))
          .Returns(false);

        var bookmarkRepository = new Mock<IBookmarkRepository>();

        var newTrending = new Trending
        {
            TrendingId = trendingId,
            ImgTrendingSmall = "img1",
            ImgTrendingLarge = "img1",
            Movie = new Movie { MovieId = 1, Title = "title1", Year = 2021, Category = "Movies", Rating = "PG" }
        };

        var repository = new Mock<ITrendingRepository>();
        repository
          .Setup(repo => repo.GetByMovieId(newTrending.MovieId))
          .ReturnsAsync(GetTestTrending().Where(t => t.MovieId == newTrending.MovieId).FirstOrDefault());

        repository
          .Setup(repo => repo.GetById(trendingId))
          .ReturnsAsync(GetTestTrending().Where(t => t.TrendingId == trendingId).FirstOrDefault());

        var service = new TrendingService(repository.Object, bookmarkRepository.Object, httpContextAccessor.Object);
        var result = await service.Add(newTrending);

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Matches("AlreadyExists", result.Error?.Code);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task Update_ReturnsSuccess(int trendingId)
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor
          .Setup(hca => hca.HttpContext!.User.IsInRole("Admin"))
          .Returns(false);

        var bookmarkRepository = new Mock<IBookmarkRepository>();

        var trending = GetTestTrending().Where(t => t.TrendingId == trendingId).FirstOrDefault()!;

        var repository = new Mock<ITrendingRepository>();
        repository
          .Setup(repo => repo.GetById(trendingId))
          .ReturnsAsync(trending);

        var service = new TrendingService(repository.Object, bookmarkRepository.Object, httpContextAccessor.Object);
        var result = await service.Update(trending);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    public async Task Update_ReturnsNotFound(int trendingId)
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor
          .Setup(hca => hca.HttpContext!.User.IsInRole("Admin"))
          .Returns(false);

        var bookmarkRepository = new Mock<IBookmarkRepository>();

        var trending = new Trending
        {
            TrendingId = trendingId,
            ImgTrendingSmall = "img1",
            ImgTrendingLarge = "img1",
            Movie = new Movie { MovieId = 1, Title = "title1", Year = 2021, Category = "Movies", Rating = "PG" }
        };

        var repository = new Mock<ITrendingRepository>();
        repository
          .Setup(repo => repo.GetById(trendingId))
          .ReturnsAsync(() => null!);

        var service = new TrendingService(repository.Object, bookmarkRepository.Object, httpContextAccessor.Object);
        var result = await service.Update(trending);

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Matches("NotFound", result.Error?.Code);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task Update_ReturnsBadRequest(int trendingId)
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor
          .Setup(hca => hca.HttpContext!.User.IsInRole("Admin"))
          .Returns(false);

        var bookmarkRepository = new Mock<IBookmarkRepository>();

        var trending = GetTestTrending().Where(t => t.TrendingId == trendingId).FirstOrDefault()!;
        trending = null!;

        var repository = new Mock<ITrendingRepository>();
        repository
          .Setup(repo => repo.GetById(trendingId))
          .ReturnsAsync(trending);

        var service = new TrendingService(repository.Object, bookmarkRepository.Object, httpContextAccessor.Object);
        var result = await service.Update(trending);

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Matches("BadRequest", result.Error?.Code);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task Delete_ReturnsSuccess(int trendingId)
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor
          .Setup(hca => hca.HttpContext!.User.IsInRole("Admin"))
          .Returns(false);

        var bookmarkRepository = new Mock<IBookmarkRepository>();

        var trending = GetTestTrending().Where(t => t.TrendingId == trendingId).FirstOrDefault()!;

        var repository = new Mock<ITrendingRepository>();
        repository
          .Setup(repo => repo.GetById(trendingId))
          .ReturnsAsync(trending);

        var service = new TrendingService(repository.Object, bookmarkRepository.Object, httpContextAccessor.Object);
        var result = await service.Delete(trendingId);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    public async Task Delete_ReturnsNotFound(int trendingId)
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor
          .Setup(hca => hca.HttpContext!.User.IsInRole("Admin"))
          .Returns(false);

        var bookmarkRepository = new Mock<IBookmarkRepository>();

        var repository = new Mock<ITrendingRepository>();
        repository
          .Setup(repo => repo.GetById(trendingId))
          .ReturnsAsync(() => null!);

        var service = new TrendingService(repository.Object, bookmarkRepository.Object, httpContextAccessor.Object);
        var result = await service.Delete(trendingId);

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Matches("NotFound", result.Error?.Code);
    }
}
