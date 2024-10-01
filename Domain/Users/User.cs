using System.Security.Cryptography;
using System.Text;

namespace Domain.Users;

public class User(string email, byte[] passwordHash, byte[] passwordSalt, string role)
{
    /// <summary>
    ///     Auto-Generated and assigned to Property by EF, using Reflection
    /// </summary>
    public int Id { get; private set; }

    public string Email { get; private set; } = email;
    public byte[] PasswordHash { get; private set; } = passwordHash;
    public byte[] PasswordSalt { get; private set; } = passwordSalt;
    public string Role { get; private set; } = role;

    public bool VerifyPassword(string password)
    {
        using var hmac = new HMACSHA512(PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return computedHash.SequenceEqual(PasswordHash);
    }
    
    public User ChangePassword(string password)
    {
        using var hmac = new HMACSHA512();
        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        PasswordSalt = hmac.Key;
        
        return this;
    }
    
    public User ChangeEmail(string email)
    {
        Email = email;
        return this;
    }

    public User ChangeRole(string role)
    {
        Role = role;
        return this;
    }
}