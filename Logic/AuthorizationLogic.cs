using DatabaseLayer;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Logic
{
    public class AuthorizationLogic : INotifyPropertyChanged
    {
        private string login = "";
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
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

        public ICommand GetUserCommand { get; set; }
        public ICommand DisplayMainCommand { get; set; }
        public ICommand DisplayRegistrationCommand { get; set; }
        private IUsersRepository usersRepository = new UsersRepository();

        public AuthorizationLogic()
        {
            GetUserCommand = new RelayCommand<object>(GetUser);
            DisplayMainCommand = new RelayCommand<object>(DisplayAdmin);
            DisplayRegistrationCommand = new RelayCommand<object>(DisplayRegistration);
        }
        public AuthorizationLogic(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
            GetUserCommand = new RelayCommand<object>(GetUser);
            DisplayMainCommand = new RelayCommand<object>(DisplayAdmin);
            DisplayRegistrationCommand = new RelayCommand<object>(DisplayRegistration);
        }

        private void GetUser(object param)
        {
            GetUser();
        }

        public bool GetUser()
        {
            User? user = usersRepository.GetByLoginPass(Login, Password);
            if (user is null)
            {
                NotifyWrongUser?.Invoke("Пользователь не найден");
                return false;
            }
            switch (user.Role)
            {
                case Roles.Admin:
                    DisplayAdmin(user.Id);
                    return true;
                case Roles.Client:
                    DisplayClient(user.Id);
                    return true;
                case Roles.Service:
                    DisplayService(user.Id);
                    return true;
                case Roles.Reception:
                    DisplayReception(user.Id);
                    return true;
            }
            return false;
        }

        private void DisplayRegistration(object param) { NotifyRegistration?.Invoke(); }

        private void DisplayAdmin(object param) { NotifyAdmin?.Invoke((int)param); }
        private void DisplayClient(object param) { NotifyClient?.Invoke((int)param); }
        private void DisplayReception(object param) { NotifyReception?.Invoke((int)param); }
        private void DisplayService(object param) { NotifyService?.Invoke((int)param); }

        public delegate void AuthHandler(int id);
        public delegate void RegistHandler();
        public delegate void AuthErrorsHandler(string message);
        public event RegistHandler? NotifyRegistration;
        public event AuthHandler? NotifyClient;
        public event AuthHandler? NotifyReception;
        public event AuthHandler? NotifyAdmin;
        public event AuthHandler? NotifyService;
        public event AuthErrorsHandler? NotifyWrongUser;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
