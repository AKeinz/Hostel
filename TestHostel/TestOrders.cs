using DatabaseLayer;
using Logic;
using Model;
using Moq;

namespace TestHostel
{
    [TestClass]
    public class TestOrders
    {
        [TestMethod]
        public void GetOrders()
        {
            var ordersRepository = new Mock<IOrdersRepository>();

            ordersRepository.Setup(r => r.GetAll()).Returns([
                new Order(),
                new Order()
            ]);
            var usersLogic = new OrdersLogic(ordersRepository.Object);

            var ors = usersLogic.Orders;

            Assert.AreEqual(2, ors.Count);
        }
    }
}
