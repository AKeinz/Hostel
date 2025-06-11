using DatabaseLayer;
using Logic;
using Model;
using Moq;

namespace TestHostel
{
    [TestClass]
    public class TestChangeOrder
    {
        [TestMethod]
        public void changeOrder()
        {
            var mockOrdersRepository = new Mock<IOrdersRepository>();

            var changeOrderLogic = new ChangeOrderLogic(mockOrdersRepository.Object)
            {
                Order = new Order() { Client = 1, Room = 7 },
            };

            changeOrderLogic.changeOrder(changeOrderLogic.Order);


            mockOrdersRepository.Verify(r => r.Update(It.Is<Order>(p =>
                p.Client == 1 && p.Room == 7
            )), Times.Once);

        }

        [TestMethod]
        public void deleteOrder()
        {
            var mockOrdersRepository = new Mock<IOrdersRepository>();

            var changeOrderLogic = new ChangeOrderLogic(mockOrdersRepository.Object)
            {
                Order = new Order() { Client = 1, Room = 7 },
            };

            changeOrderLogic.deleteOrder(changeOrderLogic.Order);


            mockOrdersRepository.Verify(r => r.Delete(It.Is<Order>(p =>
                p.Client == 1 && p.Room == 7
            )), Times.Once);

        }
    }
}
