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
            string codeSent = addUserLogic.generateCode();
            addUserLogic.Code = codeSent;

            addUserLogic.CheckCodeCommand.Execute(null);

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
        public void ValidateAndUpdateUser_WrongCode()
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
            string codeSent = changeUserLogic.generateCode();
            changeUserLogic.Code = "hdgfhdgfh";

            Assert.ThrowsException<HostelException>(() => changeUserLogic.CheckCodeCommand.Execute(null),
                "HostelException должен быть сгенерирован, если неверный код");
            mockUserRepository.Verify(repo => repo.Update(It.Is<User>(user =>
                user.Firstname == "Firstname" &&
                user.Lastname == "Lastname" &&
                user.Patronymic == "Patronymic" &&
                user.Phone == "Phone" &&
                user.Login == "Login" &&
                user.Password == "Password"
            )), Times.Never);

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

            changeUserLogic.DeleteUserCommand.Execute(null);
            
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
