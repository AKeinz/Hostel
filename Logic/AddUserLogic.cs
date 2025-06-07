using DatabaseLayer;
using MessageController;
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
    public class AddUserLogic : INotifyPropertyChanged
    {
        public readonly IUsersRepository usersRepository;

        private Roles role;
        private string lastname;
        private string firstname;
        private string patronymic;
        private string phone;
        private string login = "";
        private string password = "";
        private string code = "";

        public Roles Role
        {
            get { return role; }
            set { role = value; OnPropertyChanged(nameof(Role)); }
        }
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
        public string Phone
        {
            get { return phone; }
            set { phone = value; OnPropertyChanged(nameof(Phone)); }
        }
        public string Code
        {
            get { return code; }
            set { code = value; OnPropertyChanged(nameof(Code)); }
        }
        private string sentCode;
        public ICommand CheckDataCommand { get; set; }
        public ICommand CheckCodeCommand { get; set; }
        private IMessageContrroller messageController;

        public AddUserLogic(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
            CheckDataCommand = new RelayCommand(checkData);
            CheckCodeCommand = new RelayCommand(checkCode);
            messageController = new MessageController.MessageController();
        }

        private void checkData(object p)
        {
            if (IsUserExist())
            {
                throw new HostelException("Пользователь уже существует");
            }
            else
            {
                sendCode();
            }
        }

        public bool IsUserExist()
        {
            return usersRepository.IsUserExist(Phone, Role, Login, Password);
        }
        private void sendCode()
        {
            string sentCode = generateCode();
            messageController.SendMessage(Phone, Code);
            displayShowCode();
        }

        public string generateCode()
        {
            Random random = new Random();
            int code = random.Next(1000, 9999);
            sentCode = code.ToString();
            return code.ToString();
        }
        private void checkCode(object param)
        {
            if (isRightCode())
            {
                addUser(new User()
                {
                    Firstname = Firstname,
                    Lastname = Lastname,
                    Patronymic = Patronymic,
                    Phone = Phone,
                    Login = Login,
                    Password = Password
                });
            }
            else
            {
                throw new HostelException("Неверный код");
            }
        }

        public bool isRightCode()
        {
            return Code.Equals(sentCode);
        }

        public void addUser(User user)
        {
            usersRepository.Add(user);

            //блок для вывода сообщений
        }

        private void displayShowCode()
        {

        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
