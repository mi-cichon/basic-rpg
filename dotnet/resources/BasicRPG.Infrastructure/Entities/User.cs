using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GTANetworkAPI;
using Newtonsoft.Json;

namespace BasicRPG.Infrastructure.Entities;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    [DefaultValue(typeof(double), "0")] public double Money { get; set; }

    [DefaultValue(typeof(int), "0")] public int Experience { get; set; }

    [DefaultValue(typeof(int), "0")] public int Level { get; set; }

    public ulong CreatedBy { get; set; }

    public DateTime RegisteredDate { get; set; }

    internal string? _LastPosition { get; set; }

    [NotMapped]
    public Vector3? LastPosition
    {
        get =>
            _LastPosition == null
                ? null
                : JsonConvert.DeserializeObject<Vector3>(_LastPosition);
        set =>
            _LastPosition = value == null
                ? null
                : JsonConvert.SerializeObject(value);
    }
}