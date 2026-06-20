using System.Security.Cryptography;
using Application.Ports.Outbound.Security;

namespace Infrastructure.Adapters.Outbound.Security;

public class Rfc2898DeriveBytesPasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16; // 128 bits
    // Tamanho do hash em bytes
    private const int HashSize = 32; // 256 bits
    // Número de iterações (quanto maior, mais seguro, mas mais lento)
    private const int Iterations = 1000;

    
    // 1. Cria o Hash e o Salt
    public string EncriptPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations,  new HashAlgorithmName(), HashSize);

        // Combina o salt e o hash em uma única string para salvar no banco
        return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
    }

    // 2. Verifica a Senha
    public bool VerifyPassword(string password, string hashedPasswordWithSalt)
    {
        string[] parts = hashedPasswordWithSalt.Split(':');
        if (parts.Length != 2) return false;

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] hash = Convert.FromBase64String(parts[1]);

        byte[] testHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, new HashAlgorithmName(), HashSize);

        // Compara os dois hashes byte a byte de forma segura (tempo constante)
        return CryptographicOperations.FixedTimeEquals(hash, testHash);
    }
}