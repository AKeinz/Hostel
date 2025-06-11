using Model;

namespace DatabaseLayer
{
    public class RoomsRepository : IRoomsRepository
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
                list = db.Rooms.OrderBy(r => r.Room_number).ToList();
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
                var user = db.Rooms.Find(entity.Room_number);
                db.Entry(user).CurrentValues.SetValues(entity);
                db.SaveChanges();
            }
        }

        public int GetMaxId()
        {
            using (var db = new HostelDBContext())
            {
                return db.Rooms.Max(u => u.Room_number);
            }
        }
    }
}
