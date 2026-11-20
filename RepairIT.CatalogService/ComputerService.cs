using RepairIT.Shared;

namespace RepairIT.CatalogService;

public class ComputerService : IComputerService
{
    private readonly RepairITDbContext _context;

    public ComputerService(RepairITDbContext context)
    {
        _context = context;
    }

    public Computer CreateComputer(string name, string brand, StateComputer state)
    {
        Computer newComputer = new Computer { Name = name, Brand = brand, State = state };
        _context.Computers.Add(newComputer);
        _context.SaveChanges();
        return newComputer;
    }

    public List<Computer> ListComputers()
    {
        return _context.Computers.ToList();
    }

    public Computer? FindComputerById(int id)
    {
        Computer? computer = _context.Computers.Find(id);
        return computer;
    }

    public Computer? UpdateComputer(int id, string? name, string? brand, StateComputer? state, int? stock)
    {
        Computer? computer = FindComputerById(id);

        if (computer != null)
        {
            // updates only the data that has been given (ie not null)
            computer.Name = name ?? computer.Name;
            computer.Brand = brand ?? computer.Brand;
            computer.State = state ?? computer.State;
            computer.Stock = stock ?? computer.Stock;

            _context.SaveChanges();

            return computer;
        }

        return computer;
    }
    public Computer? DeleteComputer(int id)
    {
        Computer? computer = FindComputerById(id);

        if (computer != null)
        {
            _context.Computers.Remove(computer);
            _context.SaveChanges();
        }

        return computer;
    }
}