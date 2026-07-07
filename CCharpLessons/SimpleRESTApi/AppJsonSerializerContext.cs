using System.Text.Json.Serialization;

[JsonSerializable(typeof(Todo[]))]
[JsonSerializable(typeof(DemoRequest))]
[JsonSerializable(typeof(TwoParamsRequest))]
[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(User[]))]
[JsonSerializable(typeof(Device))]
[JsonSerializable(typeof(Device[]))]
[JsonSerializable(typeof(DestinationMachine))]
[JsonSerializable(typeof(CreateUserRequest))]
[JsonSerializable(typeof(CreateDeviceRequest))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
