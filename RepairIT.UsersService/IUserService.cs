using RepairIT.Shared;
namespace RepairIT.UsersService;

public interface IUserService
{
    public User CreateUser(string idKeycloak, string firstName, string lastName);

    public User? AddRoleToUser(int id, Role role);

    public User? ChangeUserActivationStatus(int id, bool isActivated);

    public List<User> ListUsers();

}