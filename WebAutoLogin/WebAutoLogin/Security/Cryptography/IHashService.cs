namespace WebAutoLogin.Security.Cryptography
{
    public interface IHashService
    {
        string Hash(string clearText);
    }
}