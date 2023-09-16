using System.Security.Cryptography;
using System.Text;
using BasicRPG.Domain.Enums;
using BasicRPG.Domain.Services.Spawn;
using BasicRPG.Domain.Services.Users;
using BasicRPG.Domain.Types;
using BasicRPG.Infrastructure.Entities;
using BasicRPG.Infrastructure.Entities.DbSettings;
using GTANetworkAPI;

namespace BasicRPG.Domain.Services.Login;

public static class LoginService
{
    public static void SetPlayerDimension(Player player, uint dimension)
    {
        player.Dimension = dimension;
    }

    public static ApiResponse Login(Player player, string username, string password)
    {
        var encPassword = ToSHA256(password);
        using var context = new RageDbContext();

        var user = context.Users.FirstOrDefault(x => x.Name == username && x.Password == encPassword);

        if (user == null) return new ApiResponse(ApiResponseType.Fail, "login.wrongCredentials", null);

        SetUsersVars(player, user);
        UserService.AssignPlayersValues(player);
        var hasLastPos = UserService.GetPlayersLastPos(player) != null;
        return new ApiResponse(ApiResponseType.Success, "", new {hasLastPos});
    }

    public static ApiResponse Register(Player player, string username, string password)
    {
        using var context = new RageDbContext();

        var usersCreated = context.Users.Count(x => x.CreatedBy == player.SocialClubId);

        if (usersCreated >= 2) return new ApiResponse(ApiResponseType.Fail, "register.maxAccounts", null);

        var sameUsername = context.Users.SingleOrDefault(x => x.Name == username);

        if (sameUsername != null) return new ApiResponse(ApiResponseType.Fail, "register.userExists", null);

        var newUser = new User
        {
            Name = username,
            Password = ToSHA256(password),
            CreatedBy = player.SocialClubId,
            RegisteredDate = DateTime.Now
        };

        context.Users.Add(newUser);
        context.SaveChanges();
        return new ApiResponse(ApiResponseType.Success, "", null);
    }

    public static ApiResponse SpawnPlayer(Player player, SpawnLocation location)
    {
        var position = SpawnService.GetPlayersSpawnPosition(player, location);
        if (position == null) return new ApiResponse(ApiResponseType.Exception, "errors.critical", null);

        player.Dimension = 0;
        player.Position = position;

        NAPI.Task.Run(() => { UserService.UpdatePlayersHud(player); }, 1500);
        return new ApiResponse(ApiResponseType.Success, "", null);
    }

    private static void SetUsersVars(Player player, User user)
    {
        player.SetSharedData("player_id", user.Id);
        player.Name = user.Name;
    }

    private static string ToSHA256(string randomString)
    {
        var crypt = new SHA256Managed();
        var hash = string.Empty;
        var crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
        foreach (var theByte in crypto) hash += theByte.ToString("x2");
        return hash;
    }
}