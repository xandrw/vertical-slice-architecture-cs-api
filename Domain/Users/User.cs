namespace Domain.Users;

public class User : IEntity
{
    /// <summary>Auto-Generated and assigned to Property by EF Core, using Reflection</summary>
    public int Id { get; private set; }

    public string Email { get; private set; }
    public string Role { get; private set; }
    public byte[] PasswordHash { get; private set; } = [];
    public byte[] PasswordSalt { get; private set; } = [];

    public User(string email, string role, string password, Func<string, (byte[], byte[])> passwordHasher)
    {
        Email = email;
        Role = role;
        ApplyPassword(password, passwordHasher);
    }
    
    // ReSharper disable once UnusedMember.Local
    private User(string email, string role, byte[] passwordHash, byte[] passwordSalt)
    {
        Email = email;
        Role = role;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }
    
    public bool VerifyPassword(string password, Func<string, byte[], byte[]> passwordVerifier)
    {
        var computedHash = passwordVerifier(password, PasswordSalt);
        return computedHash.SequenceEqual(PasswordHash);
    }

    public void ApplyPassword(string password, Func<string, (byte[], byte[])> passwordHasher)
    {
        var (hash, salt) = passwordHasher(password);
        PasswordHash = hash;
        PasswordSalt = salt;
    }

    public User ChangeEmail(string newEmail)
    {
        if (newEmail == Email) return this;
        Email = newEmail;

        return this;
    }

    public User ChangeRole(string newRole)
    {
        if (newRole == Role) return this;
        Role = newRole;

        return this;
    }
}