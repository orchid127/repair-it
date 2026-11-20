using RepairIT.CatalogService;
using RepairIT.Shared;
namespace RepairIT.UsersService;

public class UserService : IUserService
{
    private readonly RepairITDbContext _context;

    public UserService(RepairITDbContext context)
    {
        _context = context;
    }

    public User CreateUser(string firstName, string lastName)
    {
        User newUser = new User { FirstName = firstName, LastName = lastName, IsActivated = true };
        _context.Users.Add(newUser);
        _context.SaveChanges();
        return newUser;
    }

    public User? AddRoleToUser(int id, Role role)
    {
        User? user = _context.Users.Find(id);

        if (user != null)
        {
            user.UserRole = role;
        }

        return user;
    }

    public User? ChangeUserActivationStatus(int id, bool isActivated)
    {
        User? user = _context.Users.Find(id);

        if (user != null)
        {
            user.IsActivated = isActivated;
        }

        return user;
    }

    public List<User> ListUsers()
    {
        return _context.Users.ToList();
    }
}