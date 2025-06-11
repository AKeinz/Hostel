using Model;

namespace DatabaseLayer
{
    public class UsersRepository : IUsersRepository
    {
        public void Add(User entity)
        {
            using (var db = new HostelDBContext())
            {
                db.Users.Add(entity);
                db.SaveChanges();
            }
        }

        public void Delete(User entity)
        {
            using (var db = new HostelDBContext())
            {
                db.Users.Remove(entity);
                db.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            List<User> list;
            using (var db = new HostelDBContext())
            {
                list = db.Users.OrderBy(u => u.Id).ToList();
            }
            return list;
        }

        public User? GetById(int id)
        {
            using (var db = new HostelDBContext())
            {
                return db.Users.Find(id);
            }
        }

        public void Update(User entity)
        {
            using (var db = new HostelDBContext())
            {
                var user = db.Users.Find(entity.Id);
                db.Entry(user).CurrentValues.SetValues(entity);
                db.SaveChanges();
            }
        }

        public bool IsUserExist(string phone, Roles role, string login, string password)
        {


            List<User> users = new List<User>();
            using (var db = new HostelDBContext())
            {
                users = db.Users.Where(u => (u.Phone.Equals(phone) && u.Role.Equals(role)) ||
                (u.Password.Equals(password) && u.Login.Equals(login))).ToList();
            }
            if (users.Count > 0) { return true; }
            else { return false; }
        }


        public User? GetByLoginPass(string login, string password)
        {
            List<User>? users;
            using (var db = new HostelDBContext())
            {
                users = db.Users.Where(u => u.Login.Equals(login) && u.Password.Equals(password)).ToList();
                if (users.Count > 0)
                {
                    return users[0];
                }
            }
            return null;
        }

        public int GetMaxId()
        {
            List<User>? users;
            using (var db = new HostelDBContext())
            {
                return db.Users.Max(u => u.Id);
            }
        }
    }
}
