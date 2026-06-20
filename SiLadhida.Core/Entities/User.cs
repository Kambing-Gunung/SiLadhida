using System;

namespace SiLadhida.Core.Entities;

public class User
{
    public int Id { get; private set; }

    public string Username { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public string Role { get; private set; } = string.Empty;

    private User() { }

    private User(string username, string passwordHash, string role)
    {
        Username = username;
        PasswordHash = passwordHash;
        Role = role;
    }

    public static User Create(string username, string passwordHash, string role)
    {
        return new User(username, passwordHash, role);
    }
}