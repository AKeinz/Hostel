using Logic;

namespace TestHostel
{
    [TestClass]
    public class TestRegistration
    {
        private RegistrationLogic registration;


        [TestMethod]
        public void CheckExistedLoginAndPassword()
        {
            registration = new RegistrationLogic()
            {
                Login = "admin",
                Password = "admin"
            };
            Assert.IsTrue(registration.IsUserExist());
        }

        [TestMethod]
        public void CheckNonExisted()
        {
            registration = new RegistrationLogic()
            {
                Login = "admina",
                Password = "admin",
                Phone = "9232938283",
            };
            registration.IsUserExist();
            Assert.IsFalse(registration.IsUserExist());
        }

    }
}
