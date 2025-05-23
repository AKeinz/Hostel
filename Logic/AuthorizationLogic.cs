using DatabaseLayer;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic
{
    public class AuthorizationLogic : INotifyPropertyChanged
    {
        private int user_id;
        private string loginOrPhone="";
        public string LoginOrPhone
        {
            get { return loginOrPhone; }
            set
            {
                loginOrPhone = value;
                OnPropertyChanged("LoginOrPhone");
            }
        }
        private string password = "";
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public ICommand GetUserCommand {get; set;}
        public ICommand DisplayMainCommand { get; set; }
        public ICommand DisplayRegistrationCommand {get; set; }
        private IUsersRepository usersRepository = new UsersRepository();

        public AuthorizationLogic()
        {
            GetUserCommand = new RelayCommand(GetUser);
            DisplayMainCommand = new RelayCommand(DisplayMain);
            DisplayRegistrationCommand = new RelayCommand(DisplayRegistration);
        }
        public AuthorizationLogic(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
            GetUserCommand = new RelayCommand(GetUser);
            DisplayMainCommand = new RelayCommand(DisplayMain);
            DisplayRegistrationCommand = new RelayCommand(DisplayRegistration);
        }

        private void GetUser(object param)
        {
            GetUser();
        }

        public bool GetUser()
        {
            User? user = usersRepository.GetByLoginPass(LoginOrPhone, Password);
            if (user != null)
            {
                DisplayMain(user.Id);
                return true;
            }
            return false;
        }

        private void DisplayRegistration(object param) { NotifyRegistration?.Invoke(); }

        private void DisplayMain(object param) { NotifyMain?.Invoke(); }

        public delegate void AuthHandler();
        public event AuthHandler? NotifyRegistration;
        public event AuthHandler? NotifyMain;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
