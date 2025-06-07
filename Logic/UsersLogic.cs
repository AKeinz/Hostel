using DatabaseLayer;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic
{
    public class UsersLogic : INotifyPropertyChanged
    {
        public readonly IUsersRepository UsersRepository;

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

        public ICommand AddUserCommand { get; set; }
        public ICommand ChangeUserCommand { get; set; }

        public UsersLogic(IUsersRepository usersRepository)
        {
            UsersRepository = usersRepository;
            getUsers();
            getClients();
            AddUserCommand = new RelayCommand(displayAddUser);
            ChangeUserCommand = new RelayCommand(displayChangeUser);
        }

        public UsersLogic()
        {
            UsersRepository = new UsersRepository();
            getUsers();
            getClients();
            AddUserCommand = new RelayCommand(displayAddUser);
            ChangeUserCommand = new RelayCommand(displayChangeUser);
        }

        private List<User> getUsers()
        {
            return UsersRepository.GetAll().ToList();
        }

        private List<User> getClients()
        {
            List<User> clients = new List<User>();
            foreach(User user in Users)
            {
                if (user.Role.Equals(Roles.Client))
                    clients.Add(user);
            }
            Clients = clients;
            return clients;
        }

        private void displayAddUser(object param)
        {

        }
        private void displayChangeUser(object param)
        {

        }// что за shoqMessage

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
