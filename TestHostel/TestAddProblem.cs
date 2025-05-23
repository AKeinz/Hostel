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
    public class TestAddProblem
    {
        [TestMethod]
        public void addProblemByClient()
        {
            var mockProblemsRepository = new Mock<IRepository<Problem>>();
            var mockOrdersRepository = new Mock<IOrdersRepository>();

            mockOrdersRepository.Setup(r => r.GetCurrentRoom(123)).Returns(456);

            var addProblemLogic = new AddProblemLogic(mockProblemsRepository.Object, mockOrdersRepository.Object)
            {
                Client_id = 123,
                Description = "Test Description"
            };

            addProblemLogic.AddProblemCommand.Execute(null);


            mockProblemsRepository.Verify(r => r.Add(It.Is<Problem>(p =>
                p.Room == 456 && 
                p.Description == "Test Description" && 
                p.User == 123 
            )), Times.Once);

        }

        [TestMethod]
        public void addProblemByNotClient()
        {
            var mockProblemsRepository = new Mock<IRepository<Problem>>();
            var mockOrdersRepository = new Mock<IOrdersRepository>();

            mockOrdersRepository.Setup(r => r.GetCurrentRoom(123)).Returns(-1);

            var addProblemLogic = new AddProblemLogic(mockProblemsRepository.Object, mockOrdersRepository.Object)
            {
                Client_id = 123,
                Description = "Test Description"
            };

            addProblemLogic.AddProblemCommand.Execute(null);


            mockProblemsRepository.Verify(r => r.Add(It.Is<Problem>(p =>
                p.Room == -1 &&
                p.Description == "Test Description" &&
                p.User == 123
            )), Times.Never);

        }

    }
}
