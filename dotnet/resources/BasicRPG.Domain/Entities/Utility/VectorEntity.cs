using System.ComponentModel.DataAnnotations.Schema;
using GTANetworkAPI;

namespace BasicRPG.Domain.Entities.Utility;

public class VectorEntity : BaseEntity
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    [NotMapped]
    public Vector3 Vector
    {
        get => new(X, Y, Z);
        set
        {
            X = value.X;
            Y = value.Y;
            Z = value.Z;
        }
    }
}