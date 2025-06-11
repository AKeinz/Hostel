using DatabaseLayer;
using Logic;
using Model;
using Moq;

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
            mockUsersRepository.Setup(repo => repo.IsUserExist(Phone, Role, Login, Password))
                .Returns(true);

            AddUserLogic addUserLogic = new AddUserLogic(mockUsersRepository.Object)
            {
                User = new User()
                {
                    Role = Role,
                    Phone = Phone,
                    Login = Login,
                    Password = Password
                }
            };

            bool result = addUserLogic.IsUserExist();

            mockUsersRepository.Verify(re => re.IsUserExist(Phone, Role, Login, Password), Times.Once);
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void CheckDataCommand_UserExists()
        {
            string phone = "phone";
            Roles role = Roles.Client;
            string login = "login";
            string pasword = "password";
            var mockUsersRepository = new Mock<IUsersRepository>();

            mockUsersRepository.Setup(repo => repo.IsUserExist(phone, role, login, pasword)).Returns(true);

            var addUserLogic = new AddUserLogic(mockUsersRepository.Object)
            {
                User = new User()
                {
                    Login = login,
                    Password = pasword,
                    Phone = phone,
                    Role = Roles.Client,
                }
            };

            Assert.ThrowsException<HostelException>(() => addUserLogic.checkData(addUserLogic.User),
                "HostelException должен быть сгенерирован, если пользователь существует");

        }

        [TestMethod]
        public void CheckDataCommand_UserDoesNotExist_CallsSendCode()
        {
            string phone = "phone";
            Roles role = Roles.Client;
            string login = "login";
            string pasword = "password";
            var mockUsersRepository = new Mock<IUsersRepository>();

            mockUsersRepository.Setup(repo => repo.IsUserExist(phone, role, login, pasword)).Returns(false);

            var addUserLogic = new AddUserLogic(mockUsersRepository.Object)
            {
                User = new User()
                {
                    Login = login,
                    Password = pasword,
                    Phone = phone,
                    Role = Roles.Client,
                }
            };

            addUserLogic.checkData(addUserLogic.User);

        }

        [TestMethod]
        public void ValidateAndAddUser()
        {
            var mockUserRepository = new Mock<IUsersRepository>();
            var addUserLogic = new AddUserLogic(mockUserRepository.Object)
            {
                User = new User()
                {
                    Firstname = "Firstname",
                    Lastname = "Lastname",
                    Patronymic = "Patronymic",
                    Phone = "Phone",
                    Login = "Login",
                    Password = "Password"
                }
            };

            addUserLogic.checkData(addUserLogic.User);

            mockUserRepository.Verify(repo => repo.Add(It.Is<User>(user =>
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
