namespace WebAutoLogin.Security.Cryptography
{
    public interface IEncryptionService
    {
        string Decrypt(string ciphertext, string key, string vector);

        string Encrypt(string plainText, string key, string vector);

        string GenerateKey();

        string GenerateVector();
    }
}