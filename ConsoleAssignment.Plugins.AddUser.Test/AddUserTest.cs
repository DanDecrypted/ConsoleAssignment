using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ConsoleAssignment.Plugins;

namespace ConsoleAssignment.Plugins.AddUser.Test
{
    [TestFixture]
    public class AddUserTest
    {
        [Test]
        public void CanHandle_ValidCommand_ReturnsTrue()
        {
            AddUser addUser = new AddUser();
        }
    }
}
