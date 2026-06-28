using System;

public record User(long Id, string Username, string? Email, string CreatedUtc);

public record Device(long Id, string Name, string? SerialNumber, long UserId, string CreatedUtc);

public record CreateUserRequest(string Username, string? Email);

public record CreateDeviceRequest(string Name, string? SerialNumber, long UserId);
