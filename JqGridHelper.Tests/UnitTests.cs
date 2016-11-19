using JqGridHelper.Tests.Models;
using JqGridHelper.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JqGridHelper.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Test_Self_Referencing_Entities()
        {
            var role1 = new Role
            {
                Name = "Role 1",
                ParentRole = null
            };

            var role2 = new Role
            {
                Name = "Role 2",
                ParentRole = role1
            };

            var obj = new User
            {
                UserName = "User 1",
                Role = role2
            };

            var type = obj.GetType().FindFieldType("Role.Name", dumpLevel: 3);
            Assert.AreEqual(expected: typeof(string), actual: type);

            type = obj.GetType().FindFieldType("UserName", dumpLevel: 3);
            Assert.AreEqual(expected: typeof(string), actual: type);
        }
    }
}