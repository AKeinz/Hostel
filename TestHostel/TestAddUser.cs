using DatabaseLayer;
using Logic;
using Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TestHostel
{
    [TestClass]
    public class TestAddUser
    {
        [TestMethod]
        public void addUser()
        {
            var mockUsersRepository = new Mock<IUsersRepository>();

            AddUserLogic addUserLogic = new AddUserLogic(mockUsersRepository.Object);
            User user = new User()
            {
                Firstname = "Firstname",
                Lastname = "Lastname",
                Patronymic = "Patronymic",
                Phone = "Phone",
                Login = "Login",
                Password = "Password,"
            };

            addUserLogic.addUser(user);

            mockUsersRepository.Verify(re => re.Add(user), Times.Once);
        }

        [TestMethod]
        public void checkWrongData()
        {
            Roles Role = Roles.Client;
            string Phone = "Phone";
            string Login = "Login";
            string Password = "Password";


            var mockUsersRepository = new Mock<IUsersRepository>();
            mockUsersRepository.Setup(repo => repo.IsUserExist(Phone, Enum.GetName(Role), Login, Password))
                .Returns(true);

            AddUserLogic addUserLogic = new AddUserLogic(mockUsersRepository.Object)
            {
                Role = Role,
                Phone = Phone,
                Login = Login,
                Password = Password
            };

            bool result = addUserLogic.IsUserExist();

            mockUsersRepository.Verify(re => re.IsUserExist(Phone, Enum.GetName(Role), Login, Password), Times.Once);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckRightCode()
        {
            var mockUsersRepository = new Mock<IUsersRepository>();
            AddUserLogic userLogic = new AddUserLogic(mockUsersRepository.Object);
            string codeSent = userLogic.generateCode();
            userLogic.Code = codeSent;
            Assert.IsTrue(userLogic.isRightCode());
        }

        [TestMethod]
        public void CheckWrongCode()
        {
            var mockUsersRepository = new Mock<IUsersRepository>();
            AddUserLogic userLogic = new AddUserLogic(mockUsersRepository.Object);
            string codeSent = "rtyrty";
            userLogic.Code = codeSent;

            userLogic.generateCode();

            Assert.IsFalse(userLogic.isRightCode());
        }

        [TestMethod]
        public void CheckDataCommand_UserExists()
        {
            string phone = "phone";
            string role = "Client";
            string login = "login";
            string pasword = "password";
            var mockUsersRepository = new Mock<IUsersRepository>();

            mockUsersRepository.Setup(repo => repo.IsUserExist(phone, role, login, pasword)).Returns(true);

            var addUserLogic = new AddUserLogic(mockUsersRepository.Object)
            {
                Login = login,
                Password = pasword,
                Phone = phone,
                Role = Roles.Client,

            };

            Assert.ThrowsException<HostelException>(() => addUserLogic.CheckDataCommand.Execute(null), 
                "HostelException должен быть сгенерирован, если пользователь существует");

        }

        [TestMethod]
        public void CheckDataCommand_UserDoesNotExist_CallsSendCode()
        {
            string phone = "phone";
            string role = "Client";
            string login = "login";
            string pasword = "password";
            var mockUsersRepository = new Mock<IUsersRepository>();

            mockUsersRepository.Setup(repo => repo.IsUserExist(phone, role, login, pasword)).Returns(false);

            var addUserLogic = new AddUserLogic(mockUsersRepository.Object)
            {
                Login = login,
                Password = pasword,
                Phone = phone,
                Role = Roles.Client,

            };

            addUserLogic.CheckDataCommand.Execute(null);

        }

        [TestMethod]
        public void ValidateAndAddUser_RightCode()
        {
            var mockUserRepository = new Mock<IUsersRepository>();
            var addUserLogic = new AddUserLogic(mockUserRepository.Object)
            {
                Firstname = "Firstname",
                Lastname = "Lastname",
                Patronymic = "Patronymic",
                Phone = "Phone",
                Login = "Login",
                Password = "Password"
            };
            string codeSent = addUserLogic.generateCode();
            addUserLogic.Code = codeSent;

            addUserLogic.CheckCodeCommand.Execute(null);

            mockUserRepository.Verify(repo => repo.Add(It.Is<User>(user =>
                user.Firstname == "Firstname" &&
                user.Lastname == "Lastname" &&
                user.Patronymic == "Patronymic" &&
                user.Phone == "Phone" &&
                user.Login == "Login" &&
                user.Password == "Password"
            )), Times.Once);

        }

        [TestMethod]
        public void ValidateAndAddUser_WrongCode()
        {
            var mockUserRepository = new Mock<IUsersRepository>();
            var addUserLogic = new AddUserLogic(mockUserRepository.Object)
            {
                Firstname = "Firstname",
                Lastname = "Lastname",
                Patronymic = "Patronymic",
                Phone = "Phone",
                Login = "Login",
                Password = "Password"
            };
            string codeSent = addUserLogic.generateCode();
            addUserLogic.Code = "dgdgdg";



            Assert.ThrowsException<HostelException>(() => addUserLogic.CheckCodeCommand.Execute(null), 
                "HostelException должен быть сгенерирован, если неверный код");
            mockUserRepository.Verify(repo => repo.Add(It.Is<User>(user =>
                user.Firstname == "Firstname" &&
                user.Lastname == "Lastname" &&
                user.Patronymic == "Patronymic" &&
                user.Phone == "Phone" &&
                user.Login == "Login" &&
                user.Password == "Password"
            )), Times.Never);

        }

    }
}
