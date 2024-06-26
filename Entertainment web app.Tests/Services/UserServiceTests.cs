using Microsoft.AspNetCore.Identity;

using Entertainment_web_app.Data;
using Entertainment_web_app.Models.Dto;
using Entertainment_web_app.Repositories;
using Entertainment_web_app.Services;


namespace Entertainment_web_app.Tests.Services;

public class UserServiceTests
{

    private List<ApplicationUser> GetUsers()
    {
        return new List<ApplicationUser>
        {
          new ApplicationUser { Id = "abc-1", UserName = "user1", Email = "test1@test.com", Firstname = "John", Lastname = "Doe"},
          new ApplicationUser { Id = "abc-2", UserName = "user2", Email = "test2@test.com", Firstname = "Jane", Lastname = "Doe"},
          new ApplicationUser { Id = "abc-3", UserName = "user3", Email = "test3@test.com", Firstname = "John", Lastname = "Smith"},
          new ApplicationUser { Id = "abc-4", UserName = "user4", Email = "test4@test.com", Firstname = "Jane", Lastname = "Smith"}
        };
    }

    private Mock<UserManager<ApplicationUser>> InitializeUserManager()
    {
        return new Mock<UserManager<ApplicationUser>>(new Mock<IUserStore<ApplicationUser>>().Object, null!, null!, null!, null!, null!, null!, null!, null!);
    }

    [Fact]
    public async Task GetAll_ReturnsAllUsers()
    {
        var userManager = InitializeUserManager();

        var repository = new Mock<IUserRepository>();
        repository
          .Setup(repo => repo.GetAll())
          .ReturnsAsync(GetUsers());

        var service = new UserService(repository.Object, userManager.Object);
        var result = await service.GetAll();

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(4, result.Data?.Count());
    }

    [Fact]
    public async Task GetAll_NotFound_ReturnsError()
    {
        var userManager = InitializeUserManager();

        var repository = new Mock<IUserRepository>();
        repository
          .Setup(repo => repo.GetAll())
          .ReturnsAsync(new List<ApplicationUser>());

        var service = new UserService(repository.Object, userManager.Object);
        var result = await service.GetAll();

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Equal("NotFound", result.Error?.Code);
    }

    [Theory]
    [InlineData("abc-1")]
    [InlineData("abc-2")]
    [InlineData("abc-3")]
    [InlineData("abc-4")]
    public async Task GetById_ReturnsUserById(string userId)
    {
        var userManager = InitializeUserManager();

        var repository = new Mock<IUserRepository>();
        repository
          .Setup(repo => repo.GetById(userId))
          .ReturnsAsync(GetUsers().FirstOrDefault(u => u.Id == userId));

        var service = new UserService(repository.Object, userManager.Object);
        var result = await service.GetById(userId);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(userId, result.Data?.Id);
    }

    [Theory]
    [InlineData("abc-5")]
    [InlineData("abc-6")]
    [InlineData("abc-7")]
    [InlineData("abc-8")]
    public async Task GetById_NotFound_ReturnsError(string userId)
    {
        var userManager = InitializeUserManager();

        var repository = new Mock<IUserRepository>();
        repository
          .Setup(repo => repo.GetById(userId))
          .ReturnsAsync(GetUsers().FirstOrDefault(u => u.Id == userId));

        var service = new UserService(repository.Object, userManager.Object);
        var result = await service.GetById(userId);

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Equal("NotFound", result.Error?.Code);
    }


    [Fact]
    public async Task Update_ReturnsSuccess()
    {
        var userManager = InitializeUserManager();

        var user = new ApplicationUser { Id = "abc-4", UserName = "user4", Email = "test4@test.com", Firstname = "Jane", Lastname = "Smith" };
        var updatedUser = new UserDto { Id = "abc-4", UserName = "new username", Email = "test5@test.com", Firstname = "Jane", Lastname = "Smith" };

        var repository = new Mock<IUserRepository>();
        repository
          .Setup(repo => repo.GetById(updatedUser.Id))
          .ReturnsAsync(GetUsers().FirstOrDefault(u => u.Id == updatedUser.Id));

        repository
          .Setup(repo => repo.Update(user));

        var service = new UserService(repository.Object, userManager.Object);
        var result = await service.Update(updatedUser);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Update_NotFound_ReturnsError()
    {
        var user = new ApplicationUser
        {
            Id = "abc-5",
            UserName = "user5",
            Email = "test5@test.com",
            Firstname = "Jane",
            Lastname = "Smith"
        };

        var userManager = InitializeUserManager();
        var repository = new Mock<IUserRepository>();
        repository
          .Setup(repo => repo.GetById(user.Id))
          .ReturnsAsync(GetUsers().FirstOrDefault(u => u.Id == user.Id));

        var service = new UserService(repository.Object, userManager.Object);
        var result = await service.Update(new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Firstname = user.Firstname,
            Lastname = user.Lastname
        });

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Equal("NotFound", result.Error?.Code);
    }


    [Fact]
    public async Task Delete_ReturnsSuccess()
    {
        var userId = "abc-4";
        var user = GetUsers().FirstOrDefault(u => u.Id == userId);

        var userManager = InitializeUserManager();

        var repository = new Mock<IUserRepository>();
        repository
          .Setup(repo => repo.GetById(userId))
          .ReturnsAsync(user);

        repository
          .Setup(repo => repo.Delete(user!));

        var service = new UserService(repository.Object, userManager.Object);
        var result = await service.Delete(userId);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Delete_NotFound_ReturnsError()
    {
        var userId = "abc-5";

        var userManager = InitializeUserManager();
        var repository = new Mock<IUserRepository>();
        repository
          .Setup(repo => repo.GetById(userId))
          .ReturnsAsync(GetUsers().FirstOrDefault(u => u.Id == userId));

        var service = new UserService(repository.Object, userManager.Object);
        var result = await service.Delete(userId);

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Equal("NotFound", result.Error?.Code);
    }
}
