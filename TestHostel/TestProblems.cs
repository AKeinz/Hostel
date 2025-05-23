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
    public class TestProblems
    {
        [TestMethod]
        public void GetProblems()
        {
            var probRepository = new Mock<IRepository<Problem>>();
            var roomRepository = new Mock<IRepository<Room>>();

            probRepository.Setup(r => r.GetAll()).Returns([
                new Problem(),
            ]);
            roomRepository.Setup(r => r.GetAll()).Returns([
                new Room(){Room_number = 1},
            ]);

            var Logic = new ProblemsLogic(probRepository.Object, roomRepository.Object);

            var probs = Logic.Problems;

            Assert.AreEqual(1, probs.Count);
        }

        [TestMethod]
        public void AddProblem()
        {
            int room = 12;
            int user = 23;
            string desc = "Test";
            var mockProblemsRepository = new Mock<IRepository<Problem>>();
            var roomRepository = new Mock<IRepository<Room>>();

            var Logic = new ProblemsLogic(mockProblemsRepository.Object, roomRepository.Object)
            {
                Room = room,
                User_id = user,
                Description = desc
            };

            Logic.AddProblemCommand.Execute(null);


            mockProblemsRepository.Verify(r => r.Add(It.Is<Problem>(p =>
                p.Room == room &&
                p.Description == desc &&
                p.User == user
            )), Times.Once);

        }
    }
}
