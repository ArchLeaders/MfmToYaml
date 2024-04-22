namespace MfmToYaml.Models;

public class FunctionEnum(string type) : Dictionary<long, string>
{
    public string Type { get; } = type;
}
