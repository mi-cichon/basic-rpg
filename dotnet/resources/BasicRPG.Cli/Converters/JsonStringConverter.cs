using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;

namespace BasicRPG.Cli.Converters;

public class JsonStringConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        return text;
    }
}