using BasicRPG.Domain.Entities.Utility;
using CsvHelper;

namespace BasicRPG.Cli.Converters;

public static class VectorEntityConverter
{
    public static VectorEntity ConvertToVectorEntity(this ConvertFromStringArgs args, string varName)
    {
        var x = args.Row.GetField<float>($"{varName}X");
        var y = args.Row.GetField<float>($"{varName}Y");
        var z = args.Row.GetField<float>($"{varName}Z");
        return new VectorEntity { X = x, Y = y, Z = z };
    }
}