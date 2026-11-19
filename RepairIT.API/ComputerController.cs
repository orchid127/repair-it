using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepairIT.Shared;

namespace RepairIT.API;

[ApiController]
[Route("computers")]
public class ComputerController : ControllerBase
{
    private readonly IComputerService _computerService;
    private readonly ILogger<ComputerController> _logger;
    private readonly IConfiguration _config;

    public ComputerController(IComputerService computerService, ILogger<ComputerController> logger, IConfiguration config)
    {
        _computerService = computerService;
        _logger = logger;
        _config = config;
    }

    [HttpGet]
    public IActionResult GetAllComputers()
    {
        List<Computer> computers = _computerService.ListComputers();

        _logger.LogInformation("------------------- ⋆⭒˚.⋆ list of computers ⋆⭒˚.⋆ -------------------");
        foreach (Computer c in computers)
        {
            _logger.LogInformation($"[computer #{c.Id} ] name : {c.Name} | brand : {c.Brand} | state : {c.State}");
        }
        _logger.LogInformation("--------------- ⋆⭒˚.⋆ end of list of computers ⋆⭒˚.⋆ ---------------\n");

        return Ok(computers);
    }

    [HttpGet("{id}")]
    public IActionResult GetComputerById(int id)
    {
        Computer? computer = _computerService.FindComputerById(id);

        if (computer != null)
        {
            _logger.LogInformation($"------------------- ⋆⭒˚.⋆ info of computer #{computer.Id} ⋆⭒˚.⋆ -------------------");
            _logger.LogInformation($"[computer #{computer.Id} ] name : {computer.Name} | brand : {computer.Brand} | state : {computer.State}\n");
        }

        return computer == null ? NotFound() : Ok(computer);
    }

    [HttpGet("info")]
    public IActionResult GetConfigInfo()
    {
        string? name = _config["Application:Name"];
        string? version = _config["Application:Version"];

        _logger.LogInformation("------------------- ⋆⭒˚.⋆ configuration info ⋆⭒˚.⋆ -------------------");
        _logger.LogInformation($"application name : {name} | version : {version}");

        return Ok(new { name, version });
    }

    [HttpPost]
    public IActionResult AddNewComputer(string name, string brand, StateComputer state)
    {
        Computer newComputer = _computerService.CreateComputer(name, brand, state);

        _logger.LogInformation($"------------------- ⋆⭒˚.⋆ computer #{newComputer.Id} has been created ! ⋆⭒˚.⋆ -------------------");
        _logger.LogInformation($"[computer #{newComputer.Id} ] name : {newComputer.Name} | brand : {newComputer.Brand} | state : {newComputer.State}\n");

        var location = Url.Action(nameof(AddNewComputer), new { id = newComputer.Id }) ?? $"/{newComputer.Id}"; // URL where we can find the newly created computer object
        return Created(location, newComputer);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult ModifyComputer(int id, string? name, string? brand, StateComputer? state, int? stock)
    {
        Computer? existingComputer = _computerService.UpdateComputer(id, name, brand, state, stock);

        if (existingComputer == null)
        {
            return NotFound();
        }

        _logger.LogInformation($"------------------- ⋆⭒˚.⋆ computer #{existingComputer.Id} has been updated ! ⋆⭒˚.⋆ -------------------");
        _logger.LogInformation($"[computer #{existingComputer.Id} ] name : {existingComputer.Name} | brand : {existingComputer.Brand} | state : {existingComputer.State}\n");

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteComputer(int id)
    {
        Computer? existingComputer = _computerService.DeleteComputer(id);

        if (existingComputer == null)
        {
            _logger.LogInformation($"------------------- ⋆⭒˚.⋆ computer #{id} is not found ... (ㅠ﹏ㅠ) :( ⋆⭒˚.⋆ -------------------");
            return NotFound();
        }

        _logger.LogInformation($"------------------- ⋆⭒˚.⋆ computer #{existingComputer.Id} has been deleted ! ⋆⭒˚.⋆ -------------------");
        return NoContent();
    }

}