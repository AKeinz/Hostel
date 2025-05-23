using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Password = "admin"
            };
            registration.IsUserExist();
            Assert.IsFalse(registration.IsUserExist());
        }

        [TestMethod]
        public void CheckRightCode()
        {
            registration = new RegistrationLogic();
            string codeSent = registration.generateCode();
            registration.Code = codeSent;
            Assert.IsTrue(registration.isRightCode());
        }

        [TestMethod]
        public void CheckWrongCode()
        {
            registration = new RegistrationLogic();
            string codeSent = "djddkfjdkfj";
            registration.Code = codeSent;

            registration.generateCode();

            Assert.IsFalse(registration.isRightCode());
        }
    }
}
