using Microsoft.AspNetCore.Mvc;
using RepairIT.CatalogService;
using RepairIT.Shared;
namespace RepairIT.UsersService;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;
    private readonly IConfiguration _config;

    public UserController(IUserService userService, ILogger<UserController> logger, IConfiguration config)
    {
        _userService = userService;
        _logger = logger;
        _config = config;
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        List<User> users = _userService.ListUsers();
        return Ok(users);
    }

    [HttpPost]
    public IActionResult AddUser(string firstName, string lastName)
    {
        User newUser = _userService.CreateUser(firstName, lastName);
        var location = Url.Action(nameof(AddUser), new { id = newUser.Id }) ?? $"/{newUser.Id}"; // URL where we can find the newly created computer object
        return Created(location, newUser);
    }

    [HttpPut("{id}")]
    public IActionResult AddRoleToUser(int id, Role role)
    {
        User? user = _userService.AddRoleToUser(id, role);
        if (user == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}