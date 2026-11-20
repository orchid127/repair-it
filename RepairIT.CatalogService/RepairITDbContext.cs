using Microsoft.EntityFrameworkCore;
using RepairIT.Shared;
namespace RepairIT.CatalogService;

public class RepairITDbContext(DbContextOptions<RepairITDbContext> options) : DbContext(options)
{
    public DbSet<Computer> Computers { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=repairitinfo.db");
        }
    }
}