using Microsoft.EntityFrameworkCore;
using RepairIT.Shared;

class RepairITDbContext : DbContext
{
    public DbSet<Computer> Computers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=repairitinfo.db");
    }
}