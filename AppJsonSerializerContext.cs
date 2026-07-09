using System.Text.Json.Serialization;

[JsonSerializable(typeof(TwoParamsRequest))]
public partial class AppJsonSerializerContext : JsonSerializerContext
{
}
