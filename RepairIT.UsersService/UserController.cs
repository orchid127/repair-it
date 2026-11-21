using Microsoft.AspNetCore.Mvc;
using RepairIT.CatalogService;
using RepairIT.Shared;
namespace RepairIT.UsersService;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        List<User> users = _userService.ListUsers();
        _logger.LogInformation($"------------------- ⋆⭒˚.⋆ list of users ⋆⭒˚.⋆ -------------------");
        foreach (User u in users)
        {
            _logger.LogInformation($"[user #{u.Id}] first name : {u.FirstName} | last name : {u.LastName} | activated : {u.IsActivated}\n");
        }
        return Ok(users);
    }

    [HttpPost]
    public IActionResult AddUser(string firstName, string lastName)
    {
        User newUser = _userService.CreateUser(firstName, lastName);
        _logger.LogInformation($"------------------- ⋆⭒˚.⋆ computer #{newUser.Id} has been created ! ⋆⭒˚.⋆ -------------------");
        _logger.LogInformation($"[user #{newUser.Id}] first name : {newUser.FirstName} | last name : {newUser.LastName} | activated : {newUser.IsActivated}\n");
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

        _logger.LogInformation($"------------------- ⋆⭒˚.⋆ computer #{user.Id} has been updated ! ⋆⭒˚.⋆ -------------------");
        _logger.LogInformation($"[user #{user.Id}] first name : {user.FirstName} | last name : {user.LastName} | activated : {user.IsActivated} | role : {user.UserRole}\n");
        return NoContent();
    }
}