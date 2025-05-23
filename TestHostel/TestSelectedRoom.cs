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
    public class TestSelectedRoom
    {
        [TestMethod]
        public void changeRoom_RightPhoto()
        {
            int number = 1;
            string photopath = AppDomain.CurrentDomain.BaseDirectory + "\\PhotosForTest\\Кот.jpg";
            int capacity = 3;

            var mockRoomsRepository = new Mock<IRepository<Room>>();

            var changeRoomLogic = new SelectedRoomLogic(mockRoomsRepository.Object)
            {
                Room = new Room()
                {
                    Photo = photopath,
                    Room_number = number,
                    Capacity = capacity
                }
            };

            changeRoomLogic.UpdateRoomCommand.Execute(null);


            mockRoomsRepository.Verify(r => r.Update(It.Is<Room>(p =>
                p.Room_number == number &&
                p.Photo == photopath &&
                p.Capacity == capacity
            )), Times.Once);

        }

        [TestMethod]
        public void changeRoom_WrongPhoto()
        {
            int number = 1;
            string photopath = "C:\\Users\\";
            int capacity = 3;

            var mockRoomsRepository = new Mock<IRepository<Room>>();

            var changeRoomLogic = new SelectedRoomLogic(mockRoomsRepository.Object)
            {
                Room = new Room()
                {
                    Photo = photopath,
                    Room_number = number,
                    Capacity = capacity
                }
            };

            changeRoomLogic.UpdateRoomCommand.Execute(null);


            mockRoomsRepository.Verify(r => r.Update(It.Is<Room>(p =>
                p.Room_number == number &&
                p.Photo == photopath &&
                p.Capacity == capacity
            )), Times.Never);

        }

        [TestMethod]
        public void deleteRoom()
        {
            int number = 1;
            string photopath = AppDomain.CurrentDomain.BaseDirectory + "\\PhotosForTest\\Кот.jpg";
            int capacity = 3;

            var mockRoomsRepository = new Mock<IRepository<Room>>();

            var changeRoomLogic = new SelectedRoomLogic(mockRoomsRepository.Object)
            {
                Room = new Room()
                {
                    Photo = photopath,
                    Room_number = number,
                    Capacity = capacity
                }
            };

            changeRoomLogic.DeleteRoomCommand.Execute(null);


            mockRoomsRepository.Verify(r => r.Delete(It.Is<Room>(p =>
                p.Room_number == number &&
                p.Photo == photopath &&
                p.Capacity == capacity
            )), Times.Once);

        }
    }
}
