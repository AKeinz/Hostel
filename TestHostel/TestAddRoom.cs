using DatabaseLayer;
using Logic;
using Model;
using Moq;

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
            string endphotopath = AppDomain.CurrentDomain.BaseDirectory + "RoomPhotos\\1\\1.jpg";
            int capacity = 3;

            var mockRoomsRepository = new Mock<IRoomsRepository>();

            var addRoomLogic = new AddRoomLogic(mockRoomsRepository.Object)
            {
                New_room = new Room()
                {
                    Photo = photopath,
                    Room_number = number,
                    Capacity = capacity
                }
            };

            addRoomLogic.AddRoom(addRoomLogic.New_room);


            mockRoomsRepository.Verify(r => r.Add(It.Is<Room>(p =>
                p.Room_number == number &&
                p.Photo == endphotopath &&
                p.Capacity == capacity
            )), Times.Once);

        }

        [TestMethod]
        public void addRoom_WrongPhoto()
        {
            int number = 1;
            string photopath = "C:\\Users\\";
            int capacity = 3;

            var mockRoomsRepository = new Mock<IRoomsRepository>();

            var addRoomLogic = new AddRoomLogic(mockRoomsRepository.Object)
            {
                New_room = new Room()
                {
                    Photo = photopath,
                    Room_number = number,
                    Capacity = capacity
                }
            };

            Assert.ThrowsException<HostelException>(() => addRoomLogic.AddRoom(addRoomLogic.New_room),
                "HostelException должен быть сгенерирован, если пользователь существует");
            mockRoomsRepository.Verify(r => r.Add(It.Is<Room>(p =>
                p.Room_number == number &&
                p.Photo == photopath &&
                p.Capacity == capacity
            )), Times.Never);

        }
    }
}
