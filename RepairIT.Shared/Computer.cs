namespace RepairIT.Shared;

public enum StateComputer
{
    Running,
    Down,
    InReparation,
    OutOfOrder,
    Slow,
    Reparable
}

public class Computer
{
    public int Id { get; init; }
    public string? Name { get; set; }
    public string? Brand { get; set; }
    public StateComputer State { get; set; }
    public int Stock { get; set; } = 1;
}
