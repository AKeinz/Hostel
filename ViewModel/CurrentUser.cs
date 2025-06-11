using DatabaseLayer;
using Model;

namespace ViewModel
{
    public class CurrentUser
    {

        public static User GetUser(int id)
        {
            UsersRepository usersRepository = new UsersRepository();
            return usersRepository.GetById(id) ?? new User() { Firstname = "Неизвестно" };
        }
    }
}
