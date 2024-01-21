using BasicRPG.Domain.Commands;
using BasicRPG.Infrastructure.Data;
using GTANetworkAPI;

namespace BasicRPG.Application.Commands.Test;

public class RemoveHospitalSpawn : IBaseCommand
{
    private readonly RageDbContext _context;

    public RemoveHospitalSpawn(RageDbContext context)
    {
        _context = context;
    }

    public string Name => "removespawn";
    public Dictionary<string, Type> Args => new();
    public void Execute(Player player, string? args)
    {
        var hospital = _context.HospitalSpawns.FirstOrDefault();
        if (hospital == null)
        {
            return;
        }
        _context.HospitalSpawns.Remove(hospital);
        _context.SaveChanges();
    }
}