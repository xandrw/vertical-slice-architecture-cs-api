using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;

namespace Infrastructure.Services;

public class HmacSha512PasswordHasher : IPasswordHasher
{
    public (byte[], byte[]) HashPassword(string password)
    {
        using var hmac = new HMACSHA512();
        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        var passwordSalt = hmac.Key;

        return (passwordHash, passwordSalt);
    }
    
    public byte[] VerifyPassword(string password, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
}