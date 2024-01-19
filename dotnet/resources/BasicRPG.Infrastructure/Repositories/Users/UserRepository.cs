using BasicRPG.Domain.Entities.Users;
using BasicRPG.Domain.Repositories.Users;
using BasicRPG.Domain.Services.Notification;
using BasicRPG.Domain.SharedData;
using BasicRPG.Infrastructure.Data;
using GTANetworkAPI;

namespace BasicRPG.Infrastructure.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly RageDbContext _context;
    private readonly INotificationService _notificationService;

    public UserRepository(
        RageDbContext context, 
        INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }


    public User? GetUserEntity(Player player)
    {
        var id = Guid.Parse(player.GetSharedData<string>(PlayerSharedData.Id));

        var user = _context.Users.FirstOrDefault(x => x.Id == id);

        if (user != null)
        {
            return user;
        }

        _notificationService.ShowGenericError(player);
        return null;
    }


    public void SaveLastPosition(Player player)
    {
        var user = GetUserEntity(player);

        if (user == null)
        {
            _notificationService.ShowGenericError(player);
            return;
        }

        user.LastPosition = player.Position;

        _context.Update(user);
        _context.SaveChanges();
    }

    public Vector3? GetPlayerLastPosition(Player player)
    {
        var user = GetUserEntity(player);

        if (user != null)
        {
            return user.LastPosition;
        }

        _notificationService.ShowGenericError(player);
        return null;
    }

    public User? TryLoginUser(string username, string password)
    {
       return _context.Users
           .FirstOrDefault(x => x.Name == username && x.Password == password);
    }

    public bool UsersWithNameExist(string name)
    {
        return _context.Users.Any(x => x.Name == name);
    }

    public int AccountsCreatedBySocialClubId(ulong socialClubId)
    {
        return _context.Users.Count(x => x.CreatedBy == socialClubId);
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
}