using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer
{
    public interface IUsersRepository : IRepository<User>
    {
        public bool IsUserExist(string phone, string role, string login, string password);

        public List<User> GetClient(string firstname, string role);

        public User? GetByLoginPass(string login, string password);
    }
}
