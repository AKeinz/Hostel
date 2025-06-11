using DatabaseLayer;
using Logic;
using Model;
using Moq;

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
