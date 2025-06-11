using Model;
using System.Data;

namespace DatabaseLayer
{
    public class OrdersRepository : IOrdersRepository
    {
        public void Add(Order entity)
        {
            entity.In_day = entity.In_day.Date;
            entity.Out_day = entity.Out_day.Date;
            using (var db = new HostelDBContext())
            {
                db.Orders.Add(entity);
                db.SaveChanges();
            }
        }

        public void Delete(Order entity)
        {
            using (var db = new HostelDBContext())
            {
                db.Orders.Remove(entity);
                db.SaveChanges();
            }
        }

        public IEnumerable<Order> GetAll()
        {
            List<Order> list = new List<Order>();
            using (var db = new HostelDBContext())
            {
                list = db.Orders.OrderByDescending(o => o.Id).ToList();
            }
            return list;
        }

        public Order? GetById(int id)
        {
            using (var db = new HostelDBContext())
            {
                return db.Orders.Find(id);
            }
        }

        public void Update(Order entity)
        {
            using (var db = new HostelDBContext())
            {
                var order = db.Orders.Find(entity.Id);
                db.Entry(order).CurrentValues.SetValues(entity);
                db.SaveChanges();
            }
        }

        public List<Order> GetByDateAndRoom(int room, DateTime in_day, DateTime out_day)
        {
            using (var db = new HostelDBContext())
            {
                return db.Orders.Where((or) => (or.Room == room) && 
                (((out_day <= or.Out_day) && (in_day >= or.In_day)) ||
                ((out_day >= or.Out_day) && (in_day <= or.In_day)) ||
                 ((in_day >= or.In_day) && (in_day <= or.Out_day)) ||
                 ((out_day >= or.In_day) && (out_day <= or.Out_day))
                )).ToList();
            }
        }

        public List<Order> GetByDateAndClient(int client, DateTime in_day, DateTime out_day)
        {
            using (var db = new HostelDBContext())
            {
                return db.Orders.Where((or) => (or.Client == client) &&
               (((out_day <= or.Out_day) && (in_day >= or.In_day)) ||
                ((out_day >= or.Out_day) && (in_day <= or.In_day)) ||
                 ((in_day >= or.In_day) && (in_day <= or.Out_day)) ||
                 ((out_day >= or.In_day) && (out_day <= or.Out_day))
                )).ToList();
            }
        }

        public List<Order> GetByDateAndRoom(int id, int room, DateTime in_day, DateTime out_day)
        {
            using (var db = new HostelDBContext())
            {
                return db.Orders.Where((or) => (or.Room == room) && (or.Id != id) &&
                (((out_day <= or.Out_day) && (in_day >= or.In_day)) ||
                ((out_day >= or.Out_day) && (in_day <= or.In_day)) ||
                 ((in_day >= or.In_day) && (in_day <= or.Out_day)) ||
                 ((out_day >= or.In_day) && (out_day <= or.Out_day))
                )).ToList();
            }
        }

        public List<Order> GetByDateAndClient(int id, int client, DateTime in_day, DateTime out_day)
        {
            using (var db = new HostelDBContext())
            {
                return db.Orders.Where((or) => (or.Client == client) && (or.Id != id) &&
               (((out_day <= or.Out_day) && (in_day >= or.In_day)) ||
                ((out_day >= or.Out_day) && (in_day <= or.In_day)) ||
                 ((in_day >= or.In_day) && (in_day <= or.Out_day)) ||
                 ((out_day >= or.In_day) && (out_day <= or.Out_day))
                )).ToList();
            }
        }

        public int GetCurrentRoom(int client)
        {
            using (var db = new HostelDBContext())
            {
                Order? order = null;
                try
                {
                    order = db.Orders.Where((or) => (or.Client == client) && (((or.In_day <= DateTime.Now.ToLocalTime()) && (DateTime.Now >= or.Out_day.ToLocalTime())) || (or.Status.Equals(OrderStatuses.InProgress)))).ToList().First();
                }
                //Order? order= db.Orders.Where((or) => (or.Client == client) && (or.In_day.ToUniversalTime() >= DateTime.Now.ToUniversalTime()) && (DateTime.Now.ToUniversalTime() >= or.Out_day.ToUniversalTime())).ToList().First();
                catch
                {
                    return -1;
                }
                if (order is null) return -1;
                return order.Room;
            }
        }
    }
}
