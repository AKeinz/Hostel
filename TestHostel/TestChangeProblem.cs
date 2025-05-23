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
    public class TestChangeProblem
    {
        [TestMethod]
        public void changeProblem()
        {
            int user = 1;
            int room = 77;
            var mockProblemsRepository = new Mock<IRepository<Problem>>();

            var changeProblemLogic = new ChangeProblemLogic(mockProblemsRepository.Object)
            {
                Problem = new Problem() {Room = room , User = user},
            };

            changeProblemLogic.UpdateProblemCommand.Execute(null);


            mockProblemsRepository.Verify(r => r.Update(It.Is<Problem>(p =>
                p.User == user && p.Room == room
            )), Times.Once);

        }

        [TestMethod]
        public void deleteProblem()
        {
            int user = 1;
            int room = 77;
            var mockProblemsRepository = new Mock<IRepository<Problem>>();

            var changeProblemLogic = new ChangeProblemLogic(mockProblemsRepository.Object)
            {
                Problem = new Problem() { Room = room, User = user },
            };

            changeProblemLogic.DeleteProblemCommand.Execute(null);


            mockProblemsRepository.Verify(r => r.Delete(It.Is<Problem>(p =>
                p.User == user && p.Room == room
            )), Times.Once);

        }
    }
}
