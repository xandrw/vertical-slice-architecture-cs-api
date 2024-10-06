using System.Security.Cryptography;
using System.Text;

namespace Application.Common;

public static class PasswordHasher
{
    public static (byte[], byte[]) HashPassword(string password)
    {
        using var hmac = new HMACSHA512();
        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        var passwordSalt = hmac.Key;

        return (passwordHash, passwordSalt);
    }
    
    public static byte[] VerifyPassword(string password, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
}