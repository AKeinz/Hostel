using DatabaseLayer;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Logic
{
    public class RegistrationLogic : INotifyPropertyChanged
    {
        public readonly IUsersRepository usersRepository;

        private string lastname = "";
        private string firstname = "";
        private string patronymic = "";
        private string phone = "";
        private string login = "";
        private string password = "";

        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; OnPropertyChanged(nameof(Lastname)); }
        }
        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; OnPropertyChanged(nameof(Firstname)); }
        }
        public string Patronymic
        {
            get { return patronymic; }
            set { patronymic = value; OnPropertyChanged(nameof(Patronymic)); }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; OnPropertyChanged(nameof(Phone)); }
        }
        public string Login
        {
            get { return login; }
            set { login = value; OnPropertyChanged(nameof(Login)); }
        }
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(nameof(Password)); }
        }

        public ICommand CheckDataCommand { get; set; }
        public ICommand closeCommand { get; set; }

        public RegistrationLogic()
        {
            this.usersRepository = new UsersRepository();
            CheckDataCommand = new RelayCommand<object>(checkData);
            closeCommand = new RelayCommand<object>(Close);
        }
        public RegistrationLogic(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
            CheckDataCommand = new RelayCommand<object>(checkData);
        }

        private void checkData(object param)
        {
            try
            {
                if (IsUserExist())
                {
                    showMessage("Пользователь уже существует");
                    return;
                }
                addUser();
                showMessage("Успешно");
                closeCommand.Execute(this);
            }
            catch 
            {
                showMessage("не удалось сохранить изменения");
            }
        }


        public bool IsUserExist()
        {
            return usersRepository.IsUserExist(Phone, Roles.Client, Login, Password);
        }
       

        private void addUser()
        {
            usersRepository.Add(new User()
            {
                Id = usersRepository.GetMaxId() + 1,
                Firstname = Firstname,
                Lastname = Lastname,
                Patronymic = Patronymic,
                Phone = Phone,
                Login = Login,
                Password = Password,
            });
        }

        private void showMessage(string message)
        {
            NotifyMessage?.Invoke(message);
        }

        private void Close(object a)
        {
            NotifyClose?.Invoke();
        }


        public delegate void AuthHandler();
        public delegate void AuthErrorsHandler(string message);
        public event AuthHandler? NotifyClose;
        public event AuthErrorsHandler? NotifyMessage;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
