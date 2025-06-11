using Model;

namespace DatabaseLayer
{
    public interface IOrdersRepository : IRepository<Order>
    {
        public List<Order> GetByDateAndRoom(int room, DateTime in_day, DateTime out_day);

        public List<Order> GetByDateAndClient(int client, DateTime in_day, DateTime out_day);
        public List<Order> GetByDateAndRoom(int id, int room, DateTime in_day, DateTime out_day);

        public List<Order> GetByDateAndClient(int id, int client, DateTime in_day, DateTime out_day);

        public int GetCurrentRoom(int client);
    }
}
