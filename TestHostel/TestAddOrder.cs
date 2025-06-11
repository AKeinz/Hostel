using DatabaseLayer;
using Logic;
using Model;
using Moq;

namespace TestHostel
{
    [TestClass]
    public class TestAddOrder
    {
        [TestMethod]
        public void GetRoomNumbers()
        {
            var mockRoomsRepository = new Mock<IRepository<Room>>();
            var mockOrdersRepository = new Mock<IOrdersRepository>();

            mockRoomsRepository.Setup(r => r.GetAll()).Returns([
                new Room(){Room_number = 0 },
                new Room(){Room_number = 1 },
                new Room(){Room_number = 2 },
                new Room(){Room_number = 3 },
                new Room(){Room_number = 4 },
            ]);
            var addOrderLogic = new AddOrderLogic(mockOrdersRepository.Object, mockRoomsRepository.Object);

            List<int> numbers = addOrderLogic.getRoomNumbers();

            mockRoomsRepository.Verify(r => r.GetAll());
            Assert.AreEqual(5, numbers.Count);
        }

        [TestMethod]
        public void isRoomAvailable_Available()
        {
            var mockRoomsRepository = new Mock<IRepository<Room>>();
            var mockOrdersRepository = new Mock<IOrdersRepository>();

            int roomNumber = 0;
            DateTime ind = DateTime.Now.AddDays(-2);
            DateTime outd = DateTime.Now.AddDays(-1);
            mockOrdersRepository.Setup(r => r.GetByDateAndRoom(roomNumber, ind, outd)).Returns([
                new Order()
            ]);
            mockRoomsRepository.Setup(r => r.GetById(roomNumber)).Returns(
                new Room() { Room_number = roomNumber, Status = RoomStatuses.Free}
            );
            var addOrderLogic = new AddOrderLogic(mockOrdersRepository.Object, mockRoomsRepository.Object);

            List<Order>? numbers = addOrderLogic.isRoomAvailable(roomNumber, ind, outd);

            Assert.AreEqual(1, numbers.Count);
            Assert.AreNotEqual(numbers.Count, 0);

        }

        [TestMethod]
        public void isRoomAvailable_Busy()
        {
            var mockRoomsRepository = new Mock<IRepository<Room>>();
            var mockOrdersRepository = new Mock<IOrdersRepository>();

            int roomNumber = 0;
            DateTime ind = DateTime.Now.AddDays(-2);
            DateTime outd = DateTime.Now.AddDays(+1);
            mockOrdersRepository.Setup(r => r.GetByDateAndRoom(roomNumber, ind, outd)).Returns([]);
            mockRoomsRepository.Setup(r => r.GetById(roomNumber)).Returns(
               new Room() { Room_number = roomNumber, Status = RoomStatuses.Busy }
           );
            var addOrderLogic = new AddOrderLogic(mockOrdersRepository.Object, mockRoomsRepository.Object);

            List<Order> numbers = addOrderLogic.isRoomAvailable(roomNumber, ind, outd);

            Assert.AreEqual(numbers.Count, 0);

        }

        [TestMethod]
        public void isClientAvailable_Available()
        {
            var mockRoomsRepository = new Mock<IRepository<Room>>();
            var mockOrdersRepository = new Mock<IOrdersRepository>();

            int client = 0;
            DateTime ind = DateTime.Now.AddDays(-2);
            DateTime outd = DateTime.Now.AddDays(+1);
            mockOrdersRepository.Setup(r => r.GetByDateAndClient(client, ind, outd)).Returns([new Order()]);
            var addOrderLogic = new AddOrderLogic(mockOrdersRepository.Object, mockRoomsRepository.Object);

            List<Order> numbers = addOrderLogic.isClientAvailable(client, ind, outd);

            Assert.AreEqual(1, numbers.Count);
            Assert.AreNotEqual(numbers.Count, 0);

        }

        [TestMethod]
        public void isClientAvailable_Busy()
        {
            var mockRoomsRepository = new Mock<IRepository<Room>>();
            var mockOrdersRepository = new Mock<IOrdersRepository>();

            int client = 0;
            DateTime ind = DateTime.Now.AddDays(-2);
            DateTime outd = DateTime.Now.AddDays(+1);
            mockOrdersRepository.Setup(r => r.GetByDateAndClient(client, ind, outd)).Returns([]);
            var addOrderLogic = new AddOrderLogic(mockOrdersRepository.Object, mockRoomsRepository.Object);

            List<Order> numbers = addOrderLogic.isClientAvailable(client, ind, outd);

            Assert.AreEqual(numbers.Count, 0);

        }

