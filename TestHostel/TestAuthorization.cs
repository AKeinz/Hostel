
using Logic;

namespace TestHostel
{
    [TestClass]
    public class TestAuthorization
    {
        private AuthorizationLogic authorization;


        [TestMethod]
        public void GetAdmin()
        {
            authorization = new AuthorizationLogic()
            {
                Login = "admin",
                Password = "admin"
            };
            bool result = authorization.GetUser();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetNotExistUser()
        {
            authorization = new AuthorizationLogic()
            {
                Login = "notEXIST",
                Password = "43434"
            };
            bool result = authorization.GetUser();
            Assert.IsFalse(result);
        }
    }
}