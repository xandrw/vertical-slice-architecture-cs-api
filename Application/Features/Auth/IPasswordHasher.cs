namespace Application.Features.Auth;

public interface IPasswordHasher
{
    public (byte[], byte[]) HashPassword(string password);
    public byte[] VerifyPassword(string password, byte[] passwordSalt);
}