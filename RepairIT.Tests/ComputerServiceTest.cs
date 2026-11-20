using Microsoft.EntityFrameworkCore;
using RepairIT.CatalogService;
using RepairIT.Shared;

namespace RepairIT.Tests;

public class ComputerServiceTest
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
    public void CreateComputer_ReturnsNewComputer()
    {
        // Arrange
        RepairITDbContext context = CreateContext();
        ComputerService service = new ComputerService(context);

        // Act
        Computer newComputer = service.CreateComputer("kimi's computer", "lenovo", StateComputer.Running);

        // Assert
        Assert.NotNull(newComputer);
        Assert.Contains(context.Computers, c => c.Id == newComputer.Id); // checks that the new computer is added in the database context
    }

    [Fact]
    public void DeleteComputer_ReturnsDeletedComputer()
    {
        // Arrange
        RepairITDbContext context = CreateContext();
        ComputerService service = new ComputerService(context);

        // Act
        Computer newComputer = service.CreateComputer("kimi's computer", "lenovo", StateComputer.Running);
        Computer? deletedComputer = service.DeleteComputer(newComputer.Id);

        //Assert
        Assert.NotNull(deletedComputer);
        Assert.Equal(newComputer, deletedComputer);
        Assert.DoesNotContain(context.Computers, c => c.Id == deletedComputer.Id);
    }

    [Fact]
    public void DeleteComputer_CantDeleteInexistingComputer()
    {
        // Arrange
        RepairITDbContext context = CreateContext();
        ComputerService service = new ComputerService(context);

        // Act
        const int id = 1;
        Computer? deletedComputer = service.DeleteComputer(id);

        //Assert
        Assert.Null(deletedComputer);
        Assert.DoesNotContain(context.Computers, c => c.Id == id);
    }
}
