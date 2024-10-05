using System.Security.Cryptography;
using System.Text;

namespace Domain.Users;

public class User(string email, string role, byte[] passwordHash, byte[] passwordSalt) : IEntity
{
    /// <summary>Auto-Generated and assigned to Property by EF, using Reflection</summary>
    public int Id { get; private set; }

    public string Email { get; private set; } = email;
    public string Role { get; private set; } = role;
    public byte[] PasswordHash { get; private set; } = passwordHash;
    public byte[] PasswordSalt { get; private set; } = passwordSalt;

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