using Domain.Validation;

namespace Domain.Users;

public class User : BaseEntity
{
    /** Auto-Generated and assigned to Property by EF Core, using Reflection */
    public int Id { get; private set; }

    public string Email { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;
    public byte[] PasswordHash { get; private set; } = [];
    public byte[] PasswordSalt { get; private set; } = [];

    public static User Create(string email, string role, string password, Func<string, (byte[], byte[])> passwordHasher)
    {
        Contract.Requires(!string.IsNullOrWhiteSpace(email) && email.Length >= 6);
        Contract.Requires(!string.IsNullOrWhiteSpace(role) && Users.Role.Roles.Contains(role));
        Contract.Requires(!string.IsNullOrWhiteSpace(password) && password.Length is >= 8 and <= 50);

        return new User { Email = email, Role = role }.ApplyPassword(password, passwordHasher);
    }

    public bool VerifyPassword(string password, Func<string, byte[], byte[]> passwordVerifier)
    {
        var computedHash = passwordVerifier(password, PasswordSalt);
        return computedHash.SequenceEqual(PasswordHash);
    }

    public User ApplyPassword(string password, Func<string, (byte[], byte[])> passwordHasher)
    {
        var (hash, salt) = passwordHasher(password);
        PasswordHash = hash;
        PasswordSalt = salt;
        return this;
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

public static class Role
{
    public const string Admin = "Admin";
    public const string Author = "Author";
    public static readonly string[] Roles = [Author, Admin];
}