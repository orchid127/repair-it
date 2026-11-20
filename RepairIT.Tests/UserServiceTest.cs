using Microsoft.EntityFrameworkCore;
using RepairIT.CatalogService;
using RepairIT.Shared;
using RepairIT.UsersService;

namespace RepairIT.Tests;

public class UserServiceTest
{
    private RepairITDbContext CreateContext()
    {
        var options =
            new DbContextOptionsBuilder<RepairITDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        return new RepairITDbContext(options);
    }

    [Fact]
    public void CreateUser_ReturnsActivatedUser()
    {
        // Arrange
        RepairITDbContext context = CreateContext();
        UserService service = new UserService(context);

        // Act
        User newUser = service.CreateUser("kimberley", "ndouga");

        // Assert
        Assert.Contains(context.Users, u => u.Id == newUser.Id);
        Assert.True(newUser.IsActivated);
    }
}