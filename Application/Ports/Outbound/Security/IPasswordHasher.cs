namespace Application.Ports.Outbound.Security;

public interface IPasswordHasher
{
    string EncriptPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}