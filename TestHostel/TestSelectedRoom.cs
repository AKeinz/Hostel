using DatabaseLayer;
using Logic;
using Model;
using Moq;

namespace TestHostel
{
    [TestClass]
    public class TestSelectedRoom
    {
        [TestMethod]
        public void changeRoom_RightPhoto()
        {
            int number = 1;
            string photopath = AppDomain.CurrentDomain.BaseDirectory + "PhotosForTest\\Кот.jpg";
            string endphotopath = AppDomain.CurrentDomain.BaseDirectory + "RoomPhotos\\1\\1.jpg";
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

            changeRoomLogic.ChangeRoom(changeRoomLogic.Room);

            Room room = changeRoomLogic.Room;
            mockRoomsRepository.Verify(r => r.Update(It.Is<Room>(p =>
                p.Room_number == number &&
                p.Photo == endphotopath &&
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

            Assert.ThrowsException<HostelException>(() => changeRoomLogic.ChangeRoom(changeRoomLogic.Room),
                "HostelException должен быть сгенерирован, если пользователь существует");
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

            changeRoomLogic.DeleteRoom(changeRoomLogic.Room);


            mockRoomsRepository.Verify(r => r.Delete(It.Is<Room>(p =>
                p.Room_number == number &&
                p.Photo == photopath &&
                p.Capacity == capacity
            )), Times.Once);

        }
    }
}
