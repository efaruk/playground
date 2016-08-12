using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAutoLogin.Client;
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
            var data3 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            var hash1 = hashService.Hash(data1);
            var hash2 = hashService.Hash(data2);
            var hash3 = hashService.Hash(data3);
            Assert.AreNotEqual(hash1, hash2);
            Assert.AreEqual(hash1, hash3);
        }

        [TestMethod]
        public void TokenHashing()
        {
            var hashService = new DefaultHashService();
            var token = hashService.Hash(string.Format(GlobalModule.TokenHashFormat, "efaruk", "F4reburnu"));
            Assert.AreEqual("6fee64809cfa59f1ce113e2a3b9eee6e40c3c9396666563ec5602b8c2e893eac", token);
        }
    }
}
