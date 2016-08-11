using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAutoLogin.Security.Cryptography;

namespace WebAutoLogin.Test
{
    [TestClass]
    public class DefaultEncryptionServiceTests
    {
        [TestMethod]
        public void EncryptDecryptTest()
        {
            var encryptionService = new DefaultEncryptionService();
            var key = encryptionService.GenerateKey();
            var vector = encryptionService.GenerateVector();
            var data = "data";
            var cipher = encryptionService.Encrypt(data, key, vector);
            var decrypted = encryptionService.Decrypt(cipher, key, vector);
            Assert.AreEqual(data, decrypted);
        }

        [TestMethod]
        public void EncryptDecryptWithStaticKeyAndVectorTest()
        {
            var encryptionService = new DefaultEncryptionService();
            var key = ConfigurationManager.AppSettings.Get("Key");
            var vector = ConfigurationManager.AppSettings.Get("Vector");
            var password = "Y4mukMav!";
            var cipher = encryptionService.Encrypt(password, key, vector);
            var decrypted = encryptionService.Decrypt(cipher, key, vector);
            Assert.AreEqual(password, decrypted);
        }
    }
}
