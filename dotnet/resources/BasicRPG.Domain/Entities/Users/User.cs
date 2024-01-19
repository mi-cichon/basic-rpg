using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using GTANetworkAPI;
using Newtonsoft.Json;

namespace BasicRPG.Domain.Entities.Users;

public class User : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    [DefaultValue(typeof(double), "0")] 
    public double Money { get; set; }

    [DefaultValue(typeof(int), "0")] 
    public int Experience { get; set; }

    [DefaultValue(typeof(int), "0")] 
    public int Level { get; set; }

    public ulong CreatedBy { get; set; }

    public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;

    public string? LastPositionJson { get; set; }

    [NotMapped]
    public Vector3? LastPosition
    {
        get =>
            LastPositionJson == null
                ? null
                : JsonConvert.DeserializeObject<Vector3>(LastPositionJson);
        set =>
            LastPositionJson = value == null
                ? null
                : JsonConvert.SerializeObject(value);
    }
}