using BasicRPG.Domain.DTOs;
using BasicRPG.Domain.Entities.Users;
using BasicRPG.Domain.Enums;
using GTANetworkAPI;

namespace BasicRPG.Domain.Services.Login;

public interface ILoginService
{
    ApiResponse Login(Player player, string username, string password);
    ApiResponse Register(Player player, string username, string password);
    void SetPlayerDimension(Player player, uint dimension);
    ApiResponse SpawnPlayer(Player player, SpawnLocation location);
    string ToSha256(string randomString);
    void MovePlayerToSpawnDimension(Player player);
}