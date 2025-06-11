using DatabaseLayer;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Logic
{
    public class AddUserLogic : BaseLogic, INotifyPropertyChanged
    {
        public readonly IUsersRepository usersRepository;

        private User user;
        public User User
        {
            get { return user; }
            set { user = value; OnPropertyChanged(nameof(User)); }
        }


        public ICommand CheckDataCommand { get; set; }

        public AddUserLogic(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
            CheckDataCommand = new RelayCommand<object>(checkData);

        }

        public AddUserLogic()
        {
            this.usersRepository = new UsersRepository();
            CheckDataCommand = new RelayCommand<object>(checkData);

        }

        public void checkData(object p)
        {
            User = (User)p;
            if (IsUserExist())
            {
                throw new HostelException("Пользователь уже существует");
            }
            else
            {
                addUser(User);
            }
        }

        public bool IsUserExist()
        {
            return usersRepository.IsUserExist(User.Phone, User.Role, User.Login, User.Password);
        }


        public void addUser(User user)
        {
            user.Id = usersRepository.GetMaxId() + 1;
            usersRepository.Add(user);

        }



        public event PropertyChangedEventHandler PropertyChanged;
        public event BaseLogic.ErrorsHandler? NotifyError;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
