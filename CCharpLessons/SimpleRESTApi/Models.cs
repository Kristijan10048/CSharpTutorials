using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Read-only representation of a destination machine. Returned by the machines endpoints and
/// registered in AppJsonSerializerContext so it can be serialized to JSON responses.
/// </summary>
public record DestinationMachine(long Id, string Name, string ip_address, string? SerialNumber, string CreatedUtc);

/// <summary>
/// A user account. Persisted by EF Core into the Users table (see AppDbContext).
/// </summary>
public class User
{
    /// <summary>The primary key of the user.</summary>
    [Key]
    public int Id { get; set; }

    /// <summary>The login name. Required and never null or empty when persisted.</summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>An optional email address.</summary>
    public string? Email { get; set; }

    /// <summary>The UTC date/time the record was created.</summary>
    public DateTime CreatedUtc { get; set; }

    /// <summary>All devices owned by this user. Populated via EF Core eager loading (u.Devices).</summary>
    public List<Device> Devices { get; set; } = new();
}

/// <summary>
/// A device belonging to a user. Persisted by EF Core into the Devices table, with a foreign key back to Users (cascade delete).
/// </summary>
public class Device
{
    /// <summary>The primary key of the device.</summary>
    [Key]
    public int Id { get; set; }

    /// <summary>The device name. Required and never null or empty when persisted.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>An optional serial number.</summary>
    public string? SerialNumber { get; set; }

    /// <summary>The owning user's primary key (foreign key to Users).</summary>
    public int UserId { get; set; }

    /// <summary>The owning user. Loaded eagerly with .Include(d => d.User).</summary>
    public User? User { get; set; }

    /// <summary>The UTC date/time the record was created.</summary>
    public DateTime CreatedUtc { get; set; }
}

/// <summary>
/// Request body for POST /users. Maps to a new User persisted via EF Core.
/// </summary>
public record CreateUserRequest(string Username, string? Email);

/// <summary>
/// Request body for POST /devices. Maps to a new Device persisted via EF Core; UserId must reference an existing user.
/// </summary>
public record CreateDeviceRequest(string Name, string? SerialNumber, int UserId);
