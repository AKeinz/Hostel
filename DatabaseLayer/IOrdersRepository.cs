using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer
{
    public interface IOrdersRepository : IRepository<Order>
    {
        public List<Order> GetByDateAndRoom(int room, DateTime in_day, DateTime out_day);

        public List<Order> GetByDateAndClient(int client, DateTime in_day, DateTime out_day);

        public int GetCurrentRoom(int client);
    }
}
