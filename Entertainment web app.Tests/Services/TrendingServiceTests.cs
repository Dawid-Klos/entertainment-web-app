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
    public async Task GetAll_ReturnsTrue()
    {
        var repository = new Mock<ITrendingRepository>();
        repository
          .Setup(repo => repo.GetAll())
          .ReturnsAsync(GetTestTrending());

        var service = new TrendingService(repository.Object);
        var result = await service.GetAll();

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(6, result.Data?.Count());
    }

    [Fact]
    public async Task GetAll_ReturnsFalse()
    {
        var repository = new Mock<ITrendingRepository>();
        repository
          .Setup(repo => repo.GetAll())
          .ReturnsAsync(() => null!);

        var service = new TrendingService(repository.Object);
        var result = await service.GetAll();

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("NotFound", result.Error?.Code);
        Assert.Equal("No trending movies found", result.Error?.Description);
    }
}
