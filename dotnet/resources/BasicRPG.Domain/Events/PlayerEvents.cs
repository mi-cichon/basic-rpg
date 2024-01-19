namespace BasicRPG.Domain.Events;

public static class PlayerEvents
{
    public const string DestroySpeedometer = "client_destroySpeedometer";
    public const string LoginCompleted = "client_loginCompleted";
    public const string RegisteredSuccessfully = "client_registerSuccessful";
    public const string SpawnSelectionCompleted = "client_spawnSelectionCompleted";
    public const string ShowNotification = "client_showNotification";
    public const string UpdateHudValues = "client_updateHudValues";
}