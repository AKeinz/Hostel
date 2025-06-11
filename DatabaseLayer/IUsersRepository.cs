using Model;

namespace DatabaseLayer
{
    public interface IUsersRepository : IRepository<User>
    {
        public bool IsUserExist(string phone, Roles role, string login, string password);


        public User? GetByLoginPass(string login, string password);
        public int GetMaxId();

    }
}
