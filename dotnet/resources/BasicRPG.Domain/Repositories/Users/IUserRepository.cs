using BasicRPG.Domain.Entities.Users;
using GTANetworkAPI;

namespace BasicRPG.Domain.Repositories.Users;

public interface IUserRepository
{
    public User? GetUserEntity(Player player);
    void SaveLastPosition(Player player);
    Vector3? GetPlayerLastPosition(Player player);
    User? TryLoginUser(string username, string password);
    bool UsersWithNameExist(string name);
    int AccountsCreatedBySocialClubId(ulong socialClubId);
    void AddUser(User user);
}