        [TestMethod]
        public void addOrder_Success()
        {
            var mockRoomsRepository = new Mock<IRepository<Room>>();
            var mockOrdersRepository = new Mock<IOrdersRepository>();

            int client = 0;
            DateTime ind = DateTime.Now.AddDays(-2);
            DateTime outd = DateTime.Now.AddDays(+1);

            mockOrdersRepository.Setup(r => r.GetByDateAndClient(client, ind, outd)).Returns([]);
            mockOrdersRepository.Setup(r => r.GetByDateAndRoom(client, ind, outd)).Returns([]);
            mockRoomsRepository.Setup(r => r.GetById(456)).Returns(
                new Room() { Room_number = 456, Status = RoomStatuses.Free }
            );

            var addOrderLogic = new AddOrderLogic(mockOrdersRepository.Object, mockRoomsRepository.Object);
            Order order = new Order()
            {
                Room = 456,
                In_day = ind,
                Out_day = outd,
                Client = client,
                Number_of_people = 1
            };
            addOrderLogic.AddOrder(order);


            mockOrdersRepository.Verify(r => r.Add(It.Is<Order>(p =>
                p.Room == 456 &&
                p.In_day == ind &&
                p.Out_day == outd &&
                p.Client == client &&
                p.Number_of_days == (outd - ind).Days &&
                p.Number_of_people == 1
            )), Times.Once);

        }

        [TestMethod]
        [ExpectedException(typeof(HostelException))]
        public void addOrder_ClientBusy()
        {
            var mockRoomsRepository = new Mock<IRepository<Room>>();
            var mockOrdersRepository = new Mock<IOrdersRepository>();

            int client = 0;
            DateTime ind = DateTime.Now.AddDays(-2);
            DateTime outd = DateTime.Now.AddDays(+1);

            mockOrdersRepository.Setup(r => r.GetByDateAndClient(client, ind, outd)).Returns([new Order()]);
            mockOrdersRepository.Setup(r => r.GetByDateAndRoom(client, ind, outd)).Returns([]);
            mockRoomsRepository.Setup(r => r.GetById(456)).Returns(
               new Room() { Room_number = 456, Status = RoomStatuses.Free }
           );

            var addOrderLogic = new AddOrderLogic(mockOrdersRepository.Object, mockRoomsRepository.Object);
            Order order = new Order()
            {
                Room = 456,
                In_day = ind,
                Out_day = outd,
                Client = client,
                Number_of_people = 1
            };

            addOrderLogic.AddOrder(order);


            mockOrdersRepository.Verify(r => r.Add(It.Is<Order>(p =>
                p.Room == 456 &&
                p.In_day == ind &&
                p.Out_day == outd &&
                p.Client == client &&
                p.Number_of_days == (outd - ind).Days &&
                p.Number_of_people == 1
            )), Times.Never);

        }

        [TestMethod]
        [ExpectedException(typeof(HostelException))]
        public void addOrder_RoomBusy()
        {
            var mockRoomsRepository = new Mock<IRepository<Room>>();
            var mockOrdersRepository = new Mock<IOrdersRepository>();

            int room = 0;
            DateTime ind = DateTime.Now.AddDays(-2).Date;
            DateTime outd = DateTime.Now.AddDays(+1).Date;

            mockOrdersRepository.Setup(r => r.GetByDateAndClient(room, ind, outd)).Returns([]);
            mockOrdersRepository.Setup(r => r.GetByDateAndRoom(room, ind, outd)).Returns([new Order()]);
            mockRoomsRepository.Setup(r => r.GetById(room)).Returns(
               new Room() { Room_number = room, Status = RoomStatuses.Busy }
           );

            var addOrderLogic = new AddOrderLogic(mockOrdersRepository.Object, mockRoomsRepository.Object);
            Order order = new Order()
            {
                Room = room,
                In_day = ind,
                Out_day = outd,
                Client = 12,
                Number_of_people = 1
            };

            addOrderLogic.AddOrder(order);


            mockOrdersRepository.Verify(r => r.Add(It.Is<Order>(p =>
                p.Room == room &&
                p.In_day == ind &&
                p.Out_day == outd &&
                p.Client == 12 &&
                p.Number_of_days == (outd - ind).Days &&
                p.Number_of_people == 1
            )), Times.Never);

        }
    }
}
