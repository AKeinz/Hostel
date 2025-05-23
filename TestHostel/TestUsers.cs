using DatabaseLayer;
using Logic;
using Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHostel
{
    [TestClass]
    public class TestUsers
    {
        [TestMethod]
        public void GetUsers()
        {
            var usersRepository = new Mock<IUsersRepository>();

            usersRepository.Setup(r => r.GetAll()).Returns([
                new User(),
                new User()
            ]);
            var usersLogic = new UsersLogic(usersRepository.Object);

            var users = usersLogic.Users;
            
            Assert.AreEqual(2, users.Count);
        }
    }
}
