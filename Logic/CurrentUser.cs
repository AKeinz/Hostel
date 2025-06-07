using DatabaseLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class CurrentUser
    {
        public User User { get; set; }
        public CurrentUser(int id)
        {
            GetCurrentUser(id);
        }

        private void GetCurrentUser(int id)
        {
            UsersRepository usersRepository = new UsersRepository();
            User = usersRepository.GetById(id) ?? new User() { Firstname = "Неизвестно" };
        }
    }
}
