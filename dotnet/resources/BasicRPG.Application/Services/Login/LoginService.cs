using System.Security.Cryptography;
using System.Text;
using BasicRPG.Domain.DTOs;
using BasicRPG.Domain.Entities.Users;
using BasicRPG.Domain.Enums;
using BasicRPG.Domain.Repositories.Users;
using BasicRPG.Domain.Services.Chat;
using BasicRPG.Domain.Services.Login;
using BasicRPG.Domain.Services.Spawn;
using BasicRPG.Domain.Services.Users;
using BasicRPG.Domain.SharedData;
using GTANetworkAPI;

namespace BasicRPG.Application.Services.Login;

public class LoginService : ILoginService
{
    private readonly ISpawnService _spawnService;
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly IChatService _chatService;

    private const int SpawnDimension = 9999;
    private const int DefaultDimension = 0;

    public LoginService(
        ISpawnService spawnService, 
        IUserService userService, 
        IUserRepository userRepository, 
        IChatService chatService)
    {
        _spawnService = spawnService;
        _userService = userService;
        _userRepository = userRepository;
        _chatService = chatService;
    }

    public ApiResponse Login(Player player, string username, string password)
    {
        var encPassword = ToSha256(password);

        var user = _userRepository.TryLoginUser(username, encPassword);

        if (user == null)
        {
            return new ApiResponse(ApiResponseType.Fail, ClientError.WrongCredentials, null);
        }

        var alreadyLoggedIn = NAPI.Pools.GetAllPlayers()
            .Any(x => x != null
                       && x.HasSharedData(PlayerSharedData.Id)
                       && Guid.Parse(x.GetSharedData<string>(PlayerSharedData.Id)).Equals(user.Id));

        if (alreadyLoggedIn)
        {
            return new ApiResponse(ApiResponseType.Fail, ClientError.AlreadyLoggedIn, null);
        }

        player.SetSharedData(PlayerSharedData.Id, user.Id);

        _userService.AssignPlayersValues(player);

        SetPlayerDimension(player, SpawnDimension);

        var hasLastPos = user.LastPosition != null;

        return new ApiResponse(ApiResponseType.Success, string.Empty, new { hasLastPos });
    }

    public ApiResponse Register(Player player, string username, string password)
    {
        var usersCreated = _userRepository.AccountsCreatedBySocialClubId(player.SocialClubId);

        if (usersCreated >= 2)
        {
            return new ApiResponse(ApiResponseType.Fail, ClientError.MaxAccounts, null);
        }

        var sameUsername = _userRepository.UsersWithNameExist(username);

        if (sameUsername)
        {
            return new ApiResponse(ApiResponseType.Fail, ClientError.UserExists, null);
        }

        var newUser = new User
        {
            Name = username,
            Password = ToSha256(password),
            CreatedBy = player.SocialClubId
        };

        _userRepository.AddUser(newUser);

        return new ApiResponse(ApiResponseType.Success, string.Empty, null);
    }

    public void SetPlayerDimension(Player player, uint dimension)
    {
        player.Dimension = dimension;
    }

    public void MovePlayerToSpawnDimension(Player player)
    {
        player.Dimension = SpawnDimension;
    }
    
    public ApiResponse SpawnPlayer(Player player, SpawnLocation location)
    {
        var lastPosition = _userService.GetPlayersLastPos(player);
        var position = _spawnService.GetPlayersSpawnPosition(player, lastPosition, location);
        if (position == null)
        {
            return new ApiResponse(ApiResponseType.Exception, ClientError.Critical, null);
        }

        player.Dimension = 0;
        player.Position = position;

        NAPI.Task.Run(() =>
        {
            _userService.UpdatePlayersHud(player);
        }, 1500);
        NAPI.Task.Run(() =>
        {
            _chatService.SendInfoMessageToPlayer(player, "messages.welcomeMessage");
        }, 3000);
        return new ApiResponse(ApiResponseType.Success, string.Empty, null);
    }

    public string ToSha256(string randomString)
    {
        var crypt = new SHA256Managed();
        var hash = string.Empty;
        var crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
        foreach (var theByte in crypto) hash += theByte.ToString("x2");
        return hash;
    }
}