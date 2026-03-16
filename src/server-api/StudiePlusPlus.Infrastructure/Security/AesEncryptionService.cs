using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using StudiePlusPlus.Application.Abstractions.Security;

namespace StudiePlusPlus.Infrastructure.Security;

public sealed class AesEncryptionService : IEncryptionService
{
    private readonly byte[] _key;

    public AesEncryptionService(IConfiguration configuration)
    {
        var keyBase64 = configuration["Encryption:Key"]
            ?? throw new InvalidOperationException("Encryption:Key is missing from configuration.");

        _key = Convert.FromBase64String(keyBase64);

        if (_key.Length != 32)
            throw new InvalidOperationException("Encryption:Key must be a 256-bit (32-byte) base64 string.");
    }

    public string Encrypt(string plaintext)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor();
        var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
        var ciphertextBytes = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);

        return $"{Convert.ToBase64String(aes.IV)}:{Convert.ToBase64String(ciphertextBytes)}";
    }

    public string Decrypt(string ciphertext)
    {
        var parts = ciphertext.Split(':', 2);
        if (parts.Length != 2)
            throw new FormatException("Invalid ciphertext format. Expected 'IV:ciphertext'.");

        var iv = Convert.FromBase64String(parts[0]);
        var ciphertextBytes = Convert.FromBase64String(parts[1]);

        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor();
        var plaintextBytes = decryptor.TransformFinalBlock(ciphertextBytes, 0, ciphertextBytes.Length);
        return Encoding.UTF8.GetString(plaintextBytes);
    }
}
