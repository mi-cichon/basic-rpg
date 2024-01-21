using GTANetworkAPI;

namespace BasicRPG.Domain.Services.Users;

public interface IUserService
{
    void AssignPlayersValues(Player player);
    void UpdatePlayersHud(Player player);
    int? GetPlayersNextLevelExperience(Player player);
    Player? GetPlayerByRemoteIdOrName(string idOrName);
    Vector3? GetPlayersLastPos(Player player);
    double GetPlayersSharedDataMoney(Player player);
    void SetPlayersSharedDataMoney(Player player, double money);
    void SpawnPlayerAtClosestHospital(Player player);
    void SavePlayersLastPos(Player player);
    void TransferMoneyToPlayer(Player playerFrom, Player playerTo, double amount, string title);
    
}