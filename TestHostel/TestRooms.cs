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
    public class TestRooms
    {
        [TestMethod]
        public void GetRooms()
        {
            var roomsRepository = new Mock<IRepository<Room>>();

            roomsRepository.Setup(r => r.GetAll()).Returns([
                new Room(),
                new Room(),
                new Room()
            ]);
            var Logic = new RoomsLogic(roomsRepository.Object);

            var rooms = Logic.Rooms;

            Assert.AreEqual(3, rooms.Count);
        }
    }
}
