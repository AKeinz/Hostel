using DatabaseLayer;
using Logic;
using Model;
using Moq;

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
    }
}
