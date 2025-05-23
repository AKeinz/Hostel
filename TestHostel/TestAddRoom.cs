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
    public class TestAddRoom
    {
        [TestMethod]
        public void addRoom_RightPhoto()
        {
            int number = 1;
            string photopath = AppDomain.CurrentDomain.BaseDirectory + "\\PhotosForTest\\Кот.jpg";
            int capacity = 3;

            var mockRoomsRepository = new Mock<IRepository<Room>>();

            var addRoomLogic = new AddRoomLogic(mockRoomsRepository.Object)
            {
                Photo = photopath,
                Room_number = number,
                Capacity = capacity
            };

            addRoomLogic.AddRoomCommand.Execute(null);


            mockRoomsRepository.Verify(r => r.Add(It.Is<Room>(p =>
                p.Room_number == number &&
                p.Photo == photopath &&
                p.Capacity == capacity
            )), Times.Once);

        }

        [TestMethod]
        public void addRoom_WrongPhoto()
        {
            int number = 1;
            string photopath = "C:\\Users\\";
            int capacity = 3;

            var mockRoomsRepository = new Mock<IRepository<Room>>();

            var addRoomLogic = new AddRoomLogic(mockRoomsRepository.Object)
            {
                Photo = photopath,
                Room_number = number,
                Capacity = capacity
            };

            addRoomLogic.AddRoomCommand.Execute(null);


            mockRoomsRepository.Verify(r => r.Add(It.Is<Room>(p =>
                p.Room_number == number &&
                p.Photo == photopath &&
                p.Capacity == capacity
            )), Times.Never);

        }
    }
}
