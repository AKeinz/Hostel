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

        private List<User> users = new List<User>();
        public List<User> Users
        {
            get { return users; }
            set { users = value; OnPropertyChanged(nameof(Users)); }
        }

        public ICommand AddUserCommand { get; set; }
        public ICommand ChangeUserCommand { get; set; }

        public UsersLogic(IUsersRepository usersRepository)
        {
            UsersRepository = usersRepository;
            Users = getUsers();
            AddUserCommand = new RelayCommand(displayAddUser);
            ChangeUserCommand = new RelayCommand(displayChangeUser);
        }

        private List<User> getUsers()
        {
            return UsersRepository.GetAll().ToList();
        }
        private void displayAddUser(object param)
        {

        }
        private void displayChangeUser(object param)
        {

        }// что за shoqMessage

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
