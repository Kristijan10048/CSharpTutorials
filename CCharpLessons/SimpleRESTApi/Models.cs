using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public record DestinationMachine(long Id, string Name, string ip_address, string? SerialNumber, string CreatedUtc);
public class User
{
    [Key]
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? Email { get; set; }
    public DateTime CreatedUtc { get; set; }

    public List<Device> Devices { get; set; } = new();
}

public class Device
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? SerialNumber { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public DateTime CreatedUtc { get; set; }
}

public record CreateUserRequest(string Username, string? Email);

public record CreateDeviceRequest(string Name, string? SerialNumber, int UserId);
