using System.Security.Cryptography;
using System.Text;

namespace Domain.Users;

public class User : IEntity
{
    /// <summary>Auto-Generated and assigned to Property by EF Core, using Reflection</summary>
    public int Id { get; private set; }

    public string Email { get; private set; }
    public string Role { get; private set; }
    public byte[] PasswordHash { get; private set; } = [];
    public byte[] PasswordSalt { get; private set; } = [];

    public User(string email, string role, string password)
    {
        Email = email;
        Role = role;
        ApplyPassword(password);
    }
    
    // ReSharper disable once UnusedMember.Local
    private User(string email, string role, byte[] passwordHash, byte[] passwordSalt)
    {
        Email = email;
        Role = role;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }
    
    public bool VerifyPassword(string password)
    {
        using var hmac = new HMACSHA512(PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return computedHash.SequenceEqual(PasswordHash);
    }

    public void ApplyPassword(string password)
    {
        using var hmac = new HMACSHA512();
        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        PasswordSalt = hmac.Key;
    }

    public void ChangeEmail(string email)
    {
        Email = email;
    }

    public void ChangeRole(string role)
    {
        Role = role;
    }
}