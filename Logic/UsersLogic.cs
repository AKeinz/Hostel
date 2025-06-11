using DatabaseLayer;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Logic
{
    public class UsersLogic : INotifyPropertyChanged
    {
        public readonly IUsersRepository UsersRepository;

        private User selectedUser;
        public User SelectedUser
        {
            get { return selectedUser; }
            set { selectedUser = value; OnPropertyChanged(nameof(SelectedUser)); }
        }


        private List<User> users = new List<User>() { new User() { Firstname = "hfhfh" } };
        public List<User> Users
        {
            get { return users; }
            set { users = value; OnPropertyChanged(nameof(Users)); }
        }

        private List<User> clients = new List<User>() { new User() { Firstname = "hfhfh" } };
        public List<User> Clients
        {
            get { return clients; }
            set { clients = value; OnPropertyChanged(nameof(Clients)); }
        }


        public UsersLogic(IUsersRepository usersRepository)
        {
            UsersRepository = usersRepository;
            GetUsers();
            GetClients();
        }

        public UsersLogic()
        {
            UsersRepository = new UsersRepository();
            GetUsers();
            GetClients();
        }

        public List<User> GetUsers()
        {
            Users = UsersRepository.GetAll().ToList();
            return Users;
        }

        public List<int> GetUserIds()
        {
            List<int> ids = new List<int>();
            foreach (User user in Users)
            {
                ids.Add(user.Id);
            }
            return ids;
        }

        public List<User> GetClients()
        {
            GetUsers();
            List<User> clients = new List<User>();
            foreach (User user in Users)
            {
                if (user.Role.Equals(Roles.Client))
                    clients.Add(user);
            }
            Clients = clients;
            return clients;
        }

        public List<int> GetClietnIds()
        {
            List<int> ids = new List<int>();
            foreach (User user in Clients)
            {
                ids.Add(user.Id);
            }
            return ids;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
