using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DatabaseLayer
{
    public class OrdersRepository : IOrdersRepository
    {
        public void Add(Order entity)
        {
            using (var db = new HostelDBContext())
            {
                db.Orders.Add(entity);   
            }
        }

        public void Delete(Order entity)
        {
            using (var db = new HostelDBContext())
            {
                db.Orders.Remove(entity);
            }
        }

        public IEnumerable<Order> GetAll()
        {
            using (var db = new HostelDBContext())
            {
                return db.Orders;
            }
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
                var order = db.Orders.Find(entity.Order_id);
                db.Entry(order).CurrentValues.SetValues(entity);
                db.SaveChanges();
            }
        }
        
        public List<Order> GetByDateAndRoom(int room, DateTime in_day, DateTime out_day)
        {
            using (var db = new HostelDBContext())
            {
                return db.Orders.Where((or)=>(or.Room == room) && (!(out_day <= or.In_day) || !(in_day >= or.Out_day))).ToList();
            }
        }

        public List<Order> GetByDateAndClient(int client, DateTime in_day, DateTime out_day)
        {
            using (var db = new HostelDBContext())
            {
                return db.Orders.Where((or) => (or.Client == client) && (!(out_day <= or.In_day) || !(in_day >= or.Out_day))).ToList();
            }
        }

        public int GetCurrentRoom(int client)
        {
            using (var db = new HostelDBContext())
            {
                Order? order= db.Orders.Where((or) => (or.Client == client) && (or.In_day >= DateTime.Now) && (DateTime.Now >= or.Out_day)).ToList().First();
                if (order is null) return -1;
                return order.Room;
            }
        }
    }
}
