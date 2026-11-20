using RepairIT.Shared;

namespace RepairIT.CatalogService;

public interface IComputerService
{
    public Computer CreateComputer(string name, string brand, StateComputer state);
    public List<Computer> ListComputers();
    public Computer? FindComputerById(int id);
    public Computer? UpdateComputer(int id, string? name, string? brand, StateComputer? state, int? stock);
    public Computer? DeleteComputer(int id);

}