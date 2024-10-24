using Domain.Validation;

namespace Domain.Users;

public class User : BaseDomainEntity
{
    /** Auto-Generated and assigned to Property by EF Core, using Reflection */
    public int Id { get; private set; }

    public string Email { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;
    public byte[] PasswordHash { get; private set; } = [];
    public byte[] PasswordSalt { get; private set; } = [];

    public static User Create(string email, string role, string password, Func<string, (byte[], byte[])> passwordHasher)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(email) == false);
        Contract.Requires(email.Length is >= 6 and <= 60);
        Contract.Requires(Users.Role.Roles.Contains(role));

        return new User { Email = email, Role = role }.ChangePassword(password, passwordHasher);
    }

    public User ChangeEmail(string email)
    {
        if (email == Email) return this;

        Contract.Requires(string.IsNullOrWhiteSpace(email) == false);
        Contract.Requires(email.Length is >= 6 and <= 60);

        Email = email;
        return this;
    }

    public User ChangeRole(string role)
    {
        if (role == Role) return this;

        Contract.Requires(Users.Role.Roles.Contains(role));

        Role = role;
        return this;
    }

    public User ChangePassword(string password, Func<string, (byte[], byte[])> passwordHasher)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(password) == false);
        Contract.Requires(password.Length is >= 8 and <= 60);

        var (hash, salt) = passwordHasher(password);
        PasswordHash = hash;
        PasswordSalt = salt;
        return this;
    }

    public bool VerifyPassword(string password, Func<string, byte[], byte[]> passwordVerifier)
    {
        var computedHash = passwordVerifier(password, PasswordSalt);
        return computedHash.SequenceEqual(PasswordHash);
    }
}

public static class Role
{
    public const string Admin = "Admin";
    public const string Author = "Author";
    public static readonly string[] Roles = [Author, Admin];
}