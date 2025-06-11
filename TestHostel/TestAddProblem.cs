using DatabaseLayer;
using Logic;
using Model;
using Moq;

namespace TestHostel
{
    [TestClass]
    public class TestAddProblem
    {
        [TestMethod]
        public void addProblemByClient()
        {
            var mockProblemsRepository = new Mock<IRepository<Problem>>();
            var mockOrdersRepository = new Mock<IOrdersRepository>();
            var mockUsersRepository = new Mock<IUsersRepository>();

            int id = 123;

            mockOrdersRepository.Setup(r => r.GetCurrentRoom(123)).Returns(456);
            mockUsersRepository.Setup(r => r.GetById(id)).Returns(new User() { Role = Roles.Client });

            var addProblemLogic = new AddProblemLogic(mockProblemsRepository.Object, mockOrdersRepository.Object, mockUsersRepository.Object)
            {
                Client_id = id,
                Description = "Test Description",
                Room_number = 456
            };

            addProblemLogic.AddProblem();


            mockProblemsRepository.Verify(r => r.Add(It.Is<Problem>(p =>
                p.Room == 456 &&
                p.Description == "Test Description" &&
                p.User == id
            )), Times.Once);

        }

        [TestMethod]
        public void addProblemByNotClient()
        {
            var mockProblemsRepository = new Mock<IRepository<Problem>>();
            var mockOrdersRepository = new Mock<IOrdersRepository>();
            var mockUsersRepository = new Mock<IUsersRepository>();

            int id = 123;
            mockUsersRepository.Setup(r => r.GetById(id)).Returns(new User() { Role = Roles.Client });

            mockOrdersRepository.Setup(r => r.GetCurrentRoom(id)).Returns(-1);

            var addProblemLogic = new AddProblemLogic(mockProblemsRepository.Object, mockOrdersRepository.Object, mockUsersRepository.Object)
            {
                Client_id = id,
                Description = "Test Description"
            };

            addProblemLogic.AddProblem();


            mockProblemsRepository.Verify(r => r.Add(It.Is<Problem>(p =>
                p.Room == -1 &&
                p.Description == "Test Description" &&
                 p.User == id
            )), Times.Never);

        }

    }
}
