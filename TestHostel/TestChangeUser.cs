using DatabaseLayer;
using Logic;
using Model;
using Moq;

namespace TestHostel
{
    [TestClass]
    public class TestChangeUser
    {
        [TestMethod]
        public void ValidateAndUpdateUser_RightCode()
        {
            var mockUserRepository = new Mock<IUsersRepository>();
            var addUserLogic = new ChangeUserLogic(mockUserRepository.Object)
            {
                User = new User
                {
                    Firstname = "Firstname",
                    Lastname = "Lastname",
                    Patronymic = "Patronymic",
                    Phone = "Phone",
                    Login = "Login",
                    Password = "Password"
                }
            };


            addUserLogic.changeUser(addUserLogic.User);

            mockUserRepository.Verify(repo => repo.Update(It.Is<User>(user =>
                user.Firstname == "Firstname" &&
                user.Lastname == "Lastname" &&
                user.Patronymic == "Patronymic" &&
                user.Phone == "Phone" &&
                user.Login == "Login" &&
                user.Password == "Password"
            )), Times.Once);

        }


        [TestMethod]
        public void DeleteUser()
        {
            var mockUserRepository = new Mock<IUsersRepository>();
            var changeUserLogic = new ChangeUserLogic(mockUserRepository.Object)
            {
                User = new User
                {
                    Firstname = "Firstname",
                    Lastname = "Lastname",
                    Patronymic = "Patronymic",
                    Phone = "Phone",
                    Login = "Login",
                    Password = "Password"
                }
            };

            changeUserLogic.deleteUser(changeUserLogic.User);

            mockUserRepository.Verify(repo => repo.Delete(It.Is<User>(user =>
                user.Firstname == "Firstname" &&
                user.Lastname == "Lastname" &&
                user.Patronymic == "Patronymic" &&
                user.Phone == "Phone" &&
                user.Login == "Login" &&
                user.Password == "Password"
            )), Times.Once);

        }
    }
}
