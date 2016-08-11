using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAutoLogin.Security.Cryptography;

namespace WebAutoLogin.Test
{
    [TestClass]
    public class DefaultHashServiceTest
    {
        [TestMethod]
        public void HashCheck()
        {
            var hashService = new DefaultHashService();
            var data1 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            var data2 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum,";
            var hash1 = hashService.Hash(data1);
            var hash2 = hashService.Hash(data2);
            Assert.AreNotEqual(hash1, hash2);
        }

        [TestMethod]
        public void TokenHashing()
        {
            var hashService = new DefaultHashService();
            var token = hashService.Hash(string.Format("{0}|{1}", "efaruk", "Y4mukMav!"));
            Assert.AreEqual("938921745d3f754db30a9a3440c3ebadd0e747b4164d2e53f499104ea7771f44", token);
        }
    }
}
