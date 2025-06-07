using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer
{
    public class RoomsRepository : IRepository<Room>
    {
        public void Add(Room entity)
        {
            using (var db = new HostelDBContext())
            {
                db.Rooms.Add(entity);
                db.SaveChanges();
            }
        }

        public void Delete(Room entity)
        {
            using (var db = new HostelDBContext())
            {
                db.Rooms.Remove(entity);
                db.SaveChanges();
            }
        }

        public IEnumerable<Room> GetAll()
        {
            List<Room> list;
            using (var db = new HostelDBContext())
            {
                list =  db.Rooms.ToList();
            }
            return list;
        }

        public Room? GetById(int id)
        {
            using (var db = new HostelDBContext())
            {
                return db.Rooms.Find(id);
            }
        }

        public void Update(Room entity)
        {
            using (var db = new HostelDBContext())
            {
                var user = db.Users.Find(entity.Room_number);
                db.Entry(user).CurrentValues.SetValues(entity);
                db.SaveChanges();
            }
        }
    }
}
