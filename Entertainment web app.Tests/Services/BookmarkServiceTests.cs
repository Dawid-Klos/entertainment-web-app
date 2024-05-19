using Entertainment_web_app.Data;
using Entertainment_web_app.Repositories;
using Entertainment_web_app.Services;

namespace Entertainment_web_app.Tests.Services;

public class BookmarkServiceTests
{
    private List<Bookmark> GetBookmarks()
    {
        return new List<Bookmark>
        {
            new Bookmark {  UserId = "1", MovieId = 1 },
            new Bookmark {  UserId = "1", MovieId = 2 },
            new Bookmark {  UserId = "2", MovieId = 3 },
            new Bookmark {  UserId = "2", MovieId = 4 },
            new Bookmark {  UserId = "3", MovieId = 5 },
            new Bookmark {  UserId = "3", MovieId = 6 }
        };
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    [InlineData(2, 4)]
    public async Task GetAll_ReturnsAllBookmarks(int pageNumber, int pageSize)
    {
        var bookmarks = GetBookmarks();

        var mockRepo = new Mock<IBookmarkRepository>();
        mockRepo
          .Setup(repo => repo.CountAll())
          .ReturnsAsync(bookmarks.Count);

        mockRepo
          .Setup(repo => repo.GetAll())
          .ReturnsAsync(bookmarks);

        var service = new BookmarkService(mockRepo.Object);
        var result = await service.GetAll(pageNumber, pageSize);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(6, result.Data?.TotalRecords);
        Assert.Equal(pageNumber, result.Data?.PageNumber);
        Assert.Equal(pageSize, result.Data?.PageSize);
    }

    [Theory]
    [InlineData(2, 10)]
    [InlineData(3, 10)]
    [InlineData(-1, 1)]
    [InlineData(10, 1)]
    public async Task GetAll_InvalidPageNumber_ReturnsError(int pageNumber, int pageSize)
    {
        var bookmarks = GetBookmarks();

        var mockRepo = new Mock<IBookmarkRepository>();
        mockRepo
          .Setup(repo => repo.CountAll())
          .ReturnsAsync(bookmarks.Count);

        mockRepo
          .Setup(repo => repo.GetAll())
          .ReturnsAsync(bookmarks);

        var service = new BookmarkService(mockRepo.Object);
        var result = await service.GetAll(pageNumber, pageSize);

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Equal("InvalidPageNumber", result.Error.Code);
        Assert.Null(result.Data?.Data);
    }

    [Theory]
    [InlineData("1", 1)]
    [InlineData("1", 2)]
    [InlineData("2", 3)]
    [InlineData("2", 4)]
    public async Task GetById_ReturnsBookmark(string userId, int movieId)
    {
        var bookmarks = GetBookmarks();

        var mockRepo = new Mock<IBookmarkRepository>();
        mockRepo
          .Setup(repo => repo.GetById(userId, movieId))
          .ReturnsAsync(bookmarks.FirstOrDefault(x => x.UserId == userId && x.MovieId == movieId)!);

        var service = new BookmarkService(mockRepo.Object);
        var result = await service.GetById(userId, movieId);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(userId, result.Data?.UserId);
        Assert.Equal(movieId, result.Data?.MovieId);
    }

    [Theory]
    [InlineData("1", 10)]
    [InlineData("10", 1)]
    [InlineData("10", 10)]
    public async Task GetById_InvalidBookmark_ReturnsError(string userId, int movieId)
    {
        var bookmarks = GetBookmarks();

        var mockRepo = new Mock<IBookmarkRepository>();
        mockRepo
          .Setup(repo => repo.GetById(userId, movieId))
          .ReturnsAsync(bookmarks.FirstOrDefault(x => x.UserId == userId && x.MovieId == movieId)!);

        var service = new BookmarkService(mockRepo.Object);
        var result = await service.GetById(userId, movieId);

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Equal("NotFound", result.Error.Code);
        Assert.Null(result.Data);
    }

    [Theory]
    [InlineData("1", 1)]
    [InlineData("1", 2)]
    [InlineData("2", 3)]
    [InlineData("2", 4)]
    public async Task AddBookmark_ReturnsSuccess(string userId, int movieId)
    {
        var bookmark = new Bookmark { UserId = userId, MovieId = movieId };

        var mockRepo = new Mock<IBookmarkRepository>();
        mockRepo
          .Setup(repo => repo.Add(bookmark));

        var service = new BookmarkService(mockRepo.Object);
        var result = await service.Add(bookmark);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData("1", 1)]
    [InlineData("1", 2)]
    [InlineData("2", 3)]
    [InlineData("2", 4)]
    public async Task AddBookmark_BookmarkExists_ReturnsError(string userId, int movieId)
    {
        var bookmarks = GetBookmarks();
        var newBookmark = new Bookmark { UserId = userId, MovieId = movieId };

        var mockRepo = new Mock<IBookmarkRepository>();
        mockRepo
          .Setup(repo => repo.GetById(userId, movieId))
          .ReturnsAsync(bookmarks.FirstOrDefault(x => x.UserId == userId && x.MovieId == movieId)!);

        var service = new BookmarkService(mockRepo.Object);
        var result = await service.Add(newBookmark);

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Equal("AlreadyExists", result.Error.Code);
    }

    [Theory]
    [InlineData("1", 1)]
    [InlineData("1", 2)]
    [InlineData("2", 3)]
    [InlineData("2", 4)]
    public async Task DeleteBookmark_ReturnsSuccess(string userId, int movieId)
    {
        var bookmarks = GetBookmarks();
        var bookmark = new Bookmark { UserId = userId, MovieId = movieId };

        var mockRepo = new Mock<IBookmarkRepository>();
        mockRepo
          .Setup(repo => repo.GetById(userId, movieId))
          .ReturnsAsync(bookmarks.FirstOrDefault(x => x.UserId == userId && x.MovieId == movieId)!);

        mockRepo
          .Setup(repo => repo.Delete(bookmark));

        var service = new BookmarkService(mockRepo.Object);
        var result = await service.Delete(bookmark);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData("1", 10)]
    [InlineData("10", 1)]
    [InlineData("10", 10)]
    public async Task DeleteBookmark_NotFound_ReturnsError(string userId, int movieId)
    {
        var bookmarks = GetBookmarks();
        var bookmark = new Bookmark { UserId = userId, MovieId = movieId };

        var mockRepo = new Mock<IBookmarkRepository>();
        mockRepo
          .Setup(repo => repo.GetById(userId, movieId))
          .ReturnsAsync(bookmarks.FirstOrDefault(x => x.UserId == userId && x.MovieId == movieId)!);

        var service = new BookmarkService(mockRepo.Object);
        var result = await service.Delete(bookmark);

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Equal("NotFound", result.Error.Code);
    }
}

