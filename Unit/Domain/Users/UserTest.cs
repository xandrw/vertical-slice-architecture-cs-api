using System.Text;
using Domain.Users;

namespace Unit.Domain.Users;

[TestFixture]
public class UserTest
{
    [Test]
    public void CreateUser()
    {
        const string email = "example@email.com";
        const string role = Role.Admin;
        const string password = "password";

        var user = User.Create(email, role, password, PasswordHasher);

        var (hash, salt) = PasswordHasher(password);

        Assert.That(user.Id, Is.EqualTo(0));
        Assert.That(user.Email, Is.EqualTo(email));
        Assert.That(user.Role, Is.EqualTo(role));
        Assert.That(user.PasswordHash, Is.EqualTo(hash));
        Assert.That(user.PasswordSalt, Is.EqualTo(salt));
    }

    private const string LongPassword = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

    [Test]
    [TestCase(null, Role.Admin, "password", "email", TestName = "Email:null")]
    [TestCase("", Role.Admin, "password", "email", TestName = "Email:empty")]
    [TestCase("invalid", Role.Admin, "password", "email", TestName = "Email:invalid")]
    [TestCase("example@email.com", null, "password", "role", TestName = "Role:null")]
    [TestCase("example@email.com", "", "password", "role", TestName = "Role:empty")]
    [TestCase("example@email.com", "Role", "password", "role", TestName = "Role:invalid")]
    [TestCase("example@email.com", Role.Admin, null, "password", TestName = "Password:null")]
    [TestCase("example@email.com", Role.Admin, "", "password", TestName = "Password:empty")]
    [TestCase("example@email.com", Role.Admin, "1234567", "password", TestName = "Password:too-short")]
    [TestCase("example@email.com", Role.Admin, LongPassword, "password", TestName = "Password:too-long")]
    public void CreateInvalidUser(string? email, string? role, string? password, string field)
    {
        var exception = Assert.Throws<ArgumentException>(() => User.Create(email!, role!, password!, PasswordHasher));
        Assert.That(exception.Message, Contains.Substring(field));
    }

    [Test]
    public void ChangeEmail()
    {
        var user = User.Create("initial@email.com", Role.Admin, "password", PasswordHasher);
        const string email = "updated@email.com";
        user.ChangeEmail(email);

        Assert.That(user.Email, Is.EqualTo(email));
    }

    [Test]
    public void ChangeRole()
    {
        var user = User.Create("initial@email.com", Role.Admin, "password", PasswordHasher);
        user.ChangeRole(Role.Author);

        Assert.That(user.Role, Is.EqualTo(Role.Author));
    }

    [Test]
    public void VerifyPassword()
    {
        var user = User.Create("initial@email.com", Role.Admin, "password", PasswordHasher);

        Assert.That(user.VerifyPassword("password", PasswordVerifier), Is.True);
    }

    private static (byte[], byte[]) PasswordHasher(string password)
    {
        byte[] passwordHash = Encoding.UTF8.GetBytes($"hashed:{password}");
        byte[] passwordSalt = "test-salt"u8.ToArray();

        return (passwordHash, passwordSalt);
    }

    private static byte[] PasswordVerifier(string password, byte[] passwordSalt)
    {
        return Encoding.UTF8.GetBytes($"hashed:{password}");
    }
}